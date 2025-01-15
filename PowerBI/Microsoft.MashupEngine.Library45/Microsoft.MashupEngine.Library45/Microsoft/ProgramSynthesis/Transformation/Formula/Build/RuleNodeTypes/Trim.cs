using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158B RID: 5515
	public struct Trim : IProgramNodeBuilder, IEquatable<Trim>
	{
		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x0600B4A2 RID: 46242 RVA: 0x0027519E File Offset: 0x0027339E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4A3 RID: 46243 RVA: 0x002751A6 File Offset: 0x002733A6
		private Trim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4A4 RID: 46244 RVA: 0x002751AF File Offset: 0x002733AF
		public static Trim CreateUnsafe(ProgramNode node)
		{
			return new Trim(node);
		}

		// Token: 0x0600B4A5 RID: 46245 RVA: 0x002751B8 File Offset: 0x002733B8
		public static Trim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(node));
		}

		// Token: 0x0600B4A6 RID: 46246 RVA: 0x002751ED File Offset: 0x002733ED
		public Trim(GrammarBuilders g, fromStr value0)
		{
			this._node = g.Rule.Trim.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B4A7 RID: 46247 RVA: 0x0027520C File Offset: 0x0027340C
		public static implicit operator fromStrTrim(Trim arg)
		{
			return fromStrTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x0600B4A8 RID: 46248 RVA: 0x0027521A File Offset: 0x0027341A
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B4A9 RID: 46249 RVA: 0x0027522E File Offset: 0x0027342E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4AA RID: 46250 RVA: 0x00275244 File Offset: 0x00273444
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4AB RID: 46251 RVA: 0x0027526E File Offset: 0x0027346E
		public bool Equals(Trim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004639 RID: 17977
		private ProgramNode _node;
	}
}
