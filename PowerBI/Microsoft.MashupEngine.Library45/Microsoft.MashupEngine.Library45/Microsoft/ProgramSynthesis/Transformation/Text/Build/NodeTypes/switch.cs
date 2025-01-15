using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C37 RID: 7223
	public struct @switch : IProgramNodeBuilder, IEquatable<@switch>
	{
		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x0600F364 RID: 62308 RVA: 0x00342696 File Offset: 0x00340896
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F365 RID: 62309 RVA: 0x0034269E File Offset: 0x0034089E
		private @switch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F366 RID: 62310 RVA: 0x003426A7 File Offset: 0x003408A7
		public static @switch CreateUnsafe(ProgramNode node)
		{
			return new @switch(node);
		}

		// Token: 0x0600F367 RID: 62311 RVA: 0x003426B0 File Offset: 0x003408B0
		public static @switch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.@switch)
			{
				return null;
			}
			return new @switch?(@switch.CreateUnsafe(node));
		}

		// Token: 0x0600F368 RID: 62312 RVA: 0x003426EA File Offset: 0x003408EA
		public static @switch CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new @switch(new Hole(g.Symbol.@switch, holeId));
		}

		// Token: 0x0600F369 RID: 62313 RVA: 0x00342702 File Offset: 0x00340902
		public bool Is_SingleBranch(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleBranch;
		}

		// Token: 0x0600F36A RID: 62314 RVA: 0x0034271C File Offset: 0x0034091C
		public bool Is_SingleBranch(GrammarBuilders g, out SingleBranch value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleBranch)
			{
				value = SingleBranch.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleBranch);
			return false;
		}

		// Token: 0x0600F36B RID: 62315 RVA: 0x00342754 File Offset: 0x00340954
		public SingleBranch? As_SingleBranch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleBranch)
			{
				return null;
			}
			return new SingleBranch?(SingleBranch.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F36C RID: 62316 RVA: 0x00342794 File Offset: 0x00340994
		public SingleBranch Cast_SingleBranch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleBranch)
			{
				return SingleBranch.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleBranch is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F36D RID: 62317 RVA: 0x003427E9 File Offset: 0x003409E9
		public bool Is_switch_ite(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.switch_ite;
		}

		// Token: 0x0600F36E RID: 62318 RVA: 0x00342803 File Offset: 0x00340A03
		public bool Is_switch_ite(GrammarBuilders g, out switch_ite value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.switch_ite)
			{
				value = switch_ite.CreateUnsafe(this.Node);
				return true;
			}
			value = default(switch_ite);
			return false;
		}

		// Token: 0x0600F36F RID: 62319 RVA: 0x00342838 File Offset: 0x00340A38
		public switch_ite? As_switch_ite(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.switch_ite)
			{
				return null;
			}
			return new switch_ite?(switch_ite.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F370 RID: 62320 RVA: 0x00342878 File Offset: 0x00340A78
		public switch_ite Cast_switch_ite(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.switch_ite)
			{
				return switch_ite.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_switch_ite is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F371 RID: 62321 RVA: 0x003428D0 File Offset: 0x00340AD0
		public T Switch<T>(GrammarBuilders g, Func<SingleBranch, T> func0, Func<switch_ite, T> func1)
		{
			SingleBranch singleBranch;
			if (this.Is_SingleBranch(g, out singleBranch))
			{
				return func0(singleBranch);
			}
			switch_ite switch_ite;
			if (this.Is_switch_ite(g, out switch_ite))
			{
				return func1(switch_ite);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol switch");
		}

		// Token: 0x0600F372 RID: 62322 RVA: 0x00342928 File Offset: 0x00340B28
		public void Switch(GrammarBuilders g, Action<SingleBranch> func0, Action<switch_ite> func1)
		{
			SingleBranch singleBranch;
			if (this.Is_SingleBranch(g, out singleBranch))
			{
				func0(singleBranch);
				return;
			}
			switch_ite switch_ite;
			if (this.Is_switch_ite(g, out switch_ite))
			{
				func1(switch_ite);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol switch");
		}

		// Token: 0x0600F373 RID: 62323 RVA: 0x0034297F File Offset: 0x00340B7F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F374 RID: 62324 RVA: 0x00342994 File Offset: 0x00340B94
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F375 RID: 62325 RVA: 0x003429BE File Offset: 0x00340BBE
		public bool Equals(@switch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B26 RID: 23334
		private ProgramNode _node;
	}
}
