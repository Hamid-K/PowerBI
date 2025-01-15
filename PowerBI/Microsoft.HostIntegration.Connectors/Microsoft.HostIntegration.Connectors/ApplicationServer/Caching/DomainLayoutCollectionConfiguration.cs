using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	internal class DomainLayoutCollectionConfiguration : ConfigurationElement, IDomainLayoutCollectionConfiguration, ISerializable
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00015607 File Offset: 0x00013807
		public DomainLayoutCollectionConfiguration()
		{
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001C52C File Offset: 0x0001A72C
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001C54B File Offset: 0x0001A74B
		[ConfigurationCollection(typeof(DomainLayoutConfigurationElementCollection), AddItemName = "domain")]
		[ConfigurationProperty("domains", IsDefaultCollection = false)]
		public DomainLayoutConfigurationElementCollection DomainCollection
		{
			get
			{
				return (DomainLayoutConfigurationElementCollection)base["domains"];
			}
			internal set
			{
				base["domains"] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001C55C File Offset: 0x0001A75C
		public IEnumerable<IDomainLayoutConfiguration> DomainLayout
		{
			get
			{
				DomainLayoutConfigurationElementCollection domainCollection = this.DomainCollection;
				if (domainCollection == null || domainCollection.Count <= 0)
				{
					return null;
				}
				DomainLayoutConfiguration[] array = new DomainLayoutConfiguration[domainCollection.Count];
				this.DomainCollection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001C598 File Offset: 0x0001A798
		protected DomainLayoutCollectionConfiguration(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.DomainCollection = (DomainLayoutConfigurationElementCollection)info.GetValue("domains", typeof(DomainLayoutConfigurationElementCollection));
			this.DomainCollection.InitializeAfterSerialization();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("domains", this.DomainCollection);
		}

		// Token: 0x04000643 RID: 1603
		internal const string DomainCollectionProperty = "domains";
	}
}
