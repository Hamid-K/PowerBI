using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FE RID: 254
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00033AF8 File Offset: 0x00031CF8
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00033B05 File Offset: 0x00031D05
		[NullableContext(1)]
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00033B0E File Offset: 0x00031D0E
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00033B1B File Offset: 0x00031D1B
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00033B32 File Offset: 0x00031D32
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
