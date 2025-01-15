using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FC RID: 252
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00033191 File Offset: 0x00031391
		[Nullable(0)]
		private XText Text
		{
			[NullableContext(0)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003319E File Offset: 0x0003139E
		[NullableContext(0)]
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000331A7 File Offset: 0x000313A7
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x000331B4 File Offset: 0x000313B4
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000331C2 File Offset: 0x000313C2
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
