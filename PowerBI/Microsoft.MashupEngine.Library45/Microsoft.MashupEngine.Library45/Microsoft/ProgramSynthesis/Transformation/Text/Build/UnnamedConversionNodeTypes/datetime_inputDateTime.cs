using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF4 RID: 7156
	public struct datetime_inputDateTime : IProgramNodeBuilder, IEquatable<datetime_inputDateTime>
	{
		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x0600F089 RID: 61577 RVA: 0x0033E46E File Offset: 0x0033C66E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F08A RID: 61578 RVA: 0x0033E476 File Offset: 0x0033C676
		private datetime_inputDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F08B RID: 61579 RVA: 0x0033E47F File Offset: 0x0033C67F
		public static datetime_inputDateTime CreateUnsafe(ProgramNode node)
		{
			return new datetime_inputDateTime(node);
		}

		// Token: 0x0600F08C RID: 61580 RVA: 0x0033E488 File Offset: 0x0033C688
		public static datetime_inputDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.datetime_inputDateTime)
			{
				return null;
			}
			return new datetime_inputDateTime?(datetime_inputDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F08D RID: 61581 RVA: 0x0033E4BD File Offset: 0x0033C6BD
		public datetime_inputDateTime(GrammarBuilders g, inputDateTime value0)
		{
			this._node = g.UnnamedConversion.datetime_inputDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F08E RID: 61582 RVA: 0x0033E4DC File Offset: 0x0033C6DC
		public static implicit operator datetime(datetime_inputDateTime arg)
		{
			return datetime.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x0600F08F RID: 61583 RVA: 0x0033E4EA File Offset: 0x0033C6EA
		public inputDateTime inputDateTime
		{
			get
			{
				return inputDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F090 RID: 61584 RVA: 0x0033E4FE File Offset: 0x0033C6FE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F091 RID: 61585 RVA: 0x0033E514 File Offset: 0x0033C714
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F092 RID: 61586 RVA: 0x0033E53E File Offset: 0x0033C73E
		public bool Equals(datetime_inputDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE3 RID: 23267
		private ProgramNode _node;
	}
}
