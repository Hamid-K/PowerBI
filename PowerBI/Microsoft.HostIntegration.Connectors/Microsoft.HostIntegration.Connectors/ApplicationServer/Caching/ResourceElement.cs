using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	internal class ResourceElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00015607 File Offset: 0x00013807
		public ResourceElement()
		{
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00022E4A File Offset: 0x0002104A
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00022E5C File Offset: 0x0002105C
		[ConfigurationProperty("type", IsRequired = true)]
		public UsageResourceType Type
		{
			get
			{
				return (UsageResourceType)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00022E6F File Offset: 0x0002106F
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00022E81 File Offset: 0x00021081
		[ConfigurationProperty("isCumulative", IsRequired = false)]
		public bool IsCumulative
		{
			get
			{
				return (bool)base["isCumulative"];
			}
			set
			{
				base["isCumulative"] = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00022E94 File Offset: 0x00021094
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00022EA6 File Offset: 0x000210A6
		[ConfigurationProperty("isPerPartition", IsRequired = false)]
		public bool IsPerPartition
		{
			get
			{
				return (bool)base["isPerPartition"];
			}
			set
			{
				base["isPerPartition"] = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00022EB9 File Offset: 0x000210B9
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00022ECB File Offset: 0x000210CB
		[ConfigurationCollection(typeof(ScopeElementCollection), AddItemName = "scope")]
		[ConfigurationProperty("scopes", IsDefaultCollection = false)]
		public ScopeElementCollection Definitions
		{
			get
			{
				return (ScopeElementCollection)base["scopes"];
			}
			set
			{
				base["scopes"] = value;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00022EDC File Offset: 0x000210DC
		public static ResourceElement Filter(ResourceElement resourceElement, LocationType location)
		{
			ScopeElementCollection scopeElementCollection = new ScopeElementCollection();
			foreach (object obj in resourceElement.Definitions)
			{
				ScopeElement scopeElement = (ScopeElement)obj;
				if (scopeElement.Location == location || scopeElement.Location == LocationType.Any)
				{
					scopeElementCollection.Add(scopeElement);
				}
			}
			if (scopeElementCollection.Count == 0)
			{
				return null;
			}
			return new ResourceElement
			{
				Type = resourceElement.Type,
				IsCumulative = resourceElement.IsCumulative,
				IsPerPartition = resourceElement.IsPerPartition,
				Definitions = scopeElementCollection
			};
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022F8C File Offset: 0x0002118C
		protected ResourceElement(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this.Definitions = (ScopeElementCollection)info.GetValue("scopes", typeof(ScopeElementCollection));
			}
			catch (SerializationException)
			{
				this.Definitions = new ScopeElementCollection();
			}
			this.Type = (UsageResourceType)info.GetValue("type", typeof(UsageResourceType));
			try
			{
				this.IsCumulative = info.GetBoolean("isCumulative");
			}
			catch (SerializationException)
			{
				this.IsCumulative = false;
			}
			try
			{
				this.IsPerPartition = info.GetBoolean("isPerPartition");
			}
			catch (SerializationException)
			{
				this.IsPerPartition = false;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00023054 File Offset: 0x00021254
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("type", this.Type);
			info.AddValue("scopes", this.Definitions);
			info.AddValue("isCumulative", this.IsCumulative);
			info.AddValue("isPerPartition", this.IsPerPartition);
		}

		// Token: 0x04000740 RID: 1856
		internal const string RESOURCE = "resource";

		// Token: 0x04000741 RID: 1857
		internal const string RESOURCE_TYPE = "type";

		// Token: 0x04000742 RID: 1858
		internal const string IS_CUMULATIVE = "isCumulative";

		// Token: 0x04000743 RID: 1859
		internal const string IS_PER_PARTITION = "isPerPartition";

		// Token: 0x04000744 RID: 1860
		internal const string SCOPES = "scopes";
	}
}
