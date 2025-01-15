using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200000E RID: 14
	public sealed class DataSetRow
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000223E File Offset: 0x0000043E
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002246 File Offset: 0x00000446
		public Guid Id { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000224F File Offset: 0x0000044F
		public IDictionary<string, object> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new Dictionary<string, object>();
				}
				return this._properties;
			}
		}

		// Token: 0x04000078 RID: 120
		private IDictionary<string, object> _properties;
	}
}
