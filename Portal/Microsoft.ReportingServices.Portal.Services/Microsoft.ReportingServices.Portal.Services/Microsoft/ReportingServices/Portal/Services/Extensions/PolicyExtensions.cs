using System;
using System.Linq;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Models.Impl;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000067 RID: 103
	internal static class PolicyExtensions
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00014F9C File Offset: 0x0001319C
		public static Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Policy ToWebApiPolicy(this Microsoft.ReportingServices.Library.Soap.Policy soapPolicy)
		{
			if (soapPolicy == null)
			{
				throw new ArgumentNullException("soapPolicy");
			}
			Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Policy policy = new Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Policy();
			policy.GroupUserName = soapPolicy.GroupUserName;
			policy.Roles = soapPolicy.Roles.Select((Microsoft.ReportingServices.Library.Soap.Role x) => x.ToWebApiRole());
			return policy;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00014FF8 File Offset: 0x000131F8
		public static Microsoft.ReportingServices.Library.Soap.Policy ToSoapPolicy(this Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Policy webApiPolicy)
		{
			if (webApiPolicy == null)
			{
				throw new ArgumentNullException("webApiPolicy");
			}
			Microsoft.ReportingServices.Library.Soap.Policy policy = new Microsoft.ReportingServices.Library.Soap.Policy();
			policy.GroupUserName = webApiPolicy.GroupUserName;
			policy.Roles = webApiPolicy.Roles.Select((Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Role role) => new Microsoft.ReportingServices.Library.Soap.Role
			{
				Name = role.Name,
				Description = role.Description
			}).ToArray<Microsoft.ReportingServices.Library.Soap.Role>();
			return policy;
		}
	}
}
