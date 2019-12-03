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
    public class VendorCreationController : ApiController
    {
        private assetMVCEFDBEntities7 db = new assetMVCEFDBEntities7();

        //GET: api/VendorCreation
        //public IQueryable<vendor_creation> Getvendor_creation()
        //{
        //    return db.vendor_creation;
        //}

        // GET: api/VendorCreation/5
        [ResponseType(typeof(vendor_creation))]
        public IHttpActionResult Getvendor_creation(decimal id)
        {
            vendor_creation vendor_creation = db.vendor_creation.Find(id);
            if (vendor_creation == null)
            {
                return NotFound();
            }

            return Ok(vendor_creation);
        }
        public VendorCreationController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        public List<VendorViewModel> GetAsset()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<vendor_creation> vendorlist = db.vendor_creation.ToList();
            List<VendorViewModel> viewlist = vendorlist.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype = x.asset_type.at_name,
                vd_atype_id=x.vd_atype_id,
                vd_fromstr = x.vd_fromstr,
                vd_tostr = x.vd_tostr,
                vd_addr = x.vd_addr


            }).ToList();
            return viewlist;
        }
        // PUT: api/VendorCreation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putvendor_creation(decimal id, vendor_creation vendor_creation)
        {
            

            if (id != vendor_creation.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendor_creation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vendor_creationExists(id))
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

        // POST: api/VendorCreation
        [ResponseType(typeof(vendor_creation))]
        public int Postvendor_creation(vendor_creation vendor_creation)
        {
            vendor_creation vendor = new vendor_creation();
            vendor = db.vendor_creation.Where(x=>x.vd_name== vendor_creation.vd_name).FirstOrDefault();
            if (vendor == null)
            {
                db.vendor_creation.Add(vendor_creation);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // DELETE: api/VendorCreation/5
        [ResponseType(typeof(vendor_creation))]
        public IHttpActionResult Deletevendor_creation(decimal id)
        {
            vendor_creation vendor_creation = db.vendor_creation.Find(id);
            if (vendor_creation == null)
            {
                return NotFound();
            }

            db.vendor_creation.Remove(vendor_creation);
            db.SaveChanges();

            return Ok(vendor_creation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vendor_creationExists(decimal id)
        {
            return db.vendor_creation.Count(e => e.vd_id == id) > 0;
        }
    }
}