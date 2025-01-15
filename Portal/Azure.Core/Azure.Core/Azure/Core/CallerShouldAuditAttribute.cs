using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200006F RID: 111
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	internal class CallerShouldAuditAttribute : Attribute
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000ADF7 File Offset: 0x00008FF7
		public CallerShouldAuditAttribute(string reason)
		{
			this.Reason = reason;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000AE06 File Offset: 0x00009006
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000AE0E File Offset: 0x0000900E
		public string Reason { get; set; }
	}
}
