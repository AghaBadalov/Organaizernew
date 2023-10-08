using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Organaizer.DAL;
using Organaizer.Models;
using Organaizer.Services;
using Organaizer.ViewModels;

namespace Organaizer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IEmailService emailService,AppDBContext context,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            AppUser user = null;
            user =await _userManager.FindByEmailAsync(loginVM.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Email və ya Şifrə yanlışdır");
                return View(loginVM);
            }
            var result =await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email və ya Şifrə yanlışdır");
                return View(loginVM);
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            AppUser user2 =null;
            user2 = await _userManager.FindByEmailAsync(userModel.Email);
            if (user2!= null &&user2.EmailConfirmed==true)
            {
                ModelState.AddModelError("Email", "Bu email artıq qeydiyyatdan keçib");
                return View();
            }
            if (user2 != null && user2.EmailConfirmed == false)
            {
                await _userManager.DeleteAsync(user2);
                
            }
            AppUser user = _mapper.Map<AppUser>(userModel);
            user.Email = userModel.Email;
            user.UserName = userModel.Email;

            await _userManager.CreateAsync(user, userModel.Password);
            await _userManager.AddToRoleAsync(user, "User");

            

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message
            {
                Recipients = new string[] { user.Email },
                Subject = "Email Təsdiqlənməsi",
                Body = $"Zəhmət olmazsa linkə keçid edin: {confirmationLink}"
            };

            
            _emailService.Send(message);

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(SuccessRegistration));
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Ok("Istifadəçi tapilmadi");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }
        
        
        

      
        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            if(!ModelState.IsValid) { return View(); }
            var user=await _userManager.FindByEmailAsync(forgotPassword.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Bu emaildə istifadəçi mövcud deyil");
                return View();
            }
            var token=await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message
            {
                Recipients = new string[] { user.Email },
                Subject = "Şifrə yenilənməsi",
                Body = $"Zəhmət olmazsa linkə keçid edin: {callback}"
            };
            _emailService.Send(message);


            return RedirectToAction("ForgotPasswordConfirmation");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPassword(string token,string email)
        {
            ResetPasswordModel model=new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetmodel)
        {
            if(!ModelState.IsValid){ return View(resetmodel); }
            var user =await _userManager.FindByEmailAsync(resetmodel.Email);
            if(user is null)
            {
                return View("error");
            }
            var resetPassResult =await _userManager.ResetPasswordAsync(user, resetmodel.Token, resetmodel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction("ResetPasswordConfirmation");
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

    }
}
