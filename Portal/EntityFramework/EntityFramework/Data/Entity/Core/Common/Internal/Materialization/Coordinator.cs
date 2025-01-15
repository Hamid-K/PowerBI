using System;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000636 RID: 1590
	internal abstract class Coordinator
	{
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x0010E64A File Offset: 0x0010C84A
		// (set) Token: 0x06004C84 RID: 19588 RVA: 0x0010E652 File Offset: 0x0010C852
		public Coordinator Child { get; protected set; }

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x0010E65B File Offset: 0x0010C85B
		// (set) Token: 0x06004C86 RID: 19590 RVA: 0x0010E663 File Offset: 0x0010C863
		public bool IsEntered { get; protected set; }

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x0010E66C File Offset: 0x0010C86C
		internal bool IsRoot
		{
			get
			{
				return this.Parent == null;
			}
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x0010E677 File Offset: 0x0010C877
		protected Coordinator(CoordinatorFactory coordinatorFactory, Coordinator parent, Coordinator next)
		{
			this.CoordinatorFactory = coordinatorFactory;
			this.Parent = parent;
			this.Next = next;
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x0010E694 File Offset: 0x0010C894
		internal void Initialize(Shaper shaper)
		{
			this.ResetCollection(shaper);
			shaper.State[this.CoordinatorFactory.StateSlot] = this;
			if (this.Child != null)
			{
				this.Child.Initialize(shaper);
			}
			if (this.Next != null)
			{
				this.Next.Initialize(shaper);
			}
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x0010E6E4 File Offset: 0x0010C8E4
		internal int MaxDistanceToLeaf()
		{
			int num = 0;
			for (Coordinator coordinator = this.Child; coordinator != null; coordinator = coordinator.Next)
			{
				num = Math.Max(num, coordinator.MaxDistanceToLeaf() + 1);
			}
			return num;
		}

		// Token: 0x06004C8B RID: 19595
		internal abstract void ResetCollection(Shaper shaper);

		// Token: 0x06004C8C RID: 19596 RVA: 0x0010E718 File Offset: 0x0010C918
		internal bool HasNextElement(Shaper shaper)
		{
			bool flag = false;
			if (!this.IsEntered || !this.CoordinatorFactory.CheckKeys(shaper))
			{
				this.CoordinatorFactory.SetKeys(shaper);
				this.IsEntered = true;
				flag = true;
			}
			return flag;
		}

		// Token: 0x06004C8D RID: 19597
		internal abstract void ReadNextElement(Shaper shaper);

		// Token: 0x04001B0F RID: 6927
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x04001B10 RID: 6928
		internal readonly Coordinator Parent;

		// Token: 0x04001B12 RID: 6930
		internal readonly Coordinator Next;
	}
}
