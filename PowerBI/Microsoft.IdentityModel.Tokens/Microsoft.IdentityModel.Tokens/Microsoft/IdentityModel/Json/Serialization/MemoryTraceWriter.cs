using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	internal class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00023ED1 File Offset: 0x000220D1
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00023ED9 File Offset: 0x000220D9
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x06000826 RID: 2086 RVA: 0x00023EE2 File Offset: 0x000220E2
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00023F08 File Offset: 0x00022108
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
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

		// Token: 0x06000828 RID: 2088 RVA: 0x00023FCC File Offset: 0x000221CC
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00023FD4 File Offset: 0x000221D4
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

		// Token: 0x040002DE RID: 734
		private readonly Queue<string> _traceMessages;

		// Token: 0x040002DF RID: 735
		private readonly object _lock;
	}
}
