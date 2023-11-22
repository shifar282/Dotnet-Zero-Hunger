using System.Linq;
using System.Web.Mvc;
using Mid.Auth;
using Mid.EF;
using Mid.Models;

namespace Mid.Controllers
{
    public class RestaurantController : Controller
    {
        private SupplyEntities1 db = new SupplyEntities1();

        [Logged]
        [HttpGet]
        public ActionResult CreateFoodItems()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFoodItems(FoodItemModel foodItem)
        {
            if (ModelState.IsValid)
            {
                var email = Session["UserEmail"] as string;
                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "User email is missing. Please log in."); // Add an error message
                    return View(foodItem);
                }

                var restaurant = db.Restaurants.FirstOrDefault(r => r.email == email);
                if (restaurant is null)
                {
                    ModelState.AddModelError("", "No restaurant found with the given email."); // Add an error message
                    return View(foodItem);
                }

                if (foodItem != null)
                {
                    var food = new FoodItem
                    {
                        name = foodItem.Name,
                        quantity = foodItem.Quantity,
                        preserve_time = foodItem.PreserveTime // Parse the date string
                    };
                    db.FoodItems.Add(food);

                    // Create a new FoodTrack entry
                    var foodTrack = new FoodTracke
                    {
                        FoodItemsId = food.id, // Assuming food.id is the primary key of the FoodItem table
                        RestaurantId = restaurant.id, // Assuming restaurant.id is the primary key of the Restaurant table
                        quantity = foodItem.Quantity,
                        status = "Assigned" // You can set the initial status as needed
                    };
                    db.FoodTrackes.Add(foodTrack);

                    
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

      
            return View(foodItem);
        }



        [Logged]
        public ActionResult Index()
        {
            var foodItems = db.FoodItems.ToList();
            return View(foodItems);
        }
    }
}
