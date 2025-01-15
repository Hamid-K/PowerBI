using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public class EventFileIOException : XEventException
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0001C334 File Offset: 0x0001C334
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0001C348 File Offset: 0x0001C348
		public int ErrorNumber { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0001C35C File Offset: 0x0001C35C
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0001C370 File Offset: 0x0001C370
		public string FileName { get; set; }

		// Token: 0x06000281 RID: 641 RVA: 0x0001C384 File Offset: 0x0001C384
		public EventFileIOException()
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001C398 File Offset: 0x0001C398
		public EventFileIOException(string message)
			: base(message)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001C3AC File Offset: 0x0001C3AC
		public EventFileIOException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001C3C4 File Offset: 0x0001C3C4
		public EventFileIOException(string message, int osError)
			: base(message)
		{
			this.ErrorNumber = osError;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001C3E0 File Offset: 0x0001C3E0
		public EventFileIOException(string message, int osError, string filename)
			: base(message)
		{
			this.ErrorNumber = osError;
			this.FileName = filename;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001C404 File Offset: 0x0001C404
		protected EventFileIOException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
