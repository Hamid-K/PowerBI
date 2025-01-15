using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000223 RID: 547
	internal sealed class DMOptex
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x00039A48 File Offset: 0x00037C48
		public bool Enter(int key)
		{
			return this._optex.Enter(key);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00039A56 File Offset: 0x00037C56
		public void Exit(int key)
		{
			this._optex.Exit(key);
		}

		// Token: 0x04000B20 RID: 2848
		private DMOptexStruct _optex;
	}
}
