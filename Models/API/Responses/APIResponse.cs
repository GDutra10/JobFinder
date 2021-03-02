using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models.API.Responses
{
    public class APIResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        public object Data { get; set; }

        public void AddError(string key, string message)
        {
            List<string> list;

            if (Errors.ContainsKey(key))
            {
                Errors.TryGetValue(key, out list);
                list.Add(message);
            }
            else
            {
                list = new List<string>();
                list.Add(message);
                Errors.Add(key, list);
            }
        }

    }
}
