using System;
using System.Collections;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	internal class UsageProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00015607 File Offset: 0x00013807
		public UsageProperties()
		{
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00022A20 File Offset: 0x00020C20
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00022A32 File Offset: 0x00020C32
		[ConfigurationProperty("resources", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(ResourceElementCollection), AddItemName = "resource")]
		public ResourceElementCollection Resources
		{
			get
			{
				return (ResourceElementCollection)base["resources"];
			}
			set
			{
				base["resources"] = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00022A40 File Offset: 0x00020C40
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00022A52 File Offset: 0x00020C52
		[ConfigurationProperty("providers", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(KnownUsageProviderElementCollection), AddItemName = "provider")]
		public KnownUsageProviderElementCollection Providers
		{
			get
			{
				return (KnownUsageProviderElementCollection)base["providers"];
			}
			set
			{
				base["providers"] = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00022A60 File Offset: 0x00020C60
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x00022A72 File Offset: 0x00020C72
		[ConfigurationProperty("isUsageEnabled", IsRequired = false, DefaultValue = false)]
		public bool UsageEnabled
		{
			get
			{
				return (bool)base["isUsageEnabled"];
			}
			set
			{
				base["isUsageEnabled"] = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00022A85 File Offset: 0x00020C85
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00022A97 File Offset: 0x00020C97
		[ConfigurationProperty("aggregatorProvider", IsRequired = false)]
		public string AggregatorProvider
		{
			get
			{
				return (string)base["aggregatorProvider"];
			}
			set
			{
				base["aggregatorProvider"] = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00022AA5 File Offset: 0x00020CA5
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x00022AB7 File Offset: 0x00020CB7
		[ConfigurationProperty("sourceProvider", IsRequired = false)]
		public string SourceProvider
		{
			get
			{
				return (string)base["sourceProvider"];
			}
			set
			{
				base["sourceProvider"] = value;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00022AC8 File Offset: 0x00020CC8
		public static UsageProperties Filter(UsageProperties usageProperties, LocationType location)
		{
			UsageProperties usageProperties2 = new UsageProperties();
			usageProperties2.UsageEnabled = usageProperties.UsageEnabled;
			usageProperties2.AggregatorProvider = usageProperties.AggregatorProvider;
			usageProperties2.SourceProvider = usageProperties.SourceProvider;
			usageProperties2.Providers = usageProperties.Providers;
			usageProperties2.Resources = new ResourceElementCollection();
			foreach (object obj in usageProperties.Resources)
			{
				ResourceElement resourceElement = (ResourceElement)obj;
				ResourceElement resourceElement2 = ResourceElement.Filter(resourceElement, location);
				if (resourceElement2 != null)
				{
					usageProperties2.Resources.Add(resourceElement2);
				}
			}
			return usageProperties2;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00022B78 File Offset: 0x00020D78
		internal TimeSpan GetResourceSyncInterval(string scopeName, UsageResourceType resourceType)
		{
			TimeSpan timeSpan = default(TimeSpan);
			if (string.IsNullOrEmpty(scopeName))
			{
				return timeSpan;
			}
			foreach (object obj in this.Resources)
			{
				ResourceElement resourceElement = (ResourceElement)obj;
				if (resourceElement.Type == resourceType)
				{
					using (IEnumerator enumerator2 = resourceElement.Definitions.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							ScopeElement scopeElement = (ScopeElement)obj2;
							if (scopeElement.Name.Equals(scopeName, StringComparison.Ordinal))
							{
								timeSpan = scopeElement.IntervalProperties.SyncInterval;
								break;
							}
						}
						break;
					}
				}
			}
			return timeSpan;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00022C50 File Offset: 0x00020E50
		protected UsageProperties(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this.SourceProvider = info.GetString("sourceProvider");
			}
			catch (SerializationException)
			{
			}
			try
			{
				this.AggregatorProvider = info.GetString("aggregatorProvider");
			}
			catch (SerializationException)
			{
			}
			try
			{
				this.Resources = (ResourceElementCollection)info.GetValue("resources", typeof(ResourceElementCollection));
			}
			catch (SerializationException)
			{
				this.Resources = new ResourceElementCollection();
			}
			try
			{
				this.Providers = (KnownUsageProviderElementCollection)info.GetValue("providers", typeof(KnownUsageProviderElementCollection));
			}
			catch (SerializationException)
			{
				this.Providers = new KnownUsageProviderElementCollection();
			}
			try
			{
				this.UsageEnabled = info.GetBoolean("isUsageEnabled");
			}
			catch (SerializationException)
			{
				this.UsageEnabled = false;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00022D4C File Offset: 0x00020F4C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("resources", this.Resources);
			info.AddValue("providers", this.Providers);
			info.AddValue("isUsageEnabled", this.UsageEnabled);
		}

		// Token: 0x0400073B RID: 1851
		internal const string AGGREGATOR_PROVIDER = "aggregatorProvider";

		// Token: 0x0400073C RID: 1852
		internal const string SOURCE_PROVIDER = "sourceProvider";

		// Token: 0x0400073D RID: 1853
		internal const string RESOURCES = "resources";

		// Token: 0x0400073E RID: 1854
		internal const string PROVIDERS = "providers";

		// Token: 0x0400073F RID: 1855
		internal const string USAGE_SWITCH = "isUsageEnabled";
	}
}
