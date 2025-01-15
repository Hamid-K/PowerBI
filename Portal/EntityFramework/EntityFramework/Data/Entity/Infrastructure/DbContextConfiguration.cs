using System;
using System.ComponentModel;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000224 RID: 548
	public class DbContextConfiguration
	{
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00051ED7 File Offset: 0x000500D7
		internal DbContextConfiguration(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00051EE6 File Offset: 0x000500E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00051EEE File Offset: 0x000500EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00051EF7 File Offset: 0x000500F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00051EFF File Offset: 0x000500FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00051F07 File Offset: 0x00050107
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x00051F14 File Offset: 0x00050114
		public bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				return this._internalContext.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				this._internalContext.EnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00051F22 File Offset: 0x00050122
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x00051F2F File Offset: 0x0005012F
		public bool LazyLoadingEnabled
		{
			get
			{
				return this._internalContext.LazyLoadingEnabled;
			}
			set
			{
				this._internalContext.LazyLoadingEnabled = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00051F3D File Offset: 0x0005013D
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x00051F4A File Offset: 0x0005014A
		public bool ProxyCreationEnabled
		{
			get
			{
				return this._internalContext.ProxyCreationEnabled;
			}
			set
			{
				this._internalContext.ProxyCreationEnabled = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00051F58 File Offset: 0x00050158
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x00051F65 File Offset: 0x00050165
		public bool UseDatabaseNullSemantics
		{
			get
			{
				return this._internalContext.UseDatabaseNullSemantics;
			}
			set
			{
				this._internalContext.UseDatabaseNullSemantics = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00051F73 File Offset: 0x00050173
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00051F80 File Offset: 0x00050180
		public bool DisableFilterOverProjectionSimplificationForCustomFunctions
		{
			get
			{
				return this._internalContext.DisableFilterOverProjectionSimplificationForCustomFunctions;
			}
			set
			{
				this._internalContext.DisableFilterOverProjectionSimplificationForCustomFunctions = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00051F8E File Offset: 0x0005018E
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x00051F9B File Offset: 0x0005019B
		public bool AutoDetectChangesEnabled
		{
			get
			{
				return this._internalContext.AutoDetectChangesEnabled;
			}
			set
			{
				this._internalContext.AutoDetectChangesEnabled = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00051FA9 File Offset: 0x000501A9
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x00051FB6 File Offset: 0x000501B6
		public bool ValidateOnSaveEnabled
		{
			get
			{
				return this._internalContext.ValidateOnSaveEnabled;
			}
			set
			{
				this._internalContext.ValidateOnSaveEnabled = value;
			}
		}

		// Token: 0x04000B02 RID: 2818
		private readonly InternalContext _internalContext;
	}
}
