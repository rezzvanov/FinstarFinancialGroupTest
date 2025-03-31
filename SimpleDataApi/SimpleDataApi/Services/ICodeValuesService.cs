using SimpleDataApi.Requests;
using SimpleDataApi.Responses;

namespace SimpleDataApi.Services
{
    public interface ICodeValuesService
    {
        public Task<(IReadOnlyCollection<CodeValueResponse>, int)> GetAsync(CodeValueFilter request);

        public Task<int> AddRangeAsync(IEnumerable<CodeValueRequest> records);
    }
}
