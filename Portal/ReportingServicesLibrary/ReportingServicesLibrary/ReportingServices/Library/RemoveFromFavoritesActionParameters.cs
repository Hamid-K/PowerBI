using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000021 RID: 33
	internal sealed class RemoveFromFavoritesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000548B File Offset: 0x0000368B
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005493 File Offset: 0x00003693
		public Guid ItemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000549C File Offset: 0x0000369C
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000054A4 File Offset: 0x000036A4
		public bool Status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000054AD File Offset: 0x000036AD
		internal override void Validate()
		{
			Guid itemId = this.ItemId;
		}

		// Token: 0x040000B5 RID: 181
		private Guid _itemId = Guid.Empty;

		// Token: 0x040000B6 RID: 182
		private bool _status;
	}
}
