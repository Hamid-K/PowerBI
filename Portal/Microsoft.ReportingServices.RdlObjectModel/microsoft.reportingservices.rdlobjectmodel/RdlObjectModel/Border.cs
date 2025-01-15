using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F5 RID: 501
	public class Border : ReportObject, IShouldSerialize
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0002747A File Offset: 0x0002567A
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00027488 File Offset: 0x00025688
		[ReportExpressionDefaultValueConstant(typeof(ReportColor), "DefaultBorderColor")]
		public ReportExpression<ReportColor> Color
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0002749C File Offset: 0x0002569C
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x000274AA File Offset: 0x000256AA
		[ReportExpressionDefaultValue(typeof(BorderStyles), BorderStyles.Default)]
		public ReportExpression<BorderStyles> Style
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x000274BE File Offset: 0x000256BE
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x000274CC File Offset: 0x000256CC
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultBorderWidth")]
		public ReportExpression<ReportSize> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000274E0 File Offset: 0x000256E0
		public Border()
		{
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000274E8 File Offset: 0x000256E8
		internal Border(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000274F1 File Offset: 0x000256F1
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000274F4 File Offset: 0x000256F4
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
		{
			if (property == "Style" && !this.Style.IsExpression && this.Style.Value == BorderStyles.Default)
			{
				return SerializationMethod.Never;
			}
			if (base.Parent is Style && ((Style)base.Parent).Border != this)
			{
				return SerializationMethod.Always;
			}
			return SerializationMethod.Auto;
		}

		// Token: 0x020003FB RID: 1019
		internal class Definition : DefinitionStore<Border, Border.Definition.Properties>
		{
			// Token: 0x060018C4 RID: 6340 RVA: 0x0003BC34 File Offset: 0x00039E34
			private Definition()
			{
			}

			// Token: 0x0200050C RID: 1292
			internal enum Properties
			{
				// Token: 0x040010DF RID: 4319
				Color,
				// Token: 0x040010E0 RID: 4320
				Style,
				// Token: 0x040010E1 RID: 4321
				Width
			}
		}
	}
}
