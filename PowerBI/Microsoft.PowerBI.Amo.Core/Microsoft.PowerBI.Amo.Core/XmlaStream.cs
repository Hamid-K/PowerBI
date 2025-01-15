using System;
using System.IO;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000068 RID: 104
	internal abstract class XmlaStream : Stream
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00021F58 File Offset: 0x00020158
		private protected XmlaStream(bool isXmlaTracingSupported)
		{
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00021F6B File Offset: 0x0002016B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00021F6E File Offset: 0x0002016E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00021F71 File Offset: 0x00020171
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00021F74 File Offset: 0x00020174
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00021F7B File Offset: 0x0002017B
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00021F82 File Offset: 0x00020182
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00021F89 File Offset: 0x00020189
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00021F91 File Offset: 0x00020191
		public virtual string SessionID
		{
			get
			{
				return this.sessionID;
			}
			set
			{
				this.sessionID = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00021F9A File Offset: 0x0002019A
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00021FA2 File Offset: 0x000201A2
		public virtual bool IsSessionTokenNeeded
		{
			get
			{
				return this.isSessionTokenNeeded;
			}
			set
			{
				this.isSessionTokenNeeded = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00021FAB File Offset: 0x000201AB
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00021FB3 File Offset: 0x000201B3
		public virtual bool IsCompressionEnabled
		{
			get
			{
				return this.isCompressionEnabled;
			}
			set
			{
				this.isCompressionEnabled = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00021FBC File Offset: 0x000201BC
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00021FC4 File Offset: 0x000201C4
		public virtual Guid ActivityID { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00021FCD File Offset: 0x000201CD
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00021FD5 File Offset: 0x000201D5
		public virtual Guid RequestID { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00021FDE File Offset: 0x000201DE
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00021FE6 File Offset: 0x000201E6
		public virtual Guid CurrentActivityID { get; set; }

		// Token: 0x060005AD RID: 1453 RVA: 0x00021FEF File Offset: 0x000201EF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00021FF6 File Offset: 0x000201F6
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00021FFD File Offset: 0x000201FD
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00022004 File Offset: 0x00020204
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002200B File Offset: 0x0002020B
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00022012 File Offset: 0x00020212
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B3 RID: 1459
		public abstract XmlaDataType GetRequestDataType();

		// Token: 0x060005B4 RID: 1460 RVA: 0x00022019 File Offset: 0x00020219
		public virtual void WriteSoapActionHeader(string action)
		{
		}

		// Token: 0x060005B5 RID: 1461
		public abstract void WriteEndOfMessage();

		// Token: 0x060005B6 RID: 1462
		public abstract XmlaDataType GetResponseDataType();

		// Token: 0x060005B7 RID: 1463
		public abstract void Skip();

		// Token: 0x060005B8 RID: 1464 RVA: 0x0002201B File Offset: 0x0002021B
		public virtual string GetExtendedErrorInfo()
		{
			return string.Empty;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00022024 File Offset: 0x00020224
		public new virtual void Dispose()
		{
			try
			{
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x040003FE RID: 1022
		private protected bool disposed;

		// Token: 0x040003FF RID: 1023
		private string sessionID = string.Empty;

		// Token: 0x04000400 RID: 1024
		private bool isCompressionEnabled;

		// Token: 0x04000401 RID: 1025
		private bool isSessionTokenNeeded;
	}
}
