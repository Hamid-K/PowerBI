using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017F RID: 383
	public abstract class MapAppearanceRule : ReportObject
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x00020DCA File Offset: 0x0001EFCA
		public MapAppearanceRule()
		{
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00020DD2 File Offset: 0x0001EFD2
		internal MapAppearanceRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x00020DDB File Offset: 0x0001EFDB
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x00020DE9 File Offset: 0x0001EFE9
		[ReportExpressionDefaultValue("")]
		public ReportExpression DataValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00020DFD File Offset: 0x0001EFFD
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00020E0B File Offset: 0x0001F00B
		[ReportExpressionDefaultValue(typeof(MapRuleDistributionTypes), MapRuleDistributionTypes.Optimal)]
		public ReportExpression<MapRuleDistributionTypes> DistributionType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapRuleDistributionTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00020E1F File Offset: 0x0001F01F
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x00020E2D File Offset: 0x0001F02D
		[ReportExpressionDefaultValue(typeof(int), "5")]
		public ReportExpression<int> BucketCount
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00020E41 File Offset: 0x0001F041
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00020E4F File Offset: 0x0001F04F
		public ReportExpression StartValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00020E63 File Offset: 0x0001F063
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00020E71 File Offset: 0x0001F071
		public ReportExpression EndValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00020E85 File Offset: 0x0001F085
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00020E98 File Offset: 0x0001F098
		[XmlElement(typeof(RdlCollection<MapBucket>))]
		public IList<MapBucket> MapBuckets
		{
			get
			{
				return (IList<MapBucket>)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00020EA7 File Offset: 0x0001F0A7
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00020EB5 File Offset: 0x0001F0B5
		public string LegendName
		{
			get
			{
				return base.PropertyStore.GetObject<string>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x00020ED2 File Offset: 0x0001F0D2
		public ReportExpression LegendText
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00020EE6 File Offset: 0x0001F0E6
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00020EF9 File Offset: 0x0001F0F9
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00020F08 File Offset: 0x0001F108
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00020F17 File Offset: 0x0001F117
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("MapDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(9);
			}
			set
			{
				((EnumProperty)DefinitionStore<MapAppearanceRule, MapAppearanceRule.Definition.Properties>.GetProperty(9)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(9, (int)value);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00020F3A File Offset: 0x0001F13A
		public override void Initialize()
		{
			base.Initialize();
			this.DistributionType = MapRuleDistributionTypes.Optimal;
			this.BucketCount = 5;
			this.MapBuckets = new RdlCollection<MapBucket>();
			this.DataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x020003AD RID: 941
		internal class Definition : DefinitionStore<MapAppearanceRule, MapAppearanceRule.Definition.Properties>
		{
			// Token: 0x06001851 RID: 6225 RVA: 0x0003B609 File Offset: 0x00039809
			private Definition()
			{
			}

			// Token: 0x020004C5 RID: 1221
			internal enum Properties
			{
				// Token: 0x04000E79 RID: 3705
				DataValue,
				// Token: 0x04000E7A RID: 3706
				DistributionType,
				// Token: 0x04000E7B RID: 3707
				BucketCount,
				// Token: 0x04000E7C RID: 3708
				StartValue,
				// Token: 0x04000E7D RID: 3709
				EndValue,
				// Token: 0x04000E7E RID: 3710
				MapBuckets,
				// Token: 0x04000E7F RID: 3711
				LegendName,
				// Token: 0x04000E80 RID: 3712
				LegendText,
				// Token: 0x04000E81 RID: 3713
				DataElementName,
				// Token: 0x04000E82 RID: 3714
				DataElementOutput
			}
		}
	}
}
