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
    public class AssetMasterOrderController : ApiController
    {
        private assetMVCEFDBEntities7 db = new assetMVCEFDBEntities7();

        // GET: api/AssetMasterOrder


        public AssetMasterOrderController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public List<PurchaseOrderModel> GetPurchaseorder()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<purchase_order> purchaselist = db.purchase_order.Where(x=>x.pd_status=="Consignment Received").ToList();
            List<PurchaseOrderModel> viewlist = purchaselist.Select(x => new PurchaseOrderModel
            {
                pd_id = x.pd_id,
                pd_order_no = x.pd_order_no,
                pd_ad_id = x.asset_def.ad_id,
               assetname= x.asset_def.ad_name,
                pd_odatestr = x.pd_odatestr,
                pd_pdatestr = x.pd_pdatestr,
                pd_qty = Convert.ToInt32(x.pd_qty),
                pd_status = x.pd_status,
                pd_type_id = x.asset_type.at_id,
                assettype = x.asset_type.at_name,
                pd_vendor_id = x.pd_vendor_id,
                vendorname = x.vendor_creation.vd_name

            }).ToList();
            return viewlist;
        }
        // GET: api/AssetMasterOrderView/5
        [ResponseType(typeof(asset_master))]
        public PurchaseOrderModel GetAsset_master(string ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            purchase_order x = db.purchase_order.Where(y => y.pd_order_no == ordno).FirstOrDefault();
            PurchaseOrderModel pvModel = new PurchaseOrderModel();

            if (x == null)
            {

            }

            else
            {
                pvModel.pd_id = x.pd_id;
                pvModel.pd_order_no = x.pd_order_no;
                pvModel.pd_ad_id = x.asset_def.ad_id;
                pvModel.assetname = x.asset_def.ad_name;
                pvModel.pd_odatestr = x.pd_odatestr;
                pvModel.pd_pdatestr = x.pd_pdatestr;
                pvModel.pd_qty = Convert.ToInt32(x.pd_qty);
                pvModel.pd_status = x.pd_status;
                pvModel.pd_type_id = x.asset_type.at_id;
                pvModel.assettype = x.asset_type.at_name;
                pvModel.pd_vendor_id = x.pd_vendor_id;
                pvModel.vendorname = x.vendor_creation.vd_name;
            }

            return pvModel;
        }

        // GET: api/AssetMasterOrder/5
        //[ResponseType(typeof(asset_master))]
        //public IHttpActionResult Getasset_master(decimal id)
        //{
        //    asset_master asset_master = db.asset_master.Find(id);
        //    if (asset_master == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(asset_master);
        //}

        // PUT: api/AssetMasterOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_master(decimal id, asset_master asset_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset_master.am_id)
            {
                return BadRequest();
            }

            db.Entry(asset_master).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_masterExists(id))
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

        // POST: api/AssetMasterOrder
        [ResponseType(typeof(asset_master))]
        public IHttpActionResult Postasset_master(asset_master asset_master)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.asset_master.Add(asset_master);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_master.am_id }, asset_master);
        }

        // DELETE: api/AssetMasterOrder/5
        [ResponseType(typeof(asset_master))]
        public IHttpActionResult Deleteasset_master(decimal id)
        {
            asset_master asset_master = db.asset_master.Find(id);
            if (asset_master == null)
            {
                return NotFound();
            }

            db.asset_master.Remove(asset_master);
            db.SaveChanges();

            return Ok(asset_master);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_masterExists(decimal id)
        {
            return db.asset_master.Count(e => e.am_id == id) > 0;
        }
    }
}