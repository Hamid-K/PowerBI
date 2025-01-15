using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151D RID: 5405
	public struct outStr1_formatted : IProgramNodeBuilder, IEquatable<outStr1_formatted>
	{
		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x0600B019 RID: 45081 RVA: 0x0026E93E File Offset: 0x0026CB3E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B01A RID: 45082 RVA: 0x0026E946 File Offset: 0x0026CB46
		private outStr1_formatted(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B01B RID: 45083 RVA: 0x0026E94F File Offset: 0x0026CB4F
		public static outStr1_formatted CreateUnsafe(ProgramNode node)
		{
			return new outStr1_formatted(node);
		}

		// Token: 0x0600B01C RID: 45084 RVA: 0x0026E958 File Offset: 0x0026CB58
		public static outStr1_formatted? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outStr1_formatted)
			{
				return null;
			}
			return new outStr1_formatted?(outStr1_formatted.CreateUnsafe(node));
		}

		// Token: 0x0600B01D RID: 45085 RVA: 0x0026E98D File Offset: 0x0026CB8D
		public outStr1_formatted(GrammarBuilders g, formatted value0)
		{
			this._node = g.UnnamedConversion.outStr1_formatted.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B01E RID: 45086 RVA: 0x0026E9AC File Offset: 0x0026CBAC
		public static implicit operator outStr1(outStr1_formatted arg)
		{
			return outStr1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x0600B01F RID: 45087 RVA: 0x0026E9BA File Offset: 0x0026CBBA
		public formatted formatted
		{
			get
			{
				return formatted.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B020 RID: 45088 RVA: 0x0026E9CE File Offset: 0x0026CBCE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B021 RID: 45089 RVA: 0x0026E9E4 File Offset: 0x0026CBE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B022 RID: 45090 RVA: 0x0026EA0E File Offset: 0x0026CC0E
		public bool Equals(outStr1_formatted other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CB RID: 17867
		private ProgramNode _node;
	}
}
