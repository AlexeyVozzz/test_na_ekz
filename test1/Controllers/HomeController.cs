using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using test1.Models;

namespace test1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // Создание экземпляра модели
            User user = new User();
            user.s_name = "";
            user.name = "";
            // Заполнение дополнительных свойств модели

            // Передача модели в представление
            return View(user);
        }

        [HttpPost]
        [ActionName("Save_u")]
        public ActionResult RTA(User user)
        {
            // Сохранение фамилии студента в cookie
            Response.Cookies.Append("s_name", user.s_name);
            Response.Cookies.Append("name", user.name);

            // Редирект на метод Secondary
            return RedirectToRoute("Secondary");
        }

        private string GetUserNameFromCookie()
        {
            string name = Request.Cookies["name"];
            return name;
        }
        public ActionResult Secondary()
        {
            // Получение фамилии студента из cookie
            string s_name = Request.Cookies["s_name"];
            string name = GetUserNameFromCookie();

            // Передача фамилии студента в представление с помощью ViewBag
            ViewBag.Surname = s_name;
            ViewBag.Name = name;

            // Создание модели для передачи в представление
            User user = new User();
            user.s_name = s_name;

            return View("final", user);
        }

    }
}
