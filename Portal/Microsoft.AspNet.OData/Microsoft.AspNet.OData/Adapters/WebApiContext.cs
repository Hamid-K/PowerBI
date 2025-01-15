using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DB RID: 475
	internal class WebApiContext : IWebApiContext
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0003F6BC File Offset: 0x0003D8BC
		public WebApiContext(HttpRequestMessageProperties context)
		{
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			this.innerContext = context;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003F6D9 File Offset: 0x0003D8D9
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0003F6E6 File Offset: 0x0003D8E6
		public ApplyClause ApplyClause
		{
			get
			{
				return this.innerContext.ApplyClause;
			}
			set
			{
				this.innerContext.ApplyClause = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003F6F4 File Offset: 0x0003D8F4
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x0003F701 File Offset: 0x0003D901
		public Uri NextLink
		{
			get
			{
				return this.innerContext.NextLink;
			}
			set
			{
				this.innerContext.NextLink = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003F70F File Offset: 0x0003D90F
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x0003F71C File Offset: 0x0003D91C
		public Uri DeltaLink
		{
			get
			{
				return this.innerContext.DeltaLink;
			}
			set
			{
				this.innerContext.DeltaLink = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0003F72A File Offset: 0x0003D92A
		public Microsoft.AspNet.OData.Routing.ODataPath Path
		{
			get
			{
				return this.innerContext.Path;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0003F737 File Offset: 0x0003D937
		public string RouteName
		{
			get
			{
				return this.innerContext.RouteName;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003F744 File Offset: 0x0003D944
		public IDictionary<string, object> RoutingConventionsStore
		{
			get
			{
				return this.innerContext.RoutingConventionsStore;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0003F751 File Offset: 0x0003D951
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0003F75E File Offset: 0x0003D95E
		public SelectExpandClause ProcessedSelectExpandClause
		{
			get
			{
				return this.innerContext.SelectExpandClause;
			}
			set
			{
				this.innerContext.SelectExpandClause = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0003F76C File Offset: 0x0003D96C
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x0003F779 File Offset: 0x0003D979
		public ODataQueryOptions QueryOptions
		{
			get
			{
				return this.innerContext.QueryOptions;
			}
			set
			{
				this.innerContext.QueryOptions = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0003F787 File Offset: 0x0003D987
		public long? TotalCount
		{
			get
			{
				return this.innerContext.TotalCount;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003F794 File Offset: 0x0003D994
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0003F7A1 File Offset: 0x0003D9A1
		public int PageSize
		{
			get
			{
				return this.innerContext.PageSize;
			}
			set
			{
				this.innerContext.PageSize = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0003F7AF File Offset: 0x0003D9AF
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0003F7BC File Offset: 0x0003D9BC
		public Func<long> TotalCountFunc
		{
			get
			{
				return this.innerContext.TotalCountFunc;
			}
			set
			{
				this.innerContext.TotalCountFunc = value;
			}
		}

		// Token: 0x0400044C RID: 1100
		private HttpRequestMessageProperties innerContext;
	}
}
