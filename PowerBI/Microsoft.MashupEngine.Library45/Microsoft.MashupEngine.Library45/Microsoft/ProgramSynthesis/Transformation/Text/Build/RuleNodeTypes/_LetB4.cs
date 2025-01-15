using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C32 RID: 7218
	public struct _LetB4 : IProgramNodeBuilder, IEquatable<_LetB4>
	{
		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x0600F32D RID: 62253 RVA: 0x003421AA File Offset: 0x003403AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F32E RID: 62254 RVA: 0x003421B2 File Offset: 0x003403B2
		private _LetB4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F32F RID: 62255 RVA: 0x003421BB File Offset: 0x003403BB
		public static _LetB4 CreateUnsafe(ProgramNode node)
		{
			return new _LetB4(node);
		}

		// Token: 0x0600F330 RID: 62256 RVA: 0x003421C4 File Offset: 0x003403C4
		public static _LetB4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule._LetB4)
			{
				return null;
			}
			return new _LetB4?(_LetB4.CreateUnsafe(node));
		}

		// Token: 0x0600F331 RID: 62257 RVA: 0x003421F9 File Offset: 0x003403F9
		public _LetB4(GrammarBuilders g, _LetB2 value0, _LetB3 value1)
		{
			this._node = new LetNode(g.Rule._LetB4, value0.Node, value1.Node);
		}

		// Token: 0x0600F332 RID: 62258 RVA: 0x0034221F File Offset: 0x0034041F
		public static implicit operator _LetB4(_LetB4 arg)
		{
			return _LetB4.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028BF RID: 10431
		// (get) Token: 0x0600F333 RID: 62259 RVA: 0x0034222D File Offset: 0x0034042D
		public _LetB2 _LetB2
		{
			get
			{
				return _LetB2.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028C0 RID: 10432
		// (get) Token: 0x0600F334 RID: 62260 RVA: 0x00342241 File Offset: 0x00340441
		public _LetB3 _LetB3
		{
			get
			{
				return _LetB3.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F335 RID: 62261 RVA: 0x00342255 File Offset: 0x00340455
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F336 RID: 62262 RVA: 0x00342268 File Offset: 0x00340468
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F337 RID: 62263 RVA: 0x00342292 File Offset: 0x00340492
		public bool Equals(_LetB4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B21 RID: 23329
		private ProgramNode _node;
	}
}
