using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x0200021D RID: 541
	internal class EngineFeatures : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000AEA RID: 2794 RVA: 0x00019070 File Offset: 0x00017270
		private EngineFeatures()
		{
			this.features = new Dictionary<string, EngineFeatures.Feature>();
			this.uriNormalizationVersion = new EngineFeatures.Feature<int>("UriNormalizationVersion", 0);
			this.fileNormalizationVersion = new EngineFeatures.Feature<int>("FileNormalizationVersion", 1);
			this.AddFeatures(new EngineFeatures.Feature[] { this.uriNormalizationVersion, this.fileNormalizationVersion });
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x000190D9 File Offset: 0x000172D9
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x000190E6 File Offset: 0x000172E6
		public int UriNormalizationVersion
		{
			get
			{
				return this.uriNormalizationVersion.TypedValue;
			}
			set
			{
				this.uriNormalizationVersion.TypedValue = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000190F4 File Offset: 0x000172F4
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00019101 File Offset: 0x00017301
		public int FileNormalizationVersion
		{
			get
			{
				return this.fileNormalizationVersion.TypedValue;
			}
			set
			{
				this.fileNormalizationVersion.TypedValue = value;
			}
		}

		// Token: 0x1700032A RID: 810
		public object this[string key]
		{
			get
			{
				Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
				object obj;
				lock (dictionary)
				{
					EngineFeatures.Feature feature;
					if (this.features.TryGetValue(key, out feature))
					{
						obj = feature.Value;
					}
					else
					{
						obj = null;
					}
				}
				return obj;
			}
			set
			{
				Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
				lock (dictionary)
				{
					EngineFeatures.Feature feature;
					if (this.features.TryGetValue(key, out feature))
					{
						feature.Value = value;
					}
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000191BC File Offset: 0x000173BC
		public ICollection<string> Keys
		{
			get
			{
				Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
				ICollection<string> collection;
				lock (dictionary)
				{
					collection = this.features.Keys.ToArray<string>();
				}
				return collection;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00019208 File Offset: 0x00017408
		public ICollection<object> Values
		{
			get
			{
				Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
				ICollection<object> collection;
				lock (dictionary)
				{
					collection = this.features.Values.Select((EngineFeatures.Feature f) => f.Value).ToArray<object>();
				}
				return collection;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00019278 File Offset: 0x00017478
		public int Count
		{
			get
			{
				Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
				int count;
				lock (dictionary)
				{
					count = this.features.Count;
				}
				return count;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Add(string key, object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Add(KeyValuePair<string, object> item)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Clear()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000EE09 File Offset: 0x0000D009
		public bool Contains(KeyValuePair<string, object> item)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000192C0 File Offset: 0x000174C0
		public bool ContainsKey(string key)
		{
			Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
			bool flag2;
			lock (dictionary)
			{
				flag2 = this.features.ContainsKey(key);
			}
			return flag2;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00019308 File Offset: 0x00017508
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
			IEnumerator<KeyValuePair<string, object>> enumerator;
			lock (dictionary)
			{
				enumerator = this.features.Select((KeyValuePair<string, EngineFeatures.Feature> pair) => new KeyValuePair<string, object>(pair.Key, pair.Value.Value)).ToList<KeyValuePair<string, object>>().GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000EE09 File Offset: 0x0000D009
		public bool Remove(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000EE09 File Offset: 0x0000D009
		public bool Remove(KeyValuePair<string, object> item)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00019380 File Offset: 0x00017580
		public bool TryGetValue(string key, out object value)
		{
			Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
			bool flag2;
			lock (dictionary)
			{
				EngineFeatures.Feature feature;
				if (this.features.TryGetValue(key, out feature))
				{
					value = feature.Value;
					flag2 = true;
				}
				else
				{
					value = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000193DC File Offset: 0x000175DC
		IEnumerator IEnumerable.GetEnumerator()
		{
			Dictionary<string, EngineFeatures.Feature> dictionary = this.features;
			IEnumerator enumerator;
			lock (dictionary)
			{
				enumerator = this.features.ToArray<KeyValuePair<string, EngineFeatures.Feature>>().GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00019428 File Offset: 0x00017628
		private void AddFeatures(params EngineFeatures.Feature[] features)
		{
			foreach (EngineFeatures.Feature feature in features)
			{
				this.features.Add(feature.Name, feature);
			}
		}

		// Token: 0x04000668 RID: 1640
		public static readonly EngineFeatures Instance = new EngineFeatures();

		// Token: 0x04000669 RID: 1641
		private readonly Dictionary<string, EngineFeatures.Feature> features = new Dictionary<string, EngineFeatures.Feature>();

		// Token: 0x0400066A RID: 1642
		private readonly EngineFeatures.Feature<int> uriNormalizationVersion;

		// Token: 0x0400066B RID: 1643
		private readonly EngineFeatures.Feature<int> fileNormalizationVersion;

		// Token: 0x0200021E RID: 542
		private abstract class Feature
		{
			// Token: 0x06000B02 RID: 2818 RVA: 0x00019467 File Offset: 0x00017667
			protected Feature(string name)
			{
				this.name = name;
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00019476 File Offset: 0x00017676
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000B04 RID: 2820
			// (set) Token: 0x06000B05 RID: 2821
			public abstract object Value { get; set; }

			// Token: 0x0400066C RID: 1644
			private readonly string name;
		}

		// Token: 0x0200021F RID: 543
		private sealed class Feature<T> : EngineFeatures.Feature
		{
			// Token: 0x06000B06 RID: 2822 RVA: 0x0001947E File Offset: 0x0001767E
			public Feature(string name, T defaultValue)
				: base(name)
			{
				this.value = defaultValue;
			}

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0001948E File Offset: 0x0001768E
			// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0001949B File Offset: 0x0001769B
			public override object Value
			{
				get
				{
					return this.TypedValue;
				}
				set
				{
					this.TypedValue = (T)((object)value);
				}
			}

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000194AC File Offset: 0x000176AC
			// (set) Token: 0x06000B0A RID: 2826 RVA: 0x000194EC File Offset: 0x000176EC
			public T TypedValue
			{
				get
				{
					T t;
					lock (this)
					{
						t = this.value;
					}
					return t;
				}
				set
				{
					lock (this)
					{
						this.value = value;
					}
				}
			}

			// Token: 0x0400066D RID: 1645
			private T value;
		}
	}
}
