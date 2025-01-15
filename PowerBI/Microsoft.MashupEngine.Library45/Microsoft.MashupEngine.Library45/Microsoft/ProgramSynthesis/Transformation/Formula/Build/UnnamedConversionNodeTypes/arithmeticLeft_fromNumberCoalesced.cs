using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001534 RID: 5428
	public struct arithmeticLeft_fromNumberCoalesced : IProgramNodeBuilder, IEquatable<arithmeticLeft_fromNumberCoalesced>
	{
		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x0600B0FF RID: 45311 RVA: 0x0026FDBA File Offset: 0x0026DFBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B100 RID: 45312 RVA: 0x0026FDC2 File Offset: 0x0026DFC2
		private arithmeticLeft_fromNumberCoalesced(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B101 RID: 45313 RVA: 0x0026FDCB File Offset: 0x0026DFCB
		public static arithmeticLeft_fromNumberCoalesced CreateUnsafe(ProgramNode node)
		{
			return new arithmeticLeft_fromNumberCoalesced(node);
		}

		// Token: 0x0600B102 RID: 45314 RVA: 0x0026FDD4 File Offset: 0x0026DFD4
		public static arithmeticLeft_fromNumberCoalesced? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced)
			{
				return null;
			}
			return new arithmeticLeft_fromNumberCoalesced?(arithmeticLeft_fromNumberCoalesced.CreateUnsafe(node));
		}

		// Token: 0x0600B103 RID: 45315 RVA: 0x0026FE09 File Offset: 0x0026E009
		public arithmeticLeft_fromNumberCoalesced(GrammarBuilders g, fromNumberCoalesced value0)
		{
			this._node = g.UnnamedConversion.arithmeticLeft_fromNumberCoalesced.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B104 RID: 45316 RVA: 0x0026FE28 File Offset: 0x0026E028
		public static implicit operator arithmeticLeft(arithmeticLeft_fromNumberCoalesced arg)
		{
			return arithmeticLeft.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x0600B105 RID: 45317 RVA: 0x0026FE36 File Offset: 0x0026E036
		public fromNumberCoalesced fromNumberCoalesced
		{
			get
			{
				return fromNumberCoalesced.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B106 RID: 45318 RVA: 0x0026FE4A File Offset: 0x0026E04A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B107 RID: 45319 RVA: 0x0026FE60 File Offset: 0x0026E060
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B108 RID: 45320 RVA: 0x0026FE8A File Offset: 0x0026E08A
		public bool Equals(arithmeticLeft_fromNumberCoalesced other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E2 RID: 17890
		private ProgramNode _node;
	}
}
