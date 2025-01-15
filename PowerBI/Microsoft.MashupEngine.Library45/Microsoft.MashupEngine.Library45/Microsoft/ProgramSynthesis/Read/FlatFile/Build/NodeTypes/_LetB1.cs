using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001282 RID: 4738
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06008F48 RID: 36680 RVA: 0x001E268A File Offset: 0x001E088A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F49 RID: 36681 RVA: 0x001E2692 File Offset: 0x001E0892
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F4A RID: 36682 RVA: 0x001E269B File Offset: 0x001E089B
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06008F4B RID: 36683 RVA: 0x001E26A4 File Offset: 0x001E08A4
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06008F4C RID: 36684 RVA: 0x001E26DE File Offset: 0x001E08DE
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06008F4D RID: 36685 RVA: 0x001E26F6 File Offset: 0x001E08F6
		public ETextOutput Cast_ETextOutput()
		{
			return ETextOutput.CreateUnsafe(this.Node);
		}

		// Token: 0x06008F4E RID: 36686 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_ETextOutput(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008F4F RID: 36687 RVA: 0x001E2703 File Offset: 0x001E0903
		public bool Is_ETextOutput(GrammarBuilders g, out ETextOutput value)
		{
			value = ETextOutput.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008F50 RID: 36688 RVA: 0x001E2717 File Offset: 0x001E0917
		public ETextOutput? As_ETextOutput(GrammarBuilders g)
		{
			return new ETextOutput?(ETextOutput.CreateUnsafe(this.Node));
		}

		// Token: 0x06008F51 RID: 36689 RVA: 0x001E2729 File Offset: 0x001E0929
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F52 RID: 36690 RVA: 0x001E273C File Offset: 0x001E093C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F53 RID: 36691 RVA: 0x001E2766 File Offset: 0x001E0966
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A73 RID: 14963
		private ProgramNode _node;
	}
}
