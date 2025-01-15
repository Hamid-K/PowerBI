using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.Validation
{
	// Token: 0x02000091 RID: 145
	public sealed class ModelValidatedEventArgs : EventArgs
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000A62F File Offset: 0x0000882F
		public ModelValidatedEventArgs(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.ActionContext = actionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000A653 File Offset: 0x00008853
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000A65B File Offset: 0x0000885B
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000A664 File Offset: 0x00008864
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000A66C File Offset: 0x0000886C
		public ModelValidationNode ParentNode { get; private set; }
	}
}
