using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Services;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Repositories
{
	// Token: 0x02000014 RID: 20
	internal sealed class CatalogItemFactory
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002C84 File Offset: 0x00000E84
		public CatalogItemFactory(IPrincipal userPrincipal, ICatalogRepository catalogRepository, ILogger logger, ICatalogDataAccessor catalogAccessor, ISystemService systemService)
		{
			this._logger = logger;
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
			this._catalogAccessor = catalogAccessor;
			this._systemService = systemService;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public global::Model.CatalogItem Create(CatalogItemDescriptor itemDescriptor)
		{
			return this.Create(itemDescriptor, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CBC File Offset: 0x00000EBC
		public global::Model.CatalogItem Create(FavoriteableCatalogItemDescriptor itemDescriptor)
		{
			global::Model.CatalogItem catalogItem = this.Create(itemDescriptor, null);
			if (catalogItem != null)
			{
				catalogItem.IsFavorite = itemDescriptor.IsFavorite;
			}
			return catalogItem;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CE4 File Offset: 0x00000EE4
		internal global::Model.CatalogItem Create(CatalogItemDescriptor itemDescriptor, Action<global::Model.CatalogItem> rsLoader)
		{
			if (itemDescriptor == null)
			{
				throw new ArgumentNullException("itemDescriptor");
			}
			if (this._userPrincipal == null)
			{
				throw new InvalidOperationException(SR.Error_UserPrincipalIsNotSet);
			}
			switch (itemDescriptor.Type.ToCatalogItemType())
			{
			case CatalogItemType.Folder:
				return this.CreateFolderCatalogItem(itemDescriptor);
			case CatalogItemType.Report:
				return this.CreateReportCatalogItem(itemDescriptor);
			case CatalogItemType.DataSource:
				return this.CreateDataSourceCatalogItem(itemDescriptor);
			case CatalogItemType.DataSet:
				return this.CreateDataSetCatalogItem(itemDescriptor);
			case CatalogItemType.Component:
				return this.CreateComponentCatalogItem(itemDescriptor);
			case CatalogItemType.Resource:
				return this.CreateResourceCatalogItem(itemDescriptor);
			case CatalogItemType.Kpi:
				return this.CreateKpiCatalogItem(this._userPrincipal, itemDescriptor);
			case CatalogItemType.LinkedReport:
				return this.CreateLinkedReportCatalogItem(itemDescriptor);
			case CatalogItemType.ReportModel:
				return this.CreateModelCatalogItem(itemDescriptor);
			case CatalogItemType.PowerBIReport:
				return this.CreatePowerBIReportCatalogItem(itemDescriptor);
			case CatalogItemType.ExcelWorkbook:
				return this.CreateExcelCatalogItem(itemDescriptor);
			}
			return this.CreateUnknownCatalogItem(itemDescriptor);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DC0 File Offset: 0x00000FC0
		internal KpiDataItem CreateKpiDataItem(string name, IDictionary<string, string> properties)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}.Type", name);
			KpiDataItemType kpiDataItemType;
			if (!properties.ContainsKey(text) || !Enum.TryParse<KpiDataItemType>(properties[text], out kpiDataItemType))
			{
				return null;
			}
			if (kpiDataItemType == KpiDataItemType.Static)
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "{0}.Value", name);
				return new KpiStaticDataItem
				{
					Type = KpiDataItemType.Static,
					Value = (properties.ContainsKey(text2) ? properties[text2] : null)
				};
			}
			string text3 = string.Format(CultureInfo.InvariantCulture, "{0}.Id", name);
			string text4 = string.Format(CultureInfo.InvariantCulture, "{0}.Path", name);
			string text5 = string.Format(CultureInfo.InvariantCulture, "{0}.Column", name);
			string text6 = string.Format(CultureInfo.InvariantCulture, "{0}.Aggregation", name);
			string text7 = string.Format(CultureInfo.InvariantCulture, "{0}.Parameters", name);
			Guid empty = Guid.Empty;
			if (properties.ContainsKey(text3))
			{
				Guid.TryParse(properties[text3], out empty);
			}
			global::Model.KpiSharedDataItemAggregation kpiSharedDataItemAggregation = global::Model.KpiSharedDataItemAggregation.None;
			if (properties.ContainsKey(text6))
			{
				Enum.TryParse<global::Model.KpiSharedDataItemAggregation>(properties[text6], out kpiSharedDataItemAggregation);
			}
			if (name == "TrendSet")
			{
				kpiSharedDataItemAggregation = global::Model.KpiSharedDataItemAggregation.None;
			}
			else if (kpiSharedDataItemAggregation == global::Model.KpiSharedDataItemAggregation.None)
			{
				kpiSharedDataItemAggregation = global::Model.KpiSharedDataItemAggregation.First;
			}
			KpiSharedDataItem kpiSharedDataItem = new KpiSharedDataItem();
			kpiSharedDataItem.Type = KpiDataItemType.Shared;
			kpiSharedDataItem.Column = (properties.ContainsKey(text5) ? properties[text5] : null);
			kpiSharedDataItem.Id = empty;
			kpiSharedDataItem.Path = (properties.ContainsKey(text4) ? properties[text4] : null);
			kpiSharedDataItem.Aggregation = kpiSharedDataItemAggregation;
			IEnumerable<DataSetParameter> enumerable2;
			if (!properties.ContainsKey(text7))
			{
				IEnumerable<DataSetParameter> enumerable = new List<DataSetParameter>();
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = this.CreateDataSetParameters(properties[text7]);
			}
			kpiSharedDataItem.Parameters = enumerable2;
			return kpiSharedDataItem;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F64 File Offset: 0x00001164
		internal IEnumerable<CatalogItemParameter> CreateCatalogItemParameters(string parameterString)
		{
			List<CatalogItemParameter> list = new List<CatalogItemParameter>();
			if (string.IsNullOrWhiteSpace(parameterString))
			{
				return list;
			}
			string[] array = parameterString.Split(new char[] { '&' });
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length == 2)
				{
					CatalogItemParameter catalogItemParameter = new CatalogItemParameter
					{
						Name = Uri.UnescapeDataString(array2[0]),
						Value = Uri.UnescapeDataString(array2[1])
					};
					list.Add(catalogItemParameter);
				}
			}
			return list;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FE8 File Offset: 0x000011E8
		internal IEnumerable<DataSetParameter> CreateDataSetParameters(string parameterString)
		{
			List<DataSetParameter> list = new List<DataSetParameter>();
			if (string.IsNullOrWhiteSpace(parameterString))
			{
				return list;
			}
			string[] array = parameterString.Split(new char[] { '&' });
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '=' });
				if (array2.Length == 2)
				{
					DataSetParameter dataSetParameter = new DataSetParameter
					{
						Name = Uri.UnescapeDataString(array2[0]),
						Value = Uri.UnescapeDataString(array2[1])
					};
					list.Add(dataSetParameter);
				}
			}
			return list;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000306C File Offset: 0x0000126C
		private bool SafePopulateKpiProperty(Action populateAction, string propertyName)
		{
			try
			{
				populateAction();
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				Exception e = ex2;
				this._logger.Trace(TraceLevel.Error, () => string.Format("Error populating Kpi property '{0}': {1}", propertyName, e.Message));
				return false;
			}
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000030D4 File Offset: 0x000012D4
		internal void PopulateKpiFromProperties(Kpi modelKpi, IQueryable<Property> properties)
		{
			Dictionary<string, string> propertyDictionary = properties.ToDictionary((Property p) => p.Name, (Property v) => v.Value);
			if (propertyDictionary.ContainsKey("Visualization"))
			{
				modelKpi.Visualization = (KpiVisualization)Convert.ToInt16(propertyDictionary["Visualization"]);
			}
			if (propertyDictionary.ContainsKey("ValueFormat"))
			{
				modelKpi.ValueFormat = (KpiValueFormat)Convert.ToInt16(propertyDictionary["ValueFormat"]);
			}
			if (propertyDictionary.ContainsKey("Currency"))
			{
				modelKpi.Currency = propertyDictionary["Currency"];
			}
			modelKpi.Data = new KpiData();
			modelKpi.Data.Goal = this.CreateKpiDataItem("Goal", propertyDictionary);
			modelKpi.Data.Value = this.CreateKpiDataItem("Value", propertyDictionary);
			modelKpi.Data.Status = this.CreateKpiDataItem("Status", propertyDictionary);
			modelKpi.Data.TrendSet = this.CreateKpiDataItem("TrendSet", propertyDictionary);
			modelKpi.Values = new KpiValues();
			if (propertyDictionary.ContainsKey("Value.Value"))
			{
				this.SafePopulateKpiProperty(delegate
				{
					modelKpi.Values.Value = propertyDictionary["Value.Value"];
				}, "Value");
			}
			if (propertyDictionary.ContainsKey("Goal.Value"))
			{
				this.SafePopulateKpiProperty(delegate
				{
					modelKpi.Values.Goal = JsonConvert.DeserializeObject<double?>(propertyDictionary["Goal.Value"].ToLowerInvariant());
				}, "Goal");
			}
			if (propertyDictionary.ContainsKey("Status.Value"))
			{
				this.SafePopulateKpiProperty(delegate
				{
					modelKpi.Values.Status = JsonConvert.DeserializeObject<double?>(propertyDictionary["Status.Value"].ToLowerInvariant());
				}, "Status");
			}
			if (propertyDictionary.ContainsKey("TrendSet.Value"))
			{
				this.SafePopulateKpiProperty(delegate
				{
					modelKpi.Values.TrendSet = JsonConvert.DeserializeObject<double?[]>(propertyDictionary["TrendSet.Value"].ToLowerInvariant());
				}, "TrendSet");
			}
			DrillthroughTargetType drillthroughTargetType;
			if (propertyDictionary.ContainsKey("DrillthroughTarget.Type") && Enum.TryParse<DrillthroughTargetType>(propertyDictionary["DrillthroughTarget.Type"], out drillthroughTargetType) && drillthroughTargetType == DrillthroughTargetType.Url)
			{
				UrlDrillthroughTarget urlDrillthroughTarget = new UrlDrillthroughTarget
				{
					Type = DrillthroughTargetType.Url
				};
				if (propertyDictionary.ContainsKey("DrillthroughTarget.Url"))
				{
					urlDrillthroughTarget.Url = propertyDictionary["DrillthroughTarget.Url"];
				}
				if (propertyDictionary.ContainsKey("DrillthroughTarget.DirectNavigation"))
				{
					urlDrillthroughTarget.DirectNavigation = Convert.ToBoolean(propertyDictionary["DrillthroughTarget.DirectNavigation"]);
				}
				modelKpi.DrillthroughTarget = urlDrillthroughTarget;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000033B4 File Offset: 0x000015B4
		private Kpi CreateKpiCatalogItem(IPrincipal userPrincipal, CatalogItemDescriptor itemDescriptor)
		{
			KpiRepository kpiRepository = new KpiRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, kpiRepository);
			try
			{
				IQueryable<Property> itemProperties = this._catalogRepository.GetItemProperties(userPrincipal, kpiRepository.Path, null);
				this.PopulateKpiFromProperties(kpiRepository, itemProperties);
			}
			catch (Exception ex)
			{
				Exception ex3;
				Exception ex2 = ex3;
				Exception ex = ex2;
				kpiRepository.Values = null;
				kpiRepository.Data = null;
				this._logger.Trace(TraceLevel.Warning, () => string.Format("Kpi loading failed: {0}", ex.Message));
			}
			return kpiRepository;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003440 File Offset: 0x00001640
		private global::Model.CatalogItem CreateFolderCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Folder folder = new FolderRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, folder);
			return folder;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003468 File Offset: 0x00001668
		private Report CreateReportCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Report report = new ReportRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, report);
			report.HasDataSources = itemDescriptor.ItemMetadata.HasDataSources;
			report.HasSharedDataSets = itemDescriptor.ItemMetadata.HasSharedDataSets;
			report.HasParameters = itemDescriptor.ItemMetadata.HasParameters;
			return report;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000034C4 File Offset: 0x000016C4
		private LinkedReport CreateLinkedReportCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			LinkedReport linkedReport = new LinkedReportRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, linkedReport);
			linkedReport.HasParameters = itemDescriptor.ItemMetadata.HasParameters;
			return linkedReport;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000034FC File Offset: 0x000016FC
		private Resource CreateResourceCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Resource resource = new ResourceRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, resource);
			return resource;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003524 File Offset: 0x00001724
		private PowerBIReport CreatePowerBIReportCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			PowerBIReport powerBIReport = new PowerBIReportRepository(this._userPrincipal, this._catalogRepository, this._systemService);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, powerBIReport);
			powerBIReport.HasDataSources = itemDescriptor.ItemMetadata.HasDataSources;
			return powerBIReport;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003564 File Offset: 0x00001764
		private ExcelWorkbook CreateExcelCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			ExcelWorkbook excelWorkbook = new ExcelWorkbookRepository(this._userPrincipal, this._catalogRepository, this._systemService);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, excelWorkbook);
			return excelWorkbook;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003594 File Offset: 0x00001794
		private Component CreateComponentCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			Component component = new ComponentRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, component);
			return component;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000035BC File Offset: 0x000017BC
		private DataSource CreateDataSourceCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			DataSourceRepository dataSourceRepository = new DataSourceRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, dataSourceRepository);
			return dataSourceRepository;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035E4 File Offset: 0x000017E4
		private global::Model.DataSet CreateDataSetCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			global::Model.DataSet dataSet = new DataSetRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, dataSet);
			return dataSet;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000360C File Offset: 0x0000180C
		private ReportModel CreateModelCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			ReportModelRepository reportModelRepository = new ReportModelRepository(this._userPrincipal, this._catalogRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, reportModelRepository);
			reportModelRepository.HasDataSources = itemDescriptor.ItemMetadata.HasDataSources;
			return reportModelRepository;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003644 File Offset: 0x00001844
		private global::Model.CatalogItem CreateUnknownCatalogItem(CatalogItemDescriptor itemDescriptor)
		{
			this._logger.Trace(TraceLevel.Warning, "CatalogItem type could not be determined");
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003658 File Offset: 0x00001858
		internal static void PopulateCommonFields(CatalogItemDescriptor itemDescriptor, global::Model.CatalogItem catalogItem)
		{
			if (itemDescriptor.ID != null)
			{
				catalogItem.Id = new Guid(itemDescriptor.ID);
			}
			catalogItem.Name = itemDescriptor.Name;
			catalogItem.Description = itemDescriptor.Description;
			catalogItem.ContentType = itemDescriptor.MimeType;
			catalogItem.CreatedDate = CatalogItemFactory.ToDateTimeOffset(itemDescriptor.CreationDate);
			catalogItem.CreatedBy = itemDescriptor.CreatedBy;
			catalogItem.ModifiedDate = CatalogItemFactory.ToDateTimeOffset(itemDescriptor.ModifiedDate);
			catalogItem.ModifiedBy = itemDescriptor.ModifiedBy;
			catalogItem.Hidden = itemDescriptor.Hidden;
			catalogItem.Path = ((itemDescriptor.Path != null) ? itemDescriptor.Path.Value : null);
			catalogItem.Size = (long)itemDescriptor.Size;
			catalogItem.ParentFolderId = ((itemDescriptor.ItemMetadata.ParentID != Guid.Empty) ? new Guid?(itemDescriptor.ItemMetadata.ParentID) : null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003749 File Offset: 0x00001949
		internal static DateTimeOffset ToDateTimeOffset(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return DateTimeOffset.MinValue;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return DateTimeOffset.MaxValue;
			}
			return new DateTimeOffset(dateTime);
		}

		// Token: 0x04000056 RID: 86
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000057 RID: 87
		private readonly ILogger _logger;

		// Token: 0x04000058 RID: 88
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000059 RID: 89
		private readonly ICatalogDataAccessor _catalogAccessor;

		// Token: 0x0400005A RID: 90
		private readonly ISystemService _systemService;
	}
}
