using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C6 RID: 454
	public class HttpRequestMessageProperties
	{
		// Token: 0x06000F01 RID: 3841 RVA: 0x0003DF40 File Offset: 0x0003C140
		internal HttpRequestMessageProperties(HttpRequestMessage request)
		{
			this._request = request;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0003DF50 File Offset: 0x0003C150
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0003DF7E File Offset: 0x0003C17E
		internal Func<long> TotalCountFunc
		{
			get
			{
				object obj;
				if (this._request.Properties.TryGetValue("Microsoft.AspNet.OData.TotalCountFunc", out obj))
				{
					return (Func<long>)obj;
				}
				return null;
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.TotalCountFunc"] = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0003DF98 File Offset: 0x0003C198
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0003DFC6 File Offset: 0x0003C1C6
		internal int PageSize
		{
			get
			{
				object obj;
				if (this._request.Properties.TryGetValue("Microsoft.AspNet.OData.PageSize", out obj))
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.PageSize"] = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0003DFE3 File Offset: 0x0003C1E3
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
		internal ODataQueryOptions QueryOptions
		{
			get
			{
				return this.GetValueOrNull<ODataQueryOptions>("Microsoft.AspNet.OData.QueryOptions");
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._request.Properties["Microsoft.AspNet.OData.QueryOptions"] = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0003E016 File Offset: 0x0003C216
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0003E023 File Offset: 0x0003C223
		public string RouteName
		{
			get
			{
				return this.GetValueOrNull<string>("Microsoft.AspNet.OData.RouteName");
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.RouteName"] = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0003E03B File Offset: 0x0003C23B
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0003E048 File Offset: 0x0003C248
		public Microsoft.AspNet.OData.Routing.ODataPath Path
		{
			get
			{
				return this.GetValueOrNull<Microsoft.AspNet.OData.Routing.ODataPath>("Microsoft.AspNet.OData.Path");
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.Path"] = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0003E060 File Offset: 0x0003C260
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public long? TotalCount
		{
			get
			{
				object obj;
				if (this._request.Properties.TryGetValue("Microsoft.AspNet.OData.TotalCount", out obj))
				{
					return (long?)obj;
				}
				if (this.TotalCountFunc != null)
				{
					long num = this.TotalCountFunc();
					this._request.Properties["Microsoft.AspNet.OData.TotalCount"] = num;
					return new long?(num);
				}
				return null;
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.TotalCount"] = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003E0E9 File Offset: 0x0003C2E9
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0003E0F6 File Offset: 0x0003C2F6
		public Uri NextLink
		{
			get
			{
				return this.GetValueOrNull<Uri>("Microsoft.AspNet.OData.NextLink");
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.NextLink"] = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0003E10E File Offset: 0x0003C30E
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0003E11B File Offset: 0x0003C31B
		public Uri DeltaLink
		{
			get
			{
				return this.GetValueOrNull<Uri>("Microsoft.AspNet.OData.DeltaLink");
			}
			set
			{
				this._request.Properties["Microsoft.AspNet.OData.DeltaLink"] = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003E133 File Offset: 0x0003C333
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0003E140 File Offset: 0x0003C340
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.GetValueOrNull<SelectExpandClause>("Microsoft.AspNet.OData.SelectExpandClause");
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._request.Properties["Microsoft.AspNet.OData.SelectExpandClause"] = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003E166 File Offset: 0x0003C366
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0003E173 File Offset: 0x0003C373
		public ApplyClause ApplyClause
		{
			get
			{
				return this.GetValueOrNull<ApplyClause>("Microsoft.AspNet.OData.ApplyClause");
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._request.Properties["Microsoft.AspNet.OData.ApplyClause"] = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003E19C File Offset: 0x0003C39C
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0003E1C6 File Offset: 0x0003C3C6
		public IDictionary<string, object> RoutingConventionsStore
		{
			get
			{
				IDictionary<string, object> dictionary = this.GetValueOrNull<IDictionary<string, object>>("Microsoft.AspNet.OData.RoutingConventionsStore");
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					this.RoutingConventionsStore = dictionary;
				}
				return dictionary;
			}
			private set
			{
				this._request.Properties["Microsoft.AspNet.OData.RoutingConventionsStore"] = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0003E1DE File Offset: 0x0003C3DE
		internal ODataVersion? ODataServiceVersion
		{
			get
			{
				return HttpRequestMessageProperties.GetODataVersionFromHeader(this._request.Headers, "OData-Version");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0003E1F5 File Offset: 0x0003C3F5
		internal ODataVersion? ODataMaxServiceVersion
		{
			get
			{
				return HttpRequestMessageProperties.GetODataVersionFromHeader(this._request.Headers, "OData-MaxVersion");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0003E20C File Offset: 0x0003C40C
		internal ODataVersion? ODataMinServiceVersion
		{
			get
			{
				return HttpRequestMessageProperties.GetODataVersionFromHeader(this._request.Headers, "OData-MinVersion");
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003E224 File Offset: 0x0003C424
		private static ODataVersion? GetODataVersionFromHeader(HttpHeaders headers, string headerName)
		{
			IEnumerable<string> enumerable;
			if (headers.TryGetValues(headerName, out enumerable))
			{
				string text = enumerable.FirstOrDefault<string>();
				if (text != null)
				{
					string text2 = text.Trim(new char[] { ' ', ';' });
					try
					{
						return new ODataVersion?(ODataUtils.StringToODataVersion(text2));
					}
					catch (ODataException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003E28C File Offset: 0x0003C48C
		private T GetValueOrNull<T>(string propertyName) where T : class
		{
			object obj;
			if (this._request.Properties.TryGetValue(propertyName, out obj))
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x04000425 RID: 1061
		private const string DeltaLinkKey = "Microsoft.AspNet.OData.DeltaLink";

		// Token: 0x04000426 RID: 1062
		private const string NextLinkKey = "Microsoft.AspNet.OData.NextLink";

		// Token: 0x04000427 RID: 1063
		private const string PathKey = "Microsoft.AspNet.OData.Path";

		// Token: 0x04000428 RID: 1064
		private const string RouteNameKey = "Microsoft.AspNet.OData.RouteName";

		// Token: 0x04000429 RID: 1065
		private const string RoutingConventionsStoreKey = "Microsoft.AspNet.OData.RoutingConventionsStore";

		// Token: 0x0400042A RID: 1066
		private const string SelectExpandClauseKey = "Microsoft.AspNet.OData.SelectExpandClause";

		// Token: 0x0400042B RID: 1067
		private const string ApplyClauseKey = "Microsoft.AspNet.OData.ApplyClause";

		// Token: 0x0400042C RID: 1068
		private const string TotalCountKey = "Microsoft.AspNet.OData.TotalCount";

		// Token: 0x0400042D RID: 1069
		private const string TotalCountFuncKey = "Microsoft.AspNet.OData.TotalCountFunc";

		// Token: 0x0400042E RID: 1070
		private const string PageSizeKey = "Microsoft.AspNet.OData.PageSize";

		// Token: 0x0400042F RID: 1071
		private const string QueryOptionsKey = "Microsoft.AspNet.OData.QueryOptions";

		// Token: 0x04000430 RID: 1072
		private HttpRequestMessage _request;
	}
}
