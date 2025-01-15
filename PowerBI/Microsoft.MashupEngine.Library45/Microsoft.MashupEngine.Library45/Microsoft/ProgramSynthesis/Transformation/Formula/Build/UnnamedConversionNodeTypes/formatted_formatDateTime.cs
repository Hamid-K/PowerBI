using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001524 RID: 5412
	public struct formatted_formatDateTime : IProgramNodeBuilder, IEquatable<formatted_formatDateTime>
	{
		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x0600B05F RID: 45151 RVA: 0x0026EF7A File Offset: 0x0026D17A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B060 RID: 45152 RVA: 0x0026EF82 File Offset: 0x0026D182
		private formatted_formatDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B061 RID: 45153 RVA: 0x0026EF8B File Offset: 0x0026D18B
		public static formatted_formatDateTime CreateUnsafe(ProgramNode node)
		{
			return new formatted_formatDateTime(node);
		}

		// Token: 0x0600B062 RID: 45154 RVA: 0x0026EF94 File Offset: 0x0026D194
		public static formatted_formatDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.formatted_formatDateTime)
			{
				return null;
			}
			return new formatted_formatDateTime?(formatted_formatDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B063 RID: 45155 RVA: 0x0026EFC9 File Offset: 0x0026D1C9
		public formatted_formatDateTime(GrammarBuilders g, formatDateTime value0)
		{
			this._node = g.UnnamedConversion.formatted_formatDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B064 RID: 45156 RVA: 0x0026EFE8 File Offset: 0x0026D1E8
		public static implicit operator formatted(formatted_formatDateTime arg)
		{
			return formatted.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x0600B065 RID: 45157 RVA: 0x0026EFF6 File Offset: 0x0026D1F6
		public formatDateTime formatDateTime
		{
			get
			{
				return formatDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B066 RID: 45158 RVA: 0x0026F00A File Offset: 0x0026D20A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B067 RID: 45159 RVA: 0x0026F020 File Offset: 0x0026D220
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B068 RID: 45160 RVA: 0x0026F04A File Offset: 0x0026D24A
		public bool Equals(formatted_formatDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D2 RID: 17874
		private ProgramNode _node;
	}
}
