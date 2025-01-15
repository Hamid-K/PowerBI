using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF1 RID: 7153
	public struct lookupInput_indexInputString : IProgramNodeBuilder, IEquatable<lookupInput_indexInputString>
	{
		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x0600F06B RID: 61547 RVA: 0x0033E1C2 File Offset: 0x0033C3C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F06C RID: 61548 RVA: 0x0033E1CA File Offset: 0x0033C3CA
		private lookupInput_indexInputString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F06D RID: 61549 RVA: 0x0033E1D3 File Offset: 0x0033C3D3
		public static lookupInput_indexInputString CreateUnsafe(ProgramNode node)
		{
			return new lookupInput_indexInputString(node);
		}

		// Token: 0x0600F06E RID: 61550 RVA: 0x0033E1DC File Offset: 0x0033C3DC
		public static lookupInput_indexInputString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.lookupInput_indexInputString)
			{
				return null;
			}
			return new lookupInput_indexInputString?(lookupInput_indexInputString.CreateUnsafe(node));
		}

		// Token: 0x0600F06F RID: 61551 RVA: 0x0033E211 File Offset: 0x0033C411
		public lookupInput_indexInputString(GrammarBuilders g, indexInputString value0)
		{
			this._node = g.UnnamedConversion.lookupInput_indexInputString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F070 RID: 61552 RVA: 0x0033E230 File Offset: 0x0033C430
		public static implicit operator lookupInput(lookupInput_indexInputString arg)
		{
			return lookupInput.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x0600F071 RID: 61553 RVA: 0x0033E23E File Offset: 0x0033C43E
		public indexInputString indexInputString
		{
			get
			{
				return indexInputString.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F072 RID: 61554 RVA: 0x0033E252 File Offset: 0x0033C452
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F073 RID: 61555 RVA: 0x0033E268 File Offset: 0x0033C468
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F074 RID: 61556 RVA: 0x0033E292 File Offset: 0x0033C492
		public bool Equals(lookupInput_indexInputString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE0 RID: 23264
		private ProgramNode _node;
	}
}
