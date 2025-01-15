using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000622 RID: 1570
	internal class ToDecisionDiagramConverter<T_Identifier> : Visitor<T_Identifier, Vertex>
	{
		// Token: 0x06004BE6 RID: 19430 RVA: 0x0010B7C0 File Offset: 0x001099C0
		private ToDecisionDiagramConverter(ConversionContext<T_Identifier> context)
		{
			this._context = context;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0010B7D0 File Offset: 0x001099D0
		internal static Vertex TranslateToRobdd(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			ToDecisionDiagramConverter<T_Identifier> toDecisionDiagramConverter = new ToDecisionDiagramConverter<T_Identifier>(context);
			return expr.Accept<Vertex>(toDecisionDiagramConverter);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0010B7EB File Offset: 0x001099EB
		internal override Vertex VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return Vertex.One;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0010B7F2 File Offset: 0x001099F2
		internal override Vertex VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return Vertex.Zero;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0010B7F9 File Offset: 0x001099F9
		internal override Vertex VisitTerm(TermExpr<T_Identifier> expression)
		{
			return this._context.TranslateTermToVertex(expression);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0010B807 File Offset: 0x00109A07
		internal override Vertex VisitNot(NotExpr<T_Identifier> expression)
		{
			return this._context.Solver.Not(expression.Child.Accept<Vertex>(this));
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0010B825 File Offset: 0x00109A25
		internal override Vertex VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this._context.Solver.And(expression.Children.Select((BoolExpr<T_Identifier> child) => child.Accept<Vertex>(this)));
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0010B84E File Offset: 0x00109A4E
		internal override Vertex VisitOr(OrExpr<T_Identifier> expression)
		{
			return this._context.Solver.Or(expression.Children.Select((BoolExpr<T_Identifier> child) => child.Accept<Vertex>(this)));
		}

		// Token: 0x04001A82 RID: 6786
		private readonly ConversionContext<T_Identifier> _context;
	}
}
