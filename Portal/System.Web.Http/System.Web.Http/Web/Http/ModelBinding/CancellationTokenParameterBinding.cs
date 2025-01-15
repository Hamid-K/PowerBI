using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000050 RID: 80
	public class CancellationTokenParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00006C31 File Offset: 0x00004E31
		public CancellationTokenParameterBinding(HttpParameterDescriptor descriptor)
			: base(descriptor)
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006C3C File Offset: 0x00004E3C
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			string parameterName = base.Descriptor.ParameterName;
			actionContext.ActionArguments.Add(parameterName, cancellationToken);
			return TaskHelpers.Completed();
		}
	}
}
