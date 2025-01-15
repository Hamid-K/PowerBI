using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000036 RID: 54
	internal class DataShapeDefinitionVisitor
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00005806 File Offset: 0x00003A06
		internal virtual void Visit(DataShapeDefinition dataShapeDefinition)
		{
			this.Visit(dataShapeDefinition.DataShape);
			this.Visit(dataShapeDefinition.DataSource);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005820 File Offset: 0x00003A20
		internal virtual void Visit(DataShape dataShape)
		{
			this.Visit(dataShape.DataBinding);
			DataShapeDefinitionVisitor.Visit<DataShape>(dataShape.DataShapes, new Action<DataShape>(this.Visit));
			DataShapeDefinitionVisitor.Visit<Calculation>(dataShape.Calculations, new Action<Calculation>(this.Visit));
			DataShapeDefinitionVisitor.Visit<DataMember>(dataShape.SecondaryHierarchy, new Action<DataMember>(this.Visit));
			DataShapeDefinitionVisitor.Visit<DataMember>(dataShape.PrimaryHierarchy, new Action<DataMember>(this.Visit));
			this.Visit(dataShape.DataWindow);
			this.Visit(dataShape.DataLimits);
			DataShapeDefinitionVisitor.Visit<IList<ExpressionNode>>(dataShape.RestartDefinitions, new Action<IList<ExpressionNode>>(this.Visit));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000058CC File Offset: 0x00003ACC
		internal virtual void Visit(DataMember dataMember)
		{
			this.Visit(dataMember.DataBinding);
			this.Visit(dataMember.MatchCondition);
			DataShapeDefinitionVisitor.Visit<DataMember>(dataMember.DataMembers, new Action<DataMember>(this.Visit));
			DataShapeDefinitionVisitor.Visit<Calculation>(dataMember.Calculations, new Action<Calculation>(this.Visit));
			this.Visit(dataMember.Group);
			DataShapeDefinitionVisitor.Visit<DataIntersection>(dataMember.Intersections, new Action<DataIntersection>(this.Visit));
			this.Visit(dataMember.StartPosition);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005951 File Offset: 0x00003B51
		internal virtual void Visit(DataIntersection dataIntersection)
		{
			DataShapeDefinitionVisitor.Visit<Calculation>(dataIntersection.Calculations, new Action<Calculation>(this.Visit));
			DataShapeDefinitionVisitor.Visit<DataShape>(dataIntersection.DataShapes, new Action<DataShape>(this.Visit));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005983 File Offset: 0x00003B83
		internal virtual void Visit(Calculation calculation)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005985 File Offset: 0x00003B85
		internal virtual void Visit(DataBinding dataBinding)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005987 File Offset: 0x00003B87
		internal virtual void Visit(Group group)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005989 File Offset: 0x00003B89
		internal virtual void Visit(DataSource dataSource)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000598B File Offset: 0x00003B8B
		internal virtual void Visit(DataWindow dataWindow)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000598D File Offset: 0x00003B8D
		internal virtual void Visit(DataLimits dataLimits)
		{
			if (dataLimits == null)
			{
				return;
			}
			this.Visit(dataLimits.Binding);
			DataShapeDefinitionVisitor.Visit<DataLimit>(dataLimits.Limits, new Action<DataLimit>(this.Visit));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000059B7 File Offset: 0x00003BB7
		internal virtual void Visit(DataLimit dataLimit)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000059B9 File Offset: 0x00003BB9
		internal virtual void Visit(MatchCondition matchCondition)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000059BB File Offset: 0x00003BBB
		internal virtual void Visit(StartPosition startPosition)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000059BD File Offset: 0x00003BBD
		internal virtual void Visit(IList<ExpressionNode> restartDefinition)
		{
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000059C0 File Offset: 0x00003BC0
		internal static void Visit<T>(IList<T> dsdItems, Action<T> visit)
		{
			if (dsdItems == null || dsdItems.Count == 0)
			{
				return;
			}
			foreach (T t in dsdItems)
			{
				visit(t);
			}
		}
	}
}
