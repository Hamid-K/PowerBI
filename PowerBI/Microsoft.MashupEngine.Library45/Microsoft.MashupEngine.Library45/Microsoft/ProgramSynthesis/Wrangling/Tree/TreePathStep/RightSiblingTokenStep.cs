using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000FB RID: 251
	public class RightSiblingTokenStep : TreePathStep, IEquatable<RightSiblingTokenStep>
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public override double Score
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private RightSiblingTokenStep()
		{
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000130C0 File Offset: 0x000112C0
		public static RightSiblingTokenStep Instance { get; } = new RightSiblingTokenStep();

		// Token: 0x060005D6 RID: 1494 RVA: 0x00012DFF File Offset: 0x00010FFF
		public bool Equals(RightSiblingTokenStep other)
		{
			return other != null;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000130C7 File Offset: 0x000112C7
		internal override string Serialize()
		{
			return "[RightSiblingTokenStep]";
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000130D0 File Offset: 0x000112D0
		public override Node Find(Node node)
		{
			if (node.Parent == null)
			{
				return null;
			}
			int? num = node.Parent.Children.IndexOfByReference(node);
			if (num != null)
			{
				int? num2 = num;
				int num3 = node.Parent.Children.Length - 1;
				if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
				{
					Node node2 = node.Parent.Children.Skip(num.Value + 1).FirstOrDefault((Node n) => !n.IsIrrelevantNode());
					if (node2 == null)
					{
						return null;
					}
					while (node2.Children.Any<Node>())
					{
						node2 = node2.Children.First<Node>();
					}
					if (node2.StartPosition.Line != node.StartPosition.Line)
					{
						return null;
					}
					return node2;
				}
			}
			return null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000130C7 File Offset: 0x000112C7
		public override string ToString()
		{
			return "[RightSiblingTokenStep]";
		}

		// Token: 0x04000269 RID: 617
		internal const string StringRepresentation = "[RightSiblingTokenStep]";
	}
}
