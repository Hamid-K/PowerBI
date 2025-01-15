using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F4 RID: 4596
	public struct multi_result_matches : IProgramNodeBuilder, IEquatable<multi_result_matches>
	{
		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06008A56 RID: 35414 RVA: 0x001D0AB2 File Offset: 0x001CECB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x001D0ABA File Offset: 0x001CECBA
		private multi_result_matches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x001D0AC3 File Offset: 0x001CECC3
		public static multi_result_matches CreateUnsafe(ProgramNode node)
		{
			return new multi_result_matches(node);
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x001D0ACC File Offset: 0x001CECCC
		public static multi_result_matches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.multi_result_matches)
			{
				return null;
			}
			return new multi_result_matches?(multi_result_matches.CreateUnsafe(node));
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x001D0B06 File Offset: 0x001CED06
		public static multi_result_matches CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new multi_result_matches(new Hole(g.Symbol.multi_result_matches, holeId));
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x001D0B1E File Offset: 0x001CED1E
		public bool Is_Nil(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Nil;
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x001D0B38 File Offset: 0x001CED38
		public bool Is_Nil(GrammarBuilders g, out Nil value)
		{
			if (this.Node.GrammarRule == g.Rule.Nil)
			{
				value = Nil.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Nil);
			return false;
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x001D0B70 File Offset: 0x001CED70
		public Nil? As_Nil(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Nil)
			{
				return null;
			}
			return new Nil?(Nil.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x001D0BB0 File Offset: 0x001CEDB0
		public Nil Cast_Nil(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Nil)
			{
				return Nil.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Nil is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x001D0C05 File Offset: 0x001CEE05
		public bool Is_LetHead(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetHead;
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x001D0C1F File Offset: 0x001CEE1F
		public bool Is_LetHead(GrammarBuilders g, out LetHead value)
		{
			if (this.Node.GrammarRule == g.Rule.LetHead)
			{
				value = LetHead.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetHead);
			return false;
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x001D0C54 File Offset: 0x001CEE54
		public LetHead? As_LetHead(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetHead)
			{
				return null;
			}
			return new LetHead?(LetHead.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x001D0C94 File Offset: 0x001CEE94
		public LetHead Cast_LetHead(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetHead)
			{
				return LetHead.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetHead is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A63 RID: 35427 RVA: 0x001D0CEC File Offset: 0x001CEEEC
		public T Switch<T>(GrammarBuilders g, Func<Nil, T> func0, Func<LetHead, T> func1)
		{
			Nil nil;
			if (this.Is_Nil(g, out nil))
			{
				return func0(nil);
			}
			LetHead letHead;
			if (this.Is_LetHead(g, out letHead))
			{
				return func1(letHead);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multi_result_matches");
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x001D0D44 File Offset: 0x001CEF44
		public void Switch(GrammarBuilders g, Action<Nil> func0, Action<LetHead> func1)
		{
			Nil nil;
			if (this.Is_Nil(g, out nil))
			{
				func0(nil);
				return;
			}
			LetHead letHead;
			if (this.Is_LetHead(g, out letHead))
			{
				func1(letHead);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multi_result_matches");
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x001D0D9B File Offset: 0x001CEF9B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x001D0DB0 File Offset: 0x001CEFB0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x001D0DDA File Offset: 0x001CEFDA
		public bool Equals(multi_result_matches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A8 RID: 14504
		private ProgramNode _node;
	}
}
