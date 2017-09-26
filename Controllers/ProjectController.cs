//using KTrap.Batch;
//using KTrap.Business;
//using KTrap.Business.Domain;
//using KTrap.Business.DTO.Project;
//using KTrap.Business.DTO.Records;
//using KTrap.Business.DTO.Shared;
//using KTrap.Business.Facade;
//using KTrap.Business.Facade.Functions;
//using KTrap.Business.Facade.Shared;
//using KTrap.Business.Facade.UserManager;
//using KTrap.Business.GlossaryHelper;
//using KTrap.Business.SearchCriteria;
//using KTrap.Business.SortingCriteria;
//using KTrap.Business.StateMachine.Events;
//using KTrap.Business.StateMachine.States;
//using KTrap.Business.Utils;
//using KTrap.Business.Utils;
//using KTrap.Web.Controllers;
//using KTrap.Web.Helpers;
//using KTrap.Web.Models;
//using KTrap.Web.Models.Project;
//using KTrap.Web.Models.Shared;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.Office.Interop.Word;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;

//namespace KTrap.Web.Controllers
//{
//    [Authorize]
//    public class ProjectController : BaseController
//    {
//        private ProjectFacade P = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();

//        [AuthorizeUser(Function = FunctionNames.VIEW_PROJECT_DETAILS)]
//        public ActionResult Index()
//        {
//            var psc = new ProjectSearchCriteria();
//            SetUpSearchCriteriaPerUserAndRole(psc);
//            //P.CheckAllProjectDatesAndUpdateState();
//            var data = P.GetListResult<Project, GetProjectDTO, ProjectSearchCriteria, ProjectFacade, GetProjectListResult, ProjectSortingCriteria>(psc, 0, 10, true, null, new ProjectSortingCriteria() { ByNameAsc = true });
//            var model = new ProjectIndexViewModel(data);
//            var popupMessage = TempData[ProjectIndexViewModel.PopupMessageTempDataKey];

//            if ((popupMessage != null) && (popupMessage is string))
//            {
//                model.PopupMessage = popupMessage as string;
//            }

//            return View(model);
//        }

//        public void SetUpSearchCriteriaPerUserAndRole(ProjectSearchCriteria psc)
//        {
//             //added Reviewer and Translationvendor to the searchcriteria to filter out projects they are not assigned to
//            var userName = FacadeFactory.GetInstance().GetWebFactory().GetUsername();
//            var userRole = FacadeFactory.GetInstance().GetFacade<UserFacade>().GetUserRole(userName);

//            if(userRole.Name == RoleNames.OEM_REVIEWER.Name)
//            {
//                psc.Reviewer = userName;
//            }
//            if (userRole.Name == RoleNames.TRANSLATION_VENDOR.Name)
//            {
//                psc.TranslationVendor = userName;
//            }
//        }

//        public ActionResult DeOrReactivateProject(ProjectSearchCriteria searchCriteria, int pageSize, int pageIndex, string activeEntityId, ProjectSortingCriteria sortingCriteria)
//        {
//            return DeactivateEntity<Project, GetProjectDTO, ProjectSearchCriteria, ProjectFacade, GetProjectListResult, ProjectSortingCriteria>(searchCriteria, pageIndex, pageSize, activeEntityId);
//        }

//        public ActionResult ThankYou(int id = 0)
//        {
//            var project = P.GetById<Project>(id);

//            if (project == null)
//            {
//                return RedirectToAction("Index");
//            }

//            var validation = new Validation();

//            if (project.State == BaseState.Creation)
//            {
//                validation = P.UpdateProjectState(project, BaseEvent.Finish);

//                foreach (var message in FacadeFactory.GetInstance().GetMessageExchange().Messages)
//                {
//                    validation.AddWarning(message);
//                }
//            }
            
//            return View(new ThankYouViewModel(project, validation));
//        }

//        [Route("projectaction/{projectid}/{eventid}"), HttpGet]
//        public ActionResult ProjectAction(int projectid, int eventid)
//        {
//            var evnt = BaseEvent.All.First(ev => ev.Identifier == eventid);
//            return Redirect(evnt.Action(projectid));
//        }

