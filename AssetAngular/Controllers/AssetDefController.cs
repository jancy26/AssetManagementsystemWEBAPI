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
    public class AssetDefController : ApiController
    {
        private assetMVCEFDBEntities7 db = new assetMVCEFDBEntities7();

        //public IQueryable<asset_def> Getasset_def()
        //{
        //    return db.asset_def;
        //}
        public List<asset_def> Getasset_def(string ad_name)
        {
            List<asset_def> defs = db.asset_def.Where(x => x.ad_name == ad_name).ToList(); 
            return defs;
        }

        
       [ResponseType(typeof(asset_def))]
        public IHttpActionResult Getasset_def(decimal id)
        {
            asset_def asset_def = db.asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            return Ok(asset_def);
        }
        // Addition of Assets
        [ResponseType(typeof(asset_def))]
        public int  Postasset_def(asset_def asset_def)
        {
            asset_def asset = new asset_def();
            asset = db.asset_def.Where(x => x.ad_name == asset_def.ad_name && x.ad_type_id == asset_def.ad_type_id).FirstOrDefault();
            if(asset==null)
            {
                db.asset_def.Add(asset_def);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public List<AssetViewModel> GetAsset_def(string name)
        {

            db.Configuration.ProxyCreationEnabled = true;

            List<asset_def> alist = db.asset_def.Where(x => x.ad_name.StartsWith(name)).ToList();
            List<AssetViewModel> viewlist = alist.Select(x => new AssetViewModel
            {
                ad_id = x.ad_id,
                ad_name = x.ad_name,
                ad_type = x.asset_type.at_name,
                ad_type_id=x.ad_type_id,
                ad_class = x.ad_class


            }).ToList();
            return viewlist;
        }
        // Updation of Assets
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_def(decimal id, asset_def asset_def)
        {

            if (id != asset_def.ad_id)
            {
                return BadRequest();
            }

            db.Entry(asset_def).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_defExists(id))
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
        public AssetDefController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }
        public List<AssetViewModel> GetAsset()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<asset_def> assetlist = db.asset_def.ToList();
                List<AssetViewModel> viewlist = assetlist.Select(x => new AssetViewModel
                {
                    ad_id = x.ad_id,
                    ad_name = x.ad_name,
                    ad_type=x.asset_type.at_name,
                    ad_type_id=x.ad_type_id,
                    ad_class = x.ad_class


                }).ToList();
                return viewlist;
        }

        // Deletion of Assets
        [ResponseType(typeof(asset_def))]
        public IHttpActionResult Deleteasset_def(decimal id)
        {
            asset_def asset_def = db.asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            db.asset_def.Remove(asset_def);
            db.SaveChanges();

            return Ok(asset_def);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_defExists(decimal id)
        {
            return db.asset_def.Count(e => e.ad_id == id) > 0;
        }
        
    }
}