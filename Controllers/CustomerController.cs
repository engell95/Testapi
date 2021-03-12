//using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using Testapi;

namespace Testapi.Controllers
{
    public class CustomerController : ApiController
    {
        //private TestEntities1 dbContext = new TestEntities1();
        private TestApiEntities db = new TestApiEntities();

        //Visualiza todos los registros de tbl customer que esten activos RegCanceled = false (api/customer)
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {


            var result = (from a in db.Ticket
                          where a.RegCanceled == false
                          select a).ToList();

            return db.Customer.ToList();



        }

        //Visualiza los detalles de un registros
        [HttpGet]
        public Customer Get(int id)
        {

            return db.Customer.Find(id);

        }

        //Graba nuevos registros en la tbl customer
        [HttpPost]
        public IHttpActionResult AddCustomer([FromBody] Customer data)
        {

            if (ModelState.IsValid)
            {
                db.Customer.Add(data);
                db.SaveChanges();
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }

        }

        //Actualiza un registro en la tbl customer
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] Customer data)
        {

            if (ModelState.IsValid)
            {
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        //Elimina un registro en la tbl customer   RegCanceled = true
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {

            var tblCustomer = db.Customer.Find(id);
            tblCustomer.RegCanceled = true;
            db.SaveChanges();
            return Ok();

        }


    }

}