//        public ActionResult SendFeedback(string message, int id)
//        {
//            var project = P.GetById<Project>(id);
//             if (project == null)
//             {
//                return RedirectToAction("Index");
//             }    

//             var notificationValidation = P.SendFeedback(project, message);

//             return Json(1);
//        }


//        public ActionResult DeclineRFQ(int id = 0)
//        {
//            var project = P.GetById<Project>(id);

//            if (project != null)
//            {
//                P.DeclineRFQ(project);
//            }

//            return RedirectToAction("Index");
//        }

//        public ActionResult FileUpload(int id = 0)
//        {
//            var facade = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();
//            Project project = null;
//            if(id != 0 && ((project = facade.GetById<Project>(id)) != null))
//            {
//                return View(new FileUploadViewModel(project, SiteMenuItem.Projects));
//            }
//            return RedirectToAction("Index");
//        }

//        public ActionResult CancelProject(ProjectSearchCriteria searchCriteria, int pageSize, int pageIndex, string activeEntityId, ProjectSortingCriteria sortingCriteria)
//        {
//            var facade = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();
//            var entity = facade.GetById<Project>(activeEntityId);
//            var v = new KTrap.Business.Facade.Shared.Validation();

//            if (entity == null)
//            {
//                v.AddBrokenRule("Could not find entity");
//            }

//            facade.UpdateProjectState(entity, BaseEvent.Cancel);
            
//            var output = facade.GetListResult<Project, GetProjectDTO, ProjectSearchCriteria, ProjectFacade, GetProjectListResult, ProjectSortingCriteria>(searchCriteria, pageIndex, pageSize, false, null, sortingCriteria);

//            output.SetValidation(v);

//            return Json(output);
//        }

//        [AuthorizeUser(Function = FunctionNames.EDIT_PROJECT)]
//        public ActionResult Manage(string id)
//        {
//            return GetFormView<Project, PostProjectDTO, ProjectViewModel, ProjectFacade>(id, "Manage", SiteMenuItem.Projects);
//        }

  
//        [AuthorizeUser(Function = FunctionNames.EDIT_PROJECT)]
//        public ActionResult VendorSpecifications(string id)
//        {
//            return GetFormView<Project, PostProjectDTO, VendorSpecificationsViewModel, ProjectFacade>(id, "VendorSpecifications", SiteMenuItem.Projects);
//        }

//        [AuthorizeUser(Function = FunctionNames.EDIT_PROJECT)]
//        public ActionResult ReviewSettings(string id)
//        {
//            return GetFormView<Project, PostProjectDTO, ReviewViewModel, ProjectFacade>(id, "ReviewSettings", SiteMenuItem.Projects);
//        }

//        [AuthorizeUser(Function = FunctionNames.VIEW_PROJECT_DETAILS)]
//        public ActionResult Overview(string id)
//        {
//            var project = FacadeFactory.GetInstance().GetFacade<ProjectFacade>().GetById<Project>(id);

//            if (project == null)
//            {
//                return Redirect("~/Project");
//            }

//            return View(new OverviewViewModel(project));
//        }

//        [AuthorizeUser(Function = FunctionNames.RESPOND_TO_REQUEST_FOR_QUOTE)]
//        public ActionResult RespondToRFQ(string id)
//        {
//            var project = FacadeFactory.GetInstance().GetFacade<ProjectFacade>().GetById<Project>(id);

//            if (project == null)
//            {
//                return Redirect("~/Project");
//            }

//            return View(new RFQViewModel(project, SiteMenuItem.Projects));
//        }

//        private string GetSubmitProjectRedirectUrl(PostProjectDTO PostData, Project project)
//        {

//            if (PostData.IsModelRelated && !PostData.Vendorspecifications && !PostData.ReviewSettings)
//            {
//                return "~/Project/Models/" + project.Id;
//            }
//            if (PostData.ExtendDeadline)
//            {
//                return "~/Project/Index/";
//            }
//            if (!PostData.Vendorspecifications && !PostData.ReviewSettings)
//            {
//                return "~/Project/VendorSpecifications/" + project.Id;
//            }

//            if (PostData.NoReviewNeeded)
//            {
//                return "~/Project/FileUpload/" + PostData.Id;
//            }

