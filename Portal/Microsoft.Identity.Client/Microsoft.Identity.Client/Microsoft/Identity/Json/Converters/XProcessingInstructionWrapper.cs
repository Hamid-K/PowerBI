using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FE RID: 254
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00033235 File Offset: 0x00031435
		[Nullable(0)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(0)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00033242 File Offset: 0x00031442
		[NullableContext(0)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0003324B File Offset: 0x0003144B
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00033258 File Offset: 0x00031458
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00033265 File Offset: 0x00031465
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
