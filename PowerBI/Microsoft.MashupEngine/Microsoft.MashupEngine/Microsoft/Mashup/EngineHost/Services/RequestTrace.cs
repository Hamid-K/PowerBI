using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B2A RID: 6954
	internal sealed class RequestTrace : IRequestTrace, IDisposable
	{
		// Token: 0x0600AE22 RID: 44578 RVA: 0x0023ADA0 File Offset: 0x00238FA0
		public RequestTrace(int traceId, Guid? activityId, IResource resource, Guid sessionId, string type)
		{
			this.traceId = traceId;
			this.activityId = activityId;
			this.resource = resource;
			this.sessionId = sessionId;
			this.type = type;
			this.timestamp = DateTime.Now;
			this.sharedStream = new BlockingStream(16, 16384, 65536);
			this.metadataLock = new object();
			this.metadata = new Dictionary<string, string>();
		}

		// Token: 0x17002BB3 RID: 11187
		// (get) Token: 0x0600AE23 RID: 44579 RVA: 0x0023AE10 File Offset: 0x00239010
		public int TraceId
		{
			get
			{
				return this.traceId;
			}
		}

		// Token: 0x17002BB4 RID: 11188
		// (get) Token: 0x0600AE24 RID: 44580 RVA: 0x0023AE18 File Offset: 0x00239018
		public Guid? ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x17002BB5 RID: 11189
		// (get) Token: 0x0600AE25 RID: 44581 RVA: 0x0023AE20 File Offset: 0x00239020
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17002BB6 RID: 11190
		// (get) Token: 0x0600AE26 RID: 44582 RVA: 0x0023AE28 File Offset: 0x00239028
		public Guid SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17002BB7 RID: 11191
		// (get) Token: 0x0600AE27 RID: 44583 RVA: 0x0023AE30 File Offset: 0x00239030
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002BB8 RID: 11192
		// (get) Token: 0x0600AE28 RID: 44584 RVA: 0x0023AE38 File Offset: 0x00239038
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x0600AE29 RID: 44585 RVA: 0x0023AE40 File Offset: 0x00239040
		public void AddMetadata(string name, string value)
		{
			object obj = this.metadataLock;
			lock (obj)
			{
				this.metadata.Add(name, value);
			}
		}

		// Token: 0x0600AE2A RID: 44586 RVA: 0x0023AE88 File Offset: 0x00239088
		public Stream GetContentStream()
		{
			return this.sharedStream.OutputStream;
		}

		// Token: 0x0600AE2B RID: 44587 RVA: 0x0023AE95 File Offset: 0x00239095
		public RequestTraceData GetData()
		{
			return new RequestTrace.InternalTraceData(this);
		}

		// Token: 0x0600AE2C RID: 44588 RVA: 0x0023AE9D File Offset: 0x0023909D
		public void Dispose()
		{
			this.sharedStream.OutputStream.Dispose();
		}

		// Token: 0x040059CD RID: 22989
		private const int StartBufferSize = 16384;

		// Token: 0x040059CE RID: 22990
		private const int MaxBufferSize = 65536;

		// Token: 0x040059CF RID: 22991
		private const int MaxBuffers = 16;

		// Token: 0x040059D0 RID: 22992
		private readonly int traceId;

		// Token: 0x040059D1 RID: 22993
		private readonly Guid? activityId;

		// Token: 0x040059D2 RID: 22994
		private readonly IResource resource;

		// Token: 0x040059D3 RID: 22995
		private readonly Guid sessionId;

		// Token: 0x040059D4 RID: 22996
		private readonly string type;

		// Token: 0x040059D5 RID: 22997
		private readonly DateTime timestamp;

		// Token: 0x040059D6 RID: 22998
		private readonly object metadataLock;

		// Token: 0x040059D7 RID: 22999
		private readonly Dictionary<string, string> metadata;

		// Token: 0x040059D8 RID: 23000
		private readonly BlockingStream sharedStream;

		// Token: 0x02001B2B RID: 6955
		private class InternalTraceData : RequestTraceData
		{
			// Token: 0x0600AE2D RID: 44589 RVA: 0x0023AEAF File Offset: 0x002390AF
			public InternalTraceData(RequestTrace trace)
			{
				this.trace = trace;
			}

			// Token: 0x17002BB9 RID: 11193
			// (get) Token: 0x0600AE2E RID: 44590 RVA: 0x0023AEBE File Offset: 0x002390BE
			public override Guid? ActivityId
			{
				get
				{
					return this.trace.ActivityId;
				}
			}

			// Token: 0x17002BBA RID: 11194
			// (get) Token: 0x0600AE2F RID: 44591 RVA: 0x0023AECB File Offset: 0x002390CB
			public override IResource Resource
			{
				get
				{
					return this.trace.Resource;
				}
			}

			// Token: 0x17002BBB RID: 11195
			// (get) Token: 0x0600AE30 RID: 44592 RVA: 0x0023AED8 File Offset: 0x002390D8
			public override Guid SessionId
			{
				get
				{
					return this.trace.SessionId;
				}
			}

			// Token: 0x17002BBC RID: 11196
			// (get) Token: 0x0600AE31 RID: 44593 RVA: 0x0023AEE5 File Offset: 0x002390E5
			public override DateTime Timestamp
			{
				get
				{
					return this.trace.Timestamp;
				}
			}

			// Token: 0x17002BBD RID: 11197
			// (get) Token: 0x0600AE32 RID: 44594 RVA: 0x0023AEF2 File Offset: 0x002390F2
			public override int TraceId
			{
				get
				{
					return this.trace.TraceId;
				}
			}

			// Token: 0x17002BBE RID: 11198
			// (get) Token: 0x0600AE33 RID: 44595 RVA: 0x0023AEFF File Offset: 0x002390FF
			public override string Type
			{
				get
				{
					return this.trace.Type;
				}
			}

			// Token: 0x17002BBF RID: 11199
			// (get) Token: 0x0600AE34 RID: 44596 RVA: 0x0023AF0C File Offset: 0x0023910C
			public override Stream ContentStream
			{
				get
				{
					return this.trace.sharedStream.InputStream;
				}
			}

			// Token: 0x0600AE35 RID: 44597 RVA: 0x0023AF20 File Offset: 0x00239120
			public override IDictionary<string, string> GetMetadata()
			{
				object metadataLock = this.trace.metadataLock;
				IDictionary<string, string> dictionary;
				lock (metadataLock)
				{
					dictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(this.trace.metadata));
				}
				return dictionary;
			}

			// Token: 0x0600AE36 RID: 44598 RVA: 0x0023AF78 File Offset: 0x00239178
			public override void Dispose()
			{
				this.trace.sharedStream.InputStream.Dispose();
			}

			// Token: 0x040059D9 RID: 23001
			private readonly RequestTrace trace;
		}
	}
}
