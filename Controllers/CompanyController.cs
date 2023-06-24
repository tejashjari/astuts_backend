using astute.Models;
using astute.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace astute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CompanyController : ControllerBase
    {
        #region Fields
        private readonly ICompanyService _companyService;
        #endregion

        #region Ctor
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        #endregion

        #region Methods
        #region Company Master
        [HttpGet]
        [Route("getallcompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var result = await _companyService.GetAllCompanies();
                if (result != null && result.Count > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Records not found."
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getcompannybyid")]
        public async Task<IActionResult> GetCompannyById(int companyId)
        {
            try
            {
                var result = await _companyService.GetCompanyById(companyId);
                if (result != null)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Record not found."
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcompany")]
        public async Task<IActionResult> CreateCompany(Company_Master company_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyService.InsertCompany(company_Master);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company added successfully."
                        });
                    }
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updatecompany")]
        public async Task<IActionResult> UpdateCompany(Company_Master company_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyService.UpdateCompany(company_Master);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company updated successfully."
                        });
                    }
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("companydelete")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            try
            {
                var result = await _companyService.DeleteCompany(companyId);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Company deleted successfully."
                    });
                }
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = "Parameter mismatched."
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Company Document
        [HttpGet]
        [Route("getcompanydocument")]
        public async Task<IActionResult> GetCompanyDocument(int companyId, int catvalId, DateTime startDate)
        {
            try
            {
                var result = await _companyService.GetCompanyDocument(companyId, catvalId, startDate);
                if (result != null)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Record not fornd!"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcompanydocument")]
        public async Task<IActionResult> CreateCompanyDocument([FromForm]Company_Document company_Document, IFormFile Upload_Path)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (Upload_Path != null && Upload_Path.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/CompanyDocuments");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Upload_Path.FileName);
                        string fileExt = Path.GetExtension(Upload_Path.FileName);

                        string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Upload_Path.CopyToAsync(fileStream);
                        }
                        company_Document.Upload_Path = strFile;
                    }
                    var result = await _companyService.InsertCompanyDocument(company_Document);
                    if(result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company document created successfully."
                        });
                    }
                    return BadRequest(new { 
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatecd!"
                    });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updatecompanydocument")]
        public async Task<IActionResult> UpdateCompanyDocument([FromForm] Company_Document company_Document, IFormFile Upload_Path)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Upload_Path != null && Upload_Path.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/CompanyDocuments");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Upload_Path.FileName);
                        string fileExt = Path.GetExtension(Upload_Path.FileName);

                        string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Upload_Path.CopyToAsync(fileStream);
                        }
                        company_Document.Upload_Path = strFile;
                    }
                    var result = await _companyService.UpdateCompanyDocument(company_Document);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company document updated successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatecd!"
                    });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deletecompanydocument")]
        public async Task<IActionResult> DeleteCompanyDocument(int companyId, int catvalId, DateTime startDate)
        {
            try
            {
                var result = await _companyService.DeleteCompanyDocument(companyId, catvalId, startDate);
                if(result > 0)
                {
                    return Ok(new 
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Company document deleted successfully."
                    });
                }
                return BadRequest(new 
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Parameter mismatched!"
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Company Media
        [HttpGet]
        [Route("getcompanymedia")]
        public async Task<IActionResult> GetCompanyMedia(int companyId, int catvalId)
        {
            try
            {
                var result = await _companyService.GetCompanyMedia(companyId, catvalId);
                if (result != null)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Record not fornd!"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcompanymedia")]
        public async Task<IActionResult> CreateCompanyMedia(Company_Media company_Media)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyService.InsertCompanyMedia(company_Media);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company media created successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatecd!"
                    });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updatecompanymedia")]
        public async Task<IActionResult> UpdateCompanyMedia(Company_Media company_Media)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyService.UpdateCompanyMedia(company_Media);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Company media updated successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatecd!"
                    });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deletecompanymedia")]
        public async Task<IActionResult> DeleteCompanyMedia(int companyId, int catvalId)
        {
            try
            {
                var result = await _companyService.DeleteCompanyMedia(companyId, catvalId);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Company media deleted successfully."
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Parameter mismatched!"
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Company Bank
        [HttpGet]
        [Route("getcompanybank")]
        public async Task<IActionResult> GetCompanyBank(int companyId, int bankId)
        {
            try
            {
                var result = await _companyService.GetCompanyBank(companyId, bankId);
                if(result != null)
                    return Ok(new { statusCode = HttpStatusCode.OK, data = result});
                else
                    return BadRequest(new { statusCode = HttpStatusCode.BadRequest, message = "Records not found." });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcompanybank")]
        public async Task<IActionResult> CreateCompanyBank(Company_Bank company_Bank)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _companyService.InsertCompanyBank(company_Bank);
                    if(result > 0)
                    {
                        return Ok(new { statusCode = HttpStatusCode.OK, message = "Company bank added successfylly." });
                    }
                    return BadRequest(new { statusCode = HttpStatusCode.BadRequest, message = "Parameter mismatched!" });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("updatecompanybank")]
        public async Task<IActionResult> UpdateCompanyBank(Company_Bank company_Bank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _companyService.UpdateCompanyBank(company_Bank);
                    if (result > 0)
                    {
                        return Ok(new { statusCode = HttpStatusCode.OK, message = "Company bank updated successfylly." });
                    }
                    return BadRequest(new { statusCode = HttpStatusCode.BadRequest, message = "Parameter mismatched!" });
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deletecompanybank")]
        public async Task<IActionResult> DeleteCompanyBank(int companyId, int bankId)
        {
            try
            {
                var result = await _companyService.DeleteCompanyBank(companyId, bankId);
                if (result > 0)
                {
                    return Ok(new { statusCode = HttpStatusCode.OK, message = "Company bank deleted successfylly." });
                }
                return BadRequest(new { statusCode = HttpStatusCode.BadRequest, message = "Parameter mismatched!" });
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}
