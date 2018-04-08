using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Library.BLL;
using Felix.Messaging.Interfaces;
using Felix.Messaging.Messages.Reducers;
using Newtonsoft.Json;

namespace Felix.BarMessaging
{
    public static class BarMessagingContext
    {
        public static IReducerContext ReducerContext
        {
            get
            {
                var reducers = new Dictionary<string, IActionReducer>()
            {
                {"Bar.Add", new BarAddActionReducer() }
            };

                return new ReducerContext(reducers);
            }
        }
    }
   
    public class BarAddActionReducer : ActionReducer
    {
        private BarDomainObject BarObject = new BarDomainObject();

        public override async Task<string> Reduce(IAction action)
        {
            string stateString = string.Empty;

            try
            {
                BarAddPayload payload = JsonConvert.DeserializeObject<BarAddPayload>(action.Payload);

                await BarObject.Save(payload.Symbol, payload.Bar);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");

                Debugger.Break();
            }

            return stateString;
        }
    }

}

