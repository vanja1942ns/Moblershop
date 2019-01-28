using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotekMVC2.Models
{
    public class ViewModel
    {
        public List<Book> BookList { get; set; }
        public UserData UserData { get; set; }

        public static ViewModel viewmodel(List<Book> booklist, UserData userdata)
        {
            ViewModel VM = new ViewModel();
            VM.BookList = booklist;
            VM.UserData = userdata;
            return VM;
        }
    }
}