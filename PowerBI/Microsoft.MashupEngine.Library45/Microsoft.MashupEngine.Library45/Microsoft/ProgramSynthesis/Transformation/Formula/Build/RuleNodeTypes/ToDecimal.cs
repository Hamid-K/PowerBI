using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154B RID: 5451
	public struct ToDecimal : IProgramNodeBuilder, IEquatable<ToDecimal>
	{
		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x0600B1E7 RID: 45543 RVA: 0x0027126A File Offset: 0x0026F46A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1E8 RID: 45544 RVA: 0x00271272 File Offset: 0x0026F472
		private ToDecimal(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1E9 RID: 45545 RVA: 0x0027127B File Offset: 0x0026F47B
		public static ToDecimal CreateUnsafe(ProgramNode node)
		{
			return new ToDecimal(node);
		}

		// Token: 0x0600B1EA RID: 45546 RVA: 0x00271284 File Offset: 0x0026F484
		public static ToDecimal? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToDecimal)
			{
				return null;
			}
			return new ToDecimal?(ToDecimal.CreateUnsafe(node));
		}

		// Token: 0x0600B1EB RID: 45547 RVA: 0x002712B9 File Offset: 0x0026F4B9
		public ToDecimal(GrammarBuilders g, outNumber value0)
		{
			this._node = g.Rule.ToDecimal.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1EC RID: 45548 RVA: 0x002712D8 File Offset: 0x0026F4D8
		public static implicit operator output(ToDecimal arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x0600B1ED RID: 45549 RVA: 0x002712E6 File Offset: 0x0026F4E6
		public outNumber outNumber
		{
			get
			{
				return outNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1EE RID: 45550 RVA: 0x002712FA File Offset: 0x0026F4FA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1EF RID: 45551 RVA: 0x00271310 File Offset: 0x0026F510
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x0027133A File Offset: 0x0026F53A
		public bool Equals(ToDecimal other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F9 RID: 17913
		private ProgramNode _node;
	}
}
