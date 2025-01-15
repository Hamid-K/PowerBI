using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000289 RID: 649
	internal abstract class AbstractItemIdWrapper : IJsonSerializable
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x0003ED5E File Offset: 0x0003DD5E
		internal AbstractItemIdWrapper()
		{
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0003ED66 File Offset: 0x0003DD66
		public virtual Item GetItem()
		{
			return null;
		}

		// Token: 0x060016FF RID: 5887
		internal abstract void WriteToXml(EwsServiceXmlWriter writer);

		// Token: 0x06001700 RID: 5888 RVA: 0x0003ED69 File Offset: 0x0003DD69
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return this.IternalToJson(service);
		}

		// Token: 0x06001701 RID: 5889
		internal abstract object IternalToJson(ExchangeService service);
	}
}
