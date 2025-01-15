using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A8 RID: 424
	public class MapPointRules : ReportObject
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x00022CB3 File Offset: 0x00020EB3
		public MapPointRules()
		{
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00022CBB File Offset: 0x00020EBB
		internal MapPointRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00022CC4 File Offset: 0x00020EC4
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00022CD7 File Offset: 0x00020ED7
		public MapSizeRule MapSizeRule
		{
			get
			{
				return (MapSizeRule)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00022CE6 File Offset: 0x00020EE6
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00022CF9 File Offset: 0x00020EF9
		public MapColorRule MapColorRule
		{
			get
			{
				return (MapColorRule)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00022D08 File Offset: 0x00020F08
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00022D1B File Offset: 0x00020F1B
		public MapMarkerRule MapMarkerRule
		{
			get
			{
				return (MapMarkerRule)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00022D2A File Offset: 0x00020F2A
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003D4 RID: 980
		internal class Definition : DefinitionStore<MapPointRules, MapPointRules.Definition.Properties>
		{
			// Token: 0x06001878 RID: 6264 RVA: 0x0003B741 File Offset: 0x00039941
			private Definition()
			{
			}

			// Token: 0x020004EC RID: 1260
			internal enum Properties
			{
				// Token: 0x04001023 RID: 4131
				MapSizeRule,
				// Token: 0x04001024 RID: 4132
				MapColorRule,
				// Token: 0x04001025 RID: 4133
				MapMarkerRule,
				// Token: 0x04001026 RID: 4134
				PropertyCount
			}
		}
	}
}
