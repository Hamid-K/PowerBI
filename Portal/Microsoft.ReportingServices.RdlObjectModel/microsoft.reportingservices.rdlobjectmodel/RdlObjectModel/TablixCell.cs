using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000202 RID: 514
	public class TablixCell : DataRegionCell
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x00027DBA File Offset: 0x00025FBA
		public CellContents CellContents
		{
			get
			{
				return (CellContents)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00027DC9 File Offset: 0x00025FC9
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00027DDC File Offset: 0x00025FDC
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00027DEB File Offset: 0x00025FEB
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x00027DF9 File Offset: 0x00025FF9
		[DefaultValue(DataElementOutputTypes.ContentsOnly)]
		[ValidEnumValues("TablixCellDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(2);
			}
			set
			{
				base.PropertyStore.SetInteger(2, (int)value);
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00027E08 File Offset: 0x00026008
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x00027E16 File Offset: 0x00026016
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/accessibilityproperties")]
		[DefaultValue(StructureTypeOverwriteType.None)]
		[ValidEnumValues("TablixCellStructureTypeOverwriteType")]
		public StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return (StructureTypeOverwriteType)base.PropertyStore.GetInteger(3);
			}
			set
			{
				base.PropertyStore.SetInteger(3, (int)value);
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00027E25 File Offset: 0x00026025
		public TablixCell()
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00027E2D File Offset: 0x0002602D
		internal TablixCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00027E36 File Offset: 0x00026036
		public override void Initialize()
		{
			base.Initialize();
			this.DataElementOutput = DataElementOutputTypes.ContentsOnly;
		}

		// Token: 0x02000408 RID: 1032
		internal class Definition : DefinitionStore<TablixCell, TablixCell.Definition.Properties>
		{
			// Token: 0x060018E1 RID: 6369 RVA: 0x0003BF7B File Offset: 0x0003A17B
			private Definition()
			{
			}

			// Token: 0x02000517 RID: 1303
			internal enum Properties
			{
				// Token: 0x04001124 RID: 4388
				CellContents,
				// Token: 0x04001125 RID: 4389
				DataElementName,
				// Token: 0x04001126 RID: 4390
				DataElementOutput,
				// Token: 0x04001127 RID: 4391
				StructureTypeOverwrite
			}
		}
	}
}
