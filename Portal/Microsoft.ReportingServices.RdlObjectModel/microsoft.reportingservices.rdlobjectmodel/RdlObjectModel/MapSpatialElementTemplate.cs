using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017A RID: 378
	public abstract class MapSpatialElementTemplate : ReportObject
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00020A0D File Offset: 0x0001EC0D
		public MapSpatialElementTemplate()
		{
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00020A15 File Offset: 0x0001EC15
		internal MapSpatialElementTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00020A1E File Offset: 0x0001EC1E
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00020A31 File Offset: 0x0001EC31
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00020A40 File Offset: 0x0001EC40
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00020A53 File Offset: 0x0001EC53
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00020A62 File Offset: 0x0001EC62
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00020A70 File Offset: 0x0001EC70
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00020A84 File Offset: 0x0001EC84
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00020A92 File Offset: 0x0001EC92
		[ReportExpressionDefaultValue(typeof(double), 0)]
		public ReportExpression<double> OffsetX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00020AA6 File Offset: 0x0001ECA6
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		[ReportExpressionDefaultValue(typeof(double), 0)]
		public ReportExpression<double> OffsetY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00020AD6 File Offset: 0x0001ECD6
		[ReportExpressionDefaultValue("")]
		public ReportExpression Label
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00020AEA File Offset: 0x0001ECEA
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		[ReportExpressionDefaultValue("")]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00020B0C File Offset: 0x0001ED0C
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00020B1F File Offset: 0x0001ED1F
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00020B2E File Offset: 0x0001ED2E
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00020B3C File Offset: 0x0001ED3C
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("MapDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(8);
			}
			set
			{
				((EnumProperty)DefinitionStore<MapSpatialElementTemplate, MapSpatialElementTemplate.Definition.Properties>.GetProperty(8)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(8, (int)value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00020B5D File Offset: 0x0001ED5D
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00020B6C File Offset: 0x0001ED6C
		[ReportExpressionDefaultValue("")]
		public ReportExpression DataElementLabel
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00020B81 File Offset: 0x0001ED81
		public override void Initialize()
		{
			base.Initialize();
			this.DataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x020003A8 RID: 936
		internal class Definition : DefinitionStore<MapSpatialElementTemplate, MapSpatialElementTemplate.Definition.Properties>
		{
			// Token: 0x0600184C RID: 6220 RVA: 0x0003B5E1 File Offset: 0x000397E1
			private Definition()
			{
			}

			// Token: 0x020004C0 RID: 1216
			internal enum Properties
			{
				// Token: 0x04000E33 RID: 3635
				Style,
				// Token: 0x04000E34 RID: 3636
				ActionInfo,
				// Token: 0x04000E35 RID: 3637
				Hidden,
				// Token: 0x04000E36 RID: 3638
				OffsetX,
				// Token: 0x04000E37 RID: 3639
				OffsetY,
				// Token: 0x04000E38 RID: 3640
				Label,
				// Token: 0x04000E39 RID: 3641
				ToolTip,
				// Token: 0x04000E3A RID: 3642
				DataElementName,
				// Token: 0x04000E3B RID: 3643
				DataElementOutput,
				// Token: 0x04000E3C RID: 3644
				DataElementLabel
			}
		}
	}
}
