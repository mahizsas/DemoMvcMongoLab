using DemoMvcMongoLab.Context;
using DemoMvcMongoLab.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Net;
using System.Web.Mvc;

namespace DemoMvcMongoLab.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppContext context = new AppContext();
        private readonly MongoCollection<Customer> customersCollection = null;

        public CustomerController()
        {
            customersCollection = context.Database.GetCollection<Customer>("Customers");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(customersCollection.FindAll());
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customersCollection.FindOneById(ObjectId.Parse(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Email")] Customer customer)
        {
            customer.Id = ObjectId.GenerateNewId().ToString();
            customersCollection.Save(customer);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customersCollection.FindOneById(ObjectId.Parse(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email")] Customer customer)
        {
            var updateBuilder = Update
                    .Set("Name", customer.Name)
                    .Set("Email", customer.Email);

            customersCollection.Update(Query.EQ("_id", ObjectId.Parse(customer.Id)), updateBuilder);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customersCollection.FindOneById(ObjectId.Parse(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            customersCollection.Remove(Query.EQ("_id", ObjectId.Parse(id)));
            return RedirectToAction("Index");
        }
    }
}
