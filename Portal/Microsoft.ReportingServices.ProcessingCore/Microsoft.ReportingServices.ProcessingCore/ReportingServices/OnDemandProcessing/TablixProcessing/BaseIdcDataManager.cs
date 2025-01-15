using System;
using System.Diagnostics;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B1 RID: 2225
	internal abstract class BaseIdcDataManager : IDisposable
	{
		// Token: 0x06007970 RID: 31088 RVA: 0x001F42F0 File Offset: 0x001F24F0
		public BaseIdcDataManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet idcDataSet)
		{
			Global.Tracer.Assert(!odpContext.InSubreport, "IDC currently cannot be used inside subreports");
			this.m_odpContext = odpContext;
			this.m_idcDataSet = idcDataSet;
			this.m_needsServerAggregateTranslation = this.m_idcDataSet.HasScopeWithCustomAggregates;
		}

		// Token: 0x06007971 RID: 31089
		public abstract void Advance();

		// Token: 0x06007972 RID: 31090 RVA: 0x001F432F File Offset: 0x001F252F
		protected void ApplyGroupingFieldsForServerAggregates(IRIFReportDataScope idcReportDataScope)
		{
			if (!this.m_needsServerAggregateTranslation)
			{
				return;
			}
			idcReportDataScope.DataScopeInfo.ApplyGroupingFieldsForServerAggregates(this.m_odpContext.ReportObjectModel.FieldsImpl);
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x001F4358 File Offset: 0x001F2558
		protected virtual void PushBackLastRow()
		{
			FieldsImpl fieldsImplForUpdate = this.m_odpContext.ReportObjectModel.GetFieldsImplForUpdate(this.m_idcDataSet);
			if (fieldsImplForUpdate.IsAggregateRow)
			{
				this.m_nextDataFieldRowToProcess = new AggregateRow(fieldsImplForUpdate, true);
				return;
			}
			this.m_nextDataFieldRowToProcess = new DataFieldRow(fieldsImplForUpdate, true);
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001F43A0 File Offset: 0x001F25A0
		protected virtual bool ReadRowFromDataSet()
		{
			if (this.m_nextDataFieldRowToProcess != null)
			{
				this.m_nextDataFieldRowToProcess.SetFields(this.m_odpContext.ReportObjectModel.GetFieldsImplForUpdate(this.m_idcDataSet));
				this.m_nextDataFieldRowToProcess = null;
			}
			else
			{
				if (this.m_dataSource == null)
				{
					if (this.m_odpContext.QueryRestartInfo != null)
					{
						this.SetupRelationshipQueryRestart();
					}
					this.m_dataSource = new RuntimeIdcIncrementalDataSource(this.m_idcDataSet, this.m_odpContext);
					this.m_dataSource.Initialize();
				}
				if (!this.m_dataSource.SetupNextRow())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x001F442C File Offset: 0x001F262C
		protected bool Correlate(Relationship relationship, Microsoft.ReportingServices.RdlExpressions.VariantResult[] primaryKeys, bool advancedRowCursor)
		{
			SortDirection[] sortDirections = relationship.GetSortDirections();
			bool flag = false;
			bool flag2 = true;
			while (flag2 && !flag)
			{
				Microsoft.ReportingServices.RdlExpressions.VariantResult[] array = relationship.EvaluateJoinConditionKeys(false, this.m_odpContext.ReportRuntime);
				flag = true;
				int num = 0;
				while (flag && primaryKeys != null && num < primaryKeys.Length)
				{
					int num2 = this.m_odpContext.CompareAndStopOnError(primaryKeys[num].Value, array[num].Value, ObjectType.DataSet, this.m_idcDataSet.Name, "JoinCondition", false);
					if (sortDirections[num] == SortDirection.Ascending)
					{
						flag2 = num2 >= 0;
					}
					else
					{
						flag2 = num2 <= 0;
					}
					flag &= num2 == 0;
					num++;
				}
				if (flag2 && flag && this.m_skippingFilter != null)
				{
					flag2 = this.m_skippingFilter.ShouldSkipCurrentRow();
					flag = !flag2;
				}
				if (flag2 && !flag)
				{
					if (advancedRowCursor)
					{
						this.m_skippedRowCount += 1L;
					}
					if (!this.ReadRowFromDataSet())
					{
						return false;
					}
					advancedRowCursor = true;
				}
			}
			return flag;
		}

		// Token: 0x06007976 RID: 31094
		protected abstract void SetupRelationshipQueryRestart();

		// Token: 0x06007977 RID: 31095 RVA: 0x001F4520 File Offset: 0x001F2720
		protected void AddRelationshipRestartPosition(Relationship relationship, Microsoft.ReportingServices.RdlExpressions.VariantResult[] primaryKeys)
		{
			if (relationship == null)
			{
				return;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[] foreignKeyExpressions = relationship.GetForeignKeyExpressions();
			if (foreignKeyExpressions != null && primaryKeys != null)
			{
				RelationshipRestartContext relationshipRestartContext = new RelationshipRestartContext(foreignKeyExpressions, primaryKeys, relationship.GetSortDirections(), this.m_idcDataSet);
				this.m_odpContext.QueryRestartInfo.AddRelationshipRestartPosition(this.m_idcDataSet, relationshipRestartContext);
			}
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x001F4569 File Offset: 0x001F2769
		[Conditional("DEBUG")]
		protected void AssertPrimaryKeysMatchForeignKeys(Relationship relationship, Array primaryKeys, Array foreignKeys)
		{
			if (!relationship.NaturalJoin || primaryKeys != null)
			{
			}
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x001F4578 File Offset: 0x001F2778
		public virtual void Close()
		{
			if (this.m_dataSource != null)
			{
				this.m_dataSource.RecordTimeDataRetrieval();
				this.m_dataSource.RecordSkippedRowCount(this.m_skippedRowCount);
				this.m_dataSource.Teardown();
				this.m_dataSource = null;
			}
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x001F45B0 File Offset: 0x001F27B0
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x0600797B RID: 31099 RVA: 0x001F45B8 File Offset: 0x001F27B8
		protected bool IsDataPipelineSetup
		{
			get
			{
				return this.m_dataSource != null;
			}
		}

		// Token: 0x04003CF4 RID: 15604
		protected OnDemandProcessingContext m_odpContext;

		// Token: 0x04003CF5 RID: 15605
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_idcDataSet;

		// Token: 0x04003CF6 RID: 15606
		private readonly bool m_needsServerAggregateTranslation;

		// Token: 0x04003CF7 RID: 15607
		private RuntimeIdcIncrementalDataSource m_dataSource;

		// Token: 0x04003CF8 RID: 15608
		private DataFieldRow m_nextDataFieldRowToProcess;

		// Token: 0x04003CF9 RID: 15609
		private long m_skippedRowCount;

		// Token: 0x04003CFA RID: 15610
		protected RowSkippingFilter m_skippingFilter;
	}
}
