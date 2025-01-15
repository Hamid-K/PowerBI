using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A4D RID: 6733
	public struct x : IProgramNodeBuilder, IEquatable<x>
	{
		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x0600DDEE RID: 56814 RVA: 0x002F24EE File Offset: 0x002F06EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDEF RID: 56815 RVA: 0x002F24F6 File Offset: 0x002F06F6
		private x(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDF0 RID: 56816 RVA: 0x002F24FF File Offset: 0x002F06FF
		public static x CreateUnsafe(ProgramNode node)
		{
			return new x(node);
		}

		// Token: 0x0600DDF1 RID: 56817 RVA: 0x002F2508 File Offset: 0x002F0708
		public static x? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.x)
			{
				return null;
			}
			return new x?(x.CreateUnsafe(node));
		}

		// Token: 0x0600DDF2 RID: 56818 RVA: 0x002F2542 File Offset: 0x002F0742
		public static x CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new x(new Hole(g.Symbol.x, holeId));
		}

		// Token: 0x0600DDF3 RID: 56819 RVA: 0x002F255A File Offset: 0x002F075A
		public x(GrammarBuilders g)
		{
			this = new x(new VariableNode(g.Symbol.x));
		}

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x0600DDF4 RID: 56820 RVA: 0x002F2572 File Offset: 0x002F0772
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600DDF5 RID: 56821 RVA: 0x002F257F File Offset: 0x002F077F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDF6 RID: 56822 RVA: 0x002F2594 File Offset: 0x002F0794
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDF7 RID: 56823 RVA: 0x002F25BE File Offset: 0x002F07BE
		public bool Equals(x other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543E RID: 21566
		private ProgramNode _node;
	}
}
