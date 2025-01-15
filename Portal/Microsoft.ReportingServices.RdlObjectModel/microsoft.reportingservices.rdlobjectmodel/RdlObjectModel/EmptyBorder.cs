using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F6 RID: 502
	[XmlElementClass("Border")]
	public class EmptyBorder : Border, IShouldSerialize
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00027553 File Offset: 0x00025753
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0002755B File Offset: 0x0002575B
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public new ReportExpression<ReportColor> Color
		{
			get
			{
				return base.Color;
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00027564 File Offset: 0x00025764
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x0002756C File Offset: 0x0002576C
		[ReportExpressionDefaultValue(typeof(BorderStyles), BorderStyles.Solid)]
		public new ReportExpression<BorderStyles> Style
		{
			get
			{
				return base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00027575 File Offset: 0x00025775
		public EmptyBorder()
		{
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0002757D File Offset: 0x0002577D
		public override void Initialize()
		{
			base.Initialize();
			this.Color = Constants.DefaultEmptyColor;
			this.Style = BorderStyles.Solid;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000275A1 File Offset: 0x000257A1
		internal EmptyBorder(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000275AA File Offset: 0x000257AA
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000275B0 File Offset: 0x000257B0
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
		{
			if (property == "Style" && !this.Style.IsExpression && this.Style.Value == BorderStyles.Solid)
			{
				return SerializationMethod.Never;
			}
			if (base.Parent is EmptyColorStyle && ((EmptyColorStyle)base.Parent).Border != this)
			{
				return SerializationMethod.Always;
			}
			return SerializationMethod.Auto;
		}
	}
}
