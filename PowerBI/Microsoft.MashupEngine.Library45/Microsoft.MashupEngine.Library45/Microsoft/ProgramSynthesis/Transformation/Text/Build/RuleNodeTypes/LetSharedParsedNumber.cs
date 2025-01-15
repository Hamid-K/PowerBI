using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C30 RID: 7216
	public struct LetSharedParsedNumber : IProgramNodeBuilder, IEquatable<LetSharedParsedNumber>
	{
		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x0600F317 RID: 62231 RVA: 0x00341FB2 File Offset: 0x003401B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F318 RID: 62232 RVA: 0x00341FBA File Offset: 0x003401BA
		private LetSharedParsedNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F319 RID: 62233 RVA: 0x00341FC3 File Offset: 0x003401C3
		public static LetSharedParsedNumber CreateUnsafe(ProgramNode node)
		{
			return new LetSharedParsedNumber(node);
		}

		// Token: 0x0600F31A RID: 62234 RVA: 0x00341FCC File Offset: 0x003401CC
		public static LetSharedParsedNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSharedParsedNumber)
			{
				return null;
			}
			return new LetSharedParsedNumber?(LetSharedParsedNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F31B RID: 62235 RVA: 0x00342001 File Offset: 0x00340201
		public LetSharedParsedNumber(GrammarBuilders g, inputNumber value0, _LetB0 value1)
		{
			this._node = new LetNode(g.Rule.LetSharedParsedNumber, value0.Node, value1.Node);
		}

		// Token: 0x0600F31C RID: 62236 RVA: 0x00342027 File Offset: 0x00340227
		public static implicit operator conv(LetSharedParsedNumber arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x0600F31D RID: 62237 RVA: 0x00342035 File Offset: 0x00340235
		public inputNumber inputNumber
		{
			get
			{
				return inputNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x0600F31E RID: 62238 RVA: 0x00342049 File Offset: 0x00340249
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F31F RID: 62239 RVA: 0x0034205D File Offset: 0x0034025D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F320 RID: 62240 RVA: 0x00342070 File Offset: 0x00340270
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F321 RID: 62241 RVA: 0x0034209A File Offset: 0x0034029A
		public bool Equals(LetSharedParsedNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1F RID: 23327
		private ProgramNode _node;
	}
}
