using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001F RID: 31
	internal sealed class AddToFavoritesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005384 File Offset: 0x00003584
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000538C File Offset: 0x0000358C
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005395 File Offset: 0x00003595
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000539D File Offset: 0x0000359D
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

		// Token: 0x060000B9 RID: 185 RVA: 0x000053A6 File Offset: 0x000035A6
		internal override void Validate()
		{
			Guid itemId = this.ItemId;
		}

		// Token: 0x040000B2 RID: 178
		private Guid _itemId = Guid.Empty;

		// Token: 0x040000B3 RID: 179
		private bool _status;
	}
}
