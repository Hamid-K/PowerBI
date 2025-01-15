using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000057 RID: 87
	internal class GeographyFullGlobeImplementation : GeographyFullGlobe
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00006196 File Offset: 0x00004396
		internal GeographyFullGlobeImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000061A0 File Offset: 0x000043A0
		internal GeographyFullGlobeImplementation(SpatialImplementation creator)
			: this(CoordinateSystem.DefaultGeography, creator)
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000061AE File Offset: 0x000043AE
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000061B1 File Offset: 0x000043B1
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.FullGlobe);
			pipeline.EndGeography();
		}
	}
}
