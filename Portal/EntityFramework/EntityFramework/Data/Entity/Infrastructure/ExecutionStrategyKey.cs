using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000242 RID: 578
	public class ExecutionStrategyKey
	{
		// Token: 0x06001E42 RID: 7746 RVA: 0x0005472C File Offset: 0x0005292C
		public ExecutionStrategyKey(string providerInvariantName, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this.ProviderInvariantName = providerInvariantName;
			this.ServerName = serverName;
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0005474E File Offset: 0x0005294E
		// (set) Token: 0x06001E44 RID: 7748 RVA: 0x00054756 File Offset: 0x00052956
		public string ProviderInvariantName { get; private set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x0005475F File Offset: 0x0005295F
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x00054767 File Offset: 0x00052967
		public string ServerName { get; private set; }

		// Token: 0x06001E47 RID: 7751 RVA: 0x00054770 File Offset: 0x00052970
		public override bool Equals(object obj)
		{
			ExecutionStrategyKey executionStrategyKey = obj as ExecutionStrategyKey;
			return executionStrategyKey != null && this.ProviderInvariantName.Equals(executionStrategyKey.ProviderInvariantName, StringComparison.Ordinal) && ((this.ServerName == null && executionStrategyKey.ServerName == null) || (this.ServerName != null && this.ServerName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal)));
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000547CD File Offset: 0x000529CD
		public override int GetHashCode()
		{
			if (this.ServerName != null)
			{
				return this.ProviderInvariantName.GetHashCode() ^ this.ServerName.GetHashCode();
			}
			return this.ProviderInvariantName.GetHashCode();
		}
	}
}
