using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200052A RID: 1322
	internal sealed class ScopeTreeBuilderForDataScopeDataSet : ScopeTreeBuilder
	{
		// Token: 0x06004757 RID: 18263 RVA: 0x0012AAEC File Offset: 0x00128CEC
		public static ScopeTree BuildScopeTree(Report report, ErrorContext errorContext)
		{
			ScopeTreeBuilderForDataScopeDataSet scopeTreeBuilderForDataScopeDataSet = new ScopeTreeBuilderForDataScopeDataSet(report, errorContext);
			report.TraverseScopes(scopeTreeBuilderForDataScopeDataSet);
			report.DataPipelineCount = scopeTreeBuilderForDataScopeDataSet.m_nextDataPipelineId;
			return scopeTreeBuilderForDataScopeDataSet.Tree;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0012AB1A File Offset: 0x00128D1A
		private ScopeTreeBuilderForDataScopeDataSet(Report report, ErrorContext errorContext)
			: base(report)
		{
			this.m_errorContext = errorContext;
			report.BindAndValidateDataSetDefaultRelationships(this.m_errorContext);
			this.m_nextDataPipelineId = report.DataSetCount;
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x0012AB42 File Offset: 0x00128D42
		public override void PreVisit(DataRegion dataRegion)
		{
			base.PreVisit(dataRegion);
			this.SetDataSetForScope(dataRegion);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x0012AB52 File Offset: 0x00128D52
		private void SetDataSetForScope(IRIFReportDataScope scope)
		{
			if (scope == null || scope.DataScopeInfo == null)
			{
				return;
			}
			scope.DataScopeInfo.ValidateDataSetBindingAndRelationships(this.m_tree, scope, this.m_errorContext);
			this.DetermineDataPipelineID(scope);
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x0012AB80 File Offset: 0x00128D80
		private void DetermineDataPipelineID(IRIFReportDataScope scope)
		{
			if (scope.DataScopeInfo.DataSet == null)
			{
				return;
			}
			DataSet dataSet = scope.DataScopeInfo.DataSet;
			int num;
			if (scope.DataScopeInfo.NeedsIDC)
			{
				if (this.m_tree.IsIntersectionScope(scope))
				{
					if (DataSet.AreEqualById(dataSet, this.m_tree.GetParentRowScopeForIntersection(scope).DataScopeInfo.DataSet) || DataSet.AreEqualById(dataSet, this.m_tree.GetParentColumnScopeForIntersection(scope).DataScopeInfo.DataSet))
					{
						IRIFDataScope canonicalCellScope = this.m_tree.GetCanonicalCellScope(scope);
						if (ScopeTree.SameScope(scope, canonicalCellScope))
						{
							num = this.m_nextDataPipelineId;
							this.m_nextDataPipelineId++;
						}
						else
						{
							num = canonicalCellScope.DataScopeInfo.DataPipelineID;
						}
					}
					else
					{
						num = dataSet.IndexInCollection;
					}
				}
				else
				{
					num = dataSet.IndexInCollection;
				}
			}
			else
			{
				IRIFDataScope irifdataScope;
				if (this.m_tree.IsIntersectionScope(scope))
				{
					irifdataScope = this.m_tree.GetParentRowScopeForIntersection(scope);
				}
				else
				{
					irifdataScope = this.m_tree.GetParentScope(scope);
				}
				if (irifdataScope == null)
				{
					num = dataSet.IndexInCollection;
				}
				else
				{
					num = irifdataScope.DataScopeInfo.DataPipelineID;
				}
			}
			scope.DataScopeInfo.DataPipelineID = num;
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0012AC9F File Offset: 0x00128E9F
		public override void PreVisit(ReportHierarchyNode member)
		{
			base.PreVisit(member);
			this.SetDataSetForScope(member);
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x0012ACAF File Offset: 0x00128EAF
		public override void PreVisit(Cell cell, int rowIndex, int colIndex)
		{
			base.PreVisit(cell, rowIndex, colIndex);
			this.SetDataSetForScope(cell);
		}

		// Token: 0x04001FDB RID: 8155
		private readonly ErrorContext m_errorContext;

		// Token: 0x04001FDC RID: 8156
		private int m_nextDataPipelineId;
	}
}
