using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001543 RID: 5443
	public struct substring_sliceTrim : IProgramNodeBuilder, IEquatable<substring_sliceTrim>
	{
		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x0600B195 RID: 45461 RVA: 0x00270B16 File Offset: 0x0026ED16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B196 RID: 45462 RVA: 0x00270B1E File Offset: 0x0026ED1E
		private substring_sliceTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B197 RID: 45463 RVA: 0x00270B27 File Offset: 0x0026ED27
		public static substring_sliceTrim CreateUnsafe(ProgramNode node)
		{
			return new substring_sliceTrim(node);
		}

		// Token: 0x0600B198 RID: 45464 RVA: 0x00270B30 File Offset: 0x0026ED30
		public static substring_sliceTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.substring_sliceTrim)
			{
				return null;
			}
			return new substring_sliceTrim?(substring_sliceTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B199 RID: 45465 RVA: 0x00270B65 File Offset: 0x0026ED65
		public substring_sliceTrim(GrammarBuilders g, sliceTrim value0)
		{
			this._node = g.UnnamedConversion.substring_sliceTrim.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B19A RID: 45466 RVA: 0x00270B84 File Offset: 0x0026ED84
		public static implicit operator substring(substring_sliceTrim arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x0600B19B RID: 45467 RVA: 0x00270B92 File Offset: 0x0026ED92
		public sliceTrim sliceTrim
		{
			get
			{
				return sliceTrim.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B19C RID: 45468 RVA: 0x00270BA6 File Offset: 0x0026EDA6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B19D RID: 45469 RVA: 0x00270BBC File Offset: 0x0026EDBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B19E RID: 45470 RVA: 0x00270BE6 File Offset: 0x0026EDE6
		public bool Equals(substring_sliceTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F1 RID: 17905
		private ProgramNode _node;
	}
}
