using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005B RID: 91
	internal class GeographyFullGlobeImplementation : GeographyFullGlobe
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00006822 File Offset: 0x00004A22
		internal GeographyFullGlobeImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000682C File Offset: 0x00004A2C
		internal GeographyFullGlobeImplementation(SpatialImplementation creator)
			: this(CoordinateSystem.DefaultGeography, creator)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000683A File Offset: 0x00004A3A
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000683D File Offset: 0x00004A3D
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.FullGlobe);
			pipeline.EndGeography();
		}
	}
}
