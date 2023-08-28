using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.Core.Models;
using MyBook.Core.ViewModel;
using MyBook.Data;
using MyBook.Filter;

namespace MyBook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.AsNoTracking().ToList();
            var authorsvm = _mapper.Map<IEnumerable<AuthorVM>>(authors);
            return View(authorsvm);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = _mapper.Map<Author>(model);

            _context.Authors.Add(author);
            _context.SaveChanges();

            var authorvm = _mapper.Map<AuthorVM>(author);
            return PartialView("_ModalRow", authorvm);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int id)
        {
            var author = _context.Authors.Find(id);
            if (author is null)
                return NotFound();

            var authorvm = _mapper.Map<AuthorViewModel>(author);
            return PartialView("_Form", authorvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorViewModel model)
        {
            var author = _context.Authors.Find(model.Id);
            if (author is null)
                return BadRequest();

            author = _mapper.Map(model, author);
            author.LastUpdatedOn = DateTime.Now;

            _context.Authors.Update(author);
            _context.SaveChanges();

            var authorVM = _mapper.Map<AuthorVM>(author);
            return PartialView("_ModalRow", authorVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var author = _context.Authors.Find(id);
            if (author is null)
                return NotFound();

            author.IsDeleted = !author.IsDeleted;
            author.LastUpdatedOn = DateTime.Now;

            _context.Authors.Update(author);
            _context.SaveChanges();

            return Ok(author.LastUpdatedOn.ToString());
        }

        public IActionResult IsAllowed(AuthorViewModel model)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Name == model.Name);
            var IsAllowed = author is null || author.Id.Equals(model.Id);
            return Json(IsAllowed);
        }

    }
}
