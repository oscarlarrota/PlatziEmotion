using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Models
{
    public class EmotionPlatziContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EmotionPlatziContext() : base("name=EmotionPlatziAzure")
        {
            Database.SetInitializer<EmotionPlatziContext>(
                 new DropCreateDatabaseIfModelChanges<EmotionPlatziContext>()
                );
        }

        public DbSet<Emopicture> Emopictures { get; set; }
        public DbSet<EmoFace> EmoFaces { get; set; }
        public DbSet<EmoEmotion> EmoEmotions { get; set; }

        public System.Data.Entity.DbSet<EmotionPlatzi.Models.Home> Homes { get; set; }
    }
}
