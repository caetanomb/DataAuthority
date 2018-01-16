using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataAuthority.Base64Right.API
{
    public class RawRequestBodyFormatter : InputFormatter
    {
        public RawRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));            
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType.Contains("text/plain") ||
                contentType.Contains("application/json"))
                return true;

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || contentType.Contains("text/plain") ||
                contentType.Contains("application/json"))
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();

                    if (IsContentValidBase64(content))
                    {
                        byte[] data = Convert.FromBase64String(content);
                        content = System.Text.Encoding.UTF8.GetString(data);
                    }

                    content = RemoveUnnecessaryChar(content);
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }

        private string RemoveUnnecessaryChar(string content)
        {
            return content.Replace("\n", string.Empty)
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty);
        }

        private bool IsContentValidBase64(string content)
        {
            try
            {                
                Convert.FromBase64String(content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