//            if (PostData.Vendorspecifications)
//            {
//                return "~/Project/ReviewSettings/" + project.Id;
//                //send mail
//            }
//            else
//            {
//                return "~/Project/FileUpload/" + PostData.Id;
//            }
//        }

//        [AuthorizeUser(Function = FunctionNames.INITIATE_AND_CREATE_PROJECT)]
//        public ActionResult SubmitProject(PostProjectDTO PostData)
//        {
//            var redirectUrl = "";
//            var saveOrUpdateResult = SaveOrUpdate<Project, PostProjectDTO, ProjectFacade>(PostData);

//            if (saveOrUpdateResult.BusinessObject != null)
//            {
//                redirectUrl = GetSubmitProjectRedirectUrl(PostData, saveOrUpdateResult.BusinessObject);
//            }

//            var postResult = new PostResult<Project>(saveOrUpdateResult);

//            if (redirectUrl.StartsWith("~/"))
//            {
//                redirectUrl = VirtualPathUtility.ToAbsolute(redirectUrl);
//            }
//            postResult.Url = redirectUrl;

//            return Json(postResult);
//        }

//        public ActionResult GetProjectsBySearchCriteria(ProjectSearchCriteria searchCriteria, ProjectSortingCriteria sortingCriteria, int pageIndex, int pageSize)
//        {
//            SetUpSearchCriteriaPerUserAndRole(searchCriteria);

//            return GetListData<Project, GetProjectDTO, ProjectSearchCriteria, ProjectFacade, GetProjectListResult, ProjectSortingCriteria>(searchCriteria, pageIndex, pageSize, false, sortingCriteria);
//        }

//        public ActionResult CheckAmountOfFiles(int ProjectId)
//        {
//            Project pj = P.GetById<Project>(ProjectId);

//            return Json(pj.RecordCollections.FirstOrDefault().Records.Count);
//        }

//        [AuthorizeUser(Function = FunctionNames.EXPORT_PROJECT_LIST_TO_EXCEL_FILE)]
//        public ActionResult ExportToExcel(ProjectSearchCriteria searchCriteria)
//        {
//            var facade = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();

//            return new FileGeneratingResult("ExportProjectOverviewData.xls", "application/xls",
//               stream => facade.ExportToExcel(searchCriteria, stream));

//        }

//        [AuthorizeUser(Function = FunctionNames.MAKE_REQUEST_FOR_QUOTE)]
//        public ActionResult RFQ(int id = 0)
//        {
//            string outstring = "";
//            List<string> outstrings = new List<string>();

//            CommandObj cmd = new CommandObj();
//            Project pj = P.GetById<Project>(id);
//            var recordCollection = pj.RecordCollections.FirstOrDefault(r => r.Name == "inputFiles");

//            foreach(KTrap.Business.Domain.Language tl in pj.TargetLanguages)
//            {
//                RecordCollection targetLang = new RecordCollection();
//                if(pj.RecordCollections.Where(x => x.Name == tl.LanguageName).ToList().Count == 0)
//                {
//                    targetLang.CreatedOn = DateTime.Now;
//                    targetLang.Name = tl.LanguageName;
//                    targetLang.RepositoryPath = string.Format("{0}/{1}/{2}/{3}", Settings.RecordRepositoryBasePath, pj.Id, "output", tl.Iso2LanguageCode);
//                    pj.RecordCollections.Add(targetLang);

//                }
//                else
//                {
//                    targetLang = pj.RecordCollections.First(x => x.Name == tl.LanguageName);
//                }

//                string inputstring = string.Format(" -x {0}\\*.{1} -sl {2} -tl {3} -od {4} -trace", Server.MapPath(recordCollection.RepositoryPath), pj.SourceFileFormat.Name, pj.SourceFileLanguage.Iso2LanguageCode,tl.Iso2LanguageCode, Server.MapPath(targetLang.RepositoryPath));
//                var res = cmd.Run("C:\\okapi\\tikal.bat", inputstring, null, null, 0);
//                if(res.Errors.Count == 0)
//                {
//                    var records = recordCollection.Records;
//                    for (int i = 0; i < records.Count; i++)
//                    {
//                        Record tlrec = new Record();
//                        tlrec.SystemFileName = string.Format("{0}.xlf", records.ElementAt(i).SystemFileName);
//                        tlrec.UserFileName = string.Format("{0}.xlf", records.ElementAt(i).UserFileName);
//                        tlrec.CreatedOn = DateTime.Now;
//                        if(targetLang.Records.Where(r => r.SystemFileName == tlrec.SystemFileName).ToList().Count == 0)
//                        {
//                            targetLang.Records.Add(tlrec);
//                        }
//                    }

