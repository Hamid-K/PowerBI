using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000177 RID: 375
	public class MapCustomView : MapView
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x00020907 File Offset: 0x0001EB07
		public MapCustomView()
		{
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002090F File Offset: 0x0001EB0F
		internal MapCustomView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00020918 File Offset: 0x0001EB18
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00020926 File Offset: 0x0001EB26
		[ReportExpressionDefaultValue(typeof(double), "50")]
		public ReportExpression<double> CenterX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002093A File Offset: 0x0001EB3A
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00020948 File Offset: 0x0001EB48
		[ReportExpressionDefaultValue(typeof(double), "50")]
		public ReportExpression<double> CenterY
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

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002095C File Offset: 0x0001EB5C
		public override void Initialize()
		{
			base.Initialize();
			this.CenterX = 50.0;
			this.CenterY = 50.0;
		}

		// Token: 0x020003A5 RID: 933
		internal new class Definition : DefinitionStore<MapCustomView, MapCustomView.Definition.Properties>
		{
			// Token: 0x06001849 RID: 6217 RVA: 0x0003B5C9 File Offset: 0x000397C9
			private Definition()
			{
			}

			// Token: 0x020004BD RID: 1213
			internal enum Properties
			{
				// Token: 0x04000E26 RID: 3622
				Zoom,
				// Token: 0x04000E27 RID: 3623
				CenterX,
				// Token: 0x04000E28 RID: 3624
				CenterY,
				// Token: 0x04000E29 RID: 3625
				PropertyCount
			}
		}
	}
}
