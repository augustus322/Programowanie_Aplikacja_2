using AvaloniaApplication1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models;
public class Backend
{
    public HttpClient Client{ get; set; }
    public Backend()
    {
        Client = new HttpClient();
        Client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
    }
    //fetch list
    public async Task<List<Host>> fetch()
        {
            var content = await Client.GetStringAsync("https://konni.delthas.fr/games");
            List<Host> hostlist = JsonSerializer.Deserialize<List<Host>>(content);
            return hostlist;
        }

        #region filter methods
        public List<Host> CountryFilter(List<Host> hostlist, List<string> filter)
        {
            var tempList = new List<Host>();

            foreach (var host in hostlist)
            {
                if (!filter.Contains(host.host_country))
                {
                    tempList.Add(host);
                }
            }
            return tempList;
        }

        public List<Host> OnlyJoinableFilter(List<Host> hostlist, bool joinable)
        {
            var tempList = new List<Host>();

            foreach (var host in hostlist)
            {
                if (joinable == true && host.client_name == "")
                {
                    tempList.Add(host);
                }
                else if (joinable == false)
                {
                    tempList.Add(host);
                }
            }
            return tempList;
        }

        public List<Host> MessageFilter(List<Host> hostlist, string message)
        {
            var tempList = new List<Host>();

            foreach (var host in hostlist)
            {
                if (host.message.Contains(message))
                {
                    tempList.Add(host);
                }
            }
            return tempList;
        }
        #endregion
}