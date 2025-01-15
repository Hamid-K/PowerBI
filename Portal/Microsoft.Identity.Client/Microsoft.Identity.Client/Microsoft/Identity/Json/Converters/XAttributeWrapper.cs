using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x02000101 RID: 257
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00033460 File Offset: 0x00031660
		[Nullable(0)]
		private XAttribute Attribute
		{
			[NullableContext(0)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003346D File Offset: 0x0003166D
		[NullableContext(0)]
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00033476 File Offset: 0x00031676
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x00033483 File Offset: 0x00031683
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00033491 File Offset: 0x00031691
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000334A3 File Offset: 0x000316A3
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x000334B5 File Offset: 0x000316B5
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
