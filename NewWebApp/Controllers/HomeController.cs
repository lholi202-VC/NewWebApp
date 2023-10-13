using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NewWebApp.Model;
using NewWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NewWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ADMIN_HOME()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User account)
        {
            try
            {
                finaldatabaseContext SD = new finaldatabaseContext();
                var info = SD.Users.Where(pn => pn.Password == account.Password && pn.Username == account.Username && pn.Email == account.Email).FirstOrDefault();
                if (info != null)
                {
                    return RedirectToAction("ADMIN_HOME", "Home");
                }
                else
                {
                    ViewBag.Error = "Incorrect info";
                }
            }
            catch
            {
                ViewBag.Error = "Incorrect info";
            }
            return View();
        }

        public IActionResult MonetaryDonations()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MonetaryDonations(MonetaryDonation donations)
        {
            try
            {
                if (donations.DonationId != 0 || donations.DonationDate != null || donations.DonorName != null || donations.Amount != 0)
                {
                    finaldatabaseContext SD = new finaldatabaseContext();
                    SD.MonetaryDonations.Add(donations);
                    SD.SaveChanges();
                    return RedirectToAction("ViewMonitaryDonations", "Home");
                }
                else
                {
                    ViewBag.Error = "Please enter all the fields";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Couldn't add donation details info";
                return View();
            }
        }


        public IActionResult ViewMonitaryDonations()
        {
            finaldatabaseContext SD = new finaldatabaseContext();
            return View(SD.MonetaryDonations);
        }

        public IActionResult ViewGoodsDonations()
        {
            finaldatabaseContext SD = new finaldatabaseContext();
            return View(SD.GoodsDonations);
        }

        public IActionResult ViewDisaster()
        {
            finaldatabaseContext SD = new finaldatabaseContext();
            return View(SD.Disasters);
        }

        public IActionResult ListCategories()
        {
            finaldatabaseContext SD = new finaldatabaseContext();
            return View(SD.UserDefinedCategories);
        }








        public IActionResult AddGoodsDonations()
        {
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // Retrieve categories from the database
            var categories = dbContext.UserDefinedCategories.ToList();
            // Store categories in ViewBag
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult AddGoodsDonations(GoodsDonation donation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    finaldatabaseContext dbContext = new finaldatabaseContext();

                    // Check if the donor name is empty, and if so, set it to "Anonymous"
                    if (string.IsNullOrWhiteSpace(donation.DonorName))
                    {
                        donation.DonorName = "Anonymous";
                    }

                    // Add the donation to the database
                    dbContext.GoodsDonations.Add(donation);
                    dbContext.SaveChanges();
                    return RedirectToAction("ViewGoodsDonations", "Home");
                }
                else
                {
                    ViewBag.Error = "Please enter all the required fields.";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Couldn't add donation details.";
                return View();
            }
        }


        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(UserDefinedCategory category)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    finaldatabaseContext SD = new finaldatabaseContext();
                    SD.UserDefinedCategories.Add(category);
                    SD.SaveChanges();
                    return RedirectToAction("AddGoodsDonations", "Home");
                }
                else
                {
                    ViewBag.Error = "Please enter a category name";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Couldn't add category.";
                return View();
            }
        }

        public IActionResult AddDisaster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDisaster(Disaster disaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    finaldatabaseContext dbContext = new finaldatabaseContext();
                    dbContext.Disasters.Add(disaster);
                    dbContext.SaveChanges();
                    return RedirectToAction("ViewDisaster", "Home");
                }
                else
                {
                    ViewBag.Error = "Please enter all the required fields.";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Couldn't add disaster details.";
                return View();
            }
        }

        public IActionResult AllocateMoney()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AllocateMoney(int disasterId, decimal amount)
        {
            // TODO: Implement validation checks here (e.g., validate the amount and disasterId)
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // Insert the money allocation record into the Money_Allocation table
            var moneyAllocation = new MoneyAllocation
            {
                DisasterId = disasterId,
                Amount = amount,
                AllocationDate = DateTime.Now
            };

            dbContext.MoneyAllocations.Add(moneyAllocation);
            dbContext.SaveChanges();

            // Redirect to a suitable page
            return RedirectToAction("ViewAllocateMoney");
        }

        public IActionResult ViewAllocateMoney()
        {
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // Retrieve a list of active disasters from the Disaster table
            var activeDisasters = dbContext.Disasters.Where(d => d.EndDate >= DateTime.Now).ToList();

            // Pass the list of active disasters to the view
            return View(activeDisasters);
        }

        public IActionResult AllocateGoods()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AllocateGoods(int disasterId, int goodsDonationId, decimal quantity)
        {
            // TODO: Implement validation checks here (e.g., validate the quantity, disasterId, and goodsDonationId)
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // Insert the goods allocation record into the Goods_Allocation table
            var goodsAllocation = new GoodsAllocation
            {
                DisasterId = disasterId,
                GoodsDonationId = goodsDonationId,
                Quantity = quantity,
                AllocationDate = DateTime.Now
            };

            dbContext.GoodsAllocations.Add(goodsAllocation);
            dbContext.SaveChanges();

            // Redirect to a suitable page
            return RedirectToAction("Index");
        }

      
        public IActionResult CapturePurchase()
        {
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // Retrieve a list of active disasters from the Disaster table
            var activeDisasters = dbContext.Disasters.Where(d => d.EndDate >= DateTime.Now).ToList();

            // Retrieve a list of goods donations from the Goods_donation table
            var goodsDonations = dbContext.GoodsDonations.ToList();

            // Pass the lists to the view
            var viewModel = new Purchase
            {
                ActiveDisasters = activeDisasters,
                GoodsDonations = goodsDonations
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CapturePurchase(int disasterId, int goodsDonationId, decimal amountSpent)
        {
            finaldatabaseContext dbContext = new finaldatabaseContext();
            // TODO: Implement validation checks here (e.g., validate the amountSpent, disasterId, and goodsDonationId)

            var disaster = dbContext.Disasters.FirstOrDefault(d => d.DisasterId == disasterId);
            var goodsDonation = dbContext.GoodsDonations.FirstOrDefault(gd => gd.DonationId == goodsDonationId);

            if (disaster != null && goodsDonation != null)
            {
                // Check if there is enough available money for the purchase
                decimal totalAllocatedAmount = (decimal)dbContext.MoneyAllocations
                    .Where(ma => ma.DisasterId == disasterId)
                    .Sum(ma => ma.Amount);

                if (totalAllocatedAmount >= amountSpent)
                {
                    // Insert the purchase record into the Purchases table
                    var purchase = new Purchase
                    {
                        DisasterId = disasterId,
                        GoodsDonationId = goodsDonationId,
                        AmountSpent = amountSpent,
                        PurchaseDate = DateTime.Now
                    };

                    dbContext.Purchases.Add(purchase);

                    // Update available money and goods
                    disaster.AvailableMoney -= amountSpent;
                    goodsDonation.NumberOfItems -= 1; // Deduct one unit of the goods

                    dbContext.SaveChanges();

                    // Redirect to a suitable page
                    return RedirectToAction("CapturePurchase");
                }
            }

            // Handle errors or insufficient funds/goods
            ModelState.AddModelError("Error", "Insufficient funds or goods.");
            return View("ErrorView"); // Create a view for error handling
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}