using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000FF RID: 255
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00033B53 File Offset: 0x00031D53
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00033B60 File Offset: 0x00031D60
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00033B69 File Offset: 0x00031D69
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00033B76 File Offset: 0x00031D76
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x00033B83 File Offset: 0x00031D83
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value ?? string.Empty;
			}
		}
	}
}
