using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000047 RID: 71
	[EditorBrowsable(1)]
	public sealed class ContactPhoneEntityCollection : ComplexPropertyCollection<ContactPhoneEntity>
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000C691 File Offset: 0x0000B691
		internal ContactPhoneEntityCollection()
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C699 File Offset: 0x0000B699
		internal ContactPhoneEntityCollection(IEnumerable<ContactPhoneEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<ContactPhoneEntity>(base.InternalAdd));
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C6B6 File Offset: 0x0000B6B6
		internal override ContactPhoneEntity CreateComplexProperty(string xmlElementName)
		{
			return new ContactPhoneEntity();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C6BD File Offset: 0x0000B6BD
		internal override ContactPhoneEntity CreateDefaultComplexProperty()
		{
			return new ContactPhoneEntity();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C6C4 File Offset: 0x0000B6C4
		internal override string GetCollectionItemXmlElementName(ContactPhoneEntity complexProperty)
		{
			return "Phone";
		}
	}
}
