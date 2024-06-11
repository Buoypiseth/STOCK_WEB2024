using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AppSettingController : BaseController
    {
        private DataContext _context;
        private AppSettingRepository _appSettingRepository;
        public AppSettingController()
        {
            this._context = new DataContext();
            this._appSettingRepository = new AppSettingRepository();
        }
        // GET: AppSetting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = _appSettingRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = _appSettingRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

            var displayResult = data.Skip(param.iDisplayStart)
                                .Take(param.iDisplayLength).ToList();
            var totalRecords = data.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }
        // POST: AppSetting/Edit/5
        [Route("AppSetting/Edit")]
        [HttpPost]
        public JsonResult Edit(tbl_APP_Setting data)
        {
            var appSetting = _context.tbl_APP_Setting.Where(x => x.Name == data.Name).FirstOrDefault();
            tbl_APP_Setting _AppSetting = new tbl_APP_Setting();
            _AppSetting.Name = appSetting.Name;
            _AppSetting.Value = appSetting.Value;
            _AppSetting.Description = appSetting.Description;
            return Json(_AppSetting, JsonRequestBehavior.AllowGet);
        }
        //// POST: AppSetting/Update
        //[Route("AppSetting/Update")]
        //[HttpPost]
        //public JsonResult Update(tbl_APP_Setting data)
        //{
        //    //return Json(data.Value, JsonRequestBehavior.AllowGet);
        //    return Json(_appSettingRepository.Update(data), JsonRequestBehavior.AllowGet);
        //}
    }
}