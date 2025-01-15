using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BA RID: 1978
	internal sealed class LookupImpl : Lookup
	{
		// Token: 0x06007038 RID: 28728 RVA: 0x001D3900 File Offset: 0x001D1B00
		internal LookupImpl(LookupInfo lookupInfo, ReportRuntime reportRuntime)
		{
			this.m_lookupInfo = lookupInfo;
			this.m_reportRuntime = reportRuntime;
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x06007039 RID: 28729 RVA: 0x001D3918 File Offset: 0x001D1B18
		public override object Value
		{
			get
			{
				object obj = null;
				object[] array = this.EvaluateLookup();
				if (array != null && array.Length != 0)
				{
					obj = array[0];
				}
				return obj;
			}
		}

		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x0600703A RID: 28730 RVA: 0x001D393A File Offset: 0x001D1B3A
		public override object[] Values
		{
			get
			{
				return this.EvaluateLookup();
			}
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x001D3944 File Offset: 0x001D1B44
		internal object[] EvaluateLookup()
		{
			bool flag = this.m_lookupInfo.ReturnFirstMatchOnly();
			OnDemandProcessingContext odpContext = this.m_reportRuntime.ReportObjectModel.OdpContext;
			DataSet dataSet = odpContext.ReportDefinition.MappingDataSetIndexToDataSet[this.m_lookupInfo.DataSetIndexInCollection];
			DataSetInstance dataSetInstance = odpContext.GetDataSetInstance(dataSet);
			if (dataSetInstance == null)
			{
				throw new ReportProcessingException_InvalidOperationException();
			}
			if (dataSetInstance.NoRows)
			{
				return LookupImpl.EmptyResult;
			}
			if (dataSetInstance.LookupResults == null || dataSetInstance.LookupResults[this.m_lookupInfo.DestinationIndexInCollection] == null)
			{
				if (!odpContext.CalculateLookup(this.m_lookupInfo))
				{
					return LookupImpl.EmptyResult;
				}
				Global.Tracer.Assert(dataSetInstance.LookupResults != null, "Lookup not initialized correctly by tablix processing");
			}
			LookupObjResult lookupObjResult = dataSetInstance.LookupResults[this.m_lookupInfo.DestinationIndexInCollection];
			if (lookupObjResult.ErrorOccured)
			{
				IErrorContext reportRuntime = this.m_reportRuntime;
				if (lookupObjResult.DataFieldStatus == DataFieldStatus.None && lookupObjResult.ErrorCode != ProcessingErrorCode.rsNone)
				{
					reportRuntime.Register(lookupObjResult.ErrorCode, lookupObjResult.ErrorSeverity, lookupObjResult.ErrorMessageArgs);
				}
				else if (lookupObjResult.DataFieldStatus == DataFieldStatus.UnSupportedDataType)
				{
					reportRuntime.Register(ProcessingErrorCode.rsLookupOfInvalidExpressionDataType, Severity.Warning, lookupObjResult.ErrorMessageArgs);
				}
				throw new ReportProcessingException_InvalidOperationException();
			}
			VariantResult variantResult = this.m_lookupInfo.EvaluateSourceExpr(this.m_reportRuntime);
			this.CheckExprResultError(variantResult);
			bool flag2 = lookupObjResult.HasBeenTransferred || odpContext.CurrentDataSetIndex != dataSet.IndexInCollection;
			List<object> list = null;
			CompareInfo compareInfo = null;
			CompareOptions compareOptions = CompareOptions.None;
			bool flag3 = false;
			bool flag4 = false;
			try
			{
				if (flag2)
				{
					compareInfo = odpContext.CompareInfo;
					compareOptions = odpContext.ClrCompareOptions;
					flag3 = odpContext.NullsAsBlanks;
					flag4 = odpContext.UseOrdinalStringKeyGeneration;
					dataSetInstance.SetupCollationSettings(odpContext);
				}
				LookupTable lookupTable = lookupObjResult.GetLookupTable(odpContext);
				Global.Tracer.Assert(lookupTable != null, "LookupTable must not be null");
				ObjectModelImpl reportObjectModel = odpContext.ReportObjectModel;
				ChunkManager.DataChunkReader dataChunkReader = null;
				if (flag2)
				{
					dataChunkReader = odpContext.GetDataChunkReader(dataSet.IndexInCollection);
				}
				using (reportObjectModel.SetupNewFieldsWithBackup(dataSet, dataSetInstance, dataChunkReader))
				{
					object[] array = variantResult.Value as object[];
					if (array == null)
					{
						array = new object[] { variantResult.Value };
					}
					else
					{
						list = new List<object>(array.Length);
					}
					foreach (object obj in array)
					{
						LookupMatches lookupMatches;
						if (lookupTable.TryGetValue(obj, out lookupMatches))
						{
							int num = (flag ? 1 : lookupMatches.MatchCount);
							if (list == null)
							{
								list = new List<object>(num);
							}
							for (int j = 0; j < num; j++)
							{
								lookupMatches.SetupRow(j, odpContext);
								VariantResult variantResult2 = this.m_lookupInfo.EvaluateResultExpr(this.m_reportRuntime);
								this.CheckExprResultError(variantResult2);
								list.Add(variantResult2.Value);
							}
						}
					}
				}
			}
			finally
			{
				if (compareInfo != null)
				{
					odpContext.SetComparisonInformation(compareInfo, compareOptions, flag3, flag4);
				}
			}
			object[] array3 = LookupImpl.EmptyResult;
			if (list != null)
			{
				array3 = list.ToArray();
			}
			return array3;
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x001D3C54 File Offset: 0x001D1E54
		private void CheckExprResultError(VariantResult result)
		{
			if (result.ErrorOccurred)
			{
				throw new ReportProcessingException_InvalidOperationException();
			}
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x001D3C64 File Offset: 0x001D1E64
		internal string Name
		{
			get
			{
				return this.m_lookupInfo.Name;
			}
		}

		// Token: 0x040039FC RID: 14844
		private LookupInfo m_lookupInfo;

		// Token: 0x040039FD RID: 14845
		private ReportRuntime m_reportRuntime;

		// Token: 0x040039FE RID: 14846
		private static readonly object[] EmptyResult = new object[0];
	}
}
