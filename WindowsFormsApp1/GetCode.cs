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
        public int code;
        public string message;
        public int uses;
        public double confidence;
        public double confidence_rate;
        public string text;
        public List<DataItem> data { get; set; }

        public RespJson()
        {
            confidence = 0.0;
            confidence_rate = 0.0;
            text = "";
            data = new List<DataItem>();
        }
    }
    public class GetCode
    {
        private static readonly HttpClient client = new HttpClient();
        private string OfficialUrl = "https://api.mathpix.com/v3/text";
        private string QspUrl = "https://service-hzv8746l-1255970853.sh.apigw.tencentcs.com/release/mathcode";
        RootObject Values = new RootObject();
        
        public GetCode() 
        {
            Values.formats.Add("data");
            Values.formats.Add("text");
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
            bool isOfficial = Properties.Settings.Default.isOfficial;
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
                HttpResponseMessage response = await client.PostAsync(isOfficial?OfficialUrl:QspUrl,contents);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var tmp = JsonConvert.DeserializeObject<RespJson>(responseBody);
                //ret = JsonConvert.DeserializeObject<RespJson>(responseBody).data[1].value;
                if (!isOfficial&& tmp.code != 200)
                {
                    throw new Exception(tmp.message);
                }

                var ttt = tmp.text;
                ret = tmp.text.Replace(@"\)", "$").Replace(@"\(","$").Replace(@"\]", "$$").Replace(@"\[","$$");
                
                
                //var ret_b = new StringBuilder("$$"+ret+"$$");
                //ret_b[0] = ret_b[1] = '$';
                //ret_b[ret_b.Length-1] = ret_b[ret_b.Length-2] = '$';
                //ret = Convert.ToString(ret_b);
                retL.Add(ret);//Latex inline
                if (ret.StartsWith("$"))
                {
                    retL.Add("\\begin{equation}\n" + ret.Substring(1, ret.Length - 2) + "\n\\end{equation}");//Latex Presentation
                }
                else
                {
                    retL.Add("\\begin{equation}\n" + ret + "\n\\end{equation}");
                }
                if (tmp.data.Count != 0)
                {
                    ret = tmp.data[0].value;
                    retL.Add(ret);//MathML
                }
                else
                {
                    retL.Add("MathML功能目前只能识别单个公式，请准确地截取图片");
                }
                if (!isOfficial)
                {
                    retL.Add(tmp.uses.ToString());
                }
                else
                {
                    retL.Add("INF");
                }
            }
            catch (Exception e)
            {
                if (isOfficial)
                {
                    MessageBox.Show(e.Message, "Http错误 :{0} ,请准确截取公式，或检查Key是否正确");
                    // Console.WriteLine("\nException Caught!");
                    // Console.WriteLine("Message :{0} ", e.Message);
                }
                else
                {
                    MessageBox.Show(e.Message,"Error!");
                }
            }
            return retL;
        }

    }
}
