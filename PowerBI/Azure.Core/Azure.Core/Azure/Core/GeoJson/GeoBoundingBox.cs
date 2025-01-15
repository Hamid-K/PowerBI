using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000AC RID: 172
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class GeoBoundingBox : IEquatable<GeoBoundingBox>
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001051C File Offset: 0x0000E71C
		public double West { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00010524 File Offset: 0x0000E724
		public double South { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001052C File Offset: 0x0000E72C
		public double East { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00010534 File Offset: 0x0000E734
		public double North { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001053C File Offset: 0x0000E73C
		public double? MinAltitude { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00010544 File Offset: 0x0000E744
		public double? MaxAltitude { get; }

		// Token: 0x0600055D RID: 1373 RVA: 0x0001054C File Offset: 0x0000E74C
		public GeoBoundingBox(double west, double south, double east, double north)
			: this(west, south, east, north, null, null)
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00010576 File Offset: 0x0000E776
		public GeoBoundingBox(double west, double south, double east, double north, double? minAltitude, double? maxAltitude)
		{
			this.West = west;
			this.South = south;
			this.East = east;
			this.North = north;
			this.MinAltitude = minAltitude;
			this.MaxAltitude = maxAltitude;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000105AC File Offset: 0x0000E7AC
		public bool Equals(GeoBoundingBox other)
		{
			return other != null && (this.West.Equals(other.West) && this.South.Equals(other.South) && this.East.Equals(other.East) && this.North.Equals(other.North) && Nullable.Equals<double>(this.MinAltitude, other.MinAltitude)) && Nullable.Equals<double>(this.MaxAltitude, other.MaxAltitude);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001063C File Offset: 0x0000E83C
		public override bool Equals(object obj)
		{
			GeoBoundingBox geoBoundingBox = obj as GeoBoundingBox;
			return geoBoundingBox != null && this.Equals(geoBoundingBox);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001065C File Offset: 0x0000E85C
		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine<double, double, double, double, double?, double?>(this.West, this.South, this.East, this.North, this.MinAltitude, this.MaxAltitude);
		}

		// Token: 0x1700016D RID: 365
		public double this[int index]
		{
			get
			{
				double? num = this.MinAltitude;
				double num2;
				if (num != null)
				{
					double valueOrDefault = num.GetValueOrDefault();
					num = this.MaxAltitude;
					if (num != null)
					{
						double valueOrDefault2 = num.GetValueOrDefault();
						switch (index)
						{
						case 0:
							num2 = this.West;
							break;
						case 1:
							num2 = this.South;
							break;
						case 2:
							num2 = valueOrDefault;
							break;
						case 3:
							num2 = this.East;
							break;
						case 4:
							num2 = this.North;
							break;
						case 5:
							num2 = valueOrDefault2;
							break;
						default:
							throw new IndexOutOfRangeException();
						}
						return num2;
					}
				}
				switch (index)
				{
				case 0:
					num2 = this.West;
					break;
				case 1:
					num2 = this.South;
					break;
				case 2:
					num2 = this.East;
					break;
				case 3:
					num2 = this.North;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return num2;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001075C File Offset: 0x0000E95C
		[NullableContext(1)]
		public override string ToString()
		{
			double? num = this.MinAltitude;
			if (num != null)
			{
				double valueOrDefault = num.GetValueOrDefault();
				num = this.MaxAltitude;
				if (num != null)
				{
					double valueOrDefault2 = num.GetValueOrDefault();
					return string.Format("[{0}, {1}, {2}, {3}, {4}, {5}]", new object[] { this.West, this.South, valueOrDefault, this.East, this.North, valueOrDefault2 });
				}
			}
			return string.Format("[{0}, {1}, {2}, {3}]", new object[] { this.West, this.South, this.East, this.North });
		}
	}
}
