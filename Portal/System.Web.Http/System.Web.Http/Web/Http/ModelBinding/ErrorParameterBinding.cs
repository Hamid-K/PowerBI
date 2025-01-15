using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000051 RID: 81
	public class ErrorParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00006C6C File Offset: 0x00004E6C
		public ErrorParameterBinding(HttpParameterDescriptor descriptor, string message)
			: base(descriptor)
		{
			if (message == null)
			{
				throw Error.ArgumentNull(message);
			}
			this._message = message;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006C86 File Offset: 0x00004E86
		public override string ErrorMessage
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006C8E File Offset: 0x00004E8E
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return TaskHelpers.FromError(new InvalidOperationException());
		}

		// Token: 0x04000082 RID: 130
		private readonly string _message;
	}
}
