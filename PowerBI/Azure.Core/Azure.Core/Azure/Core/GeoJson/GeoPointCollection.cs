using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B5 RID: 181
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPointCollection : GeoObject, IReadOnlyList<GeoPoint>, IReadOnlyCollection<GeoPoint>, IEnumerable<GeoPoint>, IEnumerable
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public GeoPointCollection(IEnumerable<GeoPoint> points)
			: this(points, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00011AC3 File Offset: 0x0000FCC3
		public GeoPointCollection(IEnumerable<GeoPoint> points, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull<IEnumerable<GeoPoint>>(points, "points");
			this.Points = points.ToArray<GeoPoint>();
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00011AEB File Offset: 0x0000FCEB
		internal IReadOnlyList<GeoPoint> Points { get; }

		// Token: 0x0600059D RID: 1437 RVA: 0x00011AF3 File Offset: 0x0000FCF3
		public IEnumerator<GeoPoint> GetEnumerator()
		{
			return this.Points.GetEnumerator();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00011B00 File Offset: 0x0000FD00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00011B08 File Offset: 0x0000FD08
		public int Count
		{
			get
			{
				return this.Points.Count;
			}
		}

		// Token: 0x17000181 RID: 385
		public GeoPoint this[int index]
		{
			get
			{
				return this.Points[index];
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00011B23 File Offset: 0x0000FD23
		[Nullable(0)]
		public GeoArray<GeoPosition> Coordinates
		{
			[NullableContext(0)]
			get
			{
				return new GeoArray<GeoPosition>(this);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00011B2B File Offset: 0x0000FD2B
		public override GeoObjectType Type { get; } = GeoObjectType.MultiPoint;
	}
}
