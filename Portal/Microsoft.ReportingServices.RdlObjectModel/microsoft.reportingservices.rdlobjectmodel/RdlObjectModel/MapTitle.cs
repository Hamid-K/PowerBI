using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000195 RID: 405
	public class MapTitle : MapDockableSubItem, INamedObject
	{
		// Token: 0x06000D22 RID: 3362 RVA: 0x00021F36 File Offset: 0x00020136
		public MapTitle()
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00021F3E File Offset: 0x0002013E
		internal MapTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00021F47 File Offset: 0x00020147
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00021F5B File Offset: 0x0002015B
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00021F6B File Offset: 0x0002016B
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00021F7A File Offset: 0x0002017A
		public ReportExpression Text
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00021F8F File Offset: 0x0002018F
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00021F9E File Offset: 0x0002019E
		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Angle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00021FB3 File Offset: 0x000201B3
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00021FC2 File Offset: 0x000201C2
		public ReportExpression<ReportSize> TextShadowOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00021FD7 File Offset: 0x000201D7
		public override void Initialize()
		{
			base.Initialize();
			this.Angle = 0.0;
		}

		// Token: 0x020003C2 RID: 962
		internal new class Definition : DefinitionStore<MapTitle, MapTitle.Definition.Properties>
		{
			// Token: 0x06001866 RID: 6246 RVA: 0x0003B6B1 File Offset: 0x000398B1
			private Definition()
			{
			}

			// Token: 0x020004DA RID: 1242
			internal enum Properties
			{
				// Token: 0x04000F89 RID: 3977
				Style,
				// Token: 0x04000F8A RID: 3978
				MapLocation,
				// Token: 0x04000F8B RID: 3979
				MapSize,
				// Token: 0x04000F8C RID: 3980
				LeftMargin,
				// Token: 0x04000F8D RID: 3981
				RightMargin,
				// Token: 0x04000F8E RID: 3982
				TopMargin,
				// Token: 0x04000F8F RID: 3983
				BottomMargin,
				// Token: 0x04000F90 RID: 3984
				ZIndex,
				// Token: 0x04000F91 RID: 3985
				ActionInfo,
				// Token: 0x04000F92 RID: 3986
				MapPosition,
				// Token: 0x04000F93 RID: 3987
				DockOutsideViewport,
				// Token: 0x04000F94 RID: 3988
				Hidden,
				// Token: 0x04000F95 RID: 3989
				ToolTip,
				// Token: 0x04000F96 RID: 3990
				Name,
				// Token: 0x04000F97 RID: 3991
				Text,
				// Token: 0x04000F98 RID: 3992
				Angle,
				// Token: 0x04000F99 RID: 3993
				TextShadowOffset,
				// Token: 0x04000F9A RID: 3994
				PropertyCount
			}
		}
	}
}
