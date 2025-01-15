using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x0200005F RID: 95
	internal sealed class AliasGenerator
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x00013F70 File Offset: 0x00012170
		internal AliasGenerator(string prefix)
			: this(prefix, 250)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00013F80 File Offset: 0x00012180
		internal AliasGenerator(string prefix, int cacheSize)
		{
			this._prefix = prefix ?? string.Empty;
			if (0 < cacheSize)
			{
				string[] array = null;
				Dictionary<string, string[]> prefixCounter;
				while ((prefixCounter = AliasGenerator._prefixCounter) == null || !prefixCounter.TryGetValue(prefix, out this._cache))
				{
					if (array == null)
					{
						array = new string[cacheSize];
					}
					int num = 1 + ((prefixCounter != null) ? prefixCounter.Count : 0);
					Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>(num, StringComparer.InvariantCultureIgnoreCase);
					if (prefixCounter != null && num < 500)
					{
						foreach (KeyValuePair<string, string[]> keyValuePair in prefixCounter)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					dictionary.Add(prefix, array);
					Interlocked.CompareExchange<Dictionary<string, string[]>>(ref AliasGenerator._prefixCounter, dictionary, prefixCounter);
				}
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00014064 File Offset: 0x00012264
		internal string Next()
		{
			this._counter = Math.Max(1 + this._counter, 0);
			return this.GetName(this._counter);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00014088 File Offset: 0x00012288
		internal string GetName(int index)
		{
			string text;
			if (this._cache == null || this._cache.Length <= index)
			{
				text = this._prefix + index.ToString(CultureInfo.InvariantCulture);
			}
			else if ((text = this._cache[index]) == null)
			{
				if (AliasGenerator._counterNames.Length <= index)
				{
					text = index.ToString(CultureInfo.InvariantCulture);
				}
				else if ((text = AliasGenerator._counterNames[index]) == null)
				{
					text = (AliasGenerator._counterNames[index] = index.ToString(CultureInfo.InvariantCulture));
				}
				text = (this._cache[index] = this._prefix + text);
			}
			return text;
		}

		// Token: 0x040006EE RID: 1774
		private const int MaxPrefixCount = 500;

		// Token: 0x040006EF RID: 1775
		private const int CacheSize = 250;

		// Token: 0x040006F0 RID: 1776
		private static readonly string[] _counterNames = new string[250];

		// Token: 0x040006F1 RID: 1777
		private static Dictionary<string, string[]> _prefixCounter;

		// Token: 0x040006F2 RID: 1778
		private int _counter;

		// Token: 0x040006F3 RID: 1779
		private readonly string _prefix;

		// Token: 0x040006F4 RID: 1780
		private string[] _cache;
	}
}
