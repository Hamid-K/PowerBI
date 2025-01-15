using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B7 RID: 183
	internal sealed class RestartTokenGroupingBinding
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00007B86 File Offset: 0x00005D86
		internal RestartTokenGroupingBinding(SubtotalType subtotalPosition)
			: this(null, null, subtotalPosition)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00007B91 File Offset: 0x00005D91
		internal RestartTokenGroupingBinding(string memberId, string subtotalMemberId, SubtotalType subtotalPosition)
		{
			this.MemberId = memberId;
			this.SubtotalMemberId = subtotalMemberId;
			this.SubtotalPosition = subtotalPosition;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00007BAE File Offset: 0x00005DAE
		public string MemberId { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00007BB6 File Offset: 0x00005DB6
		public string SubtotalMemberId { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00007BBE File Offset: 0x00005DBE
		public SubtotalType SubtotalPosition { get; }
	}
}
