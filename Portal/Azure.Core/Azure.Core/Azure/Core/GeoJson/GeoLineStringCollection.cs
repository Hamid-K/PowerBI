using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B1 RID: 177
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public sealed class GeoLineStringCollection : GeoObject, IReadOnlyList<GeoLineString>, IReadOnlyCollection<GeoLineString>, IEnumerable<GeoLineString>, IEnumerable
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x000118CB File Offset: 0x0000FACB
		public GeoLineStringCollection(IEnumerable<GeoLineString> lines)
			: this(lines, null, GeoObject.DefaultProperties)
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000118DA File Offset: 0x0000FADA
		public GeoLineStringCollection(IEnumerable<GeoLineString> lines, [Nullable(2)] GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
			: base(boundingBox, customProperties)
		{
			Argument.AssertNotNull<IEnumerable<GeoLineString>>(lines, "lines");
			this.Lines = lines.ToArray<GeoLineString>();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00011902 File Offset: 0x0000FB02
		internal IReadOnlyList<GeoLineString> Lines { get; }

		// Token: 0x06000586 RID: 1414 RVA: 0x0001190A File Offset: 0x0000FB0A
		public IEnumerator<GeoLineString> GetEnumerator()
		{
			return this.Lines.GetEnumerator();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00011917 File Offset: 0x0000FB17
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001191F File Offset: 0x0000FB1F
		public int Count
		{
			get
			{
				return this.Lines.Count;
			}
		}

		// Token: 0x17000177 RID: 375
		public GeoLineString this[int index]
		{
			get
			{
				return this.Lines[index];
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001193A File Offset: 0x0000FB3A
		[Nullable(0)]
		public GeoArray<GeoArray<GeoPosition>> Coordinates
		{
			[NullableContext(0)]
			get
			{
				return new GeoArray<GeoArray<GeoPosition>>(this);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00011942 File Offset: 0x0000FB42
		public override GeoObjectType Type { get; } = GeoObjectType.MultiLineString;
	}
}
