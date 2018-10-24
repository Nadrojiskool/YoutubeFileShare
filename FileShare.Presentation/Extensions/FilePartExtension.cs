using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Presentation.Extensions
{
    public static class FilePartExtension
    {
        public static T[] GetFilePart<T>(this T[] array, int take, int skip = 0)
        {
            if (array.Length == 0 || take == 0)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (skip == 0)
            {
                return array.Take(take).ToArray();
            }
            return array.Skip(skip).Take(take).ToArray();
        }
    }
}
