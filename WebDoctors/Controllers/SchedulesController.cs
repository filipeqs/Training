using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;
using WebDoctors.Data;
using WebDoctors.Models;

namespace WebDoctors.Controllers
{
    [Authorize(Roles = "Admin, Doctor")]
    public class SchedulesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchedulesController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: AvailabilitiesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (!(await _unitOfWork.Schedules.Exists(q => q.Id == id)))
                return NotFound();

            var schedule = await _unitOfWork.Schedules.Find(q => q.Id == id);
            var scheduleTimes = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == schedule.Id);

            var model = _mapper.Map<ScheduleVM>(schedule);

            var sunday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Sunday);
            var monday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Monday);
            var tuesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Tuesday);
            var wednesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Wednesday);
            var thursday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Thursday);
            var friday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Friday);
            var saturday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Saturday);

            model.Sunday = sunday.Available;
            model.SundayStartTime = sunday.StartTime;
            model.SundayEndTime = sunday.EndTime;

            model.Monday = monday.Available;
            model.MondayStartTime = monday.StartTime;
            model.MondayEndTime = monday.EndTime;

            model.Tuesday = tuesday.Available;
            model.TuesdayStartTime = tuesday.StartTime;
            model.TuesdayEndTime = tuesday.EndTime;

            model.Wednesday = wednesday.Available;
            model.WednesdayStartTime = wednesday.StartTime;
            model.WednesdayEndTime = wednesday.EndTime;

            model.Thursday = thursday.Available;
            model.ThursdayStartTime = thursday.StartTime;
            model.ThursdayEndTime = thursday.EndTime;

            model.Friday = friday.Available;
            model.FridayStartTime = friday.StartTime;
            model.FridayEndTime = friday.EndTime;

            model.Saturday = saturday.Available;
            model.SaturdayStartTime = saturday.StartTime;
            model.SaturdayEndTime = saturday.EndTime;

            return View(model);
        }

        // POST: AvailabilitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ScheduleVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var schedule = _mapper.Map<Schedule>(model);
                _unitOfWork.Schedules.Update(schedule);

                var scheduleTimes = await _unitOfWork.ScheduleTimes.FindAll(q => q.ScheduleId == model.Id);

                var sunday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Sunday);
                var monday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Monday);
                var tuesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Tuesday);
                var wednesday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Wednesday);
                var thursday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Thursday);
                var friday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Friday);
                var saturday = scheduleTimes.SingleOrDefault(s => s.DayOfTheWeek == (int)DayOfWeek.Saturday);

                sunday.Available = model.Sunday;
                sunday.StartTime = model.SundayStartTime;
                sunday.EndTime = model.SundayEndTime;
                _unitOfWork.ScheduleTimes.Update(sunday);

                monday.Available = model.Monday;
                monday.StartTime = model.MondayStartTime;
                monday.EndTime = model.MondayEndTime;
                _unitOfWork.ScheduleTimes.Update(monday);

                tuesday.Available = model.Thursday;
                tuesday.StartTime = model.ThursdayStartTime;
                tuesday.EndTime = model.ThursdayEndTime;
                _unitOfWork.ScheduleTimes.Update(tuesday);

                wednesday.Available = model.Wednesday;
                wednesday.StartTime = model.WednesdayStartTime;
                wednesday.EndTime = model.WednesdayEndTime;
                _unitOfWork.ScheduleTimes.Update(wednesday);

                thursday.Available = model.Thursday;
                thursday.StartTime = model.ThursdayStartTime;
                thursday.EndTime = model.ThursdayEndTime;
                _unitOfWork.ScheduleTimes.Update(thursday);

                friday.Available = model.Friday;
                friday.StartTime = model.FridayStartTime;
                friday.EndTime = model.FridayEndTime;
                _unitOfWork.ScheduleTimes.Update(friday);

                saturday.Available = model.Saturday;
                saturday.StartTime = model.SaturdayStartTime;
                saturday.EndTime = model.SaturdayEndTime;
                _unitOfWork.ScheduleTimes.Update(saturday);

                await _unitOfWork.Save();

                return RedirectToAction("Index", "Doctors");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }
    }
}
