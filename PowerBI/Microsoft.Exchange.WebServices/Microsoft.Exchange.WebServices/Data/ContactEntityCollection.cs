using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000045 RID: 69
	[EditorBrowsable(1)]
	public sealed class ContactEntityCollection : ComplexPropertyCollection<ContactEntity>
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000C5AB File Offset: 0x0000B5AB
		internal ContactEntityCollection()
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C5B3 File Offset: 0x0000B5B3
		internal ContactEntityCollection(IEnumerable<ContactEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<ContactEntity>(base.InternalAdd));
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C5D0 File Offset: 0x0000B5D0
		internal override ContactEntity CreateComplexProperty(string xmlElementName)
		{
			return new ContactEntity();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C5D7 File Offset: 0x0000B5D7
		internal override ContactEntity CreateDefaultComplexProperty()
		{
			return new ContactEntity();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C5DE File Offset: 0x0000B5DE
		internal override string GetCollectionItemXmlElementName(ContactEntity complexProperty)
		{
			return "Contact";
		}
	}
}
