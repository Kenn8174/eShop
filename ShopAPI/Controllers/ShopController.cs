using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ShopService;
using DataLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IQueryable<Phone>> GetPhonesAPI()
        {
            return Ok( _shopService.GetPhones());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Phone> GetPhonesAPI(int id)
        {
            return Ok(_shopService.GetPhones().Where(x => x.PhoneID == id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Phone> PostCreatePhone(Phone value)
        {
            _shopService.CreatePhonesAPI(value);
            _shopService.Commit();

            return CreatedAtAction(nameof(GetPhonesAPI), new { id = value.PhoneID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<Phone> PutPhone(int id, Phone phone)
        {
            if (id != phone.PhoneID)
            {
                return BadRequest();
            }

            _shopService.UpdatePhoneAPI(phone);
            _shopService.Commit();

            return CreatedAtAction(nameof(GetPhonesAPI), new { id = phone.PhoneID }, phone);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phone>> Delete(int id)
        {
            var phone = await _shopService.GetEditAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            _shopService.RemovePhone(phone);
            await _shopService.Commit();

            return CreatedAtAction(nameof(GetPhonesAPI), phone);
        }
    }
}