//                    FacadeFactory.GetInstance().GetFacade<RecordFacade>().CreateZipFromRecordCollection(targetLang, out outstring);
//                    var url = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Settings.UrlBasePath + StringUtil.RemoveString(outstring, "~"));

//                    outstrings.Add(url.AbsoluteUri);
//                }

//            }
//            P.UpdateProjectState(pj, 3);

//            FacadeFactory.GetInstance().GetFacade<ProjectFacade>().Save();
//            ViewBag.List = outstrings;

//            return View(new BaseViewModel(SiteMenuItem.Projects));
//        }

//        public ActionResult Merge(int id = 0)
//        {
//            Project pj = P.GetById<Project>(id);
//            if(pj == null)
//                return View(new BaseViewModel(SiteMenuItem.Projects));

//            string outstring = "";
//            List<string> outstrings = new List<string>();

//            CommandObj cmd = new CommandObj();
//            var recordCollection = pj.RecordCollections.FirstOrDefault(r => r.Name == "inputFiles");

//            foreach (KTrap.Business.Domain.Language tl in pj.TargetLanguages)
//            {
//                RecordCollection targetLang = new RecordCollection();
//                if (pj.RecordCollections.Where(x => x.Name == tl.LanguageName).ToList().Count == 0)
//                {
//                    targetLang.CreatedOn = DateTime.Now;
//                    targetLang.Name = tl.LanguageName;
//                    targetLang.RepositoryPath = string.Format("{0}/{1}/{2}/{3}", Settings.RecordRepositoryBasePath, pj.Id, "output", tl.Iso2LanguageCode);
//                    pj.RecordCollections.Add(targetLang);

//                }
//                else
//                {
//                    targetLang = pj.RecordCollections.First(x => x.Name == tl.LanguageName);
//                }

//                string inputstring = string.Format(" -m {0}\\*.xlf -trace", Server.MapPath(targetLang.RepositoryPath), pj.SourceFileFormat.Name);
//                var res = cmd.Run("C:\\okapi\\tikal.bat", inputstring, null, null, 0);
//                if (res.Errors.Count == 0)
//                {
//                    foreach (Record rc in targetLang.Records)
//                    {
//                        string filename = rc.SystemFileName.Substring(0, targetLang.Records.FirstOrDefault().SystemFileName.Length - 9);
//                        string docpath = string.Format("{0}\\{1}.out.{2}", Server.MapPath(targetLang.RepositoryPath), filename, pj.SourceFileFormat.Name);
//                        string pdfpath = string.Format("{0}\\{1}.pdf", Server.MapPath(targetLang.RepositoryPath), rc.UserFileName);
//                        convertDocxToPDF(@docpath, @pdfpath);
//                        var url = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Settings.UrlBasePath + StringUtil.RemoveString(targetLang.RepositoryPath, "~")+"/"+ rc.UserFileName+".pdf");

//                        outstrings.Add(url.AbsoluteUri);

//                    }
//                    //for (int i = 0; i < targetLang.Count; i++)
//                    //{
//                    //    Record tlrec = new Record();
//                    //    tlrec.SystemFileName = string.Format("{0}.xlf", records.ElementAt(i).SystemFileName);
//                    //    tlrec.UserFileName = string.Format("{0}.xlf", records.ElementAt(i).UserFileName);
//                    //    tlrec.CreatedOn = DateTime.Now;
//                    //    if (targetLang.Records.Where(r => r.SystemFileName == tlrec.SystemFileName).ToList().Count == 0)
//                    //    {
//                    //        targetLang.Records.Add(tlrec);
//                    //    }
//                    //}

//                    //FacadeFactory.GetInstance().GetFacade<RecordFacade>().CreateZipFromRecordCollection(targetLang, out outstring);

