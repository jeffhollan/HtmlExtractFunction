using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ExtractHTMLFunction
{
    public class ExtractHTML
    {
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
        {
            string html = await req.Content.ReadAsStringAsync();
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var chunks = new List<string>();

            foreach (var item in doc.DocumentNode.DescendantsAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    
                    if (item.InnerText.Trim() != "" && !item.InnerText.StartsWith("<!--"))
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            return req.CreateResponse<string>(HttpStatusCode.OK, String.Join(" ", chunks));
        }
    }
}