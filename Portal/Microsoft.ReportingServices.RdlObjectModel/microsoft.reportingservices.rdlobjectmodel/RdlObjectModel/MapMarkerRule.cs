using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000185 RID: 389
	public class MapMarkerRule : MapAppearanceRule
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x0002116C File Offset: 0x0001F36C
		public MapMarkerRule()
		{
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00021174 File Offset: 0x0001F374
		internal MapMarkerRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0002117D File Offset: 0x0001F37D
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x00021191 File Offset: 0x0001F391
		[XmlElement(typeof(RdlCollection<MapMarker>))]
		public IList<MapMarker> MapMarkers
		{
			get
			{
				return (IList<MapMarker>)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000211A1 File Offset: 0x0001F3A1
		public override void Initialize()
		{
			base.Initialize();
			this.MapMarkers = new RdlCollection<MapMarker>();
		}

		// Token: 0x020003B3 RID: 947
		internal new class Definition : DefinitionStore<MapMarkerRule, MapMarkerRule.Definition.Properties>
		{
			// Token: 0x06001857 RID: 6231 RVA: 0x0003B639 File Offset: 0x00039839
			private Definition()
			{
			}

			// Token: 0x020004CB RID: 1227
			internal enum Properties
			{
				// Token: 0x04000ECA RID: 3786
				DataValue,
				// Token: 0x04000ECB RID: 3787
				DistributionType,
				// Token: 0x04000ECC RID: 3788
				BucketCount,
				// Token: 0x04000ECD RID: 3789
				StartValue,
				// Token: 0x04000ECE RID: 3790
				EndValue,
				// Token: 0x04000ECF RID: 3791
				MapBuckets,
				// Token: 0x04000ED0 RID: 3792
				LegendName,
				// Token: 0x04000ED1 RID: 3793
				LegendText,
				// Token: 0x04000ED2 RID: 3794
				DataElementName,
				// Token: 0x04000ED3 RID: 3795
				DataElementOutput,
				// Token: 0x04000ED4 RID: 3796
				MapMarkers,
				// Token: 0x04000ED5 RID: 3797
				PropertyCount
			}
		}
	}
}
