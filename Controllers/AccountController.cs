using MBBS.Dashboard.web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MBBS.Dashboard.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        // For demo purposes only. In a production app, use a proper authentication mechanism.
        public static Account ActiveAccount;

        public AccountController(IAccountRepository accountRepository, IActivityLogRepository activityLogRepository)
        {
            _accountRepository = accountRepository;
            _activityLogRepository = activityLogRepository;
        }

        // Helper method to check if the active user is an Admin.
        private bool IsAdmin()
        {
            return ActiveAccount != null &&
                   ActiveAccount.UserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        // Action to display Access Denied.
        public IActionResult AccessDenied()
        {
            return View(); // Create Views/Account/AccessDenied.cshtml
        }

        // Utility methods
        public Account GetAccountById(int id)
        {
            return _accountRepository.Accounts.FirstOrDefault(acc => acc.Id == id);
        }

        public Account GetAccountByUsername(string username)
        {
            return _accountRepository.Accounts.FirstOrDefault(acc => acc.Username == username);
        }

        // --------------------------
        // Actions accessible to all logged-in users:
        // --------------------------

        public IActionResult AccountDetails()
        {
            if (ActiveAccount == null)
            {
                return RedirectToAction("LogInPage");
            }
            return View(ActiveAccount);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                // Implement password change logic here.
                return View("PasswordChangeSuccessful");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditAccount()
        {
            if (ActiveAccount == null)
            {
                return RedirectToAction("LogInPage");
            }
            return View(ActiveAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(Account updatedAccount)
        {
            if (ActiveAccount == null)
            {
                return RedirectToAction("LogInPage");
            }

            if (ModelState.IsValid)
            {
                // Update only allowed fields.
                ActiveAccount.LegalName = updatedAccount.LegalName;
                ActiveAccount.Email = updatedAccount.Email;

                if (!string.IsNullOrEmpty(updatedAccount.Password))
                {
                    ActiveAccount.Password = _accountRepository.HashPassword(updatedAccount.Password);
                }

                _accountRepository.SaveAccount(ActiveAccount);
                TempData["SuccessMessage"] = "Account updated successfully!";
                return RedirectToAction("AccountDetails");
            }
            return View(updatedAccount);
        }

        public IActionResult ActivityLog()
        {
            if (ActiveAccount == null)
            {
                return RedirectToAction("LogInPage");
            }

            var logs = _activityLogRepository.GetLogsForAccount(ActiveAccount.Id);
            return View(logs);
        }

        // --------------------------
        // ADMIN-ONLY actions:
        // --------------------------

        public IActionResult AccountList()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }
            return View(_accountRepository.Accounts);
        }

        public IActionResult AccountCreation()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }
            return View();
        }

        [HttpPost]
        public IActionResult NewAccount(Account acc)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                return View("AccountCreation");
            }

            // Here you could decide to allow the admin to set the role.
            // For example, if the admin inputs "Admin" as the role in the form,
            // it will be saved as such.
            // If not provided, you might want to set a default:
            if (string.IsNullOrWhiteSpace(acc.UserRole))
            {
                acc.UserRole = "User";
            }

            // Hash the password before saving.
            acc.Password = _accountRepository.HashPassword(acc.Password);
            _accountRepository.SaveAccount(acc);

            return RedirectToAction("AccountList");
        }

        public IActionResult AccountSettings(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }
            var account = GetAccountById(id);
            if (account == null)
            {
                return View("Error"); // Or a NotFound view.
            }
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(Account acc)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                _accountRepository.SaveAccount(acc);
                return RedirectToAction("AccountList");
            }
            return View("AccountSettings", acc);
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(DeleteAccount model)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied");
            }

            if (ModelState.IsValid)
            {
                // Implement deletion logic.
                // For example: var accountToDelete = GetAccountById(model.AccountId);
                // if (accountToDelete != null) { /* remove account and save changes */ }

                // For demonstration, assume deletion succeeded:
                return View("DeletionSuccessful");
            }
            return View(model);
        }

        // --------------------------
        // Authentication Actions:
        // --------------------------

        public IActionResult SignIn(Account attempt)
        {
            Account acc = _accountRepository.AuthenticateUser(attempt.Username, attempt.Password);
            if (acc == null)
            {
                ViewBag.ErrorMessage = "Invalid login credentials.";
                return View("LogInPage");
            }
            ActiveAccount = acc;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            ActiveAccount = null;
            return RedirectToAction("LogInPage");
        }

        public IActionResult LogInPage()
        {
            return View();
        }
    }
    public class ActivityLog
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}