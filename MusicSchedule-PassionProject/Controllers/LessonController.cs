using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MusicSchedule_PassionProject.Models;
using System.Web.Script.Serialization;
using MusicSchedule_PassionProject.Migrations;
using MusicSchedule_PassionProject.Models.ViewModels;

namespace MusicSchedule_PassionProject.Controllers
{
    public class LessonController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static LessonController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/lessondata/");
        }


        // GET: Lesson/List
        //[Authorize]
        public ActionResult List()
        {
            //communicate with lesson data to retrieve list of animals
            //curl https://localhost:44384/api/lessondata/listlessons

            
            string url = "listlessons";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<LessonDto> Lessons = response.Content.ReadAsAsync<IEnumerable<LessonDto>>().Result;
            //Debug.WriteLine("Number of lessons recieved");
            //Debug.WriteLine(lessons.Count());

            return View(Lessons);
        }

        // GET: Lesson/Details/5
        public ActionResult Details(int id)
        {
            DetailsLesson ViewModel = new DetailsLesson();

            //communicate with lesson data api to retrieve one lesson
            //curl https://localhost:44384/api/lessondata/findlesson/{id}

            
            string url = "findlesson/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            LessonDto Selectedlesson = response.Content.ReadAsAsync<LessonDto>().Result;
            ViewModel.SelectedLesson = Selectedlesson;
            //Debug.WriteLine("Lesson recieved: ");
            //Debug.WriteLine(selectedlesson.LessonName);

            return View(ViewModel);
        }


        public ActionResult Error()
        {
            return View();
        }


        // GET: Lesson/New
        //[Authorize]
        public ActionResult New()
        {
            //string url = "liststudents";
            //HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            //IEnumerable<MusicStudentDto> MusicStudentOptions = response.Content.ReadAsAsync<IEnumerable<MusicStudentDto>>().Result;

            return View();
        }

        // POST: Lesson/Create
        [HttpPost]
        //[Authorize]
        public ActionResult Create(Lesson Lesson)
        {
            //Debug.WriteLine("the jsonpayload is: ");
            //Debug.WriteLine(Lesson.LessonName);
            //add new lesson to system 
            //curl -H "Content-type:application/json" -d @lesson.json https://localhost:44384/api/lessondata/addlesson
            string url = "addlesson";


            string jsonpayload = jss.Serialize(Lesson);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            //client.PostAsync(url, content);
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Lesson/Edit/5
        //[Authorize]
        public ActionResult Edit(int id)
        {
            UpdateLesson ViewModel = new UpdateLesson();

            //grab lesson information

            string url = "findlesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            LessonDto SelectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;
            ViewModel.SelectedLesson = SelectedLesson;
            //Debug.WriteLine("Lesson recieved: ");
            //Debug.WriteLine(selectedlesson.LessonName);

            return View(ViewModel);
        }

        // POST: Lesson/Update/5
        [HttpPost]
        //[Authorize]
        public ActionResult Update(int id, Lesson Lesson)
        {
            //Debug.WriteLine("The new lesson info is: ");
            //Debug.WriteLine(Lesson.LessonName);
            //Debug.WriteLine(Lesson.LessonDescription);
            //Debug.WriteLine(Lesson.LessonTime);
            //Debug.WriteLine(Lesson.LessonInstructor);
            //Debug.WriteLine(Lesson.MStudentID);

            //send request to the api
            string url = "updatelesson/" + id;

            string jsonplayload = jss.Serialize(Lesson);
            //Debug.WriteLine(jsonplayload);

            HttpContent content = new StringContent(jsonplayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/"+id);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Lesson/Delete/5
        //[Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findlesson/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            LessonDto SelectedLesson = response.Content.ReadAsAsync<LessonDto>().Result;

            return View(SelectedLesson);
        }

        // POST: Lesson/Delete/5
        [HttpPost]
        //[Authorize]
        public ActionResult Delete(int id)
        {
            string url = "deletelesson/" + id;

            //string jsonpayload = jss.Serialize(lesson);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            //client.PostAsync(url, content);
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
