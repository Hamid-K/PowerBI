using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000031 RID: 49
	public class ItemId : ServiceId, IJsonSerializable
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00009D6D File Offset: 0x00008D6D
		internal ItemId()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009D75 File Offset: 0x00008D75
		public static implicit operator ItemId(string uniqueId)
		{
			return new ItemId(uniqueId);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009D7D File Offset: 0x00008D7D
		internal override string GetXmlElementName()
		{
			return "ItemId";
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009D84 File Offset: 0x00008D84
		public ItemId(string uniqueId)
			: base(uniqueId)
		{
		}
	}
}
