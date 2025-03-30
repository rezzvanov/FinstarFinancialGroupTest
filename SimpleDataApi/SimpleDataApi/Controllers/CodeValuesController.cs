using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDataApi.Extensions;
using SimpleDataApi.Requests;
using SimpleDataApi.Responses;
using SimpleDataApi.Storage;

namespace SimpleDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeValuesController : ControllerBase
    {
        private readonly AppDbContext context;

        public CodeValuesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<PagedResponse<CodeValueResponse>> GetAsync([FromQuery] PagedRequest request)
        {
            var query = context.CodeValues
                .AsNoTracking();

            var count = await query.CountAsync();

            var result = await query
                .SelectPage(request.PageSize, request.PageNumber)
                .Select(c => new CodeValueResponse(c.Id, c.Code, c.Value))
                .ToListAsync();

             return new PagedResponse<CodeValueResponse>(result, count);
        }

        [HttpPost]
        public async Task<IActionResult> AddRangeAsync([FromBody] CodeValuesRequest records)
        {
            int addedRows = 0;
            IEnumerable<CodeValue> codeValues = MapRequestToEntities(records.CodeValues);

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE dbo.CodeValues");
                await context.CodeValues.AddRangeAsync(codeValues);
                addedRows = await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return Ok($"Successfully saved {addedRows} rows");
        }

        private static IEnumerable<CodeValue> MapRequestToEntities(IEnumerable<CodeValueRequest> records)
        {
            return records
                .OrderBy(c => c.Code)
                .Select(c => new CodeValue(c.Code, c.Value));
        }
    }
}
