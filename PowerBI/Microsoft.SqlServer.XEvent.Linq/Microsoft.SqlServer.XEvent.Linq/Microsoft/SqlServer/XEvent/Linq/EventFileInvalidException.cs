using System;
using System.Runtime.Serialization;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public class EventFileInvalidException : XEventException
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0001C418 File Offset: 0x0001C418
		public EventFileInvalidException()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001C42C File Offset: 0x0001C42C
		public EventFileInvalidException(string message)
			: base(message)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001C440 File Offset: 0x0001C440
		public EventFileInvalidException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001C458 File Offset: 0x0001C458
		public EventFileInvalidException(string message, string fileName)
			: base(message)
		{
			this.FileName = fileName;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001C474 File Offset: 0x0001C474
		protected EventFileInvalidException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0001C488 File Offset: 0x0001C488
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0001C49C File Offset: 0x0001C49C
		public string FileName { get; set; }
	}
}
