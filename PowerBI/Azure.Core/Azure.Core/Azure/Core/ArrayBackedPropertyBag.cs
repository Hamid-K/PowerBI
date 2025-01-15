using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000049 RID: 73
	internal struct ArrayBackedPropertyBag<TKey, [Nullable(2)] TValue> where TKey : struct, IEquatable<TKey>
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000677E File Offset: 0x0000497E
		public ArrayBackedPropertyBag()
		{
			this._first = default(global::System.ValueTuple<TKey, TValue>);
			this._second = default(global::System.ValueTuple<TKey, TValue>);
			this._rest = null;
			this._count = 0;
			this._lock = new object();
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000067B1 File Offset: 0x000049B1
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000067B9 File Offset: 0x000049B9
		public bool IsEmpty
		{
			get
			{
				return this._count == 0;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000067C4 File Offset: 0x000049C4
		public void GetAt(int index, out TKey key, [Nullable(1)] out TValue value)
		{
			global::System.ValueTuple<TKey, TValue> valueTuple;
			if (index != 0)
			{
				if (index != 1)
				{
					valueTuple = this.GetRest()[index - 2];
				}
				else
				{
					valueTuple = this._second;
				}
			}
			else
			{
				valueTuple = this._first;
			}
			global::System.ValueTuple<TKey, TValue> valueTuple2 = valueTuple;
			key = valueTuple2.Item1;
			value = valueTuple2.Item2;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006818 File Offset: 0x00004A18
		public bool TryGetValue(TKey key, [Nullable(1)] [MaybeNullWhen(false)] out TValue value)
		{
			int index = this.GetIndex(key);
			if (index < 0)
			{
				value = default(TValue);
				return false;
			}
			value = this.GetAt(index);
			return true;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006848 File Offset: 0x00004A48
		public bool TryAdd(TKey key, [Nullable(1)] TValue value, [Nullable(2)] out TValue existingValue)
		{
			int index = this.GetIndex(key);
			if (index >= 0)
			{
				existingValue = this.GetAt(index);
				return false;
			}
			this.AddInternal(key, value);
			existingValue = default(TValue);
			return true;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006880 File Offset: 0x00004A80
		public void Set(TKey key, [Nullable(1)] TValue value)
		{
			int index = this.GetIndex(key);
			if (index < 0)
			{
				this.AddInternal(key, value);
				return;
			}
			this.SetAt(index, new global::System.ValueTuple<TKey, TValue>(key, value));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000068B0 File Offset: 0x00004AB0
		public bool TryRemove(TKey key)
		{
			switch (this._count)
			{
			case 0:
				return false;
			case 1:
				if (this.IsFirst(key))
				{
					this._first = default(global::System.ValueTuple<TKey, TValue>);
					this._count--;
					return true;
				}
				return false;
			case 2:
				if (this.IsFirst(key))
				{
					this._first = this._second;
					this._second = default(global::System.ValueTuple<TKey, TValue>);
					this._count--;
					return true;
				}
				if (this.IsSecond(key))
				{
					this._second = default(global::System.ValueTuple<TKey, TValue>);
					this._count--;
					return true;
				}
				return false;
			default:
			{
				global::System.ValueTuple<TKey, TValue>[] rest = this.GetRest();
				if (this.IsFirst(key))
				{
					this._first = this._second;
					this._second = rest[0];
					this._count--;
					Array.Copy(rest, 1, rest, 0, this._count - 2);
					rest[this._count - 2] = default(global::System.ValueTuple<TKey, TValue>);
					return true;
				}
				if (this.IsSecond(key))
				{
					this._second = rest[0];
					this._count--;
					Array.Copy(rest, 1, rest, 0, this._count - 2);
					rest[this._count - 2] = default(global::System.ValueTuple<TKey, TValue>);
					return true;
				}
				for (int i = 0; i < this._count - 2; i++)
				{
					if (rest[i].Item1.Equals(key))
					{
						this._count--;
						Array.Copy(rest, i + 1, rest, i, this._count - 2 - i);
						rest[this._count - 2] = default(global::System.ValueTuple<TKey, TValue>);
						return true;
					}
				}
				return false;
			}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006A6C File Offset: 0x00004C6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsFirst(TKey key)
		{
			return this._first.Item1.Equals(key);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006A85 File Offset: 0x00004C85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsSecond(TKey key)
		{
			return this._second.Item1.Equals(key);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006AA0 File Offset: 0x00004CA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AddInternal(TKey key, [Nullable(1)] TValue value)
		{
			int count = this._count;
			if (count == 0)
			{
				this._first = new global::System.ValueTuple<TKey, TValue>(key, value);
				this._count = 1;
				return;
			}
			if (count != 1)
			{
				int num;
				if (this._rest == null)
				{
					this._rest = ArrayPool<global::System.ValueTuple<TKey, TValue>>.Shared.Rent(8);
					global::System.ValueTuple<TKey, TValue>[] rest = this._rest;
					num = this._count;
					this._count = num + 1;
					rest[num - 2] = new global::System.ValueTuple<TKey, TValue>(key, value);
					return;
				}
				if (this._rest.Length <= this._count)
				{
					global::System.ValueTuple<TKey, TValue>[] array = ArrayPool<global::System.ValueTuple<TKey, TValue>>.Shared.Rent(this._rest.Length << 1);
					this._rest.CopyTo(array, 0);
					global::System.ValueTuple<TKey, TValue>[] rest2 = this._rest;
					this._rest = array;
					ArrayPool<global::System.ValueTuple<TKey, TValue>>.Shared.Return(rest2, true);
				}
				global::System.ValueTuple<TKey, TValue>[] rest3 = this._rest;
				num = this._count;
				this._count = num + 1;
				rest3[num - 2] = new global::System.ValueTuple<TKey, TValue>(key, value);
				return;
			}
			else
			{
				if (this.IsFirst(key))
				{
					this._first = new global::System.ValueTuple<TKey, TValue>(this._first.Item1, value);
					return;
				}
				this._second = new global::System.ValueTuple<TKey, TValue>(key, value);
				this._count = 2;
				return;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006BB8 File Offset: 0x00004DB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetAt(int index, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Key", "Value" })] [Nullable(new byte[] { 0, 0, 1 })] global::System.ValueTuple<TKey, TValue> value)
		{
			if (index == 0)
			{
				this._first = value;
				return;
			}
			if (index == 1)
			{
				this._second = value;
				return;
			}
			this.GetRest()[index - 2] = value;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006BE0 File Offset: 0x00004DE0
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TValue GetAt(int index)
		{
			TValue tvalue;
			if (index != 0)
			{
				if (index != 1)
				{
					tvalue = this.GetRest()[index - 2].Item2;
				}
				else
				{
					tvalue = this._second.Item2;
				}
			}
			else
			{
				tvalue = this._first.Item2;
			}
			return tvalue;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006C28 File Offset: 0x00004E28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetIndex(TKey key)
		{
			if (this._count == 0)
			{
				return -1;
			}
			if (this._count > 0 && this._first.Item1.Equals(key))
			{
				return 0;
			}
			if (this._count > 1 && this._second.Item1.Equals(key))
			{
				return 1;
			}
			if (this._count <= 2)
			{
				return -1;
			}
			global::System.ValueTuple<TKey, TValue>[] rest = this.GetRest();
			int num = this._count - 2;
			for (int i = 0; i < num; i++)
			{
				if (rest[i].Item1.Equals(key))
				{
					return i + 2;
				}
			}
			return -1;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public void Dispose()
		{
			this._count = 0;
			this._first = default(global::System.ValueTuple<TKey, TValue>);
			this._second = default(global::System.ValueTuple<TKey, TValue>);
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._rest != null)
				{
					global::System.ValueTuple<TKey, TValue>[] rest = this._rest;
					this._rest = null;
					ArrayPool<global::System.ValueTuple<TKey, TValue>>.Shared.Return(rest, true);
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006D50 File Offset: 0x00004F50
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Key", "Value" })]
		[return: Nullable(new byte[] { 1, 0, 0, 1 })]
		private global::System.ValueTuple<TKey, TValue>[] GetRest()
		{
			global::System.ValueTuple<TKey, TValue>[] rest = this._rest;
			if (rest == null)
			{
				throw new InvalidOperationException(string.Format("{0} field is null while {1} == {2}", "_rest", "_count", this._count));
			}
			return rest;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006D81 File Offset: 0x00004F81
		[Conditional("DEBUG")]
		private void CheckDisposed()
		{
		}

		// Token: 0x040000F5 RID: 245
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Key", "Value" })]
		[Nullable(new byte[] { 0, 0, 1 })]
		private global::System.ValueTuple<TKey, TValue> _first;

		// Token: 0x040000F6 RID: 246
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Key", "Value" })]
		[Nullable(new byte[] { 0, 0, 1 })]
		private global::System.ValueTuple<TKey, TValue> _second;

		// Token: 0x040000F7 RID: 247
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Key", "Value" })]
		[Nullable(new byte[] { 2, 0, 0, 1 })]
		private global::System.ValueTuple<TKey, TValue>[] _rest;

		// Token: 0x040000F8 RID: 248
		private int _count;

		// Token: 0x040000F9 RID: 249
		[Nullable(1)]
		private readonly object _lock;
	}
}
