using astute.Models;
using astute.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace astute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class EmployeeController : ControllerBase
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;
        private readonly ICommonService _commonService;
        #endregion

        #region Ctor
        public EmployeeController(IEmployeeService employeeService,
            IConfiguration configuration,
            ICommonService commonService)
        {
            _employeeService = employeeService;
            _configuration = configuration;
            _commonService = commonService;

        }
        #endregion

        #region Utilities
        private string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtToken:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Methods
        #region Employee Master
        [HttpGet]
        [Route("getemployees")]
        public async Task<IList<Employee_Master>> GetEmployees(string userName)
        {
            try
            {
                var result = await _employeeService.GetEmployees(userName);
                return result;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getemployeebyempid")]
        public async Task<IActionResult> GetEmployeeByEmpId(int empId)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(empId);
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
        [Route("createemployee")]
        public async Task<IActionResult> CreateEmployee([FromForm] Employee_Master employee_Master, IFormFile Icon_Upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = await _employeeService.IsExistUserName(employee_Master.User_Name);
                    if (isExist)
                    {
                        var response = new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = "User name already exist"
                        };
                        return BadRequest(response);
                    }
                    if (Icon_Upload != null && Icon_Upload.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/EmployeeIconImages");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Icon_Upload.FileName);
                        string fileExt = Path.GetExtension(Icon_Upload.FileName);

                        string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Icon_Upload.CopyToAsync(fileStream);
                        }
                        employee_Master.Icon_Upload = strFile;
                    }
                    var result = await _employeeService.InsertEmployee(employee_Master);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Employee Created successfully."
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
        [Route("updateemployee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] Employee_Master employee_Master, IFormFile Icon_Upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Icon_Upload != null && Icon_Upload.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/EmployeeIconImages");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Icon_Upload.FileName);
                        string fileExt = Path.GetExtension(Icon_Upload.FileName);

                        string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Icon_Upload.CopyToAsync(fileStream);
                        }
                        employee_Master.Icon_Upload = strFile;
                    }
                    var result = await _employeeService.UpdateEmployee(employee_Master);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Employee Updated successfully."
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
        [Route("deleteemployee")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(employeeId);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Employee deleted successfully."
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "parameter mismatched."
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Employee Login
        [HttpPost]
        [Route("employee_login")]
        public async Task<IActionResult> EmployeeLogin(UserModel userModel)
        {
            try
            {
                var employee = await _employeeService.EmployeeLogin(userModel);
                if (employee != null)
                {
                    var token = GenerateToken(userModel);
                    if (token != null)
                    {
                        var response = Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            data = employee,
                            token = token
                        });
                        return response;
                    }
                    return Unauthorized();
                }
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            catch (Exception ex)
            {
                var error = new Error_Log()
                {
                    Error_Message = ex.Message,
                    Module_Name = nameof(EmployeeLogin),
                    Arise_Date = DateTime.Now,
                    Error_Trace = ex.StackTrace
                };

                await _commonService.InsertErrorLog(error);
                throw;
            }
        }
        #endregion

        #region Employee Document
        [HttpGet]
        [Route("getemployeedocuments")]
        public async Task<IList<Employee_Document>> GetEmployeeDocuments(int employeeId)
        {
            try
            {
                var result = await _employeeService.GetEmployeeDocuments(employeeId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createemployeedocument")]
        public async Task<IActionResult> CreateEmployeeDocument([FromForm] Employee_Document employee_Document, IFormFile Document_Url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Document_Url != null && Document_Url.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/EmployeeDocuments");
                        if (!(Directory.Exists(filePath)))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string fileName = Path.GetFileNameWithoutExtension(Document_Url.FileName);
                        string fileExt = Path.GetExtension(Document_Url.FileName);

                        string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                        using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                        {
                            await Document_Url.CopyToAsync(fileStream);
                        }
                        employee_Document.Document_Url = strFile;
                    }
                    var result = await _employeeService.InsertEmployeeDocument(employee_Document);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Employee document created successfully."
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
        [Route("updateemployeedocument")]
        public async Task<IActionResult> UpdateEmployeeDocument([FromForm] Employee_Document employee_Document, IFormFile Document_Url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee_Document.Employee_Id > 0)
                    {
                        if (Document_Url != null && Document_Url.Length > 0)
                        {
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/EmployeeDocuments");
                            if (!(Directory.Exists(filePath)))
                            {
                                Directory.CreateDirectory(filePath);
                            }
                            string fileName = Path.GetFileNameWithoutExtension(Document_Url.FileName);
                            string fileExt = Path.GetExtension(Document_Url.FileName);

                            string strFile = fileName + "_" + DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + fileExt;
                            using (var fileStream = new FileStream(Path.Combine(filePath, strFile), FileMode.Create))
                            {
                                await Document_Url.CopyToAsync(fileStream);
                            }
                            employee_Document.Document_Url = strFile;
                        }
                        var result = await _employeeService.UpdateEmployeeDocument(employee_Document);
                        if (result > 0)
                        {
                            return Ok(new
                            {
                                statusCode = HttpStatusCode.OK,
                                message = "Employee document updated successfully."
                            });
                        }
                    }
                    return BadRequest(HttpStatusCode.BadRequest);
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteemployeedocument")]
        public async Task<IActionResult> DeleteEmployeeDocument(int employeeId)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeDocument(employeeId);
                if (result > 0)
                {
                    return Ok(new
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Employee document deleted successfully."
                    });
                }
                return BadRequest(new
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "parameter Mismatched."
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Employee Salary
        [HttpGet]
        [Route("getemployeessallery")]
        public async Task<IList<Employee_Salary>> GetEmployeesSallery(int employeeId)
        {
            try
            {
                var result = await _employeeService.GetEmployeeSalary(employeeId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createemployeesalary")]
        public async Task<IActionResult> CreateEmployeeSalary([FromForm] Employee_Salary employee_Salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _employeeService.InsertEmployeeSalary(employee_Salary);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Employee salary created successfully."
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
        [Route("updateemployeesalary")]
        public async Task<IActionResult> UpdateEmployeeSalary([FromForm] Employee_Salary employee_Salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee_Salary.Employee_Id > 0)
                    {
                        var result = await _employeeService.UpdateEmployeeSalary(employee_Salary);
                        if (result > 0)
                        {
                            return Ok(new
                            {
                                statusCode = HttpStatusCode.OK,
                                message = "Employee salary updated successfully."
                            });
                        }
                    }
                    return BadRequest(HttpStatusCode.BadRequest);
                }
                return BadRequest(ModelState);
            }
            catch
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("deleteemployeesalary")]
        public async Task<IActionResult> DeleteEmployeeSalary(int employeeId)
        {
            try
            {
                if (employeeId > 0)
                {
                    var result = await _employeeService.DeleteEmployeeSalary(employeeId);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Employee salary deleted successfully."
                        });
                    }
                }
                return BadRequest(HttpStatusCode.NotFound);
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
