using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ODataQueryParameterBindingAttribute : ParameterBindingAttribute
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000335A File Offset: 0x0000155A
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			return new ODataQueryParameterBindingAttribute.ODataQueryParameterBinding(parameter);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003362 File Offset: 0x00001562
		internal static Type GetEntityClrTypeFromParameterType(Type parameterType)
		{
			if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<>))
			{
				return parameterType.GetGenericArguments().Single<Type>();
			}
			return null;
		}

		// Token: 0x020001E9 RID: 489
		internal class ODataQueryParameterBinding : HttpParameterBinding
		{
			// Token: 0x06000FD6 RID: 4054 RVA: 0x0003FF15 File Offset: 0x0003E115
			public ODataQueryParameterBinding(HttpParameterDescriptor parameterDescriptor)
				: base(parameterDescriptor)
			{
			}

			// Token: 0x06000FD7 RID: 4055 RVA: 0x0003FF20 File Offset: 0x0003E120
			public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
			{
				if (actionContext == null)
				{
					throw Error.ArgumentNull("actionContext");
				}
				HttpRequestMessage request = actionContext.Request;
				if (request == null)
				{
					throw Error.Argument("actionContext", SRResources.ActionContextMustHaveRequest, new object[0]);
				}
				HttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor;
				if (actionDescriptor == null)
				{
					throw Error.Argument("actionContext", SRResources.ActionContextMustHaveDescriptor, new object[0]);
				}
				if (HttpRequestMessageExtensions.GetConfiguration(request) == null)
				{
					throw Error.Argument("actionContext", SRResources.RequestMustContainConfiguration, new object[0]);
				}
				Type entityClrType = ODataQueryParameterBindingAttribute.GetEntityClrTypeFromParameterType(base.Descriptor.ParameterType) ?? ODataQueryParameterBindingAttribute.ODataQueryParameterBinding.GetEntityClrTypeFromActionReturnType(actionDescriptor);
				IEdmModel model = request.GetModel();
				ODataQueryContext odataQueryContext = new ODataQueryContext((model != EdmCoreModel.Instance) ? model : actionDescriptor.GetEdmModel(entityClrType), entityClrType, request.ODataProperties().Path);
				ODataQueryOptions odataQueryOptions = ((Func<ODataQueryContext, HttpRequestMessage, ODataQueryOptions>)base.Descriptor.Properties.GetOrAdd("MS_CreateODataQueryOptionsOfT", (object _) => Delegate.CreateDelegate(typeof(Func<ODataQueryContext, HttpRequestMessage, ODataQueryOptions>), ODataQueryParameterBindingAttribute.ODataQueryParameterBinding._createODataQueryOptions.MakeGenericMethod(new Type[] { entityClrType }))))(odataQueryContext, request);
				base.SetValue(actionContext, odataQueryOptions);
				return TaskHelpers.Completed();
			}

			// Token: 0x06000FD8 RID: 4056 RVA: 0x00040033 File Offset: 0x0003E233
			public static ODataQueryOptions<T> CreateODataQueryOptions<T>(ODataQueryContext context, HttpRequestMessage request)
			{
				return new ODataQueryOptions<T>(context, request);
			}

			// Token: 0x06000FD9 RID: 4057 RVA: 0x0004003C File Offset: 0x0003E23C
			internal static Type GetEntityClrTypeFromActionReturnType(HttpActionDescriptor actionDescriptor)
			{
				if (actionDescriptor.ReturnType == null)
				{
					throw Error.InvalidOperation(SRResources.FailedToBuildEdmModelBecauseReturnTypeIsNull, new object[]
					{
						actionDescriptor.ActionName,
						actionDescriptor.ControllerDescriptor.ControllerName
					});
				}
				Type implementedIEnumerableType = TypeHelper.GetImplementedIEnumerableType(actionDescriptor.ReturnType);
				if (implementedIEnumerableType == null)
				{
					throw Error.InvalidOperation(SRResources.FailedToRetrieveTypeToBuildEdmModel, new object[]
					{
						actionDescriptor.ActionName,
						actionDescriptor.ControllerDescriptor.ControllerName,
						actionDescriptor.ReturnType.FullName
					});
				}
				return implementedIEnumerableType;
			}

			// Token: 0x04000470 RID: 1136
			private static MethodInfo _createODataQueryOptions = typeof(ODataQueryParameterBindingAttribute.ODataQueryParameterBinding).GetMethod("CreateODataQueryOptions");

			// Token: 0x04000471 RID: 1137
			private const string CreateODataQueryOptionsCtorKey = "MS_CreateODataQueryOptionsOfT";
		}
	}
}
