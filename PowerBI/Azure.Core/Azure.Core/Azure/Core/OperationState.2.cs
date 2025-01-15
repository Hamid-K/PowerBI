using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200007D RID: 125
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct OperationState<T>
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000C409 File Offset: 0x0000A609
		private OperationState([Nullable(1)] Response rawResponse, bool hasCompleted, bool hasSucceeded, T value, RequestFailedException operationFailedException)
		{
			this.RawResponse = rawResponse;
			this.HasCompleted = hasCompleted;
			this.HasSucceeded = hasSucceeded;
			this.Value = value;
			this.OperationFailedException = operationFailedException;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000C430 File Offset: 0x0000A630
		[Nullable(1)]
		public Response RawResponse
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000C438 File Offset: 0x0000A638
		public bool HasCompleted { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000C440 File Offset: 0x0000A640
		public bool HasSucceeded { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000C448 File Offset: 0x0000A648
		public T Value { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000C450 File Offset: 0x0000A650
		public RequestFailedException OperationFailedException { get; }

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C458 File Offset: 0x0000A658
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static OperationState<T> Success(Response rawResponse, T value)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new OperationState<T>(rawResponse, true, true, value, null);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C484 File Offset: 0x0000A684
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static OperationState<T> Failure(Response rawResponse, [Nullable(2)] RequestFailedException operationFailedException = null)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			return new OperationState<T>(rawResponse, true, false, default(T), operationFailedException);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static OperationState<T> Pending(Response rawResponse)
		{
			Argument.AssertNotNull<Response>(rawResponse, "rawResponse");
			return new OperationState<T>(rawResponse, false, false, default(T), null);
		}
	}
}
