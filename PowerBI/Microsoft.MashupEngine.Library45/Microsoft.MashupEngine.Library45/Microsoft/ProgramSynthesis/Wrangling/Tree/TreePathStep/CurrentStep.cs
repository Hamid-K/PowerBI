using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000F7 RID: 247
	public class CurrentStep : TreePathStep, IEquatable<CurrentStep>
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public override double Score
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private CurrentStep()
		{
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00012DF8 File Offset: 0x00010FF8
		public static CurrentStep Instance { get; } = new CurrentStep();

		// Token: 0x060005B3 RID: 1459 RVA: 0x00012DFF File Offset: 0x00010FFF
		public bool Equals(CurrentStep other)
		{
			return other != null;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00012E07 File Offset: 0x00011007
		internal override string Serialize()
		{
			return "[CurrentStep]";
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000E945 File Offset: 0x0000CB45
		public override Node Find(Node node)
		{
			return node;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00012E07 File Offset: 0x00011007
		public override string ToString()
		{
			return "[CurrentStep]";
		}

		// Token: 0x04000262 RID: 610
		internal const string StringRepresentation = "[CurrentStep]";
	}
}
