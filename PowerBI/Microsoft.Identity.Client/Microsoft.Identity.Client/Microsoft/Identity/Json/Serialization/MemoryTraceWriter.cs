using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200009C RID: 156
	internal class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00023871 File Offset: 0x00021A71
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00023879 File Offset: 0x00021A79
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x0600081C RID: 2076 RVA: 0x00023882 File Offset: 0x00021A82
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000238A8 File Offset: 0x00021AA8
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(' ');
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(' ');
			stringBuilder.Append(message);
			string text = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(text);
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00023968 File Offset: 0x00021B68
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00023970 File Offset: 0x00021B70
		public override string ToString()
		{
			object @lock = this._lock;
			string text2;
			lock (@lock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this._traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(text);
				}
				text2 = stringBuilder.ToString();
			}
			return text2;
		}

		// Token: 0x040002C3 RID: 707
		private readonly Queue<string> _traceMessages;

		// Token: 0x040002C4 RID: 708
		private readonly object _lock;
	}
}
