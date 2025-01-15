using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000121 RID: 289
	internal class DataShapeDefinitionVisitor
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0000FCC2 File Offset: 0x0000DEC2
		internal virtual void Visit(DataShapeDefinition dataShapeDefinition)
		{
			this.Visit(dataShapeDefinition.DataShape);
			this.Visit(dataShapeDefinition.DataSource);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000FCDC File Offset: 0x0000DEDC
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

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000FD88 File Offset: 0x0000DF88
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

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000FE0D File Offset: 0x0000E00D
		internal virtual void Visit(DataIntersection dataIntersection)
		{
			DataShapeDefinitionVisitor.Visit<Calculation>(dataIntersection.Calculations, new Action<Calculation>(this.Visit));
			DataShapeDefinitionVisitor.Visit<DataShape>(dataIntersection.DataShapes, new Action<DataShape>(this.Visit));
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000FE3F File Offset: 0x0000E03F
		internal virtual void Visit(Calculation calculation)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000FE41 File Offset: 0x0000E041
		internal virtual void Visit(DataBinding dataBinding)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0000FE43 File Offset: 0x0000E043
		internal virtual void Visit(Group group)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0000FE45 File Offset: 0x0000E045
		internal virtual void Visit(DataSource dataSource)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0000FE47 File Offset: 0x0000E047
		internal virtual void Visit(DataWindow dataWindow)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0000FE49 File Offset: 0x0000E049
		internal virtual void Visit(DataLimits dataLimits)
		{
			if (dataLimits == null)
			{
				return;
			}
			this.Visit(dataLimits.DataBinding);
			DataShapeDefinitionVisitor.Visit<DataLimit>(dataLimits.Limits, new Action<DataLimit>(this.Visit));
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0000FE73 File Offset: 0x0000E073
		internal virtual void Visit(DataLimit dataLimit)
		{
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000FE75 File Offset: 0x0000E075
		internal virtual void Visit(MatchCondition matchCondition)
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000FE77 File Offset: 0x0000E077
		internal virtual void Visit(StartPosition startPosition)
		{
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000FE79 File Offset: 0x0000E079
		internal virtual void Visit(IList<ExpressionNode> restartDefinition)
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000FE7C File Offset: 0x0000E07C
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
