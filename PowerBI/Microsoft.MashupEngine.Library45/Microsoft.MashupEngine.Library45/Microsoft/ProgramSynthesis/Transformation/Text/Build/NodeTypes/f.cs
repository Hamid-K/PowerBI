using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3C RID: 7228
	public struct f : IProgramNodeBuilder, IEquatable<f>
	{
		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x0600F3AC RID: 62380 RVA: 0x00342FDE File Offset: 0x003411DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F3AD RID: 62381 RVA: 0x00342FE6 File Offset: 0x003411E6
		private f(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F3AE RID: 62382 RVA: 0x00342FEF File Offset: 0x003411EF
		public static f CreateUnsafe(ProgramNode node)
		{
			return new f(node);
		}

		// Token: 0x0600F3AF RID: 62383 RVA: 0x00342FF8 File Offset: 0x003411F8
		public static f? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.f)
			{
				return null;
			}
			return new f?(f.CreateUnsafe(node));
		}

		// Token: 0x0600F3B0 RID: 62384 RVA: 0x00343032 File Offset: 0x00341232
		public static f CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new f(new Hole(g.Symbol.f, holeId));
		}

		// Token: 0x0600F3B1 RID: 62385 RVA: 0x0034304A File Offset: 0x0034124A
		public bool Is_ConstStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstStr;
		}

		// Token: 0x0600F3B2 RID: 62386 RVA: 0x00343064 File Offset: 0x00341264
		public bool Is_ConstStr(GrammarBuilders g, out ConstStr value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStr)
			{
				value = ConstStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstStr);
			return false;
		}

		// Token: 0x0600F3B3 RID: 62387 RVA: 0x0034309C File Offset: 0x0034129C
		public ConstStr? As_ConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstStr)
			{
				return null;
			}
			return new ConstStr?(ConstStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3B4 RID: 62388 RVA: 0x003430DC File Offset: 0x003412DC
		public ConstStr Cast_ConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStr)
			{
				return ConstStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3B5 RID: 62389 RVA: 0x00343131 File Offset: 0x00341331
		public bool Is_LetColumnName(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetColumnName;
		}

		// Token: 0x0600F3B6 RID: 62390 RVA: 0x0034314B File Offset: 0x0034134B
		public bool Is_LetColumnName(GrammarBuilders g, out LetColumnName value)
		{
			if (this.Node.GrammarRule == g.Rule.LetColumnName)
			{
				value = LetColumnName.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetColumnName);
			return false;
		}

		// Token: 0x0600F3B7 RID: 62391 RVA: 0x00343180 File Offset: 0x00341380
		public LetColumnName? As_LetColumnName(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetColumnName)
			{
				return null;
			}
			return new LetColumnName?(LetColumnName.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3B8 RID: 62392 RVA: 0x003431C0 File Offset: 0x003413C0
		public LetColumnName Cast_LetColumnName(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetColumnName)
			{
				return LetColumnName.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetColumnName is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3B9 RID: 62393 RVA: 0x00343218 File Offset: 0x00341418
		public T Switch<T>(GrammarBuilders g, Func<ConstStr, T> func0, Func<LetColumnName, T> func1)
		{
			ConstStr constStr;
			if (this.Is_ConstStr(g, out constStr))
			{
				return func0(constStr);
			}
			LetColumnName letColumnName;
			if (this.Is_LetColumnName(g, out letColumnName))
			{
				return func1(letColumnName);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol f");
		}

		// Token: 0x0600F3BA RID: 62394 RVA: 0x00343270 File Offset: 0x00341470
		public void Switch(GrammarBuilders g, Action<ConstStr> func0, Action<LetColumnName> func1)
		{
			ConstStr constStr;
			if (this.Is_ConstStr(g, out constStr))
			{
				func0(constStr);
				return;
			}
			LetColumnName letColumnName;
			if (this.Is_LetColumnName(g, out letColumnName))
			{
				func1(letColumnName);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol f");
		}

		// Token: 0x0600F3BB RID: 62395 RVA: 0x003432C7 File Offset: 0x003414C7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3BC RID: 62396 RVA: 0x003432DC File Offset: 0x003414DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3BD RID: 62397 RVA: 0x00343306 File Offset: 0x00341506
		public bool Equals(f other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2B RID: 23339
		private ProgramNode _node;
	}
}
