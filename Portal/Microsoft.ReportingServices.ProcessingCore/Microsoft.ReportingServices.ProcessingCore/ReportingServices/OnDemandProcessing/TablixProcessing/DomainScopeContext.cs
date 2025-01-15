using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C4 RID: 2244
	internal sealed class DomainScopeContext
	{
		// Token: 0x17002870 RID: 10352
		// (get) Token: 0x06007AEF RID: 31471 RVA: 0x001FA47F File Offset: 0x001F867F
		// (set) Token: 0x06007AF0 RID: 31472 RVA: 0x001FA487 File Offset: 0x001F8687
		internal DomainScopeContext.DomainScopeInfo CurrentDomainScope
		{
			get
			{
				return this.m_currentDomainScopeInfo;
			}
			set
			{
				this.m_currentDomainScopeInfo = value;
			}
		}

		// Token: 0x17002871 RID: 10353
		// (get) Token: 0x06007AF1 RID: 31473 RVA: 0x001FA490 File Offset: 0x001F8690
		internal Dictionary<int, IReference<RuntimeGroupRootObj>> DomainScopes
		{
			get
			{
				return this.m_domainScopes;
			}
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x001FA498 File Offset: 0x001F8698
		internal void AddDomainScopes(IReference<RuntimeMemberObj>[] membersDef, int startIndex)
		{
			for (int i = startIndex; i < membersDef.Length; i++)
			{
				IReference<RuntimeMemberObj> reference = membersDef[i];
				using (reference.PinValue())
				{
					IReference<RuntimeGroupRootObj> groupRoot = ((RuntimeDataTablixMemberObj)reference.Value()).GroupRoot;
					using (groupRoot.PinValue())
					{
						this.m_domainScopes.Add(groupRoot.Value().HierarchyDef.OriginalScopeID, groupRoot);
					}
				}
			}
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x001FA528 File Offset: 0x001F8728
		internal void RemoveDomainScopes(IReference<RuntimeMemberObj>[] membersDef, int startIndex)
		{
			for (int i = startIndex; i < membersDef.Length; i++)
			{
				IReference<RuntimeMemberObj> reference = membersDef[i];
				using (reference.PinValue())
				{
					IReference<RuntimeGroupRootObj> groupRoot = ((RuntimeDataTablixMemberObj)reference.Value()).GroupRoot;
					using (groupRoot.PinValue())
					{
						this.m_domainScopes.Remove(groupRoot.Value().HierarchyDef.OriginalScopeID);
					}
				}
			}
		}

		// Token: 0x04003D5E RID: 15710
		private Dictionary<int, IReference<RuntimeGroupRootObj>> m_domainScopes = new Dictionary<int, IReference<RuntimeGroupRootObj>>();

		// Token: 0x04003D5F RID: 15711
		private DomainScopeContext.DomainScopeInfo m_currentDomainScopeInfo;

		// Token: 0x02000D1C RID: 3356
		internal class DomainScopeInfo
		{
			// Token: 0x06008F00 RID: 36608 RVA: 0x002464EA File Offset: 0x002446EA
			internal void InitializeKeys(int count)
			{
				this.m_keys = new object[count];
				this.m_keyCount = 0;
				this.m_currentKeyIndex = -1;
			}

			// Token: 0x06008F01 RID: 36609 RVA: 0x00246508 File Offset: 0x00244708
			internal void AddKey(object key)
			{
				object[] keys = this.m_keys;
				int keyCount = this.m_keyCount;
				this.m_keyCount = keyCount + 1;
				keys[keyCount] = key;
			}

			// Token: 0x06008F02 RID: 36610 RVA: 0x0024652E File Offset: 0x0024472E
			internal void RemoveKey()
			{
				this.m_keyCount--;
			}

			// Token: 0x17002BDB RID: 11227
			// (get) Token: 0x06008F03 RID: 36611 RVA: 0x0024653E File Offset: 0x0024473E
			internal object CurrentKey
			{
				get
				{
					return this.m_keys[this.m_currentKeyIndex];
				}
			}

			// Token: 0x17002BDC RID: 11228
			// (get) Token: 0x06008F04 RID: 36612 RVA: 0x0024654D File Offset: 0x0024474D
			// (set) Token: 0x06008F05 RID: 36613 RVA: 0x00246555 File Offset: 0x00244755
			internal DataFieldRow CurrentRow
			{
				get
				{
					return this.m_currentRow;
				}
				set
				{
					this.m_currentRow = value;
				}
			}

			// Token: 0x06008F06 RID: 36614 RVA: 0x0024655E File Offset: 0x0024475E
			internal void MoveNext()
			{
				this.m_currentKeyIndex++;
			}

			// Token: 0x06008F07 RID: 36615 RVA: 0x0024656E File Offset: 0x0024476E
			internal void MovePrevious()
			{
				this.m_currentKeyIndex--;
			}

			// Token: 0x0400505B RID: 20571
			private int m_currentKeyIndex = -1;

			// Token: 0x0400505C RID: 20572
			private int m_keyCount;

			// Token: 0x0400505D RID: 20573
			private object[] m_keys;

			// Token: 0x0400505E RID: 20574
			internal DataFieldRow m_currentRow;
		}
	}
}
