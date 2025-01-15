using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017D RID: 381
	public class MapMarkerTemplate : MapPointTemplate
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x00020D0B File Offset: 0x0001EF0B
		public MapMarkerTemplate()
		{
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00020D13 File Offset: 0x0001EF13
		internal MapMarkerTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00020D1C File Offset: 0x0001EF1C
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00020D30 File Offset: 0x0001EF30
		public MapMarker MapMarker
		{
			get
			{
				return (MapMarker)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00020D40 File Offset: 0x0001EF40
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003AB RID: 939
		internal new class Definition : DefinitionStore<MapMarkerTemplate, MapMarkerTemplate.Definition.Properties>
		{
			// Token: 0x0600184F RID: 6223 RVA: 0x0003B5F9 File Offset: 0x000397F9
			private Definition()
			{
			}

			// Token: 0x020004C3 RID: 1219
			internal enum Properties
			{
				// Token: 0x04000E5C RID: 3676
				Style,
				// Token: 0x04000E5D RID: 3677
				ActionInfo,
				// Token: 0x04000E5E RID: 3678
				Hidden,
				// Token: 0x04000E5F RID: 3679
				OffsetX,
				// Token: 0x04000E60 RID: 3680
				OffsetY,
				// Token: 0x04000E61 RID: 3681
				Label,
				// Token: 0x04000E62 RID: 3682
				ToolTip,
				// Token: 0x04000E63 RID: 3683
				DataElementName,
				// Token: 0x04000E64 RID: 3684
				DataElementOutput,
				// Token: 0x04000E65 RID: 3685
				DataElementLabel,
				// Token: 0x04000E66 RID: 3686
				Size,
				// Token: 0x04000E67 RID: 3687
				LabelPlacement,
				// Token: 0x04000E68 RID: 3688
				MapMarker,
				// Token: 0x04000E69 RID: 3689
				PropertyCount
			}
		}
	}
}
