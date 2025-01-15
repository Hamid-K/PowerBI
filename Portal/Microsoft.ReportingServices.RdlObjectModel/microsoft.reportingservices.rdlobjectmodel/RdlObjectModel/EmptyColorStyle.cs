using System;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F4 RID: 500
	[XmlElementClass("Style")]
	public class EmptyColorStyle : Style
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x000273FF File Offset: 0x000255FF
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x0002740C File Offset: 0x0002560C
		public new EmptyBorder Border
		{
			get
			{
				return (EmptyBorder)base.Border;
			}
			set
			{
				if (value != null && value.Color == ReportColor.Empty)
				{
					value.Color = Constants.DefaultEmptyColor;
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00027440 File Offset: 0x00025640
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x00027448 File Offset: 0x00025648
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

		// Token: 0x060010CA RID: 4298 RVA: 0x00027451 File Offset: 0x00025651
		public EmptyColorStyle()
		{
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00027459 File Offset: 0x00025659
		public override void Initialize()
		{
			base.Initialize();
			this.Color = Constants.DefaultEmptyColor;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00027471 File Offset: 0x00025671
		internal EmptyColorStyle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
