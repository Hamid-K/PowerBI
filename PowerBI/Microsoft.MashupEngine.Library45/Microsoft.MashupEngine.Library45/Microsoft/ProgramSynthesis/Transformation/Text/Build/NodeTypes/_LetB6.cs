using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5C RID: 7260
	public struct _LetB6 : IProgramNodeBuilder, IEquatable<_LetB6>
	{
		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x0600F5CC RID: 62924 RVA: 0x003484E2 File Offset: 0x003466E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5CD RID: 62925 RVA: 0x003484EA File Offset: 0x003466EA
		private _LetB6(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5CE RID: 62926 RVA: 0x003484F3 File Offset: 0x003466F3
		public static _LetB6 CreateUnsafe(ProgramNode node)
		{
			return new _LetB6(node);
		}

		// Token: 0x0600F5CF RID: 62927 RVA: 0x003484FC File Offset: 0x003466FC
		public static _LetB6? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB6)
			{
				return null;
			}
			return new _LetB6?(_LetB6.CreateUnsafe(node));
		}

		// Token: 0x0600F5D0 RID: 62928 RVA: 0x00348536 File Offset: 0x00346736
		public static _LetB6 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB6(new Hole(g.Symbol._LetB6, holeId));
		}

		// Token: 0x0600F5D1 RID: 62929 RVA: 0x0034854E File Offset: 0x0034674E
		public LetPL2 Cast_LetPL2()
		{
			return LetPL2.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5D2 RID: 62930 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetPL2(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5D3 RID: 62931 RVA: 0x0034855B File Offset: 0x0034675B
		public bool Is_LetPL2(GrammarBuilders g, out LetPL2 value)
		{
			value = LetPL2.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5D4 RID: 62932 RVA: 0x0034856F File Offset: 0x0034676F
		public LetPL2? As_LetPL2(GrammarBuilders g)
		{
			return new LetPL2?(LetPL2.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5D5 RID: 62933 RVA: 0x00348581 File Offset: 0x00346781
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5D6 RID: 62934 RVA: 0x00348594 File Offset: 0x00346794
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5D7 RID: 62935 RVA: 0x003485BE File Offset: 0x003467BE
		public bool Equals(_LetB6 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4B RID: 23371
		private ProgramNode _node;
	}
}
