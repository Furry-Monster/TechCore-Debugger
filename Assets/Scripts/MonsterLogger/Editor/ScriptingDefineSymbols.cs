using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace MonsterLogger.Editor
{
    public static class ScriptingDefineSymbols
    {
        private static readonly BuildTargetGroup[] BuildTargetGroups =
        {
            BuildTargetGroup.Standalone,
            BuildTargetGroup.iOS,
            BuildTargetGroup.Android,
        };

        /// <summary>
        /// 检查指定平台是否存在指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTargetGroup">要检查脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbol">要检查的脚本宏定义。</param>
        /// <returns>指定平台是否存在指定的脚本宏定义。</returns>
        public static bool HasScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
                return false;

            var symbolStrings = GetScriptingDefineSymbols(buildTargetGroup);
            return symbolStrings
                .Any(symbolStr => symbolStr == scriptingDefineSymbol);
        }

        /// <summary>
        /// 为指定平台增加指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTargetGroup">要增加脚本宏定义的平台。</param>
        /// <param name="symbolToAdd">要增加的脚本宏定义。</param>
        public static void AddScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string symbolToAdd)
        {
            if (string.IsNullOrEmpty(symbolToAdd))
                return;
            if (HasScriptingDefineSymbol(buildTargetGroup, symbolToAdd))
                return;

            var symbolList = new List<string>(GetScriptingDefineSymbols(buildTargetGroup))
            {
                symbolToAdd
            };

            SetScriptingDefineSymbols(buildTargetGroup, symbolList.ToArray());
        }

        /// <summary>
        /// 为指定平台移除指定的脚本宏定义。
        /// </summary>
        /// <param name="buildTargetGroup">要移除脚本宏定义的平台。</param>
        /// <param name="symbolToRemove">要移除的脚本宏定义。</param>
        public static void RemoveScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string symbolToRemove)
        {
            if (string.IsNullOrEmpty(symbolToRemove))
                return;

            if (!HasScriptingDefineSymbol(buildTargetGroup, symbolToRemove))
                return;

            var symbolList = new List<string>(GetScriptingDefineSymbols(buildTargetGroup));
            while (symbolList.Contains(symbolToRemove))
                symbolList.Remove(symbolToRemove);

            SetScriptingDefineSymbols(buildTargetGroup, symbolList.ToArray());
        }

        /// <summary>
        /// 检查所有平台中是否有任意一个存在指定的脚本宏定义。
        /// </summary>
        /// <param name="scriptingDefineSymbol">要检查的脚本宏定义。</param>
        /// <returns>指定平台是否存在指定的脚本宏定义。</returns >
        public static bool AnyScriptingDefineSymbol(string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
                return false;

            var buildTargetCound = BuildTargetGroups.Length;
            for (var i = 0; i < buildTargetCound; i++)
            {
                var buildTargetGroup = BuildTargetGroups[i];
                if (HasScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol))
                    return true;
            }

            return buildTargetCound == 0;
        }

        /// <summary>
        /// 逐个检查所有平台是否存在指定的脚本宏定义。
        /// </summary>
        /// <param name="scriptingDefineSymbol">要检查的脚本宏定义。</param>
        /// <returns>指定平台是否存在指定的脚本宏定义。</returns>
        public static BitArray HasScriptingDefineSymbol(string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
                return new BitArray(BuildTargetGroups.Length, false);

            var buildTargetCount = BuildTargetGroups.Length;
            var result = new BitArray(buildTargetCount);
            for (var i = 0; i < buildTargetCount; i++)
            {
                var buildTargetGroup = BuildTargetGroups[i];
                result[i] = HasScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol);
            }

            return result;
        }

        /// <summary>
        /// 为所有平台增加指定的脚本宏定义。
        /// </summary>
        /// <param name="symbolToAdd">要增加的脚本宏定义。</param>
        public static void AddScriptingDefineSymbol(string symbolToAdd)
        {
            if (string.IsNullOrEmpty(symbolToAdd))
                return;

            foreach (var buildTargetGroup in BuildTargetGroups)
                AddScriptingDefineSymbol(buildTargetGroup, symbolToAdd);
        }

        /// <summary>
        /// 为所有平台移除指定的脚本宏定义。
        /// </summary>
        /// <param name="scriptingDefineSymbol">要移除的脚本宏定义。</param>
        public static void RemoveScriptingDefineSymbol(string scriptingDefineSymbol)
        {
            if (string.IsNullOrEmpty(scriptingDefineSymbol))
                return;

            foreach (var buildTargetGroup in BuildTargetGroups)
                RemoveScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol);
        }

        /// <summary>
        /// 获取指定平台的脚本宏定义。
        /// </summary>
        /// <param name="buildTargetGroup">要获取脚本宏定义的平台。</param>
        /// <returns>平台的脚本宏定义。</returns>
        public static string[] GetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup) =>
            PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');

        /// <summary>
        /// 设置指定平台的脚本宏定义。
        /// </summary>
        /// <param name="buildTargetGroup">要设置脚本宏定义的平台。</param>
        /// <param name="scriptingDefineSymbols">要设置的脚本宏定义。</param>
        public static void
            SetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup, string[] scriptingDefineSymbols) =>
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,
                string.Join(";", scriptingDefineSymbols));
    }
}