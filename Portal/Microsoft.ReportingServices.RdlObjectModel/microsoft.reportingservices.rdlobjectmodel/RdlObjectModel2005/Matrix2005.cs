using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000027 RID: 39
	internal class Matrix2005 : Tablix, IReportItem2005, IUpgradeable, IPageBreakLocation2005
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000031B4 File Offset: 0x000013B4
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000031C8 File Offset: 0x000013C8
		public Corner2005 Corner
		{
			get
			{
				return (Corner2005)base.PropertyStore.GetObject(44);
			}
			set
			{
				base.PropertyStore.SetObject(44, value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000031D8 File Offset: 0x000013D8
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000031EC File Offset: 0x000013EC
		[XmlElement(typeof(RdlCollection<ColumnGrouping2005>))]
		[XmlArrayItem("ColumnGrouping", typeof(ColumnGrouping2005))]
		public IList<ColumnGrouping2005> ColumnGroupings
		{
			get
			{
				return (IList<ColumnGrouping2005>)base.PropertyStore.GetObject(45);
			}
			set
			{
				base.PropertyStore.SetObject(45, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000031FC File Offset: 0x000013FC
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00003210 File Offset: 0x00001410
		[XmlElement(typeof(RdlCollection<RowGrouping2005>))]
		[XmlArrayItem("RowGrouping", typeof(RowGrouping2005))]
		public IList<RowGrouping2005> RowGroupings
		{
			get
			{
				return (IList<RowGrouping2005>)base.PropertyStore.GetObject(46);
			}
			set
			{
				base.PropertyStore.SetObject(46, value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003220 File Offset: 0x00001420
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00003234 File Offset: 0x00001434
		[XmlElement(typeof(RdlCollection<MatrixRow2005>))]
		[XmlArrayItem("MatrixRow", typeof(MatrixRow2005))]
		public IList<MatrixRow2005> MatrixRows
		{
			get
			{
				return (IList<MatrixRow2005>)base.PropertyStore.GetObject(47);
			}
			set
			{
				base.PropertyStore.SetObject(47, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00003251 File Offset: 0x00001451
		[XmlElement(typeof(RdlCollection<TablixColumn>))]
		[XmlArrayItem("MatrixColumn", typeof(TablixColumn))]
		public IList<TablixColumn> MatrixColumns
		{
			get
			{
				return base.TablixBody.TablixColumns;
			}
			set
			{
				base.TablixBody.TablixColumns = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000325F File Offset: 0x0000145F
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00003273 File Offset: 0x00001473
		[DefaultValue("")]
		public string CellDataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(48);
			}
			set
			{
				base.PropertyStore.SetObject(48, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00003283 File Offset: 0x00001483
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00003292 File Offset: 0x00001492
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues(typeof(Constants2005), "Matrix2005CellDataElementOutputTypes")]
		public DataElementOutputTypes CellDataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(49);
			}
			set
			{
				((EnumProperty)DefinitionStore<Matrix2005, Matrix2005.Definition.Properties>.GetProperty(49)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(49, (int)value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000032B5 File Offset: 0x000014B5
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000032C4 File Offset: 0x000014C4
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(37);
			}
			set
			{
				base.PropertyStore.SetBoolean(37, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000032D4 File Offset: 0x000014D4
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000032DC File Offset: 0x000014DC
		[ReportExpressionDefaultValue]
		public ReportExpression NoRows
		{
			get
			{
				return base.NoRowsMessage;
			}
			set
			{
				base.NoRowsMessage = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000032E5 File Offset: 0x000014E5
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000032F4 File Offset: 0x000014F4
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(38);
			}
			set
			{
				base.PropertyStore.SetBoolean(38, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003304 File Offset: 0x00001504
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00003318 File Offset: 0x00001518
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003328 File Offset: 0x00001528
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003330 File Offset: 0x00001530
		[ReportExpressionDefaultValue]
		public ReportExpression Label
		{
			get
			{
				return base.DocumentMapLabel;
			}
			set
			{
				base.DocumentMapLabel = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003339 File Offset: 0x00001539
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000334D File Offset: 0x0000154D
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000335D File Offset: 0x0000155D
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000336A File Offset: 0x0000156A
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003373 File Offset: 0x00001573
		public Matrix2005()
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000337B File Offset: 0x0000157B
		public Matrix2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003384 File Offset: 0x00001584
		public override void Initialize()
		{
			base.Initialize();
			this.ColumnGroupings = new RdlCollection<ColumnGrouping2005>();
			this.RowGroupings = new RdlCollection<RowGrouping2005>();
			this.MatrixRows = new RdlCollection<MatrixRow2005>();
			this.CellDataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000033B4 File Offset: 0x000015B4
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeMatrix(this);
		}

		// Token: 0x02000302 RID: 770
		internal new class Definition : DefinitionStore<Matrix2005, Matrix2005.Definition.Properties>
		{
			// Token: 0x060016FE RID: 5886 RVA: 0x0003648A File Offset: 0x0003468A
			private Definition()
			{
			}

			// Token: 0x02000436 RID: 1078
			public enum Properties
			{
				// Token: 0x0400086E RID: 2158
				Action = 36,
				// Token: 0x0400086F RID: 2159
				PageBreakAtStart,
				// Token: 0x04000870 RID: 2160
				PageBreakAtEnd,
				// Token: 0x04000871 RID: 2161
				Grouping,
				// Token: 0x04000872 RID: 2162
				Sorting,
				// Token: 0x04000873 RID: 2163
				ReportItems,
				// Token: 0x04000874 RID: 2164
				DataInstanceName,
				// Token: 0x04000875 RID: 2165
				DataInstanceElementOutput,
				// Token: 0x04000876 RID: 2166
				Corner,
				// Token: 0x04000877 RID: 2167
				ColumnGroupings,
				// Token: 0x04000878 RID: 2168
				RowGroupings,
				// Token: 0x04000879 RID: 2169
				MatrixRows,
				// Token: 0x0400087A RID: 2170
				CellDataElementName,
				// Token: 0x0400087B RID: 2171
				CellDataElementOutput,
				// Token: 0x0400087C RID: 2172
				PropertyCount
			}
		}
	}
}
