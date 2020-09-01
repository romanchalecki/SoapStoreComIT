﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SoapStoreComIT.Models;
using SoapStoreComIT.Utility;

namespace SoapStoreComIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole>roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [PersonalData]
            [Display(Name = "First Name")]
            [Column(TypeName = "nvarchar(100)")]
            public string FirstName { get; set; }

            //[Required]
            //[Display(Name = "Street Adress")]
            //public string StreetAdress { get; set; }

            //[Display(Name = "Apartment Number")]
            //public string ApartmentNumber { get; set; }

            //[Required]
            //[Display(Name = "State/Province")]
            //public string Province { get; set; }

            //[Required]
            //[Display(Name = "City")]
            //public string City { get; set; }

            //[Required]
            //[Display(Name = "Postal Code")]
            //public string PostalCode { get; set; }

            //[Required]
            //[Display(Name = "Country")]
            //public string Country { get; set; }

            [Required]
            [PersonalData]
            [Column(TypeName = "nvarchar(100)")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string role = Request.Form["rdUserRole"].ToString();

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName=Input.FirstName,
                    LastName=Input.LastName,
                    //StreetAdress=Input.StreetAdress,
                    //ApartmentNumber=Input.ApartmentNumber,
                    //Province=Input.Province,
                    //City=Input.City,
                    //PostalCode=Input.PostalCode,
                    //Country=Input.Country,
                    PhoneNumber=Input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    if(!await _roleManager.RoleExistsAsync(SD.ManagerUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.WarehouseUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.WarehouseUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.FrontDeskUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.FrontDeskUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.CustomerAndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerAndUser));
                    }

                    if(role==SD.WarehouseUser)
                    {
                        await _userManager.AddToRoleAsync(user, SD.WarehouseUser);
                    }
                    else
                    {
                        if (role == SD.FrontDeskUser)
                        {
                            await _userManager.AddToRoleAsync(user, SD.FrontDeskUser);
                        }
                        else
                        {
                            if (role == SD.ManagerUser)
                            {
                                await _userManager.AddToRoleAsync(user, SD.ManagerUser);
                            }
                            else
                            {
                                await _userManager.AddToRoleAsync(user, SD.CustomerAndUser);
                                await _signInManager.SignInAsync(user, isPersistent: false);
                            }
                        }
                    }


                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}