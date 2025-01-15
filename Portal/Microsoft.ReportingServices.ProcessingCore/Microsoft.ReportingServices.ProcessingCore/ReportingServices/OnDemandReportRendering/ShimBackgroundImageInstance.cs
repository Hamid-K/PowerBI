using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000330 RID: 816
	internal sealed class ShimBackgroundImageInstance : BackgroundImageInstance
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x000764F9 File Offset: 0x000746F9
		internal ShimBackgroundImageInstance(BackgroundImage backgroundImageDef, BackgroundImage renderImage, string backgroundRepeat)
			: base(null)
		{
			this.m_backgroundImageDef = backgroundImageDef;
			this.m_renderImage = renderImage;
			this.m_backgroundRepeat = StyleTranslator.TranslateBackgroundRepeat(backgroundRepeat, null, this.m_backgroundImageDef.StyleDef.IsDynamicImageStyle);
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0007652D File Offset: 0x0007472D
		public override byte[] ImageData
		{
			get
			{
				return this.m_renderImage.ImageData;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0007653A File Offset: 0x0007473A
		public override string StreamName
		{
			get
			{
				return this.m_renderImage.StreamName;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00076547 File Offset: 0x00074747
		public override string MIMEType
		{
			get
			{
				return this.m_renderImage.MIMEType;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00076554 File Offset: 0x00074754
		public override BackgroundRepeatTypes BackgroundRepeat
		{
			get
			{
				return this.m_backgroundRepeat;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0007655C File Offset: 0x0007475C
		public override Positions Position
		{
			get
			{
				return this.m_backgroundImageDef.Position.Value;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0007656E File Offset: 0x0007476E
		public override ReportColor TransparentColor
		{
			get
			{
				return this.m_backgroundImageDef.TransparentColor.Value;
			}
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00076580 File Offset: 0x00074780
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000FA1 RID: 4001
		private readonly BackgroundImage m_renderImage;

		// Token: 0x04000FA2 RID: 4002
		private readonly BackgroundRepeatTypes m_backgroundRepeat;

		// Token: 0x04000FA3 RID: 4003
		private readonly BackgroundImage m_backgroundImageDef;
	}
}
