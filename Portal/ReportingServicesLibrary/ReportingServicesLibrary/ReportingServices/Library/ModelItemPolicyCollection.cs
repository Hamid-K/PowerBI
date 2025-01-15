using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000139 RID: 313
	internal sealed class ModelItemPolicyCollection
	{
		// Token: 0x1700040B RID: 1035
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0002E48F File Offset: 0x0002C68F
		internal bool CacheInherited
		{
			set
			{
				this.m_cacheInherited = value;
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002E498 File Offset: 0x0002C698
		internal ModelItemPolicy GetPolicy(ModelItem modelItem)
		{
			ModelItemPolicy modelItemPolicy = (ModelItemPolicy)this.m_policies[ModelItem.IDToString(modelItem.ID)];
			if (modelItemPolicy == null)
			{
				ModelItem parentItem = modelItem.ParentItem;
				if (parentItem == null)
				{
					modelItemPolicy = new ModelItemPolicy(ModelItem.IDToString(modelItem.ID), new UserRights(), true);
				}
				else
				{
					ModelItemPolicy policy = this.GetPolicy(parentItem);
					modelItemPolicy = new ModelItemPolicy(ModelItem.IDToString(modelItem.ID), policy.Rights, true);
				}
				if (this.m_cacheInherited)
				{
					this.AddPolicy(modelItemPolicy);
				}
			}
			return modelItemPolicy;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002E517 File Offset: 0x0002C717
		internal ICollection GetAllPolicies()
		{
			return this.m_policies.Values;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002E524 File Offset: 0x0002C724
		internal void AddPolicyRoot(string modelItemID, byte[] securityDescriptor, string xmlPolicy, ExternalItemPath itemPath)
		{
			UserRights userRights = new UserRights(securityDescriptor, xmlPolicy, itemPath);
			ModelItemPolicy modelItemPolicy = new ModelItemPolicy(modelItemID, userRights, false);
			this.AddPolicy(modelItemPolicy);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002E54B File Offset: 0x0002C74B
		private void AddPolicy(ModelItemPolicy policy)
		{
			this.m_policies[policy.ModelItemID] = policy;
		}

		// Token: 0x0400050E RID: 1294
		private Hashtable m_policies = new Hashtable(StringComparer.Ordinal);

		// Token: 0x0400050F RID: 1295
		private bool m_cacheInherited;
	}
}
