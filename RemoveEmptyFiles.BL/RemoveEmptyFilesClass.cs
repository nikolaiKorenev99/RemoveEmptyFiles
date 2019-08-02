using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RemoveEmptyFiles.BL
{
    public class RemoveEmptyFilesClass
    {
        public int RemoveFiles(ref FileInfo[] fAr)
        {
            int count = 0;
            foreach (var item in fAr)
            {
                item.Delete();
                count++;
            }
            for (int i = 0; i < int.MaxValue; i++)
            {

            }
            return count;
        }
        public async Task<int> RemoveFilesAsync(FileInfo[] fAr)
        {
            return await Task<int>.Run(() =>
            {
                return RemoveFiles(ref fAr);
            });
        }
        public string AppendFindFiles(ref FileInfo[] fAr)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Find " + fAr?.Length + " files");
            result.Append(Environment.NewLine);
            foreach (var f in fAr)
            {
                if (f.Length == 0)
                {
                    result.Append(f.FullName);
                    result.Append(Environment.NewLine);
                }
            }
            for (int i = 0; i < int.MaxValue; i++)
            {

            }
            return result.ToString();
        }
        public async Task<string> AppendFindFilesAsync(FileInfo[] fAr)
        {
            return await Task<string>.Run(() =>
            {
            return AppendFindFiles(ref fAr);
        });

        }
}
}
