using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016B RID: 363
	public class BackFrame : ReportObject
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000202FB File Offset: 0x0001E4FB
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0002030E File Offset: 0x0001E50E
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002031D File Offset: 0x0001E51D
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0002032B File Offset: 0x0001E52B
		[ReportExpressionDefaultValue(typeof(FrameStyles), FrameStyles.None)]
		public ReportExpression<FrameStyles> FrameStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FrameStyles>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002033F File Offset: 0x0001E53F
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0002034D File Offset: 0x0001E54D
		[ReportExpressionDefaultValue(typeof(FrameShapes), FrameShapes.Default)]
		public ReportExpression<FrameShapes> FrameShape
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FrameShapes>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00020361 File Offset: 0x0001E561
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0002036F File Offset: 0x0001E56F
		[ValidValues(0.0, 50.0)]
		[ReportExpressionDefaultValue(typeof(double), 8.0)]
		public ReportExpression<double> FrameWidth
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00020383 File Offset: 0x0001E583
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x00020391 File Offset: 0x0001E591
		[ReportExpressionDefaultValue(typeof(GlassEffects), GlassEffects.None)]
		public ReportExpression<GlassEffects> GlassEffect
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GlassEffects>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000203A5 File Offset: 0x0001E5A5
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x000203B8 File Offset: 0x0001E5B8
		public FrameBackground FrameBackground
		{
			get
			{
				return (FrameBackground)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000203C7 File Offset: 0x0001E5C7
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x000203DA File Offset: 0x0001E5DA
		public FrameImage FrameImage
		{
			get
			{
				return (FrameImage)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000203E9 File Offset: 0x0001E5E9
		public BackFrame()
		{
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000203F1 File Offset: 0x0001E5F1
		internal BackFrame(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000203FA File Offset: 0x0001E5FA
		public override void Initialize()
		{
			base.Initialize();
			this.FrameWidth = 8.0;
		}

		// Token: 0x0200039C RID: 924
		internal class Definition : DefinitionStore<BackFrame, BackFrame.Definition.Properties>
		{
			// Token: 0x0600183F RID: 6207 RVA: 0x0003B523 File Offset: 0x00039723
			private Definition()
			{
			}

			// Token: 0x020004B5 RID: 1205
			internal enum Properties
			{
				// Token: 0x04000DD2 RID: 3538
				Style,
				// Token: 0x04000DD3 RID: 3539
				FrameStyle,
				// Token: 0x04000DD4 RID: 3540
				FrameShape,
				// Token: 0x04000DD5 RID: 3541
				FrameWidth,
				// Token: 0x04000DD6 RID: 3542
				GlassEffect,
				// Token: 0x04000DD7 RID: 3543
				FrameBackground,
				// Token: 0x04000DD8 RID: 3544
				FrameImage,
				// Token: 0x04000DD9 RID: 3545
				PropertyCount
			}
		}
	}
}
