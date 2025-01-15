using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200105B RID: 4187
	public struct resultSequence : IProgramNodeBuilder, IEquatable<resultSequence>
	{
		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06007C55 RID: 31829 RVA: 0x001A49B2 File Offset: 0x001A2BB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C56 RID: 31830 RVA: 0x001A49BA File Offset: 0x001A2BBA
		private resultSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C57 RID: 31831 RVA: 0x001A49C3 File Offset: 0x001A2BC3
		public static resultSequence CreateUnsafe(ProgramNode node)
		{
			return new resultSequence(node);
		}

		// Token: 0x06007C58 RID: 31832 RVA: 0x001A49CC File Offset: 0x001A2BCC
		public static resultSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.resultSequence)
			{
				return null;
			}
			return new resultSequence?(resultSequence.CreateUnsafe(node));
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x001A4A06 File Offset: 0x001A2C06
		public static resultSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new resultSequence(new Hole(g.Symbol.resultSequence, holeId));
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x001A4A1E File Offset: 0x001A2C1E
		public bool Is_resultSequence_subNodeSequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.resultSequence_subNodeSequence;
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x001A4A38 File Offset: 0x001A2C38
		public bool Is_resultSequence_subNodeSequence(GrammarBuilders g, out resultSequence_subNodeSequence value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultSequence_subNodeSequence)
			{
				value = resultSequence_subNodeSequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(resultSequence_subNodeSequence);
			return false;
		}

		// Token: 0x06007C5C RID: 31836 RVA: 0x001A4A70 File Offset: 0x001A2C70
		public resultSequence_subNodeSequence? As_resultSequence_subNodeSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.resultSequence_subNodeSequence)
			{
				return null;
			}
			return new resultSequence_subNodeSequence?(resultSequence_subNodeSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x001A4AB0 File Offset: 0x001A2CB0
		public resultSequence_subNodeSequence Cast_resultSequence_subNodeSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultSequence_subNodeSequence)
			{
				return resultSequence_subNodeSequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_resultSequence_subNodeSequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C5E RID: 31838 RVA: 0x001A4B05 File Offset: 0x001A2D05
		public bool Is_resultSequence_regionSequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.resultSequence_regionSequence;
		}

		// Token: 0x06007C5F RID: 31839 RVA: 0x001A4B1F File Offset: 0x001A2D1F
		public bool Is_resultSequence_regionSequence(GrammarBuilders g, out resultSequence_regionSequence value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultSequence_regionSequence)
			{
				value = resultSequence_regionSequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(resultSequence_regionSequence);
			return false;
		}

		// Token: 0x06007C60 RID: 31840 RVA: 0x001A4B54 File Offset: 0x001A2D54
		public resultSequence_regionSequence? As_resultSequence_regionSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.resultSequence_regionSequence)
			{
				return null;
			}
			return new resultSequence_regionSequence?(resultSequence_regionSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x001A4B94 File Offset: 0x001A2D94
		public resultSequence_regionSequence Cast_resultSequence_regionSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultSequence_regionSequence)
			{
				return resultSequence_regionSequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_resultSequence_regionSequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x001A4BE9 File Offset: 0x001A2DE9
		public bool Is_ConvertToWebRegions(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConvertToWebRegions;
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x001A4C03 File Offset: 0x001A2E03
		public bool Is_ConvertToWebRegions(GrammarBuilders g, out ConvertToWebRegions value)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertToWebRegions)
			{
				value = ConvertToWebRegions.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConvertToWebRegions);
			return false;
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x001A4C38 File Offset: 0x001A2E38
		public ConvertToWebRegions? As_ConvertToWebRegions(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConvertToWebRegions)
			{
				return null;
			}
			return new ConvertToWebRegions?(ConvertToWebRegions.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x001A4C78 File Offset: 0x001A2E78
		public ConvertToWebRegions Cast_ConvertToWebRegions(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertToWebRegions)
			{
				return ConvertToWebRegions.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConvertToWebRegions is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C66 RID: 31846 RVA: 0x001A4CCD File Offset: 0x001A2ECD
		public bool Is_Union(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Union;
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x001A4CE7 File Offset: 0x001A2EE7
		public bool Is_Union(GrammarBuilders g, out Union value)
		{
			if (this.Node.GrammarRule == g.Rule.Union)
			{
				value = Union.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Union);
			return false;
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x001A4D1C File Offset: 0x001A2F1C
		public Union? As_Union(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Union)
			{
				return null;
			}
			return new Union?(Union.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x001A4D5C File Offset: 0x001A2F5C
		public Union Cast_Union(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Union)
			{
				return Union.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Union is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x001A4DB1 File Offset: 0x001A2FB1
		public bool Is_EmptySequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EmptySequence;
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x001A4DCB File Offset: 0x001A2FCB
		public bool Is_EmptySequence(GrammarBuilders g, out EmptySequence value)
		{
			if (this.Node.GrammarRule == g.Rule.EmptySequence)
			{
				value = EmptySequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EmptySequence);
			return false;
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x001A4E00 File Offset: 0x001A3000
		public EmptySequence? As_EmptySequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EmptySequence)
			{
				return null;
			}
			return new EmptySequence?(EmptySequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x001A4E40 File Offset: 0x001A3040
		public EmptySequence Cast_EmptySequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EmptySequence)
			{
				return EmptySequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EmptySequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x001A4E98 File Offset: 0x001A3098
		public T Switch<T>(GrammarBuilders g, Func<resultSequence_subNodeSequence, T> func0, Func<resultSequence_regionSequence, T> func1, Func<ConvertToWebRegions, T> func2, Func<Union, T> func3, Func<EmptySequence, T> func4)
		{
			resultSequence_subNodeSequence resultSequence_subNodeSequence;
			if (this.Is_resultSequence_subNodeSequence(g, out resultSequence_subNodeSequence))
			{
				return func0(resultSequence_subNodeSequence);
			}
			resultSequence_regionSequence resultSequence_regionSequence;
			if (this.Is_resultSequence_regionSequence(g, out resultSequence_regionSequence))
			{
				return func1(resultSequence_regionSequence);
			}
			ConvertToWebRegions convertToWebRegions;
			if (this.Is_ConvertToWebRegions(g, out convertToWebRegions))
			{
				return func2(convertToWebRegions);
			}
			Union union;
			if (this.Is_Union(g, out union))
			{
				return func3(union);
			}
			EmptySequence emptySequence;
			if (this.Is_EmptySequence(g, out emptySequence))
			{
				return func4(emptySequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultSequence");
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x001A4F2C File Offset: 0x001A312C
		public void Switch(GrammarBuilders g, Action<resultSequence_subNodeSequence> func0, Action<resultSequence_regionSequence> func1, Action<ConvertToWebRegions> func2, Action<Union> func3, Action<EmptySequence> func4)
		{
			resultSequence_subNodeSequence resultSequence_subNodeSequence;
			if (this.Is_resultSequence_subNodeSequence(g, out resultSequence_subNodeSequence))
			{
				func0(resultSequence_subNodeSequence);
				return;
			}
			resultSequence_regionSequence resultSequence_regionSequence;
			if (this.Is_resultSequence_regionSequence(g, out resultSequence_regionSequence))
			{
				func1(resultSequence_regionSequence);
				return;
			}
			ConvertToWebRegions convertToWebRegions;
			if (this.Is_ConvertToWebRegions(g, out convertToWebRegions))
			{
				func2(convertToWebRegions);
				return;
			}
			Union union;
			if (this.Is_Union(g, out union))
			{
				func3(union);
				return;
			}
			EmptySequence emptySequence;
			if (this.Is_EmptySequence(g, out emptySequence))
			{
				func4(emptySequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultSequence");
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x001A4FC0 File Offset: 0x001A31C0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x001A4FD4 File Offset: 0x001A31D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x001A4FFE File Offset: 0x001A31FE
		public bool Equals(resultSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003374 RID: 13172
		private ProgramNode _node;
	}
}
