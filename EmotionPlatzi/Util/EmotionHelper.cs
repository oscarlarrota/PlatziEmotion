using EmotionPlatzi.Models;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Face;










namespace EmotionPlatzi.Util
{
    public class EmotionHelper
    {
        public FaceServiceClient emoClient;

        public EmotionHelper(string key)
         {
             emoClient = new FaceServiceClient(key);
       
        }

        public async Task<Emopicture>  DetectAndExtracfacesAsync(Stream imageStream)
        {

            IEnumerable<FaceAttributeType> faceAttributes = new FaceAttributeType[] { FaceAttributeType.Emotion };
            Face[] faces = await emoClient.DetectAsync(imageStream, false, false, faceAttributes);

            var emoPicture = new Emopicture();

            emoPicture.Faces = ExtractFaces(faces, emoPicture);

            return emoPicture;

        }

        private ObservableCollection<EmoFace> ExtractFaces(Face[] faces, Emopicture emoPicture)
        {
            var listaFaces = new ObservableCollection<EmoFace>();

            foreach (var face in faces)
            {
                var emoFace = new EmoFace()
                {
                    X = face.FaceRectangle.Left,
                    Y = face.FaceRectangle.Top,
                    Width = face.FaceRectangle.Width,
                    Height = face.FaceRectangle.Height,
                    Picture = emoPicture

                };
                emoFace.Emotions = ProcessEmotions(face.FaceAttributes.Emotion, emoFace);
                listaFaces.Add(emoFace);

            }

            return listaFaces;
        }

        private ObservableCollection<EmoEmotion> ProcessEmotions(EmotionScores scores, EmoFace emoFace)
        {
            var emotionList = new ObservableCollection<EmoEmotion>();
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var filterproperties = properties.Where(p => p.PropertyType == typeof(float));

            var emoType = EmoEmotionEnum.Undetermined;
            foreach (var prop in filterproperties)
            {
                if (!Enum.TryParse<EmoEmotionEnum>(prop.Name, out emoType))
                    emoType = EmoEmotionEnum.Undetermined;
                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emoType;
                emoEmotion.EmoFaceId = emoFace.Id;
                emoEmotion.Face = emoFace;

                emotionList.Add(emoEmotion);

            }

            return emotionList;
        }
    }
}