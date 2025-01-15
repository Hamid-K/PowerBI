using System;
using System.Configuration;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	internal class DomainLayoutConfiguration : ConfigurationElement, IDomainLayoutConfiguration, ISerializable
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00015607 File Offset: 0x00013807
		public DomainLayoutConfiguration()
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001C257 File Offset: 0x0001A457
		public DomainLayoutConfiguration(IDomainLayoutConfiguration domainLayout)
		{
			if (domainLayout == null)
			{
				throw new ArgumentNullException("domainLayout");
			}
			this.DomainAddress = domainLayout.DomainAddress;
			this.DomainCategory = domainLayout.DomainCategory;
			this.DomainName = domainLayout.DomainName;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001C291 File Offset: 0x0001A491
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsDefaultCollection = false, IsRequired = false)]
		public string DomainName
		{
			get
			{
				return base["name"] as string;
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001C2A3 File Offset: 0x0001A4A3
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001C2B5 File Offset: 0x0001A4B5
		[ConfigurationProperty("category", IsDefaultCollection = false, IsRequired = true)]
		public string DomainCategory
		{
			get
			{
				return base["category"] as string;
			}
			set
			{
				base["category"] = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C2C3 File Offset: 0x0001A4C3
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001C2D5 File Offset: 0x0001A4D5
		[ConfigurationProperty("address", IsDefaultCollection = false, IsRequired = true)]
		public Uri DomainAddress
		{
			get
			{
				return base["address"] as Uri;
			}
			set
			{
				if (null == value)
				{
					throw new ArgumentNullException("value");
				}
				base["address"] = value;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		protected DomainLayoutConfiguration(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.DomainName = info.GetString("name");
			this.DomainAddress = (Uri)info.GetValue("address", typeof(Uri));
			this.DomainCategory = info.GetString("category");
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001C35C File Offset: 0x0001A55C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("name", this.DomainName);
			info.AddValue("address", this.DomainAddress);
			info.AddValue("category", this.DomainCategory);
		}

		// Token: 0x0400063E RID: 1598
		internal const string DomainAddressProperty = "address";

		// Token: 0x0400063F RID: 1599
		internal const string DomainNameProperty = "name";

		// Token: 0x04000640 RID: 1600
		internal const string DomainCategoryProperty = "category";
	}
}
