using System;
using System.Web;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003F2 RID: 1010
	public class AspNetGlobalBase<T> : HttpApplication where T : ApplicationRoot, new()
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x000741DE File Offset: 0x000723DE
		protected void Application_Start(object sender, EventArgs e)
		{
			this.ApplicationStart();
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000741E6 File Offset: 0x000723E6
		protected void Application_End(object sender, EventArgs e)
		{
			this.ApplicationEnd();
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000741F0 File Offset: 0x000723F0
		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex = base.Server.GetLastError();
			if (ex != null && ex is HttpUnhandledException && ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			this.ApplicationError(ex);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0007422A File Offset: 0x0007242A
		protected void Session_Start(object sender, EventArgs e)
		{
			this.SessionStart();
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00074232 File Offset: 0x00072432
		protected void Session_End(object sender, EventArgs e)
		{
			this.SessionEnd();
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0007423A File Offset: 0x0007243A
		protected virtual void ApplicationStart()
		{
			this.m_winStarterContext = this.RunWinStarter();
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00074248 File Offset: 0x00072448
		protected virtual void ApplicationEnd()
		{
			WinStarter.StopRunAsync(this.m_winStarterContext);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void ApplicationError(Exception exception)
		{
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void SessionStart()
		{
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void SessionEnd()
		{
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00074255 File Offset: 0x00072455
		protected virtual IWinStarterRunAsyncContext RunWinStarter()
		{
			return WinStarter.RunAsync<T>(new string[0]);
		}

		// Token: 0x04000AED RID: 2797
		private IWinStarterRunAsyncContext m_winStarterContext;
	}
}
