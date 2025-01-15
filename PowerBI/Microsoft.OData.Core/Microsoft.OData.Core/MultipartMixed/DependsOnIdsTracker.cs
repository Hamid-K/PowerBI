using System;
using System.Collections.Generic;

namespace Microsoft.OData.MultipartMixed
{
	// Token: 0x02000201 RID: 513
	internal sealed class DependsOnIdsTracker
	{
		// Token: 0x060016A3 RID: 5795 RVA: 0x0003F453 File Offset: 0x0003D653
		internal DependsOnIdsTracker()
		{
			this.topLevelDependsOnIds = new List<string>();
			this.changeSetDependsOnIds = new List<string>();
			this.isInChangeSet = false;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0003F478 File Offset: 0x0003D678
		internal void ChangeSetStarted()
		{
			this.isInChangeSet = true;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0003F481 File Offset: 0x0003D681
		internal void ChangeSetEnded()
		{
			this.isInChangeSet = false;
			this.changeSetDependsOnIds.Clear();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0003F495 File Offset: 0x0003D695
		internal void AddDependsOnId(string id)
		{
			if (this.isInChangeSet)
			{
				this.changeSetDependsOnIds.Add(id);
				return;
			}
			this.topLevelDependsOnIds.Add(id);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0003F4B8 File Offset: 0x0003D6B8
		internal IEnumerable<string> GetDependsOnIds()
		{
			if (!this.isInChangeSet)
			{
				return this.topLevelDependsOnIds;
			}
			return this.changeSetDependsOnIds;
		}

		// Token: 0x04000A48 RID: 2632
		private readonly IList<string> topLevelDependsOnIds;

		// Token: 0x04000A49 RID: 2633
		private readonly IList<string> changeSetDependsOnIds;

		// Token: 0x04000A4A RID: 2634
		private bool isInChangeSet;
	}
}
