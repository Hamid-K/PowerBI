using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B66 RID: 2918
	public struct @struct : IProgramNodeBuilder, IEquatable<@struct>
	{
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x000E87FE File Offset: 0x000E69FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x000E8806 File Offset: 0x000E6A06
		private @struct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x000E880F File Offset: 0x000E6A0F
		public static @struct CreateUnsafe(ProgramNode node)
		{
			return new @struct(node);
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x000E8818 File Offset: 0x000E6A18
		public static @struct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.@struct)
			{
				return null;
			}
			return new @struct?(@struct.CreateUnsafe(node));
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x000E8852 File Offset: 0x000E6A52
		public static @struct CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new @struct(new Hole(g.Symbol.@struct, holeId));
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x000E886A File Offset: 0x000E6A6A
		public bool Is_Struct(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Struct;
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x000E8884 File Offset: 0x000E6A84
		public bool Is_Struct(GrammarBuilders g, out Struct value)
		{
			if (this.Node.GrammarRule == g.Rule.Struct)
			{
				value = Struct.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Struct);
			return false;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x000E88BC File Offset: 0x000E6ABC
		public Struct? As_Struct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Struct)
			{
				return null;
			}
			return new Struct?(Struct.CreateUnsafe(this.Node));
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x000E88FC File Offset: 0x000E6AFC
		public Struct Cast_Struct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Struct)
			{
				return Struct.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Struct is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x000E8951 File Offset: 0x000E6B51
		public bool Is_Field(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Field;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x000E896B File Offset: 0x000E6B6B
		public bool Is_Field(GrammarBuilders g, out Field value)
		{
			if (this.Node.GrammarRule == g.Rule.Field)
			{
				value = Field.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Field);
			return false;
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x000E89A0 File Offset: 0x000E6BA0
		public Field? As_Field(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Field)
			{
				return null;
			}
			return new Field?(Field.CreateUnsafe(this.Node));
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x000E89E0 File Offset: 0x000E6BE0
		public Field Cast_Field(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Field)
			{
				return Field.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Field is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x000E8A38 File Offset: 0x000E6C38
		public T Switch<T>(GrammarBuilders g, Func<Struct, T> func0, Func<Field, T> func1)
		{
			Struct @struct;
			if (this.Is_Struct(g, out @struct))
			{
				return func0(@struct);
			}
			Field field;
			if (this.Is_Field(g, out field))
			{
				return func1(field);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol struct");
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x000E8A90 File Offset: 0x000E6C90
		public void Switch(GrammarBuilders g, Action<Struct> func0, Action<Field> func1)
		{
			Struct @struct;
			if (this.Is_Struct(g, out @struct))
			{
				func0(@struct);
				return;
			}
			Field field;
			if (this.Is_Field(g, out field))
			{
				func1(field);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol struct");
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x000E8AE7 File Offset: 0x000E6CE7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x000E8AFC File Offset: 0x000E6CFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x000E8B26 File Offset: 0x000E6D26
		public bool Equals(@struct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002161 RID: 8545
		private ProgramNode _node;
	}
}
