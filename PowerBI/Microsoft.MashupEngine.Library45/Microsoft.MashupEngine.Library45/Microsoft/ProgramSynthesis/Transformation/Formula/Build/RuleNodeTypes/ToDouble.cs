using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154A RID: 5450
	public struct ToDouble : IProgramNodeBuilder, IEquatable<ToDouble>
	{
		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x0600B1DD RID: 45533 RVA: 0x00271186 File Offset: 0x0026F386
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1DE RID: 45534 RVA: 0x0027118E File Offset: 0x0026F38E
		private ToDouble(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1DF RID: 45535 RVA: 0x00271197 File Offset: 0x0026F397
		public static ToDouble CreateUnsafe(ProgramNode node)
		{
			return new ToDouble(node);
		}

		// Token: 0x0600B1E0 RID: 45536 RVA: 0x002711A0 File Offset: 0x0026F3A0
		public static ToDouble? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToDouble)
			{
				return null;
			}
			return new ToDouble?(ToDouble.CreateUnsafe(node));
		}

		// Token: 0x0600B1E1 RID: 45537 RVA: 0x002711D5 File Offset: 0x0026F3D5
		public ToDouble(GrammarBuilders g, outNumber value0)
		{
			this._node = g.Rule.ToDouble.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x002711F4 File Offset: 0x0026F3F4
		public static implicit operator output(ToDouble arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x0600B1E3 RID: 45539 RVA: 0x00271202 File Offset: 0x0026F402
		public outNumber outNumber
		{
			get
			{
				return outNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1E4 RID: 45540 RVA: 0x00271216 File Offset: 0x0026F416
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1E5 RID: 45541 RVA: 0x0027122C File Offset: 0x0026F42C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1E6 RID: 45542 RVA: 0x00271256 File Offset: 0x0026F456
		public bool Equals(ToDouble other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F8 RID: 17912
		private ProgramNode _node;
	}
}
