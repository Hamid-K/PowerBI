using System;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000032 RID: 50
	internal class DaxQueryResultObjectWriterBase : DaxQueryResultWriterBase
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004243 File Offset: 0x00002443
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000424B File Offset: 0x0000244B
		public bool HasBegun { get; private set; }

		// Token: 0x06000127 RID: 295 RVA: 0x00004254 File Offset: 0x00002454
		internal override void Begin()
		{
			this.HasBegun = true;
			base.Writer.BeginObject();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004268 File Offset: 0x00002468
		internal override void End()
		{
			base.Writer.EndObject();
		}
	}
}
