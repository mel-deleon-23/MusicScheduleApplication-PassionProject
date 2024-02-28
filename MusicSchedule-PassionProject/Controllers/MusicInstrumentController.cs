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
    public class MusicInstrumentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MusicInstrumentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384/api/musicinstrumentdata/");
        }

        // GET: MusicInstrument/List
        public ActionResult List()
        {
            //communicate with lesson data to retrieve list of instruments
            //curl https://localhost:44384/api/musicinstrumentdata/listinstruments

            string url = "listinstruments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<MusicInstrumentDto> MusicInstruments = response.Content.ReadAsAsync<IEnumerable<MusicInstrumentDto>>().Result;
            //Debug.WriteLine("Number of lessons recieved");
            //Debug.WriteLine(lessons.Count());

            return View(MusicInstruments);
        }

        // GET: MusicInstrument/Details/5
        public ActionResult Details(int id)
        {
            DetailsInstrument ViewModel = new DetailsInstrument();

            //communicate with isntrument data api to retrieve one instrument
            //curl https://localhost:44384/api/musicinstrumentdata/findinstrument/{id}

            string url = "findinstrument/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            MusicInstrumentDto SelectedInstrument = response.Content.ReadAsAsync<MusicInstrumentDto>().Result;
            ViewModel.SelectedInstrument = SelectedInstrument;
            //Debug.WriteLine("Music instrument recieved: ");
            //Debug.WriteLine(selectedmusicInstrument.InstrumentID);

            return View(ViewModel);
        }


        public ActionResult Error()
        {
            return View();
        }


        // GET: MusicInstrument/New
        //[Authorize]
        public ActionResult New()
        {
            return View();
        }

        // POST: MusicInstrument/Create
        [HttpPost]
        //[Authorize]
        public ActionResult Create(MusicInstrument MusicInstrument)
        {
            //Debug.WriteLine("the jsonpayload is: ");
            //Debug.WriteLine(MusicInstrument.InstrumentID);
            //add new instrument to system 
            //curl -H "Content-type:application/json" -d @musicinstrument.json https://localhost:44384/api/musicinstrumentdata/addinstrument
            string url = "addinstrument";

            string jsonpayload = jss.Serialize(MusicInstrument);
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

        // GET: MusicInstrument/Edit/5
        //[Authorize]
        public ActionResult Edit(int id)
        {
            UpdateInstrument ViewModel = new UpdateInstrument();

            //grab instrument information

            string url = "findinstrument/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("Response code is ");
            //Debug.WriteLine(response.StatusCode);

            MusicInstrumentDto SelectedInstrument = response.Content.ReadAsAsync<MusicInstrumentDto>().Result;
            ViewModel.SelectedInstrument = SelectedInstrument;
            //Debug.WriteLine("Music instrument recieved: ");
            //Debug.WriteLine(selectedmusicInstrument.mStudentID);

            return View(ViewModel);
        }

        // POST: MusicInstrument/Update/5
        [HttpPost]
        //[Authorize]
        public ActionResult Update(int id, MusicInstrument MusicInstrument)
        {
            
             //Debug.WriteLine("The new instrument info is: ");
             //Debug.WriteLine(musicInstrument.InstrumentID);
             //Debug.WriteLine(musicInstrument.InstrumentType);

             //send request to the api
             string url = "updateinstrument/" + id;

             string jsonplayload = jss.Serialize(MusicInstrument);
             Debug.WriteLine(jsonplayload);

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

        // GET: MusicInstrument/Delete/5
        //[Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findinstrument/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            MusicInstrumentDto SelectedInstrument = response.Content.ReadAsAsync<MusicInstrumentDto>().Result;

            return View(SelectedInstrument);
        }

        // POST: MusicInstrument/Delete/5
        [HttpPost]
        //[Authorize]
        public ActionResult Delete(int id)
        {
            string url = "deleteinstrument/" + id;

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
