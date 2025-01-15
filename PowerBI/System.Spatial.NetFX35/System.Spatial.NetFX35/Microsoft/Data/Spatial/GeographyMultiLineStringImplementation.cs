using System;
using System.Collections.ObjectModel;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000053 RID: 83
	internal class GeographyMultiLineStringImplementation : GeographyMultiLineString
	{
		// Token: 0x0600022A RID: 554 RVA: 0x000063E4 File Offset: 0x000045E4
		internal GeographyMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeographyLineString[0];
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000063FF File Offset: 0x000045FF
		internal GeographyMultiLineStringImplementation(SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeography, creator, lineStrings)
		{
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000640E File Offset: 0x0000460E
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000641B File Offset: 0x0000461B
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.lineStrings);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006428 File Offset: 0x00004628
		public override ReadOnlyCollection<GeographyLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.lineStrings);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006438 File Offset: 0x00004638
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.MultiLineString);
			for (int i = 0; i < this.lineStrings.Length; i++)
			{
				this.lineStrings[i].SendTo(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000066 RID: 102
		private GeographyLineString[] lineStrings;
	}
}
