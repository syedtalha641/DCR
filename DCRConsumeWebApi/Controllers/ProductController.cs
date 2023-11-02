using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using DCRHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;

namespace DCRConsumeWebApi.Controllers
{
    public class ProductController : Controller
    {
        ApiCall apiCall = new ApiCall();
        JSONRsponse resp = new JSONRsponse();


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> JSONGetProducts(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);

            string response = await apiCall.consumeapi(data, "/Product/GetProducts");

            return Json(response);

        }




        [HttpPost]
        public async Task<IActionResult> GetProducts(ProductViewModel model)
        {
            try
            {
                int pageSize = 0;
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                pageSize = !string.IsNullOrEmpty(length) ? Convert.ToInt32(length) : 0;
                int skip = !string.IsNullOrEmpty(start) ? Convert.ToInt32(start) : 0;

                //Fetch data using JSONGetProducts method asynchronously
                var apiresponse = await JSONGetProducts(model);



                // Deserialize the JSON content
                List<ProductViewModel> result = JsonConvert.DeserializeObject<List<ProductViewModel>>(apiresponse.Value.ToString());



                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    // Apply sorting based on the selected column and direction
                    if (sortColumnDir.ToLower() == "asc")
                    {
                        result = result.OrderBy(p => p.MaterialId).ToList();
                    }
                    else
                    {
                        result = result.OrderByDescending(p => p.MaterialId).ToList();
                    }
                }

                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    // Apply filtering based on the search value
                //    result = result.Where(e =>
                //    e.ProductType.Contains(searchValue) ||
                //    e.MarketName.Contains(searchValue) ||
                //    e.Model.Contains(searchValue) ||
                //    e.Color.Contains(searchValue) ||
                //    e.Brand.Contains(searchValue) ||
                //    e.ProductSku.Contains(searchValue) ||
                //    e.ProductCode.Contains(searchValue) 
                //    ).ToList();
                //}

                int totalRecord = result.Count();
                if (pageSize >= 0)
                {
                    result = result.Skip(skip).Take(pageSize).ToList();
                }

                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = totalRecord,
                    recordsTotal = totalRecord,
                    data = result,
                };

                return Json(jsonData);

            }
            catch (Exception ex)
            {
                //Handle the exception here
                return StatusCode(500, "Internal Server Error");
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> JsonAddProducts(ProductViewModel model)
        {
            try
            {

                var addproduct = new ProductViewModel
                {

                    ProductType = model.ProductType,
                    MaterialId = model.MaterialId,
                    ProductPrice = model.ProductPrice,
                    ProductSku = model.ProductSku,
                    ProductCode = model.ProductCode,
                    MarketName = model.MarketName,
                    Brand = model.Brand,
                    Memory = model.Memory,
                    Model = model.Model,
                    Color = model.Color,
                    Series = model.Series

                };


                if (addproduct != null)
                {
                    string data = JsonConvert.SerializeObject(addproduct);

                    string response = await apiCall.consumeapi(data, "/Product/CreateProduct");

                    if (response != null)
                    {
                        resp.response = true;
                        resp.erorMessage = "Record Saved Successfully";
                    }
                    else
                    {
                        resp.response = false;
                        resp.erorMessage = "Record Not Saved";
                    }

                }
                else
                {
                    resp.hasError = true;
                    resp.erorMessage = "Please Fill The Form";
                }
            }
            catch (Exception ex)
            {
                resp.hasError = true;
                resp.erorMessage = ex.Message;
            }

            return Json(resp);


        }
    }
}
