using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B2 RID: 434
	internal class JsonObject
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x000AF5F1 File Offset: 0x000AD7F1
		public JsonObject()
		{
			this.mapValueByName = new Dictionary<string, object>();
			this.mapInfoByName = new Dictionary<string, JsonObject.JsonPropInfo>();
		}

		// Token: 0x1700062F RID: 1583
		public object this[string name, TomPropCategory category, int relIndex = 0, bool readOnly = false]
		{
			get
			{
				return this.mapValueByName[name];
			}
			set
			{
				this.mapValueByName[name] = value;
				this.mapInfoByName[name] = new JsonObject.JsonPropInfo
				{
					Category = category,
					RelIndex = relIndex,
					IsReadOnly = readOnly,
					Name = name
				};
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000AF65C File Offset: 0x000AD85C
		public IDictionary<string, object> ToDictObject()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<JsonObject.JsonPropInfo> list = this.mapInfoByName.Values.ToList<JsonObject.JsonPropInfo>();
			list.Sort(new Comparison<JsonObject.JsonPropInfo>(this.JsonPropInfoCompare));
			foreach (JsonObject.JsonPropInfo jsonPropInfo in list)
			{
				dictionary[jsonPropInfo.Name] = this.mapValueByName[jsonPropInfo.Name];
			}
			return dictionary;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000AF6E8 File Offset: 0x000AD8E8
		private int JsonPropInfoCompare(JsonObject.JsonPropInfo info1, JsonObject.JsonPropInfo info2)
		{
			if (this.GetTomPropCategoryOrder(info1.Category) != this.GetTomPropCategoryOrder(info2.Category))
			{
				return this.GetTomPropCategoryOrder(info1.Category) - this.GetTomPropCategoryOrder(info2.Category);
			}
			if (info1.IsReadOnly != info2.IsReadOnly)
			{
				if (!info1.IsReadOnly)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (info1.RelIndex != info2.RelIndex)
				{
					return info1.RelIndex - info2.RelIndex;
				}
				return 0;
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000AF760 File Offset: 0x000AD960
		private int GetTomPropCategoryOrder(TomPropCategory category)
		{
			switch (category)
			{
			case TomPropCategory.Type:
				return 0;
			case TomPropCategory.Name:
				return 1;
			case TomPropCategory.Regular:
				return 2;
			case TomPropCategory.CrossLink:
				return 3;
			case TomPropCategory.ChildLink:
				return 4;
			case TomPropCategory.ChildCollection:
				return 5;
			}
			throw TomInternalException.Create("Unhandled TomPropCategory: {0}", new object[] { category });
		}

		// Token: 0x04000521 RID: 1313
		private Dictionary<string, object> mapValueByName;

		// Token: 0x04000522 RID: 1314
		private Dictionary<string, JsonObject.JsonPropInfo> mapInfoByName;

		// Token: 0x020003F9 RID: 1017
		private class JsonPropInfo
		{
			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06002749 RID: 10057 RVA: 0x000ED517 File Offset: 0x000EB717
			// (set) Token: 0x0600274A RID: 10058 RVA: 0x000ED51F File Offset: 0x000EB71F
			public TomPropCategory Category { get; set; }

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x0600274B RID: 10059 RVA: 0x000ED528 File Offset: 0x000EB728
			// (set) Token: 0x0600274C RID: 10060 RVA: 0x000ED530 File Offset: 0x000EB730
			public int RelIndex { get; set; }

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x0600274D RID: 10061 RVA: 0x000ED539 File Offset: 0x000EB739
			// (set) Token: 0x0600274E RID: 10062 RVA: 0x000ED541 File Offset: 0x000EB741
			public bool IsReadOnly { get; set; }

			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x0600274F RID: 10063 RVA: 0x000ED54A File Offset: 0x000EB74A
			// (set) Token: 0x06002750 RID: 10064 RVA: 0x000ED552 File Offset: 0x000EB752
			public string Name { get; set; }
		}
	}
}
