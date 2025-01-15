using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Azure
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	[DebuggerTypeProxy(typeof(ResponseDebugView<>))]
	public abstract class Response<[Nullable(2)] T> : NullableResponse<T>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003C03 File Offset: 0x00001E03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003C06 File Offset: 0x00001E06
		public override T Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003C0E File Offset: 0x00001E0E
		public static implicit operator T(Response<T> response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response", string.Format("The implicit cast from Response<{0}> to {1} failed because the Response<{2}> was null.", typeof(T), typeof(T), typeof(T)));
			}
			return response.Value;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003C4C File Offset: 0x00001E4C
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003C55 File Offset: 0x00001E55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
