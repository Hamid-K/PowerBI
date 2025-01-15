using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B8 RID: 184
	internal sealed class RestartTokenGroupingValues
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00007BC6 File Offset: 0x00005DC6
		internal RestartTokenGroupingValues(RestartToken groupToken, RestartToken subtotalToken)
		{
			this.GroupToken = groupToken;
			this.SubtotalToken = subtotalToken;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00007BDC File Offset: 0x00005DDC
		public RestartToken GroupToken { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public RestartToken SubtotalToken { get; }
	}
}
