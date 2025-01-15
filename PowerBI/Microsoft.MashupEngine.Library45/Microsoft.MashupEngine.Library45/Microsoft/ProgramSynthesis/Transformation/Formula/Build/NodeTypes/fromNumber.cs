using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C5 RID: 5573
	public struct fromNumber : IProgramNodeBuilder, IEquatable<fromNumber>
	{
		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x0600B8C2 RID: 47298 RVA: 0x0028035A File Offset: 0x0027E55A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8C3 RID: 47299 RVA: 0x00280362 File Offset: 0x0027E562
		private fromNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8C4 RID: 47300 RVA: 0x0028036B File Offset: 0x0027E56B
		public static fromNumber CreateUnsafe(ProgramNode node)
		{
			return new fromNumber(node);
		}

		// Token: 0x0600B8C5 RID: 47301 RVA: 0x00280374 File Offset: 0x0027E574
		public static fromNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromNumber)
			{
				return null;
			}
			return new fromNumber?(fromNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B8C6 RID: 47302 RVA: 0x002803AE File Offset: 0x0027E5AE
		public static fromNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromNumber(new Hole(g.Symbol.fromNumber, holeId));
		}

		// Token: 0x0600B8C7 RID: 47303 RVA: 0x002803C6 File Offset: 0x0027E5C6
		public FromNumber Cast_FromNumber()
		{
			return FromNumber.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8C8 RID: 47304 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromNumber(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8C9 RID: 47305 RVA: 0x002803D3 File Offset: 0x0027E5D3
		public bool Is_FromNumber(GrammarBuilders g, out FromNumber value)
		{
			value = FromNumber.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8CA RID: 47306 RVA: 0x002803E7 File Offset: 0x0027E5E7
		public FromNumber? As_FromNumber(GrammarBuilders g)
		{
			return new FromNumber?(FromNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8CB RID: 47307 RVA: 0x002803F9 File Offset: 0x0027E5F9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8CC RID: 47308 RVA: 0x0028040C File Offset: 0x0027E60C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8CD RID: 47309 RVA: 0x00280436 File Offset: 0x0027E636
		public bool Equals(fromNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004673 RID: 18035
		private ProgramNode _node;
	}
}
