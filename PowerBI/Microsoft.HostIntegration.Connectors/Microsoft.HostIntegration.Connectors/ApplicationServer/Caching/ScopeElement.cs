using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	internal class ScopeElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00015607 File Offset: 0x00013807
		public ScopeElement()
		{
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002316E File Offset: 0x0002136E
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00023180 File Offset: 0x00021380
		[ConfigurationProperty("processAt", IsRequired = true)]
		public ProcessAtType ProcessAt
		{
			get
			{
				return (ProcessAtType)base["processAt"];
			}
			set
			{
				base["processAt"] = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00023193 File Offset: 0x00021393
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x000231A5 File Offset: 0x000213A5
		[ConfigurationProperty("location", IsRequired = false, DefaultValue = LocationType.Any)]
		public LocationType Location
		{
			get
			{
				return (LocationType)base["location"];
			}
			set
			{
				base["location"] = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x000231B8 File Offset: 0x000213B8
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x000231CA File Offset: 0x000213CA
		[ConfigurationProperty("isAligned", IsRequired = false, DefaultValue = true)]
		public bool IsAligned
		{
			get
			{
				return (bool)base["isAligned"];
			}
			set
			{
				base["isAligned"] = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x000231DD File Offset: 0x000213DD
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x000231EF File Offset: 0x000213EF
		[ConfigurationProperty("interval", IsRequired = false)]
		public IntervalProperties IntervalProperties
		{
			get
			{
				return (IntervalProperties)base["interval"];
			}
			set
			{
				base["interval"] = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000231FD File Offset: 0x000213FD
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0002320F File Offset: 0x0002140F
		[ConfigurationProperty("persistenceProviders", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(PersistenceProviderElementCollection), AddItemName = "persistenceProvider")]
		public PersistenceProviderElementCollection PersistenceProviders
		{
			get
			{
				return (PersistenceProviderElementCollection)base["persistenceProviders"];
			}
			set
			{
				base["persistenceProviders"] = value;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00023220 File Offset: 0x00021420
		protected ScopeElement(SerializationInfo info, StreamingContext context)
		{
			this.Name = info.GetString("name");
			this.ProcessAt = (ProcessAtType)info.GetValue("processAt", typeof(ProcessAtType));
			try
			{
				this.Location = (LocationType)info.GetValue("location", typeof(LocationType));
			}
			catch (SerializationException)
			{
				this.Location = LocationType.Any;
			}
			try
			{
				this.PersistenceProviders = (PersistenceProviderElementCollection)info.GetValue("persistenceProviders", typeof(PersistenceProviderElementCollection));
			}
			catch (SerializationException)
			{
				this.PersistenceProviders = new PersistenceProviderElementCollection();
			}
			try
			{
				this.IntervalProperties = (IntervalProperties)info.GetValue("interval", typeof(IntervalProperties));
			}
			catch (SerializationException)
			{
				this.IntervalProperties = new IntervalProperties();
			}
			try
			{
				this.IsAligned = info.GetBoolean("isAligned");
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002333C File Offset: 0x0002153C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("name", this.Name);
			info.AddValue("processAt", this.ProcessAt);
			info.AddValue("location", this.Location);
			info.AddValue("isAligned", this.IsAligned);
			info.AddValue("interval", this.IntervalProperties);
			info.AddValue("persistenceProviders", this.PersistenceProviders);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000233BC File Offset: 0x000215BC
		public override string ToString()
		{
			return string.Concat(new object[] { this.Name, "#", this.ProcessAt, "#", this.Location });
		}

		// Token: 0x04000745 RID: 1861
		internal const string SCOPE = "scope";

		// Token: 0x04000746 RID: 1862
		internal const string PERSISTENCE_PROVIDERS = "persistenceProviders";

		// Token: 0x04000747 RID: 1863
		internal const string NAME = "name";

		// Token: 0x04000748 RID: 1864
		internal const string PROCESS_AT = "processAt";

		// Token: 0x04000749 RID: 1865
		internal const string LOCATION = "location";

		// Token: 0x0400074A RID: 1866
		internal const string IS_ALIGNED = "isAligned";

		// Token: 0x0400074B RID: 1867
		internal const string INTERVAL = "interval";
	}
}
