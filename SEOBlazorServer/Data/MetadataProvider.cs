using System.Collections.Generic;

namespace SEOBlazorServer.Data
{
    public class MetadataProvider
    {
        public Dictionary<string, MetadataValue> RouteDetailMapping { get; set; } = new()
        {
            {
                "/",
                new()
                {
                    Title = "Blazor School Example",
                    Description = "Visit more at https://blazorschool.com"
                }
            },
            {
                "/about",
                new()
                {
                    Title = "About us",
                    Description = "Email us: dotnetprotech@gmail.com - The DotNetPro team."
                }
            }
        };
    }
}
