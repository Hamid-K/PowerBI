using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000340 RID: 832
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class StyleBaseInstance : BaseInstance
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x00077F39 File Offset: 0x00076139
		internal StyleBaseInstance(RenderingContext context, IReportScope reportScope)
			: base(reportScope)
		{
			this.m_renderingContext = context;
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06001F37 RID: 7991
		public abstract List<StyleAttributeNames> StyleAttributes { get; }

		// Token: 0x1700118F RID: 4495
		public abstract object this[StyleAttributeNames style] { get; }

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06001F39 RID: 7993
		// (set) Token: 0x06001F3A RID: 7994
		public abstract ReportColor BackgroundGradientEndColor { get; set; }

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06001F3B RID: 7995
		// (set) Token: 0x06001F3C RID: 7996
		public abstract ReportColor BackgroundColor { get; set; }

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06001F3D RID: 7997
		// (set) Token: 0x06001F3E RID: 7998
		public abstract ReportColor Color { get; set; }

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06001F3F RID: 7999
		// (set) Token: 0x06001F40 RID: 8000
		public abstract FontStyles FontStyle { get; set; }

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06001F41 RID: 8001
		// (set) Token: 0x06001F42 RID: 8002
		public abstract string FontFamily { get; set; }

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06001F43 RID: 8003
		// (set) Token: 0x06001F44 RID: 8004
		public abstract FontWeights FontWeight { get; set; }

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06001F45 RID: 8005
		// (set) Token: 0x06001F46 RID: 8006
		public abstract string Format { get; set; }

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06001F47 RID: 8007
		// (set) Token: 0x06001F48 RID: 8008
		public abstract TextDecorations TextDecoration { get; set; }

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06001F49 RID: 8009
		// (set) Token: 0x06001F4A RID: 8010
		public abstract TextAlignments TextAlign { get; set; }

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06001F4B RID: 8011
		// (set) Token: 0x06001F4C RID: 8012
		public abstract VerticalAlignments VerticalAlign { get; set; }

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06001F4D RID: 8013
		// (set) Token: 0x06001F4E RID: 8014
		public abstract Directions Direction { get; set; }

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06001F4F RID: 8015
		// (set) Token: 0x06001F50 RID: 8016
		public abstract WritingModes WritingMode { get; set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06001F51 RID: 8017
		// (set) Token: 0x06001F52 RID: 8018
		public abstract string Language { get; set; }

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06001F53 RID: 8019
		// (set) Token: 0x06001F54 RID: 8020
		public abstract UnicodeBiDiTypes UnicodeBiDi { get; set; }

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06001F55 RID: 8021
		// (set) Token: 0x06001F56 RID: 8022
		public abstract Calendars Calendar { get; set; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06001F57 RID: 8023
		// (set) Token: 0x06001F58 RID: 8024
		public abstract string CurrencyLanguage { get; set; }

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06001F59 RID: 8025
		// (set) Token: 0x06001F5A RID: 8026
		public abstract string NumeralLanguage { get; set; }

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06001F5B RID: 8027
		// (set) Token: 0x06001F5C RID: 8028
		public abstract BackgroundGradients BackgroundGradientType { get; set; }

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06001F5D RID: 8029
		// (set) Token: 0x06001F5E RID: 8030
		public abstract ReportSize FontSize { get; set; }

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06001F5F RID: 8031
		// (set) Token: 0x06001F60 RID: 8032
		public abstract ReportSize PaddingLeft { get; set; }

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06001F61 RID: 8033
		// (set) Token: 0x06001F62 RID: 8034
		public abstract ReportSize PaddingRight { get; set; }

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06001F63 RID: 8035
		// (set) Token: 0x06001F64 RID: 8036
		public abstract ReportSize PaddingTop { get; set; }

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06001F65 RID: 8037
		// (set) Token: 0x06001F66 RID: 8038
		public abstract ReportSize PaddingBottom { get; set; }

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06001F67 RID: 8039
		// (set) Token: 0x06001F68 RID: 8040
		public abstract ReportSize LineHeight { get; set; }

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06001F69 RID: 8041
		// (set) Token: 0x06001F6A RID: 8042
		public abstract int NumeralVariant { get; set; }

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06001F6B RID: 8043
		// (set) Token: 0x06001F6C RID: 8044
		public abstract TextEffects TextEffect { get; set; }

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06001F6D RID: 8045
		// (set) Token: 0x06001F6E RID: 8046
		public abstract BackgroundHatchTypes BackgroundHatchType { get; set; }

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06001F6F RID: 8047
		// (set) Token: 0x06001F70 RID: 8048
		public abstract ReportColor ShadowColor { get; set; }

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06001F71 RID: 8049
		// (set) Token: 0x06001F72 RID: 8050
		public abstract ReportSize ShadowOffset { get; set; }

		// Token: 0x04000FCB RID: 4043
		internal RenderingContext m_renderingContext;
	}
}
