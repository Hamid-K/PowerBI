using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x02000102 RID: 258
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00033C2C File Offset: 0x00031E2C
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00033C39 File Offset: 0x00031E39
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00033C42 File Offset: 0x00031E42
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x00033C4F File Offset: 0x00031E4F
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value ?? string.Empty;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00033C66 File Offset: 0x00031E66
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00033C78 File Offset: 0x00031E78
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00033C8A File Offset: 0x00031E8A
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
