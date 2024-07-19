using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace DYLib
{
    public static class Debugger
    {
        private static readonly string formatStr = "<color={0}><b>[{1}]</b> {2}</color>";
        private static readonly string boldStr = "<b>{0}</b>";
        [Conditional("DEBUG")]
        public static void Log(string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.Log(getLog("Log", str, color, bold));
        }
        [Conditional("DEBUG")]
        public static void Log(string header, string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.Log(getLog(header, str, color, bold));
        }
        [Conditional("DEBUG")]
        public static void LogError(string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.LogError(getLog("Error", str, color, bold));
        }
        [Conditional("DEBUG")]
        public static void LogError(string header, string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.LogError(getLog(header, str, color, bold));
        }
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.Assert(condition, getLog("Assertion", str, color, bold));
        }
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string header, string str, EColor color = EColor.white, bool bold = false)
        {
            UnityEngine.Debug.Assert(condition, getLog(header, str, color, bold));
        }
        // [Conditional("DEBUG")]
        // public static void AssertPause(bool condition, string str, EColor color = EColor.white, bool bold = false)
        // {
        //     if (!condition)
        //     {
        //         LogError(str, color, bold);
        //         // EditorApplication.isPaused = true;
        //     }
        // }
        // [Conditional("DEBUG")]
        // public static void AssertExit(bool condition, string str, EColor color = EColor.white, bool bold = false)
        // {
        //     if (!condition)
        //     {
        //         LogError(str, color, bold);
        //         EditorApplication.isPlaying = false;
        //         Application.Quit();
        //     }
        // }
        private static string getLog(string header, string str, EColor color, bool bold)
        {
            string logStr = string.Format(formatStr, color, header, str);
            if (bold)
            {
                logStr = string.Format(boldStr, logStr);
            }

            return logStr;
        }
    }
}