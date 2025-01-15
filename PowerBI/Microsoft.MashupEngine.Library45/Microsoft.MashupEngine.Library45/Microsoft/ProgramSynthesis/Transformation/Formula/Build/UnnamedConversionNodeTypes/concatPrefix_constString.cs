using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152A RID: 5418
	public struct concatPrefix_constString : IProgramNodeBuilder, IEquatable<concatPrefix_constString>
	{
		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x0600B09B RID: 45211 RVA: 0x0026F4D2 File Offset: 0x0026D6D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B09C RID: 45212 RVA: 0x0026F4DA File Offset: 0x0026D6DA
		private concatPrefix_constString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B09D RID: 45213 RVA: 0x0026F4E3 File Offset: 0x0026D6E3
		public static concatPrefix_constString CreateUnsafe(ProgramNode node)
		{
			return new concatPrefix_constString(node);
		}

		// Token: 0x0600B09E RID: 45214 RVA: 0x0026F4EC File Offset: 0x0026D6EC
		public static concatPrefix_constString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatPrefix_constString)
			{
				return null;
			}
			return new concatPrefix_constString?(concatPrefix_constString.CreateUnsafe(node));
		}

		// Token: 0x0600B09F RID: 45215 RVA: 0x0026F521 File Offset: 0x0026D721
		public concatPrefix_constString(GrammarBuilders g, constString value0)
		{
			this._node = g.UnnamedConversion.concatPrefix_constString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0A0 RID: 45216 RVA: 0x0026F540 File Offset: 0x0026D740
		public static implicit operator concatPrefix(concatPrefix_constString arg)
		{
			return concatPrefix.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x0600B0A1 RID: 45217 RVA: 0x0026F54E File Offset: 0x0026D74E
		public constString constString
		{
			get
			{
				return constString.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0A2 RID: 45218 RVA: 0x0026F562 File Offset: 0x0026D762
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0A3 RID: 45219 RVA: 0x0026F578 File Offset: 0x0026D778
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0A4 RID: 45220 RVA: 0x0026F5A2 File Offset: 0x0026D7A2
		public bool Equals(concatPrefix_constString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D8 RID: 17880
		private ProgramNode _node;
	}
}
