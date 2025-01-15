using System;
using System.Threading.Tasks;

namespace Microsoft.Owin
{
	// Token: 0x02000013 RID: 19
	public abstract class OwinMiddleware
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00002906 File Offset: 0x00000B06
		protected OwinMiddleware(OwinMiddleware next)
		{
			this.Next = next;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000291D File Offset: 0x00000B1D
		protected OwinMiddleware Next { get; set; }

		// Token: 0x060000BC RID: 188
		public abstract Task Invoke(IOwinContext context);
	}
}
