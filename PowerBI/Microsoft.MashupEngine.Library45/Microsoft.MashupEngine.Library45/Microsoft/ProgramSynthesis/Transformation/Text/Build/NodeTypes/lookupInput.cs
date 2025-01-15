using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C40 RID: 7232
	public struct lookupInput : IProgramNodeBuilder, IEquatable<lookupInput>
	{
		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x0600F3EE RID: 62446 RVA: 0x00343A82 File Offset: 0x00341C82
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F3EF RID: 62447 RVA: 0x00343A8A File Offset: 0x00341C8A
		private lookupInput(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F3F0 RID: 62448 RVA: 0x00343A93 File Offset: 0x00341C93
		public static lookupInput CreateUnsafe(ProgramNode node)
		{
			return new lookupInput(node);
		}

		// Token: 0x0600F3F1 RID: 62449 RVA: 0x00343A9C File Offset: 0x00341C9C
		public static lookupInput? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.lookupInput)
			{
				return null;
			}
			return new lookupInput?(lookupInput.CreateUnsafe(node));
		}

		// Token: 0x0600F3F2 RID: 62450 RVA: 0x00343AD6 File Offset: 0x00341CD6
		public static lookupInput CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new lookupInput(new Hole(g.Symbol.lookupInput, holeId));
		}

		// Token: 0x0600F3F3 RID: 62451 RVA: 0x00343AEE File Offset: 0x00341CEE
		public bool Is_LookupInput(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LookupInput;
		}

		// Token: 0x0600F3F4 RID: 62452 RVA: 0x00343B08 File Offset: 0x00341D08
		public bool Is_LookupInput(GrammarBuilders g, out LookupInput value)
		{
			if (this.Node.GrammarRule == g.Rule.LookupInput)
			{
				value = LookupInput.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LookupInput);
			return false;
		}

		// Token: 0x0600F3F5 RID: 62453 RVA: 0x00343B40 File Offset: 0x00341D40
		public LookupInput? As_LookupInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LookupInput)
			{
				return null;
			}
			return new LookupInput?(LookupInput.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3F6 RID: 62454 RVA: 0x00343B80 File Offset: 0x00341D80
		public LookupInput Cast_LookupInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LookupInput)
			{
				return LookupInput.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LookupInput is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3F7 RID: 62455 RVA: 0x00343BD5 File Offset: 0x00341DD5
		public bool Is_lookupInput_indexInputString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.lookupInput_indexInputString;
		}

		// Token: 0x0600F3F8 RID: 62456 RVA: 0x00343BEF File Offset: 0x00341DEF
		public bool Is_lookupInput_indexInputString(GrammarBuilders g, out lookupInput_indexInputString value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.lookupInput_indexInputString)
			{
				value = lookupInput_indexInputString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(lookupInput_indexInputString);
			return false;
		}

		// Token: 0x0600F3F9 RID: 62457 RVA: 0x00343C24 File Offset: 0x00341E24
		public lookupInput_indexInputString? As_lookupInput_indexInputString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.lookupInput_indexInputString)
			{
				return null;
			}
			return new lookupInput_indexInputString?(lookupInput_indexInputString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3FA RID: 62458 RVA: 0x00343C64 File Offset: 0x00341E64
		public lookupInput_indexInputString Cast_lookupInput_indexInputString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.lookupInput_indexInputString)
			{
				return lookupInput_indexInputString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_lookupInput_indexInputString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3FB RID: 62459 RVA: 0x00343CBC File Offset: 0x00341EBC
		public T Switch<T>(GrammarBuilders g, Func<LookupInput, T> func0, Func<lookupInput_indexInputString, T> func1)
		{
			LookupInput lookupInput;
			if (this.Is_LookupInput(g, out lookupInput))
			{
				return func0(lookupInput);
			}
			lookupInput_indexInputString lookupInput_indexInputString;
			if (this.Is_lookupInput_indexInputString(g, out lookupInput_indexInputString))
			{
				return func1(lookupInput_indexInputString);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol lookupInput");
		}

		// Token: 0x0600F3FC RID: 62460 RVA: 0x00343D14 File Offset: 0x00341F14
		public void Switch(GrammarBuilders g, Action<LookupInput> func0, Action<lookupInput_indexInputString> func1)
		{
			LookupInput lookupInput;
			if (this.Is_LookupInput(g, out lookupInput))
			{
				func0(lookupInput);
				return;
			}
			lookupInput_indexInputString lookupInput_indexInputString;
			if (this.Is_lookupInput_indexInputString(g, out lookupInput_indexInputString))
			{
				func1(lookupInput_indexInputString);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol lookupInput");
		}

		// Token: 0x0600F3FD RID: 62461 RVA: 0x00343D6B File Offset: 0x00341F6B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3FE RID: 62462 RVA: 0x00343D80 File Offset: 0x00341F80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3FF RID: 62463 RVA: 0x00343DAA File Offset: 0x00341FAA
		public bool Equals(lookupInput other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2F RID: 23343
		private ProgramNode _node;
	}
}
