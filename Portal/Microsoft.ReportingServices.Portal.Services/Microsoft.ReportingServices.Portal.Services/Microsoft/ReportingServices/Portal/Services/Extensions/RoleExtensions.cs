using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Models.Impl;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000069 RID: 105
	internal static class RoleExtensions
	{
		// Token: 0x0600031C RID: 796 RVA: 0x000150C4 File Offset: 0x000132C4
		public static Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Role ToWebApiRole(this Microsoft.ReportingServices.Library.Soap.Role soapRole)
		{
			if (soapRole == null)
			{
				throw new ArgumentNullException("soapRole");
			}
			return new Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Role
			{
				Name = soapRole.Name,
				Description = soapRole.Description
			};
		}
	}
}
