using System.Linq;
using System.Web.Mvc;
using Mid.EF;
using Mid.Models; // Import your SignupModel and database context
using System.Web.Security;

public class SignupController : Controller
{
    private SupplyEntities1 db = new SupplyEntities1(); // Replace YourDbContext with your actual database context class

    [HttpGet]
    public ActionResult Signup()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Signup(SignupModel model, string userType)
    {
        if (ModelState.IsValid)
        {
            // Check if the email is unique for the selected user type
            bool isEmailUnique = IsEmailUniqueForUserType(model.Email, userType);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            if (!isEmailUnique)
            {
                ModelState.AddModelError("Email", "Email address is already in use for the selected user type.");
                return View(model);
            }

            // Determine the user type selected by the user
            if (userType == "Restaurant")
            {
                var restaurant = new Restaurant
                {
                    name = model.Name,
                    address = model.Address,
                    phone = model.Phone,
                    email = model.Email,
                    password = hashedPassword
                };

                db.Restaurants.Add(restaurant);
                db.SaveChanges();
            }
            else if (userType == "NGO")
            {
                var ngo = new NGO
                {
                    name = model.Name,
                    address = model.Address,
                    phone = model.Phone,
                    email = model.Email,
                    password = hashedPassword
                };

                db.NGOs.Add(ngo);
                db.SaveChanges();
            }
            else if (userType == "Employee")
            {
                var employee = new Employee
                {
                    name = model.Name,
                    address = model.Address,
                    phone = model.Phone,
                    email = model.Email,
                    password = hashedPassword
                };

                db.Employees.Add(employee);
                db.SaveChanges();
            }

           
        }

        return RedirectToAction("Login");
    }

   
    private bool IsEmailUniqueForUserType(string email, string userType)
    {
        if (userType == "Restaurant")
        {
            return !db.Restaurants.Any(r => r.email == email);
        }
        else if (userType == "NGO")
        {
            return !db.NGOs.Any(n => n.email == email);
        }
        else if (userType == "Employee")
        {
            return !db.Employees.Any(e => e.email == email);
        }

        return false;
    }



    //---------------------------------Login---------------------------------//
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            // Try to find the user in each table
            var restaurant = db.Restaurants.SingleOrDefault(r => r.email == loginModel.Email);
            var ngo = db.NGOs.SingleOrDefault(n => n.email == loginModel.Email);
            var employee = db.Employees.SingleOrDefault(e => e.email == loginModel.Email);

            // Check if any of the user types have a matching email
            if (restaurant != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, restaurant.password))
            {
                // Authentication successful for a restaurant user
                Session["UserEmail"] = restaurant.email;
                Session["Password"] = BCrypt.Net.BCrypt.HashPassword(restaurant.password);
                return RedirectToAction("CreateFoodItems", "Restaurant"); // Redirect to the restaurant home page
            }
            else if (ngo != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, ngo.password))
            {
                // Authentication successful for an NGO user
                Session["UserEmail"] = ngo.email;
                Session["Password"] = BCrypt.Net.BCrypt.HashPassword(ngo.password);
                return RedirectToAction("NGOHome", "NGO"); // Redirect to the NGO home page
            }
            else if (employee != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, employee.password))
            {
                // Authentication successful for an employee user
                Session["UserEmail"] = employee.email;
                Session["Password"] = BCrypt.Net.BCrypt.HashPassword(employee.password);
                return RedirectToAction("EmployeeHome", "Employee"); // Redirect to the employee home page
            }
            else
            {
                ModelState.AddModelError("", "Invalid login credentials. Please try again.");
            }
        }
        return View(loginModel);
    }


    public ActionResult Logout()
    {
        // Sign the user out
        FormsAuthentication.SignOut();

        // Clear the session (optional)
        Session.Clear();

        // Redirect to the login page or any other desired page
        return RedirectToAction("Login", "Signup"); // Redirect to the login page
    }

}