using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class EnableQueryAttribute : ActionFilterAttribute
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003390 File Offset: 0x00001590
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext == null)
			{
				throw Error.ArgumentNull("actionExecutedContext");
			}
			HttpRequestMessage request = actionExecutedContext.Request;
			if (request == null)
			{
				throw Error.Argument("actionExecutedContext", SRResources.ActionExecutedContextMustHaveRequest, new object[0]);
			}
			if (HttpRequestMessageExtensions.GetConfiguration(request) == null)
			{
				throw Error.Argument("actionExecutedContext", SRResources.RequestMustContainConfiguration, new object[0]);
			}
			if (actionExecutedContext.ActionContext == null)
			{
				throw Error.Argument("actionExecutedContext", SRResources.ActionExecutedContextMustHaveActionContext, new object[0]);
			}
			HttpActionDescriptor actionDescriptor = actionExecutedContext.ActionContext.ActionDescriptor;
			if (actionDescriptor == null)
			{
				throw Error.Argument("actionExecutedContext", SRResources.ActionContextMustHaveDescriptor, new object[0]);
			}
			HttpResponseMessage response = actionExecutedContext.Response;
			if (response != null && response.IsSuccessStatusCode && response.Content != null)
			{
				ObjectContent objectContent = response.Content as ObjectContent;
				if (objectContent == null)
				{
					throw Error.Argument("actionExecutedContext", SRResources.QueryingRequiresObjectContent, new object[] { response.Content.GetType().FullName });
				}
				IQueryable queryable = null;
				SingleResult singleResult = objectContent.Value as SingleResult;
				if (singleResult != null)
				{
					queryable = (from p in objectContent.Value.GetType().GetProperties()
						orderby p.GetIndexParameters().Count<ParameterInfo>()
						where p.Name.Equals("Queryable")
						select p).LastOrDefault<PropertyInfo>().GetValue(singleResult) as IQueryable;
				}
				object obj = this.OnActionExecuted(objectContent.Value, queryable, new WebApiActionDescriptor(actionDescriptor), new WebApiRequestMessage(request), (Type elementClrType) => this.GetModel(elementClrType, request, actionDescriptor), (ODataQueryContext queryContext) => this.CreateAndValidateQueryOptions(request, queryContext), delegate(HttpStatusCode statusCode)
				{
					actionExecutedContext.Response = HttpRequestMessageExtensions.CreateResponse(request, statusCode);
				}, delegate(HttpStatusCode statusCode, string message, Exception exception)
				{
					actionExecutedContext.Response = HttpRequestMessageExtensions.CreateErrorResponse(request, statusCode, message, exception);
				});
				if (obj != null)
				{
					objectContent.Value = obj;
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035AC File Offset: 0x000017AC
		private ODataQueryOptions CreateAndValidateQueryOptions(HttpRequestMessage request, ODataQueryContext queryContext)
		{
			ODataQueryOptions odataQueryOptions = new ODataQueryOptions(queryContext, request);
			this.ValidateQuery(request, odataQueryOptions);
			return odataQueryOptions;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035CC File Offset: 0x000017CC
		public virtual void ValidateQuery(HttpRequestMessage request, ODataQueryOptions queryOptions)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (queryOptions == null)
			{
				throw Error.ArgumentNull("queryOptions");
			}
			foreach (KeyValuePair<string, string> keyValuePair in HttpRequestMessageExtensions.GetQueryNameValuePairs(request))
			{
				if (!queryOptions.IsSupportedQueryOption(keyValuePair.Key) && keyValuePair.Key.StartsWith("$", StringComparison.Ordinal))
				{
					throw new HttpResponseException(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.BadRequest, Error.Format(SRResources.QueryParameterNotSupported, new object[] { keyValuePair.Key })));
				}
			}
			queryOptions.Validate(this._validationSettings);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003688 File Offset: 0x00001888
		public virtual IEdmModel GetModel(Type elementClrType, HttpRequestMessage request, HttpActionDescriptor actionDescriptor)
		{
			IEdmModel edmModel = request.GetModel();
			if (edmModel == EdmCoreModel.Instance || edmModel.GetEdmType(elementClrType) == null)
			{
				edmModel = actionDescriptor.GetEdmModel(elementClrType);
			}
			return edmModel;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000036B6 File Offset: 0x000018B6
		public EnableQueryAttribute()
		{
			this._validationSettings = new ODataValidationSettings();
			this._querySettings = new ODataQuerySettings();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036D4 File Offset: 0x000018D4
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000036E1 File Offset: 0x000018E1
		public bool EnsureStableOrdering
		{
			get
			{
				return this._querySettings.EnsureStableOrdering;
			}
			set
			{
				this._querySettings.EnsureStableOrdering = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000036EF File Offset: 0x000018EF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000036FC File Offset: 0x000018FC
		public HandleNullPropagationOption HandleNullPropagation
		{
			get
			{
				return this._querySettings.HandleNullPropagation;
			}
			set
			{
				this._querySettings.HandleNullPropagation = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000370A File Offset: 0x0000190A
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003717 File Offset: 0x00001917
		public bool EnableConstantParameterization
		{
			get
			{
				return this._querySettings.EnableConstantParameterization;
			}
			set
			{
				this._querySettings.EnableConstantParameterization = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003725 File Offset: 0x00001925
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003732 File Offset: 0x00001932
		public bool EnableCorrelatedSubqueryBuffering
		{
			get
			{
				return this._querySettings.EnableCorrelatedSubqueryBuffering;
			}
			set
			{
				this._querySettings.EnableCorrelatedSubqueryBuffering = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003740 File Offset: 0x00001940
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000374D File Offset: 0x0000194D
		public int MaxAnyAllExpressionDepth
		{
			get
			{
				return this._validationSettings.MaxAnyAllExpressionDepth;
			}
			set
			{
				this._validationSettings.MaxAnyAllExpressionDepth = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000375B File Offset: 0x0000195B
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003768 File Offset: 0x00001968
		public int MaxNodeCount
		{
			get
			{
				return this._validationSettings.MaxNodeCount;
			}
			set
			{
				this._validationSettings.MaxNodeCount = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003778 File Offset: 0x00001978
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003798 File Offset: 0x00001998
		public int PageSize
		{
			get
			{
				return this._querySettings.PageSize.GetValueOrDefault();
			}
			set
			{
				this._querySettings.PageSize = new int?(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000037AB File Offset: 0x000019AB
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000037B8 File Offset: 0x000019B8
		public bool HandleReferenceNavigationPropertyExpandFilter
		{
			get
			{
				return this._querySettings.HandleReferenceNavigationPropertyExpandFilter;
			}
			set
			{
				this._querySettings.HandleReferenceNavigationPropertyExpandFilter = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000037C6 File Offset: 0x000019C6
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000037D3 File Offset: 0x000019D3
		public AllowedQueryOptions AllowedQueryOptions
		{
			get
			{
				return this._validationSettings.AllowedQueryOptions;
			}
			set
			{
				this._validationSettings.AllowedQueryOptions = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037E1 File Offset: 0x000019E1
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000037EE File Offset: 0x000019EE
		public AllowedFunctions AllowedFunctions
		{
			get
			{
				return this._validationSettings.AllowedFunctions;
			}
			set
			{
				this._validationSettings.AllowedFunctions = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000037FC File Offset: 0x000019FC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003809 File Offset: 0x00001A09
		public AllowedArithmeticOperators AllowedArithmeticOperators
		{
			get
			{
				return this._validationSettings.AllowedArithmeticOperators;
			}
			set
			{
				this._validationSettings.AllowedArithmeticOperators = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003817 File Offset: 0x00001A17
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003824 File Offset: 0x00001A24
		public AllowedLogicalOperators AllowedLogicalOperators
		{
			get
			{
				return this._validationSettings.AllowedLogicalOperators;
			}
			set
			{
				this._validationSettings.AllowedLogicalOperators = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003832 File Offset: 0x00001A32
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000383C File Offset: 0x00001A3C
		public string AllowedOrderByProperties
		{
			get
			{
				return this._allowedOrderByProperties;
			}
			set
			{
				this._allowedOrderByProperties = value;
				if (string.IsNullOrEmpty(value))
				{
					this._validationSettings.AllowedOrderByProperties.Clear();
					return;
				}
				string[] array = this._allowedOrderByProperties.Split(new char[] { ',' });
				for (int i = 0; i < array.Length; i++)
				{
					this._validationSettings.AllowedOrderByProperties.Add(array[i].Trim());
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000038A8 File Offset: 0x00001AA8
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000038C8 File Offset: 0x00001AC8
		public int MaxSkip
		{
			get
			{
				return this._validationSettings.MaxSkip.GetValueOrDefault();
			}
			set
			{
				this._validationSettings.MaxSkip = new int?(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000038DC File Offset: 0x00001ADC
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000038FC File Offset: 0x00001AFC
		public int MaxTop
		{
			get
			{
				return this._validationSettings.MaxTop.GetValueOrDefault();
			}
			set
			{
				this._validationSettings.MaxTop = new int?(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000390F File Offset: 0x00001B0F
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000391C File Offset: 0x00001B1C
		public int MaxExpansionDepth
		{
			get
			{
				return this._validationSettings.MaxExpansionDepth;
			}
			set
			{
				this._validationSettings.MaxExpansionDepth = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000392A File Offset: 0x00001B2A
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003937 File Offset: 0x00001B37
		public int MaxOrderByNodeCount
		{
			get
			{
				return this._validationSettings.MaxOrderByNodeCount;
			}
			set
			{
				this._validationSettings.MaxOrderByNodeCount = value;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003948 File Offset: 0x00001B48
		private object OnActionExecuted(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor, IWebApiRequestMessage request, Func<Type, IEdmModel> modelFunction, Func<ODataQueryContext, ODataQueryOptions> createQueryOptionFunction, Action<HttpStatusCode> createResponseAction, Action<HttpStatusCode, string, Exception> createErrorAction)
		{
			if (this._querySettings.PageSize == null && responseValue != null)
			{
				this.GetModelBoundPageSize(responseValue, singleResultCollection, actionDescriptor, modelFunction, request.Context.Path, createErrorAction);
			}
			bool flag = responseValue != null && request.RequestUri != null && (!string.IsNullOrWhiteSpace(request.RequestUri.Query) || this._querySettings.PageSize != null || this._querySettings.ModelBoundPageSize != null || singleResultCollection != null || request.IsCountRequest() || EnableQueryAttribute.ContainsAutoSelectExpandProperty(responseValue, singleResultCollection, actionDescriptor, modelFunction, request.Context.Path));
			object obj = null;
			if (flag)
			{
				try
				{
					object obj2 = this.ExecuteQuery(responseValue, singleResultCollection, actionDescriptor, modelFunction, request, createQueryOptionFunction);
					if (obj2 == null && (request.Context.Path == null || singleResultCollection != null))
					{
						createResponseAction(HttpStatusCode.NotFound);
					}
					obj = obj2;
				}
				catch (ArgumentOutOfRangeException ex)
				{
					createErrorAction(HttpStatusCode.BadRequest, Error.Format(SRResources.QueryParameterNotSupported, new object[] { ex.Message }), ex);
				}
				catch (NotImplementedException ex2)
				{
					createErrorAction(HttpStatusCode.BadRequest, Error.Format(SRResources.UriQueryStringInvalid, new object[] { ex2.Message }), ex2);
				}
				catch (NotSupportedException ex3)
				{
					createErrorAction(HttpStatusCode.BadRequest, Error.Format(SRResources.UriQueryStringInvalid, new object[] { ex3.Message }), ex3);
				}
				catch (InvalidOperationException ex4)
				{
					createErrorAction(HttpStatusCode.BadRequest, Error.Format(SRResources.UriQueryStringInvalid, new object[] { ex4.Message }), ex4);
				}
			}
			return obj;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B1C File Offset: 0x00001D1C
		public virtual IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
		{
			if (queryable == null)
			{
				throw Error.ArgumentNull("queryable");
			}
			if (queryOptions == null)
			{
				throw Error.ArgumentNull("queryOptions");
			}
			return queryOptions.ApplyTo(queryable, this._querySettings);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B47 File Offset: 0x00001D47
		public virtual object ApplyQuery(object entity, ODataQueryOptions queryOptions)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			if (queryOptions == null)
			{
				throw Error.ArgumentNull("queryOptions");
			}
			return queryOptions.ApplyTo(entity, this._querySettings);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B74 File Offset: 0x00001D74
		private static ODataQueryContext GetODataQueryContext(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor, Func<Type, IEdmModel> modelFunction, ODataPath path)
		{
			Type elementType = EnableQueryAttribute.GetElementType(responseValue, singleResultCollection, actionDescriptor);
			IEdmModel edmModel = modelFunction(elementType);
			if (edmModel == null)
			{
				throw Error.InvalidOperation(SRResources.QueryGetModelMustNotReturnNull, new object[0]);
			}
			return new ODataQueryContext(edmModel, elementType, path);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003BB0 File Offset: 0x00001DB0
		private void GetModelBoundPageSize(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor, Func<Type, IEdmModel> modelFunction, ODataPath path, Action<HttpStatusCode, string, Exception> createErrorAction)
		{
			ODataQueryContext odataQueryContext = null;
			try
			{
				odataQueryContext = EnableQueryAttribute.GetODataQueryContext(responseValue, singleResultCollection, actionDescriptor, modelFunction, path);
			}
			catch (InvalidOperationException ex)
			{
				createErrorAction(HttpStatusCode.BadRequest, Error.Format(SRResources.UriQueryStringInvalid, new object[] { ex.Message }), ex);
				return;
			}
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(odataQueryContext.TargetProperty, odataQueryContext.TargetStructuredType, odataQueryContext.Model, null);
			if (modelBoundQuerySettings != null && modelBoundQuerySettings.PageSize != null)
			{
				this._querySettings.ModelBoundPageSize = modelBoundQuerySettings.PageSize;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C44 File Offset: 0x00001E44
		private object ExecuteQuery(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor, Func<Type, IEdmModel> modelFunction, IWebApiRequestMessage request, Func<ODataQueryContext, ODataQueryOptions> createQueryOptionFunction)
		{
			ODataQueryContext odataQueryContext = EnableQueryAttribute.GetODataQueryContext(responseValue, singleResultCollection, actionDescriptor, modelFunction, request.Context.Path);
			ODataQueryOptions odataQueryOptions = createQueryOptionFunction(odataQueryContext);
			IEnumerable enumerable = responseValue as IEnumerable;
			if (enumerable != null && !(responseValue is string) && !(responseValue is byte[]))
			{
				IQueryable queryable = (enumerable as IQueryable) ?? enumerable.AsQueryable();
				queryable = this.ApplyQuery(queryable, odataQueryOptions);
				if (request.IsCountRequest())
				{
					long? totalCount = request.Context.TotalCount;
					if (totalCount != null)
					{
						return totalCount.Value;
					}
				}
				return queryable;
			}
			EnableQueryAttribute.ValidateSelectExpandOnly(odataQueryOptions);
			if (singleResultCollection == null)
			{
				return this.ApplyQuery(responseValue, odataQueryOptions);
			}
			IQueryable queryable2 = this.ApplyQuery(singleResultCollection, odataQueryOptions);
			return EnableQueryAttribute.SingleOrDefault(queryable2, actionDescriptor);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003CFC File Offset: 0x00001EFC
		internal static Type GetElementType(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor)
		{
			IEnumerable enumerable = responseValue as IEnumerable;
			if (enumerable == null)
			{
				if (singleResultCollection == null)
				{
					return responseValue.GetType();
				}
				enumerable = singleResultCollection;
			}
			Type implementedIEnumerableType = TypeHelper.GetImplementedIEnumerableType(enumerable.GetType());
			if (implementedIEnumerableType == null)
			{
				throw Error.InvalidOperation(SRResources.FailedToRetrieveTypeToBuildEdmModel, new object[]
				{
					typeof(EnableQueryAttribute).Name,
					actionDescriptor.ActionName,
					actionDescriptor.ControllerName,
					responseValue.GetType().FullName
				});
			}
			return implementedIEnumerableType;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D78 File Offset: 0x00001F78
		internal static object SingleOrDefault(IQueryable queryable, IWebApiActionDescriptor actionDescriptor)
		{
			object obj2;
			using (IEnumerator enumerator = queryable.GetEnumerator())
			{
				object obj = (enumerator.MoveNext() ? enumerator.Current : null);
				if (enumerator.MoveNext())
				{
					throw new InvalidOperationException(Error.Format(SRResources.SingleResultHasMoreThanOneEntity, new object[] { actionDescriptor.ActionName, actionDescriptor.ControllerName, "SingleResult" }));
				}
				obj2 = obj;
			}
			return obj2;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003DFC File Offset: 0x00001FFC
		internal static void ValidateSelectExpandOnly(ODataQueryOptions queryOptions)
		{
			if (queryOptions.Filter != null || queryOptions.Count != null || queryOptions.OrderBy != null || queryOptions.Skip != null || queryOptions.Top != null)
			{
				throw new ODataException(Error.Format(SRResources.NonSelectExpandOnSingleEntity, new object[0]));
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E3C File Offset: 0x0000203C
		private static bool ContainsAutoSelectExpandProperty(object responseValue, IQueryable singleResultCollection, IWebApiActionDescriptor actionDescriptor, Func<Type, IEdmModel> modelFunction, ODataPath path)
		{
			Type elementType = EnableQueryAttribute.GetElementType(responseValue, singleResultCollection, actionDescriptor);
			IEdmModel model = modelFunction(elementType);
			if (model == null)
			{
				throw Error.InvalidOperation(SRResources.QueryGetModelMustNotReturnNull, new object[0]);
			}
			IEdmEntityType edmEntityType = model.GetEdmType(elementType) as IEdmEntityType;
			IEdmStructuredType edmStructuredType = model.GetEdmType(elementType) as IEdmStructuredType;
			IEdmProperty property = null;
			if (path != null)
			{
				string text;
				EdmLibHelpers.GetPropertyAndStructuredTypeFromPath(path.Segments, out property, out edmStructuredType, out text);
			}
			if (edmEntityType != null)
			{
				List<IEdmEntityType> list = new List<IEdmEntityType>();
				list.Add(edmEntityType);
				list.AddRange(EdmLibHelpers.GetAllDerivedEntityTypes(edmEntityType, model));
				using (List<IEdmEntityType>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmEntityType entityType = enumerator.Current;
						IEnumerable<IEdmNavigationProperty> enumerable = ((entityType == edmEntityType) ? entityType.NavigationProperties() : entityType.DeclaredNavigationProperties());
						if (enumerable != null && enumerable.Any((IEdmNavigationProperty navigationProperty) => EdmLibHelpers.IsAutoExpand(navigationProperty, property, entityType, model, false, null)))
						{
							return true;
						}
						IEnumerable<IEdmStructuralProperty> enumerable2 = ((entityType == edmEntityType) ? entityType.StructuralProperties() : entityType.DeclaredStructuralProperties());
						if (enumerable2 != null)
						{
							using (IEnumerator<IEdmStructuralProperty> enumerator2 = enumerable2.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (EdmLibHelpers.IsAutoSelect(enumerator2.Current, property, entityType, model, null))
									{
										return true;
									}
								}
							}
						}
					}
					return false;
				}
			}
			if (edmStructuredType != null)
			{
				IEnumerable<IEdmStructuralProperty> enumerable3 = edmStructuredType.StructuralProperties();
				if (enumerable3 != null)
				{
					using (IEnumerator<IEdmStructuralProperty> enumerator2 = enumerable3.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (EdmLibHelpers.IsAutoSelect(enumerator2.Current, property, edmStructuredType, model, null))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000018 RID: 24
		private const char CommaSeparator = ',';

		// Token: 0x04000019 RID: 25
		private ODataValidationSettings _validationSettings;

		// Token: 0x0400001A RID: 26
		private string _allowedOrderByProperties;

		// Token: 0x0400001B RID: 27
		private ODataQuerySettings _querySettings;
	}
}
