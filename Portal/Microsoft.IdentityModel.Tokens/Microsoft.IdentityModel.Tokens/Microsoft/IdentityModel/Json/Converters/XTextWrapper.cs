using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000FD RID: 253
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00033945 File Offset: 0x00031B45
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00033952 File Offset: 0x00031B52
		[NullableContext(1)]
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0003395B File Offset: 0x00031B5B
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00033968 File Offset: 0x00031B68
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

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0003397F File Offset: 0x00031B7F
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
