using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BibliotekMVC2.Models;

namespace BibliotekMVC2.Controllers
{
    //User data och visa i webbshop specifika antal kvar och köpta grejer
    public class HomeController : Controller
    {
        public List<Book> booklist = Book.GetData();
        public UserData userdata;

        public ActionResult Index()
        {
            if(Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            ViewModel VM = ViewModel.viewmodel(booklist, userdata);

            return View(VM);
        }
        //Action när man köpa nån grj
        public ActionResult Lend(int id)
        {
            foreach(Models.Book book in booklist)
            {
                if(book.Id == id)
                {
                    book.Count--;
                    book.LendCount++;
                    Models.Book.SaveData(booklist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if(userdata.ShopedList == null)
                    {
                        userdata.ShopedList = new List<Models.UserData.Shopped>();
                    }
                    userdata.ShopedList.Add(new Models.UserData.Shopped { Id = book.Id});
                    Models.UserData.SaveUserData(userdata);
                }
            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(booklist, userdata);
            return View("Index", VM);
        }

        //Sida med möler text och bilder
        public ActionResult Möblerinfo()
        {
            return View();
        }
        //Korgen med inköpta grejer
        public ActionResult Korgen()
        {
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(booklist, userdata);
            return View(VM);
        }
        //Action i korgen att rensa korgen och fixa ny listan i webbshop med nya antal kvar
        public ActionResult Köpa()
        {
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(booklist, userdata);
            userdata.ShopedList.Clear();
            UserData.SaveUserData(userdata);
            return View("Index", VM);
        }
    }
    }
