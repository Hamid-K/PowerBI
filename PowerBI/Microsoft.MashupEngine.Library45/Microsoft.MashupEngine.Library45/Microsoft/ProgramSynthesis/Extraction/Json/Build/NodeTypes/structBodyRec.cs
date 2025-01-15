using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B67 RID: 2919
	public struct structBodyRec : IProgramNodeBuilder, IEquatable<structBodyRec>
	{
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x000E8B3A File Offset: 0x000E6D3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x000E8B42 File Offset: 0x000E6D42
		private structBodyRec(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x000E8B4B File Offset: 0x000E6D4B
		public static structBodyRec CreateUnsafe(ProgramNode node)
		{
			return new structBodyRec(node);
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x000E8B54 File Offset: 0x000E6D54
		public static structBodyRec? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.structBodyRec)
			{
				return null;
			}
			return new structBodyRec?(structBodyRec.CreateUnsafe(node));
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x000E8B8E File Offset: 0x000E6D8E
		public static structBodyRec CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new structBodyRec(new Hole(g.Symbol.structBodyRec, holeId));
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000E8BA6 File Offset: 0x000E6DA6
		public bool Is_Concat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Concat;
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x000E8BC0 File Offset: 0x000E6DC0
		public bool Is_Concat(GrammarBuilders g, out Concat value)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				value = Concat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Concat);
			return false;
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x000E8BF8 File Offset: 0x000E6DF8
		public Concat? As_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(this.Node));
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x000E8C38 File Offset: 0x000E6E38
		public Concat Cast_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				return Concat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Concat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x000E8C8D File Offset: 0x000E6E8D
		public bool Is_ToList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToList;
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x000E8CA7 File Offset: 0x000E6EA7
		public bool Is_ToList(GrammarBuilders g, out ToList value)
		{
			if (this.Node.GrammarRule == g.Rule.ToList)
			{
				value = ToList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToList);
			return false;
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x000E8CDC File Offset: 0x000E6EDC
		public ToList? As_ToList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToList)
			{
				return null;
			}
			return new ToList?(ToList.CreateUnsafe(this.Node));
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x000E8D1C File Offset: 0x000E6F1C
		public ToList Cast_ToList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToList)
			{
				return ToList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x000E8D71 File Offset: 0x000E6F71
		public bool Is_Empty(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Empty;
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x000E8D8B File Offset: 0x000E6F8B
		public bool Is_Empty(GrammarBuilders g, out Empty value)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				value = Empty.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Empty);
			return false;
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x000E8DC0 File Offset: 0x000E6FC0
		public Empty? As_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(this.Node));
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x000E8E00 File Offset: 0x000E7000
		public Empty Cast_Empty(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Empty)
			{
				return Empty.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Empty is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x000E8E58 File Offset: 0x000E7058
		public T Switch<T>(GrammarBuilders g, Func<Concat, T> func0, Func<ToList, T> func1, Func<Empty, T> func2)
		{
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				return func0(concat);
			}
			ToList toList;
			if (this.Is_ToList(g, out toList))
			{
				return func1(toList);
			}
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				return func2(empty);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol structBodyRec");
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x000E8EC4 File Offset: 0x000E70C4
		public void Switch(GrammarBuilders g, Action<Concat> func0, Action<ToList> func1, Action<Empty> func2)
		{
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				func0(concat);
				return;
			}
			ToList toList;
			if (this.Is_ToList(g, out toList))
			{
				func1(toList);
				return;
			}
			Empty empty;
			if (this.Is_Empty(g, out empty))
			{
				func2(empty);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol structBodyRec");
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x000E8F2F File Offset: 0x000E712F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x000E8F44 File Offset: 0x000E7144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x000E8F6E File Offset: 0x000E716E
		public bool Equals(structBodyRec other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002162 RID: 8546
		private ProgramNode _node;
	}
}
