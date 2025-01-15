using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F9 RID: 249
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CDC RID: 3292
		XmlNodeType NodeType { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CDD RID: 3293
		string LocalName { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CDE RID: 3294
		[Nullable(1)]
		List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CDF RID: 3295
		[Nullable(1)]
		List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CE0 RID: 3296
		IXmlNode ParentNode { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CE1 RID: 3297
		// (set) Token: 0x06000CE2 RID: 3298
		string Value { get; set; }

		// Token: 0x06000CE3 RID: 3299
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CE4 RID: 3300
		string NamespaceUri { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CE5 RID: 3301
		object WrappedNode { get; }
	}
}
