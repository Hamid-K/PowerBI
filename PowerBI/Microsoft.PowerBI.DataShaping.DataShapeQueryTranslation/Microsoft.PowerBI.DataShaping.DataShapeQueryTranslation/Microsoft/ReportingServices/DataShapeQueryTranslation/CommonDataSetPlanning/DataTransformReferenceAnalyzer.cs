using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x0200012A RID: 298
	internal sealed class DataTransformReferenceAnalyzer : DataShapeVisitor
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x0002BD4D File Offset: 0x00029F4D
		private DataTransformReferenceAnalyzer(ExpressionTable expressionTable)
		{
			this.m_expressionTable = expressionTable;
			this.m_referrersByColumn = new Dictionary<DataTransformTableColumn, DataTransformColumnReferrers>();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002BD67 File Offset: 0x00029F67
		public static DataTransformReferenceMap Analyze(DataShape dataShape, ExpressionTable expressionTable, DataShapeAnnotations dataShapeAnnotations)
		{
			if (!dataShapeAnnotations.HasOrContainsTransforms)
			{
				return DataTransformReferenceMap.Empty;
			}
			DataTransformReferenceAnalyzer dataTransformReferenceAnalyzer = new DataTransformReferenceAnalyzer(expressionTable);
			dataTransformReferenceAnalyzer.Visit(dataShape);
			return new DataTransformReferenceMap(dataTransformReferenceAnalyzer.m_referrersByColumn);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002BD90 File Offset: 0x00029F90
		protected override void Visit(DataTransformTableColumn column)
		{
			DataTransformColumnReferrers dataTransformColumnReferrers;
			this.CheckDataTransformColumnReference(column.Value, out dataTransformColumnReferrers);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002BDAC File Offset: 0x00029FAC
		protected override void Visit(Calculation calculation)
		{
			DataTransformColumnReferrers dataTransformColumnReferrers;
			if (this.CheckDataTransformColumnReference(calculation.Value, out dataTransformColumnReferrers))
			{
				dataTransformColumnReferrers.AddCalculation(calculation);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.Group == null)
			{
				return;
			}
			Group group = dataMember.Group;
			this.CheckDataTransformColumnReference(dataMember, group.GroupKeys);
			this.CheckDataTransformColumnReference(dataMember, group.SortKeys);
			this.CheckDataTransformColumnReference(dataMember, group.ScopeIdDefinition);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002BE14 File Offset: 0x0002A014
		private void CheckDataTransformColumnReference(DataMember member, IReadOnlyList<GroupKey> groupKeys)
		{
			if (groupKeys == null)
			{
				return;
			}
			foreach (GroupKey groupKey in groupKeys)
			{
				DataTransformColumnReferrers dataTransformColumnReferrers;
				if (this.CheckDataTransformColumnReference(groupKey.Value, out dataTransformColumnReferrers))
				{
					this.AddGroup(dataTransformColumnReferrers, member);
				}
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002BE74 File Offset: 0x0002A074
		private void CheckDataTransformColumnReference(DataMember member, IReadOnlyList<SortKey> sortKeys)
		{
			if (sortKeys == null)
			{
				return;
			}
			foreach (SortKey sortKey in sortKeys)
			{
				DataTransformColumnReferrers dataTransformColumnReferrers;
				if (this.CheckDataTransformColumnReference(sortKey.Value, out dataTransformColumnReferrers))
				{
					this.AddGroup(dataTransformColumnReferrers, member);
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		private void CheckDataTransformColumnReference(DataMember member, ScopeIdDefinition scopeIdDefn)
		{
			if (scopeIdDefn == null)
			{
				return;
			}
			foreach (ScopeValueDefinition scopeValueDefinition in scopeIdDefn.Values)
			{
				DataTransformColumnReferrers dataTransformColumnReferrers;
				if (this.CheckDataTransformColumnReference(scopeValueDefinition.Value, out dataTransformColumnReferrers))
				{
					this.AddGroup(dataTransformColumnReferrers, member);
				}
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002BF3C File Offset: 0x0002A13C
		private void AddGroup(DataTransformColumnReferrers referrers, DataMember member)
		{
			if (referrers.Groups == null || !referrers.Groups.Contains(member))
			{
				referrers.AddGroup(member);
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002BF5C File Offset: 0x0002A15C
		private bool CheckDataTransformColumnReference(Expression expression, out DataTransformColumnReferrers referrers)
		{
			DataTransformTableColumn dataTransformTableColumn;
			if (ExpressionAnalysisUtils.TryExtractDataTransformColumnReference(expression, this.m_expressionTable, out dataTransformTableColumn))
			{
				if (!this.m_referrersByColumn.TryGetValue(dataTransformTableColumn, out referrers))
				{
					referrers = new DataTransformColumnReferrers();
					this.m_referrersByColumn.Add(dataTransformTableColumn, referrers);
				}
				referrers.AddExpression(expression);
				return true;
			}
			referrers = null;
			return false;
		}

		// Token: 0x040005AB RID: 1451
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040005AC RID: 1452
		private readonly Dictionary<DataTransformTableColumn, DataTransformColumnReferrers> m_referrersByColumn;
	}
}
