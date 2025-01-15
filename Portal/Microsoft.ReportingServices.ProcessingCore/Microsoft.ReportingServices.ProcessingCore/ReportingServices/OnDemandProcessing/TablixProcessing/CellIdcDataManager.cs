using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B0 RID: 2224
	internal sealed class CellIdcDataManager : BaseIdcDataManager
	{
		// Token: 0x06007967 RID: 31079 RVA: 0x001F3EB8 File Offset: 0x001F20B8
		public CellIdcDataManager(OnDemandProcessingContext odpContext, IRIFReportDataScope idcReportDataScope)
			: base(odpContext, idcReportDataScope.DataScopeInfo.DataSet)
		{
			this.m_joinInfo = idcReportDataScope.DataScopeInfo.JoinInfo as IntersectJoinInfo;
			Global.Tracer.Assert(this.m_joinInfo != null, "Did not find expected IntersectionJoinInfo");
			this.m_cellScope = (IRIFReportIntersectionScope)idcReportDataScope;
			if (!this.m_cellScope.IsColumnOuterGrouping)
			{
				this.m_activeOuterRelationship = this.m_joinInfo.GetActiveRowRelationship(this.m_idcDataSet);
				this.m_activeInnerRelationship = this.m_joinInfo.GetActiveColumnRelationship(this.m_idcDataSet);
				this.m_sharedDataSet = this.m_joinInfo.RowParentDataSet;
			}
			else
			{
				this.m_activeInnerRelationship = this.m_joinInfo.GetActiveRowRelationship(this.m_idcDataSet);
				this.m_activeOuterRelationship = this.m_joinInfo.GetActiveColumnRelationship(this.m_idcDataSet);
				this.m_sharedDataSet = this.m_joinInfo.ColumnParentDataSet;
			}
			this.m_shareOuterGroupDataSet = this.m_activeOuterRelationship == null;
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x001F3FB0 File Offset: 0x001F21B0
		public void RegisterActiveIntersection(RuntimeDataTablixGroupLeafObjReference innerGroupLeafRef, RuntimeDataTablixGroupLeafObjReference outerGroupLeafRef)
		{
			if (this.m_lastOuterGroupLeafRef != outerGroupLeafRef)
			{
				if (this.m_lastOuterGroupLeafRef != null)
				{
					using (this.m_lastOuterGroupLeafRef.PinValue())
					{
						this.m_lastOuterGroupLeafRef.Value().ResetStreamingModeIdcRowBuffer();
					}
				}
				this.m_lastOuterGroupLeafRef = outerGroupLeafRef;
				if (this.m_activeOuterRelationship != null)
				{
					this.m_lastOuterPrimaryKeyValues = this.EvaluatePrimaryKeyExpressions(this.m_lastOuterGroupLeafRef, this.m_activeOuterRelationship);
				}
			}
			if (this.m_lastInnerGroupLeafRef != innerGroupLeafRef)
			{
				this.m_lastInnerGroupLeafRef = innerGroupLeafRef;
				this.m_lastInnerPrimaryKeyValues = this.EvaluatePrimaryKeyExpressions(this.m_lastInnerGroupLeafRef, this.m_activeInnerRelationship);
			}
		}

		// Token: 0x06007969 RID: 31081 RVA: 0x001F4068 File Offset: 0x001F2268
		private Microsoft.ReportingServices.RdlExpressions.VariantResult[] EvaluatePrimaryKeyExpressions(RuntimeDataTablixGroupLeafObjReference groupLeafRef, Relationship relationship)
		{
			groupLeafRef.Value().DataRows[0].RestoreDataSetAndSetFields(this.m_odpContext, relationship.RelatedDataSet.DataSetCore.FieldsContext);
			return relationship.EvaluateJoinConditionKeys(true, this.m_odpContext.ReportRuntime);
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x001F40A8 File Offset: 0x001F22A8
		public override void Advance()
		{
			using (this.m_lastInnerGroupLeafRef.PinValue())
			{
				using (this.m_lastOuterGroupLeafRef.PinValue())
				{
					RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = this.m_lastInnerGroupLeafRef.Value();
					RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj2 = this.m_lastOuterGroupLeafRef.Value();
					OnDemandStateManager stateManager = this.m_odpContext.StateManager;
					ObjectModelImpl reportObjectModel = this.m_odpContext.ReportObjectModel;
					if (this.m_idcDataSet.DataSetCore.FieldsContext != null)
					{
						reportObjectModel.RestoreFields(this.m_idcDataSet.DataSetCore.FieldsContext);
					}
					while (this.SetupNextRow(this.m_lastOuterPrimaryKeyValues, this.m_activeOuterRelationship, this.m_lastInnerPrimaryKeyValues, this.m_activeInnerRelationship))
					{
						base.ApplyGroupingFieldsForServerAggregates(this.m_cellScope);
						bool flag = runtimeDataTablixGroupLeafObj.GetOrCreateCell(runtimeDataTablixGroupLeafObj2).NextRow();
						if (stateManager.ShouldStopPipelineAdvance(flag))
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600796B RID: 31083 RVA: 0x001F41A4 File Offset: 0x001F23A4
		private bool SetupNextRow(Microsoft.ReportingServices.RdlExpressions.VariantResult[] rowPrimaryKeys, Relationship rowRelationship, Microsoft.ReportingServices.RdlExpressions.VariantResult[] columnPrimaryKeys, Relationship columnRelationship)
		{
			if (!this.ReadRowFromDataSet())
			{
				return false;
			}
			bool flag;
			if (this.m_shareOuterGroupDataSet)
			{
				flag = base.Correlate(columnRelationship, columnPrimaryKeys, true);
			}
			else if (flag = base.Correlate(rowRelationship, rowPrimaryKeys, true))
			{
				flag &= base.Correlate(columnRelationship, columnPrimaryKeys, true);
			}
			if (!flag)
			{
				this.PushBackLastRow();
			}
			return flag;
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x001F41F8 File Offset: 0x001F23F8
		protected override void PushBackLastRow()
		{
			if (this.m_shareOuterGroupDataSet)
			{
				using (this.m_lastOuterGroupLeafRef.PinValue())
				{
					this.m_lastOuterGroupLeafRef.Value().PushBackStreamingModeIdcRowToBuffer();
					return;
				}
			}
			base.PushBackLastRow();
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x001F424C File Offset: 0x001F244C
		protected override bool ReadRowFromDataSet()
		{
			if (this.m_shareOuterGroupDataSet)
			{
				using (this.m_lastOuterGroupLeafRef.PinValue())
				{
					return this.m_lastOuterGroupLeafRef.Value().ReadStreamingModeIdcRowFromBufferOrDataSet(this.m_sharedDataSet.DataSetCore.FieldsContext);
				}
			}
			return base.ReadRowFromDataSet();
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x001F42B4 File Offset: 0x001F24B4
		protected override void SetupRelationshipQueryRestart()
		{
			base.AddRelationshipRestartPosition(this.m_activeOuterRelationship, this.m_lastOuterPrimaryKeyValues);
			base.AddRelationshipRestartPosition(this.m_activeInnerRelationship, this.m_lastInnerPrimaryKeyValues);
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x001F42DA File Offset: 0x001F24DA
		public override void Close()
		{
			base.Close();
			this.m_lastOuterGroupLeafRef = null;
			this.m_lastInnerGroupLeafRef = null;
		}

		// Token: 0x04003CEA RID: 15594
		private readonly IntersectJoinInfo m_joinInfo;

		// Token: 0x04003CEB RID: 15595
		private readonly bool m_shareOuterGroupDataSet;

		// Token: 0x04003CEC RID: 15596
		private readonly IRIFReportIntersectionScope m_cellScope;

		// Token: 0x04003CED RID: 15597
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_sharedDataSet;

		// Token: 0x04003CEE RID: 15598
		private RuntimeDataTablixGroupLeafObjReference m_lastOuterGroupLeafRef;

		// Token: 0x04003CEF RID: 15599
		private RuntimeDataTablixGroupLeafObjReference m_lastInnerGroupLeafRef;

		// Token: 0x04003CF0 RID: 15600
		private readonly Relationship m_activeOuterRelationship;

		// Token: 0x04003CF1 RID: 15601
		private readonly Relationship m_activeInnerRelationship;

		// Token: 0x04003CF2 RID: 15602
		private Microsoft.ReportingServices.RdlExpressions.VariantResult[] m_lastOuterPrimaryKeyValues;

		// Token: 0x04003CF3 RID: 15603
		private Microsoft.ReportingServices.RdlExpressions.VariantResult[] m_lastInnerPrimaryKeyValues;
	}
}
