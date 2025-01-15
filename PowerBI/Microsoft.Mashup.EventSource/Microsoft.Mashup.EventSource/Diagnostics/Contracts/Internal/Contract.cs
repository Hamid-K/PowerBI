using System;
using System.Diagnostics;

namespace Microsoft.Diagnostics.Contracts.Internal
{
	// Token: 0x0200000B RID: 11
	internal class Contract
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000022DA File Offset: 0x000004DA
		public static void Assert(bool invariant)
		{
			Contract.Assert(invariant, string.Empty);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022E7 File Offset: 0x000004E7
		public static void Assert(bool invariant, string message)
		{
			if (!invariant)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
				throw new Exception("Assertion failed: " + message);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002309 File Offset: 0x00000509
		public static void EndContractBlock()
		{
		}
	}
}
