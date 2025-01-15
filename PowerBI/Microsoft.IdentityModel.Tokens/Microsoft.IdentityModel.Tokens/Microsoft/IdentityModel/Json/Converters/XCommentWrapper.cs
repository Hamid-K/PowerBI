using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000FE RID: 254
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000339A0 File Offset: 0x00031BA0
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000339AD File Offset: 0x00031BAD
		[NullableContext(1)]
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x000339B6 File Offset: 0x00031BB6
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x000339C3 File Offset: 0x00031BC3
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value ?? string.Empty;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000339DA File Offset: 0x00031BDA
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
