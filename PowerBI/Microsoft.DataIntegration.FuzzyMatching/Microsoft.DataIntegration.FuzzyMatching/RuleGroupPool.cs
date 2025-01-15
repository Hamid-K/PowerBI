using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	internal sealed class RuleGroupPool
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public RuleGroupPool(int numDims)
		{
			this.m_numDimensions = numDims;
			this.ruleGroups = new RuleGroup[0];
			this.GrowRuleGroupPool(2);
			this.Size = 0;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001D2F5 File Offset: 0x0001B4F5
		public void BeginUpdate(int size)
		{
			if (this.ruleGroups.Length < size)
			{
				this.GrowRuleGroupPool(size);
			}
			this.Size = size;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001D310 File Offset: 0x0001B510
		public RuleGroup GetNewRuleGroup()
		{
			if (this.Size == this.ruleGroups.Length)
			{
				this.GrowRuleGroupPool();
			}
			RuleGroup[] array = this.ruleGroups;
			int size = this.Size;
			this.Size = size + 1;
			object obj = array[size];
			obj.BeginUpdate();
			return obj;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001D351 File Offset: 0x0001B551
		public void EndUpdate()
		{
		}

		// Token: 0x17000152 RID: 338
		public RuleGroup this[int id]
		{
			get
			{
				return this.ruleGroups[id];
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001D366 File Offset: 0x0001B566
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001D35D File Offset: 0x0001B55D
		public int Size { get; private set; }

		// Token: 0x06000699 RID: 1689 RVA: 0x0001D370 File Offset: 0x0001B570
		private void GrowRuleGroupPool()
		{
			int num = this.ruleGroups.Length;
			int num2 = (int)((double)num * 1.5);
			Array.Resize<RuleGroup>(ref this.ruleGroups, num2);
			for (int i = num; i < num2; i++)
			{
				this.ruleGroups[i] = new RuleGroup(i, this.m_numDimensions);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		private void GrowRuleGroupPool(int desiredCapacity)
		{
			int num = this.ruleGroups.Length;
			int num2 = (int)((double)desiredCapacity * 1.5);
			Array.Resize<RuleGroup>(ref this.ruleGroups, num2);
			for (int i = num; i < num2; i++)
			{
				this.ruleGroups[i] = new RuleGroup(i, this.m_numDimensions);
			}
		}

		// Token: 0x0400025B RID: 603
		private int m_numDimensions;

		// Token: 0x0400025C RID: 604
		private RuleGroup[] ruleGroups;
	}
}
