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
    public class AssetTypeController : ApiController
    {
        private assetMVCEFDBEntities7 db = new assetMVCEFDBEntities7();
        
        // List Asset Type
        public IQueryable<asset_type> Getasset_type()
        {
            return db.asset_type;
        }

        public AssetTypeController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        public List<asset_type> Getasset_type(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<asset_def> asset = db.asset_def.Where(x => x.ad_name == name).ToList();
            List<asset_type> list = asset.Select(x => new asset_type
            {
                at_id = Convert.ToInt32(x.ad_type_id),
                at_name = x.asset_type.at_name
            }).ToList();
            return list;
        }

        // List Asset Type with id
        [ResponseType(typeof(asset_type))]
        public IHttpActionResult Getasset_type(decimal id)
        {
            asset_type asset_type = db.asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return Ok(asset_type);
        }

        // Updation of Asset Type
        [ResponseType(typeof(void))]
        public IHttpActionResult Putasset_type(decimal id, asset_type asset_type)
        {
           if (id != asset_type.at_id)
            {
                return BadRequest();
            }

            db.Entry(asset_type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!asset_typeExists(id))
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

        // Addition of Asset Type
        [ResponseType(typeof(asset_type))]
        public IHttpActionResult Postasset_type(asset_type asset_type)
        {

            db.asset_type.Add(asset_type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_type.at_id }, asset_type);
        }

        // Deletion of Asset Type
         [ResponseType(typeof(asset_type))]
        public IHttpActionResult Deleteasset_type(decimal id)
        {
            asset_type asset_type = db.asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            db.asset_type.Remove(asset_type);
            db.SaveChanges();

            return Ok(asset_type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asset_typeExists(decimal id)
        {
            return db.asset_type.Count(e => e.at_id == id) > 0;
        }
    }
}