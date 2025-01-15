using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FD RID: 253
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x000331E3 File Offset: 0x000313E3
		[Nullable(0)]
		private XComment Text
		{
			[NullableContext(0)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000331F0 File Offset: 0x000313F0
		[NullableContext(0)]
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000331F9 File Offset: 0x000313F9
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00033206 File Offset: 0x00031406
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00033214 File Offset: 0x00031414
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
