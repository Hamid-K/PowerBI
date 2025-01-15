using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000052 RID: 82
	internal class GeographyFullGlobeImplementation : GeographyFullGlobe
	{
		// Token: 0x06000200 RID: 512 RVA: 0x000054CE File Offset: 0x000036CE
		internal GeographyFullGlobeImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000054D8 File Offset: 0x000036D8
		internal GeographyFullGlobeImplementation(SpatialImplementation creator)
			: this(CoordinateSystem.DefaultGeography, creator)
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000054E6 File Offset: 0x000036E6
		public override bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000054E9 File Offset: 0x000036E9
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.FullGlobe);
			pipeline.EndGeography();
		}
	}
}