//                }

//            }
//            // P.UpdateProjectState(pj, 3);

//            FacadeFactory.GetInstance().GetFacade<ProjectFacade>().Save();


//          //  convertDocxToPDF(@"C:\Tmp\t23.docx", @"C:\Tmp\pdfs\t23.pdf");
//            convertXlsxToPDF(@"C:\Tmp\book1.xlsx");
//            convertPptxToPDF(@"C:\Tmp\ppt.pptx");
//            GlossaryManager tm = new GlossaryManager();
//            tm.MergeFiles(@"C:\Tmp\tsv\", "ge");
//            ViewBag.List = outstrings;

//            return View(new BaseViewModel(SiteMenuItem.Projects));

//        }

//        public void convertDocxToPDF(string inputfilepath, string outputfilepath)
//        {
//            object MISSING_VALUE = System.Reflection.Missing.Value;

//            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
//            Document wordDocument = appWord.Documents.Open(@inputfilepath, false, true, false, MISSING_VALUE, MISSING_VALUE,
//                MISSING_VALUE, MISSING_VALUE, MISSING_VALUE, MISSING_VALUE, MISSING_VALUE, false, 
//                MISSING_VALUE, MISSING_VALUE, MISSING_VALUE, MISSING_VALUE);
//            wordDocument.ExportAsFixedFormat(@outputfilepath, WdExportFormat.wdExportFormatPDF);
//            appWord.Quit();
//        }

//        public void convertXlsxToPDF(string filepath)
//        {
//            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
//            Workbook wkb = excelApplication.Workbooks.Open(filepath, 0, true, 5, "", "", true, XlPlatform.xlWindows, 
//                                            "\t", false, false, 0, true, 1, XlCorruptLoad.xlNormalLoad);
//            wkb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, @"C:\Tmp\pdfs\XlsTo.pdf");
//            excelApplication.Quit();
//        }

//        public void convertPptxToPDF(string filepath)
//        {
//            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
//            Microsoft.Office.Interop.PowerPoint.Presentation pptPresentation = pptApplication.Presentations.Open(filepath,
//                    Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue,
//                    Microsoft.Office.Core.MsoTriState.msoFalse);

//                // save PowerPoint as PDF
//                pptPresentation.ExportAsFixedFormat(@"C:\Tmp\pdfs\PptxTo.pdf",
//                    Microsoft.Office.Interop.PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF,
//                    Microsoft.Office.Interop.PowerPoint.PpFixedFormatIntent.ppFixedFormatIntentPrint,
//                    MsoTriState.msoFalse, Microsoft.Office.Interop.PowerPoint.PpPrintHandoutOrder.ppPrintHandoutVerticalFirst,
//                    Microsoft.Office.Interop.PowerPoint.PpPrintOutputType.ppPrintOutputSlides, MsoTriState.msoFalse, null,
//                    Microsoft.Office.Interop.PowerPoint.PpPrintRangeType.ppPrintAll, string.Empty, true, true, true,
//                    true, false);

//            pptApplication.Quit();
//        }

//        [AuthorizeUser(Function = FunctionNames.EXPORT_PROJECT_LIST_TO_EXCEL_FILE)]
//        public ActionResult ConvertToXLIFF(int id = 0)
//        {
//            CommandObj cmd = new CommandObj();

//            var res = cmd.Run("C:\\okapi\\tikal.bat", " -x C:\\t2.docx -sl EN -tl FR -od C:\\okapi", null,null, 0);

//            return RedirectToAction("Index");

//        }



//            // Return post result

//            return Json(new PostResult<Project>(saveOrUpdateResult) { Url = VirtualPathUtility.ToAbsolute("~/Project") });
//        }

//        #region Models

//        public ActionResult Models(int id)
//        {
//            var facade = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();
//            var parentObj = facade.GetById<Project>(id);

//            if (parentObj == null)
//            {
//                return RedirectToAction("Index");
//            }

//            var sortingCriteria = new ProjectModelSortingCriteria() { ByModelNameAsc = true };
//            var searchCriteria = new ProjectModelSearchCriteria() { ParentId = id };
//            var results = facade.GetListResult<ProjectModel, GetProjectModelDTO, ProjectModelSearchCriteria, ProjectFacade, GetProjectModelListResult, ProjectModelSortingCriteria>(searchCriteria, 0, 10, true, null, sortingCriteria);

