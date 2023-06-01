using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Models;

public class Host
{
    public bool spectatable { get; set; }
    public bool started { get; set; }
    public string ip { get; set; }
    public string host_name { get; set; }
    public string host_character { get; set; }
    public string host_country { get; set; }
    public string host_challonge { get; set; }
    public string client_name { get; set; }
    public string client_character { get; set; }
    public string client_country { get; set; }
    public string client_challonge { get; set; }
    public bool ranked { get; set; }
    public bool autopunch { get; set; }
    public int spectators { get; set; }
    public double start { get; set; }
    public string message { get; set; }
}
