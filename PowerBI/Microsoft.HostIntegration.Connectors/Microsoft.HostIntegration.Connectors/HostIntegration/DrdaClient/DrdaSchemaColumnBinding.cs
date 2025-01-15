using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A31 RID: 2609
	internal class DrdaSchemaColumnBinding : DrdaColumnBinding
	{
		// Token: 0x060051A3 RID: 20899 RVA: 0x0014D5BB File Offset: 0x0014B7BB
		public DrdaSchemaColumnBinding(DrdaSchemaResultColumn column)
		{
			this._column = column;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0014D5CC File Offset: 0x0014B7CC
		public bool Initialize()
		{
			base.Name = this._column.Name;
			base.Type = DrdaMetaType.GetMetaTypeForType(this._column.Type);
			if (this._column.UsesMaxLength)
			{
				base.Size = this._column.MaxLength;
			}
			base.InferProperties(false);
			return true;
		}

		// Token: 0x04004044 RID: 16452
		private DrdaSchemaResultColumn _column;
	}
}
