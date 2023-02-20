using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticeWithVideo.DataAccess.Data;
using RealPractice.Models;
using RealProject.DataAccess.Repository;
using RealProject.DataAccess.Repository.IRepository;

namespace PracticeWithVideo.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;          //ye dependency injection az IWebHostEnvironment mirim ta be folder menuItems ke toye images toye ww.roots barname has dasresi peyda konim
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            MenuItem = new();
        }


        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);

            }
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {

                Text = i.Name,
                Value = i.Id.ToString()

            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem()
            {

                Text = i.Name,
                Value = i.Id.ToString()

            });

        }


        public async Task<IActionResult> OnPost()
        {  //when once an image or everything is uploaded ,we will be on post handler
            //we need to get the root part of wwwroot folder
            string webRootPath = _hostEnvironment.WebRootPath;
            //Now we need to capture the file which was uploaded
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString(); //ba guid make sure mikonim ke file name ma unique ast
                //now we want to find out the folder of uploads
                var uploads = Path.Combine(webRootPath, @"images\menuItems");
                //final place where we want to upload the file,we want to make sure that files should have the same extension
                var extension = Path.GetExtension(files[0].FileName);
                //copy the files[0] which is the first file inside the folder
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);//this syntax makes sure that the uploaded in our location
                }
                MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));//delete the old image
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    //new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItem.Image = @"\images\menuItems\" + fileName_new + extension;

                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }


                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }


            return RedirectToPage("./Index");
        }
    }
}


