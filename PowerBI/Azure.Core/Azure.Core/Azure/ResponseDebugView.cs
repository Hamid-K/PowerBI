using System;
using System.Runtime.CompilerServices;

namespace Azure
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	internal class ResponseDebugView<[Nullable(2)] T>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000284C File Offset: 0x00000A4C
		public ResponseDebugView(Response<T> response)
		{
			this._response = response;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000285B File Offset: 0x00000A5B
		public Response GetRawResponse
		{
			get
			{
				return this._response.GetRawResponse();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002868 File Offset: 0x00000A68
		public T Value
		{
			get
			{
				return this._response.Value;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly Response<T> _response;
	}
}
