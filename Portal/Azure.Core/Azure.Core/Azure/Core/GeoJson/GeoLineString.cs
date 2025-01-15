using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B0 RID: 176
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoLineString : GeoObject
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0001188A File Offset: 0x0000FA8A
		[NullableContext(1)]
		public GeoLineString(IEnumerable<GeoPosition> coordinates)
			: this(coordinates, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00011899 File Offset: 0x0000FA99
		[NullableContext(1)]
		public GeoLineString(IEnumerable<GeoPosition> coordinates, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			this.Coordinates = new GeoArray<GeoPosition>(coordinates.ToArray<GeoPosition>());
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000118BB File Offset: 0x0000FABB
		public GeoArray<GeoPosition> Coordinates { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000118C3 File Offset: 0x0000FAC3
		public override GeoObjectType Type { get; } = GeoObjectType.LineString;
	}
}
