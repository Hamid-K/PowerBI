using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B2C RID: 6956
	public abstract class RequestTraceData : IDisposable
	{
		// Token: 0x17002BC0 RID: 11200
		// (get) Token: 0x0600AE37 RID: 44599
		public abstract int TraceId { get; }

		// Token: 0x17002BC1 RID: 11201
		// (get) Token: 0x0600AE38 RID: 44600
		public abstract Guid? ActivityId { get; }

		// Token: 0x17002BC2 RID: 11202
		// (get) Token: 0x0600AE39 RID: 44601
		public abstract IResource Resource { get; }

		// Token: 0x17002BC3 RID: 11203
		// (get) Token: 0x0600AE3A RID: 44602
		public abstract Guid SessionId { get; }

		// Token: 0x17002BC4 RID: 11204
		// (get) Token: 0x0600AE3B RID: 44603
		public abstract string Type { get; }

		// Token: 0x17002BC5 RID: 11205
		// (get) Token: 0x0600AE3C RID: 44604
		public abstract DateTime Timestamp { get; }

		// Token: 0x17002BC6 RID: 11206
		// (get) Token: 0x0600AE3D RID: 44605
		public abstract Stream ContentStream { get; }

		// Token: 0x0600AE3E RID: 44606
		public abstract IDictionary<string, string> GetMetadata();

		// Token: 0x0600AE3F RID: 44607
		public abstract void Dispose();
	}
}
