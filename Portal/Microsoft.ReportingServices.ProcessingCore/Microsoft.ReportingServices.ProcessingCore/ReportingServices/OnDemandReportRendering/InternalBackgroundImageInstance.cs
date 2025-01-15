using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032F RID: 815
	internal sealed class InternalBackgroundImageInstance : BackgroundImageInstance
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x000763D0 File Offset: 0x000745D0
		internal InternalBackgroundImageInstance(BackgroundImage backgroundImageDef)
			: base(backgroundImageDef.StyleDef.ReportScope)
		{
			this.m_backgroundImageDef = backgroundImageDef;
			this.m_imageDataHandler = ImageDataHandlerFactory.Create(this.m_backgroundImageDef.StyleDef.ReportElement, backgroundImageDef);
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0007641C File Offset: 0x0007461C
		public override byte[] ImageData
		{
			get
			{
				return this.m_imageDataHandler.ImageData;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00076429 File Offset: 0x00074629
		public override string StreamName
		{
			get
			{
				return this.m_imageDataHandler.StreamName;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00076436 File Offset: 0x00074636
		public override string MIMEType
		{
			get
			{
				return this.m_imageDataHandler.MIMEType;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00076443 File Offset: 0x00074643
		public override BackgroundRepeatTypes BackgroundRepeat
		{
			get
			{
				if (!this.m_backgroundRepeatEvaluated)
				{
					this.m_backgroundRepeatEvaluated = true;
					this.m_backgroundRepeat = (BackgroundRepeatTypes)this.m_backgroundImageDef.StyleDef.EvaluateInstanceStyleEnum(StyleAttributeNames.BackgroundImageRepeat);
				}
				return this.m_backgroundRepeat;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00076472 File Offset: 0x00074672
		public override Positions Position
		{
			get
			{
				if (!this.m_positionEvaluated)
				{
					this.m_positionEvaluated = true;
					this.m_position = (Positions)this.m_backgroundImageDef.StyleDef.EvaluateInstanceStyleEnum(StyleAttributeNames.Position);
				}
				return this.m_position;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000764A1 File Offset: 0x000746A1
		public override ReportColor TransparentColor
		{
			get
			{
				if (!this.m_transparentColorEvaluated)
				{
					this.m_transparentColorEvaluated = true;
					this.m_transparentColor = this.m_backgroundImageDef.StyleDef.EvaluateInstanceReportColor(StyleAttributeNames.TransparentColor);
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000764D0 File Offset: 0x000746D0
		protected override void ResetInstanceCache()
		{
			this.m_backgroundRepeatEvaluated = false;
			this.m_positionEvaluated = false;
			this.m_transparentColorEvaluated = false;
			this.m_transparentColor = null;
			this.m_imageDataHandler.ClearCache();
		}

		// Token: 0x04000F99 RID: 3993
		private bool m_backgroundRepeatEvaluated;

		// Token: 0x04000F9A RID: 3994
		private BackgroundRepeatTypes m_backgroundRepeat = Style.DefaultEnumBackgroundRepeatType;

		// Token: 0x04000F9B RID: 3995
		private bool m_positionEvaluated;

		// Token: 0x04000F9C RID: 3996
		private Positions m_position;

		// Token: 0x04000F9D RID: 3997
		private bool m_transparentColorEvaluated;

		// Token: 0x04000F9E RID: 3998
		private ReportColor m_transparentColor;

		// Token: 0x04000F9F RID: 3999
		private readonly ImageDataHandler m_imageDataHandler;

		// Token: 0x04000FA0 RID: 4000
		private readonly BackgroundImage m_backgroundImageDef;
	}
}
