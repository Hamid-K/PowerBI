using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159D RID: 5533
	public struct outStr : IProgramNodeBuilder, IEquatable<outStr>
	{
		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x0600B590 RID: 46480 RVA: 0x0027702A File Offset: 0x0027522A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B591 RID: 46481 RVA: 0x00277032 File Offset: 0x00275232
		private outStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B592 RID: 46482 RVA: 0x0027703B File Offset: 0x0027523B
		public static outStr CreateUnsafe(ProgramNode node)
		{
			return new outStr(node);
		}

		// Token: 0x0600B593 RID: 46483 RVA: 0x00277044 File Offset: 0x00275244
		public static outStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.outStr)
			{
				return null;
			}
			return new outStr?(outStr.CreateUnsafe(node));
		}

		// Token: 0x0600B594 RID: 46484 RVA: 0x0027707E File Offset: 0x0027527E
		public static outStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new outStr(new Hole(g.Symbol.outStr, holeId));
		}

		// Token: 0x0600B595 RID: 46485 RVA: 0x00277096 File Offset: 0x00275296
		public bool Is_outStr_outStr1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outStr_outStr1;
		}

		// Token: 0x0600B596 RID: 46486 RVA: 0x002770B0 File Offset: 0x002752B0
		public bool Is_outStr_outStr1(GrammarBuilders g, out outStr_outStr1 value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr_outStr1)
			{
				value = outStr_outStr1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outStr_outStr1);
			return false;
		}

		// Token: 0x0600B597 RID: 46487 RVA: 0x002770E8 File Offset: 0x002752E8
		public outStr_outStr1? As_outStr_outStr1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outStr_outStr1)
			{
				return null;
			}
			return new outStr_outStr1?(outStr_outStr1.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B598 RID: 46488 RVA: 0x00277128 File Offset: 0x00275328
		public outStr_outStr1 Cast_outStr_outStr1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr_outStr1)
			{
				return outStr_outStr1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outStr_outStr1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B599 RID: 46489 RVA: 0x0027717D File Offset: 0x0027537D
		public bool Is_Replace(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Replace;
		}

		// Token: 0x0600B59A RID: 46490 RVA: 0x00277197 File Offset: 0x00275397
		public bool Is_Replace(GrammarBuilders g, out Replace value)
		{
			if (this.Node.GrammarRule == g.Rule.Replace)
			{
				value = Replace.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Replace);
			return false;
		}

		// Token: 0x0600B59B RID: 46491 RVA: 0x002771CC File Offset: 0x002753CC
		public Replace? As_Replace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Replace)
			{
				return null;
			}
			return new Replace?(Replace.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B59C RID: 46492 RVA: 0x0027720C File Offset: 0x0027540C
		public Replace Cast_Replace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Replace)
			{
				return Replace.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Replace is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B59D RID: 46493 RVA: 0x00277264 File Offset: 0x00275464
		public T Switch<T>(GrammarBuilders g, Func<outStr_outStr1, T> func0, Func<Replace, T> func1)
		{
			outStr_outStr1 outStr_outStr;
			if (this.Is_outStr_outStr1(g, out outStr_outStr))
			{
				return func0(outStr_outStr);
			}
			Replace replace;
			if (this.Is_Replace(g, out replace))
			{
				return func1(replace);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outStr");
		}

		// Token: 0x0600B59E RID: 46494 RVA: 0x002772BC File Offset: 0x002754BC
		public void Switch(GrammarBuilders g, Action<outStr_outStr1> func0, Action<Replace> func1)
		{
			outStr_outStr1 outStr_outStr;
			if (this.Is_outStr_outStr1(g, out outStr_outStr))
			{
				func0(outStr_outStr);
				return;
			}
			Replace replace;
			if (this.Is_Replace(g, out replace))
			{
				func1(replace);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outStr");
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x00277313 File Offset: 0x00275513
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B5A0 RID: 46496 RVA: 0x00277328 File Offset: 0x00275528
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B5A1 RID: 46497 RVA: 0x00277352 File Offset: 0x00275552
		public bool Equals(outStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464B RID: 17995
		private ProgramNode _node;
	}
}
