using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Metadata;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FD RID: 253
	public class HttpActionBinding
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public HttpActionBinding()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00010599 File Offset: 0x0000E799
		public HttpActionBinding(HttpActionDescriptor actionDescriptor, HttpParameterBinding[] bindings)
		{
			this.ActionDescriptor = actionDescriptor;
			this.ParameterBindings = bindings;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000105AF File Offset: 0x0000E7AF
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x000105B7 File Offset: 0x0000E7B7
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000105CE File Offset: 0x0000E7CE
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x000105D6 File Offset: 0x0000E7D6
		public HttpParameterBinding[] ParameterBindings
		{
			get
			{
				return this._parameterBindings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._parameterBindings = value;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000105F0 File Offset: 0x0000E7F0
		public virtual Task ExecuteBindingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (this._parameterBindings.Length == 0)
			{
				return TaskHelpers.Completed();
			}
			for (int i = 0; i < this.ParameterBindings.Length; i++)
			{
				HttpParameterBinding httpParameterBinding = this.ParameterBindings[i];
				if (!httpParameterBinding.IsValid)
				{
					throw new InvalidOperationException(httpParameterBinding.ErrorMessage);
				}
			}
			if (this._metadataProvider == null)
			{
				HttpConfiguration configuration = actionContext.ControllerContext.Configuration;
				this._metadataProvider = configuration.Services.GetModelMetadataProvider();
			}
			return this.ExecuteBindingAsyncCore(actionContext, cancellationToken);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001066C File Offset: 0x0000E86C
		private async Task ExecuteBindingAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			for (int index = 0; index < this.ParameterBindings.Length; index++)
			{
				await this.ParameterBindings[index].ExecuteBindingAsync(this._metadataProvider, actionContext, cancellationToken);
			}
		}

		// Token: 0x040001A2 RID: 418
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x040001A3 RID: 419
		private HttpParameterBinding[] _parameterBindings;

		// Token: 0x040001A4 RID: 420
		private ModelMetadataProvider _metadataProvider;
	}
}
