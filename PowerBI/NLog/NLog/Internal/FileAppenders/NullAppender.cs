using System;
using System.Security;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000164 RID: 356
	[SecuritySafeCritical]
	internal class NullAppender : BaseFileAppender
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x0002BE36 File Offset: 0x0002A036
		public NullAppender(string fileName, ICreateFileParameters createParameters)
			: base(fileName, createParameters)
		{
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002BE40 File Offset: 0x0002A040
		public override void Close()
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002BE42 File Offset: 0x0002A042
		public override void Flush()
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002BE44 File Offset: 0x0002A044
		public override DateTime? GetFileCreationTimeUtc()
		{
			return new DateTime?(DateTime.UtcNow);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002BE50 File Offset: 0x0002A050
		public override long? GetFileLength()
		{
			return new long?(0L);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002BE59 File Offset: 0x0002A059
		public override void Write(byte[] bytes, int offset, int count)
		{
		}

		// Token: 0x04000489 RID: 1161
		public static readonly IFileAppenderFactory TheFactory = new NullAppender.Factory();

		// Token: 0x02000296 RID: 662
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016CE RID: 5838 RVA: 0x0003BEC5 File Offset: 0x0003A0C5
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new NullAppender(fileName, parameters);
			}
		}
	}
}
