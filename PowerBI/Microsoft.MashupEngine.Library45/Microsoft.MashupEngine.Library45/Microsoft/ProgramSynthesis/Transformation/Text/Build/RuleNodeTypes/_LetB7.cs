using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C34 RID: 7220
	public struct _LetB7 : IProgramNodeBuilder, IEquatable<_LetB7>
	{
		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x0600F343 RID: 62275 RVA: 0x003423A2 File Offset: 0x003405A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F344 RID: 62276 RVA: 0x003423AA File Offset: 0x003405AA
		private _LetB7(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F345 RID: 62277 RVA: 0x003423B3 File Offset: 0x003405B3
		public static _LetB7 CreateUnsafe(ProgramNode node)
		{
			return new _LetB7(node);
		}

		// Token: 0x0600F346 RID: 62278 RVA: 0x003423BC File Offset: 0x003405BC
		public static _LetB7? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule._LetB7)
			{
				return null;
			}
			return new _LetB7?(_LetB7.CreateUnsafe(node));
		}

		// Token: 0x0600F347 RID: 62279 RVA: 0x003423F1 File Offset: 0x003405F1
		public _LetB7(GrammarBuilders g, _LetB5 value0, _LetB6 value1)
		{
			this._node = new LetNode(g.Rule._LetB7, value0.Node, value1.Node);
		}

		// Token: 0x0600F348 RID: 62280 RVA: 0x00342417 File Offset: 0x00340617
		public static implicit operator _LetB7(_LetB7 arg)
		{
			return _LetB7.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x0600F349 RID: 62281 RVA: 0x00342425 File Offset: 0x00340625
		public _LetB5 _LetB5
		{
			get
			{
				return _LetB5.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x0600F34A RID: 62282 RVA: 0x00342439 File Offset: 0x00340639
		public _LetB6 _LetB6
		{
			get
			{
				return _LetB6.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F34B RID: 62283 RVA: 0x0034244D File Offset: 0x0034064D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F34C RID: 62284 RVA: 0x00342460 File Offset: 0x00340660
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F34D RID: 62285 RVA: 0x0034248A File Offset: 0x0034068A
		public bool Equals(_LetB7 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B23 RID: 23331
		private ProgramNode _node;
	}
}
