using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000AD RID: 173
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoCollection : GeoObject, IReadOnlyList<GeoObject>, IReadOnlyCollection<GeoObject>, IEnumerable<GeoObject>, IEnumerable
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0001083C File Offset: 0x0000EA3C
		public GeoCollection(IEnumerable<GeoObject> geometries)
			: this(geometries, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001084B File Offset: 0x0000EA4B
		public GeoCollection(IEnumerable<GeoObject> geometries, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull<IEnumerable<GeoObject>>(geometries, "geometries");
			this.Geometries = geometries.ToArray<GeoObject>();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00010873 File Offset: 0x0000EA73
		internal IReadOnlyList<GeoObject> Geometries { get; }

		// Token: 0x06000567 RID: 1383 RVA: 0x0001087B File Offset: 0x0000EA7B
		public IEnumerator<GeoObject> GetEnumerator()
		{
			return this.Geometries.GetEnumerator();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00010888 File Offset: 0x0000EA88
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00010890 File Offset: 0x0000EA90
		public int Count
		{
			get
			{
				return this.Geometries.Count;
			}
		}

		// Token: 0x17000170 RID: 368
		public GeoObject this[int index]
		{
			get
			{
				return this.Geometries[index];
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000108AB File Offset: 0x0000EAAB
		public override GeoObjectType Type { get; } = GeoObjectType.GeometryCollection;
	}
}
