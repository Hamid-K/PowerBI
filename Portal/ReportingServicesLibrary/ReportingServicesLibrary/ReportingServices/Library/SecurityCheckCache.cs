using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002D RID: 45
	internal sealed class SecurityCheckCache
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00006528 File Offset: 0x00004728
		internal SecurityCheckCache(Security securityManager, CommonOperation operation)
		{
			this.m_securityManager = securityManager;
			this.m_operation = operation;
			this.m_typesChecked = new SecurityCheckCache.CheckResult[SecurityCheckCache.CacheArrayMax];
			this.SetNewPolicy(Guid.Empty, null, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000655B File Offset: 0x0000475B
		internal SecurityCheckCache.CheckResult CheckAccess(ItemType type, Guid policyId)
		{
			if (policyId != this.m_cachedPolicyId)
			{
				return SecurityCheckCache.CheckResult.NotChecked;
			}
			return this.CheckAccessForCachedPolicy(type);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006574 File Offset: 0x00004774
		internal SecurityCheckCache.CheckResult CheckAccess(ItemType type, Guid policyId, byte[] securityDescriptor, ExternalItemPath itemPath)
		{
			if (policyId != this.m_cachedPolicyId)
			{
				this.SetNewPolicy(policyId, securityDescriptor, itemPath);
			}
			return this.CheckAccessForCachedPolicy(type);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006598 File Offset: 0x00004798
		private void SetNewPolicy(Guid policyId, byte[] securityDescriptor, ExternalItemPath itemPath)
		{
			this.m_cachedPolicyId = policyId;
			this.m_cachedSecurityDescriptor = securityDescriptor;
			this.m_itemPath = itemPath;
			for (int i = 0; i < SecurityCheckCache.CacheArrayMax; i++)
			{
				this.m_typesChecked[i] = SecurityCheckCache.CheckResult.NotChecked;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000065D4 File Offset: 0x000047D4
		private SecurityCheckCache.CheckResult CheckAccessForCachedPolicy(ItemType type)
		{
			if (this.m_typesChecked[(int)type] != SecurityCheckCache.CheckResult.NotChecked)
			{
				return this.m_typesChecked[(int)type];
			}
			SecurityCheckCache.CheckResult checkResult;
			if (this.m_securityManager.CheckAccess(type, this.m_cachedSecurityDescriptor, this.m_operation, this.m_itemPath))
			{
				checkResult = SecurityCheckCache.CheckResult.AccessGranted;
			}
			else
			{
				checkResult = SecurityCheckCache.CheckResult.AccessDenied;
			}
			this.m_typesChecked[(int)type] = checkResult;
			return checkResult;
		}

		// Token: 0x04000107 RID: 263
		private static readonly int CacheArrayMax = ItemTypeHelpers.MaxItemType + 1;

		// Token: 0x04000108 RID: 264
		private Security m_securityManager;

		// Token: 0x04000109 RID: 265
		private CommonOperation m_operation;

		// Token: 0x0400010A RID: 266
		private Guid m_cachedPolicyId;

		// Token: 0x0400010B RID: 267
		private byte[] m_cachedSecurityDescriptor;

		// Token: 0x0400010C RID: 268
		private SecurityCheckCache.CheckResult[] m_typesChecked;

		// Token: 0x0400010D RID: 269
		private ExternalItemPath m_itemPath;

		// Token: 0x02000437 RID: 1079
		internal enum CheckResult
		{
			// Token: 0x04000F13 RID: 3859
			NotChecked,
			// Token: 0x04000F14 RID: 3860
			AccessGranted,
			// Token: 0x04000F15 RID: 3861
			AccessDenied
		}
	}
}
