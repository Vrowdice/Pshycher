using System;
using System.Collections.Generic;
using System.IO;

public class FileManager
{
    /// <summary>
    /// ���� �� ��� ������ �̸��� ���ڷ� ��ȯ�� ����Ʈ�� ��ȯ (���ڰ� �ƴ� ������ ����)
    /// </summary>
    /// <param name="argFolderPath">���� ���</param>
    /// <returns>���ĵ� ���� �̸� ����Ʈ</returns>
    public List<int> GetFileNum(string argFolderPath)
    {
        if (Directory.Exists(argFolderPath))
        {
            List<int> numFileNames = new List<int>();
            string[] files = Directory.GetFiles(argFolderPath);

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (int.TryParse(fileName, out int fileNumber))
                {
                    numFileNames.Add(fileNumber);
                }
            }
            numFileNames.Sort();
            return numFileNames;
        }
        return new List<int>();
    }
}

