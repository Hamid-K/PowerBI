using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F0 RID: 240
	internal sealed class TopLimitOperator : LimitOperator
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x00023BEF File Offset: 0x00021DEF
		internal TopLimitOperator(int count, long? skip = null)
			: base(count)
		{
			this.Skip = skip;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00023BFF File Offset: 0x00021DFF
		public long? Skip { get; }
	}
}
