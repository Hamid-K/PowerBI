using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001033 RID: 4147
	public struct SubstringField : IProgramNodeBuilder, IEquatable<SubstringField>
	{
		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06007AA5 RID: 31397 RVA: 0x001A222A File Offset: 0x001A042A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x001A2232 File Offset: 0x001A0432
		private SubstringField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x001A223B File Offset: 0x001A043B
		public static SubstringField CreateUnsafe(ProgramNode node)
		{
			return new SubstringField(node);
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x001A2244 File Offset: 0x001A0444
		public static SubstringField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SubstringField)
			{
				return null;
			}
			return new SubstringField?(SubstringField.CreateUnsafe(node));
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x001A2279 File Offset: 0x001A0479
		public SubstringField(GrammarBuilders g, fieldSubstring value0)
		{
			this._node = g.Rule.SubstringField.BuildASTNode(value0.Node);
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x001A2298 File Offset: 0x001A0498
		public static implicit operator singletonField(SubstringField arg)
		{
			return singletonField.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06007AAB RID: 31403 RVA: 0x001A22A6 File Offset: 0x001A04A6
		public fieldSubstring fieldSubstring
		{
			get
			{
				return fieldSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x001A22BA File Offset: 0x001A04BA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x001A22D0 File Offset: 0x001A04D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x001A22FA File Offset: 0x001A04FA
		public bool Equals(SubstringField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400334C RID: 13132
		private ProgramNode _node;
	}
}
