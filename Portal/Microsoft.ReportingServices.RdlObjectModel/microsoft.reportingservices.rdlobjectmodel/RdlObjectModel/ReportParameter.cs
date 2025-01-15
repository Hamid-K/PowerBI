using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E6 RID: 486
	public class ReportParameter : ReportObject, INamedObject
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00026336 File Offset: 0x00024536
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x00026349 File Offset: 0x00024549
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00026358 File Offset: 0x00024558
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00026366 File Offset: 0x00024566
		public DataTypes DataType
		{
			get
			{
				return (DataTypes)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00026375 File Offset: 0x00024575
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x00026383 File Offset: 0x00024583
		[DefaultValue(false)]
		public bool Nullable
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00026392 File Offset: 0x00024592
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x000263A5 File Offset: 0x000245A5
		public DefaultValue DefaultValue
		{
			get
			{
				return (DefaultValue)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000263B4 File Offset: 0x000245B4
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x000263C2 File Offset: 0x000245C2
		[DefaultValue(false)]
		public bool AllowBlank
		{
			get
			{
				return base.PropertyStore.GetBoolean(4);
			}
			set
			{
				base.PropertyStore.SetBoolean(4, value);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000263D1 File Offset: 0x000245D1
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x000263DF File Offset: 0x000245DF
		[ReportExpressionDefaultValue]
		public ReportExpression Prompt
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x000263F3 File Offset: 0x000245F3
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00026401 File Offset: 0x00024601
		[DefaultValue(false)]
		public bool Hidden
		{
			get
			{
				return base.PropertyStore.GetBoolean(7);
			}
			set
			{
				base.PropertyStore.SetBoolean(7, value);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00026410 File Offset: 0x00024610
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x00026423 File Offset: 0x00024623
		public ValidValues ValidValues
		{
			get
			{
				return (ValidValues)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00026432 File Offset: 0x00024632
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00026441 File Offset: 0x00024641
		[DefaultValue(false)]
		public bool MultiValue
		{
			get
			{
				return base.PropertyStore.GetBoolean(9);
			}
			set
			{
				base.PropertyStore.SetBoolean(9, value);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00026451 File Offset: 0x00024651
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00026460 File Offset: 0x00024660
		[DefaultValue(UsedInQueryTypes.Auto)]
		public UsedInQueryTypes UsedInQuery
		{
			get
			{
				return (UsedInQueryTypes)base.PropertyStore.GetInteger(10);
			}
			set
			{
				base.PropertyStore.SetInteger(10, (int)value);
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00026470 File Offset: 0x00024670
		public ReportParameter()
		{
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00026478 File Offset: 0x00024678
		internal ReportParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003F1 RID: 1009
		internal class Definition : DefinitionStore<ReportParameter, ReportParameter.Definition.Properties>
		{
			// Token: 0x060018B3 RID: 6323 RVA: 0x0003BB57 File Offset: 0x00039D57
			private Definition()
			{
			}

			// Token: 0x02000503 RID: 1283
			internal enum Properties
			{
				// Token: 0x040010B4 RID: 4276
				Name,
				// Token: 0x040010B5 RID: 4277
				DataType,
				// Token: 0x040010B6 RID: 4278
				Nullable,
				// Token: 0x040010B7 RID: 4279
				DefaultValue,
				// Token: 0x040010B8 RID: 4280
				AllowBlank,
				// Token: 0x040010B9 RID: 4281
				Prompt,
				// Token: 0x040010BA RID: 4282
				PromptLocID,
				// Token: 0x040010BB RID: 4283
				Hidden,
				// Token: 0x040010BC RID: 4284
				ValidValues,
				// Token: 0x040010BD RID: 4285
				MultiValue,
				// Token: 0x040010BE RID: 4286
				UsedInQuery
			}
		}
	}
}
