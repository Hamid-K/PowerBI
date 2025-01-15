using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.SharedDataSets;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000AD RID: 173
	internal sealed class JsonContentCacheManager : IContentCacheManager
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00020828 File Offset: 0x0001EA28
		private static byte[] LoadJsonSharedDataSetBytes(string dataSetPath, ParameterInfoCollection queryParameters)
		{
			RSService rsservice = new RSService(false);
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(SharedDataSetRendering.LoadDataSetDefinition(rsservice, dataSetPath));
			string[] parameterNames = queryParameters.GetParameterNames();
			Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet = SharedDataSetJsonRendering.CreateRdlDataSet(rdlSharedDataSet, dataSetPath, parameterNames);
			Microsoft.ReportingServices.RdlObjectModel.Report tablixBasedRdlReport = SharedDataSetJsonRendering.GetTablixBasedRdlReport(null);
			bool flag = SharedDataSetRendering.GetDataSourceExtensionForDataSet(rsservice, dataSetPath).Equals("OLEDB-MD", StringComparison.OrdinalIgnoreCase);
			SharedDataSetJsonRendering.CreateRdlFieldBindings(tablixBasedRdlReport, dataSet, flag);
			SharedDataSetJsonRendering.BindDataSetParameters(tablixBasedRdlReport, dataSet, queryParameters);
			return SharedDataSetRendering.GetSharedDataSetBytes(rsservice, tablixBasedRdlReport, dataSet, "SHAREDDATASETJSON", "<DeviceInfo><SharedDataSetTable>true</SharedDataSetTable><JsonCacheCreation>true</JsonCacheCreation></DeviceInfo>");
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002089C File Offset: 0x0001EA9C
		private MemoryStream CompressBytes(byte[] bytes, int offset)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
			{
				gzipStream.Write(bytes, offset, bytes.Length - offset);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000208EC File Offset: 0x0001EAEC
		internal bool NeedsToCache()
		{
			return !this.IsInternalRequest() && this.HasDependentMobileReports();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020900 File Offset: 0x0001EB00
		internal bool IsInternalRequest()
		{
			return this.m_requestParameters != null && this.m_requestParameters.RenderingParameters != null && this.m_requestParameters.RenderingParameters["JsonCacheCreation"] != null && bool.Parse(this.m_requestParameters.RenderingParameters["JsonCacheCreation"]);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00020955 File Offset: 0x0001EB55
		internal bool HasDependentMobileReports()
		{
			return this.m_service.Storage.HasRelatedItem(this.m_dataSetId, 12);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00020970 File Offset: 0x0001EB70
		internal bool IsCached()
		{
			int num = 0;
			if (!this.m_service.Storage.TryGetContentCacheDetails(this.m_dataSetId, this.m_queryParametersHash, DBInterface.ContentCacheTypes.SharedDataSetJson, out num))
			{
				return false;
			}
			if (num != 1)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Unexpected version {0} when fetching Json cache for shared dataSet. Expected version {1}", num, 1));
				return false;
			}
			return true;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000209D0 File Offset: 0x0001EBD0
		public JsonContentCacheManager(Guid dataSetId, string dataSetPath, ParameterInfoCollection queryParameters, IRSRequestParameters requestParameters, IRSService rsService)
		{
			if (dataSetId == Guid.Empty)
			{
				throw new ArgumentException("data set id");
			}
			if (string.IsNullOrEmpty(dataSetPath))
			{
				throw new ArgumentNullException("data set path");
			}
			if (queryParameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (rsService == null)
			{
				throw new ArgumentNullException("rs service");
			}
			this.m_dataSetId = dataSetId;
			this.m_dataSetPath = dataSetPath;
			this.m_queryParameters = queryParameters;
			this.m_requestParameters = requestParameters;
			this.m_service = rsService;
			this.m_queryParametersString = queryParameters.ToXml(true);
			this.m_queryParametersHash = StackTraceUtil.GetInvariantHashCode<char>(this.m_queryParametersString);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00020A6E File Offset: 0x0001EC6E
		public void CreateOrUpdateCacheIfNeededAsync()
		{
			if (this.NeedsToCache() && !this.IsCached())
			{
				RSTrace.RunningJobsTrace.Trace(TraceLevel.Verbose, "Queuing creation of JSON Cache in background thread.");
				ReportServerThreadPool.QueueUserWorkItem(delegate(object o)
				{
					this.CreateOrUpdateCacheInternal();
				}, null);
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00020AA2 File Offset: 0x0001ECA2
		public void CreateOrUpdateCache()
		{
			if (this.NeedsToCache())
			{
				this.CreateOrUpdateCacheInternal();
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		private void CreateOrUpdateCacheInternal()
		{
			try
			{
				RSTrace.RunningJobsTrace.Trace(TraceLevel.Verbose, "Starting JSON Cache creation for Data Set ID: {0} and Path: {1}.", new object[] { this.m_dataSetId, this.m_dataSetPath });
				byte[] array = JsonContentCacheManager.LoadJsonSharedDataSetBytes(this.m_dataSetPath, this.m_queryParameters);
				byte[] preamble = Encoding.UTF8.GetPreamble();
				int num = ((array.Length >= preamble.Length && array.Take(preamble.Length).SequenceEqual(preamble)) ? preamble.Length : 0);
				using (MemoryStream memoryStream = this.CompressBytes(array, num))
				{
					this.m_service.Storage.CreateOrUpdateContentCache(this.m_dataSetId, this.m_queryParametersHash, this.m_queryParametersString, DBInterface.ContentCacheTypes.SharedDataSetJson, 1, memoryStream);
				}
				RSTrace.RunningJobsTrace.Trace(TraceLevel.Verbose, "JSON Cache creation completed for Data Set ID: {0} and Path: {1}.", new object[] { this.m_dataSetId, this.m_dataSetPath });
			}
			catch (Exception ex)
			{
				RSTrace.RunningJobsTrace.Trace(TraceLevel.Error, "Error while creating JSON Cache for Data Set ID: {0} and Path: {1}. Exception: {2}", new object[] { this.m_dataSetId, this.m_dataSetPath, ex.Message });
			}
		}

		// Token: 0x04000405 RID: 1029
		private const string JsonCacheCreationRequestDeviceInfo = "<DeviceInfo><SharedDataSetTable>true</SharedDataSetTable><JsonCacheCreation>true</JsonCacheCreation></DeviceInfo>";

		// Token: 0x04000406 RID: 1030
		internal const string JsonCacheCreationFlag = "JsonCacheCreation";

		// Token: 0x04000407 RID: 1031
		private readonly Guid m_dataSetId;

		// Token: 0x04000408 RID: 1032
		private readonly string m_dataSetPath;

		// Token: 0x04000409 RID: 1033
		private readonly IRSService m_service;

		// Token: 0x0400040A RID: 1034
		private readonly IRSRequestParameters m_requestParameters;

		// Token: 0x0400040B RID: 1035
		private readonly ParameterInfoCollection m_queryParameters;

		// Token: 0x0400040C RID: 1036
		private readonly string m_queryParametersString;

		// Token: 0x0400040D RID: 1037
		private readonly int m_queryParametersHash;
	}
}
