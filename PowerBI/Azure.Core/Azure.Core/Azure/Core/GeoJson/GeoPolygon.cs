using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B6 RID: 182
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPolygon : GeoObject
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00011B33 File Offset: 0x0000FD33
		public GeoPolygon(IEnumerable<GeoPosition> positions)
			: this(new GeoLinearRing[]
			{
				new GeoLinearRing(positions)
			}, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011B50 File Offset: 0x0000FD50
		public GeoPolygon(IEnumerable<GeoLinearRing> rings)
			: this(rings, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00011B5F File Offset: 0x0000FD5F
		public GeoPolygon(IEnumerable<GeoLinearRing> rings, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull<IEnumerable<GeoLinearRing>>(rings, "rings");
			this.Rings = rings.ToArray<GeoLinearRing>();
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00011B87 File Offset: 0x0000FD87
		public IReadOnlyList<GeoLinearRing> Rings { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00011B8F File Offset: 0x0000FD8F
		public GeoLinearRing OuterRing
		{
			get
			{
				return this.Rings[0];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00011B9D File Offset: 0x0000FD9D
		[Nullable(0)]
		public GeoArray<GeoArray<GeoPosition>> Coordinates
		{
			[NullableContext(0)]
			get
			{
				return new GeoArray<GeoArray<GeoPosition>>(this);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00011BA5 File Offset: 0x0000FDA5
		public override GeoObjectType Type { get; } = GeoObjectType.Polygon;
	}
}
