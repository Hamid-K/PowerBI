using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ByteArrayReaderCache<[IsUnmanaged, Nullable(0)] TPhysical, [Nullable(2)] TLogical> where TPhysical : struct
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000291C File Offset: 0x00000B1C
		public ByteArrayReaderCache(ColumnChunkMetaData columnChunkMetaData)
		{
			this._map = ((columnChunkMetaData.Encodings.Any((Encoding e) => e == Encoding.PlainDictionary || e == Encoding.RleDictionary) && (typeof(TPhysical) == typeof(ByteArray) || typeof(TPhysical) == typeof(FixedLenByteArray))) ? new Dictionary<TPhysical, TLogical>() : null);
			this._scratch = new byte[64];
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029C0 File Offset: 0x00000BC0
		public bool IsUsable
		{
			get
			{
				return this._map != null;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029CC File Offset: 0x00000BCC
		public void Clear()
		{
			if (this._map == null)
			{
				throw new InvalidOperationException("cache is not in a usable state");
			}
			this._map.Clear();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029F0 File Offset: 0x00000BF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TLogical Add([Nullable(0)] TPhysical physical, TLogical logical)
		{
			if (this._map == null)
			{
				throw new InvalidOperationException("cache is not in a usable state");
			}
			this._map.Add(physical, logical);
			return logical;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A18 File Offset: 0x00000C18
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetValue(TPhysical physical, [Nullable(1)] out TLogical logical)
		{
			if (this._map == null)
			{
				throw new InvalidOperationException("cache is not in a usable state");
			}
			return this._map.TryGetValue(physical, out logical);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A40 File Offset: 0x00000C40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte[] GetScratchBuffer(int minLength)
		{
			if (this._scratch.Length < minLength)
			{
				return this._scratch = new byte[Math.Max(this._scratch.Length * 2, minLength)];
			}
			return this._scratch;
		}

		// Token: 0x04000021 RID: 33
		[Nullable(new byte[] { 2, 0, 1 })]
		private readonly Dictionary<TPhysical, TLogical> _map;

		// Token: 0x04000022 RID: 34
		private byte[] _scratch;
	}
}
