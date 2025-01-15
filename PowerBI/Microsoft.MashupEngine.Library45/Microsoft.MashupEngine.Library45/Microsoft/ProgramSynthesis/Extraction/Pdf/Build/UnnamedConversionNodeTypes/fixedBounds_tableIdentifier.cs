using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000BF4 RID: 3060
	public struct fixedBounds_tableIdentifier : IProgramNodeBuilder, IEquatable<fixedBounds_tableIdentifier>
	{
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x000F8DB2 File Offset: 0x000F6FB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x000F8DBA File Offset: 0x000F6FBA
		private fixedBounds_tableIdentifier(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x000F8DC3 File Offset: 0x000F6FC3
		public static fixedBounds_tableIdentifier CreateUnsafe(ProgramNode node)
		{
			return new fixedBounds_tableIdentifier(node);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x000F8DCC File Offset: 0x000F6FCC
		public static fixedBounds_tableIdentifier? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.fixedBounds_tableIdentifier)
			{
				return null;
			}
			return new fixedBounds_tableIdentifier?(fixedBounds_tableIdentifier.CreateUnsafe(node));
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x000F8E01 File Offset: 0x000F7001
		public fixedBounds_tableIdentifier(GrammarBuilders g, tableIdentifier value0)
		{
			this._node = g.UnnamedConversion.fixedBounds_tableIdentifier.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x000F8E20 File Offset: 0x000F7020
		public static implicit operator fixedBounds(fixedBounds_tableIdentifier arg)
		{
			return fixedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x000F8E2E File Offset: 0x000F702E
		public tableIdentifier tableIdentifier
		{
			get
			{
				return tableIdentifier.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x000F8E42 File Offset: 0x000F7042
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x000F8E58 File Offset: 0x000F7058
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x000F8E82 File Offset: 0x000F7082
		public bool Equals(fixedBounds_tableIdentifier other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231C RID: 8988
		private ProgramNode _node;
	}
}
