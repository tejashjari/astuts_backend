using astute.CoreModel;
using astute.Models;
using astute.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace astute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region Fields
        private readonly ICommonService _commonService;
        private readonly ITermsService _termsService;
        private readonly IProcessService _processService;
        private readonly ICurrencyService _currencyService;
        private readonly IPointerService _pointerService;
        #endregion

        #region Ctor
        public CommonController(ICommonService commonService, 
            ITermsService termsService,
            IProcessService processService,
            ICurrencyService currencyService,
            IPointerService pointerService)
        {
            _commonService = commonService;
            _termsService = termsService;
            _processService = processService;
            _currencyService = currencyService;
            _pointerService = pointerService;
        }
        #endregion

        #region Methods
        #region Country Master
        [HttpPost]
        [Route("getcountries")]
        public async Task<IActionResult> GetCountries([FromForm] Country_Mas country_Mas)
        {
            try
            {
                var result = await _commonService.GetCountry(country_Mas);
                if(result != null && result.Count > 0)
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
                    statusCode= HttpStatusCode.NotFound,
                    message = CoreCommonMessage.DataNotFound
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createcountry")]
        public async Task<IActionResult> CreateCountry(Country_Mas country_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _commonService.InsertCountry(country_Mas);
                    if (result == -1)
                    {
                        return Conflict(new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = CoreCommonMessage.CountryExists
                        });
                    }
                    else if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CountryCreated
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
        [Route("updatecountry")]
        public async Task<IActionResult> UpdateCountry(Country_Mas country_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _commonService.UpdateCountry(country_Mas);
                    if (result == -1)
                    {
                        return Conflict(new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = CoreCommonMessage.CountryExists
                        });
                    }
                    else if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CountryUpdated
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
        [Route("deletecountry")]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            try
            {
                if (countryId > 0)
                {
                    var result = await _commonService.DeleteCountry(countryId);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CountryDeleted
                        });
                    }
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

        #region State Master
        [HttpGet]
        [Route("getstates")]
        public async Task<IActionResult> GetStates([FromForm]State_Mas state_Mas)
        {
            try
            {
                var result = await _commonService.GetStates(state_Mas);
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
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createstate")]
        public async Task<IActionResult> CreateState(State_Mas state_Mas)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _commonService.InsertState(state_Mas);
                    if(result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.StateCreated
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
        [Route("updatestate")]
        public async Task<IActionResult> UpdateState(State_Mas state_Mas)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(state_Mas.State_Id > 0)
                    {
                        var result = await _commonService.UpdateState(state_Mas);
                        if(result > 0)
                        {
                            return Ok(new
                            {
                                statusCode = HttpStatusCode.OK,
                                message = CoreCommonMessage.StateUpdated
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
        [Route("deletestate")]
        public async Task<IActionResult> DeleteState(int stateId)
        {
            try
            {
                if (stateId > 0)
                {
                    var result = await _commonService.DeleteState(stateId);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.StateDeleted
                        });
                    }
                }
                return BadRequest(HttpStatusCode.BadRequest);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region City Master
        [HttpGet]
        [Route("getcities")]
        public async Task<IActionResult> GetCities(int cityId, string city, int stateId)
        {
            try
            {
                var result = await _commonService.GetCity(cityId, city, stateId);
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
            catch
            {

                throw;
            }
        }

        [HttpPost]
        [Route("createcity")]
        public async Task<IActionResult> CreateCity(City_Mas city_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _commonService.InsertCity(city_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CityCreated
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
        [Route("updatecity")]
        public async Task<IActionResult> UpdateCity(City_Mas city_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (city_Mas.City_Id > 0)
                    {
                        var result = await _commonService.UpdateCity(city_Mas);
                        if (result > 0)
                        {
                            return Ok(new
                            {
                                statusCode = HttpStatusCode.OK,
                                message = CoreCommonMessage.CityUpdated
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
        [Route("deletecity")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            try
            {
                if (cityId > 0)
                {
                    var result = await _commonService.DeleteCity(cityId);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CityDeleted
                        });
                    }
                }
                return BadRequest(HttpStatusCode.BadRequest);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Terms Master
        [HttpGet]
        [Route("getallterms")]
        public async Task<IActionResult> GetAllTerms()
        {
            try
            {
                var result = await _termsService.GetAllTerms();
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
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getterms")]
        public async Task<IActionResult> GetTerms(string terms)
        {
            try
            {
                var result = await _termsService.GetTerms(terms);
                if(result != null)
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
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createterms")]
        public async Task<IActionResult> CreateTerms(Terms_Mas terms_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _termsService.InsertTerms(terms_Mas);
                    if(result > 0)
                    {
                        return Ok(new 
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.TermsCreated
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
        [Route("updateterms")]
        public async Task<IActionResult> UpdateTerms(Terms_Mas terms_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {   
                    var result = await _termsService.UpdateTerms(terms_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.TermsUpdated
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
        [Route("deleteterms")]
        public async Task<IActionResult> DeleteTerms(string terms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _termsService.DeleteTerms(terms);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.TermsDeleted
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
        #endregion

        #region Years Master
        [HttpGet]
        [Route("getyears")]
        public async Task<IActionResult> GetYears()
        {
            try
            {
                var result = await _commonService.GetAllYears();
                if(result != null && result.Count > 0)
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
            catch
            {
                throw;
            }
        }
        #endregion

        #region Quote Mas
        [HttpGet]
        [Route("getrandomquote")]
        public async Task<IActionResult> GetRandomQuote()
        {
            try
            {
                var result = await _commonService.GetRandomQuote();
                return Ok(new 
                {
                    statusCode = HttpStatusCode.OK,
                    message = CoreCommonMessage.DataSuccessfullyFound,
                    data = result
                });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Process Master
        [HttpGet]
        [Route("getallprocess")]
        public async Task<IActionResult> GetAllProcessMas()
        {
            try
            {
                var result = await _processService.GetAllProcessMas();
                if(result != null && result.Count > 0)
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
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getprocessbyid")]
        public async Task<IActionResult> GetProcessMasById(int processId)
        {
            try
            {
                if(processId > 0)
                {
                    var result = await _processService.GetProcessById(processId);
                    if(result != null)
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
                return BadRequest(new {
                    statusCode = HttpStatusCode.BadRequest,
                    message = CoreCommonMessage.ParameterMismatched
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createprocessmas")]
        public async Task<IActionResult> CreateProcessMas(Process_Mas process_Mas)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _processService.InsertProcessMas(process_Mas);
                    if(result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.ProccessMasterCreated
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
        [Route("updateprocessmas")]
        public async Task<IActionResult> UpdateProcessMas(Process_Mas process_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _processService.UpdateProcessMas(process_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.ProccessMasterUpdated
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = CoreCommonMessage.ParameterMismatched
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
        [Route("deleteprocessmas")]
        public async Task<IActionResult> DeleteProcessMas(int processId)
        {
            try
            {
                if(processId > 0)
                {
                    var result = await _processService.DeleteProcessMas(processId);
                    if(result == 1)
                    {
                        return Ok(new 
                        {
                            
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.ProccessMasterDeleted
                        });
                    }
                    else if(result == 2)
                    {
                        return Conflict(new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = "First remove process mas reference from company bank."
                        });
                    }
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

        #region Currency Master
        [HttpGet]
        [Route("getallcurrency")]
        public async Task<IActionResult> GetAllCurrency()
        {
            try
            {
                var result = await _currencyService.GetAllCurrency();
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
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getcurrencybycurrency")]
        public async Task<IActionResult> GetCurrencyByCurrency(string currency)
        {
            try
            {
                if (!string.IsNullOrEmpty(currency))
                {
                    var result = await _currencyService.GetCurrencyByCurrency(currency);
                    if (result != null)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.DataSuccessfullyFound,
                            data = result
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = CoreCommonMessage.DataNotFound
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

        [HttpPost]
        [Route("createcurrency")]
        public async Task<IActionResult> CreateCurrency(Currency_Mas currency_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _currencyService.InsertCurrency(currency_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CurrencyCreated
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = CoreCommonMessage.ParameterMismatched
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
        [Route("updatecurrency")]
        public async Task<IActionResult> UpdateCurrency(Currency_Mas currency_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _currencyService.UpdateCurrency(currency_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CurrencyUpdated
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = CoreCommonMessage.ParameterMismatched
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
        [Route("deletecurrency")]
        public async Task<IActionResult> DeleteCurrency(string currency)
        {
            try
            {
                if (!string.IsNullOrEmpty(currency))
                {
                    var result = await _currencyService.DeleteCurrency(currency);
                    if (result == 1)
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.OK,
                            message = CoreCommonMessage.CurrencyDeleted
                        });
                    }
                    else if (result == 2)
                    {
                        return Conflict(new
                        {
                            statusCode = HttpStatusCode.Conflict,
                            message = "First remove currency reference from company bank."
                        });
                    }
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

        #region Pointer Master
        [HttpGet]
        [Route("getallpointers")]
        public async Task<IActionResult> GetAllPointers()
        {
            try
            {
                var result = await _pointerService.GetAllPointers();
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
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Records not found!"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getpointerbypointerid")]
        public async Task<IActionResult> GetPointerByPointerId(int pointerId)
        {
            try
            {
                if (pointerId > 0)
                {
                    var result = await _pointerService.GetPointerById(pointerId);
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
                        message = "Record not found!"
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

        [HttpPost]
        [Route("createpointer")]
        public async Task<IActionResult> CreatePointer(Pointer_Mas pointer_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _pointerService.InsertPointer(pointer_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Pointer added successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatched!"
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
        [Route("updatepointer")]
        public async Task<IActionResult> UpdatePointer(Pointer_Mas pointer_Mas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _pointerService.UpdatePointer(pointer_Mas);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Pointer updated successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatched!"
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
        [Route("deletepointer")]
        public async Task<IActionResult> DeletePointer(int pointerId)
        {
            try
            {
                if (pointerId > 0)
                {
                    var result = await _pointerService.DeletePointer(pointerId);                    
                    if (result == 1)
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.OK,
                            message = "Pointer deleted successfully."
                        });
                    }
                    else if (result == 2)
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.Conflict,
                            message = "First remove reference from pointer detail."
                        });
                    }
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

        #region Pointer Detail
        [HttpGet]
        [Route("getallpointerdetails")]
        public async Task<IActionResult> GetAllPointerDetails()
        {
            try
            {
                var result = await _pointerService.GetAllPointersDetail();
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
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Records not found!"
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getpointerdetailbypointeridandsubpointername")]
        public async Task<IActionResult> GetPointerDetailByPointerIdAndSubPointerName(int pointerId, string subPointerName)
        {
            try
            {
                if (pointerId > 0)
                {
                    var result = await _pointerService.GetPointerDetailByPointerIdAndSubPointerName(pointerId, subPointerName);
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
                        message = "Record not found!"
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

        [HttpPost]
        [Route("createpointerdetail")]
        public async Task<IActionResult> CreatePointerDetail(Pointer_Det pointer_Det)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _pointerService.InsertPointerDetail(pointer_Det);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Pointer detail added successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatched!"
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
        [Route("updatepointerdetail")]
        public async Task<IActionResult> UpdatePointerDetail(Pointer_Det pointer_Det)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _pointerService.UpdatePointerDetail(pointer_Det);
                    if (result > 0)
                    {
                        return Ok(new
                        {
                            statusCode = HttpStatusCode.OK,
                            message = "Pointer detail updated successfully."
                        });
                    }
                    return BadRequest(new
                    {
                        statusCode = HttpStatusCode.BadRequest,
                        message = "Parameter mismatched!"
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
        [Route("deletepointerdetail")]
        public async Task<IActionResult> DeletePointerDetail(int pointerId, string subPointerName)
        {
            try
            {
                if (pointerId > 0)
                {
                    var result = await _pointerService.DeletePointerDetail(pointerId, subPointerName);
                    if (result > 0)
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.OK,
                            message = "Pointer detail deleted successfully."
                        });
                    }
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
        #endregion
    }
}
