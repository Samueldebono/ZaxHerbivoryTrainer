using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace EasyPT.API.ActionConstraints
{
    [AttributeUsage(AttributeTargets.All,Inherited = true,AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute:Attribute, IActionConstraint
    {
        private readonly MediaTypeCollection _mediaType = new MediaTypeCollection();
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string mediaType,
            params string[] otherMediaTypes)
        {
            _requestHeaderToMatch =
                requestHeaderToMatch ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaTypeValue))
            {
                _mediaType.Add(parsedMediaTypeValue);

            }
            else
            {
                throw new ArgumentException(nameof(mediaType));
            }

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType, out MediaTypeHeaderValue parsedOtherMediaTypeValue))
                {
                    _mediaType.Add(parsedOtherMediaTypeValue);
                }
                else
                {
                    throw new ArgumentException(nameof(otherMediaType));
                }
            }

        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey(_requestHeaderToMatch)) return false;

            var parsedRequestedMediaType = new MediaType(requestHeaders[_requestHeaderToMatch]);

            //if one of the media types matches, return true

            foreach (var mediaType in _mediaType)
            {
                var parsedMediaType = new MediaType(mediaType);
                if (parsedRequestedMediaType.Equals(parsedMediaType)) return true;
            }

            return false;
        }

        public int Order => 0;
    }
}
