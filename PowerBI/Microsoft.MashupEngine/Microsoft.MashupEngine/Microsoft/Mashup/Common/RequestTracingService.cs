using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C1A RID: 7194
	public static class RequestTracingService
	{
		// Token: 0x0600B391 RID: 45969 RVA: 0x00247D8C File Offset: 0x00245F8C
		public static IRequestTracingService GetService(IEngineHost host)
		{
			if (host == null)
			{
				return RequestTracingService.dummyTracingService;
			}
			return host.QueryService<IRequestTracingService>() ?? RequestTracingService.dummyTracingService;
		}

		// Token: 0x0600B392 RID: 45970 RVA: 0x00247DA6 File Offset: 0x00245FA6
		public static bool IsTracePermitted(IEngineHost host, IResource resource)
		{
			return RequestTracingService.GetService(host).IsTraceEnabled(resource);
		}

		// Token: 0x0600B393 RID: 45971 RVA: 0x00247DB4 File Offset: 0x00245FB4
		public static IRequestTrace CreateTrace(IEngineHost host, IResource resource, Guid sessionId, string type)
		{
			return RequestTracingService.GetService(host).CreateTrace(host.GetEvaluationConstants().GetActivityId(), resource, sessionId, type);
		}

		// Token: 0x04005B90 RID: 23440
		public static IRequestTrace DroppedTrace = RequestTracingService.DummyTrace.Instance;

		// Token: 0x04005B91 RID: 23441
		private static IRequestTracingService dummyTracingService = new RequestTracingService.DummyTracingService();

		// Token: 0x02001C1B RID: 7195
		private class DummyTracingService : IRequestTracingService
		{
			// Token: 0x0600B395 RID: 45973 RVA: 0x00247DE5 File Offset: 0x00245FE5
			public IRequestTrace CreateTrace(Guid? activityId, IResource resource, Guid sessionId, string type)
			{
				return RequestTracingService.DummyTrace.Instance;
			}

			// Token: 0x0600B396 RID: 45974 RVA: 0x00002105 File Offset: 0x00000305
			public bool IsTraceEnabled(IResource resource)
			{
				return false;
			}
		}

		// Token: 0x02001C1C RID: 7196
		private sealed class DummyTrace : IRequestTrace, IDisposable
		{
			// Token: 0x17002CF8 RID: 11512
			// (get) Token: 0x0600B399 RID: 45977 RVA: 0x00002105 File Offset: 0x00000305
			public int TraceId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002CF9 RID: 11513
			// (get) Token: 0x0600B39A RID: 45978 RVA: 0x00247DEC File Offset: 0x00245FEC
			public Guid? ActivityId
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002CFA RID: 11514
			// (get) Token: 0x0600B39B RID: 45979 RVA: 0x000020FA File Offset: 0x000002FA
			public IResource Resource
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002CFB RID: 11515
			// (get) Token: 0x0600B39C RID: 45980 RVA: 0x00247E02 File Offset: 0x00246002
			public Guid SessionId
			{
				get
				{
					return Guid.Empty;
				}
			}

			// Token: 0x17002CFC RID: 11516
			// (get) Token: 0x0600B39D RID: 45981 RVA: 0x00247E09 File Offset: 0x00246009
			public string Type
			{
				get
				{
					return "dummy";
				}
			}

			// Token: 0x17002CFD RID: 11517
			// (get) Token: 0x0600B39E RID: 45982 RVA: 0x00247E10 File Offset: 0x00246010
			public DateTime Timestamp
			{
				get
				{
					return DateTime.MaxValue;
				}
			}

			// Token: 0x0600B39F RID: 45983 RVA: 0x0000336E File Offset: 0x0000156E
			public void AddMetadata(string name, string value)
			{
			}

			// Token: 0x0600B3A0 RID: 45984 RVA: 0x00247E17 File Offset: 0x00246017
			public Stream GetContentStream()
			{
				return RequestTracingService.DummyStream.Instance;
			}

			// Token: 0x0600B3A1 RID: 45985 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005B92 RID: 23442
			public static readonly RequestTracingService.DummyTrace Instance = new RequestTracingService.DummyTrace();
		}

		// Token: 0x02001C1D RID: 7197
		private sealed class DummyStream : Stream
		{
			// Token: 0x17002CFE RID: 11518
			// (get) Token: 0x0600B3A3 RID: 45987 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002CFF RID: 11519
			// (get) Token: 0x0600B3A4 RID: 45988 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002D00 RID: 11520
			// (get) Token: 0x0600B3A5 RID: 45989 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002D01 RID: 11521
			// (get) Token: 0x0600B3A6 RID: 45990 RVA: 0x001819C2 File Offset: 0x0017FBC2
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17002D02 RID: 11522
			// (get) Token: 0x0600B3A7 RID: 45991 RVA: 0x000033E7 File Offset: 0x000015E7
			// (set) Token: 0x0600B3A8 RID: 45992 RVA: 0x000033E7 File Offset: 0x000015E7
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

			// Token: 0x0600B3A9 RID: 45993 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Flush()
			{
			}

			// Token: 0x0600B3AA RID: 45994 RVA: 0x00002105 File Offset: 0x00000305
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x0600B3AB RID: 45995 RVA: 0x000033E7 File Offset: 0x000015E7
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600B3AC RID: 45996 RVA: 0x0000336E File Offset: 0x0000156E
			public override void SetLength(long value)
			{
			}

			// Token: 0x0600B3AD RID: 45997 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x04005B93 RID: 23443
			public static readonly RequestTracingService.DummyStream Instance = new RequestTracingService.DummyStream();
		}
	}
}
