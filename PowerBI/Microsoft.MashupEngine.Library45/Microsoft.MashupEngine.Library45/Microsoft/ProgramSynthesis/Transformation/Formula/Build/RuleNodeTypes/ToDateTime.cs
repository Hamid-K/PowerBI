using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154C RID: 5452
	public struct ToDateTime : IProgramNodeBuilder, IEquatable<ToDateTime>
	{
		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x0600B1F1 RID: 45553 RVA: 0x0027134E File Offset: 0x0026F54E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1F2 RID: 45554 RVA: 0x00271356 File Offset: 0x0026F556
		private ToDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1F3 RID: 45555 RVA: 0x0027135F File Offset: 0x0026F55F
		public static ToDateTime CreateUnsafe(ProgramNode node)
		{
			return new ToDateTime(node);
		}

		// Token: 0x0600B1F4 RID: 45556 RVA: 0x00271368 File Offset: 0x0026F568
		public static ToDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToDateTime)
			{
				return null;
			}
			return new ToDateTime?(ToDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B1F5 RID: 45557 RVA: 0x0027139D File Offset: 0x0026F59D
		public ToDateTime(GrammarBuilders g, outDate value0)
		{
			this._node = g.Rule.ToDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1F6 RID: 45558 RVA: 0x002713BC File Offset: 0x0026F5BC
		public static implicit operator output(ToDateTime arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x0600B1F7 RID: 45559 RVA: 0x002713CA File Offset: 0x0026F5CA
		public outDate outDate
		{
			get
			{
				return outDate.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1F8 RID: 45560 RVA: 0x002713DE File Offset: 0x0026F5DE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1F9 RID: 45561 RVA: 0x002713F4 File Offset: 0x0026F5F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1FA RID: 45562 RVA: 0x0027141E File Offset: 0x0026F61E
		public bool Equals(ToDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FA RID: 17914
		private ProgramNode _node;
	}
}
