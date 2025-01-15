using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200004B RID: 75
	internal abstract class XmlaStream : Stream
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x0001E594 File Offset: 0x0001C794
		private protected XmlaStream(bool isXmlaTracingSupported)
		{
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001E5A7 File Offset: 0x0001C7A7
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001E5AA File Offset: 0x0001C7AA
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001E5AD File Offset: 0x0001C7AD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0001E5BE File Offset: 0x0001C7BE
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001E5C5 File Offset: 0x0001C7C5
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0001E5CD File Offset: 0x0001C7CD
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001E5D6 File Offset: 0x0001C7D6
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0001E5DE File Offset: 0x0001C7DE
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001E5E7 File Offset: 0x0001C7E7
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0001E5EF File Offset: 0x0001C7EF
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

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0001E600 File Offset: 0x0001C800
		public virtual Guid ActivityID { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001E609 File Offset: 0x0001C809
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0001E611 File Offset: 0x0001C811
		public virtual Guid RequestID { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001E61A File Offset: 0x0001C81A
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001E622 File Offset: 0x0001C822
		public virtual Guid CurrentActivityID { get; set; }

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001E62B File Offset: 0x0001C82B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001E632 File Offset: 0x0001C832
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001E639 File Offset: 0x0001C839
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001E640 File Offset: 0x0001C840
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001E647 File Offset: 0x0001C847
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001E64E File Offset: 0x0001C84E
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004FA RID: 1274
		public abstract XmlaDataType GetRequestDataType();

		// Token: 0x060004FB RID: 1275 RVA: 0x0001E655 File Offset: 0x0001C855
		public virtual void WriteSoapActionHeader(string action)
		{
		}

		// Token: 0x060004FC RID: 1276
		public abstract void WriteEndOfMessage();

		// Token: 0x060004FD RID: 1277
		public abstract XmlaDataType GetResponseDataType();

		// Token: 0x060004FE RID: 1278
		public abstract void Skip();

		// Token: 0x060004FF RID: 1279 RVA: 0x0001E657 File Offset: 0x0001C857
		public virtual string GetExtendedErrorInfo()
		{
			return string.Empty;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001E660 File Offset: 0x0001C860
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

		// Token: 0x040003CF RID: 975
		private protected bool disposed;

		// Token: 0x040003D0 RID: 976
		private string sessionID = string.Empty;

		// Token: 0x040003D1 RID: 977
		private bool isCompressionEnabled;

		// Token: 0x040003D2 RID: 978
		private bool isSessionTokenNeeded;
	}
}
