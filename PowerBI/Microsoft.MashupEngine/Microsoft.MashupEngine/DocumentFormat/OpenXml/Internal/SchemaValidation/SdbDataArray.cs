using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003123 RID: 12579
	internal class SdbDataArray<T> where T : SdbData, new()
	{
		// Token: 0x0601B4A6 RID: 111782 RVA: 0x00375874 File Offset: 0x00373A74
		public SdbDataArray(byte[] sdbDataBytes)
		{
			this._sdbDataBytes = sdbDataBytes;
		}

		// Token: 0x1700993A RID: 39226
		public T this[int index]
		{
			get
			{
				T t = new T();
				t.LoadFromBytes(this._sdbDataBytes, index * t.DataSize);
				return t;
			}
		}

		// Token: 0x0400B4DB RID: 46299
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private byte[] _sdbDataBytes;
	}
}
