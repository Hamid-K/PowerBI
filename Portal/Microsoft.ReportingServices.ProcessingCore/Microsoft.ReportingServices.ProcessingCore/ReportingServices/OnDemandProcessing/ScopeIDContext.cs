using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007FD RID: 2045
	internal sealed class ScopeIDContext : RestartContext
	{
		// Token: 0x060071F2 RID: 29170 RVA: 0x001D9124 File Offset: 0x001D7324
		internal ScopeIDContext(ScopeID scopeID, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef, InternalStreamingOdpDynamicMemberLogic memberLogic, RestartMode restartMode)
			: base(restartMode)
		{
			this.m_scopeID = scopeID;
			this.m_memberDef = memberDef;
			this.m_memberLogic = memberLogic;
		}

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x060071F3 RID: 29171 RVA: 0x001D9143 File Offset: 0x001D7343
		internal ScopeID ScopeID
		{
			get
			{
				return this.m_scopeID;
			}
		}

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x060071F4 RID: 29172 RVA: 0x001D914B File Offset: 0x001D734B
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x060071F5 RID: 29173 RVA: 0x001D9153 File Offset: 0x001D7353
		internal InternalStreamingOdpDynamicMemberLogic MemberLogic
		{
			get
			{
				return this.m_memberLogic;
			}
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x001D915B File Offset: 0x001D735B
		public bool RomBasedRestart()
		{
			return this.m_memberLogic.RomBasedRestart(this.m_scopeID);
		}

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x001D9170 File Offset: 0x001D7370
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> Expressions
		{
			get
			{
				if (this.m_memberDef.Sorting != null && this.m_memberDef.Sorting.NaturalSort)
				{
					return this.m_memberDef.Sorting.SortExpressions;
				}
				return this.m_memberDef.Grouping.GroupExpressions;
			}
		}

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x060071F8 RID: 29176 RVA: 0x001D91BD File Offset: 0x001D73BD
		internal List<bool> SortDirections
		{
			get
			{
				return this.m_memberDef.Sorting.SortDirections;
			}
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x001D91D0 File Offset: 0x001D73D0
		public override List<ScopeValueFieldName> GetScopeValueFieldNameCollection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			List<ScopeValueFieldName> list = new List<ScopeValueFieldName>();
			int num = 0;
			foreach (ScopeValue scopeValue in this.m_scopeID.QueryRestartPosition)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.Expressions[num];
				string dataField = dataSet.Fields[expressionInfo.FieldIndex].DataField;
				list.Add(new ScopeValueFieldName(dataField, scopeValue.Value));
				num++;
			}
			return list;
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x001D9264 File Offset: 0x001D7464
		public override RowSkippingControlFlag DoesNotMatchRowRecordField(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.RecordField[] recordFields)
		{
			int num = 0;
			foreach (ScopeValue scopeValue in this.m_scopeID.QueryRestartPosition)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.Expressions[num];
				Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField = recordFields[expressionInfo.FieldIndex];
				RowSkippingControlFlag rowSkippingControlFlag = base.CompareFieldWithScopeValueAndStopOnInequality(odpContext, recordField, scopeValue.Value, this.SortDirections[num], ObjectType.DataSet, this.m_memberDef.DataScopeInfo.DataSet.Name, "ScopeID.QueryRestart");
				if (rowSkippingControlFlag != RowSkippingControlFlag.ExactMatch)
				{
					return rowSkippingControlFlag;
				}
				num++;
			}
			return RowSkippingControlFlag.ExactMatch;
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x001D9314 File Offset: 0x001D7514
		public override void TraceStartAtRecoveryMessage()
		{
			Global.Tracer.Trace(TraceLevel.Warning, "START AT Recovery Mode: Target row grouping {0} did not match with ScopeID = {1}.", new object[]
			{
				this.m_memberDef.Grouping.Name,
				this.m_scopeID.ToString()
			});
		}

		// Token: 0x04003A96 RID: 14998
		private readonly ScopeID m_scopeID;

		// Token: 0x04003A97 RID: 14999
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode m_memberDef;

		// Token: 0x04003A98 RID: 15000
		private readonly InternalStreamingOdpDynamicMemberLogic m_memberLogic;
	}
}
