using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008EE RID: 2286
	internal sealed class RuntimeLookupProcessing
	{
		// Token: 0x06007DC5 RID: 32197 RVA: 0x0020754F File Offset: 0x0020574F
		internal RuntimeLookupProcessing(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, RuntimeOnDemandDataSetObj lookupOwner)
		{
			this.m_odpContext = odpContext;
			this.m_dataSet = dataSet;
			this.m_dataSetInstance = dataSetInstance;
			this.m_lookupOwner = lookupOwner;
			this.m_mustBufferAllRows = dataSet.HasSameDataSetLookups;
			this.InitializeRuntimeStructures();
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x00207590 File Offset: 0x00205790
		private void InitializeRuntimeStructures()
		{
			Global.Tracer.Assert(this.m_dataSet.LookupDestinationInfos != null && this.m_dataSet.LookupDestinationInfos.Count > 0, "Attempted to perform Lookup processing on a DataSet with no Lookups");
			IScalabilityCache tablixProcessingScalabilityCache = this.m_odpContext.TablixProcessingScalabilityCache;
			Global.Tracer.Assert(tablixProcessingScalabilityCache != null, "Cannot start Lookup processing unless Scalability is setup");
			if (this.m_mustBufferAllRows)
			{
				this.m_odpContext.TablixProcessingLookupRowCache = new CommonRowCache(tablixProcessingScalabilityCache);
			}
			int count = this.m_dataSet.LookupDestinationInfos.Count;
			List<LookupObjResult> list = new List<LookupObjResult>(count);
			this.m_dataSetInstance.LookupResults = list;
			for (int i = 0; i < count; i++)
			{
				LookupDestinationInfo lookupDestinationInfo = this.m_dataSet.LookupDestinationInfos[i];
				LookupObjResult lookupObjResult = new LookupObjResult(new LookupTable(tablixProcessingScalabilityCache, this.m_odpContext.ProcessingComparer, lookupDestinationInfo.UsedInSameDataSetTablixProcessing));
				list.Add(lookupObjResult);
			}
		}

		// Token: 0x170028FC RID: 10492
		// (get) Token: 0x06007DC7 RID: 32199 RVA: 0x00207671 File Offset: 0x00205871
		internal bool MustBufferAllRows
		{
			get
			{
				return this.m_mustBufferAllRows;
			}
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x0020767C File Offset: 0x0020587C
		internal void NextRow()
		{
			long streamOffset = this.m_odpContext.ReportObjectModel.FieldsImpl.StreamOffset;
			int num = -1;
			CommonRowCache tablixProcessingLookupRowCache = this.m_odpContext.TablixProcessingLookupRowCache;
			if (this.m_mustBufferAllRows)
			{
				num = tablixProcessingLookupRowCache.AddRow(RuntimeDataTablixObj.SaveData(this.m_odpContext));
				if (this.m_firstRowCacheIndex == -1)
				{
					this.m_firstRowCacheIndex = num;
				}
			}
			IScalabilityCache tablixProcessingScalabilityCache = this.m_odpContext.TablixProcessingScalabilityCache;
			for (int i = 0; i < this.m_dataSet.LookupDestinationInfos.Count; i++)
			{
				LookupDestinationInfo lookupDestinationInfo = this.m_dataSet.LookupDestinationInfos[i];
				LookupObjResult lookupObjResult = this.m_dataSetInstance.LookupResults[i];
				if (!lookupObjResult.ErrorOccured)
				{
					Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = lookupDestinationInfo.EvaluateDestExpr(this.m_odpContext, lookupObjResult);
					if (variantResult.ErrorOccurred)
					{
						lookupObjResult.DataFieldStatus = variantResult.FieldStatus;
					}
					else
					{
						object value = variantResult.Value;
						LookupTable lookupTable = lookupObjResult.GetLookupTable(this.m_odpContext);
						try
						{
							LookupMatches lookupMatches;
							IDisposable disposable;
							if (!lookupTable.TryGetAndPinValue(value, out lookupMatches, out disposable))
							{
								if (lookupDestinationInfo.UsedInSameDataSetTablixProcessing)
								{
									lookupMatches = new LookupMatchesWithRows();
								}
								else
								{
									lookupMatches = new LookupMatches();
								}
								disposable = lookupTable.AddAndPin(value, lookupMatches);
							}
							if (lookupDestinationInfo.IsMultiValue || !lookupMatches.HasRow)
							{
								lookupMatches.AddRow(streamOffset, num, tablixProcessingScalabilityCache);
							}
							disposable.Dispose();
						}
						catch (ReportProcessingException_SpatialTypeComparisonError reportProcessingException_SpatialTypeComparisonError)
						{
							throw new ReportProcessingException(this.m_lookupOwner.RegisterSpatialElementComparisonError(reportProcessingException_SpatialTypeComparisonError.Type));
						}
					}
				}
			}
			if (!this.m_mustBufferAllRows)
			{
				this.m_lookupOwner.PostLookupNextRow();
			}
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x00207818 File Offset: 0x00205A18
		internal void FinishReadingRows()
		{
			if (!this.m_mustBufferAllRows)
			{
				return;
			}
			CommonRowCache tablixProcessingLookupRowCache = this.m_odpContext.TablixProcessingLookupRowCache;
			if (this.m_firstRowCacheIndex != -1)
			{
				for (int i = this.m_firstRowCacheIndex; i < tablixProcessingLookupRowCache.Count; i++)
				{
					tablixProcessingLookupRowCache.SetupRow(i, this.m_odpContext);
					this.m_lookupOwner.PostLookupNextRow();
				}
			}
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x00207874 File Offset: 0x00205A74
		internal void CompleteLookupProcessing()
		{
			for (int i = 0; i < this.m_dataSetInstance.LookupResults.Count; i++)
			{
				this.m_dataSetInstance.LookupResults[i].TransferToLookupCache(this.m_odpContext);
			}
			if (this.m_odpContext.TablixProcessingLookupRowCache != null)
			{
				this.m_odpContext.TablixProcessingLookupRowCache.Dispose();
				this.m_odpContext.TablixProcessingLookupRowCache = null;
			}
		}

		// Token: 0x04003E04 RID: 15876
		private RuntimeOnDemandDataSetObj m_lookupOwner;

		// Token: 0x04003E05 RID: 15877
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003E06 RID: 15878
		private Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;

		// Token: 0x04003E07 RID: 15879
		private DataSetInstance m_dataSetInstance;

		// Token: 0x04003E08 RID: 15880
		private bool m_mustBufferAllRows;

		// Token: 0x04003E09 RID: 15881
		private int m_firstRowCacheIndex = -1;
	}
}
