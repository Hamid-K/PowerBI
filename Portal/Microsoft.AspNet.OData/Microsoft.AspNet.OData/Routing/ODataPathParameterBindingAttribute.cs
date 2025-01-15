using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ODataPathParameterBindingAttribute : ParameterBindingAttribute
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			return new ODataPathParameterBindingAttribute.ODataPathParameterBinding(parameter);
		}

		// Token: 0x02000207 RID: 519
		internal class ODataPathParameterBinding : HttpParameterBinding
		{
			// Token: 0x06001033 RID: 4147 RVA: 0x0003FF15 File Offset: 0x0003E115
			public ODataPathParameterBinding(HttpParameterDescriptor parameterDescriptor)
				: base(parameterDescriptor)
			{
			}

			// Token: 0x06001034 RID: 4148 RVA: 0x00040520 File Offset: 0x0003E720
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
				base.SetValue(actionContext, request.ODataProperties().Path);
				return TaskHelpers.Completed();
			}
		}
	}
}
