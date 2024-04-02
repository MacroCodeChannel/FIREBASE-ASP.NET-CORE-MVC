using Firebase.Auth;
using FIREBASEASPMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FIREBASEASPMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        FirebaseAuthProvider _firebaseAuth; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBHnqlcwbJyhDZuoKiwhW95LGUR_6mxqew"));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        public IActionResult SubmitStudent(Student vm)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> RegisterUser(LoginModel vm)
        {
            try
            {
                //create the user
                await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(vm.EmailAddress, vm.Password);

                var fribaselink = await _firebaseAuth.SignInWithEmailAndPasswordAsync(vm.EmailAddress, vm.Password);
                string accestoken = fribaselink.FirebaseToken;
                if(accestoken != null)
                {
                    HttpContext.Session.SetString("AccessToken", accestoken);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(vm);
                }
                
            }
            catch(FirebaseAuthException ex) 
            {
                var firebesex = JsonConvert.DeserializeObject<ErrorModel>(ex.RequestData);
                ModelState.AddModelError(string.Empty, firebesex.message);
                return View(vm);
            }
           
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
