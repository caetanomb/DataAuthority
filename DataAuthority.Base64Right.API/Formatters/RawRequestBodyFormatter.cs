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
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == "text/plain" ||
                contentType == "application/octet-stream" || contentType == "application/json")
                return true;

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || contentType == "text/plain"
                || contentType == "application/json")
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();
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
    }
}
