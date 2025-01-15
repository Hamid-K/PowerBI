using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5D RID: 7261
	public struct _LetB7 : IProgramNodeBuilder, IEquatable<_LetB7>
	{
		// Token: 0x170028F3 RID: 10483
		// (get) Token: 0x0600F5D8 RID: 62936 RVA: 0x003485D2 File Offset: 0x003467D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5D9 RID: 62937 RVA: 0x003485DA File Offset: 0x003467DA
		private _LetB7(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5DA RID: 62938 RVA: 0x003485E3 File Offset: 0x003467E3
		public static _LetB7 CreateUnsafe(ProgramNode node)
		{
			return new _LetB7(node);
		}

		// Token: 0x0600F5DB RID: 62939 RVA: 0x003485EC File Offset: 0x003467EC
		public static _LetB7? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB7)
			{
				return null;
			}
			return new _LetB7?(_LetB7.CreateUnsafe(node));
		}

		// Token: 0x0600F5DC RID: 62940 RVA: 0x00348626 File Offset: 0x00346826
		public static _LetB7 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB7(new Hole(g.Symbol._LetB7, holeId));
		}

		// Token: 0x0600F5DD RID: 62941 RVA: 0x0034863E File Offset: 0x0034683E
		public _LetB7 Cast__LetB7()
		{
			return _LetB7.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5DE RID: 62942 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is__LetB7(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5DF RID: 62943 RVA: 0x0034864B File Offset: 0x0034684B
		public bool Is__LetB7(GrammarBuilders g, out _LetB7 value)
		{
			value = _LetB7.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5E0 RID: 62944 RVA: 0x0034865F File Offset: 0x0034685F
		public _LetB7? As__LetB7(GrammarBuilders g)
		{
			return new _LetB7?(_LetB7.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5E1 RID: 62945 RVA: 0x00348671 File Offset: 0x00346871
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5E2 RID: 62946 RVA: 0x00348684 File Offset: 0x00346884
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5E3 RID: 62947 RVA: 0x003486AE File Offset: 0x003468AE
		public bool Equals(_LetB7 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4C RID: 23372
		private ProgramNode _node;
	}
}
