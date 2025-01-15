using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5A RID: 7258
	public struct _LetB4 : IProgramNodeBuilder, IEquatable<_LetB4>
	{
		// Token: 0x170028F0 RID: 10480
		// (get) Token: 0x0600F5B4 RID: 62900 RVA: 0x00348302 File Offset: 0x00346502
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5B5 RID: 62901 RVA: 0x0034830A File Offset: 0x0034650A
		private _LetB4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5B6 RID: 62902 RVA: 0x00348313 File Offset: 0x00346513
		public static _LetB4 CreateUnsafe(ProgramNode node)
		{
			return new _LetB4(node);
		}

		// Token: 0x0600F5B7 RID: 62903 RVA: 0x0034831C File Offset: 0x0034651C
		public static _LetB4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB4)
			{
				return null;
			}
			return new _LetB4?(_LetB4.CreateUnsafe(node));
		}

		// Token: 0x0600F5B8 RID: 62904 RVA: 0x00348356 File Offset: 0x00346556
		public static _LetB4 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB4(new Hole(g.Symbol._LetB4, holeId));
		}

		// Token: 0x0600F5B9 RID: 62905 RVA: 0x0034836E File Offset: 0x0034656E
		public _LetB4 Cast__LetB4()
		{
			return _LetB4.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5BA RID: 62906 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is__LetB4(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5BB RID: 62907 RVA: 0x0034837B File Offset: 0x0034657B
		public bool Is__LetB4(GrammarBuilders g, out _LetB4 value)
		{
			value = _LetB4.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5BC RID: 62908 RVA: 0x0034838F File Offset: 0x0034658F
		public _LetB4? As__LetB4(GrammarBuilders g)
		{
			return new _LetB4?(_LetB4.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5BD RID: 62909 RVA: 0x003483A1 File Offset: 0x003465A1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5BE RID: 62910 RVA: 0x003483B4 File Offset: 0x003465B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5BF RID: 62911 RVA: 0x003483DE File Offset: 0x003465DE
		public bool Equals(_LetB4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B49 RID: 23369
		private ProgramNode _node;
	}
}
