using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000081 RID: 129
	[EditorBrowsable(1)]
	public sealed class PhoneEntityCollection : ComplexPropertyCollection<PhoneEntity>
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00014241 File Offset: 0x00013241
		internal PhoneEntityCollection()
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00014249 File Offset: 0x00013249
		internal PhoneEntityCollection(IEnumerable<PhoneEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<PhoneEntity>(base.InternalAdd));
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014266 File Offset: 0x00013266
		internal override PhoneEntity CreateComplexProperty(string xmlElementName)
		{
			return new PhoneEntity();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001426D File Offset: 0x0001326D
		internal override PhoneEntity CreateDefaultComplexProperty()
		{
			return new PhoneEntity();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00014274 File Offset: 0x00013274
		internal override string GetCollectionItemXmlElementName(PhoneEntity complexProperty)
		{
			return "Phone";
		}
	}
}
