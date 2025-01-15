using System;
using System.Runtime.CompilerServices;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x0200018A RID: 394
	public sealed class Exec
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x0002A5E0 File Offset: 0x000287E0
		private Exec()
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0002A5E8 File Offset: 0x000287E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DvText ToTX(string str)
		{
			return new DvText(str);
		}
	}
}
