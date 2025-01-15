using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E15 RID: 3605
	public struct aboveOrOutput_titleOf : IProgramNodeBuilder, IEquatable<aboveOrOutput_titleOf>
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06006012 RID: 24594 RVA: 0x0013D2A6 File Offset: 0x0013B4A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x0013D2AE File Offset: 0x0013B4AE
		private aboveOrOutput_titleOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0013D2B7 File Offset: 0x0013B4B7
		public static aboveOrOutput_titleOf CreateUnsafe(ProgramNode node)
		{
			return new aboveOrOutput_titleOf(node);
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0013D2C0 File Offset: 0x0013B4C0
		public static aboveOrOutput_titleOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.aboveOrOutput_titleOf)
			{
				return null;
			}
			return new aboveOrOutput_titleOf?(aboveOrOutput_titleOf.CreateUnsafe(node));
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x0013D2F5 File Offset: 0x0013B4F5
		public aboveOrOutput_titleOf(GrammarBuilders g, titleOf value0)
		{
			this._node = g.UnnamedConversion.aboveOrOutput_titleOf.BuildASTNode(value0.Node);
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x0013D314 File Offset: 0x0013B514
		public static implicit operator aboveOrOutput(aboveOrOutput_titleOf arg)
		{
			return aboveOrOutput.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x0013D322 File Offset: 0x0013B522
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x0013D336 File Offset: 0x0013B536
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x0013D34C File Offset: 0x0013B54C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x0013D376 File Offset: 0x0013B576
		public bool Equals(aboveOrOutput_titleOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BBF RID: 11199
		private ProgramNode _node;
	}
}
