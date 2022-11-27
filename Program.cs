using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

var client = new HttpClient();
var repsonse = client.GetAsync("https://apicr.minzdrav.gov.ru/api.ashx?op=GetClinrec2&id=724_1&ssid=undefined").Result
    .Content.ReadAsStringAsync().Result;

System.Console.WriteLine("response are gaved");
System.Console.WriteLine(repsonse != null);
File.WriteAllText("response.json", repsonse);

var jObj = JObject.Parse(repsonse);
var content = (string)((JArray)jObj["obj"]["sections"]).First(obj => (string)obj["title"] == "Термины и определения")["content"];
var csv = HtmlToCsv.HtmlToCsvParser.Parse(content);
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
File.WriteAllLines("result.csv", csv, Encoding.GetEncoding("windows-1251"));