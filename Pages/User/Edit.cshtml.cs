﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_Students.Data;
using CRUD_Students.Model;

namespace CRUD_Students.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly CRUD_Students.Data.CRUD_StudentsContext _context;
        private readonly IWebHostEnvironment _environment;
        public EditModel(CRUD_Students.Data.CRUD_StudentsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Model.User Users { get; set; } = default!;
        [BindProperty]
        public IFormFile UserImage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users =  await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            Users = users;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (UserImage != null)
                {
                    TempData["FileName"] = UserImage.FileName;
                    TempData["FileSize"] = UserImage.Length;
                }

                return Page();
            }

            var userToUpdate = await _context.Users.FindAsync(Users.Id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
            /*
            _context.Attach(Users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(Users.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
            */


        }

    }
}
