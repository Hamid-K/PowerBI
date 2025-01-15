using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors
{
	// Token: 0x02001603 RID: 5635
	internal class ProgramExtractVisitor : ProgramNodeVisitor<IReadOnlyList<ProgramNodeDetail>>
	{
		// Token: 0x0600BB57 RID: 47959 RVA: 0x00284F8C File Offset: 0x0028318C
		private ProgramExtractVisitor(Func<ProgramNode, bool> predicate, bool includeConversionRules)
		{
			this._predicate = predicate;
			this._includeConversionRules = includeConversionRules;
		}

		// Token: 0x0600BB58 RID: 47960 RVA: 0x00284FB8 File Offset: 0x002831B8
		public static IEnumerable<ProgramNodeDetail> Extract(IProgram program, bool includeConversionRules = false)
		{
			return ProgramExtractVisitor.Extract(program.ProgramNode, (ProgramNode _) => true, includeConversionRules);
		}

		// Token: 0x0600BB59 RID: 47961 RVA: 0x00284FE5 File Offset: 0x002831E5
		public static IEnumerable<ProgramNodeDetail> Extract(ProgramNode node, bool includeConversionRules = false)
		{
			return ProgramExtractVisitor.Extract(node, (ProgramNode _) => true, includeConversionRules);
		}

		// Token: 0x0600BB5A RID: 47962 RVA: 0x0028500D File Offset: 0x0028320D
		public static IEnumerable<ProgramNodeDetail> Extract(IProgram program, Func<ProgramNode, bool> predicate, bool includeConversionRules = false)
		{
			return program.ProgramNode.AcceptVisitor<IReadOnlyList<ProgramNodeDetail>>(new ProgramExtractVisitor(predicate, includeConversionRules));
		}

		// Token: 0x0600BB5B RID: 47963 RVA: 0x00285021 File Offset: 0x00283221
		public static IEnumerable<ProgramNodeDetail> Extract(ProgramNode node, Func<ProgramNode, bool> predicate, bool includeConversionRules = false)
		{
			return node.AcceptVisitor<IReadOnlyList<ProgramNodeDetail>>(new ProgramExtractVisitor(predicate, includeConversionRules));
		}

		// Token: 0x0600BB5C RID: 47964 RVA: 0x00285030 File Offset: 0x00283230
		public static IEnumerable<ProgramNode> ExtractNodes(IProgram program, bool includeConversionRules = false)
		{
			return from e in ProgramExtractVisitor.Extract(program.ProgramNode, includeConversionRules)
				select e.Node;
		}

		// Token: 0x0600BB5D RID: 47965 RVA: 0x00285062 File Offset: 0x00283262
		public static IEnumerable<ProgramNode> ExtractNodes(ProgramNode node, bool includeConversionRules = false)
		{
			return from e in ProgramExtractVisitor.Extract(node, includeConversionRules)
				select e.Node;
		}

		// Token: 0x0600BB5E RID: 47966 RVA: 0x0028508F File Offset: 0x0028328F
		public static IEnumerable<ProgramNode> ExtractNodes(IProgram program, Func<ProgramNode, bool> predicate, bool includeConversionRules = false)
		{
			return from e in ProgramExtractVisitor.Extract(program, predicate, includeConversionRules)
				select e.Node;
		}

		// Token: 0x0600BB5F RID: 47967 RVA: 0x002850BD File Offset: 0x002832BD
		public static IEnumerable<ProgramNode> ExtractNodes(ProgramNode node, Func<ProgramNode, bool> predicate, bool includeConversionRules = false)
		{
			return from e in ProgramExtractVisitor.Extract(node, predicate, includeConversionRules)
				select e.Node;
		}

		// Token: 0x0600BB60 RID: 47968 RVA: 0x002850EC File Offset: 0x002832EC
		public IReadOnlyList<ProgramNodeDetail> Visit(ProgramNode node)
		{
			if (!this._includeConversionRules && node.GrammarRule is ConversionRule)
			{
				ProgramNode[] array = node.Children;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].AcceptVisitor<IReadOnlyList<ProgramNodeDetail>>(this);
				}
				return this._results;
			}
			this._order++;
			ProgramNodeDetail programNodeDetail = new ProgramNodeDetail(this._results, this._ancestors.ToList<ProgramNodeDetail>())
			{
				Parent = this._parent,
				Depth = this._depth,
				Order = this._order,
				Node = node
			};
			if (this._predicate(node))
			{
				this._results.Add(programNodeDetail);
			}
			if (!node.Children.Any<ProgramNode>())
			{
				return this._results;
			}
			this._depth++;
			this._ancestors.Insert(0, programNodeDetail);
			foreach (ProgramNode programNode in node.Children)
			{
				this._parent = programNodeDetail;
				programNode.AcceptVisitor<IReadOnlyList<ProgramNodeDetail>>(this);
			}
			this._ancestors.Remove(programNodeDetail);
			this._depth--;
			return this._results;
		}

		// Token: 0x0600BB61 RID: 47969 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitHole(Hole node)
		{
			return this.Visit(node);
		}

		// Token: 0x0600BB62 RID: 47970 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitLambda(LambdaNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x0600BB63 RID: 47971 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitLet(LetNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x0600BB64 RID: 47972 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitLiteral(LiteralNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x0600BB65 RID: 47973 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitNonterminal(NonterminalNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x0600BB66 RID: 47974 RVA: 0x00285214 File Offset: 0x00283414
		public override IReadOnlyList<ProgramNodeDetail> VisitVariable(VariableNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x040046DB RID: 18139
		private readonly IList<ProgramNodeDetail> _ancestors = new List<ProgramNodeDetail>();

		// Token: 0x040046DC RID: 18140
		private int _depth;

		// Token: 0x040046DD RID: 18141
		private readonly bool _includeConversionRules;

		// Token: 0x040046DE RID: 18142
		private int _order;

		// Token: 0x040046DF RID: 18143
		private ProgramNodeDetail _parent;

		// Token: 0x040046E0 RID: 18144
		private readonly Func<ProgramNode, bool> _predicate;

		// Token: 0x040046E1 RID: 18145
		private readonly List<ProgramNodeDetail> _results = new List<ProgramNodeDetail>();
	}
}
