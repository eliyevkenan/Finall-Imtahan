using BackEnd.DAL;
using BackEnd.Extension;
using BackEnd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
namespace BackEnd.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        public IWebHostEnvironment _env;
        public AppDbContext _context;
        public TeamController(IWebHostEnvironment env,AppDbContext context)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            List<Team> Teams = _context.Team.ToList();
            return View(Teams.ToPagedList(page,2));
        }

        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(Team team)
        {
            if (!ModelState.IsValid) return View();
            if (team == null) return BadRequest();
            if (team.ImageFile != null)
            {
                if (!team.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Format duzgun deyil!");
                    return View();
                }
                if (!team.ImageFile.IsSize(5))
                {
                    ModelState.AddModelError("ImageFile", "Resmin olcusu maksimum 5 mb olmalidir!");
                    return View();
                }
                team.Image = team.ImageFile.SaveImage(_env.WebRootPath, "assets/images");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Resim elave edin!");
                return View();
            }
            _context.Team.Add(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            Team team = _context.Team.FirstOrDefault(t => t.Id == id);
            if (team == null) return BadRequest();
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int?id,Team team)
        {
            if (!ModelState.IsValid) return View();
            Team teamEdit = _context.Team.FirstOrDefault(t => t.Id == id);
            if (teamEdit == null) return BadRequest();
            if (team.ImageFile != null)
            {
                if (!team.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Format duzgun deyil!");
                    return View();
                }
                if (!team.ImageFile.IsSize(5))
                {
                    ModelState.AddModelError("ImageFile", "Resmin olcusu maksimum 5 mb olmalidir!");
                    return View();
                }
                Helper.Helpers.DeleteImage(_env.WebRootPath, "assets/images", teamEdit.Image);
                teamEdit.Image = team.ImageFile.SaveImage(_env.WebRootPath, "assets/images");
            }
            teamEdit.Name = team.Name;
            teamEdit.Job = team.Job;
            teamEdit.SocialIcon1 = team.SocialIcon1;
            teamEdit.SocialIcon2 = team.SocialIcon2;
            teamEdit.SocialIcon3 = team.SocialIcon3;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Team team = _context.Team.Find(id);
            if (team == null) return BadRequest();
            _context.Team.Remove(team);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
