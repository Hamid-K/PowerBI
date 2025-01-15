using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001527 RID: 5415
	public struct concatCase_concat : IProgramNodeBuilder, IEquatable<concatCase_concat>
	{
		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x0600B07D RID: 45181 RVA: 0x0026F226 File Offset: 0x0026D426
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B07E RID: 45182 RVA: 0x0026F22E File Offset: 0x0026D42E
		private concatCase_concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B07F RID: 45183 RVA: 0x0026F237 File Offset: 0x0026D437
		public static concatCase_concat CreateUnsafe(ProgramNode node)
		{
			return new concatCase_concat(node);
		}

		// Token: 0x0600B080 RID: 45184 RVA: 0x0026F240 File Offset: 0x0026D440
		public static concatCase_concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatCase_concat)
			{
				return null;
			}
			return new concatCase_concat?(concatCase_concat.CreateUnsafe(node));
		}

		// Token: 0x0600B081 RID: 45185 RVA: 0x0026F275 File Offset: 0x0026D475
		public concatCase_concat(GrammarBuilders g, concat value0)
		{
			this._node = g.UnnamedConversion.concatCase_concat.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B082 RID: 45186 RVA: 0x0026F294 File Offset: 0x0026D494
		public static implicit operator concatCase(concatCase_concat arg)
		{
			return concatCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x0600B083 RID: 45187 RVA: 0x0026F2A2 File Offset: 0x0026D4A2
		public concat concat
		{
			get
			{
				return concat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B084 RID: 45188 RVA: 0x0026F2B6 File Offset: 0x0026D4B6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B085 RID: 45189 RVA: 0x0026F2CC File Offset: 0x0026D4CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B086 RID: 45190 RVA: 0x0026F2F6 File Offset: 0x0026D4F6
		public bool Equals(concatCase_concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D5 RID: 17877
		private ProgramNode _node;
	}
}
