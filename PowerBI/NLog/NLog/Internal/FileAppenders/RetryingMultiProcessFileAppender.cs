using System;
using System.IO;
using System.Security;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000165 RID: 357
	[SecuritySafeCritical]
	internal class RetryingMultiProcessFileAppender : BaseMutexFileAppender
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0002BE67 File Offset: 0x0002A067
		public RetryingMultiProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0002BE74 File Offset: 0x0002A074
		public override void Write(byte[] bytes, int offset, int count)
		{
			int num = Math.Min((count / 4096 + 1) * 4096, base.CreateFileParameters.BufferSize);
			using (FileStream fileStream = base.CreateFileStream(false, num))
			{
				fileStream.Write(bytes, offset, count);
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public override void Flush()
		{
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0002BED2 File Offset: 0x0002A0D2
		public override void Close()
		{
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public override DateTime? GetFileCreationTimeUtc()
		{
			FileInfo fileInfo = new FileInfo(base.FileName);
			if (fileInfo.Exists)
			{
				return new DateTime?(fileInfo.GetCreationTimeUtc());
			}
			return null;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0002BF0C File Offset: 0x0002A10C
		public override long? GetFileLength()
		{
			FileInfo fileInfo = new FileInfo(base.FileName);
			if (fileInfo.Exists)
			{
				return new long?(fileInfo.Length);
			}
			return null;
		}

		// Token: 0x0400048A RID: 1162
		public static readonly IFileAppenderFactory TheFactory = new RetryingMultiProcessFileAppender.Factory();

		// Token: 0x02000297 RID: 663
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x060016D0 RID: 5840 RVA: 0x0003BED6 File Offset: 0x0003A0D6
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new RetryingMultiProcessFileAppender(fileName, parameters);
			}
		}
	}
}
