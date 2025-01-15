using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000053 RID: 83
	public class HttpRequestParameterBinding : HttpParameterBinding
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00006C31 File Offset: 0x00004E31
		public HttpRequestParameterBinding(HttpParameterDescriptor descriptor)
			: base(descriptor)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007140 File Offset: 0x00005340
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			string parameterName = base.Descriptor.ParameterName;
			HttpRequestMessage request = actionContext.ControllerContext.Request;
			actionContext.ActionArguments.Add(parameterName, request);
			return TaskHelpers.Completed();
		}
	}
}
