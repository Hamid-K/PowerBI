using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	public class DataModelErrorEventArgs : EventArgs
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000C004C File Offset: 0x000BE24C
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x000C0054 File Offset: 0x000BE254
		public string PropertyName { get; internal set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000C005D File Offset: 0x000BE25D
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000C0065 File Offset: 0x000BE265
		public string ErrorMessage { get; internal set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000C006E File Offset: 0x000BE26E
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x000C0076 File Offset: 0x000BE276
		public MetadataItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x0400135C RID: 4956
		[NonSerialized]
		private MetadataItem _item;
	}
}
