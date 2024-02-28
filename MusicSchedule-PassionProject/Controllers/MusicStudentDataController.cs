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
    public class MusicStudentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MusicStudentData/ListStudents
        [HttpGet]
        [ResponseType(typeof(MusicStudentDto))]
        public IHttpActionResult ListStudents()
        {
            List<MusicStudent> MusicStudents = db.MusicStudents.ToList();
            List<MusicStudentDto> MusicStudentDtos = new List<MusicStudentDto>();

            MusicStudents.ForEach(m => MusicStudentDtos.Add(new MusicStudentDto()
            {
                MStudentID = m.MStudentID,
                MStudentFName = m.MStudentFName,
                MStudentLName = m.MStudentLName,
                InstrumentID = m.InstrumentID
            }));

            return Ok(MusicStudentDtos);
        }

        // GET: api/MusicStudentData/ListStudentsForInstrument
        [HttpGet]
        [ResponseType(typeof(MusicStudent))]
        public IHttpActionResult ListStudentsForInstrument(int id)
        {
            List<MusicStudent> MusicStudents = db.MusicStudents.Where(m => m.InstrumentID == id).ToList();
            List<MusicStudentDto> MusicStudentDtos = new List<MusicStudentDto>();

            MusicStudents.ForEach(m => MusicStudentDtos.Add(new MusicStudentDto()
            {
                MStudentID = m.MStudentID,
                MStudentFName = m.MStudentFName,
                MStudentLName = m.MStudentLName,
                InstrumentID = m.InstrumentID,
            }));

            return Ok(MusicStudentDtos);
        }

        // GET: api/MusicStudentData/FindMusicStudent/5
        [HttpGet]
        [ResponseType(typeof(MusicStudent))]
        public IHttpActionResult FindMusicStudent(int id)
        {
            MusicStudent MusicStudent = db.MusicStudents.Find(id);
            MusicStudentDto MusicStudentDto = new MusicStudentDto()
            {
                MStudentID = MusicStudent.MStudentID,
                MStudentFName = MusicStudent.MStudentFName,
                MStudentLName = MusicStudent.MStudentLName,
                InstrumentID = MusicStudent.InstrumentID
            };

            if (MusicStudent == null)
            {
                return NotFound();
            }

            return Ok(MusicStudentDto);
        }


        // POST: api/MusicStudentData/UpdateMusicStudent/5
        [HttpPost]
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult UpdateMusicStudent(int id, MusicStudent MusicStudent)
        {
            Debug.WriteLine("I've reached the update music student method");
            if (!ModelState.IsValid)
            {
                //Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != MusicStudent.MStudentID)
            {
                //Debug.WriteLine("ID mismatch");
                //Debug.WriteLine("GET parameter" + id);
                //Debug.WriteLine("POST parameter" + MusicStudent.MStudentID);
                return BadRequest();
            }

            db.Entry(MusicStudent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicStudentExists(id))
                {
                    Debug.WriteLine("Music student not found");
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

        // POST: api/MusicStudentData/AddMusicStudent
        [ResponseType(typeof(MusicStudent))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult AddMusicStudent(MusicStudent MusicStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MusicStudents.Add(MusicStudent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = MusicStudent.MStudentID }, MusicStudent);
        }

        // DELETE: api/MusicStudentData/DeleteMusicStudent/5
        [ResponseType(typeof(MusicStudent))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult DeleteMusicStudent(int id)
        {
            MusicStudent MusicStudent = db.MusicStudents.Find(id);
            if (MusicStudent == null)
            {
                return NotFound();
            }

            db.MusicStudents.Remove(MusicStudent);
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

        private bool MusicStudentExists(int id)
        {
            return db.MusicStudents.Count(e => e.MStudentID == id) > 0;
        }
    }
}