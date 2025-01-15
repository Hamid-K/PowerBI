using System;
using System.Linq;
using Microsoft.ReportingServices.Library.Soap;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000040 RID: 64
	internal static class PolicyExtensions
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000106C4 File Offset: 0x0000E8C4
		public static global::Model.Policy ToWebApiPolicy(this Microsoft.ReportingServices.Library.Soap.Policy soapPolicy)
		{
			if (soapPolicy == null)
			{
				throw new ArgumentNullException("soapPolicy");
			}
			global::Model.Policy policy = new global::Model.Policy();
			policy.GroupUserName = soapPolicy.GroupUserName;
			policy.Roles = soapPolicy.Roles.Select((Microsoft.ReportingServices.Library.Soap.Role x) => x.ToWebApiRole());
			return policy;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00010720 File Offset: 0x0000E920
		public static Microsoft.ReportingServices.Library.Soap.Policy ToSoapPolicy(this global::Model.Policy webApiPolicy)
		{
			if (webApiPolicy == null)
			{
				throw new ArgumentNullException("webApiPolicy");
			}
			Microsoft.ReportingServices.Library.Soap.Policy policy = new Microsoft.ReportingServices.Library.Soap.Policy();
			policy.GroupUserName = webApiPolicy.GroupUserName;
			policy.Roles = webApiPolicy.Roles.Select((global::Model.Role role) => new Microsoft.ReportingServices.Library.Soap.Role
			{
				Name = role.Name,
				Description = role.Description
			}).ToArray<Microsoft.ReportingServices.Library.Soap.Role>();
			return policy;
		}
	}
}
