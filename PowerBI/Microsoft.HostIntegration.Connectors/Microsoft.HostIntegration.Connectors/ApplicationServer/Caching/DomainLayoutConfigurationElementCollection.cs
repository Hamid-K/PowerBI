using System;
using System.Configuration;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	internal class DomainLayoutConfigurationElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x00016D51 File Offset: 0x00014F51
		public DomainLayoutConfigurationElementCollection()
		{
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		public void InitializeAfterSerialization()
		{
			if (this.collectionElements != null)
			{
				foreach (DomainLayoutConfiguration domainLayoutConfiguration in this.collectionElements)
				{
					this.Add(domainLayoutConfiguration);
				}
				this.collectionElements = null;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		protected DomainLayoutConfigurationElementCollection(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.collectionElements = info.GetValue("domain", typeof(DomainLayoutConfiguration[])) as DomainLayoutConfiguration[];
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C420 File Offset: 0x0001A620
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			DomainLayoutConfiguration[] array = new DomainLayoutConfiguration[base.Count];
			base.CopyTo(array, 0);
			info.AddValue("domain", array, array.GetType());
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001C461 File Offset: 0x0001A661
		protected override ConfigurationElement CreateNewElement()
		{
			return new DomainLayoutConfiguration();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C468 File Offset: 0x0001A668
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			DomainLayoutConfiguration domainLayoutConfiguration = element as DomainLayoutConfiguration;
			if (element == null)
			{
				throw new ArgumentException("element is not SingleDomainLayoutConfiguration");
			}
			return domainLayoutConfiguration.DomainAddress.ToString();
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C4A3 File Offset: 0x0001A6A3
		public void Add(DomainLayoutConfiguration domainConfiguration)
		{
			if (domainConfiguration == null)
			{
				throw new ArgumentNullException("domainConfiguration");
			}
			this.BaseAdd(domainConfiguration);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public bool Delete(string domainAddress)
		{
			if (string.IsNullOrEmpty(domainAddress))
			{
				throw new ArgumentNullException("domainAddress");
			}
			bool flag;
			try
			{
				base.BaseRemove(domainAddress);
				flag = true;
			}
			catch (Exception ex)
			{
				CacheEventHelper.WriteError("DomainLayoutConfigurationElementCollection", "Deletion of {0} failed with following exception {1}", new object[]
				{
					domainAddress,
					ex.ToString()
				});
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001C524 File Offset: 0x0001A724
		internal void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x04000641 RID: 1601
		internal const string DomainInformationProperty = "domain";

		// Token: 0x04000642 RID: 1602
		private DomainLayoutConfiguration[] collectionElements;
	}
}
