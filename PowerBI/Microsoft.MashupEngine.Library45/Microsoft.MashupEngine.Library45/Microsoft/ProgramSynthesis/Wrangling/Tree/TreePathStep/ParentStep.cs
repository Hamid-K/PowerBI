using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000FA RID: 250
	public class ParentStep : TreePathStep, IEquatable<ParentStep>
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public override double Score
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private ParentStep()
		{
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001309E File Offset: 0x0001129E
		public static ParentStep Instance { get; } = new ParentStep();

		// Token: 0x060005CE RID: 1486 RVA: 0x00012DFF File Offset: 0x00010FFF
		public bool Equals(ParentStep other)
		{
			return other != null;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000130A5 File Offset: 0x000112A5
		internal override string Serialize()
		{
			return "[ParentStep]";
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000130AC File Offset: 0x000112AC
		public override Node Find(Node node)
		{
			return node.Parent;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000130A5 File Offset: 0x000112A5
		public override string ToString()
		{
			return "[ParentStep]";
		}

		// Token: 0x04000267 RID: 615
		internal const string StringRepresentation = "[ParentStep]";
	}
}
