using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200107C RID: 4220
	public struct singletonField : IProgramNodeBuilder, IEquatable<singletonField>
	{
		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x001A9F42 File Offset: 0x001A8142
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x001A9F4A File Offset: 0x001A814A
		private singletonField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x001A9F53 File Offset: 0x001A8153
		public static singletonField CreateUnsafe(ProgramNode node)
		{
			return new singletonField(node);
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x001A9F5C File Offset: 0x001A815C
		public static singletonField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.singletonField)
			{
				return null;
			}
			return new singletonField?(singletonField.CreateUnsafe(node));
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x001A9F96 File Offset: 0x001A8196
		public static singletonField CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new singletonField(new Hole(g.Symbol.singletonField, holeId));
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x001A9FAE File Offset: 0x001A81AE
		public bool Is_TrimmedTextField(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimmedTextField;
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x001A9FC8 File Offset: 0x001A81C8
		public bool Is_TrimmedTextField(GrammarBuilders g, out TrimmedTextField value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimmedTextField)
			{
				value = TrimmedTextField.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimmedTextField);
			return false;
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x001AA000 File Offset: 0x001A8200
		public TrimmedTextField? As_TrimmedTextField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimmedTextField)
			{
				return null;
			}
			return new TrimmedTextField?(TrimmedTextField.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x001AA040 File Offset: 0x001A8240
		public TrimmedTextField Cast_TrimmedTextField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimmedTextField)
			{
				return TrimmedTextField.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimmedTextField is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x001AA095 File Offset: 0x001A8295
		public bool Is_SubstringField(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SubstringField;
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x001AA0AF File Offset: 0x001A82AF
		public bool Is_SubstringField(GrammarBuilders g, out SubstringField value)
		{
			if (this.Node.GrammarRule == g.Rule.SubstringField)
			{
				value = SubstringField.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SubstringField);
			return false;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x001AA0E4 File Offset: 0x001A82E4
		public SubstringField? As_SubstringField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SubstringField)
			{
				return null;
			}
			return new SubstringField?(SubstringField.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x001AA124 File Offset: 0x001A8324
		public SubstringField Cast_SubstringField(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SubstringField)
			{
				return SubstringField.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SubstringField is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E92 RID: 32402 RVA: 0x001AA17C File Offset: 0x001A837C
		public T Switch<T>(GrammarBuilders g, Func<TrimmedTextField, T> func0, Func<SubstringField, T> func1)
		{
			TrimmedTextField trimmedTextField;
			if (this.Is_TrimmedTextField(g, out trimmedTextField))
			{
				return func0(trimmedTextField);
			}
			SubstringField substringField;
			if (this.Is_SubstringField(g, out substringField))
			{
				return func1(substringField);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol singletonField");
		}

		// Token: 0x06007E93 RID: 32403 RVA: 0x001AA1D4 File Offset: 0x001A83D4
		public void Switch(GrammarBuilders g, Action<TrimmedTextField> func0, Action<SubstringField> func1)
		{
			TrimmedTextField trimmedTextField;
			if (this.Is_TrimmedTextField(g, out trimmedTextField))
			{
				func0(trimmedTextField);
				return;
			}
			SubstringField substringField;
			if (this.Is_SubstringField(g, out substringField))
			{
				func1(substringField);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol singletonField");
		}

		// Token: 0x06007E94 RID: 32404 RVA: 0x001AA22B File Offset: 0x001A842B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x001AA240 File Offset: 0x001A8440
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E96 RID: 32406 RVA: 0x001AA26A File Offset: 0x001A846A
		public bool Equals(singletonField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003395 RID: 13205
		private ProgramNode _node;
	}
}
