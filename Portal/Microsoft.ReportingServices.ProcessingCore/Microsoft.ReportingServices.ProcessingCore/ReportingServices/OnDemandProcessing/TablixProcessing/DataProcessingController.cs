using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008AF RID: 2223
	internal sealed class DataProcessingController
	{
		// Token: 0x0600795B RID: 31067 RVA: 0x001F3874 File Offset: 0x001F1A74
		public DataProcessingController(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance)
		{
			this.m_odpContext = odpContext;
			this.m_dataSet = dataSet;
			this.m_dataSetInstance = dataSetInstance;
			this.m_report = odpContext.ReportDefinition;
			this.m_odpContext.EnsureScalabilitySetup();
			UserSortFilterContext userSortFilterContext = this.m_odpContext.UserSortFilterContext;
			if (!this.m_odpContext.InSubreportInDataRegion)
			{
				userSortFilterContext.ResetContextForTopLevelDataSet();
			}
			this.m_hasSortFilterInfo = this.m_odpContext.PopulateRuntimeSortFilterEventInfo(this.m_dataSet);
			if (-1 == userSortFilterContext.DataSetGlobalId)
			{
				userSortFilterContext.DataSetGlobalId = this.m_dataSet.GlobalID;
			}
			Global.Tracer.Assert(this.m_odpContext.ReportObjectModel != null && this.m_odpContext.ReportRuntime != null);
			this.m_odpContext.SetupFieldsForNewDataSet(this.m_dataSet, this.m_dataSetInstance, true, true);
			this.m_dataSet.SetFilterExprHost(this.m_odpContext.ReportObjectModel);
			this.m_dataSetObj = new RuntimeOnDemandDataSetObj(this.m_odpContext, this.m_dataSet, this.m_dataSetInstance);
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x001F3977 File Offset: 0x001F1B77
		public void InitializeDataProcessing()
		{
			this.m_dataSetObj.Initialize();
			Global.Tracer.Assert(this.m_odpContext.CurrentDataSetIndex == this.m_dataSet.IndexInCollection, "(m_odpContext.CurrentDataSetIndex == m_dataSet.IndexInCollection)");
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x001F39AB File Offset: 0x001F1BAB
		public void TeardownDataProcessing()
		{
			this.m_dataSetObj.Teardown();
			this.m_odpContext.EnsureScalabilityCleanup();
		}

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x0600795E RID: 31070 RVA: 0x001F39C3 File Offset: 0x001F1BC3
		public IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_dataSetObj;
			}
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x001F39CC File Offset: 0x001F1BCC
		public void NextRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row, int rowNumber, bool useRowOffset, bool readerExtensionsSupported)
		{
			FieldsImpl fieldsImplForUpdate = this.m_odpContext.ReportObjectModel.GetFieldsImplForUpdate(this.m_dataSet);
			if (useRowOffset)
			{
				fieldsImplForUpdate.NewRow();
			}
			else
			{
				fieldsImplForUpdate.NewRow(row.StreamPosition);
			}
			if (fieldsImplForUpdate.AddRowIndex)
			{
				fieldsImplForUpdate.SetRowIndex(rowNumber);
			}
			this.m_odpContext.ReportObjectModel.UpdateFieldValues(false, row, this.m_dataSetInstance, readerExtensionsSupported);
			this.m_dataSetObj.NextRow();
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x001F3A3C File Offset: 0x001F1C3C
		public void AllRowsRead()
		{
			this.m_dataSetObj.FinishReadingRows();
			this.m_odpContext.FirstPassPostProcess();
			Global.Tracer.Trace(TraceLevel.Verbose, "TablixProcessing: FirstPass Complete");
			if (this.m_report.HasAggregatesOfAggregatesInUserSort && this.m_odpContext.RuntimeSortFilterInfo != null && this.m_odpContext.RuntimeSortFilterInfo.Count > 0)
			{
				this.PreComputeAggregatesOfAggregates();
			}
			this.m_odpContext.ApplyUserSorts();
			Global.Tracer.Trace(TraceLevel.Verbose, "TablixProcessing: ApplyUserSorts Complete");
			this.SortAndFilter();
			Global.Tracer.Trace(TraceLevel.Verbose, "TablixProcessing: SortAndFilter Complete");
			this.m_odpContext.CheckAndThrowIfAborted();
			this.PostSortOperations();
			Global.Tracer.Trace(TraceLevel.Verbose, "TablixProcessing: PostSortOperations Complete");
			this.StoreDataSetLevelAggregates();
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x001F3AFC File Offset: 0x001F1CFC
		private void PreComputeAggregatesOfAggregates()
		{
			if (this.m_report.NeedPostGroupProcessing)
			{
				this.m_odpContext.SecondPassOperation = SecondPassOperations.FilteringOrAggregatesOrDomainScope;
				AggregateUpdateContext aggregateUpdateContext = new AggregateUpdateContext(this.m_odpContext, AggregateMode.Aggregates);
				this.m_odpContext.DomainScopeContext = new DomainScopeContext();
				this.m_dataSetObj.SortAndFilter(aggregateUpdateContext);
				this.m_odpContext.DomainScopeContext = null;
			}
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x001F3B58 File Offset: 0x001F1D58
		private void SortAndFilter()
		{
			if (this.m_report.NeedPostGroupProcessing)
			{
				this.m_odpContext.SecondPassOperation = (this.m_report.HasVariables ? SecondPassOperations.Variables : SecondPassOperations.None);
				if (this.m_report.HasSpecialRecursiveAggregates)
				{
					this.m_odpContext.SecondPassOperation |= SecondPassOperations.FilteringOrAggregatesOrDomainScope;
				}
				else
				{
					this.m_odpContext.SecondPassOperation |= SecondPassOperations.Sorting | SecondPassOperations.FilteringOrAggregatesOrDomainScope;
				}
				AggregateUpdateContext aggregateUpdateContext = new AggregateUpdateContext(this.m_odpContext, AggregateMode.Aggregates);
				this.m_dataSetObj.EnterProcessUserSortPhase();
				this.m_odpContext.DomainScopeContext = new DomainScopeContext();
				this.m_dataSetObj.SortAndFilter(aggregateUpdateContext);
				this.m_odpContext.DomainScopeContext = null;
				if (this.m_report.HasSpecialRecursiveAggregates)
				{
					this.m_odpContext.SecondPassOperation = SecondPassOperations.Sorting;
					this.m_dataSetObj.SortAndFilter(aggregateUpdateContext);
				}
				this.m_dataSetObj.LeaveProcessUserSortPhase();
			}
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x001F3C38 File Offset: 0x001F1E38
		private void PostSortOperations()
		{
			if (this.m_report.HasPostSortAggregates)
			{
				Dictionary<string, IReference<RuntimeGroupRootObj>> dictionary = new Dictionary<string, IReference<RuntimeGroupRootObj>>();
				AggregateUpdateContext aggregateUpdateContext = new AggregateUpdateContext(this.m_odpContext, AggregateMode.PostSortAggregates);
				this.m_dataSetObj.CalculateRunningValues(dictionary, aggregateUpdateContext);
			}
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x001F3C74 File Offset: 0x001F1E74
		private void StoreDataSetLevelAggregates()
		{
			if (this.m_dataSet.Aggregates != null || this.m_dataSet.PostSortAggregates != null)
			{
				DataSetInstance dataSetInstance = this.m_odpContext.CurrentReportInstance.GetDataSetInstance(this.m_dataSet.IndexInCollection, this.m_odpContext);
				if (this.m_dataSet.Aggregates != null)
				{
					dataSetInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataSet.Aggregates);
				}
				if (this.m_dataSet.PostSortAggregates != null)
				{
					dataSetInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataSet.PostSortAggregates);
				}
			}
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x001F3D08 File Offset: 0x001F1F08
		public void GenerateGroupTree()
		{
			Global.Tracer.Trace(TraceLevel.Verbose, "Data processing complete.  Beginning group tree creation");
			OnDemandMetadata odpMetadata = this.m_odpContext.OdpMetadata;
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager.EnsureGroupTreeStorageSetup(odpMetadata, this.m_odpContext.ChunkFactory, odpMetadata.GlobalIDOwnerCollection, false, this.m_odpContext.GetActiveCompatibilityVersion(), this.m_odpContext.ProhibitSerializableValues);
			this.CreateInstances();
			if (!this.m_odpContext.InSubreportInDataRegion)
			{
				this.m_odpContext.TopLevelContext.MergeNewUserSortFilterInformation();
				odpMetadata.ResetUserSortFilterContexts();
			}
			this.m_dataSetObj.CompleteLookupProcessing();
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x001F3D94 File Offset: 0x001F1F94
		private void CreateInstances()
		{
			this.m_odpContext.ReportRuntime.CurrentScope = this.m_dataSetObj;
			if (this.m_hasSortFilterInfo && this.m_odpContext.RuntimeSortFilterInfo != null)
			{
				if (this.m_odpContext.TopLevelContext.ReportRuntimeUserSortFilterInfo == null)
				{
					this.m_odpContext.TopLevelContext.ReportRuntimeUserSortFilterInfo = new List<IReference<RuntimeSortFilterEventInfo>>();
				}
				this.m_odpContext.TopLevelContext.ReportRuntimeUserSortFilterInfo.AddRange(this.m_odpContext.RuntimeSortFilterInfo);
			}
			this.m_dataSetObj.CreateInstances();
			if (this.m_odpContext.ReportDefinition.InScopeEventSources != null)
			{
				int count = this.m_odpContext.ReportDefinition.InScopeEventSources.Count;
				List<IInScopeEventSource> list = null;
				for (int i = 0; i < count; i++)
				{
					IInScopeEventSource inScopeEventSource = this.m_odpContext.ReportDefinition.InScopeEventSources[i];
					if (inScopeEventSource.UserSort.DataSet == this.m_dataSet)
					{
						if (list == null)
						{
							list = new List<IInScopeEventSource>(count - i);
						}
						list.Add(inScopeEventSource);
					}
				}
				if (list != null)
				{
					UserSortFilterContext.ProcessEventSources(this.m_odpContext, this.m_dataSetObj, list);
				}
			}
			this.m_odpContext.ReportRuntime.CurrentScope = null;
		}

		// Token: 0x04003CE4 RID: 15588
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003CE5 RID: 15589
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;

		// Token: 0x04003CE6 RID: 15590
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04003CE7 RID: 15591
		private readonly DataSetInstance m_dataSetInstance;

		// Token: 0x04003CE8 RID: 15592
		private RuntimeOnDemandDataSetObj m_dataSetObj;

		// Token: 0x04003CE9 RID: 15593
		private bool m_hasSortFilterInfo;
	}
}