//            var viewModel = new ProjectModelViewModel(parentObj)
//            {
//                ListResult = results,
//                PostData = new PostProjectModelDTO() { Project = parentObj.Id },
//                SiteMenu = new SiteMenuViewModel(SiteMenuItem.Projects)
//            };

//            return View(viewModel);

//            //return GetParentListView<Project, ProjectModel, GetProjectModelDTO, ProjectModelSearchCriteria, ProjectFacade, GetProjectModelListResult, ProjectModelSortingCriteria>(
//            //    id, null, 0, 10, true, new ProjectModelSortingCriteria() { ByModelNameAsc = true });
//        }

//        public ActionResult GetProjectModelsBySearchCriteria(ProjectModelSearchCriteria searchCriteria, int pageIndex, int pageSize, bool initialLoad, ProjectModelSortingCriteria sortingCriteria)
//        {
//            return GetListData<ProjectModel, GetProjectModelDTO, ProjectModelSearchCriteria, ProjectFacade, GetProjectModelListResult, ProjectModelSortingCriteria>(searchCriteria, pageIndex, pageSize, initialLoad, sortingCriteria);
//        }

//        public ActionResult DeleteProjectModel(ProjectModelSearchCriteria searchCriteria, int pageIndex, int pageSize, bool initialLoad, int activeEntityId, ProjectModelSortingCriteria sortingCriteria)
//        {
//            return DeactivateEntity<ProjectModel, GetProjectModelDTO, ProjectModelSearchCriteria, ProjectFacade, GetProjectModelListResult, ProjectModelSortingCriteria>(searchCriteria, pageIndex, pageSize, activeEntityId.ToString(), null, sortingCriteria);
//        }

//        //public ActionResult Model(int id)
//        //{
//        //    return GetFormView<ProjectModel, PostProjectModelDTO, FormViewModel<PostProjectModelDTO>, ProjectFacade>(id.ToString());
//        //}

//        public ActionResult SubmitProjectModel(PostProjectModelDTO PostData)
//        {
//            return SubmitEntity<ProjectModel, PostProjectModelDTO, ProjectFacade>(PostData, string.Format("~/Project/Models/{0}", PostData.Project));
//        }

//        public ActionResult ContinueProjectAfterAddingModels(int id)
//        {
//            var facade = FacadeFactory.GetInstance().GetFacade<ProjectFacade>();
//            var project = facade.GetById<Project>(id);

//            if (project == null)
//            {
//                return Redirect("~/Project/Index");
//            }

//            var postData = new PostProjectDTO();

//            facade.Map(project, postData);
//            postData.IsModelRelated = false; // Skip model adding step

//            return Redirect(GetSubmitProjectRedirectUrl(postData, project));
//        }

//        #endregion

//        #region Vendor

//        public ActionResult SubmitPricing(PostVendorDTO PostData)
//        {
//            bool allPricingSubmitted = true;
//            PostData.RFQaccepted = true;
//            var saveOrUpdateResult = SaveOrUpdate<Vendor, PostVendorDTO, VendorFacade>(PostData);

//            if (saveOrUpdateResult.BusinessObject != null)
//            {
//                Project pj = saveOrUpdateResult.BusinessObject.Project;
//                foreach(Vendor vendor in pj.TranslationVendors)
//                {
//                    if(vendor.RFQaccepted == null)
//                    {
//                        allPricingSubmitted = false;
//                        break;
//                    }
//                }
//                //Notify project manager about new pricing submitted
//                P.SendNewPricingProjectNotification(saveOrUpdateResult.BusinessObject);

//                if (allPricingSubmitted)
//                {
//                    //Change project state to RFA
//                    P.UpdateProjectState(pj.Id, BaseEvent.AllPricesSubmitted);
//                }
//            }

//            var postResult = new PostResult<Vendor>(saveOrUpdateResult);


//            postResult.Url = VirtualPathUtility.ToAbsolute(string.Format("~/Project/RespondToRFQ/{0}",PostData.ProjectId));

