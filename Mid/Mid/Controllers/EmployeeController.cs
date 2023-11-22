using Mid.Auth;
using Mid.EF;
using System.Linq;
using System.Web.Mvc;

namespace Mid.Controllers
{
    public class EmployeeController : Controller
    {
        private SupplyEntities1 db = new SupplyEntities1();

        // GET: Employee

        // Action method for Employee Home page
        [Logged]
        public ActionResult EmployeeHome()
        {
            // Retrieve all food items from the database
            var foodItems = db.FoodItems.ToList();
            return View(foodItems);
        }

        // Action method for updating order status
        [Logged]
        public ActionResult UpdateOrderStatus(int id, string value)
        {
            // Get the user's email from the session
            var email = Session["UserEmail"] as string;

            // Find the employee with the given email
            var employee = db.Employees.FirstOrDefault(n => n.email == email);

            
            var foodTrack = db.FoodTrackes.FirstOrDefault(f => f.id == id);

            // Check if the food track exists
            if (foodTrack != null)
            {
    
                if (value == "Collect")
                {
                    // Assign the employee id and update the order status to "Collected"
                    foodTrack.EmployeeId = employee.id;
                    foodTrack.status = "Collected";
                    db.SaveChanges();
                }
            }

            
            return RedirectToAction("NGOHome", "NGO");
        }
    }
}
