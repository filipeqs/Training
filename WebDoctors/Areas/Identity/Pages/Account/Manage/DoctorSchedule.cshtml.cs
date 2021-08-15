using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebDoctors.Contracts;
using WebDoctors.Data;

namespace WebDoctors.Areas.Identity.Pages.Account.Manage
{
    public class DoctorScheduleModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Person> _userManager;

        public DoctorScheduleModel(
            IUnitOfWork unitOfWork,
            UserManager<Person> userManager
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }

            public Doctor Doctor { get; set; }
            public int DoctorId { get; set; }

            [Display(Name = "Consultation Time")]
            public string ConsultationTime { get; set; }

            public int SundayId { get; set; }

            public bool Sunday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string SundayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string SundayEndTime { get; set; }

            public bool Monday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string MondayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string MondayEndTime { get; set; }

            public bool Tuesday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string TuesdayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string TuesdayEndTime { get; set; }

            public bool Wednesday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string WednesdayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string WednesdayEndTime { get; set; }

            public bool Thursday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string ThursdayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string ThursdayEndTime { get; set; }

            public bool Friday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string FridayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string FridayEndTime { get; set; }

            public bool Saturday { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "From")]
            public string SaturdayStartTime { get; set; }

            [DataType(DataType.Time)]
            [Display(Name = "To")]
            public string SaturdayEndTime { get; set; }
        }

        private async Task LoadAsync(Person user)
        {
            var doctor = await _unitOfWork.Doctors.Find(q => q.PersonId == user.Id);
            var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == doctor.Id);
            var scheduleTimes = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);

            Input = new InputModel
            {
                Id = schedule.Id,
                DoctorId = doctor.Id,
                ConsultationTime = schedule.ConsultationTime.ToString(),
            };

            var sunday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Sunday);
            var monday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Monday);
            var tuesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Tuesday);
            var wednesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Wednesday);
            var thursday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Thursday);
            var friday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Friday);
            var saturday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Saturday);

            Input.SundayId = sunday.Id;
            Input.Sunday = sunday.Available;
            Input.SundayStartTime = sunday.StartTime;
            Input.SundayEndTime = sunday.EndTime;

            Input.Monday = monday.Available;
            Input.MondayStartTime = monday.StartTime;
            Input.MondayEndTime = monday.EndTime;

            Input.Tuesday = tuesday.Available;
            Input.TuesdayStartTime = tuesday.StartTime;
            Input.TuesdayEndTime = tuesday.EndTime;

            Input.Wednesday = wednesday.Available;
            Input.WednesdayStartTime = wednesday.StartTime;
            Input.WednesdayEndTime = wednesday.EndTime;

            Input.Thursday = thursday.Available;
            Input.ThursdayStartTime = thursday.StartTime;
            Input.ThursdayEndTime = thursday.EndTime;

            Input.Friday = friday.Available;
            Input.FridayStartTime = friday.StartTime;
            Input.FridayEndTime = friday.EndTime;

            Input.Saturday = saturday.Available;
            Input.SaturdayStartTime = saturday.StartTime;
            Input.SaturdayEndTime = saturday.EndTime;
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

        public async Task<IActionResult> OnPostAsync()
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

            var doctor = await _unitOfWork.Doctors.Find(q => q.Id == Input.DoctorId);
            var schedule = await _unitOfWork.Schedules.Find(q => q.DoctorId == doctor.Id);
            var scheduleTimes = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);

            schedule.ConsultationTime = TimeSpan.Parse(Input.ConsultationTime);

            _unitOfWork.Schedules.Update(schedule);

            var sunday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Sunday);
            var monday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Monday);
            var tuesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Tuesday);
            var wednesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Wednesday);
            var thursday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Thursday);
            var friday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Friday);
            var saturday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Saturday);

            sunday.Available = Input.Sunday;
            sunday.StartTime = Input.SundayStartTime;
            sunday.EndTime = Input.SundayEndTime;
            _unitOfWork.ScheduleTimes.Update(sunday);

            monday.Available = Input.Monday;
            monday.StartTime = Input.MondayStartTime;
            monday.EndTime = Input.MondayEndTime;
            _unitOfWork.ScheduleTimes.Update(monday);

            tuesday.Available = Input.Thursday;
            tuesday.StartTime = Input.ThursdayStartTime;
            tuesday.EndTime = Input.ThursdayEndTime;
            _unitOfWork.ScheduleTimes.Update(tuesday);

            wednesday.Available = Input.Wednesday;
            wednesday.StartTime = Input.WednesdayStartTime;
            wednesday.EndTime = Input.WednesdayEndTime;
            _unitOfWork.ScheduleTimes.Update(wednesday);

            thursday.Available = Input.Thursday;
            thursday.StartTime = Input.ThursdayStartTime;
            thursday.EndTime = Input.ThursdayEndTime;
            _unitOfWork.ScheduleTimes.Update(thursday);

            friday.Available = Input.Friday;
            friday.StartTime = Input.FridayStartTime;
            friday.EndTime = Input.FridayEndTime;
            _unitOfWork.ScheduleTimes.Update(friday);

            saturday.Available = Input.Saturday;
            saturday.StartTime = Input.SaturdayStartTime;
            saturday.EndTime = Input.SaturdayEndTime;
            _unitOfWork.ScheduleTimes.Update(saturday);

            await _unitOfWork.Save();

            await LoadAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
