using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	[Nullable(0)]
	public class StatusCodeClassifier : ResponseClassifier
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A019 File Offset: 0x00008219
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000A021 File Offset: 0x00008221
		[Nullable(new byte[] { 2, 1 })]
		internal ResponseClassificationHandler[] Handlers
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A02C File Offset: 0x0000822C
		[NullableContext(0)]
		public unsafe StatusCodeClassifier(ReadOnlySpan<ushort> successStatusCodes)
		{
			this._successCodes = new ulong[10];
			ReadOnlySpan<ushort> readOnlySpan = successStatusCodes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				int num = (int)(*readOnlySpan[i]);
				this.AddClassifier(num, false);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A072 File Offset: 0x00008272
		private StatusCodeClassifier(ulong[] successCodes, [Nullable(new byte[] { 2, 1 })] ResponseClassificationHandler[] handlers)
		{
			this._successCodes = successCodes;
			this.Handlers = handlers;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A088 File Offset: 0x00008288
		public override bool IsErrorResponse(HttpMessage message)
		{
			if (this.Handlers != null)
			{
				ResponseClassificationHandler[] handlers = this.Handlers;
				for (int i = 0; i < handlers.Length; i++)
				{
					bool flag;
					if (handlers[i].TryClassify(message, out flag))
					{
						return flag;
					}
				}
			}
			return !this.IsSuccessCode(message.Response.Status);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A0D8 File Offset: 0x000082D8
		internal virtual StatusCodeClassifier Clone()
		{
			ulong[] array = new ulong[10];
			Array.Copy(this._successCodes, array, 10);
			return new StatusCodeClassifier(array, this.Handlers);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A108 File Offset: 0x00008308
		internal void AddClassifier(int statusCode, bool isError)
		{
			Argument.AssertInRange<int>(statusCode, 0, 639, "statusCode");
			int num = statusCode >> 6;
			int num2 = statusCode & 63;
			ulong num3 = 1UL << num2;
			ulong num4 = this._successCodes[num];
			if (!isError)
			{
				num4 |= num3;
			}
			else
			{
				num4 &= ~num3;
			}
			this._successCodes[num] = num4;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A158 File Offset: 0x00008358
		private bool IsSuccessCode(int statusCode)
		{
			int num = statusCode >> 6;
			int num2 = statusCode & 63;
			ulong num3 = 1UL << num2;
			return (this._successCodes[num] & num3) > 0UL;
		}

		// Token: 0x0400016D RID: 365
		private const int Length = 10;

		// Token: 0x0400016E RID: 366
		private ulong[] _successCodes;
	}
}
