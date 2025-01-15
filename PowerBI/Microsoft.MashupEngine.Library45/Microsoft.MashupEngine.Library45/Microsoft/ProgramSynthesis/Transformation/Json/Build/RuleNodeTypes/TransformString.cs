using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A37 RID: 6711
	public struct TransformString : IProgramNodeBuilder, IEquatable<TransformString>
	{
		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x0600DC9E RID: 56478 RVA: 0x002EF30A File Offset: 0x002ED50A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC9F RID: 56479 RVA: 0x002EF312 File Offset: 0x002ED512
		private TransformString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCA0 RID: 56480 RVA: 0x002EF31B File Offset: 0x002ED51B
		public static TransformString CreateUnsafe(ProgramNode node)
		{
			return new TransformString(node);
		}

		// Token: 0x0600DCA1 RID: 56481 RVA: 0x002EF324 File Offset: 0x002ED524
		public static TransformString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TransformString)
			{
				return null;
			}
			return new TransformString?(TransformString.CreateUnsafe(node));
		}

		// Token: 0x0600DCA2 RID: 56482 RVA: 0x002EF359 File Offset: 0x002ED559
		public TransformString(GrammarBuilders g, @switch value0)
		{
			this._node = g.Rule.TransformString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DCA3 RID: 56483 RVA: 0x002EF378 File Offset: 0x002ED578
		public static implicit operator transformString(TransformString arg)
		{
			return transformString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x0600DCA4 RID: 56484 RVA: 0x002EF386 File Offset: 0x002ED586
		public @switch @switch
		{
			get
			{
				return @switch.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DCA5 RID: 56485 RVA: 0x002EF39A File Offset: 0x002ED59A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DCA6 RID: 56486 RVA: 0x002EF3B0 File Offset: 0x002ED5B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DCA7 RID: 56487 RVA: 0x002EF3DA File Offset: 0x002ED5DA
		public bool Equals(TransformString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005428 RID: 21544
		private ProgramNode _node;
	}
}
