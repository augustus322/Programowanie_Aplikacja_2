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
            //Console.WriteLine(content);
            List<Host> hostlist = JsonSerializer.Deserialize<List<Host>>(content);
            return hostlist;
        }

        //var content = await client.GetStringAsync("https://konni.delthas.fr/games");
        //Console.WriteLine(content);

        //Host l = JsonSerializer.Deserialize<Host>(content);

        //List<Host> hostlist = JsonSerializer.Deserialize<List<Host>>(content);
        //Console.WriteLine(hostlist.Count);

        //Console.WriteLine(hostlist[0].host_name);

        //var filterOptions = new Dictionary<string, int>()
        //{
        //	{"host_countries",0},
        //	{"characters",0},
        //	{"autopunch",0}
        //};

        //var characterFilter = new List<string>();
        //characterFilter.Add("Alice");



        #region filter methods
        List<Host> CountryFilter(List<Host> hostlist, List<string> filter)
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

        List<Host> OnlyJoinableFilter(List<Host> hostlist, bool joinable)
        {
            var tempList = new List<Host>();

            foreach (var host in hostlist)
            {
                if (joinable == true && host.client_name == "")
                {
                    tempList.Add(host);
                }
                else
                {
                    tempList.Add(host);
                }
            }
            return tempList;
        }

        List<Host> MessageFilter(List<Host> hostlist, string message)
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

        void display(List<Host> hostslits)
        {
            var i = 1;

            // hostlist sort by (press button to sort)
            // hostslits.Sort((x, y) => string.Compare(x.host_country, y.host_country));

            #region filterVariables
            bool joinableOnly = false;

            string hostMessage = "";

            var contryFilter = new List<string>();
            contryFilter.Add("fr");
            contryFilter.Add("my");
            contryFilter.Add("us");
            #endregion

            var query = CountryFilter(hostslits, contryFilter);
            query = OnlyJoinableFilter(query, joinableOnly);
            query = MessageFilter(query, hostMessage);

            // query gets replaced by new filter in foreach where filter = item
            // lists for each filter -> list for characters, list for countires
            //query = hostslits.Where(a => a.host_country == characterFilter);
            // test
            //   foreach (var item in characterFilter)
            //{
            //	query = hostslits.Where(a => a.host_country == item);
            //}
            //

            foreach (var item in query)
            {
                Console.WriteLine("Host no.: " + i);
                foreach (var prop in item.GetType().GetProperties())
                {
                    Console.WriteLine(prop.Name + ": " + prop.GetValue(item));
                }
                i++;
                Console.WriteLine("=============================================");
            }
        }

        //IEnumerable<Host> openHosts(List<Host> hostslits)
        //{
        //	var query = hostslits.Where(a => a.client_name == "");
        //	return query;
        //}

        // main loop
        //while (true)
        //{
        //    List<Host> hosts = await fetch();
        //    display(hosts);
        //    Console.ReadLine();
        //    Console.Clear();
        //}
        }