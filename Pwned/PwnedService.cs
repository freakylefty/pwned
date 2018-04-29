using System;

namespace Pwned
{
    enum Status { OK, Error, BadData }

    struct Request
    {
        public string Prefix;
        public string Hash;
        public string Uri;
    }

    struct Response
    {
        public Status Status;
        public string Message;
        public int Value;
    }

    static class PwnedService
    {
        const string Uri = "https://api.pwnedpasswords.com/range/{0}";

        public static Response GetCount(string input)
        {
            var result = new Response();
            if (string.IsNullOrEmpty(input))
            {
                result.Status = Status.BadData;
                result.Message = "Invalid value: " + input;
                return result;
            }
            var request = GetRequest(input);
            var response = GetResponse(request);

            return response;
        }

        static Response GetResponse(Request request)
        {
            var response = new Response();
            var data = "";
            try
            {
                data = HttpUtil.Get(request.Uri);
            }
            catch (Exception ex)
            {
                response.Status = Status.Error;
                response.Message = "Error calling Pwned Passwords: " + ex.Message;
                return response;
            }

            try
            {
                string[] lines = StringUtil.GetLines(data);
                foreach (var line in lines)
                {
                    var pair = StringUtil.GetPair(line);
                    var matchHash = request.Prefix + pair.Key;
                    if (matchHash == request.Hash)
                    {
                        response.Value = pair.Value;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error parsing Pwned Passwords response: " + ex.Message;
                response.Status = Status.Error;
            }
            return response;
        }

        static Request GetRequest(string input)
        {
            var request = new Request();
            request.Hash = StringUtil.Hash(input);
            request.Prefix = request.Hash.Substring(0, 5);
            request.Uri = string.Format(Uri, request.Prefix);

            return request;
        }
    }
}
