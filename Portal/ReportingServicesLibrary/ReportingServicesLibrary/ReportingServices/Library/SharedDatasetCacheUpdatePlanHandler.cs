using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.SharedDataSets;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000065 RID: 101
	internal sealed class SharedDatasetCacheUpdatePlanHandler : ScheduleFireEventHandlerBase, IEventHandler, IExtension
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool CanSubscribe(ICatalogQuery catalogQuery, string itemName)
		{
			return false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void ValidateSubscriptionData(Microsoft.ReportingServices.Extensions.Subscription subscription, string subscriptionData, UserContext userContext)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void CleanUp(Microsoft.ReportingServices.Extensions.Subscription subscription)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public void HandleEvent(ICatalogQuery catalogQuery, string eventType, string eventData)
		{
			base.HandleScheduleEvent(catalogQuery, eventData, new ScheduleFireEventHandlerBase.PerformEventActions2(this.PerformActionHandler));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000117F6 File Offset: 0x0000F9F6
		public string LocalizedName
		{
			get
			{
				return RepLibRes.SharedDatasetCacheUpdatePlans;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00011800 File Offset: 0x0000FA00
		private void PerformActionHandler(ICatalogQuery catalogQuery, ArrayList reportActions, string dataSetId)
		{
			RSService rsservice = new RSService(false)
			{
				AllowEditSessionItemPaths = true
			};
			try
			{
				string value;
				using (new RSServiceStorageAccess(rsservice))
				{
					value = rsservice.Storage.GetPathById(new Guid(dataSetId)).Value;
				}
				this.UpdateKpis(rsservice, reportActions, dataSetId, value);
			}
			catch (Exception ex)
			{
				RSTrace.ScheduleTracer.TraceException(TraceLevel.Error, ex.Message);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00011884 File Offset: 0x0000FA84
		private void UpdateKpis(RSService rsService, ArrayList reportActions, string dataSetId, string dataSetPath)
		{
			DataSetCache dataSetCache = new DataSetCache(dataSetId, dataSetPath);
			foreach (ItemScheduleAction itemScheduleAction in from ItemScheduleAction x in reportActions
				group x by new
				{
					ItemType = x.ItemType,
					ItemPath = x.ItemPath.Value
				} into x
				where x.Key.ItemType == ItemType.Kpi
				select x.First<ItemScheduleAction>())
			{
				RSTrace.ScheduleTracer.Trace(TraceLevel.Info, "Updating dataset-bound properties for KPI {0}", new object[] { itemScheduleAction.ItemPath });
				CatalogItem catalogItem = this.GetCatalogItem(rsService, itemScheduleAction.ItemPath.Value);
				foreach (KpiPropertyMapItem kpiPropertyMapItem in this.propertyMap)
				{
					if (string.Equals(dataSetId, catalogItem.Properties[kpiPropertyMapItem.DataSetId], StringComparison.OrdinalIgnoreCase))
					{
						KpiDataSetLastUpdateStatus kpiDataSetLastUpdateStatus = KpiDataSetLastUpdateStatus.Success;
						string[] array = null;
						if (string.IsNullOrEmpty(catalogItem.Properties[kpiPropertyMapItem.DataSetColumn]))
						{
							goto IL_02B5;
						}
						string text = this.GenerateKpiSharedDataSetKey(catalogItem, kpiPropertyMapItem);
						RSTrace.ScheduleTracer.Trace(TraceLevel.Verbose, "KPI SharedDataSet Hash Key {0}", new object[] { text });
						if (!dataSetCache.Cache.ContainsKey(text))
						{
							try
							{
								KpiSharedDataItemAggregation kpiSharedDataItemAggregation = (KpiSharedDataItemAggregation)Convert.ToInt32(catalogItem.Properties[kpiPropertyMapItem.DataSetAggregation]);
								byte[] array2 = this.LoadXmlSharedDataSetBytes(rsService, dataSetPath, catalogItem.Properties[kpiPropertyMapItem.DataSetColumn], catalogItem.Properties[kpiPropertyMapItem.DataSetParameters], kpiSharedDataItemAggregation);
								try
								{
									RSTrace.ScheduleTracer.Trace(TraceLevel.Verbose, "Parsing DataSet for KPI {0}", new object[] { catalogItem.Properties.Path });
									if (kpiPropertyMapItem.DataValue == "TrendSet.Value")
									{
										array = XmlRdlParser.GetValues(array2, catalogItem.Properties[kpiPropertyMapItem.DataSetColumn], XmlRdlParserMode.Downsample, 10000);
									}
									else
									{
										array = XmlRdlParser.GetValues(array2, catalogItem.Properties[kpiPropertyMapItem.DataSetColumn], (kpiSharedDataItemAggregation == KpiSharedDataItemAggregation.None) ? XmlRdlParserMode.First : XmlRdlParserMode.Aggregation, 0);
									}
									if (array != null && array.Length != 0)
									{
										dataSetCache.Cache.Add(text, array);
										RSTrace.ScheduleTracer.Trace(TraceLevel.Verbose, "KPI SharedDataSet Hash Key {0} not found in cache, adding value", new object[] { text });
									}
									else
									{
										kpiDataSetLastUpdateStatus = KpiDataSetLastUpdateStatus.DataSetContainedNoData;
									}
								}
								catch (Exception ex)
								{
									kpiDataSetLastUpdateStatus = KpiDataSetLastUpdateStatus.DataSetReportCouldNotBeParsed;
									RSTrace.ScheduleTracer.TraceException(TraceLevel.Error, ex.Message);
								}
								goto IL_02B8;
							}
							catch (Exception ex2)
							{
								kpiDataSetLastUpdateStatus = KpiDataSetLastUpdateStatus.DataSetReportCouldNotBeGenerated;
								RSTrace.ScheduleTracer.TraceException(TraceLevel.Error, ex2.Message);
								goto IL_02B8;
							}
							goto IL_02B5;
						}
						array = dataSetCache.Cache[text];
						RSTrace.ScheduleTracer.Trace(TraceLevel.Verbose, "Found cached value for KPI SharedDataSet Hash Key {0}", new object[] { text });
						IL_02B8:
						this.UpdateKpi(catalogItem, kpiPropertyMapItem, array, kpiDataSetLastUpdateStatus);
						continue;
						IL_02B5:
						kpiDataSetLastUpdateStatus = KpiDataSetLastUpdateStatus.InvalidKpiSharedDataSetColumn;
						goto IL_02B8;
					}
				}
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00011BEC File Offset: 0x0000FDEC
		private void UpdateKpi(CatalogItem catalogItem, KpiPropertyMapItem property, string[] newValues, KpiDataSetLastUpdateStatus status = KpiDataSetLastUpdateStatus.Success)
		{
			List<Property> list2;
			if (status != KpiDataSetLastUpdateStatus.Success)
			{
				RSTrace.ScheduleTracer.Trace(TraceLevel.Warning, "Updating dataset-bound properties for KPI {0} failed", new object[] { catalogItem.Properties.Path });
				List<Property> list = new List<Property>();
				Property property2 = new Property();
				property2.Name = property.DataLastUpdateStatus;
				int num = (int)status;
				property2.Value = num.ToString();
				list.Add(property2);
				list2 = list;
			}
			else if (newValues == null || newValues.Length == 0)
			{
				RSTrace.ScheduleTracer.Trace(TraceLevel.Warning, "Updating dataset-bound properties for KPI {0} failed", new object[] { catalogItem.Properties.Path });
				list2 = new List<Property>
				{
					new Property
					{
						Name = property.DataLastUpdateStatus,
						Value = 1.ToString()
					}
				};
			}
			else
			{
				string text;
				if (property.DataValue == "TrendSet.Value")
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", string.Join(",", newValues));
				}
				else
				{
					text = newValues[0];
				}
				list2 = new List<Property>
				{
					new Property
					{
						Name = property.DataValue,
						Value = text
					},
					new Property
					{
						Name = property.DataLastUpdateStatus,
						Value = 0.ToString()
					}
				};
			}
			ItemProperties itemProperties = new ItemProperties(list2.ToArray(), ItemType.Kpi);
			RSService rsservice = new RSService(false);
			rsservice.WillDisconnectStorage();
			try
			{
				rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
				CatalogItem catalogItem2 = this.GetCatalogItem(rsservice, catalogItem.Properties.Path);
				if (this.VerifyUpdate(catalogItem2, catalogItem, property))
				{
					rsservice.SetPropertiesAction.SetProperties(catalogItem2, itemProperties);
				}
				else
				{
					RSTrace.ScheduleTracer.Trace(TraceLevel.Verbose, "KPI {0} was edited during update, update aborted", new object[] { catalogItem.Properties.Path });
				}
			}
			catch (Exception ex)
			{
				RSTrace.ScheduleTracer.TraceException(TraceLevel.Error, "Error Updating KPI: " + ex.Message);
				rsservice.AbortTransaction();
			}
			finally
			{
				rsservice.DisconnectStorage();
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011DFC File Offset: 0x0000FFFC
		private bool VerifyUpdate(CatalogItem currentCatalogItem, CatalogItem initialCatalogItem, KpiPropertyMapItem property)
		{
			return currentCatalogItem.ItemID.Equals(initialCatalogItem.ItemID) && (string.IsNullOrEmpty(initialCatalogItem.Properties[property.DataItemType]) || (!string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataItemType]) && string.Equals(currentCatalogItem.Properties[property.DataItemType], initialCatalogItem.Properties[property.DataItemType], StringComparison.Ordinal))) && !string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetId]) && string.Equals(currentCatalogItem.Properties[property.DataSetId], initialCatalogItem.Properties[property.DataSetId], StringComparison.Ordinal) && !string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetColumn]) && string.Equals(currentCatalogItem.Properties[property.DataSetColumn], initialCatalogItem.Properties[property.DataSetColumn], StringComparison.Ordinal) && (string.IsNullOrEmpty(initialCatalogItem.Properties[property.DataSetParameters]) || (!string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetParameters]) && string.Equals(currentCatalogItem.Properties[property.DataSetParameters], initialCatalogItem.Properties[property.DataSetParameters], StringComparison.Ordinal))) && (!string.IsNullOrEmpty(initialCatalogItem.Properties[property.DataSetParameters]) || string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetParameters])) && (string.IsNullOrEmpty(initialCatalogItem.Properties[property.DataSetAggregation]) || (!string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetAggregation]) && string.Equals(currentCatalogItem.Properties[property.DataSetAggregation], initialCatalogItem.Properties[property.DataSetAggregation], StringComparison.Ordinal))) && (!string.IsNullOrEmpty(initialCatalogItem.Properties[property.DataSetAggregation]) || string.IsNullOrEmpty(currentCatalogItem.Properties[property.DataSetAggregation]));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012024 File Offset: 0x00010224
		private string GenerateKpiSharedDataSetKey(CatalogItem catalogItem, KpiPropertyMapItem kpiProperty)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(catalogItem.Properties[kpiProperty.DataSetParameters]))
			{
				string[] array = catalogItem.Properties[kpiProperty.DataSetParameters].Split(new char[] { '&' });
				Array.Sort<string>(array);
				stringBuilder.Append(string.Join("&", array));
				stringBuilder.Append("&");
			}
			stringBuilder.Append(string.Format("{0}=", "Col"));
			stringBuilder.Append(catalogItem.Properties[kpiProperty.DataSetColumn]);
			stringBuilder.Append(string.Format("&{0}=", "Agr"));
			if (catalogItem.Properties[kpiProperty.DataSetAggregation] != null)
			{
				stringBuilder.Append(catalogItem.Properties[kpiProperty.DataSetAggregation]);
			}
			else if (kpiProperty.DataValue == "TrendSet.Value")
			{
				stringBuilder.Append(0.ToString());
			}
			else
			{
				stringBuilder.Append(1.ToString());
			}
			return stringBuilder.ToString().GetHashCode().ToString();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012150 File Offset: 0x00010350
		private static NameValueCollection GetParameterCollection(string parameters)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (!string.IsNullOrEmpty(parameters))
			{
				string[] array = parameters.Split(new char[] { '&' });
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[] { '=' });
					if (array2.Length == 2)
					{
						string text = Uri.UnescapeDataString(array2[0]);
						string text2 = Uri.UnescapeDataString(array2[1]);
						nameValueCollection.Add(text, text2);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000121C4 File Offset: 0x000103C4
		private byte[] LoadXmlSharedDataSetBytes(RSService rsService, string dataSetPath, string columnName, string parameters, KpiSharedDataItemAggregation aggregation)
		{
			byte[] array = SharedDataSetRendering.LoadDataSetDefinition(rsService, dataSetPath);
			Microsoft.ReportingServices.RdlObjectModel.SharedDataSets.SharedDataSet rdlSharedDataSet = SharedDataSetJsonRendering.GetRdlSharedDataSet(array);
			IDictionary<string, bool> dataSetParameterCardinalities = this.GetDataSetParameterCardinalities(array);
			rdlSharedDataSet.DataSet.Fields = rdlSharedDataSet.DataSet.Fields.Where((Microsoft.ReportingServices.RdlObjectModel.Field x) => x.Name == columnName).ToArray<Microsoft.ReportingServices.RdlObjectModel.Field>();
			Microsoft.ReportingServices.RdlObjectModel.Report report = ((aggregation == KpiSharedDataItemAggregation.None) ? SharedDataSetJsonRendering.GetTablixBasedRdlReport(null) : SharedDataSetJsonRendering.GetEmptyRdlReport());
			NameValueCollection parameterCollection = SharedDatasetCacheUpdatePlanHandler.GetParameterCollection(parameters);
			Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet = SharedDataSetJsonRendering.CreateRdlDataSet(rdlSharedDataSet, dataSetPath, parameterCollection.AllKeys);
			if (aggregation == KpiSharedDataItemAggregation.None)
			{
				SharedDataSetJsonRendering.CreateRdlFieldBindings(report, dataSet, false);
			}
			else
			{
				SharedDataSetJsonRendering.CreateRdlAggregatedFieldBindings(report, dataSet, KpiAggregation.ToRdlFunctionName(aggregation));
			}
			if (parameterCollection.Count > 0)
			{
				this.BindDataSetParameters(report, dataSet, parameterCollection, dataSetParameterCardinalities);
			}
			return SharedDataSetRendering.GetSharedDataSetBytes(rsService, report, dataSet, "XML", null);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000122A0 File Offset: 0x000104A0
		private void BindDataSetParameters(Microsoft.ReportingServices.RdlObjectModel.Report report, Microsoft.ReportingServices.RdlObjectModel.DataSet dataSet, NameValueCollection parameterCollection, IDictionary<string, bool> dataSetParamCardinalities)
		{
			int num = 0;
			foreach (string text in parameterCollection.AllKeys)
			{
				string text2 = parameterCollection[text];
				bool flag = dataSetParamCardinalities[text];
				if (!string.IsNullOrEmpty(text2) && (!Regex.IsMatch(text2, "^=new Object\\(\\) \\{(.*)\\}$") && flag))
				{
					string[] array = ConversionUtils.CsvToValuesList(text2, ',', true).ToArray<string>();
					if (array.Length > 1)
					{
						text2 = "=new Object() {" + string.Join(",", array) + "}";
					}
				}
				if (SharedDataSetJsonRendering.TryAddDataSetParameterToReport(report, dataSet, text, text2, flag, num))
				{
					num++;
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012346 File Offset: 0x00010546
		private IDictionary<string, bool> GetDataSetParameterCardinalities(byte[] dataSetDefinitionBytes)
		{
			return SharedDataSetJsonRendering.GetDataSetParameterCardinalities(XElement.Load(new StringReader(ConversionUtils.Utf8BytesToString(dataSetDefinitionBytes))));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001235D File Offset: 0x0001055D
		protected override ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData)
		{
			return new ScheduleFireEventHandlerBase.RetrievalCommand
			{
				SqlCommand = "FindItemsToUpdateByDataSet",
				Parameters = { { "@DataSetID", eventData } }
			};
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012380 File Offset: 0x00010580
		internal CatalogItem GetCatalogItem(RSService rsService, string path)
		{
			CatalogItem catalogItem2;
			using (new RSServiceStorageAccess(rsService))
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				catalogItemContext.SetPath(path);
				CatalogItem catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
				catalogItem.LoadProperties();
				catalogItem.Properties.Path = path;
				catalogItem2 = catalogItem;
			}
			return catalogItem2;
		}

		// Token: 0x040001F9 RID: 505
		private const int DownsamplingMaxRows = 10000;

		// Token: 0x040001FA RID: 506
		private const string AggregationKeySegment = "Agr";

		// Token: 0x040001FB RID: 507
		private const string ColumnKeySegment = "Col";

		// Token: 0x040001FC RID: 508
		private IEnumerable<KpiPropertyMapItem> propertyMap = new KpiPropertyMapItem[]
		{
			new KpiPropertyMapItem
			{
				DataSetId = "Value.Id",
				DataValue = "Value.Value",
				DataSetColumn = "Value.Column",
				DataSetAggregation = "Value.Aggregation",
				DataSetParameters = "Value.Parameters",
				DataItemType = "Value",
				DataLastUpdateStatus = "Value.LastUpdateStatus"
			},
			new KpiPropertyMapItem
			{
				DataSetId = "Goal.Id",
				DataValue = "Goal.Value",
				DataSetColumn = "Goal.Column",
				DataSetAggregation = "Goal.Aggregation",
				DataSetParameters = "Goal.Parameters",
				DataItemType = "Goal",
				DataLastUpdateStatus = "Goal.LastUpdateStatus"
			},
			new KpiPropertyMapItem
			{
				DataSetId = "Status.Id",
				DataValue = "Status.Value",
				DataSetColumn = "Status.Column",
				DataSetAggregation = "Status.Aggregation",
				DataSetParameters = "Status.Parameters",
				DataItemType = "Status",
				DataLastUpdateStatus = "Status.LastUpdateStatus"
			},
			new KpiPropertyMapItem
			{
				DataSetId = "TrendSet.Id",
				DataValue = "TrendSet.Value",
				DataSetColumn = "TrendSet.Column",
				DataSetAggregation = "TrendSet.Aggregation",
				DataSetParameters = "TrendSet.Parameters",
				DataItemType = "TrendSet",
				DataLastUpdateStatus = "TrendSet.LastUpdateStatus"
			}
		};
	}
}
