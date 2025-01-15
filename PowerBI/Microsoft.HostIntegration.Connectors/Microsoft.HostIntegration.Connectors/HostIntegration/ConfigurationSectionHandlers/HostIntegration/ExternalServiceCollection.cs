using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005CA RID: 1482
	[ConfigurationCollection(typeof(ExternalService), AddItemName = "externalService", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ExternalServiceCollection : ConfigurationElementCollection
	{
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000AD6D4 File Offset: 0x000AB8D4
		protected override string ElementName
		{
			get
			{
				return "externalService";
			}
		}

		// Token: 0x17000B11 RID: 2833
		public ExternalService this[int index]
		{
			get
			{
				return (ExternalService)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000AD6E9 File Offset: 0x000AB8E9
		protected override ConfigurationElement CreateNewElement()
		{
			return new ExternalService();
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000AD6F0 File Offset: 0x000AB8F0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ExternalService).GetElementKey();
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddExternalService(ExternalService externalService)
		{
			base.BaseAdd(externalService, true);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000AD6FD File Offset: 0x000AB8FD
		public void RemoveExternalService(ExternalService externalService)
		{
			base.BaseRemove(externalService.GetElementKey());
		}
	}
}
