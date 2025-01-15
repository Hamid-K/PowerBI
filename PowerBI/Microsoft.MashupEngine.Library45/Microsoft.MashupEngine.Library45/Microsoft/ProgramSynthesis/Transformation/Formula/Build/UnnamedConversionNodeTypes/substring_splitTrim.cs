using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001542 RID: 5442
	public struct substring_splitTrim : IProgramNodeBuilder, IEquatable<substring_splitTrim>
	{
		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x0600B18B RID: 45451 RVA: 0x00270A32 File Offset: 0x0026EC32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B18C RID: 45452 RVA: 0x00270A3A File Offset: 0x0026EC3A
		private substring_splitTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B18D RID: 45453 RVA: 0x00270A43 File Offset: 0x0026EC43
		public static substring_splitTrim CreateUnsafe(ProgramNode node)
		{
			return new substring_splitTrim(node);
		}

		// Token: 0x0600B18E RID: 45454 RVA: 0x00270A4C File Offset: 0x0026EC4C
		public static substring_splitTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.substring_splitTrim)
			{
				return null;
			}
			return new substring_splitTrim?(substring_splitTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B18F RID: 45455 RVA: 0x00270A81 File Offset: 0x0026EC81
		public substring_splitTrim(GrammarBuilders g, splitTrim value0)
		{
			this._node = g.UnnamedConversion.substring_splitTrim.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B190 RID: 45456 RVA: 0x00270AA0 File Offset: 0x0026ECA0
		public static implicit operator substring(substring_splitTrim arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x0600B191 RID: 45457 RVA: 0x00270AAE File Offset: 0x0026ECAE
		public splitTrim splitTrim
		{
			get
			{
				return splitTrim.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B192 RID: 45458 RVA: 0x00270AC2 File Offset: 0x0026ECC2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B193 RID: 45459 RVA: 0x00270AD8 File Offset: 0x0026ECD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B194 RID: 45460 RVA: 0x00270B02 File Offset: 0x0026ED02
		public bool Equals(substring_splitTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F0 RID: 17904
		private ProgramNode _node;
	}
}
