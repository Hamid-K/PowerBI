using System;
using System.ComponentModel;
using System.Web.Http.Controllers;

namespace System.Web.Http.Validation
{
	// Token: 0x02000092 RID: 146
	public sealed class ModelValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000A675 File Offset: 0x00008875
		public ModelValidatingEventArgs(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.ActionContext = actionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000A699 File Offset: 0x00008899
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000A6A1 File Offset: 0x000088A1
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000A6AA File Offset: 0x000088AA
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000A6B2 File Offset: 0x000088B2
		public ModelValidationNode ParentNode { get; private set; }
	}
}
