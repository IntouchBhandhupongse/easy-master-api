using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using easy_master_api.Models;
using easy_master_api.Class;

namespace easy_master_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class masterController : ControllerBase
    {
        private readonly DBContext _dbcontext;

        public masterController(DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Class
        public class PostMasterReq
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string tel { get; set; }
            public string email { get; set; }
        }
        #endregion

        [HttpGet("")]
        public ActionResult<IEnumerable<MasterModel>> GetMasterAll()
        {
            try
            {
                List<MasterModel> list = _dbcontext.MASTER.Where(x => x.is_active).ToList();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MasterModel> GetMasterById(Guid id)
        {
            try
            {
                MasterModel masterModel = _dbcontext.MASTER.FirstOrDefault(x => x.id == id && x.is_active);
                if (masterModel != null)
                {
                    return Ok(masterModel);
                }
                else
                {
                    throw new ArgumentException("Can't find this Id in MASTER");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("")]
        public ActionResult<string> PostMaster([FromBody] PostMasterReq req)
        {
            try
            {
                MasterModel masterModel = new MasterModel();
                masterModel.id = new Guid();
                masterModel.hn = new Random().Next(1000000).ToString().PadLeft(6, '0'); ;
                masterModel.first_name = req.first_name;
                masterModel.last_name = req.last_name;
                masterModel.tel = req.tel;
                masterModel.email = req.email;
                masterModel.created_at = DateTime.Now;
                masterModel.updated_at = DateTime.Now;
                masterModel.is_active = true;

                _dbcontext.MASTER.Add(masterModel);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok(new { message = "CREATED 1 Row" });

        }

        [HttpPut("{id}")]
        public IActionResult PutMaster(Guid id, [FromBody] PostMasterReq req)
        {
            try
            {
                MasterModel masterModel = _dbcontext.MASTER.FirstOrDefault(x => x.id == id && x.is_active);
                if (masterModel != null)
                {
                    if (!string.IsNullOrEmpty(req.first_name))
                        masterModel.first_name = req.first_name;

                    if (!string.IsNullOrEmpty(req.last_name))
                        masterModel.last_name = req.last_name;

                    if (!string.IsNullOrEmpty(req.tel))
                        masterModel.tel = req.tel;

                    if (!string.IsNullOrEmpty(req.email))
                        masterModel.email = req.email;

                    masterModel.updated_at = DateTime.Now;

                    _dbcontext.MASTER.Update(masterModel);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Can't find this Id in MASTER");
                }

                return Ok(new { message = "UPDATED 1 Row" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteMasterById(Guid id)
        {
            try
            {
                MasterModel masterModel = _dbcontext.MASTER.FirstOrDefault(x => x.id == id && x.is_active);
                if (masterModel != null)
                {
                    masterModel.is_active = false;
                    masterModel.updated_at = DateTime.Now;

                    _dbcontext.MASTER.Update(masterModel);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Can't find this Id in MASTER");
                }

                return Ok(new { message = "DELETED 1 Row" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}