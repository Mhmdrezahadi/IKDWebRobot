using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IKDWebRobot.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<IndexModel> _logger;
        public Root? ResponseContent { get; set; }
        public string? ResponseContentAsString { get; set; }


        [BindProperty]
        public Input Input { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            HttpClient _client = new HttpClient();

            _client.DefaultRequestHeaders.Add("authorization", "bearer " + Input.TokenValue);
            var model = new Model
            {
                IdRespon = 166525
            };
            HttpResponseMessage response;
            do
            {
                response = await _client.PostAsJsonAsync("https://esale.ikd.ir:400/sales/getRespons", model);
                Input.Seconds--;
            }
            while (!response.IsSuccessStatusCode && Input.Seconds >= 0);
            //ResponseContent = await response.Content.ReadFromJsonAsync<Root>();
            ResponseContentAsString = await response.Content.ReadAsStringAsync();
            //ResponseContent = System.Text.Json.JsonSerializer.Deserialize<Root>();
            //ResponseContent = JsonConvert.DeserializeObject<Root>(responseAsString);
            return Page();
        }
    }
    public class Model
    {
        public int IdRespon { get; set; }
    }
    public class Input
    {
        [Required]
        public string TokenValue { get; set; } = string.Empty;
        [Required]
        public int Seconds { get; set; } = 30;
    }
    public class Root
    {
        public int? statusResult { get; set; }
        public List<Row>? rows { get; set; }
    }

    public class Row
    {
        public int? id { get; set; }
        public string? Amount { get; set; }
        public string? CalculateDate { get; set; }
        public string? ReceiveDate { get; set; }
        public int? BenefitOk { get; set; }
        public string? DocumentNo { get; set; }
        public string? DocumentDate { get; set; }
        public string? TypePaymentTitle { get; set; }
        public string? CheckDate { get; set; }
        public string? VaziatVosol { get; set; }
        public string? FlagTitle { get; set; }
        public int? MandePayment { get; set; }
    }
}