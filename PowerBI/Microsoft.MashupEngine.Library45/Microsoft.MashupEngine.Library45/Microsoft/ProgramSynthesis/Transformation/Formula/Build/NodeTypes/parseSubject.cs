using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BA RID: 5562
	public struct parseSubject : IProgramNodeBuilder, IEquatable<parseSubject>
	{
		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x0600B7EE RID: 47086 RVA: 0x0027DFB6 File Offset: 0x0027C1B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B7EF RID: 47087 RVA: 0x0027DFBE File Offset: 0x0027C1BE
		private parseSubject(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B7F0 RID: 47088 RVA: 0x0027DFC7 File Offset: 0x0027C1C7
		public static parseSubject CreateUnsafe(ProgramNode node)
		{
			return new parseSubject(node);
		}

		// Token: 0x0600B7F1 RID: 47089 RVA: 0x0027DFD0 File Offset: 0x0027C1D0
		public static parseSubject? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.parseSubject)
			{
				return null;
			}
			return new parseSubject?(parseSubject.CreateUnsafe(node));
		}

		// Token: 0x0600B7F2 RID: 47090 RVA: 0x0027E00A File Offset: 0x0027C20A
		public static parseSubject CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new parseSubject(new Hole(g.Symbol.parseSubject, holeId));
		}

		// Token: 0x0600B7F3 RID: 47091 RVA: 0x0027E022 File Offset: 0x0027C222
		public bool Is_parseSubject_fromStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.parseSubject_fromStr;
		}

		// Token: 0x0600B7F4 RID: 47092 RVA: 0x0027E03C File Offset: 0x0027C23C
		public bool Is_parseSubject_fromStr(GrammarBuilders g, out parseSubject_fromStr value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.parseSubject_fromStr)
			{
				value = parseSubject_fromStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(parseSubject_fromStr);
			return false;
		}

		// Token: 0x0600B7F5 RID: 47093 RVA: 0x0027E074 File Offset: 0x0027C274
		public parseSubject_fromStr? As_parseSubject_fromStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.parseSubject_fromStr)
			{
				return null;
			}
			return new parseSubject_fromStr?(parseSubject_fromStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7F6 RID: 47094 RVA: 0x0027E0B4 File Offset: 0x0027C2B4
		public parseSubject_fromStr Cast_parseSubject_fromStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.parseSubject_fromStr)
			{
				return parseSubject_fromStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_parseSubject_fromStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7F7 RID: 47095 RVA: 0x0027E109 File Offset: 0x0027C309
		public bool Is_parseSubject_letSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.parseSubject_letSubstring;
		}

		// Token: 0x0600B7F8 RID: 47096 RVA: 0x0027E123 File Offset: 0x0027C323
		public bool Is_parseSubject_letSubstring(GrammarBuilders g, out parseSubject_letSubstring value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.parseSubject_letSubstring)
			{
				value = parseSubject_letSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(parseSubject_letSubstring);
			return false;
		}

		// Token: 0x0600B7F9 RID: 47097 RVA: 0x0027E158 File Offset: 0x0027C358
		public parseSubject_letSubstring? As_parseSubject_letSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.parseSubject_letSubstring)
			{
				return null;
			}
			return new parseSubject_letSubstring?(parseSubject_letSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7FA RID: 47098 RVA: 0x0027E198 File Offset: 0x0027C398
		public parseSubject_letSubstring Cast_parseSubject_letSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.parseSubject_letSubstring)
			{
				return parseSubject_letSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_parseSubject_letSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7FB RID: 47099 RVA: 0x0027E1F0 File Offset: 0x0027C3F0
		public T Switch<T>(GrammarBuilders g, Func<parseSubject_fromStr, T> func0, Func<parseSubject_letSubstring, T> func1)
		{
			parseSubject_fromStr parseSubject_fromStr;
			if (this.Is_parseSubject_fromStr(g, out parseSubject_fromStr))
			{
				return func0(parseSubject_fromStr);
			}
			parseSubject_letSubstring parseSubject_letSubstring;
			if (this.Is_parseSubject_letSubstring(g, out parseSubject_letSubstring))
			{
				return func1(parseSubject_letSubstring);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol parseSubject");
		}

		// Token: 0x0600B7FC RID: 47100 RVA: 0x0027E248 File Offset: 0x0027C448
		public void Switch(GrammarBuilders g, Action<parseSubject_fromStr> func0, Action<parseSubject_letSubstring> func1)
		{
			parseSubject_fromStr parseSubject_fromStr;
			if (this.Is_parseSubject_fromStr(g, out parseSubject_fromStr))
			{
				func0(parseSubject_fromStr);
				return;
			}
			parseSubject_letSubstring parseSubject_letSubstring;
			if (this.Is_parseSubject_letSubstring(g, out parseSubject_letSubstring))
			{
				func1(parseSubject_letSubstring);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol parseSubject");
		}

		// Token: 0x0600B7FD RID: 47101 RVA: 0x0027E29F File Offset: 0x0027C49F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7FE RID: 47102 RVA: 0x0027E2B4 File Offset: 0x0027C4B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7FF RID: 47103 RVA: 0x0027E2DE File Offset: 0x0027C4DE
		public bool Equals(parseSubject other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004668 RID: 18024
		private ProgramNode _node;
	}
}
