using System;
using dotless.Core.Parser;

namespace dotless.Core.Exceptions
{
	// Token: 0x020000BD RID: 189
	public class ParserException : Exception
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00017EE8 File Offset: 0x000160E8
		public ParserException(string message)
			: base(message)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00017EF1 File Offset: 0x000160F1
		public ParserException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00017EFB File Offset: 0x000160FB
		public ParserException(string message, Exception innerException, Zone errorLocation)
			: base(message, innerException)
		{
			this.ErrorLocation = errorLocation;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00017F0C File Offset: 0x0001610C
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00017F14 File Offset: 0x00016114
		public Zone ErrorLocation { get; set; }
	}
}
