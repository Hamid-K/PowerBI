using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Metadata;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000109 RID: 265
	public abstract class HttpParameterBinding
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0001160F File Offset: 0x0000F80F
		protected HttpParameterBinding(HttpParameterDescriptor descriptor)
		{
			if (descriptor == null)
			{
				throw Error.ArgumentNull("descriptor");
			}
			this._descriptor = descriptor;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00003B5D File Offset: 0x00001D5D
		public virtual bool WillReadBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001162C File Offset: 0x0000F82C
		public bool IsValid
		{
			get
			{
				return this.ErrorMessage == null;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000413B File Offset: 0x0000233B
		public virtual string ErrorMessage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00011637 File Offset: 0x0000F837
		public HttpParameterDescriptor Descriptor
		{
			get
			{
				return this._descriptor;
			}
		}

		// Token: 0x060006F2 RID: 1778
		public abstract Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken);

		// Token: 0x060006F3 RID: 1779 RVA: 0x00011640 File Offset: 0x0000F840
		protected object GetValue(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			object obj;
			actionContext.ActionArguments.TryGetValue(this.Descriptor.ParameterName, out obj);
			return obj;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00011675 File Offset: 0x0000F875
		protected void SetValue(HttpActionContext actionContext, object value)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			actionContext.ActionArguments[this.Descriptor.ParameterName] = value;
		}

		// Token: 0x040001C4 RID: 452
		private readonly HttpParameterDescriptor _descriptor;
	}
}
