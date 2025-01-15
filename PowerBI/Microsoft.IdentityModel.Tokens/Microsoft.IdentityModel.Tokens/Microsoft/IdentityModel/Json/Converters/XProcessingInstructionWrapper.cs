using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000FF RID: 255
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x000339FB File Offset: 0x00031BFB
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00033A08 File Offset: 0x00031C08
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00033A11 File Offset: 0x00031C11
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00033A1E File Offset: 0x00031C1E
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00033A2B File Offset: 0x00031C2B
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
