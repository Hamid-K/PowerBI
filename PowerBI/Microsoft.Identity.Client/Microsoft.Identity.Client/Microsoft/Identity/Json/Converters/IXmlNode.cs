using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000F8 RID: 248
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CCC RID: 3276
		XmlNodeType NodeType { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CCD RID: 3277
		string LocalName { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CCE RID: 3278
		[Nullable(0)]
		List<IXmlNode> ChildNodes
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CCF RID: 3279
		[Nullable(0)]
		List<IXmlNode> Attributes
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CD0 RID: 3280
		IXmlNode ParentNode { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CD1 RID: 3281
		// (set) Token: 0x06000CD2 RID: 3282
		string Value { get; set; }

		// Token: 0x06000CD3 RID: 3283
		[NullableContext(0)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CD4 RID: 3284
		string NamespaceUri { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CD5 RID: 3285
		object WrappedNode { get; }
	}
}
