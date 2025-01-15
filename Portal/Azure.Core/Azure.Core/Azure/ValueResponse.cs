using System;
using System.Runtime.CompilerServices;

namespace Azure
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal class ValueResponse<[Nullable(2)] T> : Response<T>
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002875 File Offset: 0x00000A75
		public ValueResponse(Response response, T value)
		{
			this._response = response;
			this.Value = value;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000288B File Offset: 0x00000A8B
		public override T Value { get; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002893 File Offset: 0x00000A93
		public override Response GetRawResponse()
		{
			return this._response;
		}

		// Token: 0x0400003E RID: 62
		private readonly Response _response;
	}
}
