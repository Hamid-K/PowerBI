using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159E RID: 5534
	public struct outStr1 : IProgramNodeBuilder, IEquatable<outStr1>
	{
		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x0600B5A2 RID: 46498 RVA: 0x00277366 File Offset: 0x00275566
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B5A3 RID: 46499 RVA: 0x0027736E File Offset: 0x0027556E
		private outStr1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B5A4 RID: 46500 RVA: 0x00277377 File Offset: 0x00275577
		public static outStr1 CreateUnsafe(ProgramNode node)
		{
			return new outStr1(node);
		}

		// Token: 0x0600B5A5 RID: 46501 RVA: 0x00277380 File Offset: 0x00275580
		public static outStr1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.outStr1)
			{
				return null;
			}
			return new outStr1?(outStr1.CreateUnsafe(node));
		}

		// Token: 0x0600B5A6 RID: 46502 RVA: 0x002773BA File Offset: 0x002755BA
		public static outStr1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new outStr1(new Hole(g.Symbol.outStr1, holeId));
		}

		// Token: 0x0600B5A7 RID: 46503 RVA: 0x002773D2 File Offset: 0x002755D2
		public bool Is_outStr1_segmentCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outStr1_segmentCase;
		}

		// Token: 0x0600B5A8 RID: 46504 RVA: 0x002773EC File Offset: 0x002755EC
		public bool Is_outStr1_segmentCase(GrammarBuilders g, out outStr1_segmentCase value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_segmentCase)
			{
				value = outStr1_segmentCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outStr1_segmentCase);
			return false;
		}

		// Token: 0x0600B5A9 RID: 46505 RVA: 0x00277424 File Offset: 0x00275624
		public outStr1_segmentCase? As_outStr1_segmentCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outStr1_segmentCase)
			{
				return null;
			}
			return new outStr1_segmentCase?(outStr1_segmentCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5AA RID: 46506 RVA: 0x00277464 File Offset: 0x00275664
		public outStr1_segmentCase Cast_outStr1_segmentCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_segmentCase)
			{
				return outStr1_segmentCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outStr1_segmentCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5AB RID: 46507 RVA: 0x002774B9 File Offset: 0x002756B9
		public bool Is_outStr1_formatted(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outStr1_formatted;
		}

		// Token: 0x0600B5AC RID: 46508 RVA: 0x002774D3 File Offset: 0x002756D3
		public bool Is_outStr1_formatted(GrammarBuilders g, out outStr1_formatted value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_formatted)
			{
				value = outStr1_formatted.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outStr1_formatted);
			return false;
		}

		// Token: 0x0600B5AD RID: 46509 RVA: 0x00277508 File Offset: 0x00275708
		public outStr1_formatted? As_outStr1_formatted(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outStr1_formatted)
			{
				return null;
			}
			return new outStr1_formatted?(outStr1_formatted.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5AE RID: 46510 RVA: 0x00277548 File Offset: 0x00275748
		public outStr1_formatted Cast_outStr1_formatted(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_formatted)
			{
				return outStr1_formatted.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outStr1_formatted is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5AF RID: 46511 RVA: 0x0027759D File Offset: 0x0027579D
		public bool Is_outStr1_concatEntry(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outStr1_concatEntry;
		}

		// Token: 0x0600B5B0 RID: 46512 RVA: 0x002775B7 File Offset: 0x002757B7
		public bool Is_outStr1_concatEntry(GrammarBuilders g, out outStr1_concatEntry value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_concatEntry)
			{
				value = outStr1_concatEntry.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outStr1_concatEntry);
			return false;
		}

		// Token: 0x0600B5B1 RID: 46513 RVA: 0x002775EC File Offset: 0x002757EC
		public outStr1_concatEntry? As_outStr1_concatEntry(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outStr1_concatEntry)
			{
				return null;
			}
			return new outStr1_concatEntry?(outStr1_concatEntry.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5B2 RID: 46514 RVA: 0x0027762C File Offset: 0x0027582C
		public outStr1_concatEntry Cast_outStr1_concatEntry(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_concatEntry)
			{
				return outStr1_concatEntry.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outStr1_concatEntry is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5B3 RID: 46515 RVA: 0x00277681 File Offset: 0x00275881
		public bool Is_outStr1_constString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.outStr1_constString;
		}

		// Token: 0x0600B5B4 RID: 46516 RVA: 0x0027769B File Offset: 0x0027589B
		public bool Is_outStr1_constString(GrammarBuilders g, out outStr1_constString value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_constString)
			{
				value = outStr1_constString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(outStr1_constString);
			return false;
		}

		// Token: 0x0600B5B5 RID: 46517 RVA: 0x002776D0 File Offset: 0x002758D0
		public outStr1_constString? As_outStr1_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.outStr1_constString)
			{
				return null;
			}
			return new outStr1_constString?(outStr1_constString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5B6 RID: 46518 RVA: 0x00277710 File Offset: 0x00275910
		public outStr1_constString Cast_outStr1_constString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.outStr1_constString)
			{
				return outStr1_constString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_outStr1_constString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5B7 RID: 46519 RVA: 0x00277768 File Offset: 0x00275968
		public T Switch<T>(GrammarBuilders g, Func<outStr1_segmentCase, T> func0, Func<outStr1_formatted, T> func1, Func<outStr1_concatEntry, T> func2, Func<outStr1_constString, T> func3)
		{
			outStr1_segmentCase outStr1_segmentCase;
			if (this.Is_outStr1_segmentCase(g, out outStr1_segmentCase))
			{
				return func0(outStr1_segmentCase);
			}
			outStr1_formatted outStr1_formatted;
			if (this.Is_outStr1_formatted(g, out outStr1_formatted))
			{
				return func1(outStr1_formatted);
			}
			outStr1_concatEntry outStr1_concatEntry;
			if (this.Is_outStr1_concatEntry(g, out outStr1_concatEntry))
			{
				return func2(outStr1_concatEntry);
			}
			outStr1_constString outStr1_constString;
			if (this.Is_outStr1_constString(g, out outStr1_constString))
			{
				return func3(outStr1_constString);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outStr1");
		}

		// Token: 0x0600B5B8 RID: 46520 RVA: 0x002777E8 File Offset: 0x002759E8
		public void Switch(GrammarBuilders g, Action<outStr1_segmentCase> func0, Action<outStr1_formatted> func1, Action<outStr1_concatEntry> func2, Action<outStr1_constString> func3)
		{
			outStr1_segmentCase outStr1_segmentCase;
			if (this.Is_outStr1_segmentCase(g, out outStr1_segmentCase))
			{
				func0(outStr1_segmentCase);
				return;
			}
			outStr1_formatted outStr1_formatted;
			if (this.Is_outStr1_formatted(g, out outStr1_formatted))
			{
				func1(outStr1_formatted);
				return;
			}
			outStr1_concatEntry outStr1_concatEntry;
			if (this.Is_outStr1_concatEntry(g, out outStr1_concatEntry))
			{
				func2(outStr1_concatEntry);
				return;
			}
			outStr1_constString outStr1_constString;
			if (this.Is_outStr1_constString(g, out outStr1_constString))
			{
				func3(outStr1_constString);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol outStr1");
		}

		// Token: 0x0600B5B9 RID: 46521 RVA: 0x00277867 File Offset: 0x00275A67
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B5BA RID: 46522 RVA: 0x0027787C File Offset: 0x00275A7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B5BB RID: 46523 RVA: 0x002778A6 File Offset: 0x00275AA6
		public bool Equals(outStr1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464C RID: 17996
		private ProgramNode _node;
	}
}
