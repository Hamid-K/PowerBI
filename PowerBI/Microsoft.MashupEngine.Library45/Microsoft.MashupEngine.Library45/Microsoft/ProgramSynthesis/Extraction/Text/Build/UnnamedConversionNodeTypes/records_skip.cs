using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000F20 RID: 3872
	public struct records_skip : IProgramNodeBuilder, IEquatable<records_skip>
	{
		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06006B06 RID: 27398 RVA: 0x00160792 File Offset: 0x0015E992
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x0016079A File Offset: 0x0015E99A
		private records_skip(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x001607A3 File Offset: 0x0015E9A3
		public static records_skip CreateUnsafe(ProgramNode node)
		{
			return new records_skip(node);
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x001607AC File Offset: 0x0015E9AC
		public static records_skip? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.records_skip)
			{
				return null;
			}
			return new records_skip?(records_skip.CreateUnsafe(node));
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x001607E1 File Offset: 0x0015E9E1
		public records_skip(GrammarBuilders g, skip value0)
		{
			this._node = g.UnnamedConversion.records_skip.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x00160800 File Offset: 0x0015EA00
		public static implicit operator records(records_skip arg)
		{
			return records.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0016080E File Offset: 0x0015EA0E
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x00160822 File Offset: 0x0015EA22
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x00160838 File Offset: 0x0015EA38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x00160862 File Offset: 0x0015EA62
		public bool Equals(records_skip other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F0B RID: 12043
		private ProgramNode _node;
	}
}
