using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E9 RID: 489
	public class CellDefinition : ReportObject
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00026546 File Offset: 0x00024746
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00026554 File Offset: 0x00024754
		public int ColumnIndex
		{
			get
			{
				return base.PropertyStore.GetInteger(0);
			}
			set
			{
				base.PropertyStore.SetInteger(0, value);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00026563 File Offset: 0x00024763
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x00026571 File Offset: 0x00024771
		public int RowIndex
		{
			get
			{
				return base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, value);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x00026580 File Offset: 0x00024780
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x00026593 File Offset: 0x00024793
		public string ParameterName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000265A2 File Offset: 0x000247A2
		public CellDefinition()
		{
			this.ColumnIndex = 0;
			this.RowIndex = 0;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000265B8 File Offset: 0x000247B8
		internal CellDefinition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003F4 RID: 1012
		internal class Definition : DefinitionStore<GridLayoutDefinition, CellDefinition.Definition.Properties>
		{
			// Token: 0x060018B6 RID: 6326 RVA: 0x0003BB6F File Offset: 0x00039D6F
			private Definition()
			{
			}

			// Token: 0x02000506 RID: 1286
			internal enum Properties
			{
				// Token: 0x040010C6 RID: 4294
				ColumnIndex,
				// Token: 0x040010C7 RID: 4295
				RowIndex,
				// Token: 0x040010C8 RID: 4296
				ParameterName
			}
		}
	}
}
