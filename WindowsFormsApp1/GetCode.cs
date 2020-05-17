using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace MathpixCsharp
{
    public class DataOptions
    {
        public bool include_latex { get; set; }

        public bool include_mathml { get; set; }

        public DataOptions()
        {
            include_latex = true;
        }
    }

    public class RootObject
    {
        public string src { get; set; }
        public List<string> formats { get; set; }
        public DataOptions data_options { get; set; }

        public RootObject()
        {
            src = "";
            formats = new List<string>();
            data_options = new DataOptions();
        }

    }
    public class DataItem
    {
        public string type { get; set; }
        public string value { get; set; }
    }
    public class RespJson
    {
        public double confidence;
        public double confidence_rate;
        public List<DataItem> data { get; set; }

        public RespJson()
        {
            confidence = 0.0;
            confidence_rate = 0.0;
            data = new List<DataItem>();
        }
    }
    public class GetCode
    {
        private static readonly HttpClient client = new HttpClient();
        private string url = "https://api.mathpix.com/v3/text";
        RootObject Values = new RootObject();
        
        public GetCode() 
        {
            Values.formats.Add("data");
            Values.data_options.include_latex = true;
            Values.data_options.include_mathml = true;
        }

        public void SetImg(Bitmap bit)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bit.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = stream.ToArray();
            Values.src = "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
        }

        public async Task<List<string> > GetLatex()
        {
            List<string> retL = new List<string>();
            string ret = "";
            var contents =new StringContent (JsonConvert.SerializeObject(Values),Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("app_id", Properties.Settings.Default.id);
            client.DefaultRequestHeaders.Add("app_key", Properties.Settings.Default.key);
            try
            {
                HttpResponseMessage response = await client.PostAsync(url,contents);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //var tmp=JsonConvert.DeserializeObject<RespJson>(responseBody);
                ret = JsonConvert.DeserializeObject<RespJson>(responseBody).data[1].value;
                //var ret_b = new StringBuilder("$$"+ret+"$$");
                //ret_b[0] = ret_b[1] = '$';
                //ret_b[ret_b.Length-1] = ret_b[ret_b.Length-2] = '$';
                //ret = Convert.ToString(ret_b);
                retL.Add("$"+ret+"$");//Latex inline
                retL.Add("$$" + ret + "$$");//Latex Presentation
                ret = JsonConvert.DeserializeObject<RespJson>(responseBody).data[0].value;
                retL.Add(ret);//MathML
            }
            catch (Exception e)
            {
                MessageBox.Show("Http错误 :{0} ,请重试", e.Message);
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return retL;
        }

    }
}
