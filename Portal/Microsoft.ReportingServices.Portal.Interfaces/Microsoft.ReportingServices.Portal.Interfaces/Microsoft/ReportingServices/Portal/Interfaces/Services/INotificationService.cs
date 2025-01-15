using System;
using System.Collections.Generic;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x0200008E RID: 142
	public interface INotificationService
	{
		// Token: 0x06000460 RID: 1120
		IEnumerable<Notification> GetNotifications(IPrincipal userPrincipal);
	}
}
