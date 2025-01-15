using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A0 RID: 5536
	public struct segment : IProgramNodeBuilder, IEquatable<segment>
	{
		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x0600B5D6 RID: 46550 RVA: 0x00277E0E File Offset: 0x0027600E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B5D7 RID: 46551 RVA: 0x00277E16 File Offset: 0x00276016
		private segment(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B5D8 RID: 46552 RVA: 0x00277E1F File Offset: 0x0027601F
		public static segment CreateUnsafe(ProgramNode node)
		{
			return new segment(node);
		}

		// Token: 0x0600B5D9 RID: 46553 RVA: 0x00277E28 File Offset: 0x00276028
		public static segment? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.segment)
			{
				return null;
			}
			return new segment?(segment.CreateUnsafe(node));
		}

		// Token: 0x0600B5DA RID: 46554 RVA: 0x00277E62 File Offset: 0x00276062
		public static segment CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new segment(new Hole(g.Symbol.segment, holeId));
		}

		// Token: 0x0600B5DB RID: 46555 RVA: 0x00277E7A File Offset: 0x0027607A
		public bool Is_segment_fromStrTrim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.segment_fromStrTrim;
		}

		// Token: 0x0600B5DC RID: 46556 RVA: 0x00277E94 File Offset: 0x00276094
		public bool Is_segment_fromStrTrim(GrammarBuilders g, out segment_fromStrTrim value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segment_fromStrTrim)
			{
				value = segment_fromStrTrim.CreateUnsafe(this.Node);
				return true;
			}
			value = default(segment_fromStrTrim);
			return false;
		}

		// Token: 0x0600B5DD RID: 46557 RVA: 0x00277ECC File Offset: 0x002760CC
		public segment_fromStrTrim? As_segment_fromStrTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.segment_fromStrTrim)
			{
				return null;
			}
			return new segment_fromStrTrim?(segment_fromStrTrim.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5DE RID: 46558 RVA: 0x00277F0C File Offset: 0x0027610C
		public segment_fromStrTrim Cast_segment_fromStrTrim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segment_fromStrTrim)
			{
				return segment_fromStrTrim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_segment_fromStrTrim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5DF RID: 46559 RVA: 0x00277F61 File Offset: 0x00276161
		public bool Is_segment_letSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.segment_letSubstring;
		}

		// Token: 0x0600B5E0 RID: 46560 RVA: 0x00277F7B File Offset: 0x0027617B
		public bool Is_segment_letSubstring(GrammarBuilders g, out segment_letSubstring value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segment_letSubstring)
			{
				value = segment_letSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(segment_letSubstring);
			return false;
		}

		// Token: 0x0600B5E1 RID: 46561 RVA: 0x00277FB0 File Offset: 0x002761B0
		public segment_letSubstring? As_segment_letSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.segment_letSubstring)
			{
				return null;
			}
			return new segment_letSubstring?(segment_letSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5E2 RID: 46562 RVA: 0x00277FF0 File Offset: 0x002761F0
		public segment_letSubstring Cast_segment_letSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segment_letSubstring)
			{
				return segment_letSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_segment_letSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5E3 RID: 46563 RVA: 0x00278048 File Offset: 0x00276248
		public T Switch<T>(GrammarBuilders g, Func<segment_fromStrTrim, T> func0, Func<segment_letSubstring, T> func1)
		{
			segment_fromStrTrim segment_fromStrTrim;
			if (this.Is_segment_fromStrTrim(g, out segment_fromStrTrim))
			{
				return func0(segment_fromStrTrim);
			}
			segment_letSubstring segment_letSubstring;
			if (this.Is_segment_letSubstring(g, out segment_letSubstring))
			{
				return func1(segment_letSubstring);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol segment");
		}

		// Token: 0x0600B5E4 RID: 46564 RVA: 0x002780A0 File Offset: 0x002762A0
		public void Switch(GrammarBuilders g, Action<segment_fromStrTrim> func0, Action<segment_letSubstring> func1)
		{
			segment_fromStrTrim segment_fromStrTrim;
			if (this.Is_segment_fromStrTrim(g, out segment_fromStrTrim))
			{
				func0(segment_fromStrTrim);
				return;
			}
			segment_letSubstring segment_letSubstring;
			if (this.Is_segment_letSubstring(g, out segment_letSubstring))
			{
				func1(segment_letSubstring);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol segment");
		}

		// Token: 0x0600B5E5 RID: 46565 RVA: 0x002780F7 File Offset: 0x002762F7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B5E6 RID: 46566 RVA: 0x0027810C File Offset: 0x0027630C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B5E7 RID: 46567 RVA: 0x00278136 File Offset: 0x00276336
		public bool Equals(segment other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464E RID: 17998
		private ProgramNode _node;
	}
}
