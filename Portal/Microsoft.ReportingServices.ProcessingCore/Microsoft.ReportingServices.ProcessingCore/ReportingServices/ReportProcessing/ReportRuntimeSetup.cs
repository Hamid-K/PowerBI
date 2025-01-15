using System;
using System.Collections.Specialized;
using System.Security.Policy;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063A RID: 1594
	[Serializable]
	public sealed class ReportRuntimeSetup
	{
		// Token: 0x0600574A RID: 22346 RVA: 0x0016F3D0 File Offset: 0x0016D5D0
		public ReportRuntimeSetup(ReportRuntimeSetup originalSetup, AppDomain newAppDomain)
			: this(newAppDomain, originalSetup.m_exprHostEvidence, originalSetup.m_restrictCodeModulesInCurrentAppDomain, originalSetup.m_requireExpressionHostWithRefusedPermissions)
		{
			if (originalSetup.m_currentAppDomainTrustedCodeModules != null)
			{
				foreach (string text in originalSetup.m_currentAppDomainTrustedCodeModules)
				{
					this.AddTrustedCodeModuleInCurrentAppDomain(text);
				}
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x0016F448 File Offset: 0x0016D648
		public static ReportRuntimeSetup GetDefault()
		{
			if (ReportRuntimeSetup.DefaultRuntimeSetup == null)
			{
				ReportRuntimeSetup.DefaultRuntimeSetup = new ReportRuntimeSetup(null, null, false, false);
			}
			return ReportRuntimeSetup.DefaultRuntimeSetup;
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x0016F464 File Offset: 0x0016D664
		public static ReportRuntimeSetup CreateForSeparateAppDomainExecution(AppDomain exprHostAppDomain)
		{
			return new ReportRuntimeSetup(exprHostAppDomain, null, false, false);
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0016F46F File Offset: 0x0016D66F
		public static ReportRuntimeSetup CreateForCurrentAppDomainExecution()
		{
			return new ReportRuntimeSetup(null, null, true, true);
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x0016F47A File Offset: 0x0016D67A
		public static ReportRuntimeSetup CreateForCurrentAppDomainExecution(Evidence exprHostEvidence)
		{
			return new ReportRuntimeSetup(null, exprHostEvidence, true, false);
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x0016F485 File Offset: 0x0016D685
		public void AddTrustedCodeModuleInCurrentAppDomain(string assemblyName)
		{
			if (this.m_currentAppDomainTrustedCodeModules == null)
			{
				this.m_currentAppDomainTrustedCodeModules = new StringCollection();
			}
			if (!this.m_currentAppDomainTrustedCodeModules.Contains(assemblyName))
			{
				this.m_currentAppDomainTrustedCodeModules.Add(assemblyName);
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06005750 RID: 22352 RVA: 0x0016F4B5 File Offset: 0x0016D6B5
		public bool ExecutesInSeparateAppDomain
		{
			get
			{
				return this.ExprHostAppDomain != null;
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x0016F4C0 File Offset: 0x0016D6C0
		internal AppDomain ExprHostAppDomain
		{
			get
			{
				return this.m_exprHostAppDomain;
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06005752 RID: 22354 RVA: 0x0016F4C8 File Offset: 0x0016D6C8
		internal Evidence ExprHostEvidence
		{
			get
			{
				return this.m_exprHostEvidence;
			}
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x0016F4D0 File Offset: 0x0016D6D0
		internal bool CheckCodeModuleIsTrustedInCurrentAppDomain(string assemblyName)
		{
			return !this.m_restrictCodeModulesInCurrentAppDomain || (this.m_currentAppDomainTrustedCodeModules != null && this.m_currentAppDomainTrustedCodeModules.Contains(assemblyName));
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x0016F4F2 File Offset: 0x0016D6F2
		public bool RequireExpressionHostWithRefusedPermissions
		{
			get
			{
				return this.m_requireExpressionHostWithRefusedPermissions;
			}
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x0016F4FA File Offset: 0x0016D6FA
		private ReportRuntimeSetup(AppDomain exprHostAppDomain, Evidence exprHostEvidence, bool restrictCodeModulesInCurrentAppDomain, bool requireExpressionHostWithRefusedPermissions)
		{
			this.m_exprHostAppDomain = exprHostAppDomain;
			this.m_exprHostEvidence = exprHostEvidence;
			this.m_restrictCodeModulesInCurrentAppDomain = restrictCodeModulesInCurrentAppDomain;
			this.m_requireExpressionHostWithRefusedPermissions = requireExpressionHostWithRefusedPermissions;
		}

		// Token: 0x04002E28 RID: 11816
		private static ReportRuntimeSetup DefaultRuntimeSetup;

		// Token: 0x04002E29 RID: 11817
		[NonSerialized]
		private readonly AppDomain m_exprHostAppDomain;

		// Token: 0x04002E2A RID: 11818
		private readonly Evidence m_exprHostEvidence;

		// Token: 0x04002E2B RID: 11819
		private readonly bool m_restrictCodeModulesInCurrentAppDomain;

		// Token: 0x04002E2C RID: 11820
		private StringCollection m_currentAppDomainTrustedCodeModules;

		// Token: 0x04002E2D RID: 11821
		private readonly bool m_requireExpressionHostWithRefusedPermissions;
	}
}
