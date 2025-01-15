using System;
using Microsoft.ReportingServices.Library.Soap;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000052 RID: 82
	internal static class RoleExtensions
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0001282A File Offset: 0x00010A2A
		public static global::Model.Role ToWebApiRole(this Microsoft.ReportingServices.Library.Soap.Role soapRole)
		{
			if (soapRole == null)
			{
				throw new ArgumentNullException("soapRole");
			}
			return new global::Model.Role
			{
				Name = soapRole.Name,
				Description = soapRole.Description
			};
		}
	}
}
