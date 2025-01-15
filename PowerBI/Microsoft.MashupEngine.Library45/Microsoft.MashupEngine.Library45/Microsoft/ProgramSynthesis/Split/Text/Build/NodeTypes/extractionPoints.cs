using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001363 RID: 4963
	public struct extractionPoints : IProgramNodeBuilder, IEquatable<extractionPoints>
	{
		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x0600996F RID: 39279 RVA: 0x00208A5E File Offset: 0x00206C5E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009970 RID: 39280 RVA: 0x00208A66 File Offset: 0x00206C66
		private extractionPoints(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009971 RID: 39281 RVA: 0x00208A6F File Offset: 0x00206C6F
		public static extractionPoints CreateUnsafe(ProgramNode node)
		{
			return new extractionPoints(node);
		}

		// Token: 0x06009972 RID: 39282 RVA: 0x00208A78 File Offset: 0x00206C78
		public static extractionPoints? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.extractionPoints)
			{
				return null;
			}
			return new extractionPoints?(extractionPoints.CreateUnsafe(node));
		}

		// Token: 0x06009973 RID: 39283 RVA: 0x00208AB2 File Offset: 0x00206CB2
		public static extractionPoints CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new extractionPoints(new Hole(g.Symbol.extractionPoints, holeId));
		}

		// Token: 0x06009974 RID: 39284 RVA: 0x00208ACA File Offset: 0x00206CCA
		public bool Is_ExtPointsList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ExtPointsList;
		}

		// Token: 0x06009975 RID: 39285 RVA: 0x00208AE4 File Offset: 0x00206CE4
		public bool Is_ExtPointsList(GrammarBuilders g, out ExtPointsList value)
		{
			if (this.Node.GrammarRule == g.Rule.ExtPointsList)
			{
				value = ExtPointsList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ExtPointsList);
			return false;
		}

		// Token: 0x06009976 RID: 39286 RVA: 0x00208B1C File Offset: 0x00206D1C
		public ExtPointsList? As_ExtPointsList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ExtPointsList)
			{
				return null;
			}
			return new ExtPointsList?(ExtPointsList.CreateUnsafe(this.Node));
		}

		// Token: 0x06009977 RID: 39287 RVA: 0x00208B5C File Offset: 0x00206D5C
		public ExtPointsList Cast_ExtPointsList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ExtPointsList)
			{
				return ExtPointsList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ExtPointsList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009978 RID: 39288 RVA: 0x00208BB1 File Offset: 0x00206DB1
		public bool Is_EmptyExtPointsList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EmptyExtPointsList;
		}

		// Token: 0x06009979 RID: 39289 RVA: 0x00208BCB File Offset: 0x00206DCB
		public bool Is_EmptyExtPointsList(GrammarBuilders g, out EmptyExtPointsList value)
		{
			if (this.Node.GrammarRule == g.Rule.EmptyExtPointsList)
			{
				value = EmptyExtPointsList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EmptyExtPointsList);
			return false;
		}

		// Token: 0x0600997A RID: 39290 RVA: 0x00208C00 File Offset: 0x00206E00
		public EmptyExtPointsList? As_EmptyExtPointsList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EmptyExtPointsList)
			{
				return null;
			}
			return new EmptyExtPointsList?(EmptyExtPointsList.CreateUnsafe(this.Node));
		}

		// Token: 0x0600997B RID: 39291 RVA: 0x00208C40 File Offset: 0x00206E40
		public EmptyExtPointsList Cast_EmptyExtPointsList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EmptyExtPointsList)
			{
				return EmptyExtPointsList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EmptyExtPointsList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600997C RID: 39292 RVA: 0x00208C98 File Offset: 0x00206E98
		public T Switch<T>(GrammarBuilders g, Func<ExtPointsList, T> func0, Func<EmptyExtPointsList, T> func1)
		{
			ExtPointsList extPointsList;
			if (this.Is_ExtPointsList(g, out extPointsList))
			{
				return func0(extPointsList);
			}
			EmptyExtPointsList emptyExtPointsList;
			if (this.Is_EmptyExtPointsList(g, out emptyExtPointsList))
			{
				return func1(emptyExtPointsList);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol extractionPoints");
		}

		// Token: 0x0600997D RID: 39293 RVA: 0x00208CF0 File Offset: 0x00206EF0
		public void Switch(GrammarBuilders g, Action<ExtPointsList> func0, Action<EmptyExtPointsList> func1)
		{
			ExtPointsList extPointsList;
			if (this.Is_ExtPointsList(g, out extPointsList))
			{
				func0(extPointsList);
				return;
			}
			EmptyExtPointsList emptyExtPointsList;
			if (this.Is_EmptyExtPointsList(g, out emptyExtPointsList))
			{
				func1(emptyExtPointsList);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol extractionPoints");
		}

		// Token: 0x0600997E RID: 39294 RVA: 0x00208D47 File Offset: 0x00206F47
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600997F RID: 39295 RVA: 0x00208D5C File Offset: 0x00206F5C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009980 RID: 39296 RVA: 0x00208D86 File Offset: 0x00206F86
		public bool Equals(extractionPoints other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDA RID: 15834
		private ProgramNode _node;
	}
}
