using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeForDummies.Models
{
    public class ImageAndRecipeConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ImageAndRecipeConnectionId { get; set; }

        [ForeignKey("Recipe")]
        public virtual int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public byte[] Image { get; set; }
    }
}
