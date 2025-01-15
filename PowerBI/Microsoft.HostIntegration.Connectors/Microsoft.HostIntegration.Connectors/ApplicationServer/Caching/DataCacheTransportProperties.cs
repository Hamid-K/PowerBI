using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A9 RID: 681
	public class DataCacheTransportProperties : ICloneable
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0004B030 File Offset: 0x00049230
		// (set) Token: 0x060018F0 RID: 6384 RVA: 0x0004B038 File Offset: 0x00049238
		public long MaxBufferPoolSize
		{
			get
			{
				return this._maxBufferPoolSize;
			}
			set
			{
				DataCacheTransportProperties.LongNonNegativeValidator.Validate(value);
				this._maxBufferPoolSize = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0004B051 File Offset: 0x00049251
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0004B059 File Offset: 0x00049259
		public int MaxBufferSize
		{
			get
			{
				return this._maxBufferSize;
			}
			set
			{
				DataCacheTransportProperties.IntegerNonNegativeValidator.Validate(value);
				this._maxBufferSize = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0004B072 File Offset: 0x00049272
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0004B07A File Offset: 0x0004927A
		public int ConnectionBufferSize
		{
			get
			{
				return this._connectionBufferSize;
			}
			set
			{
				DataCacheTransportProperties.IntegerNonNegativeValidator.Validate(value);
				this._connectionBufferSize = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0004B093 File Offset: 0x00049293
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0004B09B File Offset: 0x0004929B
		public TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return this._channelInitializationTimeout;
			}
			set
			{
				DataCacheTransportProperties.TimeSpanNonNegativeValidator.Validate(value);
				this._channelInitializationTimeout = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0004B0B4 File Offset: 0x000492B4
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0004B0BC File Offset: 0x000492BC
		public TimeSpan MaxOutputDelay
		{
			get
			{
				return this._maxOutputDelay;
			}
			set
			{
				DataCacheTransportProperties.TimeSpanNonNegativeValidator.Validate(value);
				this._maxOutputDelay = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0004B0D5 File Offset: 0x000492D5
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0004B0DD File Offset: 0x000492DD
		public TimeSpan ReceiveTimeout
		{
			get
			{
				return this._receiveTimeout;
			}
			set
			{
				DataCacheTransportProperties.TimeSpanNonNegativeValidator.Validate(value);
				this._receiveTimeout = value;
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04000D95 RID: 3477
		private TimeSpan _maxOutputDelay = TimeSpan.FromMilliseconds((double)DataCacheTransportProperties.NOT_ASSIGNED);

		// Token: 0x04000D96 RID: 3478
		private TimeSpan _channelInitializationTimeout = TimeSpan.FromMilliseconds((double)DataCacheTransportProperties.NOT_ASSIGNED);

		// Token: 0x04000D97 RID: 3479
		private TimeSpan _receiveTimeout = TimeSpan.FromMilliseconds((double)DataCacheTransportProperties.NOT_ASSIGNED);

		// Token: 0x04000D98 RID: 3480
		private int _connectionBufferSize = DataCacheTransportProperties.NOT_ASSIGNED;

		// Token: 0x04000D99 RID: 3481
		private long _maxBufferPoolSize = (long)DataCacheTransportProperties.NOT_ASSIGNED;

		// Token: 0x04000D9A RID: 3482
		private int _maxBufferSize = DataCacheTransportProperties.NOT_ASSIGNED;

		// Token: 0x04000D9B RID: 3483
		private static TimeSpanValidator TimeSpanNonNegativeValidator = new TimeSpanValidator(TimeSpan.Zero, TimeSpan.MaxValue);

		// Token: 0x04000D9C RID: 3484
		private static IntegerValidator IntegerNonNegativeValidator = new IntegerValidator(0, int.MaxValue);

		// Token: 0x04000D9D RID: 3485
		private static LongValidator LongNonNegativeValidator = new LongValidator(0L, long.MaxValue);

		// Token: 0x04000D9E RID: 3486
		internal static int NOT_ASSIGNED = -1;
	}
}
