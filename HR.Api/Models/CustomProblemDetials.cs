using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Models
{
    public class CustomProblemDetials : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    }
}
