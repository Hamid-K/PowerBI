using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000AF RID: 175
	public sealed class GeoLinearRing
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000117F8 File Offset: 0x0000F9F8
		[NullableContext(1)]
		public GeoLinearRing(IEnumerable<GeoPosition> coordinates)
		{
			Argument.AssertNotNull<IEnumerable<GeoPosition>>(coordinates, "coordinates");
			this.Coordinates = new GeoArray<GeoPosition>(coordinates.ToArray<GeoPosition>());
			if (this.Coordinates.Count < 4)
			{
				throw new ArgumentException("The linear ring is required to have at least 4 coordinates");
			}
			if (this.Coordinates[0] != this.Coordinates[this.Coordinates.Count - 1])
			{
				throw new ArgumentException("The first and last coordinate of the linear ring are required to be equal");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00011882 File Offset: 0x0000FA82
		public GeoArray<GeoPosition> Coordinates { get; }
	}
}
