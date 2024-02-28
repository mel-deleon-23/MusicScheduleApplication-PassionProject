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
using MusicSchedule_PassionProject.Models;

namespace MusicSchedule_PassionProject.Controllers
{
    public class LessonDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LessonData/ListLessons
        [HttpGet]
        [ResponseType(typeof(LessonDto))]
        //[Authorize]
        public IHttpActionResult ListLessons()
        {
            List<Lesson> Lessons = db.Lessons.ToList();
            List<LessonDto> LessonDtos = new List<LessonDto>();

            Lessons.ForEach(l => LessonDtos.Add(new LessonDto()
            {
                LessonId = l.LessonId,
                LessonName = l.LessonName,
                LessonDescription = l.LessonDescription,
                LessonTime = (l.LessonTime),
                LessonInstructor = l.LessonInstructor,
                MStudentID = l.MStudentID
            }));

            return Ok(LessonDtos);
        }

        // GET: api/LessonData/ListLessonsForStudent
        [HttpGet]
        [ResponseType(typeof(LessonDto))]
        public IHttpActionResult ListLessonsForStudent(int id)
        {
            List<Lesson> Lessons = db.Lessons.Where(l => l.MStudentID == id).ToList();
            List<LessonDto> LessonDtos = new List<LessonDto>();

            Lessons.ForEach(l => LessonDtos.Add(new LessonDto()
            {
                LessonId = l.LessonId,
                LessonName = l.LessonName,
                LessonDescription = l.LessonDescription,
                LessonTime = l.LessonTime,
                LessonInstructor = l.LessonInstructor,
                MStudentID = l.MStudentID
            }));

            return Ok(LessonDtos);
        }

        // GET: api/LessonData/FindLesson/5
        [HttpGet]
        [ResponseType(typeof(LessonDto))]
        public IHttpActionResult FindLesson(int id)
        {
            Lesson Lesson = db.Lessons.Find(id);
            LessonDto LessonDto = new LessonDto()
            {
                LessonId = Lesson.LessonId,
                LessonName = Lesson.LessonName,
                LessonDescription = Lesson.LessonDescription,
                LessonTime = Lesson.LessonTime,
                LessonInstructor = Lesson.LessonInstructor,
                MStudentID = Lesson.MStudentID
            };

            if (Lesson == null)
            {
                return NotFound();
            }

            return Ok(LessonDto);
        }

        // POST: api/LessonData/UpdateLesson/5
        [HttpPost]
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult UpdateLesson(int id, Lesson Lesson)
        {
            Debug.WriteLine("I've reached the update lesson method");
            if (!ModelState.IsValid)
            {
                //Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != Lesson.LessonId)
            {
                //Debug.WriteLine("ID mismatch");
                //Debug.WriteLine("GET parameter" + id);
                //Debug.WriteLine("POST parameter" + Lesson.LessonId);
                return BadRequest();
            }

            db.Entry(Lesson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
                {
                    Debug.WriteLine("Lesson not found");
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

        // POST: api/LessonData/AddLesson
        [ResponseType(typeof(Lesson))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult AddLesson(Lesson Lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lessons.Add(Lesson);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Lesson.LessonId }, Lesson);
        }

        // DELETE: api/LessonData/DeleteLesson/5
        [ResponseType(typeof(Lesson))]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson Lesson = db.Lessons.Find(id);
            if (Lesson == null)
            {
                return NotFound();
            }

            db.Lessons.Remove(Lesson);
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

        private bool LessonExists(int id)
        {
            return db.Lessons.Count(e => e.LessonId == id) > 0;
        }
    }
}