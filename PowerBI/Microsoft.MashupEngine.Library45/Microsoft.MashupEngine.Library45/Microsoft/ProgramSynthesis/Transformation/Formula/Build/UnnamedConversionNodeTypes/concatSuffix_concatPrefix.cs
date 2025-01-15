using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152D RID: 5421
	public struct concatSuffix_concatPrefix : IProgramNodeBuilder, IEquatable<concatSuffix_concatPrefix>
	{
		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x0600B0B9 RID: 45241 RVA: 0x0026F77E File Offset: 0x0026D97E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0BA RID: 45242 RVA: 0x0026F786 File Offset: 0x0026D986
		private concatSuffix_concatPrefix(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0BB RID: 45243 RVA: 0x0026F78F File Offset: 0x0026D98F
		public static concatSuffix_concatPrefix CreateUnsafe(ProgramNode node)
		{
			return new concatSuffix_concatPrefix(node);
		}

		// Token: 0x0600B0BC RID: 45244 RVA: 0x0026F798 File Offset: 0x0026D998
		public static concatSuffix_concatPrefix? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatSuffix_concatPrefix)
			{
				return null;
			}
			return new concatSuffix_concatPrefix?(concatSuffix_concatPrefix.CreateUnsafe(node));
		}

		// Token: 0x0600B0BD RID: 45245 RVA: 0x0026F7CD File Offset: 0x0026D9CD
		public concatSuffix_concatPrefix(GrammarBuilders g, concatPrefix value0)
		{
			this._node = g.UnnamedConversion.concatSuffix_concatPrefix.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0BE RID: 45246 RVA: 0x0026F7EC File Offset: 0x0026D9EC
		public static implicit operator concatSuffix(concatSuffix_concatPrefix arg)
		{
			return concatSuffix.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x0600B0BF RID: 45247 RVA: 0x0026F7FA File Offset: 0x0026D9FA
		public concatPrefix concatPrefix
		{
			get
			{
				return concatPrefix.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0C0 RID: 45248 RVA: 0x0026F80E File Offset: 0x0026DA0E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0C1 RID: 45249 RVA: 0x0026F824 File Offset: 0x0026DA24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0C2 RID: 45250 RVA: 0x0026F84E File Offset: 0x0026DA4E
		public bool Equals(concatSuffix_concatPrefix other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DB RID: 17883
		private ProgramNode _node;
	}
}
