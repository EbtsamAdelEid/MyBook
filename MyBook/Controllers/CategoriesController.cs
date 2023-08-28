using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.Core.Models;
using MyBook.Core.ViewModel;
using MyBook.Data;
using MyBook.Filter;

namespace MyBook.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var books = _context.Categories.AsNoTracking().ToList();
            var ViewModal = _mapper.Map<IEnumerable<CategoryVM>>(books);
            return View(ViewModal);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = _mapper.Map<Category>(model);

            
            _context.Categories.Add(book);
            _context.SaveChanges();

            var bookVM = _mapper.Map<CategoryVM>(book);
            return PartialView("_ModalRow", bookVM);
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int id)
        {
            var book = _context.Categories.Find(id);
            if (book is null)
                return NotFound();

            var category = _mapper.Map<CategoryViewModel>(book);
            return PartialView("_Form" , category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model)
        {
            var book = _context.Categories.Find(model.Id);
            if (book is null)
                return BadRequest();

            book = _mapper.Map(model, book);
            book.LastUpdatedOn = DateTime.Now;

            _context.Categories.Update(book);
            _context.SaveChanges();

            var bookVM = _mapper.Map<CategoryVM>(book);
            return PartialView("_ModalRow", bookVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var book = _context.Categories.Find(id);
            if (book is null)
                return NotFound();

            book.IsDeleted = !book.IsDeleted;
            book.LastUpdatedOn = DateTime.Now;

            _context.Categories.Update(book);
            _context.SaveChanges();

            return Ok(book.LastUpdatedOn.ToString());
        }

        public IActionResult IsAllowed(CategoryViewModel model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
            var IsAllowed = category is null || category.Id.Equals(model.Id);
            return Json(IsAllowed);
        }

    }
}
