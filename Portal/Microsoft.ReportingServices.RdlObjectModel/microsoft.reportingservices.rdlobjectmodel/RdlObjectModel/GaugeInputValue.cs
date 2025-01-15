using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014B RID: 331
	public class GaugeInputValue : ReportObject
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001DE58 File Offset: 0x0001C058
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0001DE66 File Offset: 0x0001C066
		public ReportExpression Value
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

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001DE7A File Offset: 0x0001C07A
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001DE88 File Offset: 0x0001C088
		[ReportExpressionDefaultValue(typeof(FormulaTypes), FormulaTypes.None)]
		public ReportExpression<FormulaTypes> Formula
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FormulaTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001DE9C File Offset: 0x0001C09C
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0001DEAA File Offset: 0x0001C0AA
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> MinPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001DEBE File Offset: 0x0001C0BE
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0001DECC File Offset: 0x0001C0CC
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> MaxPercent
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0001DEEE File Offset: 0x0001C0EE
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Multiplier
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001DF02 File Offset: 0x0001C102
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0001DF10 File Offset: 0x0001C110
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> AddConstant
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001DF24 File Offset: 0x0001C124
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0001DF37 File Offset: 0x0001C137
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0001DF46 File Offset: 0x0001C146
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0001DF54 File Offset: 0x0001C154
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("GaugeInputValueDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(7);
			}
			set
			{
				((EnumProperty)DefinitionStore<GaugeInputValue, GaugeInputValue.Definition.Properties>.GetProperty(7)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(7, (int)value);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001DF75 File Offset: 0x0001C175
		public GaugeInputValue()
		{
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001DF7D File Offset: 0x0001C17D
		internal GaugeInputValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001DF86 File Offset: 0x0001C186
		public override void Initialize()
		{
			base.Initialize();
			this.DataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x0200037C RID: 892
		internal class Definition : DefinitionStore<GaugeInputValue, GaugeInputValue.Definition.Properties>
		{
			// Token: 0x0600181F RID: 6175 RVA: 0x0003B423 File Offset: 0x00039623
			private Definition()
			{
			}

			// Token: 0x02000495 RID: 1173
			internal enum Properties
			{
				// Token: 0x04000BE9 RID: 3049
				Value,
				// Token: 0x04000BEA RID: 3050
				Formula,
				// Token: 0x04000BEB RID: 3051
				MinPercent,
				// Token: 0x04000BEC RID: 3052
				MaxPercent,
				// Token: 0x04000BED RID: 3053
				Multiplier,
				// Token: 0x04000BEE RID: 3054
				AddConstant,
				// Token: 0x04000BEF RID: 3055
				DataElementName,
				// Token: 0x04000BF0 RID: 3056
				DataElementOutput,
				// Token: 0x04000BF1 RID: 3057
				PropertyCount
			}
		}
	}
}
