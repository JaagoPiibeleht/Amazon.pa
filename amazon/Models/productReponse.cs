using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using amazon.AWSE;
using System.Configuration;

namespace amazon.Models
{
    public class productReponse
    {
        public IEnumerable<Item> GetProducts(string sona, int leht)
        {
            AWSECommerceServicePortTypeClient ecs = new AWSECommerceServicePortTypeClient();
            ItemSearchRequest paring = new ItemSearchRequest();
            paring.ResponseGroup = new string[] { "ItemAttributes,OfferSummary" };
            paring.SearchIndex = "All";
            paring.Keywords = sona;
            paring.ItemPage = String.Format("{0}", leht);
            paring.MinimumPrice = "0";

            ItemSearch otsi = new ItemSearch();
            otsi.Request = new ItemSearchRequest[] { paring };
            otsi.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            otsi.AssociateTag = ConfigurationManager.AppSettings["associateTag"];
            ItemSearchResponse vastus = ecs.ItemSearch(otsi);

            if (vastus == null)
            {
                throw new Exception("Server Error - didn't get any reponse from server!");
            }
            else if (vastus.OperationRequest.Errors != null)
            {
                throw new Exception(vastus.OperationRequest.Errors[0].Message);
            }
            else if (vastus.Items[0].Item == null)
            {                
                throw new Exception("Didn't get any items!Try agen with different keyword.");
            }
            else
            {
                return vastus.Items[0].Item;
            }
        }
    }
}