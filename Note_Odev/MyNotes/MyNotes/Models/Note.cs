using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNotes.Models
{
    public class Note
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Note Header is Required.")]
        [Display(Name ="Note Header")]
        [DataType(DataType.Text)]
        [StringLength(300,ErrorMessage ="Note Header should be less than or 300 characters long.")]
        public string NoteHeader { get; set; }
        
        [Required(ErrorMessage = "Note Content is Required.")]
        [Display(Name ="Note Content")]
        [StringLength(1100,ErrorMessage ="Note Header should be less than or 1100 characters long.")]
        [DataType(DataType.MultilineText)]
        public string NoteContent { get; set; }
        
        [Display(Name ="Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? NoteTime { get; set; } = DateTime.Now;

        
        [Display(Name ="Is Star")]
        public bool IsStar { get; set; } = false; //0:false, 1:true in myNotesDB
    }
}