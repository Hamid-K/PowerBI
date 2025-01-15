using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000041 RID: 65
	public class NameValuePairsValueProvider : IEnumerableValueProvider, IValueProvider
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00005C52 File Offset: 0x00003E52
		public NameValuePairsValueProvider(IEnumerable<KeyValuePair<string, string>> values, CultureInfo culture)
		{
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._values = NameValuePairsValueProvider.InitializeValues<string>(values);
			this._culture = culture;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005C7C File Offset: 0x00003E7C
		public NameValuePairsValueProvider(Func<IEnumerable<KeyValuePair<string, string>>> valuesFactory, CultureInfo culture)
		{
			if (valuesFactory == null)
			{
				throw Error.ArgumentNull("valuesFactory");
			}
			this._lazyValues = new Lazy<Dictionary<string, object>>(() => NameValuePairsValueProvider.InitializeValues<string>(valuesFactory()), true);
			this._culture = culture;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005CCE File Offset: 0x00003ECE
		public NameValuePairsValueProvider(IDictionary<string, object> values, CultureInfo culture)
		{
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._values = NameValuePairsValueProvider.InitializeValues<object>(values);
			this._culture = culture;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005CF7 File Offset: 0x00003EF7
		internal CultureInfo Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005CFF File Offset: 0x00003EFF
		private PrefixContainer PrefixContainer
		{
			get
			{
				if (this._prefixContainer == null)
				{
					this._prefixContainer = new PrefixContainer(this.Values.Keys);
				}
				return this._prefixContainer;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005D25 File Offset: 0x00003F25
		private Dictionary<string, object> Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = this._lazyValues.Value;
				}
				return this._values;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005D48 File Offset: 0x00003F48
		private static Dictionary<string, object> InitializeValues<T>(IEnumerable<KeyValuePair<string, T>> nameValuePairs) where T : class
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			KeyValuePair<string, string>[] array = nameValuePairs as KeyValuePair<string, string>[];
			if (array != null && array.Length == 0)
			{
				return dictionary;
			}
			Dictionary<string, object> dictionary2 = nameValuePairs as Dictionary<string, object>;
			if (dictionary2 != null && dictionary2.Count == 0)
			{
				return dictionary;
			}
			foreach (KeyValuePair<string, T> keyValuePair in nameValuePairs)
			{
				string key = keyValuePair.Key;
				object obj;
				if (dictionary.TryGetValue(key, out obj))
				{
					List<T> list = obj as List<T>;
					if (list == null)
					{
						dictionary[key] = new List<T>
						{
							obj as T,
							keyValuePair.Value
						};
					}
					else
					{
						list.Add(keyValuePair.Value);
					}
				}
				else
				{
					dictionary[key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005E34 File Offset: 0x00004034
		public virtual bool ContainsPrefix(string prefix)
		{
			return this.PrefixContainer.ContainsPrefix(prefix);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005E42 File Offset: 0x00004042
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw Error.ArgumentNull("prefix");
			}
			return this.PrefixContainer.GetKeysFromPrefix(prefix);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005E60 File Offset: 0x00004060
		public virtual ValueProviderResult GetValue(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			object obj;
			if (this.Values.TryGetValue(key, out obj))
			{
				return new ValueProviderResult(obj, NameValuePairsValueProvider.GetAttemptedValue(obj), this._culture);
			}
			return null;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005EA0 File Offset: 0x000040A0
		private static string GetAttemptedValue(object value)
		{
			List<string> list = value as List<string>;
			if (list == null)
			{
				return value as string;
			}
			return string.Join(",", list);
		}

		// Token: 0x04000059 RID: 89
		private readonly CultureInfo _culture;

		// Token: 0x0400005A RID: 90
		private PrefixContainer _prefixContainer;

		// Token: 0x0400005B RID: 91
		private Dictionary<string, object> _values;

		// Token: 0x0400005C RID: 92
		private readonly Lazy<Dictionary<string, object>> _lazyValues;
	}
}
