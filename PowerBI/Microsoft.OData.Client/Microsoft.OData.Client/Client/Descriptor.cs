using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C8 RID: 200
	public abstract class Descriptor
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x0001C046 File Offset: 0x0001A246
		internal Descriptor(EntityStates state)
		{
			this.state = state;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001C05C File Offset: 0x0001A25C
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001C064 File Offset: 0x0001A264
		public EntityStates State
		{
			get
			{
				return this.state;
			}
			internal set
			{
				this.state = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600067A RID: 1658
		internal abstract DescriptorKind DescriptorKind { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001C06D File Offset: 0x0001A26D
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001C075 File Offset: 0x0001A275
		internal uint ChangeOrder
		{
			get
			{
				return this.changeOrder;
			}
			set
			{
				this.changeOrder = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001C07E File Offset: 0x0001A27E
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001C086 File Offset: 0x0001A286
		internal bool ContentGeneratedForSave
		{
			get
			{
				return this.saveContentGenerated;
			}
			set
			{
				this.saveContentGenerated = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001C08F File Offset: 0x0001A28F
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001C097 File Offset: 0x0001A297
		internal EntityStates SaveResultWasProcessed
		{
			get
			{
				return this.saveResultProcessed;
			}
			set
			{
				this.saveResultProcessed = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001C0A0 File Offset: 0x0001A2A0
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		internal Exception SaveError
		{
			get
			{
				return this.saveError;
			}
			set
			{
				this.saveError = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		internal virtual bool IsModified
		{
			get
			{
				return EntityStates.Unchanged != this.state;
			}
		}

		// Token: 0x06000684 RID: 1668
		internal abstract void ClearChanges();

		// Token: 0x040002E0 RID: 736
		private uint changeOrder = uint.MaxValue;

		// Token: 0x040002E1 RID: 737
		private bool saveContentGenerated;

		// Token: 0x040002E2 RID: 738
		private EntityStates saveResultProcessed;

		// Token: 0x040002E3 RID: 739
		private Exception saveError;

		// Token: 0x040002E4 RID: 740
		private EntityStates state;
	}
}
