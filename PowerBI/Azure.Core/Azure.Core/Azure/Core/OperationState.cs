using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct OperationState
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C0BF File Offset: 0x0000A2BF
		private OperationState(Response rawResponse, bool hasCompleted, bool hasSucceeded, [Nullable(2)] RequestFailedException operationFailedException)
		{
			this.RawResponse = rawResponse;
			this.HasCompleted = hasCompleted;
			this.HasSucceeded = hasSucceeded;
			this.OperationFailedException = operationFailedException;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		public Response RawResponse { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		public bool HasCompleted { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		public bool HasSucceeded { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		[Nullable(2)]
		public RequestFailedException OperationFailedException
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		public static OperationState Success(Response rawResponse)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			return new OperationState(rawResponse, true, true, null);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000C114 File Offset: 0x0000A314
		public static OperationState Failure(Response rawResponse, [Nullable(2)] RequestFailedException operationFailedException = null)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			return new OperationState(rawResponse, true, false, operationFailedException);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000C12A File Offset: 0x0000A32A
		public static OperationState Pending(Response rawResponse)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			return new OperationState(rawResponse, false, false, null);
		}
	}
}
