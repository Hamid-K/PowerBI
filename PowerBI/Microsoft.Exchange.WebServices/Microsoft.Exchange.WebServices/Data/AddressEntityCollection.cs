using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002F RID: 47
	[EditorBrowsable(1)]
	public sealed class AddressEntityCollection : ComplexPropertyCollection<AddressEntity>
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00009AEB File Offset: 0x00008AEB
		internal AddressEntityCollection()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009AF3 File Offset: 0x00008AF3
		internal AddressEntityCollection(IEnumerable<AddressEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<AddressEntity>(base.InternalAdd));
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009B10 File Offset: 0x00008B10
		internal override AddressEntity CreateComplexProperty(string xmlElementName)
		{
			return new AddressEntity();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009B17 File Offset: 0x00008B17
		internal override AddressEntity CreateDefaultComplexProperty()
		{
			return new AddressEntity();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009B1E File Offset: 0x00008B1E
		internal override string GetCollectionItemXmlElementName(AddressEntity complexProperty)
		{
			return "Address";
		}
	}
}
