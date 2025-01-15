using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C37 RID: 3127
	[NullableContext(1)]
	[Nullable(0)]
	public class FloatKeyReadOnlyDictionary<[Nullable(2)] T> : IReadOnlyDictionary<float, T>, IReadOnlyCollection<KeyValuePair<float, T>>, IEnumerable<KeyValuePair<float, T>>, IEnumerable
	{
		// Token: 0x17000E63 RID: 3683
		public T this[float key]
		{
			get
			{
				return this._data[FloatKeyReadOnlyDictionary<T>.RoundKey(key)];
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x000FD56B File Offset: 0x000FB76B
		IEnumerable<float> IReadOnlyDictionary<float, T>.Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060050A9 RID: 20649 RVA: 0x000FD578 File Offset: 0x000FB778
		IEnumerable<T> IReadOnlyDictionary<float, T>.Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060050AA RID: 20650 RVA: 0x000FD578 File Offset: 0x000FB778
		public ICollection<T> Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x000FD585 File Offset: 0x000FB785
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x000FD592 File Offset: 0x000FB792
		public static FloatKeyReadOnlyDictionary<T> Empty { get; } = new FloatKeyReadOnlyDictionary<T>();

		// Token: 0x060050AD RID: 20653 RVA: 0x000FD599 File Offset: 0x000FB799
		protected FloatKeyReadOnlyDictionary()
		{
			this._data = new Dictionary<float, T>();
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x000FD5AC File Offset: 0x000FB7AC
		private FloatKeyReadOnlyDictionary([Nullable(new byte[] { 1, 0, 1 })] IEnumerable<KeyValuePair<float, T>> values)
		{
			this._data = values.ToDictionary<float, T>();
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x000FD5C0 File Offset: 0x000FB7C0
		public bool ContainsKey(float key)
		{
			return this._data.ContainsKey(FloatKeyReadOnlyDictionary<T>.RoundKey(key));
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x000FD5D3 File Offset: 0x000FB7D3
		public bool TryGetValue(float key, out T value)
		{
			return this._data.TryGetValue(FloatKeyReadOnlyDictionary<T>.RoundKey(key), out value);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x000FD5E7 File Offset: 0x000FB7E7
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public IEnumerator<KeyValuePair<float, T>> GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x000FD5F4 File Offset: 0x000FB7F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x000FD5FC File Offset: 0x000FB7FC
		public static FloatKeyReadOnlyDictionary<T> Create<[Nullable(2)] TInput>(IEnumerable<TInput> xs, Func<TInput, float> getKey, Func<IEnumerable<TInput>, T> combine)
		{
			return new FloatKeyReadOnlyDictionary<T>(from x in xs
				group x by FloatKeyReadOnlyDictionary<T>.RoundKey(getKey(x)) into g
				select new KeyValuePair<float, T>(g.Key, combine(g)));
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x000FD648 File Offset: 0x000FB848
		public static FloatKeyReadOnlyDictionary<T> CreateDropNullKeys<[Nullable(2)] TInput>(IEnumerable<TInput> xs, Func<TInput, float?> getKey, Func<IEnumerable<TInput>, T> combine)
		{
			return new FloatKeyReadOnlyDictionary<T>(from g in xs.GroupByNonNull(delegate(TInput x)
				{
					float? num = getKey(x);
					if (num != null)
					{
						float valueOrDefault = num.GetValueOrDefault();
						return new float?(FloatKeyReadOnlyDictionary<T>.RoundKey(valueOrDefault));
					}
					return null;
				})
				select new KeyValuePair<float, T>(g.Key, combine(g)));
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x000FD691 File Offset: 0x000FB891
		public static FloatKeyReadOnlyDictionary<T> Create<[Nullable(0)] TInput>(IEnumerable<TInput> xs, Func<IEnumerable<TInput>, T> combine) where TInput : IRotatedPixelBounded
		{
			return FloatKeyReadOnlyDictionary<T>.Create<TInput>(xs, new Func<TInput, float>(FloatKeyReadOnlyDictionary<T>.<Create>g__GetAngle|22_0<TInput>), combine);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x000FD6A6 File Offset: 0x000FB8A6
		public bool Contains([Nullable(new byte[] { 0, 1 })] KeyValuePair<float, T> item)
		{
			return this._data.Contains(new KeyValuePair<float, T>(FloatKeyReadOnlyDictionary<T>.RoundKey(item.Key), item.Value));
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x000FD6CB File Offset: 0x000FB8CB
		public void CopyTo([Nullable(new byte[] { 1, 0, 1 })] KeyValuePair<float, T>[] array, int arrayIndex)
		{
			this._data.CopyTo(array, arrayIndex);
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x000FD6DA File Offset: 0x000FB8DA
		protected static float RoundKey(float key)
		{
			return (float)Math.Round((double)key, 2);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x000FD6E8 File Offset: 0x000FB8E8
		public FloatKeyReadOnlyDictionary<TResult> Select<[Nullable(2)] TResult>(Func<T, TResult> func)
		{
			return new FloatKeyReadOnlyDictionary<TResult>(this._data.Select((KeyValuePair<float, T> kv) => new KeyValuePair<float, TResult>(kv.Key, func(kv.Value))));
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x000FD720 File Offset: 0x000FB920
		public FloatKeyReadOnlyDictionary<TResult> SelectOptionalValues<[Nullable(2)] TResult>([Nullable(new byte[] { 1, 1, 0, 1 })] Func<float, T, Optional<TResult>> func)
		{
			return new FloatKeyReadOnlyDictionary<TResult>(this._data.SelectMany((KeyValuePair<float, T> kv) => from computed in func(kv.Key, kv.Value)
				select new KeyValuePair<float, TResult>(kv.Key, computed)));
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x000FD780 File Offset: 0x000FB980
		[CompilerGenerated]
		internal static float <Create>g__GetAngle|22_0<TInput>(TInput x) where TInput : IRotatedPixelBounded
		{
			return x.RotationAngle.GetValueOrDefault();
		}

		// Token: 0x0400238F RID: 9103
		protected readonly IDictionary<float, T> _data;

		// Token: 0x04002391 RID: 9105
		public const int RoundingDigits = 2;

		// Token: 0x04002392 RID: 9106
		public static double Epsilon = Math.Pow(10.0, -2.0);
	}
}
