using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004B RID: 75
	internal abstract class XmlaStream : Stream
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0001E264 File Offset: 0x0001C464
		private protected XmlaStream(bool isXmlaTracingSupported)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001E277 File Offset: 0x0001C477
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001E27A File Offset: 0x0001C47A
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001E27D File Offset: 0x0001C47D
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001E280 File Offset: 0x0001C480
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001E287 File Offset: 0x0001C487
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0001E28E File Offset: 0x0001C48E
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001E295 File Offset: 0x0001C495
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001E29D File Offset: 0x0001C49D
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001E2A6 File Offset: 0x0001C4A6
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001E2AE File Offset: 0x0001C4AE
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001E2B7 File Offset: 0x0001C4B7
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001E2BF File Offset: 0x0001C4BF
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		public virtual Guid ActivityID { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001E2D9 File Offset: 0x0001C4D9
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001E2E1 File Offset: 0x0001C4E1
		public virtual Guid RequestID { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001E2EA File Offset: 0x0001C4EA
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
		public virtual Guid CurrentActivityID { get; set; }

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001E2FB File Offset: 0x0001C4FB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001E302 File Offset: 0x0001C502
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001E309 File Offset: 0x0001C509
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001E310 File Offset: 0x0001C510
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001E317 File Offset: 0x0001C517
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001E31E File Offset: 0x0001C51E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004ED RID: 1261
		public abstract XmlaDataType GetRequestDataType();

		// Token: 0x060004EE RID: 1262 RVA: 0x0001E325 File Offset: 0x0001C525
		public virtual void WriteSoapActionHeader(string action)
		{
		}

		// Token: 0x060004EF RID: 1263
		public abstract void WriteEndOfMessage();

		// Token: 0x060004F0 RID: 1264
		public abstract XmlaDataType GetResponseDataType();

		// Token: 0x060004F1 RID: 1265
		public abstract void Skip();

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001E327 File Offset: 0x0001C527
		public virtual string GetExtendedErrorInfo()
		{
			return string.Empty;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001E330 File Offset: 0x0001C530
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

		// Token: 0x040003C2 RID: 962
		private protected bool disposed;

		// Token: 0x040003C3 RID: 963
		private string sessionID = string.Empty;

		// Token: 0x040003C4 RID: 964
		private bool isCompressionEnabled;

		// Token: 0x040003C5 RID: 965
		private bool isSessionTokenNeeded;
	}
}
