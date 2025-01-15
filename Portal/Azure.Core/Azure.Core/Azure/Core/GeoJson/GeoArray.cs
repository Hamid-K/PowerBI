using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct GeoArray<[Nullable(2)] T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00010367 File Offset: 0x0000E567
		internal GeoArray(object container)
		{
			this._container = container;
		}

		// Token: 0x17000165 RID: 357
		public T this[int index]
		{
			get
			{
				object container = this._container;
				T[] array = container as T[];
				T t;
				if (array == null)
				{
					GeoPointCollection geoPointCollection = container as GeoPointCollection;
					if (geoPointCollection == null)
					{
						GeoLineStringCollection geoLineStringCollection = container as GeoLineStringCollection;
						if (geoLineStringCollection == null)
						{
							GeoPolygon geoPolygon = container as GeoPolygon;
							if (geoPolygon == null)
							{
								GeoPolygonCollection geoPolygonCollection = container as GeoPolygonCollection;
								if (geoPolygonCollection == null)
								{
									t = default(T);
								}
								else
								{
									t = (T)((object)geoPolygonCollection.Polygons[index].Coordinates);
								}
							}
							else
							{
								t = (T)((object)geoPolygon.Rings[index].Coordinates);
							}
						}
						else
						{
							t = (T)((object)geoLineStringCollection.Lines[index].Coordinates);
						}
					}
					else
					{
						t = (T)((object)geoPointCollection.Points[index].Coordinates);
					}
				}
				else
				{
					t = array[index];
				}
				return t;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001045C File Offset: 0x0000E65C
		public int Count
		{
			get
			{
				object container = this._container;
				T[] array = container as T[];
				int num;
				if (array == null)
				{
					GeoPointCollection geoPointCollection = container as GeoPointCollection;
					if (geoPointCollection == null)
					{
						GeoLineStringCollection geoLineStringCollection = container as GeoLineStringCollection;
						if (geoLineStringCollection == null)
						{
							GeoPolygon geoPolygon = container as GeoPolygon;
							if (geoPolygon == null)
							{
								GeoPolygonCollection geoPolygonCollection = container as GeoPolygonCollection;
								if (geoPolygonCollection == null)
								{
									num = 0;
								}
								else
								{
									num = geoPolygonCollection.Polygons.Count;
								}
							}
							else
							{
								num = geoPolygon.Rings.Count;
							}
						}
						else
						{
							num = geoLineStringCollection.Lines.Count;
						}
					}
					else
					{
						num = geoPointCollection.Points.Count;
					}
				}
				else
				{
					num = array.Length;
				}
				return num;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000104F5 File Offset: 0x0000E6F5
		[NullableContext(0)]
		public GeoArray<T>.Enumerator GetEnumerator()
		{
			return new GeoArray<T>.Enumerator(this);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00010502 File Offset: 0x0000E702
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001050F File Offset: 0x0000E70F
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400022E RID: 558
		private readonly object _container;

		// Token: 0x02000141 RID: 321
		[Nullable(0)]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000877 RID: 2167 RVA: 0x00020A9E File Offset: 0x0001EC9E
			internal Enumerator([Nullable(new byte[] { 0, 1 })] GeoArray<T> array)
			{
				this = default(GeoArray<T>.Enumerator);
				this._array = array;
				this._index = -1;
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x00020AB5 File Offset: 0x0001ECB5
			public bool MoveNext()
			{
				this._index++;
				return this._index < this._array.Count;
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00020AD8 File Offset: 0x0001ECD8
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x0600087A RID: 2170 RVA: 0x00020AE1 File Offset: 0x0001ECE1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x0600087B RID: 2171 RVA: 0x00020AEE File Offset: 0x0001ECEE
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x00020B01 File Offset: 0x0001ED01
			public void Dispose()
			{
			}

			// Token: 0x040004E9 RID: 1257
			[Nullable(new byte[] { 0, 1 })]
			private readonly GeoArray<T> _array;

			// Token: 0x040004EA RID: 1258
			private int _index;
		}
	}
}
