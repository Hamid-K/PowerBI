using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200017B RID: 379
	[DomName("Attr")]
	public interface IAttr : IEquatable<IAttr>
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DA7 RID: 3495
		[DomName("localName")]
		string LocalName { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DA8 RID: 3496
		[DomName("name")]
		string Name { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DA9 RID: 3497
		// (set) Token: 0x06000DAA RID: 3498
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DAB RID: 3499
		[DomName("namespaceURI")]
		string NamespaceUri { get; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DAC RID: 3500
		[DomName("prefix")]
		string Prefix { get; }
	}
}
