using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006E RID: 110
	[EditorBrowsable(1)]
	public sealed class ItemIdCollection : ComplexPropertyCollection<ItemId>
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0001211C File Offset: 0x0001111C
		internal ItemIdCollection()
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00012124 File Offset: 0x00011124
		internal override ItemId CreateComplexProperty(string xmlElementName)
		{
			return new ItemId();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001212B File Offset: 0x0001112B
		internal override ItemId CreateDefaultComplexProperty()
		{
			return new ItemId();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00012132 File Offset: 0x00011132
		internal override string GetCollectionItemXmlElementName(ItemId complexProperty)
		{
			return complexProperty.GetXmlElementName();
		}
	}
}
