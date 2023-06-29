using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using astute.CoreModel;
using astute.Models;
using astute.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace astute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CategoryController : ControllerBase
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Ctor
        public CategoryController(ICategoryService categoryService,
            ISupplierService supplierService,
            IWebHostEnvironment environment)
        {
            _categoryService = categoryService;
            _supplierService = supplierService;
            _environment = environment;
        }
        #endregion

        #region Methods
        #region Category Master
        [HttpGet]
        [Route("getcategory")]
        public async Task<IActionResult> GetCategory(int catId, int colId)
        {
            try
            {
                var result = await _categoryService.GetCategory(catId, colId);
                if (result != null && result.Count > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = CoreCommonMessage.DataSuccessfullyFound,
                        data = result
                    });
                }
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = CoreCommonMessage.DataNotFound,
                    data = result,
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcategory")]
        public async Task<IActionResult> CreateCategory(Category_Master category_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.InsertCategory(category_Master);
                    if (result == 1)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CategoryMasterCreated
                        });
                    }
                    else if (result == 2)
                    {
                        return Conflict(new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = CoreCommonMessage.CategoryExists,
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
        [Route("updatecategory")]
        public async Task<IActionResult> UpdateCategory(Category_Master category_Master)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateCategory(category_Master);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CategoryMasterUpdated
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
        [Route("deletecategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(id);
                if (result == 1)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = CoreCommonMessage.CategoryMasterDeleted
                    });
                }
                else if (result == 2)
                {
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.Conflict,
                        message = "First remove category value reference."
                    });

                }
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = CoreCommonMessage.ParameterMismatched
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Category Values
        [HttpGet]
        [Route("GetAllCategoryValues")]
        public async Task<IActionResult> GetAllCategoryValues(int catId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string token = Request.Headers["Authorization"];

            var result = await _categoryService.GetCategoryValuesByCatId(catId);
            if (result != null && result.Count > 0)
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = CoreCommonMessage.DataSuccessfullyFound,
                    data = result
                });
            }
            return NoContent();
        }

        [HttpGet]
        [Route("getcategoryvaluebycatvalid")]
        public async Task<IActionResult> GetCategoryValueByCatValId(int catValId)
        {
            try
            {
                var result = await _categoryService.GetCategoryValueByCatValId(catValId);
                if (result != null)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = CoreCommonMessage.DataSuccessfullyFound,
                        data = result
                    });
                }
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = CoreCommonMessage.DataNotFound,
                    data = result,
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcategoryvalue")]
        public async Task<IActionResult> CreateCategoryValue([FromForm] Category_Value category_Value, IFormFile Icon_Url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Icon_Url != null && Icon_Url.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/CategoryValueIcon");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Icon_Url.FileName);
                        string fileExt = Path.GetExtension(Icon_Url.FileName);

                        string strFile = category_Value.Cat_Name + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Icon_Url.CopyToAsync(fileStream);
                        }
                        category_Value.Icon_Url = strFile;
                    }
                    var result = await _categoryService.InsertCategoryValue(category_Value);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CategoryValueCreated
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
        [Route("updatecategoryvalue")]
        public async Task<IActionResult> UpdateCategoryValue([FromForm] Category_Value category_Value, IFormFile Icon_Url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Icon_Url != null && Icon_Url.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/CategoryValueIcon");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Icon_Url.FileName);
                        string fileExt = Path.GetExtension(Icon_Url.FileName);

                        string strFile = category_Value.Cat_Name + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Icon_Url.CopyToAsync(fileStream);
                        }
                        category_Value.Icon_Url = strFile;
                    }
                    var result = await _categoryService.UpdateCategoryValue(category_Value);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CategoryValueUpdated
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
        [Route("deletecategoryvalue")]
        public async Task<IActionResult> DeleteCategoryValue(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryValue(id);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = CoreCommonMessage.CategoryValueDeleted
                    });
                }
                return BadRequest(new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    message = CoreCommonMessage.ParameterMismatched
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Supplier Value
        [HttpGet]
        [Route("getallsuppliervalues")]
        public async Task<IActionResult> GetAllSupplierValues(int catValId)
        {
            var result = await _supplierService.GetSupplier_ValueByCatValId(catValId);
            if (result != null && result.Count > 0)
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.OK,
                    message = CoreCommonMessage.DataSuccessfullyFound,
                    data = result
                });
            }
            return NotFound(new
            {
                statusCode = HttpStatusCode.NotFound,
                message = CoreCommonMessage.DataNotFound
            });
        }

        [HttpGet]
        [Route("getsupplierbysupid")]
        public async Task<IActionResult> GetSupplierBySupId(int supId)
        {
            try
            {
                var result = await _supplierService.GetSupplierById(supId);
                if (result != null)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        data = result
                    });
                }
                return NotFound(new
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = CoreCommonMessage.DataNotFound
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createsuppliervalue")]
        public async Task<IActionResult> CreateSupplierValue(Supplier_Value supplier_Value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _supplierService.InsertSupplierValue(supplier_Value);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.SupplierValueCreated
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
        [Route("updatesuppliervalue")]
        public async Task<IActionResult> UpdateSupplierValue(Supplier_Value supplier_Value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _supplierService.UpdateSupplierValue(supplier_Value);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.SupplierValueUpdated
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
        [Route("deletesuppliervalue")]
        public async Task<IActionResult> DeleteSupplierValue(int supId)
        {
            try
            {
                var result = await _supplierService.DeleteSupplierValue(supId);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = CoreCommonMessage.SupplierValueDeleted
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = CoreCommonMessage.ParameterMismatched
                });
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
