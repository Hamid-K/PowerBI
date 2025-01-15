using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F7 RID: 2039
	internal class QueryRestartInfo
	{
		// Token: 0x060071D1 RID: 29137 RVA: 0x001D89B0 File Offset: 0x001D6BB0
		internal QueryRestartInfo()
		{
			this.m_queryRestartPosition = new List<ScopeIDContext>();
		}

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x060071D2 RID: 29138 RVA: 0x001D89CE File Offset: 0x001D6BCE
		// (set) Token: 0x060071D3 RID: 29139 RVA: 0x001D89D6 File Offset: 0x001D6BD6
		internal bool QueryRestartEnabled
		{
			get
			{
				return this.m_queryRestartEnabled;
			}
			set
			{
				this.m_queryRestartEnabled = value;
			}
		}

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x060071D4 RID: 29140 RVA: 0x001D89DF File Offset: 0x001D6BDF
		internal List<ScopeIDContext> QueryRestartPosition
		{
			get
			{
				return this.m_queryRestartPosition;
			}
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x060071D5 RID: 29141 RVA: 0x001D89E7 File Offset: 0x001D6BE7
		private ScopeIDContext LastScopeIDContext
		{
			get
			{
				if (this.m_queryRestartPosition.Count == 0)
				{
					return null;
				}
				return this.m_queryRestartPosition[this.m_queryRestartPosition.Count - 1];
			}
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x001D8A10 File Offset: 0x001D6C10
		private bool IsRestartable()
		{
			for (int i = 0; i < this.QueryRestartPosition.Count; i++)
			{
				if (this.QueryRestartPosition[i].IsRowLevelRestart)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060071D7 RID: 29143 RVA: 0x001D8A49 File Offset: 0x001D6C49
		internal void EnableQueryRestart()
		{
			if (this.IsRestartable())
			{
				this.m_queryRestartEnabled = true;
			}
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x001D8A5C File Offset: 0x001D6C5C
		public DataSetQueryRestartPosition GetRestartPositionForDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet)
		{
			if (!this.m_queryRestartEnabled)
			{
				return null;
			}
			List<RestartContext> list = new List<RestartContext>();
			List<RelationshipRestartContext> list2;
			if (this.m_relationshipRestartPositions.TryGetValue(targetDataSet, out list2))
			{
				foreach (RelationshipRestartContext relationshipRestartContext in list2)
				{
					list.Add(relationshipRestartContext);
				}
			}
			foreach (ScopeIDContext scopeIDContext in this.m_queryRestartPosition)
			{
				if (scopeIDContext.MemberDefinition.DataScopeInfo.DataSet == targetDataSet && scopeIDContext.RestartMode != RestartMode.Rom)
				{
					list.Add(scopeIDContext);
				}
			}
			DataSetQueryRestartPosition dataSetQueryRestartPosition = null;
			if (list.Count > 0)
			{
				dataSetQueryRestartPosition = new DataSetQueryRestartPosition(list);
			}
			return dataSetQueryRestartPosition;
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x001D8B44 File Offset: 0x001D6D44
		public void AddRelationshipRestartPosition(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, RelationshipRestartContext relationshipRestart)
		{
			List<RelationshipRestartContext> list = null;
			if (this.m_relationshipRestartPositions.TryGetValue(dataSet, out list))
			{
				this.m_relationshipRestartPositions[dataSet].Add(relationshipRestart);
				return;
			}
			list = new List<RelationshipRestartContext>();
			list.Add(relationshipRestart);
			this.m_relationshipRestartPositions.Add(dataSet, list);
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x001D8B90 File Offset: 0x001D6D90
		internal bool TryAddScopeID(ScopeID scopeID, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef, InternalStreamingOdpDynamicMemberLogic memberLogic)
		{
			if (this.IsParentScopeIDAlreadySet(memberDef))
			{
				RestartMode restartMode;
				if (this.CanMarkRestartable(memberDef))
				{
					restartMode = RestartMode.Query;
				}
				else
				{
					restartMode = RestartMode.Rom;
				}
				this.m_queryRestartPosition.Add(new ScopeIDContext(scopeID, memberDef, memberLogic, restartMode));
				return true;
			}
			return false;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x001D8BCC File Offset: 0x001D6DCC
		internal void RomBasedRestart()
		{
			for (int i = 0; i < this.m_queryRestartPosition.Count; i++)
			{
				ScopeIDContext scopeIDContext = this.m_queryRestartPosition[i];
				if (!scopeIDContext.IsRowLevelRestart && !scopeIDContext.RomBasedRestart())
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsRombasedRestartFailed, new object[] { scopeIDContext.MemberDefinition.Grouping.Name });
				}
			}
			this.ClearRomRestartScopeIDs();
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x001D8C38 File Offset: 0x001D6E38
		private void ClearRomRestartScopeIDs()
		{
			for (int i = this.m_queryRestartPosition.Count - 1; i >= 0; i--)
			{
				if (!this.m_queryRestartPosition[i].IsRowLevelRestart)
				{
					this.m_queryRestartPosition.RemoveAt(i);
				}
			}
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x001D8C7C File Offset: 0x001D6E7C
		private bool CanMarkRestartable(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef)
		{
			return memberDef.DataScopeInfo.IsDecomposable && memberDef.Sorting != null && memberDef.Sorting.NaturalSort && (this.LastScopeIDContext == null || this.LastScopeIDContext.IsRowLevelRestart);
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x001D8CB7 File Offset: 0x001D6EB7
		private bool IsParentScopeIDAlreadySet(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode target)
		{
			if (this.m_queryRestartPosition.Count == 0)
			{
				return !this.ParentScopeIsDynamic(target);
			}
			return this.IsParentScopeAdded(target);
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x001D8CD8 File Offset: 0x001D6ED8
		private bool ParentScopeIsDynamic(IRIFReportDataScope target)
		{
			for (IRIFReportDataScope irifreportDataScope = target.ParentReportScope; irifreportDataScope != null; irifreportDataScope = irifreportDataScope.ParentReportScope)
			{
				if (irifreportDataScope.IsGroup)
				{
					return true;
				}
				if (irifreportDataScope.IsDataIntersectionScope)
				{
					IRIFReportIntersectionScope irifreportIntersectionScope = (IRIFReportIntersectionScope)irifreportDataScope;
					return this.ParentScopeIsDynamic(irifreportIntersectionScope.ParentRowReportScope) || this.ParentScopeIsDynamic(irifreportIntersectionScope.ParentColumnReportScope);
				}
			}
			return false;
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x001D8D30 File Offset: 0x001D6F30
		private bool IsParentScopeAdded(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode target)
		{
			if (target.DataScopeInfo.DataSet != this.LastScopeIDContext.MemberDefinition.DataScopeInfo.DataSet && !target.IsChildScopeOf(this.LastScopeIDContext.MemberDefinition) && !this.LastScopeIDContext.MemberDefinition.IsChildScopeOf(target))
			{
				return true;
			}
			if (target.IsChildScopeOf(this.LastScopeIDContext.MemberDefinition))
			{
				return true;
			}
			if (!target.DataScopeInfo.IsDecomposable)
			{
				for (int i = this.m_queryRestartPosition.Count - 2; i >= 0; i--)
				{
					if (target.IsChildScopeOf(this.m_queryRestartPosition[i].MemberDefinition))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04003A85 RID: 14981
		private bool m_queryRestartEnabled;

		// Token: 0x04003A86 RID: 14982
		private List<ScopeIDContext> m_queryRestartPosition;

		// Token: 0x04003A87 RID: 14983
		private Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet, List<RelationshipRestartContext>> m_relationshipRestartPositions = new Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.DataSet, List<RelationshipRestartContext>>();
	}
}
