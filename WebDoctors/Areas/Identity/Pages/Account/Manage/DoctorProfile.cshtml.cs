using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Doctor")]
    public class DoctorProfileModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Person> _userManager;
        private readonly IImageUpload _imageUpload;

        public DoctorProfileModel(
            IUnitOfWork unitOfWork,
            UserManager<Person> userManager,
            IImageUpload imageUpload
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _imageUpload = imageUpload;
        }

        public SelectList Specializations { get; set; }

        public string ImagePath { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }

            [Required]
            [Display(Name = "Specialization")]
            public int SpecializationId { get; set; }

            [Required]
            [Display(Name = "Consultation Fee")]
            [DataType(DataType.Currency)]
            [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
            public float ConsultationFee { get; set; }
            public string Description { get; set; }
            public string YoutubeVideo { get; set; }
        }

        private async Task LoadAsync(Person user)
        {
            var doctor = await _unitOfWork.Doctors.Find(q => q.PersonId == user.Id);
            var specializations = await _unitOfWork.Specializations.FindAll();

            Input = new InputModel
            {
                Id = doctor.Id,
                SpecializationId = doctor.SpecializationId,
                ConsultationFee = doctor.ConsultationFee,
                Description = doctor.Description,
                YoutubeVideo = doctor.YoutubeVideo
            };

            Specializations = new SelectList(specializations, "Id", "Name");
            ImagePath = doctor.ImagePath;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var doctor = await _unitOfWork.Doctors.Find(q => q.Id == Input.Id);

            var isUploaded = _imageUpload.SaveFile(image);
            if (isUploaded)
                doctor.ImagePath = _imageUpload.GetFilePath(image);

            doctor.SpecializationId = Input.SpecializationId;
            doctor.ConsultationFee = Input.ConsultationFee;
            doctor.Description = Input.Description;
            doctor.YoutubeVideo = Input.YoutubeVideo;
            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.Save();

            await LoadAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
