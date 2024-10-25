namespace OCOO.Models
{
    public class FileExtension
    {
        public static bool IsPdf(string filepath)
        {
            if (filepath.Contains(".pdf"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
