using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B7 RID: 183
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoPolygonCollection : GeoObject, IReadOnlyList<GeoPolygon>, IReadOnlyCollection<GeoPolygon>, IEnumerable<GeoPolygon>, IEnumerable
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00011BAD File Offset: 0x0000FDAD
		public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons)
			: this(polygons, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull<IEnumerable<GeoPolygon>>(polygons, "polygons");
			this.Polygons = polygons.ToArray<GeoPolygon>();
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		internal IReadOnlyList<GeoPolygon> Polygons { get; }

		// Token: 0x060005AD RID: 1453 RVA: 0x00011BEC File Offset: 0x0000FDEC
		public IEnumerator<GeoPolygon> GetEnumerator()
		{
			return this.Polygons.GetEnumerator();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00011BF9 File Offset: 0x0000FDF9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00011C01 File Offset: 0x0000FE01
		public int Count
		{
			get
			{
				return this.Polygons.Count;
			}
		}

		// Token: 0x1700018A RID: 394
		public GeoPolygon this[int index]
		{
			get
			{
				return this.Polygons[index];
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00011C1C File Offset: 0x0000FE1C
		[Nullable(0)]
		public GeoArray<GeoArray<GeoArray<GeoPosition>>> Coordinates
		{
			[NullableContext(0)]
			get
			{
				return new GeoArray<GeoArray<GeoArray<GeoPosition>>>(this);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00011C24 File Offset: 0x0000FE24
		public override GeoObjectType Type { get; } = GeoObjectType.MultiPolygon;
	}
}
