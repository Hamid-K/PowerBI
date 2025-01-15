using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ProcessingRenderingCommon;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A7 RID: 167
	public static class ProcessingContext
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000101A6 File Offset: 0x0000E3A6
		public static string RsAppDomainManagerTypeName
		{
			get
			{
				return "Microsoft.ReportingServices.AppDomainManager.RsAppDomainManager, ReportingServicesAppDomainManager, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000101AD File Offset: 0x0000E3AD
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public static IProcessRenderingConfiguration Configuration { get; set; }

		// Token: 0x06000520 RID: 1312 RVA: 0x000101BC File Offset: 0x0000E3BC
		public static void LoadImageResource(string imageName, ResourceManager resourceManager, CreateAndRegisterStream createStreamCallback)
		{
			Stream stream = createStreamCallback(imageName, "gif", null, "image/gif", false, StreamOper.CreateAndRegister);
			Image image = (Image)resourceManager.GetObject(imageName);
			object obj = ProcessingContext.lockObj;
			lock (obj)
			{
				image.Save(stream, image.RawFormat);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00010224 File Offset: 0x0000E424
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00010230 File Offset: 0x0000E430
		public static RunningJobContext JobContext
		{
			get
			{
				return ProcessingContext._jobContext.Value;
			}
			set
			{
				ProcessingContext._jobContext.Value = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001023D File Offset: 0x0000E43D
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00010249 File Offset: 0x0000E449
		public static ThreadJobContext ThreadContext
		{
			get
			{
				return ProcessingContext._threadContext.Value;
			}
			set
			{
				ProcessingContext._threadContext.Value = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010256 File Offset: 0x0000E456
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010262 File Offset: 0x0000E462
		public static RequestContext ReqContext
		{
			get
			{
				return ProcessingContext._reqContext.Value;
			}
			set
			{
				ProcessingContext._reqContext.Value = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001026F File Offset: 0x0000E46F
		public static IServiceInstanceContext ServiceInstanceContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00010272 File Offset: 0x0000E472
		public static void InitializeRequestContext(RequestContext context)
		{
			RSTrace.RunningJobsTrace.Assert(context != null, "context != null");
			ProcessingContext._reqContext.Value = context;
			context.RequestTimer.StartTimer();
			RequestCache.InitializeForRequest();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000102A2 File Offset: 0x0000E4A2
		public static void EndRequestContext()
		{
			RequestCache.ReleaseReferenceAndDetach();
			if (ProcessingContext.ReqContext != null)
			{
				ProcessingContext._reqContext.Value = null;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000102BB File Offset: 0x0000E4BB
		public static void DelayUntilResourcesAvailableBlocking()
		{
			RequestContext reqContext = ProcessingContext.ReqContext;
			if (reqContext == null)
			{
				return;
			}
			IResourceTicket resourceTicket = reqContext.ResourceTicket;
			if (resourceTicket == null)
			{
				return;
			}
			resourceTicket.DelayUntilResourcesAvailableBlocking();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public static async Task DelayUntilResourcesAvailableAsync()
		{
			await ProcessingContext.DelayUntilResourcesAvailableAsync(CancellationToken.None);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00010314 File Offset: 0x0000E514
		public static async Task DelayUntilResourcesAvailableAsync(CancellationToken cancellationToken)
		{
			if (ProcessingContext.ReqContext != null && ProcessingContext.ReqContext.ResourceTicket != null)
			{
				await ProcessingContext.ReqContext.ResourceTicket.DelayUntilResourcesAvailableAsync(cancellationToken);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00010357 File Offset: 0x0000E557
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001035E File Offset: 0x0000E55E
		public static RSTrace JobsTracer
		{
			get
			{
				return ProcessingContext.m_jobsTracer;
			}
			set
			{
				ProcessingContext.m_jobsTracer = value;
			}
		}

		// Token: 0x040002F6 RID: 758
		private const string m_RsAppDomainManagerTypeName = "Microsoft.ReportingServices.AppDomainManager.RsAppDomainManager, ReportingServicesAppDomainManager, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

		// Token: 0x040002F7 RID: 759
		private static AsyncLocal<RunningJobContext> _jobContext = new AsyncLocal<RunningJobContext>();

		// Token: 0x040002F8 RID: 760
		private static AsyncLocal<ThreadJobContext> _threadContext = new AsyncLocal<ThreadJobContext>();

		// Token: 0x040002F9 RID: 761
		private static AsyncLocal<RequestContext> _reqContext = new AsyncLocal<RequestContext>();

		// Token: 0x040002FB RID: 763
		private const string m_gifMimeType = "image/gif";

		// Token: 0x040002FC RID: 764
		private static object lockObj = new object();

		// Token: 0x040002FD RID: 765
		private static RSTrace m_jobsTracer = RSTrace.RunningJobsTrace;
	}
}
