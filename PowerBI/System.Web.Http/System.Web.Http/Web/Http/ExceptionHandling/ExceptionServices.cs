using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000DC RID: 220
	public static class ExceptionServices
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		public static IExceptionLogger GetLogger(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			return ExceptionServices.GetLogger(configuration.Services);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000E6DB File Offset: 0x0000C8DB
		public static IExceptionLogger GetLogger(ServicesContainer services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.ExceptionServicesLogger.Value;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		internal static IExceptionLogger CreateLogger(ServicesContainer services)
		{
			return new CompositeExceptionLogger(services.GetExceptionLoggers());
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000E703 File Offset: 0x0000C903
		public static IExceptionHandler GetHandler(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			return ExceptionServices.GetHandler(configuration.Services);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000E71E File Offset: 0x0000C91E
		public static IExceptionHandler GetHandler(ServicesContainer services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.ExceptionServicesHandler.Value;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E739 File Offset: 0x0000C939
		internal static IExceptionHandler CreateHandler(ServicesContainer services)
		{
			return new LastChanceExceptionHandler(services.GetExceptionHandler() ?? new EmptyExceptionHandler());
		}
	}
}
