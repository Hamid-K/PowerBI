using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A4 RID: 164
	[EditorBrowsable(1)]
	public sealed class UrlEntityCollection : ComplexPropertyCollection<UrlEntity>
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000191A9 File Offset: 0x000181A9
		internal UrlEntityCollection()
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000191B1 File Offset: 0x000181B1
		internal UrlEntityCollection(IEnumerable<UrlEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<UrlEntity>(base.InternalAdd));
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000191CE File Offset: 0x000181CE
		internal override UrlEntity CreateComplexProperty(string xmlElementName)
		{
			return new UrlEntity();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000191D5 File Offset: 0x000181D5
		internal override UrlEntity CreateDefaultComplexProperty()
		{
			return new UrlEntity();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000191DC File Offset: 0x000181DC
		internal override string GetCollectionItemXmlElementName(UrlEntity complexProperty)
		{
			return "Url";
		}
	}
}
