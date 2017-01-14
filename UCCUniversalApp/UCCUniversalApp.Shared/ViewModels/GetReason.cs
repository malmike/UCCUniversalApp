using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCCUniversalApp.Model;

namespace UCCUniversalApp.ViewModels
{
    class GetReason
    {
        public string DataElements(String json)
        {
            Reasons reason = new Reasons();
            try
            {

                var rootObject = JsonConvert.DeserializeObject<ReasonRootObject>(json);
                foreach (Reasons f in rootObject.data)
                {
                    reason = f;
                    break;
                }
          
                return reason.Reason;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
