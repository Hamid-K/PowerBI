using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Web;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000099 RID: 153
	internal sealed class ServerParameterStore
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public ServerParameterStore(ConnectionManager connection)
		{
			this.m_store = new ServerParameterStore.ParameterInstancesStore();
			this.m_store.ConnectionManager = connection;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00019DD4 File Offset: 0x00017FD4
		public NameValueCollection Store(ICatalogItemContext itemPath, NameValueCollection paramsInstance, bool[] sharedParameters, out bool replaced)
		{
			CatalogItemContext catalogItemContext = itemPath as CatalogItemContext;
			RSTrace.CatalogTrace.Assert(catalogItemContext != null, "itemContext");
			return this.Store(catalogItemContext.ItemPath, paramsInstance, sharedParameters, out replaced);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00019E0B File Offset: 0x0001800B
		public NameValueCollection Store(ExternalItemPath itemPath, NameValueCollection paramsInstance, bool[] sharedParameters, out bool replaced)
		{
			return this.m_store.StoreReportParameters(itemPath, paramsInstance, sharedParameters, out replaced);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00019E1D File Offset: 0x0001801D
		public void Get(string parametersId, out ExternalItemPath itemPath, out NameValueCollection parameters)
		{
			this.m_store.GetReportParameterDefinitions(parametersId, out itemPath, out parameters);
		}

		// Token: 0x0400034B RID: 843
		private readonly ServerParameterStore.ParameterInstancesStore m_store;

		// Token: 0x0200045B RID: 1115
		internal sealed class ParameterInstancesStore : Storage
		{
			// Token: 0x06002331 RID: 9009 RVA: 0x00016246 File Offset: 0x00014446
			internal ParameterInstancesStore()
			{
			}

			// Token: 0x06002332 RID: 9010 RVA: 0x00083800 File Offset: 0x00081A00
			internal void GetReportParameterDefinitions(string paramsInstanceId, out ExternalItemPath itemPath, out NameValueCollection parameters)
			{
				parameters = null;
				string text;
				string text2;
				this.GetServerParamsInstanceFromDb(paramsInstanceId, out itemPath, out text, out text2);
				if (text != null || text2 != null)
				{
					NameValueCollection asNameValueCollectionInUserCulture = ParameterInfoCollection.DecodeFromXml(text).AsNameValueCollectionInUserCulture;
					NameValueCollection asNameValueCollectionInUserCulture2 = ParameterInfoCollection.DecodeFromXml(text2).AsNameValueCollectionInUserCulture;
					foreach (object obj in asNameValueCollectionInUserCulture2.Keys)
					{
						string text3 = (string)obj;
						asNameValueCollectionInUserCulture.Add(text3, asNameValueCollectionInUserCulture2[text3]);
					}
					parameters = asNameValueCollectionInUserCulture;
				}
			}

			// Token: 0x06002333 RID: 9011 RVA: 0x0008389C File Offset: 0x00081A9C
			internal NameValueCollection StoreReportParameters(ExternalItemPath itemPath, NameValueCollection paramsInstance, bool[] sharedParameters, out bool replaced)
			{
				NameValueCollection nameValueCollection2;
				NameValueCollection nameValueCollection = RSRequestParameters.ExtractReportParameters(paramsInstance, ref sharedParameters, out nameValueCollection2);
				replaced = false;
				if (!this.IsTooLong(nameValueCollection))
				{
					return paramsInstance;
				}
				NameValueCollection nameValueCollection3;
				NameValueCollection nameValueCollection4;
				this.SplitParameters(nameValueCollection, sharedParameters, out nameValueCollection3, out nameValueCollection4);
				ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(nameValueCollection3);
				ParameterInfoCollection parameterInfoCollection2 = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(nameValueCollection4);
				string text = this.StoreParamsInstance(itemPath, parameterInfoCollection, parameterInfoCollection2);
				NameValueCollection nameValueCollection5 = new NameValueCollection();
				nameValueCollection5.Add("rs:StoredParametersID", text);
				nameValueCollection5.Add(nameValueCollection2);
				replaced = true;
				return nameValueCollection5;
			}

			// Token: 0x06002334 RID: 9012 RVA: 0x0008390C File Offset: 0x00081B0C
			private void SplitParameters(NameValueCollection reportParams, bool[] whichParamsAreShared, out NameValueCollection commonParams, out NameValueCollection instanceParams)
			{
				commonParams = new NameValueCollection();
				instanceParams = new NameValueCollection();
				if (whichParamsAreShared == null)
				{
					commonParams = reportParams;
					return;
				}
				if (whichParamsAreShared.Length != reportParams.Count)
				{
					throw new InternalCatalogException(string.Concat(new object[] { "Expected same length for arrays, got: ", whichParamsAreShared.Length, "and ", reportParams.Count }));
				}
				for (int i = 0; i < reportParams.Count; i++)
				{
					string[] values = reportParams.GetValues(i);
					if (whichParamsAreShared != null && whichParamsAreShared[i])
					{
						for (int j = 0; j < values.Length; j++)
						{
							commonParams.Add(reportParams.Keys[i], values[j]);
						}
					}
					else
					{
						for (int k = 0; k < values.Length; k++)
						{
							instanceParams.Add(reportParams.Keys[i], values[k]);
						}
					}
				}
			}

			// Token: 0x06002335 RID: 9013 RVA: 0x000839E0 File Offset: 0x00081BE0
			private string StoreParamsInstance(ExternalItemPath itemPath, ParameterInfoCollection parentParams, ParameterInfoCollection childParams)
			{
				string text = UrlFriendlyUIDGenerator.Create();
				string text2 = UrlFriendlyUIDGenerator.Create();
				this.StoreParamsInstanceToDb(text, text2, itemPath, parentParams, childParams, Global.StoredParametersLifetime);
				if (childParams.Count > 0)
				{
					return text2;
				}
				return text;
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x00083A18 File Offset: 0x00081C18
			private byte[] Compress(string source)
			{
				if (source == null)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				byte[] array;
				using (Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
				{
					StreamWriter streamWriter = new StreamWriter(stream);
					streamWriter.Write(source);
					streamWriter.Flush();
					stream.Close();
					array = memoryStream.ToArray();
				}
				return array;
			}

			// Token: 0x06002337 RID: 9015 RVA: 0x00083A78 File Offset: 0x00081C78
			private string Decompress(byte[] source)
			{
				if (source == null)
				{
					return null;
				}
				string text;
				using (Stream stream = new DeflateStream(new MemoryStream(source), CompressionMode.Decompress, true))
				{
					text = new StreamReader(stream).ReadToEnd();
				}
				return text;
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x00083AC4 File Offset: 0x00081CC4
			private bool NeedToStoreCommonParams(ParameterInfoCollection commonParams, ref string paramId)
			{
				ParameterInfoCollection parameterInfo = this.m_parameterInfo;
				if (parameterInfo == null)
				{
					this.m_parameterInfo = commonParams;
					this.m_parameterId = paramId;
					return true;
				}
				bool flag2;
				bool flag = (flag2 = false);
				if (parameterInfo.Count == commonParams.Count)
				{
					parameterInfo.SameParameters(commonParams, out flag2, out flag);
				}
				if (flag2 && flag)
				{
					paramId = this.m_parameterId;
					return false;
				}
				this.m_parameterInfo = commonParams;
				this.m_parameterId = paramId;
				return true;
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x00083B28 File Offset: 0x00081D28
			private void StoreParamsInstanceToDb(string parentSessionId, string childSessionId, ExternalItemPath itemPath, ParameterInfoCollection commonParameters, ParameterInfoCollection instanceParameters, int timeout)
			{
				DateTime now = DateTime.Now;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("StoreServerParameters", null))
				{
					if (instanceParameters.Count == 0 || this.NeedToStoreCommonParams(commonParameters, ref parentSessionId))
					{
						byte[] array = this.Compress(commonParameters.ToXml(false));
						instrumentedSqlCommand.AddParameter("@ServerParametersID", SqlDbType.NVarChar, parentSessionId);
						instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, itemPath.Value);
						instrumentedSqlCommand.AddParameter("@CurrentDate", SqlDbType.DateTime, now);
						instrumentedSqlCommand.AddParameter("@Timeout", SqlDbType.Int, timeout);
						instrumentedSqlCommand.AddParameter("@Expiration", SqlDbType.DateTime, now.AddDays((double)timeout));
						instrumentedSqlCommand.AddParameter("@ParametersValues", SqlDbType.Image, array);
						instrumentedSqlCommand.ExecuteNonQuery();
					}
					if (instanceParameters.Count > 0)
					{
						using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("StoreServerParameters", null))
						{
							byte[] array2 = this.Compress(instanceParameters.ToXml(false));
							instrumentedSqlCommand2.AddParameter("@ServerParametersID", SqlDbType.NVarChar, childSessionId);
							instrumentedSqlCommand2.AddParameter("@Path", SqlDbType.NVarChar, itemPath.Value);
							instrumentedSqlCommand2.AddParameter("@CurrentDate", SqlDbType.DateTime, now);
							instrumentedSqlCommand2.AddParameter("@Timeout", SqlDbType.Int, timeout);
							instrumentedSqlCommand2.AddParameter("@Expiration", SqlDbType.DateTime, now.AddDays((double)timeout));
							instrumentedSqlCommand2.AddParameter("@ParametersValues", SqlDbType.Image, array2);
							instrumentedSqlCommand2.AddParameter("@ParentParametersID", SqlDbType.NVarChar, parentSessionId);
							instrumentedSqlCommand2.ExecuteNonQuery();
						}
					}
				}
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x00083CEC File Offset: 0x00081EEC
			private void GetServerParamsInstanceFromDb(string sessionId, out ExternalItemPath itemPath, out string reportItemparameters, out string commonParameters)
			{
				itemPath = null;
				reportItemparameters = null;
				commonParameters = null;
				byte[] array = null;
				byte[] array2 = null;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetServerParameters", null))
				{
					instrumentedSqlCommand.AddParameter("@ServerParametersID", SqlDbType.NText, sessionId);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						if (!dataReader.Read())
						{
							return;
						}
						itemPath = new ExternalItemPath(dataReader.GetString(0));
						array = DataReaderHelper.ReadAllBytes(dataReader, 1);
						if (!dataReader.IsDBNull(2))
						{
							array2 = DataReaderHelper.ReadAllBytes(dataReader, 2);
						}
					}
				}
				reportItemparameters = this.Decompress(array);
				if (array2 != null)
				{
					commonParameters = this.Decompress(array2);
				}
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x00083DA4 File Offset: 0x00081FA4
			private bool IsTooLong(NameValueCollection paramsInstance)
			{
				int num = 0;
				for (int i = 0; i < paramsInstance.Count; i++)
				{
					string key = paramsInstance.GetKey(i);
					string[] values = paramsInstance.GetValues(i);
					if (values != null)
					{
						foreach (string text in values)
						{
							num += HttpUtility.UrlEncode(key).Length;
							num++;
							num += HttpUtility.UrlEncode(text).Length;
							num++;
						}
					}
					if (num >= Global.StoredParametersThreshold)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04000FA6 RID: 4006
			private ParameterInfoCollection m_parameterInfo;

			// Token: 0x04000FA7 RID: 4007
			private string m_parameterId;
		}
	}
}
