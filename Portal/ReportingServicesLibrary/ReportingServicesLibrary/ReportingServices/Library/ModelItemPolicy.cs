using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000138 RID: 312
	internal sealed class ModelItemPolicy
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x0002E45A File Offset: 0x0002C65A
		internal ModelItemPolicy(string modelItemID, UserRights rights, bool inherited)
		{
			this.m_modelItemID = modelItemID;
			this.m_rights = rights;
			this.m_inherited = inherited;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002E477 File Offset: 0x0002C677
		internal string ModelItemID
		{
			get
			{
				return this.m_modelItemID;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0002E47F File Offset: 0x0002C67F
		internal bool Inherited
		{
			get
			{
				return this.m_inherited;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0002E487 File Offset: 0x0002C687
		internal UserRights Rights
		{
			get
			{
				return this.m_rights;
			}
		}

		// Token: 0x0400050B RID: 1291
		private string m_modelItemID;

		// Token: 0x0400050C RID: 1292
		private UserRights m_rights;

		// Token: 0x0400050D RID: 1293
		private bool m_inherited;
	}
}
