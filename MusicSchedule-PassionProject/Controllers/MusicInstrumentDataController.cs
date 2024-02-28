using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MusicSchedule_PassionProject.Migrations;
using MusicSchedule_PassionProject.Models;

namespace MusicSchedule_PassionProject.Controllers
{
    public class MusicInstrumentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MusicInstrumentData/ListInstruments
        [HttpGet]
        [ResponseType(typeof(MusicInstrument))]
        public IHttpActionResult ListInstruments()
        {
            List<MusicInstrument> Instruments = db.MusicInstruments.ToList();
            List<MusicInstrumentDto> MusicInstrumentDtos = new List<MusicInstrumentDto>();

            Instruments.ForEach(n => MusicInstrumentDtos.Add(new MusicInstrumentDto()
            { 
                InstrumentID = n.InstrumentID,
                InstrumentType = n.InstrumentType
            }));

            return Ok(MusicInstrumentDtos);
        }

        // GET: api/MusicInstrumentData/FindInstrument/5
        [HttpGet]
        [ResponseType(typeof(MusicInstrument))]
        public IHttpActionResult FindInstrument(int id)
        {
            MusicInstrument Instrument = db.MusicInstruments.Find(id);
            MusicInstrumentDto MusicInstrumentDto = new MusicInstrumentDto()
            {
                InstrumentID = Instrument.InstrumentID,
                InstrumentType = Instrument.InstrumentType
            };

            if (Instrument == null)
            {
                return NotFound();
            }

            return Ok(MusicInstrumentDto);
        }

        // POST: api/MusicInstrumentData/UpdateInstrument/5
        [HttpPost]
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult UpdateInstrument(int id, MusicInstrument Instrument)
        {
            Debug.WriteLine("I've reached the update music instrument method");
            if (!ModelState.IsValid)
            {
                //Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != Instrument.InstrumentID)
            {
                //Debug.WriteLine("ID mismatch");
                //Debug.WriteLine("GET parameter" + id);
                //Debug.WriteLine("POST parameter" + MusicInstrument.InstrumentID);
                return BadRequest();
            }

            db.Entry(Instrument).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicInstrumentExists(id))
                {
                    Debug.WriteLine("Music instrument not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MusicInstrumentData/AddInstrument
        [ResponseType(typeof(MusicInstrument))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult AddInstrument(MusicInstrument MusicInstrument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MusicInstruments.Add(MusicInstrument);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = MusicInstrument.InstrumentID }, MusicInstrument);
        }

        // DELETE: api/MusicInstrumentData/DeletInstrument/5
        [ResponseType(typeof(MusicInstrument))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult DeleteInstrument(int id)
        {
            MusicInstrument MusicInstrument = db.MusicInstruments.Find(id);
            if (MusicInstrument == null)
            {
                return NotFound();
            }

            db.MusicInstruments.Remove(MusicInstrument);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MusicInstrumentExists(int id)
        {
            return db.MusicInstruments.Count(e => e.InstrumentID == id) > 0;
        }
    }
}