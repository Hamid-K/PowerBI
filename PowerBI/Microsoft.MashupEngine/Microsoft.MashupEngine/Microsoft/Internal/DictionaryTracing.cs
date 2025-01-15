using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Internal
{
	// Token: 0x0200017B RID: 379
	internal static class DictionaryTracing
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		public static void AddWithTracing<K, V>(IDictionary<K, V> dictionary, K key, V value, IEngineHost host, bool keyIsPii, bool valueIsPii)
		{
			try
			{
				dictionary.Add(key, value);
			}
			catch (ArgumentException)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "DictionaryExtensions/AddWithTracing", TraceEventType.Information, null))
				{
					hostTrace.Add("Key", key, keyIsPii);
					hostTrace.Add("OriginalValue", DictionaryTracing.GetString(dictionary[key], 0), valueIsPii);
					hostTrace.Add("DuplicateValue", DictionaryTracing.GetString(value, 0), valueIsPii);
				}
				throw;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000C650 File Offset: 0x0000A850
		private static string GetString(object value, int depth = 0)
		{
			if (value == null)
			{
				return "<null>";
			}
			if (value == DBNull.Value)
			{
				return "<DBNull>";
			}
			if (value is Value)
			{
				return ((Value)value).ToSource();
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable == null || depth >= 5 || value is string)
			{
				return value.ToString();
			}
			return "{" + string.Join(", ", (from object o in enumerable
				select DictionaryTracing.GetString(o, depth + 1)).ToArray<string>()) + "}";
		}
	}
}
