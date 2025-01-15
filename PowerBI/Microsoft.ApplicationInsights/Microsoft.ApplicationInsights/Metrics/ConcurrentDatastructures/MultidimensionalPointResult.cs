using System;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x02000039 RID: 57
	internal struct MultidimensionalPointResult<TPoint>
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000B9CD File Offset: 0x00009BCD
		internal MultidimensionalPointResult(MultidimensionalPointResultCodes failureCode, int failureCoordinateIndex)
		{
			this.resultCode = failureCode;
			this.failureCoordinateIndex = failureCoordinateIndex;
			this.point = default(TPoint);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B9E9 File Offset: 0x00009BE9
		internal MultidimensionalPointResult(MultidimensionalPointResultCodes successCode, TPoint point)
		{
			this.resultCode = successCode;
			this.failureCoordinateIndex = -1;
			this.point = point;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000BA00 File Offset: 0x00009C00
		public TPoint Point
		{
			get
			{
				return this.point;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000BA08 File Offset: 0x00009C08
		public int FailureCoordinateIndex
		{
			get
			{
				return this.failureCoordinateIndex;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000BA10 File Offset: 0x00009C10
		public MultidimensionalPointResultCodes ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000BA18 File Offset: 0x00009C18
		public bool IsPointCreatedNew
		{
			get
			{
				return (this.ResultCode & MultidimensionalPointResultCodes.Success_NewPointCreated) > (MultidimensionalPointResultCodes)0;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000BA25 File Offset: 0x00009C25
		public bool IsSuccess
		{
			get
			{
				return (this.ResultCode & MultidimensionalPointResultCodes.Success_NewPointCreated) != (MultidimensionalPointResultCodes)0 || (this.ResultCode & MultidimensionalPointResultCodes.Success_ExistingPointRetrieved) > (MultidimensionalPointResultCodes)0;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BA3E File Offset: 0x00009C3E
		internal void SetAsyncTimeoutReachedFailure()
		{
			this.resultCode |= MultidimensionalPointResultCodes.Failure_AsyncTimeoutReached;
		}

		// Token: 0x040000FA RID: 250
		private TPoint point;

		// Token: 0x040000FB RID: 251
		private int failureCoordinateIndex;

		// Token: 0x040000FC RID: 252
		private MultidimensionalPointResultCodes resultCode;
	}
}
