using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<CodeValueResponse>> GetAsync()
        {
            var result = await context.CodeValues
                .AsNoTracking()
                .Select(c => new CodeValueResponse(c.Id, c.Code, c.Value))
                .ToListAsync();

            return result;
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
