using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF5 RID: 7157
	public struct inputDateTime_parsedDateTime : IProgramNodeBuilder, IEquatable<inputDateTime_parsedDateTime>
	{
		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x0600F093 RID: 61587 RVA: 0x0033E552 File Offset: 0x0033C752
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F094 RID: 61588 RVA: 0x0033E55A File Offset: 0x0033C75A
		private inputDateTime_parsedDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F095 RID: 61589 RVA: 0x0033E563 File Offset: 0x0033C763
		public static inputDateTime_parsedDateTime CreateUnsafe(ProgramNode node)
		{
			return new inputDateTime_parsedDateTime(node);
		}

		// Token: 0x0600F096 RID: 61590 RVA: 0x0033E56C File Offset: 0x0033C76C
		public static inputDateTime_parsedDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.inputDateTime_parsedDateTime)
			{
				return null;
			}
			return new inputDateTime_parsedDateTime?(inputDateTime_parsedDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F097 RID: 61591 RVA: 0x0033E5A1 File Offset: 0x0033C7A1
		public inputDateTime_parsedDateTime(GrammarBuilders g, parsedDateTime value0)
		{
			this._node = g.UnnamedConversion.inputDateTime_parsedDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F098 RID: 61592 RVA: 0x0033E5C0 File Offset: 0x0033C7C0
		public static implicit operator inputDateTime(inputDateTime_parsedDateTime arg)
		{
			return inputDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x0600F099 RID: 61593 RVA: 0x0033E5CE File Offset: 0x0033C7CE
		public parsedDateTime parsedDateTime
		{
			get
			{
				return parsedDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F09A RID: 61594 RVA: 0x0033E5E2 File Offset: 0x0033C7E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F09B RID: 61595 RVA: 0x0033E5F8 File Offset: 0x0033C7F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F09C RID: 61596 RVA: 0x0033E622 File Offset: 0x0033C822
		public bool Equals(inputDateTime_parsedDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE4 RID: 23268
		private ProgramNode _node;
	}
}
