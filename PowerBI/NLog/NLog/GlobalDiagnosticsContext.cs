using System;
using System.Collections.Generic;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000003 RID: 3
	public static class GlobalDiagnosticsContext
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002089 File Offset: 0x00000289
		public static void Set(string item, string value)
		{
			GlobalDiagnosticsContext.Set(item, value);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		public static void Set(string item, object value)
		{
			object lockObject = GlobalDiagnosticsContext._lockObject;
			lock (lockObject)
			{
				GlobalDiagnosticsContext.GetWritableDict(GlobalDiagnosticsContext._dictReadOnly != null && !GlobalDiagnosticsContext._dict.ContainsKey(item), false)[item] = value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F4 File Offset: 0x000002F4
		public static string Get(string item)
		{
			return GlobalDiagnosticsContext.Get(item, null);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020FD File Offset: 0x000002FD
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(GlobalDiagnosticsContext.GetObject(item), formatProvider);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000210C File Offset: 0x0000030C
		public static object GetObject(string item)
		{
			object obj;
			GlobalDiagnosticsContext.GetReadOnlyDict().TryGetValue(item, out obj);
			return obj;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002128 File Offset: 0x00000328
		public static ICollection<string> GetNames()
		{
			return GlobalDiagnosticsContext.GetReadOnlyDict().Keys;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002134 File Offset: 0x00000334
		public static bool Contains(string item)
		{
			return GlobalDiagnosticsContext.GetReadOnlyDict().ContainsKey(item);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		public static void Remove(string item)
		{
			object lockObject = GlobalDiagnosticsContext._lockObject;
			lock (lockObject)
			{
				GlobalDiagnosticsContext.GetWritableDict(GlobalDiagnosticsContext._dictReadOnly != null && GlobalDiagnosticsContext._dict.ContainsKey(item), false).Remove(item);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A0 File Offset: 0x000003A0
		public static void Clear()
		{
			object lockObject = GlobalDiagnosticsContext._lockObject;
			lock (lockObject)
			{
				GlobalDiagnosticsContext.GetWritableDict(GlobalDiagnosticsContext._dictReadOnly != null && GlobalDiagnosticsContext._dict.Count > 0, true).Clear();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021FC File Offset: 0x000003FC
		private static Dictionary<string, object> GetReadOnlyDict()
		{
			Dictionary<string, object> dictionary = GlobalDiagnosticsContext._dictReadOnly;
			if (dictionary == null)
			{
				object lockObject = GlobalDiagnosticsContext._lockObject;
				lock (lockObject)
				{
					dictionary = (GlobalDiagnosticsContext._dictReadOnly = GlobalDiagnosticsContext._dict);
				}
			}
			return dictionary;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224C File Offset: 0x0000044C
		private static Dictionary<string, object> GetWritableDict(bool requireCopyOnWrite, bool clearDictionary = false)
		{
			if (requireCopyOnWrite)
			{
				GlobalDiagnosticsContext._dict = GlobalDiagnosticsContext.CopyDictionaryOnWrite(clearDictionary);
				GlobalDiagnosticsContext._dictReadOnly = null;
			}
			return GlobalDiagnosticsContext._dict;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		private static Dictionary<string, object> CopyDictionaryOnWrite(bool clearDictionary)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(clearDictionary ? 0 : (GlobalDiagnosticsContext._dict.Count + 1));
			if (!clearDictionary)
			{
				foreach (KeyValuePair<string, object> keyValuePair in GlobalDiagnosticsContext._dict)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x04000001 RID: 1
		private static readonly object _lockObject = new object();

		// Token: 0x04000002 RID: 2
		private static Dictionary<string, object> _dict = new Dictionary<string, object>();

		// Token: 0x04000003 RID: 3
		private static Dictionary<string, object> _dictReadOnly;
	}
}
