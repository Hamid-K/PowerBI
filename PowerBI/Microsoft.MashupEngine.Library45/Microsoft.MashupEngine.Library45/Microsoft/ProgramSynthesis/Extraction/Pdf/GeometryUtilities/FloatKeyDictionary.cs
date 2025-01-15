using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C3D RID: 3133
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public class FloatKeyDictionary<[Nullable(2)] T> : FloatKeyReadOnlyDictionary<T>, IDictionary<float, T>, ICollection<KeyValuePair<float, T>>, IEnumerable<KeyValuePair<float, T>>, IEnumerable
	{
		// Token: 0x17000E69 RID: 3689
		public T this[float key]
		{
			get
			{
				return this._data[FloatKeyReadOnlyDictionary<T>.RoundKey(key)];
			}
			set
			{
				this._data[FloatKeyReadOnlyDictionary<T>.RoundKey(key)] = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060050CB RID: 20683 RVA: 0x000FD56B File Offset: 0x000FB76B
		ICollection<float> IDictionary<float, T>.Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x000FD578 File Offset: 0x000FB778
		ICollection<T> IDictionary<float, T>.Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x000FD8BF File Offset: 0x000FBABF
		public void Add(float key, T value)
		{
			this._data.Add(FloatKeyReadOnlyDictionary<T>.RoundKey(key), value);
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x000FD8D3 File Offset: 0x000FBAD3
		public bool Remove(float key)
		{
			return this._data.Remove(FloatKeyReadOnlyDictionary<T>.RoundKey(key));
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x000FD8E6 File Offset: 0x000FBAE6
		public void Add([Nullable(new byte[] { 0, 1 })] KeyValuePair<float, T> item)
		{
			this._data.Add(new KeyValuePair<float, T>(FloatKeyReadOnlyDictionary<T>.RoundKey(item.Key), item.Value));
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x000FD90B File Offset: 0x000FBB0B
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x000FD918 File Offset: 0x000FBB18
		public bool Remove([Nullable(new byte[] { 0, 1 })] KeyValuePair<float, T> item)
		{
			return this._data.Remove(new KeyValuePair<float, T>(FloatKeyReadOnlyDictionary<T>.RoundKey(item.Key), item.Value));
		}
	}
}
