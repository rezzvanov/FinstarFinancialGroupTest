using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Requests;
using SimpleDataApi.Responses;
using SimpleDataApi.Storage;

namespace SimpleDataApi.Services
{
    public class CodeValuesService : ICodeValuesService
    {
        private readonly AppDbContext context;

        public CodeValuesService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<(IReadOnlyCollection<CodeValueResponse>, int)> GetAsync(CodeValueFilter filter)
        {
            var query = context.CodeValues
                .AsNoTracking()
                .AddFilterIfSet(filter.Code.HasValue, c => c.Code == filter.Code)
                .AddFilterIfSet(!string.IsNullOrEmpty(filter.ValuePrefix), c => c.Value.StartsWith(filter.ValuePrefix!));

            var count = await query.CountAsync();
            var result = await query
                .SelectPage(filter.PageSize, filter.PageNumber)
                .Select(c => new CodeValueResponse(c.Id, c.Code, c.Value))
                .ToListAsync();

            return (result, count);
        }

        public async Task<int> AddRangeAsync(IEnumerable<CodeValueRequest> records)
        {
            int addedRows = 0;
            var codeValues = MapDtoToOrderedEntities(records).ToList();

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(codeValues);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return addedRows;
        }

        private static IEnumerable<CodeValue> MapDtoToOrderedEntities(IEnumerable<CodeValueRequest> records)
        {
            return records
                .Select(c => new CodeValue(c.Code, c.Value))
                .OrderBy(c => c.Code);
        }
    }
}
