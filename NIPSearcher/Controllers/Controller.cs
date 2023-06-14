using Microsoft.AspNetCore.Mvc;
using NipSearcher.Entities;
using System.Text.Json.Nodes;
using System.Text.Json;
using NipSearcher;

namespace NIPSearcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {

        private readonly ILogger<ApiController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRepository _repository;

        public ApiController(ILogger<ApiController> logger, IHttpClientFactory httpClientFactory, IRepository repository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _repository = repository;
        }

        [HttpGet("gethistory")]
        public List<Subject> GetHistory()
        {
            return _repository.GetSubjects();
        }

        [HttpGet("getnip/{nip}")]
        public async Task<Subject?> GetNip(string nip)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var response = await httpClient.GetAsync(nip + "?date=" + DateTime.Now.ToString("yyyy-MM-dd"));
            var content = await response.Content.ReadAsStringAsync();

            JsonNode responseNode = JsonNode.Parse(content)!;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                JsonNode resultNode = responseNode?["result"]!;
                JsonNode subjectNode = resultNode?["subject"]!;
                JsonNode accountsNode = subjectNode?["accountNumbers"]!;

                var subject = subjectNode?.Deserialize<Subject>(options);

                if (subject != null)
                {
                    var Accounts = accountsNode.Deserialize<List<string>>(options)!.Select(e => new Account() { Number = e }).ToList();
                    subject.Accounts = Accounts;

                    _repository.SaveSubject(subject);
                }
                return subject;

            }
            else
            {
                return null;
            }
        }
    }
}
