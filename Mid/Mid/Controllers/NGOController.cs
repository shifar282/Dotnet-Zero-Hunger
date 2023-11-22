using System.Linq;
using System.Web.Mvc;
using Mid.Auth;
using Mid.EF;

namespace Mid.Controllers
{
    public class NGOController : Controller
    {
        private SupplyEntities1 db = new SupplyEntities1(); // Create an instance of the database context

        
        [Logged]
        public ActionResult NGOHome()
        {
            // Retrieve all food items from the database
            var foodItems = db.FoodItems.ToList();
            return View(foodItems); // Pass the food items to the view and render it
        }

        [Logged]
        public ActionResult UpdateOrderStatus(int id, string value)
        {
            // Retrieve the user's email from the session
            var email = Session["UserEmail"] as string;

            // Retrieve the NGO associated with the logged-in user
            var NGO = db.NGOs.FirstOrDefault(n => n.email == email);


            var foodTrack = db.FoodTrackes.FirstOrDefault(f => f.id == id);

            if (foodTrack != null)
            {
                if (value == "Accept")
                {
                   
                    foodTrack.NGOId = NGO.id;
                    foodTrack.status = "Accepted";
                    db.SaveChanges(); // Save changes to the database
                }
                else if (value == "Reject")
                {
                   
                    foodTrack.NGOId = NGO.id;
                    foodTrack.status = "Rejected";
                    db.SaveChanges(); 
                }
            }


            return RedirectToAction("NGOHome", "NGO");
        }
    }
}
