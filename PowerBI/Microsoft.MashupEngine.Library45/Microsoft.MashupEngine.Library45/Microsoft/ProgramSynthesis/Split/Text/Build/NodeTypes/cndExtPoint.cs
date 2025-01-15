using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001364 RID: 4964
	public struct cndExtPoint : IProgramNodeBuilder, IEquatable<cndExtPoint>
	{
		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06009981 RID: 39297 RVA: 0x00208D9A File Offset: 0x00206F9A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009982 RID: 39298 RVA: 0x00208DA2 File Offset: 0x00206FA2
		private cndExtPoint(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009983 RID: 39299 RVA: 0x00208DAB File Offset: 0x00206FAB
		public static cndExtPoint CreateUnsafe(ProgramNode node)
		{
			return new cndExtPoint(node);
		}

		// Token: 0x06009984 RID: 39300 RVA: 0x00208DB4 File Offset: 0x00206FB4
		public static cndExtPoint? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.cndExtPoint)
			{
				return null;
			}
			return new cndExtPoint?(cndExtPoint.CreateUnsafe(node));
		}

		// Token: 0x06009985 RID: 39301 RVA: 0x00208DEE File Offset: 0x00206FEE
		public static cndExtPoint CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new cndExtPoint(new Hole(g.Symbol.cndExtPoint, holeId));
		}

		// Token: 0x06009986 RID: 39302 RVA: 0x00208E06 File Offset: 0x00207006
		public bool Is_cndExtPoint_extPoint(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.cndExtPoint_extPoint;
		}

		// Token: 0x06009987 RID: 39303 RVA: 0x00208E20 File Offset: 0x00207020
		public bool Is_cndExtPoint_extPoint(GrammarBuilders g, out cndExtPoint_extPoint value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.cndExtPoint_extPoint)
			{
				value = cndExtPoint_extPoint.CreateUnsafe(this.Node);
				return true;
			}
			value = default(cndExtPoint_extPoint);
			return false;
		}

		// Token: 0x06009988 RID: 39304 RVA: 0x00208E58 File Offset: 0x00207058
		public cndExtPoint_extPoint? As_cndExtPoint_extPoint(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.cndExtPoint_extPoint)
			{
				return null;
			}
			return new cndExtPoint_extPoint?(cndExtPoint_extPoint.CreateUnsafe(this.Node));
		}

		// Token: 0x06009989 RID: 39305 RVA: 0x00208E98 File Offset: 0x00207098
		public cndExtPoint_extPoint Cast_cndExtPoint_extPoint(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.cndExtPoint_extPoint)
			{
				return cndExtPoint_extPoint.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_cndExtPoint_extPoint is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600998A RID: 39306 RVA: 0x00208EED File Offset: 0x002070ED
		public bool Is_ConditionalExtract(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConditionalExtract;
		}

		// Token: 0x0600998B RID: 39307 RVA: 0x00208F07 File Offset: 0x00207107
		public bool Is_ConditionalExtract(GrammarBuilders g, out ConditionalExtract value)
		{
			if (this.Node.GrammarRule == g.Rule.ConditionalExtract)
			{
				value = ConditionalExtract.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConditionalExtract);
			return false;
		}

		// Token: 0x0600998C RID: 39308 RVA: 0x00208F3C File Offset: 0x0020713C
		public ConditionalExtract? As_ConditionalExtract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConditionalExtract)
			{
				return null;
			}
			return new ConditionalExtract?(ConditionalExtract.CreateUnsafe(this.Node));
		}

		// Token: 0x0600998D RID: 39309 RVA: 0x00208F7C File Offset: 0x0020717C
		public ConditionalExtract Cast_ConditionalExtract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConditionalExtract)
			{
				return ConditionalExtract.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConditionalExtract is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600998E RID: 39310 RVA: 0x00208FD4 File Offset: 0x002071D4
		public T Switch<T>(GrammarBuilders g, Func<cndExtPoint_extPoint, T> func0, Func<ConditionalExtract, T> func1)
		{
			cndExtPoint_extPoint cndExtPoint_extPoint;
			if (this.Is_cndExtPoint_extPoint(g, out cndExtPoint_extPoint))
			{
				return func0(cndExtPoint_extPoint);
			}
			ConditionalExtract conditionalExtract;
			if (this.Is_ConditionalExtract(g, out conditionalExtract))
			{
				return func1(conditionalExtract);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol cndExtPoint");
		}

		// Token: 0x0600998F RID: 39311 RVA: 0x0020902C File Offset: 0x0020722C
		public void Switch(GrammarBuilders g, Action<cndExtPoint_extPoint> func0, Action<ConditionalExtract> func1)
		{
			cndExtPoint_extPoint cndExtPoint_extPoint;
			if (this.Is_cndExtPoint_extPoint(g, out cndExtPoint_extPoint))
			{
				func0(cndExtPoint_extPoint);
				return;
			}
			ConditionalExtract conditionalExtract;
			if (this.Is_ConditionalExtract(g, out conditionalExtract))
			{
				func1(conditionalExtract);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol cndExtPoint");
		}

		// Token: 0x06009990 RID: 39312 RVA: 0x00209083 File Offset: 0x00207283
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009991 RID: 39313 RVA: 0x00209098 File Offset: 0x00207298
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009992 RID: 39314 RVA: 0x002090C2 File Offset: 0x002072C2
		public bool Equals(cndExtPoint other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDB RID: 15835
		private ProgramNode _node;
	}
}
