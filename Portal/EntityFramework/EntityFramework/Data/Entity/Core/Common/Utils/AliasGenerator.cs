using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F0 RID: 1520
	internal sealed class AliasGenerator
	{
		// Token: 0x06004A70 RID: 19056 RVA: 0x00107DE4 File Offset: 0x00105FE4
		internal AliasGenerator(string prefix)
			: this(prefix, 250)
		{
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00107DF4 File Offset: 0x00105FF4
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

		// Token: 0x06004A72 RID: 19058 RVA: 0x00107ED8 File Offset: 0x001060D8
		internal string Next()
		{
			this._counter = Math.Max(1 + this._counter, 0);
			return this.GetName(this._counter);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00107EFC File Offset: 0x001060FC
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

		// Token: 0x04001A30 RID: 6704
		private const int MaxPrefixCount = 500;

		// Token: 0x04001A31 RID: 6705
		private const int CacheSize = 250;

		// Token: 0x04001A32 RID: 6706
		private static readonly string[] _counterNames = new string[250];

		// Token: 0x04001A33 RID: 6707
		private static Dictionary<string, string[]> _prefixCounter;

		// Token: 0x04001A34 RID: 6708
		private int _counter;

		// Token: 0x04001A35 RID: 6709
		private readonly string _prefix;

		// Token: 0x04001A36 RID: 6710
		private readonly string[] _cache;
	}
}
