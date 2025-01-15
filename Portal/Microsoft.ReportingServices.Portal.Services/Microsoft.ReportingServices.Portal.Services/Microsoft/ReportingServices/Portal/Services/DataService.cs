using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Microsoft.ReportingServices.Portal.Services.Util;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.SharedDataSets;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x02000019 RID: 25
	internal sealed class DataService : IDataService
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000B33C File Offset: 0x0000953C
		internal DataService(ISoapRS2010Proxy rs2010Proxy, ISoapRSExecutionProxyFactory rsProxyFactory, ILogger logger, ICatalogRepository rsCatalogRepository)
		{
			if (rs2010Proxy == null)
			{
				throw new ArgumentNullException("rs2010Proxy");
			}
			if (rsProxyFactory == null)
			{
				throw new ArgumentNullException("rsProxyFactory");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (rsCatalogRepository == null)
			{
				throw new ArgumentNullException("rsCatalogRepository");
			}
			this._rs2010Proxy = rs2010Proxy;
			this._rsProxyFactory = rsProxyFactory;
			this._logger = logger;
			this._rsCatalogRepository = rsCatalogRepository;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B3CC File Offset: 0x000095CC
		public DataSetSchema GetDataSetSchema(IPrincipal userPrincipal, Guid key)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = this.ResolveCatalogPath(rsservice, key);
			return DataService.CreateDataSetSchema(this.GetDataSetDefinitionBytes(rsservice, userPrincipal, text));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B405 File Offset: 0x00009605
		internal byte[] GetDataSetDefinitionBytes(RSService rsService, IPrincipal userPrincipal, string path)
		{
			GetDataSetDefinitionAction getDataSetDefinitionAction = rsService.GetDataSetDefinitionAction;
			getDataSetDefinitionAction.ActionParameters.ItemPath = path;
			getDataSetDefinitionAction.ActionParameters.InternalUsePermissionForExecution = true;
			getDataSetDefinitionAction.Execute();
			return getDataSetDefinitionAction.ActionParameters.DataSetDefinition;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B435 File Offset: 0x00009635
		internal static DataSetField CreateDataSetField(Microsoft.ReportingServices.RdlObjectModel.Field field)
		{
			return new DataSetField
			{
				Name = field.Name,
				DataType = SharedDataSetJsonRendering.GetRdlDataType(field.TypeName).ToReportParameterType()
			};
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B460 File Offset: 0x00009660
		internal static DataSetParameterInfo CreateDataSetParameterInfo(Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.DataSetParameter parameter, string dbType, bool isMultiValued)
		{
			string text = ((parameter.DefaultValue != null) ? parameter.DefaultValue.Value.Value : null);
			if (!string.IsNullOrEmpty(text) && parameter.DefaultValue.Value.IsExpression)
			{
				GroupCollection groups = Regex.Match(text, "^=new Object\\(\\) \\{(.*)\\}$").Groups;
				if (groups.Count == 2)
				{
					text = groups[1].Value.Replace("\"", string.Empty);
				}
			}
			return new DataSetParameterInfo
			{
				Name = parameter.Name,
				DefaultValue = text,
				Nullable = parameter.Nullable,
				DataType = SharedDataSetJsonRendering.GetRdlDataType(dbType).ToReportParameterType(),
				IsExpression = (parameter.DefaultValue != null && parameter.DefaultValue.Value.IsExpression),
				IsMultiValued = isMultiValued
			};
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B558 File Offset: 0x00009758
		internal static DataSetSchema CreateDataSetSchema(byte[] dataSetDefinitionBytes)
		{
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(dataSetDefinitionBytes);
			DataSetSchema dataSetSchema = new DataSetSchema
			{
				Name = rdlSharedDataSet.Name
			};
			if (rdlSharedDataSet.DataSet.Fields != null)
			{
				dataSetSchema.Fields = rdlSharedDataSet.DataSet.Fields.Select((Microsoft.ReportingServices.RdlObjectModel.Field x) => DataService.CreateDataSetField(x));
			}
			XElement dataSetSchemaXml = DataService.DeserializeDataSetDefinitionXml(dataSetDefinitionBytes);
			if (rdlSharedDataSet.DataSet.Query.DataSetParameters != null)
			{
				dataSetSchema.Parameters = rdlSharedDataSet.DataSet.Query.DataSetParameters.Where((Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.DataSetParameter x) => !x.ReadOnly).Select(delegate(Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.DataSetParameter x)
				{
					string designerParameterPropertyValue = SharedDataSetJsonRendering.GetDesignerParameterPropertyValue(dataSetSchemaXml, x.Name, "DbType");
					bool flag = false;
					bool.TryParse(SharedDataSetJsonRendering.GetDesignerParameterPropertyValue(dataSetSchemaXml, x.Name, "IsMultiValued"), out flag);
					return DataService.CreateDataSetParameterInfo(x, designerParameterPropertyValue, flag);
				});
			}
			return dataSetSchema;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B630 File Offset: 0x00009830
		internal static XElement DeserializeDataSetDefinitionXml(byte[] dataSetDefinition)
		{
			XElement xelement = null;
			using (MemoryStream memoryStream = new MemoryStream(dataSetDefinition))
			{
				xelement = XElement.Load(memoryStream);
			}
			return xelement;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B66C File Offset: 0x0000986C
		public string GetDataSetTableJson(IPrincipal userPrincipal, Guid key, int? maxRows, out bool fromJsonCache)
		{
			return this.GetDataSetTableJson(userPrincipal, key, new global::Model.DataSetParameter[0], maxRows, out fromJsonCache);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B680 File Offset: 0x00009880
		public string GetDataSetColumnJson(IPrincipal userPrincipal, Guid key, IEnumerable<global::Model.DataSetParameter> parameterOverrides, string columnName, int sampledRows)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = this.ResolveCatalogPath(rsservice, key);
			byte[] dataSetDefinitionBytes = this.GetDataSetDefinitionBytes(rsservice, userPrincipal, text);
			DataSetSchema dataSetSchema = DataService.CreateDataSetSchema(dataSetDefinitionBytes);
			DataService.CheckRequiredDataSetParameters(dataSetSchema, parameterOverrides);
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(dataSetDefinitionBytes);
			rdlSharedDataSet.DataSet.Fields = rdlSharedDataSet.DataSet.Fields.Where((Microsoft.ReportingServices.RdlObjectModel.Field x) => x.Name == columnName).ToArray<Microsoft.ReportingServices.RdlObjectModel.Field>();
			return DataSetTable.GetDataSetValuesJson<double?>(this.GetDataSetTableJsonInternal(userPrincipal, rdlSharedDataSet, text, dataSetSchema, parameterOverrides, null, false), new int?(sampledRows));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B71C File Offset: 0x0000991C
		private static byte[] DecompressGZipBytes(byte[] compressedBytes)
		{
			byte[] array = new byte[4];
			Array.Copy(compressedBytes, compressedBytes.Length - 4, array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream(compressedBytes))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					if (num < 2147483647)
					{
						array2 = new byte[num];
						gzipStream.Read(array2, 0, array2.Length);
					}
					else
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							gzipStream.CopyTo(memoryStream2);
							array2 = memoryStream2.ToArray();
						}
					}
				}
			}
			return array2;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B7DC File Offset: 0x000099DC
		public string GetDataSetTableJson(IPrincipal userPrincipal, Guid key, IEnumerable<global::Model.DataSetParameter> parameterOverrides, int? maxRows, out bool fromJsonCache)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = this.ResolveCatalogPath(rsservice, key);
			fromJsonCache = false;
			DataService.CheckMaxRows(maxRows);
			byte[] dataSetDefinitionBytes = this.GetDataSetDefinitionBytes(rsservice, userPrincipal, text);
			DataSetSchema dataSetSchema = DataService.CreateDataSetSchema(dataSetDefinitionBytes);
			DataService.CheckRequiredDataSetParameters(dataSetSchema, parameterOverrides);
			if (maxRows == null)
			{
				byte[] cachedDataSetJson = this.GetCachedDataSetJson(rsservice, key, parameterOverrides, dataSetSchema, userPrincipal, text);
				if (cachedDataSetJson != null)
				{
					fromJsonCache = true;
					return ConversionUtils.Utf8BytesToString(DataService.DecompressGZipBytes(cachedDataSetJson));
				}
			}
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(dataSetDefinitionBytes);
			bool flag = SharedDataSetRendering.GetDataSourceExtensionForDataSet(rsservice, text).Equals("OLEDB-MD", StringComparison.OrdinalIgnoreCase);
			return this.GetDataSetTableJsonInternal(userPrincipal, rdlSharedDataSet, text, dataSetSchema, parameterOverrides, maxRows, flag);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B874 File Offset: 0x00009A74
		private static byte[] CompressUtf8String(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public byte[] GetCompressedDataSetTableJson(IPrincipal userPrincipal, Guid key, IEnumerable<global::Model.DataSetParameter> parameterOverrides, int? maxRows, out bool fromJsonCache)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = this.ResolveCatalogPath(rsservice, key);
			fromJsonCache = false;
			DataService.CheckMaxRows(maxRows);
			byte[] dataSetDefinitionBytes = this.GetDataSetDefinitionBytes(rsservice, userPrincipal, text);
			DataSetSchema dataSetSchema = DataService.CreateDataSetSchema(dataSetDefinitionBytes);
			DataService.CheckRequiredDataSetParameters(dataSetSchema, parameterOverrides);
			if (maxRows == null)
			{
				byte[] cachedDataSetJson = this.GetCachedDataSetJson(rsservice, key, parameterOverrides, dataSetSchema, userPrincipal, text);
				if (cachedDataSetJson != null)
				{
					fromJsonCache = true;
					return cachedDataSetJson;
				}
			}
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(dataSetDefinitionBytes);
			bool flag = SharedDataSetRendering.GetDataSourceExtensionForDataSet(rsservice, text).Equals("OLEDB-MD", StringComparison.OrdinalIgnoreCase);
			return DataService.CompressUtf8String(this.GetDataSetTableJsonInternal(userPrincipal, rdlSharedDataSet, text, dataSetSchema, parameterOverrides, maxRows, flag));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B978 File Offset: 0x00009B78
		public string GetDataSetAggregatedValuesJson(IPrincipal userPrincipal, Guid key, IEnumerable<global::Model.DataSetParameter> parameterOverrides, string columnName, global::Model.KpiSharedDataItemAggregation aggregation)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = this.ResolveCatalogPath(rsservice, key);
			byte[] dataSetDefinitionBytes = this.GetDataSetDefinitionBytes(rsservice, userPrincipal, text);
			DataSetSchema dataSetSchema = DataService.CreateDataSetSchema(dataSetDefinitionBytes);
			DataService.CheckRequiredDataSetParameters(dataSetSchema, parameterOverrides);
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(dataSetDefinitionBytes);
			rdlSharedDataSet.DataSet.Fields = rdlSharedDataSet.DataSet.Fields.Where((Microsoft.ReportingServices.RdlObjectModel.Field x) => x.Name == columnName).ToArray<Microsoft.ReportingServices.RdlObjectModel.Field>();
			return DataSetTable.GetDataSetValuesJson<string>(this.GetDataSetAggregatedValuesInternal(userPrincipal, rdlSharedDataSet, text, dataSetSchema, parameterOverrides, aggregation), null);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BA0C File Offset: 0x00009C0C
		internal string GetDataSetTableJsonInternal(IPrincipal userPrincipal, Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet sharedDataSet, string path, DataSetSchema datasetSchema, IEnumerable<global::Model.DataSetParameter> parameterOverrides, int? maxRows, bool usingAS)
		{
			Microsoft.ReportingServices.RdlObjectModel.Report tablixBasedRdlReport = SharedDataSetJsonRendering.GetTablixBasedRdlReport(maxRows);
			List<string> list = parameterOverrides.Select((global::Model.DataSetParameter x) => x.Name).ToList<string>();
			Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet = SharedDataSetJsonRendering.CreateRdlDataSet(sharedDataSet, path, list);
			SharedDataSetJsonRendering.CreateRdlFieldBindings(tablixBasedRdlReport, dataSet, usingAS);
			DataService.BindDataSetParameters(tablixBasedRdlReport, dataSet, datasetSchema, parameterOverrides);
			return this.RenderReport(userPrincipal, tablixBasedRdlReport, path, maxRows);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000BA78 File Offset: 0x00009C78
		internal string GetDataSetAggregatedValuesInternal(IPrincipal userPrincipal, Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet sharedDataSet, string path, DataSetSchema datasetSchema, IEnumerable<global::Model.DataSetParameter> parameterOverrides, global::Model.KpiSharedDataItemAggregation aggregation)
		{
			Microsoft.ReportingServices.RdlObjectModel.Report emptyRdlReport = SharedDataSetJsonRendering.GetEmptyRdlReport();
			List<string> list = parameterOverrides.Select((global::Model.DataSetParameter x) => x.Name).ToList<string>();
			Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet = SharedDataSetJsonRendering.CreateRdlDataSet(sharedDataSet, path, list);
			SharedDataSetJsonRendering.CreateRdlAggregatedFieldBindings(emptyRdlReport, dataSet, KpiAggregation.ToRdlFunctionName(aggregation.ToServiceAggregationType()));
			DataService.BindDataSetParameters(emptyRdlReport, dataSet, datasetSchema, parameterOverrides);
			return this.RenderReport(userPrincipal, emptyRdlReport, path, null);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		internal byte[] GetCachedDataSetJson(RSService rsService, Guid key, IEnumerable<global::Model.DataSetParameter> parameterOverrides, DataSetSchema dataSetSchema, IPrincipal userPrincipal, string path)
		{
			ItemParameter[] array = new ItemParameter[0];
			if (dataSetSchema.Parameters.Any<DataSetParameterInfo>())
			{
				Dictionary<string, ReportParameterType> parameterTypes = this._rs2010Proxy.GetParameterTypes(userPrincipal, path);
				IEnumerable<Microsoft.SqlServer.ReportingServices2010.ParameterValue> enumerable = Enumerable.Empty<Microsoft.SqlServer.ReportingServices2010.ParameterValue>();
				enumerable = parameterOverrides.Select(delegate(global::Model.DataSetParameter parameterValue)
				{
					if (parameterTypes.ContainsKey(parameterValue.Name))
					{
						return parameterValue.ToSoapParameterValue(parameterTypes[parameterValue.Name]);
					}
					throw new MissingParameterException(parameterValue.Name);
				});
				array = this._rs2010Proxy.GetItemParameters(userPrincipal, path, null, true, enumerable.ToArray<Microsoft.SqlServer.ReportingServices2010.ParameterValue>(), null);
			}
			byte[] array3;
			using (new RSServiceStorageAccess(rsService))
			{
				ParameterInfoCollection parameterInfoCollection = DataService.GetParameterInfoCollection(array, parameterOverrides);
				int invariantHashCode = StackTraceUtil.GetInvariantHashCode<char>(parameterInfoCollection.ToXml(true));
				DateTime now = DateTime.Now;
				byte[] array2;
				int num;
				bool flag = rsService.Storage.TryGetContentCache(key, invariantHashCode, DBInterface.ContentCacheTypes.SharedDataSetJson, out array2, out num);
				if (flag && array2 != null && num != 1)
				{
					this._logger.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Unexpected version {0} when fetching Json cache for shared dataSet. Expected version {1}", num, 1));
					array3 = null;
				}
				else
				{
					if (flag)
					{
						ExecutionLogInfo executionLogInfo = new ExecutionLogInfo
						{
							Source = global::Model.ExecutionLogExecType.Cache,
							ItemPath = path,
							StartTime = now,
							EndTime = DateTime.Now,
							Status = 0L,
							Format = "SHAREDDATASETJSON",
							Parameters = parameterInfoCollection.ToUrl(false),
							EventType = ExecutionLogEventType.Execute,
							ByteCount = (long)array2.Length
						};
						this._rsCatalogRepository.AddExecutionLogInfo(userPrincipal, executionLogInfo);
					}
					array3 = array2;
				}
			}
			return array3;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000BC6C File Offset: 0x00009E6C
		internal static ParameterInfoCollection GetParameterInfoCollection(ItemParameter[] parameters, IEnumerable<global::Model.DataSetParameter> parameterOverrides)
		{
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			for (int i = 0; i < parameters.Length; i++)
			{
				ItemParameter parameter = parameters[i];
				ParameterInfo parameterInfo = new ParameterInfo();
				parameterInfo.UsedInQuery = parameter.QueryParameter;
				parameterInfo.DataType = DataType.Object;
				parameterInfo.Name = parameter.Name;
				object[] array = parameter.DefaultValues;
				parameterInfo.DefaultValues = array;
				parameterInfo.Nullable = parameter.Nullable;
				parameterInfo.MultiValue = parameter.MultiValue;
				array = parameter.DefaultValues;
				parameterInfo.Values = array;
				ParameterInfo parameterInfo2 = parameterInfo;
				global::Model.DataSetParameter dataSetParameter = parameterOverrides.FirstOrDefault((global::Model.DataSetParameter m) => m.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase));
				if (dataSetParameter != null)
				{
					ParameterInfo parameterInfo3 = parameterInfo2;
					array = ConversionUtils.CsvToValuesList(dataSetParameter.Value, ',', false).ToArray<string>();
					parameterInfo3.Values = array;
				}
				parameterInfoCollection.Add(parameterInfo2);
			}
			return parameterInfoCollection;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000BD60 File Offset: 0x00009F60
		internal static void CheckRequiredDataSetParameters(DataSetSchema dataSetSchema, IEnumerable<global::Model.DataSetParameter> parameterOverrides)
		{
			IEnumerable<DataSetParameterInfo> enumerable = dataSetSchema.Parameters.Where((DataSetParameterInfo x) => string.IsNullOrEmpty(x.DefaultValue) && !x.Nullable);
			if (!enumerable.Any<DataSetParameterInfo>())
			{
				return;
			}
			IEnumerable<global::Model.DataSetParameter> overriden = parameterOverrides.Where((global::Model.DataSetParameter x) => x.Value != null);
			IEnumerable<DataSetParameterInfo> enumerable2 = enumerable.Where((DataSetParameterInfo m) => !overriden.Select((global::Model.DataSetParameter o) => o.Name.ToLowerInvariant()).Contains(m.Name.ToLowerInvariant()));
			if (enumerable2.Any<DataSetParameterInfo>())
			{
				throw new DataSetParameterValueNotSetException(enumerable2.First<DataSetParameterInfo>().Name);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000BE00 File Offset: 0x0000A000
		internal static void BindDataSetParameters(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, DataSetSchema datasetSchema, IEnumerable<global::Model.DataSetParameter> parameters)
		{
			int num = 0;
			using (IEnumerator<global::Model.DataSetParameter> enumerator = parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					global::Model.DataSetParameter parameter = enumerator.Current;
					DataSetParameterInfo dataSetParameterInfo = datasetSchema.Parameters.FirstOrDefault((DataSetParameterInfo x) => x.Name == parameter.Name);
					bool flag = dataSetParameterInfo != null && dataSetParameterInfo.IsMultiValued;
					if (!string.IsNullOrEmpty(parameter.Value) && !Regex.IsMatch(parameter.Value, "^=new Object\\(\\) \\{(.*)\\}$") && flag)
					{
						string[] array = ConversionUtils.CsvToValuesList(parameter.Value, ',', true).ToArray<string>();
						if (array.Length > 1)
						{
							parameter.Value = "=new Object() {" + string.Join(",", array) + "}";
						}
					}
					if (SharedDataSetJsonRendering.TryAddDataSetParameterToReport(report, dataSet, parameter.Name, parameter.Value, flag, num))
					{
						num++;
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000BF1C File Offset: 0x0000A11C
		internal static void CheckMaxRows(int? maxRows)
		{
			if (maxRows != null && maxRows.Value < 1)
			{
				throw new ArgumentOutOfRangeException(SR.Error_DataSetMaxRowsLessThanOne);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BF3C File Offset: 0x0000A13C
		internal string RenderRdlToJson(IPrincipal userPrincipal, byte[] rdlBytes, string deviceInfo, string path)
		{
			ISoapRSExecutionProxy soapRSExecutionProxy = this._rsProxyFactory.CreateSoapRSExecutionProxy(userPrincipal);
			soapRSExecutionProxy.Timeout = 3600000;
			CreateReportEditSessionResult editSession = null;
			byte[] array = null;
			try
			{
				string[] array2 = path.Split(new char[] { '/' });
				string text = array2.Last<string>();
				string text2 = string.Join("/", array2.Take(array2.Length - 1));
				if (string.IsNullOrWhiteSpace(text2))
				{
					text2 = "/";
				}
				editSession = this._rs2010Proxy.CreateReportEditSession(userPrincipal, text, text2, rdlBytes);
				soapRSExecutionProxy.LoadReport(userPrincipal, editSession.Report, null);
				array = soapRSExecutionProxy.Render(userPrincipal, "SHAREDDATASETJSON", deviceInfo).ByteContents;
			}
			catch (Exception ex)
			{
				string text3 = string.Format(CultureInfo.InvariantCulture, SR.Error_DataSetSoapError, ex.Message);
				this._logger.Trace(TraceLevel.Error, text3);
				throw new DataSetRenderingSoapException(text3, ex);
			}
			finally
			{
				if (editSession != null)
				{
					this.ScheduleAsyncAction(delegate
					{
						this.DeleteSession(userPrincipal, editSession.Report);
					});
				}
			}
			return ConversionUtils.Utf8BytesToString(array);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C088 File Offset: 0x0000A288
		internal void DeleteSession(IPrincipal userPrincipal, string itemPath)
		{
			try
			{
				if (this._rs2010Proxy != null && userPrincipal != null)
				{
					this._rs2010Proxy.DeleteItem(userPrincipal, itemPath);
					this._logger.Trace(TraceLevel.Verbose, string.Format(CultureInfo.InvariantCulture, "Successfully deleted item {0} async", itemPath));
				}
			}
			catch (Exception ex)
			{
				this._logger.Trace(TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, "Error when deleting item {0} async: {1}", itemPath, ex.Message));
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C100 File Offset: 0x0000A300
		internal string RenderReport(IPrincipal userPrincipal, Microsoft.ReportingServices.RdlObjectModel.Report rdlReport, string path, int? maxRows)
		{
			string text = "<DeviceInfo><SharedDataSetTable>true</SharedDataSetTable></DeviceInfo>";
			if (maxRows != null)
			{
				XElement xelement = XElement.Parse(text);
				xelement.Add(new XElement("MaxRows", maxRows.Value));
				text = xelement.ToString();
			}
			RdlSerializer rdlSerializer = SharedDataSetJsonRendering.CreateRdlSerializer();
			byte[] reportBytes;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				rdlSerializer.Serialize(memoryStream, rdlReport);
				reportBytes = memoryStream.ToArray();
				this._logger.Trace(TraceLevel.Verbose, () => Encoding.UTF8.GetString(reportBytes.ToArray<byte>()));
			}
			return this.RenderRdlToJson(userPrincipal, reportBytes, text, path);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		private string ResolveCatalogPath(RSService rsService, Guid key)
		{
			string text = null;
			CatalogItemPath pathById;
			using (new RSServiceStorageAccess(rsService))
			{
				pathById = rsService.Storage.GetPathById(key);
			}
			if (pathById != null)
			{
				text = pathById.Value;
				if (string.IsNullOrEmpty(text))
				{
					text = "/";
				}
			}
			return text;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C20C File Offset: 0x0000A40C
		private Guid ResolveCatalogId(RSService rsService, string path)
		{
			Guid guid;
			using (new RSServiceStorageAccess(rsService))
			{
				string text;
				rsService.ResolveCatalogItem(Guid.Empty, path, out guid, out text);
			}
			return guid;
		}

		// Token: 0x04000071 RID: 113
		private readonly ISoapRS2010Proxy _rs2010Proxy;

		// Token: 0x04000072 RID: 114
		private readonly ISoapRSExecutionProxyFactory _rsProxyFactory;

		// Token: 0x04000073 RID: 115
		private readonly ICatalogRepository _rsCatalogRepository;

		// Token: 0x04000074 RID: 116
		private readonly ILogger _logger;

		// Token: 0x04000075 RID: 117
		internal Action<global::System.Action> ScheduleAsyncAction = delegate(global::System.Action a)
		{
			global::System.Threading.Tasks.Task.Run(a);
		};

		// Token: 0x04000076 RID: 118
		private const string MultiValueRegExString = "^=new Object\\(\\) \\{(.*)\\}$";
	}
}
