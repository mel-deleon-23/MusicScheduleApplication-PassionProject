using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Script.Serialization;
using MusicSchedule_PassionProject.Models;
using MusicSchedule_PassionProject.Migrations;
using MusicSchedule_PassionProject.Models.ViewModels;

namespace MusicSchedule_PassionProject.Controllers
{
    public class MusicStudentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MusicStudentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/musicstudentdata/");
        }

        // GET: MusicStudent/List
        public ActionResult List()
        {
            //communicate with lesson data to retrieve list of students
            //curl https://localhost:44384/api/musicstudentdata/liststudents

            string url = "liststudents";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<MusicStudentDto> MusicStudent = response.Content.ReadAsAsync<IEnumerable<MusicStudentDto>>().Result;
            //Debug.WriteLine("Number of students recieved");
            //Debug.WriteLine(lessons.Count());

            return View(MusicStudent);
        }

        // GET: MusicStudent/Details/5
        public ActionResult Details(int id)
        {
            DetailsMusicStudent ViewModel = new DetailsMusicStudent();

            //communicate with student data api to retrieve one student
            //curl https://localhost:44384/api/musicstudentdata/findmusicstudent/{id}

            string url = "findmusicstudent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            MusicStudentDto SelectedMusicStudent = response.Content.ReadAsAsync<MusicStudentDto>().Result;
            ViewModel.SelectedMusicStudent = SelectedMusicStudent;
            //Debug.WriteLine("Music student recieved: ");
            //Debug.WriteLine(selectedmusicStudent.mStudentID);

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: MusicStudent/New
        //[Authorize]
        public ActionResult New()
        {
            return View();
        }

        // POST: MusicStudent/Create
        [HttpPost]
        //[Authorize]
        public ActionResult Create(MusicStudent MusicStudent)
        {
            //Debug.WriteLine("the jsonpayload is: ");
            //Debug.WriteLine(MusicStudent.MStudentID);
            //add new student to system 
            //curl -H "Content-type:application/json" -d @musicstudent.json https://localhost:44384/api/musicstudentdata/addmusicstudent
            string url = "addmusicstudent";

            string jsonpayload = jss.Serialize(MusicStudent);
            //Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
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

        // GET: MusicStudent/Edit/5
        //[Authorize]
        public ActionResult Edit(int id)
        {
            UpdateMusicStudent ViewModel = new UpdateMusicStudent();

            //grab student information

            string url = "findmusicstudent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            MusicStudentDto SelectedMusicstudent = response.Content.ReadAsAsync<MusicStudentDto>().Result;
            ViewModel.SelectedMusicStudent = SelectedMusicstudent;
            //Debug.WriteLine("Music student recieved: ");
            //Debug.WriteLine(selectedmusicStudent.mStudentID);

            return View(ViewModel);
        }

        // POST: MusicStudent/Update/5
        [HttpPost]
        //[Authorize]
        public ActionResult Update(int id, MusicStudent MusicStudent)
        {
            //Debug.WriteLine("The new lesson info is: ");
            //Debug.WriteLine(MusicStudent.MStudentID);
            //Debug.WriteLine(MusicStudent.MStudentFName);
            //Debug.WriteLine(MusicStudent.MStudentLName);
            //Debug.WriteLine(MusicStudent.InstrumentID);

            //send request to the api
            string url = "updatemusicstudent/" + id;

            string jsonplayload = jss.Serialize(MusicStudent);
            //Debug.WriteLine(jsonplayload);

            HttpContent content = new StringContent(jsonplayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/" + id);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: MusicStudent/Delete/5
        //[Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findmusicstudent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            MusicStudentDto SelectedMusicStudent = response.Content.ReadAsAsync<MusicStudentDto>().Result;

            return View(SelectedMusicStudent);
        }

        // POST: MusicStudent/Delete/5
        [HttpPost]
        //[Authorize]
        public ActionResult Delete(int id)
        {
            string url = "deletemusicstudent/" + id;

            //string jsonpayload = jss.Serialize(musicStudent);
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
