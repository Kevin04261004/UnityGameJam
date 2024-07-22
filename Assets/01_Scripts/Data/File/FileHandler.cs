using System;
using System.IO;
using UnityEngine;

namespace DYLib
{
    public static class FileHandler
    {
        private static readonly string PERSISTENCE_FILE_PATH = Path.Combine(Application.persistentDataPath, "Data");

        public static bool TryWrite(string relativePath, byte[] data)
        {
            try
            {
                string fullPath = GetPath(relativePath);
                
                string directoryPath = Path.GetDirectoryName(fullPath);
                Debugger.Log($"Dir: {directoryPath}");
                Debugger.Assert(directoryPath != null, "Directory Path가 NULL입니다.");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!File.Exists(fullPath))
                {
                    using (FileStream fs = File.Create(fullPath))
                    {
                        fs.Close();
                    }
                }
                
                File.WriteAllBytes(fullPath, data);
            }
            catch (Exception ex)
            {
                Debugger.LogError("TryWrite File", ex.Message);
                return false;
            }
            return true;
        }

        public static bool TryRead(string relativePath, out byte[] data)
        {
            data = null;
            try
            {
                string fullPath = GetPath(relativePath);

                if (File.Exists(fullPath))
                {
                    data = File.ReadAllBytes(fullPath);
                }
                else
                {
                    Debugger.LogError("TryRead File", "File does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debugger.LogError("TryRead File",ex.Message);
                return false;
            }
            return true;
        }
        
        private static string GetPath(string path)
        {
            return Path.Combine(PERSISTENCE_FILE_PATH, path);
        }
    }
}