using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000190 RID: 400
	[DomName("NamedNodeMap")]
	public interface INamedNodeMap : IEnumerable<IAttr>, IEnumerable
	{
		// Token: 0x170002CC RID: 716
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		IAttr this[int index] { get; }

		// Token: 0x170002CD RID: 717
		[DomAccessor(Accessors.Getter)]
		IAttr this[string name] { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000E57 RID: 3671
		[DomName("length")]
		int Length { get; }

		// Token: 0x06000E58 RID: 3672
		[DomName("getNamedItem")]
		IAttr GetNamedItem(string name);

		// Token: 0x06000E59 RID: 3673
		[DomName("setNamedItem")]
		IAttr SetNamedItem(IAttr item);

		// Token: 0x06000E5A RID: 3674
		[DomName("removeNamedItem")]
		IAttr RemoveNamedItem(string name);

		// Token: 0x06000E5B RID: 3675
		[DomName("getNamedItemNS")]
		IAttr GetNamedItem(string namespaceUri, string localName);

		// Token: 0x06000E5C RID: 3676
		[DomName("setNamedItemNS")]
		IAttr SetNamedItemWithNamespaceUri(IAttr item);

		// Token: 0x06000E5D RID: 3677
		[DomName("removeNamedItemNS")]
		IAttr RemoveNamedItem(string namespaceUri, string localName);
	}
}
