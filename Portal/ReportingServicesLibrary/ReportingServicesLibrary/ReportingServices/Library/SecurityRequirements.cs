using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012B RID: 299
	internal sealed class SecurityRequirements
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0002D68C File Offset: 0x0002B88C
		private static SecurityRequirements BuildFromList(IEnumerable<SecurityCheck> checks, Security security, string userName)
		{
			SecurityRequirements securityRequirements = new SecurityRequirements(security, userName);
			foreach (SecurityCheck securityCheck in checks)
			{
				securityRequirements.AddCheck(securityCheck);
			}
			return securityRequirements;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		public static SecurityRequirements None
		{
			get
			{
				return SecurityRequirements.m_noRequirements;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002D6E7 File Offset: 0x0002B8E7
		public static SecurityRequirements GenerateForLoadCompiledDefinition(Security security, string userName)
		{
			return SecurityRequirements.BuildFromList(SecurityRequirements.m_loadCompiledDefinition, security, userName);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002D6F5 File Offset: 0x0002B8F5
		public static SecurityRequirements GenerateForExecuteReport(Security security, string userName)
		{
			return SecurityRequirements.BuildFromList(SecurityRequirements.m_executeReport, security, userName);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002D703 File Offset: 0x0002B903
		public SecurityRequirements()
		{
			this.m_security = null;
			this.m_userName = null;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002D724 File Offset: 0x0002B924
		public SecurityRequirements(Security security, string userName)
		{
			RSTrace.CatalogTrace.Assert(security != null, "security");
			this.m_security = security;
			this.m_userName = userName;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002D758 File Offset: 0x0002B958
		public void AddCheck(SecurityCheck check)
		{
			RSTrace.CatalogTrace.Assert(check != null, "check");
			this.m_checks.Add(check);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002D77C File Offset: 0x0002B97C
		public void CheckAccess(ItemType itemType, byte[] securityDescriptor, ExternalItemPath itemPath)
		{
			using (List<SecurityCheck>.Enumerator enumerator = this.m_checks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check(this.m_security, itemType, securityDescriptor, itemPath))
					{
						throw new AccessDeniedException(this.m_userName, ErrorCode.rsAccessDenied);
					}
				}
			}
		}

		// Token: 0x040004E6 RID: 1254
		private static readonly SecurityCheck[] m_loadCompiledDefinition = new SecurityCheck[] { ReportOptionSecurityCheck.ReadProperties };

		// Token: 0x040004E7 RID: 1255
		private static readonly SecurityCheck[] m_executeReport = new SecurityCheck[]
		{
			ReportOptionSecurityCheck.ReadProperties,
			ReportOptionSecurityCheck.ExecuteAndView
		};

		// Token: 0x040004E8 RID: 1256
		private static readonly SecurityRequirements m_noRequirements = new SecurityRequirements();

		// Token: 0x040004E9 RID: 1257
		private readonly List<SecurityCheck> m_checks = new List<SecurityCheck>();

		// Token: 0x040004EA RID: 1258
		private readonly Security m_security;

		// Token: 0x040004EB RID: 1259
		private readonly string m_userName;
	}
}
