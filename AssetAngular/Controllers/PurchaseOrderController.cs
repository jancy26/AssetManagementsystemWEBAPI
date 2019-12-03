using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AssetAngular.Models;

namespace AssetAngular.Controllers
{
    public class PurchaseOrderController : ApiController
    {
        private assetMVCEFDBEntities7 db = new assetMVCEFDBEntities7();

        // GET: api/PurchaseOrder
        //public IQueryable<purchase_order> Getpurchase_order()
        //{
        //    return db.purchase_order;
        //}
        public PurchaseOrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        //public List<AssetViewModel> GetAssetType(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = true;

        //    List<asset_def> assetlist = db.asset_def.Where(x=>x.ad_type_id==id).ToList();
        //    List<AssetViewModel> viewlist = assetlist.Select(x => new AssetViewModel
        //    {
        //        ad_id = Convert.ToInt32(x.ad_id),
        //        ad_name = x.ad_name,
        //        ad_type = x.asset_type.at_name,
        //        ad_class = x.ad_class
        //     }).ToList();
        //    return viewlist;
        //}
        public List<PurchaseOrderModel> GetPurchaseorder()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<purchase_order> purchaselist = db.purchase_order.ToList();
            List<PurchaseOrderModel> viewlist = purchaselist.Select(x => new PurchaseOrderModel
            {
                pd_id=x.pd_id,
                pd_order_no=x.pd_order_no,
               assetname=x.asset_def.ad_name,
                assettype=x.asset_type.at_name,
                pd_qty=Convert.ToInt32(x.pd_qty),
                vendorname=x.vendor_creation.vd_name,
                pd_odatestr=x.pd_odatestr,
                pd_pdatestr=x.pd_pdatestr,
                pd_status=x.pd_status
             }).ToList();
            return viewlist;
        }
        public List<VendorViewModel> Getvendor_creation(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<vendor_creation> vendorlist = db.vendor_creation.Where(x => x.vd_atype_id == id).ToList();
            List<VendorViewModel> viewlist = vendorlist.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.vd_atype_id,
                vd_atype=x.asset_type.at_name,
                vd_fromstr = x.vd_fromstr,
                vd_tostr = x.vd_tostr,
                vd_addr = x.vd_addr


            }).ToList();
            return viewlist;
        }
        public List<asset_type> Getasset_Types(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            asset_def asset = db.asset_def.Where(x => x.ad_name == name).FirstOrDefault();
            List<asset_type> list = new List<asset_type>();
            if(asset!=null)
            {
                list = db.asset_type.Where(x => x.at_id == asset.ad_type_id).ToList();
            }
            return list;
        }
        //// GET: api/PurchaseOrder/5
        //[ResponseType(typeof(purchase_order))]
        //public IHttpActionResult Getpurchase_order(decimal id)
        //{
        //    purchase_order purchase_order = db.purchase_order.Find(id);
        //    if (purchase_order == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(purchase_order);
        //}

        // PUT: api/PurchaseOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpurchase_order(decimal id, purchase_order purchase_order)
        {
           
         if (id != purchase_order.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchase_order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!purchase_orderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseOrder
        [ResponseType(typeof(purchase_order))]
        public IHttpActionResult Postpurchase_order(purchase_order purchase_order)
        {
            purchase_order.pd_odate = DateTime.Now;
            db.purchase_order.Add(purchase_order);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/PurchaseOrder/5
        [ResponseType(typeof(purchase_order))]
        public IHttpActionResult Deletepurchase_order(decimal id)
        {
            purchase_order purchase_order = db.purchase_order.Find(id);
            if (purchase_order == null)
            {
                return NotFound();
            }

            db.purchase_order.Remove(purchase_order);
            db.SaveChanges();
            return Ok(purchase_order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool purchase_orderExists(decimal id)
        {
            return db.purchase_order.Count(e => e.pd_id == id) > 0;
        }
    }
}