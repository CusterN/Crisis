using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crisis.Models;
using Crisis.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crisis.Pages
{   
    [BindProperties]
    public class AdminModel : PageModel
    {

        private readonly Crisis.Data.CrisisContext _context;

        public AdminModel(Crisis.Data.CrisisContext context)
        {
            _context = context;
        }

        public string StatusMessage { get; set; }
        public string ObjectType { get; set; }

        public ComboBox ComboBox { get; set; }
        public IList<Status> Statuses { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<CallResponse> CallResponses { get; set; }
        public IList<Escalation> Escalations { get; set; }
        public IList<AttachmentType> AttachmentTypes { get; set; }

        public async Task OnGetAsync()
        {
            Statuses = await _context.Status.ToListAsync();
            Categories = await _context.Category.ToListAsync();
            CallResponses = await _context.CallResponse.ToListAsync();
            AttachmentTypes = await _context.AttachmentType.ToListAsync();
            Escalations = await _context.Escalation.ToListAsync();
        }


        public async Task OnPostAsync()
        {
            //Make a table of all the possible ComboBox types.
            Dictionary<string, Type> TableTypeDictionary = new Dictionary<string, Type>()
            {
                { "Status", typeof(Status) },
                { "Category", typeof(Category) },
                { "CallResponse", typeof(CallResponse) },
                { "AttachmentType", typeof(AttachmentType) },
                { "Escalation", typeof(Escalation) }
            };
            //Set a dynamic object to the type that was passed in ObjectType
            dynamic obj = Activator.CreateInstance(TableTypeDictionary[ObjectType]);
            //Loop through the properties of that ObjectType and set the values to what was passed in from ComboBox
            //You need to start at 1 because the first property at position 0 is the lists like Supplier or Call or Attachment
            for (int i = 1; i < obj.GetType().GetProperties().Length; i++)
            {
                obj.GetType().GetProperties()[i].SetValue(obj, ComboBox.GetType().GetProperty(obj.GetType().GetProperties()[i].Name).GetValue(ComboBox, null), null);
            }
            //See if there is an existing Record. If there isn't, make a new one, if there is, update it.
            var ExistingRecord = _context.Find(TableTypeDictionary[ObjectType], obj.Id);
            if (ExistingRecord == null)
            {
                obj.Id = 0;//ID is self growing, no need to customize settings
                _context.Add(obj);
            }
            else
            {
                ExistingRecord.Description = ComboBox.Description;
                ExistingRecord.Hint = ComboBox.Hint;
                _context.Attach(ExistingRecord).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            StatusMessage = "Saved!";
            await OnGetAsync();

        }
    }
}