using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003D RID: 61
	internal class EmptyBorderColor2005 : BorderColor2005
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000040D7 File Offset: 0x000022D7
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000040DF File Offset: 0x000022DF
		public new ReportExpression<ReportColor> Default
		{
			get
			{
				return base.Default;
			}
			set
			{
				base.Default = value;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000040E8 File Offset: 0x000022E8
		public EmptyBorderColor2005()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000040F0 File Offset: 0x000022F0
		public EmptyBorderColor2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000040F9 File Offset: 0x000022F9
		public override void Initialize()
		{
			this.Default = Constants.DefaultEmptyColor;
		}
	}
}
