using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000056 RID: 86
	[EditorBrowsable(1)]
	public sealed class EmailAddressEntityCollection : ComplexPropertyCollection<EmailAddressEntity>
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000DFC1 File Offset: 0x0000CFC1
		internal EmailAddressEntityCollection()
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000DFC9 File Offset: 0x0000CFC9
		internal EmailAddressEntityCollection(IEnumerable<EmailAddressEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<EmailAddressEntity>(base.InternalAdd));
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000DFE6 File Offset: 0x0000CFE6
		internal override EmailAddressEntity CreateComplexProperty(string xmlElementName)
		{
			return new EmailAddressEntity();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000DFED File Offset: 0x0000CFED
		internal override EmailAddressEntity CreateDefaultComplexProperty()
		{
			return new EmailAddressEntity();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000DFF4 File Offset: 0x0000CFF4
		internal override string GetCollectionItemXmlElementName(EmailAddressEntity complexProperty)
		{
			return "EmailAddress";
		}
	}
}
