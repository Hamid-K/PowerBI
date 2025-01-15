using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C59 RID: 7257
	public struct _LetB3 : IProgramNodeBuilder, IEquatable<_LetB3>
	{
		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x0600F5A8 RID: 62888 RVA: 0x00348212 File Offset: 0x00346412
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5A9 RID: 62889 RVA: 0x0034821A File Offset: 0x0034641A
		private _LetB3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5AA RID: 62890 RVA: 0x00348223 File Offset: 0x00346423
		public static _LetB3 CreateUnsafe(ProgramNode node)
		{
			return new _LetB3(node);
		}

		// Token: 0x0600F5AB RID: 62891 RVA: 0x0034822C File Offset: 0x0034642C
		public static _LetB3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB3)
			{
				return null;
			}
			return new _LetB3?(_LetB3.CreateUnsafe(node));
		}

		// Token: 0x0600F5AC RID: 62892 RVA: 0x00348266 File Offset: 0x00346466
		public static _LetB3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB3(new Hole(g.Symbol._LetB3, holeId));
		}

		// Token: 0x0600F5AD RID: 62893 RVA: 0x0034827E File Offset: 0x0034647E
		public PosPairRelative Cast_PosPairRelative()
		{
			return PosPairRelative.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5AE RID: 62894 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_PosPairRelative(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5AF RID: 62895 RVA: 0x0034828B File Offset: 0x0034648B
		public bool Is_PosPairRelative(GrammarBuilders g, out PosPairRelative value)
		{
			value = PosPairRelative.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5B0 RID: 62896 RVA: 0x0034829F File Offset: 0x0034649F
		public PosPairRelative? As_PosPairRelative(GrammarBuilders g)
		{
			return new PosPairRelative?(PosPairRelative.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5B1 RID: 62897 RVA: 0x003482B1 File Offset: 0x003464B1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5B2 RID: 62898 RVA: 0x003482C4 File Offset: 0x003464C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5B3 RID: 62899 RVA: 0x003482EE File Offset: 0x003464EE
		public bool Equals(_LetB3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B48 RID: 23368
		private ProgramNode _node;
	}
}
