using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E8 RID: 488
	public class GridLayoutDefinition : ReportObject
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000264C0 File Offset: 0x000246C0
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x000264CE File Offset: 0x000246CE
		public int NumberOfColumns
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000264DD File Offset: 0x000246DD
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x000264EB File Offset: 0x000246EB
		public int NumberOfRows
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

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000264FA File Offset: 0x000246FA
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x0002650D File Offset: 0x0002470D
		[XmlElement(typeof(RdlCollection<CellDefinition>))]
		public IList<CellDefinition> CellDefinitions
		{
			get
			{
				return (IList<CellDefinition>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002651C File Offset: 0x0002471C
		public GridLayoutDefinition()
		{
			this.CellDefinitions = new RdlCollection<CellDefinition>();
			this.NumberOfColumns = 4;
			this.NumberOfRows = 2;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002653D File Offset: 0x0002473D
		internal GridLayoutDefinition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0400056C RID: 1388
		public const int MaxNumberOfRows = 10000;

		// Token: 0x0400056D RID: 1389
		public const int MaxNumberOfColumns = 8;

		// Token: 0x0400056E RID: 1390
		public const int MaxNumberOfConsecutiveEmptyRows = 20;

		// Token: 0x020003F3 RID: 1011
		internal class Definition : DefinitionStore<GridLayoutDefinition, GridLayoutDefinition.Definition.Properties>
		{
			// Token: 0x060018B5 RID: 6325 RVA: 0x0003BB67 File Offset: 0x00039D67
			private Definition()
			{
			}

			// Token: 0x02000505 RID: 1285
			internal enum Properties
			{
				// Token: 0x040010C2 RID: 4290
				NumberOfColumns,
				// Token: 0x040010C3 RID: 4291
				NumberOfRows,
				// Token: 0x040010C4 RID: 4292
				CellDefinitions
			}
		}
	}
}
