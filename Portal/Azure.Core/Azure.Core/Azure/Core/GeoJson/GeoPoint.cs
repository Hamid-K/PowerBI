using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B4 RID: 180
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPoint : GeoObject
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x00011A59 File Offset: 0x0000FC59
		public GeoPoint(double longitude, double latitude)
			: this(new GeoPosition(longitude, latitude), null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011A6E File Offset: 0x0000FC6E
		public GeoPoint(double longitude, double latitude, double? altitude)
			: this(new GeoPosition(longitude, latitude, altitude), null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00011A84 File Offset: 0x0000FC84
		public GeoPoint(GeoPosition position)
			: this(position, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00011A93 File Offset: 0x0000FC93
		[NullableContext(2)]
		public GeoPoint(GeoPosition position, GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			this.Coordinates = position;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		public GeoPosition Coordinates { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public override GeoObjectType Type { get; }
	}
}
