using System;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Learning
{
	// Token: 0x02001EB4 RID: 7860
	public class NodeExample : TTreeExample<Node, Node>
	{
		// Token: 0x06010974 RID: 67956 RVA: 0x0039084A File Offset: 0x0038EA4A
		public NodeExample(Node input, Node output, bool isPositive = true)
			: base(input, output, isPositive)
		{
		}
	}
}
