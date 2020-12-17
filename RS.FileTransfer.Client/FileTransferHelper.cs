using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Client
{
    public static class FileTransferHelper
    {
        public static byte[] GetSHA1Hash(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    return sha1.ComputeHash(bs);
                }
            }
        }

        public static int GetNumberOfFileParts(string filePath, int partSize)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return (int)(fileInfo.Length / partSize + 1);
        }

        public static byte[] GetFilePartData(string filePath, int partIndex, int partSize)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                long startIndex = partIndex * partSize;
                if (startIndex >= fs.Length)
                    throw new Exception("Invalid partIndex.");

                int buffLength = (int)(fs.Length - (partIndex * partSize));
                buffLength = (buffLength < partSize) ? buffLength : partSize;
                byte[] fileData = new byte[buffLength];

                fs.Seek(startIndex, SeekOrigin.Begin);
                int length = fs.Read(fileData, 0, buffLength);
                return fileData;
            }
        }

        public async static Task CombineFileParts(string destinationFilePath, List<string> filePartPaths)
        {
            using (FileStream destFile = File.Create(destinationFilePath, 100000, FileOptions.None))
            {
                foreach (string partPath in filePartPaths)
                {
                    byte[] buff = null;
                    using (FileStream srcFile = File.OpenRead(partPath))
                    {
                        buff = new byte[srcFile.Length];
                        await srcFile.ReadAsync(buff, 0, (int)srcFile.Length);
                        srcFile.Close();
                    }
                    await destFile.WriteAsync(buff, 0, buff.Length);
                }
                destFile.Close();
            }
        }
    }
}
