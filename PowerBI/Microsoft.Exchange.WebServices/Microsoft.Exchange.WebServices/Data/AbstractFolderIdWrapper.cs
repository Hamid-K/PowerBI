using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000288 RID: 648
	internal abstract class AbstractFolderIdWrapper : IJsonSerializable
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x0003ED48 File Offset: 0x0003DD48
		public virtual Folder GetFolder()
		{
			return null;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0003ED4B File Offset: 0x0003DD4B
		internal AbstractFolderIdWrapper()
		{
		}

		// Token: 0x060016F9 RID: 5881
		internal abstract void WriteToXml(EwsServiceXmlWriter writer);

		// Token: 0x060016FA RID: 5882 RVA: 0x0003ED53 File Offset: 0x0003DD53
		internal virtual void Validate(ExchangeVersion version)
		{
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0003ED55 File Offset: 0x0003DD55
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return this.InternalToJson(service);
		}

		// Token: 0x060016FC RID: 5884
		internal abstract object InternalToJson(ExchangeService service);
	}
}
