using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E5 RID: 741
	internal sealed class SurrogateContextFactory
	{
		// Token: 0x06001A88 RID: 6792 RVA: 0x0006BCE4 File Offset: 0x00069EE4
		internal static ImpersonationContext CreateSurrogateContext()
		{
			RSTrace.CatalogTrace.Assert(Globals.Configuration.IsSurrogatePresent, "SurrogateContextFactory.CreateContext(): Globals.Configuration.IsSurrogatePresent");
			ImpersonationContext impersonationContext = null;
			try
			{
				impersonationContext = new ImpersonationContext(Globals.Configuration.SurrogateUserName, Globals.Configuration.SurrogatePassword, Globals.Configuration.SurrogateDomain);
			}
			catch (LogonFailedException ex)
			{
				string text = ex.Message;
				if (ex.InnerException != null)
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", text, ex.InnerException.Message);
				}
				string text2 = string.Format(CultureInfo.InvariantCulture, "Login failed for Unattended Execution account: {0}", text);
				throw new ServerConfigurationErrorException(ex, text2, RepLibRes.ExecutionAccountLogonError);
			}
			return impersonationContext;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0006BD90 File Offset: 0x00069F90
		internal static ImpersonationContext CreateContext(out ReportProcessing.ExecutionType execType)
		{
			execType = ReportProcessing.ExecutionType.Live;
			ImpersonationContext impersonationContext;
			if (Globals.Configuration.IsSurrogatePresent)
			{
				impersonationContext = SurrogateContextFactory.CreateSurrogateContext();
			}
			else
			{
				impersonationContext = new ImpersonationContext();
			}
			RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
			if (jobContext != null)
			{
				if (jobContext.Type == JobTypeEnum.User)
				{
					execType = ReportProcessing.ExecutionType.Live;
				}
				if (jobContext.Type == JobTypeEnum.System)
				{
					if (Globals.Configuration.IsSurrogatePresent)
					{
						execType = ReportProcessing.ExecutionType.SurrogateAccount;
					}
					else
					{
						execType = ReportProcessing.ExecutionType.ServiceAccount;
					}
				}
			}
			return impersonationContext;
		}
	}
}
