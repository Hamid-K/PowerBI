using System;
using System.IO;

namespace System.Net.Http.Internal
{
	// Token: 0x02000030 RID: 48
	internal class NonClosingDelegatingStream : DelegatingStream
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000679F File Offset: 0x0000499F
		public NonClosingDelegatingStream(Stream innerStream)
			: base(innerStream)
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000353A File Offset: 0x0000173A
		public override void Close()
		{
		}
	}
}
