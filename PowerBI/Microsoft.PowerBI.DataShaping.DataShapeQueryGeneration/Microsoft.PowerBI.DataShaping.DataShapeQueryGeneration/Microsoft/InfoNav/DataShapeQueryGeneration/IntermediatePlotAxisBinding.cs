using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000034 RID: 52
	internal sealed class IntermediatePlotAxisBinding
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009977 File Offset: 0x00007B77
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000997F File Offset: 0x00007B7F
		internal int Index { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009988 File Offset: 0x00007B88
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009990 File Offset: 0x00007B90
		internal DataReductionPlotAxisTransform Transform { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009999 File Offset: 0x00007B99
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000099A1 File Offset: 0x00007BA1
		internal string Applied { get; set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x000099AC File Offset: 0x00007BAC
		public static bool AreEquivalent(IntermediatePlotAxisBinding first, IntermediatePlotAxisBinding second)
		{
			bool? flag = Util.AreEqual<IntermediatePlotAxisBinding>(first, second);
			if (flag == null)
			{
				return first != null && second != null && first.Index == second.Index && first.Transform == second.Transform && first.Applied == second.Applied;
			}
			return flag.GetValueOrDefault();
		}
	}
}