//            return Json(postResult);
//            //return SubmitEntity<Vendor, PostVendorDTO, VendorFacade>(PostData, VirtualPathUtility.ToAbsolute(string.Format("~/Project/RespondToRFQ/{0}",PostData.ProjectId)));
//        }

//        public ActionResult UpdatePricing(List<PostPricingDTO> pricing)
//        {
//            foreach(PostPricingDTO prc in pricing)
//            {
//                int fillInCounter = 0;
//                if (prc.NumberOfNewWords != null && prc.PriceOfNewWords != null)
//                {
//                    prc.TotalPriceOfNewWords = prc.NumberOfNewWords * prc.PriceOfNewWords;
//                    fillInCounter++;
//                }
//                if (prc.NumberOfFuzzyMatches != null && prc.PriceOfFuzzyMatches != null)
//                {
//                    prc.TotalPriceOfFuzzyMatches = prc.NumberOfFuzzyMatches * prc.PriceOfFuzzyMatches;
//                    fillInCounter++;
//                }
//                if (prc.NumberOf100PrMatches != null && prc.PriceOf100PrMatches != null)
//                {
//                    prc.TotalPriceOf100Matches = prc.NumberOf100PrMatches * prc.PriceOf100PrMatches;
//                    fillInCounter++;
//                }
//                if (prc.NumberOfRepetitions != null && prc.PriceOfRepetitions != null)
//                {
//                    prc.TotalPriceOfRepetitions = prc.NumberOfRepetitions * prc.PriceOfRepetitions;
//                    fillInCounter++;
//                }
//                if (prc.FileProcessingFees != null && prc.ProjectManagementFees != null && fillInCounter == 4)
//                {
//                    prc.TotalPriceOfLanguage = prc.TotalPriceOfNewWords + prc.TotalPriceOfFuzzyMatches + prc.ProjectManagementFees +
//                        prc.TotalPriceOf100Matches + prc.TotalPriceOfRepetitions + prc.FileProcessingFees;
//                    if(prc.OtherExtraServicesFee != null)
//                    {
//                        prc.TotalPriceOfLanguage += prc.OtherExtraServicesFee;
//                    }
//                }
//            }

//            return Json(pricing);
//        }


//        [AuthorizeUser(Function = FunctionNames.RESPOND_TO_REQUEST_FOR_QUOTE)]
//        public ActionResult Pricing(string id)
//        {
//            var project = P.GetById<Project>(id);

//            if (project == null)
//            {
//                return RedirectToAction("Index");
//            }

//            var userName = FacadeFactory.GetInstance().GetWebFactory().GetUsername();
//            var user = FacadeFactory.GetInstance().GetFacade<UserFacade>().GetAll<ApplicationUser>().FirstOrDefault(u => u.UserName == userName);

//            var actVendor = project.TranslationVendors.FirstOrDefault(t => t.TranslationVendor == user);

//            ViewBag.ProjectName = project.Name;
//            ViewBag.TargetLanguages = project.GetTargetLanguages();

//            if (actVendor == null)
//            {
//                return RedirectToAction("Index");
//            }
//            return GetFormView<Vendor, PostVendorDTO, PricingViewModel, VendorFacade>(actVendor.GetId(), "Pricing", SiteMenuItem.Projects);
//        }

//       #endregion

//        public ActionResult DownloadProjectFiles(string id)
//        {
//            if (!KTrap.Business.Utils.Base64Converter.IsBase64EncodedString(id))
//            {
//                return Error();
//            }

//            var projectId = KTrap.Business.Utils.Base64Converter.Decode(id);
//            var project = P.GetById<Project>(projectId);
            
//            if (project == null)
//            {
//                return Error();
//            }

//            var downloadPath = "";
//            var validation = P.CreateZipFromProjectFiles(project, out downloadPath, true, false, false, false);

//            if (!validation.IsValid)
//            {
//                return Error(string.Join(",", validation.BrokenRules));
//            }

//            var base64EncodedDownloadPath = KTrap.Business.Utils.Base64Converter.Encode(VirtualPathUtility.ToAbsolute(downloadPath));

//            return View("Overview", new OverviewViewModel(project, base64EncodedDownloadPath));
//        }
//    }
//}