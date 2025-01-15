using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2010;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000215 RID: 533
	internal sealed class CreateCacheRefreshPlanActionParameters : RSSoapActionParameters
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000432D0 File Offset: 0x000414D0
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x000432D8 File Offset: 0x000414D8
		public ItemType ItemType { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000432E1 File Offset: 0x000414E1
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x000432E9 File Offset: 0x000414E9
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_itemPath = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00043302 File Offset: 0x00041502
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0004330A File Offset: 0x0004150A
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00043313 File Offset: 0x00041513
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x00043329 File Offset: 0x00041529
		public string EventType
		{
			get
			{
				if (this.m_eventType != null)
				{
					return this.m_eventType;
				}
				return "RefreshCache";
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_eventType = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00043342 File Offset: 0x00041542
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x0004334A File Offset: 0x0004154A
		public string MatchData
		{
			get
			{
				return this.m_matchData;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_matchData = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00043363 File Offset: 0x00041563
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x0004336B File Offset: 0x0004156B
		public ParameterValueOrFieldReference[] Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00043374 File Offset: 0x00041574
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004337C File Offset: 0x0004157C
		public string CacheRefreshPlanID
		{
			get
			{
				return this.m_cacheRefreshPlanID;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_cacheRefreshPlanID = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00043395 File Offset: 0x00041595
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.CacheRefreshPlanID);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000433AC File Offset: 0x000415AC
		internal override string InputTrace
		{
			get
			{
				string text = string.Empty;
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					text = string.Format(CultureInfo.InvariantCulture, "ItemPath={0}, Description={1}, EventType={2}, MatchData={3}, Parameters={4}", new object[]
					{
						(this.ItemPath != null) ? this.ItemPath : "null",
						(this.Description != null) ? this.Description : "null",
						(this.EventType != null) ? this.EventType : "null",
						(this.MatchData != null) ? this.MatchData : "null",
						(this.Parameters != null) ? ParameterValueOrFieldReference.ThisArrayToXml(this.Parameters) : "null"
					});
				}
				return text;
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00043464 File Offset: 0x00041664
		internal override void Validate()
		{
			if (this.ItemType != ItemType.PowerBIReport && CatalogItem.ItemTypeToSoapType(this.ItemType) == Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown)
			{
				throw new ItemNotFoundException(this.ItemPath);
			}
			if (string.IsNullOrEmpty(this.ItemPath))
			{
				throw new MissingParameterException("ItemPath");
			}
			if (string.IsNullOrEmpty(this.EventType))
			{
				throw new MissingParameterException("EventType");
			}
			if ("RefreshCache" != this.EventType && this.EventType.Equals("DataModelRefresh") && this.ItemType != ItemType.PowerBIReport)
			{
				throw new InvalidParameterException("EventType");
			}
			Subscription.CheckParameterArray(this.Parameters, "Parameters");
		}

		// Token: 0x040006C0 RID: 1728
		internal const string DataModelRefreshType = "DataModelRefresh";

		// Token: 0x040006C2 RID: 1730
		private string m_itemPath;

		// Token: 0x040006C3 RID: 1731
		private string m_description;

		// Token: 0x040006C4 RID: 1732
		private const string _RefreshCacheEventType = "RefreshCache";

		// Token: 0x040006C5 RID: 1733
		private string m_eventType;

		// Token: 0x040006C6 RID: 1734
		private string m_matchData;

		// Token: 0x040006C7 RID: 1735
		private ParameterValueOrFieldReference[] m_parameters;

		// Token: 0x040006C8 RID: 1736
		private string m_cacheRefreshPlanID = Guid.Empty.ToString();
	}
}
