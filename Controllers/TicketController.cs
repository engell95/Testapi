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
    public class TicketController : ApiController
    {
        private TestApiEntities db = new TestApiEntities();

        //Visualiza todos los registros de tbl customer que esten activos RegCanceled = false (api/customer)
        [HttpGet]
        public IEnumerable<Ticket> GetAll()
        {

            var list = (from a in db.Ticket
                        where a.RegCanceled == false
                        select a);

            return list.ToList();

        }

        //Visualiza los detalles de un registros
        [HttpGet]
        public Ticket Get(int id)
        {

            return db.Ticket.Find(id);

        }

        //Graba nuevos registros en la tbl Ticket
        [HttpPost]
        public IHttpActionResult AddTicket([FromBody] Ticket data)
        {

            if (ModelState.IsValid)
            {
                db.Ticket.Add(data);
                db.SaveChanges();
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }

        }

        //Actualiza un registro en la tbl Ticket
        [HttpPut]
        public IHttpActionResult UpdateTicket(int id, [FromBody] Ticket data)
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

        //Elimina un registro en la tbl Ticket  RegCanceled = true
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {

            var tblTicket = db.Ticket.Find(id);
            tblTicket.RegCanceled = true;
            db.SaveChanges();
            return Ok();

        }

    }
}
