using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x02000198 RID: 408
	public class ODataSerializerContext
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000362F7 File Offset: 0x000344F7
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x000362FF File Offset: 0x000344FF
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
				this.InternalRequest = ((this._request != null) ? new WebApiRequestMessage(this._request) : null);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00036324 File Offset: 0x00034524
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0003632C File Offset: 0x0003452C
		public HttpRequestContext RequestContext { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00036335 File Offset: 0x00034535
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0003633D File Offset: 0x0003453D
		public UrlHelper Url
		{
			get
			{
				return this._urlHelper;
			}
			set
			{
				this._urlHelper = value;
				this.InternalUrlHelper = ((this._urlHelper != null) ? new WebApiUrlHelper(this._urlHelper) : null);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00036362 File Offset: 0x00034562
		private void CopyPlatformSpecificProperties(ODataSerializerContext context)
		{
			this.Request = context.Request;
			this.Url = context.Url;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00002557 File Offset: 0x00000757
		public ODataSerializerContext()
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003637C File Offset: 0x0003457C
		public ODataSerializerContext(ResourceContext resource, SelectExpandClause selectExpandClause, IEdmProperty edmProperty)
			: this(resource, edmProperty, null, null)
		{
			this.SelectExpandClause = selectExpandClause;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00036390 File Offset: 0x00034590
		internal ODataSerializerContext(ResourceContext resource, IEdmProperty edmProperty, ODataQueryContext queryContext, SelectItem currentSelectItem)
		{
			if (resource == null)
			{
				throw Error.ArgumentNull("resource");
			}
			ODataSerializerContext serializerContext = resource.SerializerContext;
			this.CopyPlatformSpecificProperties(serializerContext);
			this.Model = serializerContext.Model;
			this.Path = serializerContext.Path;
			this.RootElementName = serializerContext.RootElementName;
			this.SkipExpensiveAvailabilityChecks = serializerContext.SkipExpensiveAvailabilityChecks;
			this.MetadataLevel = serializerContext.MetadataLevel;
			this.Items = serializerContext.Items;
			this.ExpandReference = serializerContext.ExpandReference;
			this.QueryContext = queryContext;
			this.ExpandedResource = resource;
			this.CurrentSelectItem = currentSelectItem;
			ExpandedNavigationSelectItem expandedNavigationSelectItem = currentSelectItem as ExpandedNavigationSelectItem;
			if (expandedNavigationSelectItem != null)
			{
				this.SelectExpandClause = expandedNavigationSelectItem.SelectAndExpand;
				this.NavigationSource = expandedNavigationSelectItem.NavigationSource;
			}
			else
			{
				PathSelectItem pathSelectItem = currentSelectItem as PathSelectItem;
				if (pathSelectItem != null)
				{
					this.SelectExpandClause = pathSelectItem.SelectAndExpand;
					this.NavigationSource = resource.NavigationSource;
				}
				ExpandedReferenceSelectItem expandedReferenceSelectItem = currentSelectItem as ExpandedReferenceSelectItem;
				if (expandedReferenceSelectItem != null)
				{
					this.ExpandReference = true;
					this.NavigationSource = expandedReferenceSelectItem.NavigationSource;
				}
			}
			this.EdmProperty = edmProperty;
			if (currentSelectItem == null || this.NavigationSource is IEdmUnknownEntitySet)
			{
				if (edmProperty is IEdmNavigationProperty && serializerContext.NavigationSource != null)
				{
					this.NavigationSource = serializerContext.NavigationSource.FindNavigationTarget(this.NavigationProperty);
					return;
				}
				this.NavigationSource = resource.NavigationSource;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x000364DB File Offset: 0x000346DB
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x000364E3 File Offset: 0x000346E3
		internal IWebApiRequestMessage InternalRequest { get; private set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000364EC File Offset: 0x000346EC
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x000364F4 File Offset: 0x000346F4
		internal IWebApiUrlHelper InternalUrlHelper { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x000364FD File Offset: 0x000346FD
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x00036519 File Offset: 0x00034719
		internal ODataQueryContext QueryContext
		{
			get
			{
				if (this.QueryOptions != null)
				{
					return this.QueryOptions.Context;
				}
				return this._queryContext;
			}
			private set
			{
				this._queryContext = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00036522 File Offset: 0x00034722
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0003652A File Offset: 0x0003472A
		public IEdmNavigationSource NavigationSource { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00036533 File Offset: 0x00034733
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0003653B File Offset: 0x0003473B
		public IEdmModel Model { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00036544 File Offset: 0x00034744
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x0003654C File Offset: 0x0003474C
		public Microsoft.AspNet.OData.Routing.ODataPath Path { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00036555 File Offset: 0x00034755
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x0003655D File Offset: 0x0003475D
		public string RootElementName { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00036566 File Offset: 0x00034766
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x0003656E File Offset: 0x0003476E
		public bool SkipExpensiveAvailabilityChecks { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00036577 File Offset: 0x00034777
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x0003657F File Offset: 0x0003477F
		public ODataMetadataLevel MetadataLevel { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00036588 File Offset: 0x00034788
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x000365E3 File Offset: 0x000347E3
		public SelectExpandClause SelectExpandClause
		{
			get
			{
				if (this._isSelectExpandClauseSet)
				{
					return this._selectExpandClause;
				}
				if (this.QueryOptions != null)
				{
					if (this.QueryOptions.SelectExpand != null)
					{
						return this.QueryOptions.SelectExpand.ProcessedSelectExpandClause;
					}
					return null;
				}
				else
				{
					ExpandedNavigationSelectItem expandedNavigationSelectItem = this.CurrentSelectItem as ExpandedNavigationSelectItem;
					if (expandedNavigationSelectItem != null)
					{
						return expandedNavigationSelectItem.SelectAndExpand;
					}
					return null;
				}
			}
			set
			{
				this._isSelectExpandClauseSet = true;
				this._selectExpandClause = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000365F3 File Offset: 0x000347F3
		internal ExpandedReferenceSelectItem CurrentExpandedSelectItem
		{
			get
			{
				return this.CurrentSelectItem as ExpandedReferenceSelectItem;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00036600 File Offset: 0x00034800
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00036608 File Offset: 0x00034808
		internal SelectItem CurrentSelectItem { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00036611 File Offset: 0x00034811
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00036619 File Offset: 0x00034819
		public ODataQueryOptions QueryOptions { get; internal set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00036622 File Offset: 0x00034822
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0003662A File Offset: 0x0003482A
		internal Queue<IEdmProperty> PropertiesInPath { get; private set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00036633 File Offset: 0x00034833
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0003663B File Offset: 0x0003483B
		public ResourceContext ExpandedResource { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00036644 File Offset: 0x00034844
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x0003664C File Offset: 0x0003484C
		public bool ExpandReference { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00036655 File Offset: 0x00034855
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0003665D File Offset: 0x0003485D
		public IEdmProperty EdmProperty { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00036666 File Offset: 0x00034866
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.EdmProperty as IEdmNavigationProperty;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00036673 File Offset: 0x00034873
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00036690 File Offset: 0x00034890
		public IDictionary<object, object> Items
		{
			get
			{
				this._items = this._items ?? new Dictionary<object, object>();
				return this._items;
			}
			private set
			{
				this._items = value;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003669C File Offset: 0x0003489C
		internal IEdmTypeReference GetEdmType(object instance, Type type)
		{
			IEdmObject edmObject = instance as IEdmObject;
			IEdmTypeReference edmTypeReference;
			if (edmObject != null)
			{
				edmTypeReference = edmObject.GetEdmType();
				if (edmTypeReference == null)
				{
					throw Error.InvalidOperation(SRResources.EdmTypeCannotBeNull, new object[]
					{
						edmObject.GetType().FullName,
						typeof(IEdmObject).Name
					});
				}
			}
			else
			{
				if (this.Model == null)
				{
					throw Error.InvalidOperation(SRResources.RequestMustHaveModel, new object[0]);
				}
				this._typeMappingCache = this._typeMappingCache ?? this.Model.GetTypeMappingCache();
				edmTypeReference = this._typeMappingCache.GetEdmType(type, this.Model);
				if (edmTypeReference == null)
				{
					if (instance != null)
					{
						edmTypeReference = this._typeMappingCache.GetEdmType(instance.GetType(), this.Model);
					}
					if (edmTypeReference == null)
					{
						throw Error.InvalidOperation(SRResources.ClrTypeNotInModel, new object[] { type });
					}
				}
				else if (instance != null)
				{
					IEdmTypeReference edmType = this._typeMappingCache.GetEdmType(instance.GetType(), this.Model);
					if (edmType != null && edmType != edmTypeReference)
					{
						edmTypeReference = edmType;
					}
				}
			}
			return edmTypeReference;
		}

		// Token: 0x040003D4 RID: 980
		private HttpRequestMessage _request;

		// Token: 0x040003D5 RID: 981
		private UrlHelper _urlHelper;

		// Token: 0x040003D7 RID: 983
		private ClrTypeCache _typeMappingCache;

		// Token: 0x040003D8 RID: 984
		private IDictionary<object, object> _items;

		// Token: 0x040003D9 RID: 985
		private ODataQueryContext _queryContext;

		// Token: 0x040003DA RID: 986
		private SelectExpandClause _selectExpandClause;

		// Token: 0x040003DB RID: 987
		private bool _isSelectExpandClauseSet;
	}
}
