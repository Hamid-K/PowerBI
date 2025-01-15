using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000122 RID: 290
	internal sealed class RenderStream : RenderFromSession
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B427 File Offset: 0x00029627
		public RenderStream(ReportExecutionBase execContext)
			: base(execContext)
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B430 File Offset: 0x00029630
		public override ExecutionResult ExecuteStrategy(out OnDemandProcessingResult processingResult)
		{
			processingResult = null;
			base.VerifySession();
			this.PrepareForExecution();
			RSTrace.CatalogTrace.Assert(base.SnapshotManager.ChunkTargetSnapshot != null, "ChunkTargetSnapshot");
			RSStream rsstream = this.TryGetImageFromCache();
			if (rsstream == null)
			{
				base.SnapshotManager.SnapshotUpdated += delegate(object sender, SnapshotUpdatedEventArgs e)
				{
					this.m_sessionSnapshotUpdated = true;
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Snapshot '{0}' updated to '{1}' during stream rendering", new object[]
					{
						e.OldSnapshot.ToString(),
						e.NewSnapshot.ToString()
					});
				};
				base.LockSession();
				rsstream = this.GetImage(out processingResult);
				if (rsstream == null)
				{
					throw new StreamNotFoundException(this.ImageId);
				}
				DateTime dateTime = base.ExecutionContext.RequestInfo.Session.SessionReport.SnapshotExpirationDateTime;
				if (dateTime == DateTime.MinValue)
				{
					dateTime = DateTime.Now.AddSeconds((double)Global.SessionTimeoutSeconds);
				}
				ReportSnapshot chunkTargetSnapshot = base.SnapshotManager.ChunkTargetSnapshot;
				if (rsstream != null && dateTime != DateTime.MinValue)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Caching secondary stream '{0}' for snapshot '{1}'", new object[] { this.ImageId, chunkTargetSnapshot.SnapshotDataID });
					RSLocalCacheManager.Current.CacheSecondaryStream(base.ExecutionContext.RequestInfo.ReportContext, rsstream, dateTime, chunkTargetSnapshot);
				}
			}
			ExecutionResult executionResult = new ExecutionResult();
			string text;
			if (ImageMimeTypeDetector.TryDetectMimeType(rsstream, out text))
			{
				rsstream.MimeType = text;
			}
			executionResult.OutputStream = rsstream;
			executionResult.EffectiveParameters = null;
			executionResult.StreamIds = null;
			executionResult.Warnings = null;
			return executionResult;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002B57F File Offset: 0x0002977F
		public bool SessionSnapshotUpdated
		{
			get
			{
				return this.m_sessionSnapshotUpdated;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000053DC File Offset: 0x000035DC
		public override SessionReportItem.SaveAction SessionSaveFlags
		{
			get
			{
				return SessionReportItem.SaveAction.SaveSession;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002A6CA File Offset: 0x000288CA
		protected override RenderStrategyBase.ProcessOrRender ProcessRenderRequirements
		{
			get
			{
				return RenderStrategyBase.ProcessOrRender.Render | RenderStrategyBase.ProcessOrRender.Process;
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B587 File Offset: 0x00029787
		protected override void SetSourceSnapshot()
		{
			base.OriginalSnapshot = base.ExecutionContext.RequestInfo.Session.SessionReport.Report.SnapshotData;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected override bool CheckNeedsReprocessing()
		{
			return false;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B5AE File Offset: 0x000297AE
		protected override void CallProcessingAndRendering(ProcessingContext pc, RenderingContext rc, out OnDemandProcessingResult result)
		{
			RSTrace.CatalogTrace.Assert(pc.ChunkFactory == base.SnapshotManager, "pc.ChunkFactory == SnapshotManger");
			result = base.ExecutionContext.ProcessingEngine.RenderSnapshotStream(this.ImageId, rc, pc);
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002B5E7 File Offset: 0x000297E7
		private string ImageId
		{
			get
			{
				return base.ExecutionContext.RequestInfo.ReportContext.RSRequestParameters.ImageIDParamValue;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B604 File Offset: 0x00029804
		private RSStream GetImage(out OnDemandProcessingResult processingResult)
		{
			RSStream rsstream2;
			using (ISnapshotTransaction snapshotTransaction = base.SnapshotManager.EnterTransactionContext())
			{
				RSStream rsstream = this.TryGetImageFromSession();
				if (rsstream != null)
				{
					processingResult = null;
				}
				else
				{
					using (base.ExecutionContext.DataProvider.EnterStorageContext(null))
					{
						base.SetPaginationDataFromSnapshot(base.OriginalSnapshot);
						processingResult = this.GetImageByRendering();
						if (processingResult == null)
						{
							throw new InternalCatalogException("Could not render image.");
						}
						if (processingResult.SnapshotChanged)
						{
							processingResult.Save();
						}
					}
					rsstream = base.ExecutionContext.DataProvider.StreamManager.GetStream(this.ImageId);
				}
				snapshotTransaction.Commit();
				rsstream2 = rsstream;
			}
			return rsstream2;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B6D4 File Offset: 0x000298D4
		private RSStream TryGetImageFromSession()
		{
			return base.ExecutionContext.RequestInfo.Session.SessionReport.GetImage(this.ImageId);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002B6F8 File Offset: 0x000298F8
		private RSStream TryGetImageFromCache()
		{
			ReportRenderingResult cachedResult = RSLocalCacheManager.Current.GetCachedResult(base.ExecutionContext.RequestInfo.ReportContext, this.ImageId, base.SnapshotManager.ChunkTargetSnapshot);
			RSStream rsstream;
			if (cachedResult != null && cachedResult.Stream != null)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Image from cache : " + base.ExecutionContext.RequestInfo.ReportContext.OriginalItemPath);
				rsstream = cachedResult.Stream;
			}
			else
			{
				rsstream = null;
			}
			return rsstream;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002B774 File Offset: 0x00029974
		private OnDemandProcessingResult GetImageByRendering()
		{
			RenderingContext renderingContext = base.GenerateRenderingContext();
			base.ExecutionContext.DataProvider.StreamManager.NeedCacheableStreams = true;
			ISubreportRetrieval subreportRetrieval = null;
			ProcessingContext processingContext;
			OnDemandProcessingResult onDemandProcessingResult;
			using (base.BeginProcessingContext(out processingContext, out subreportRetrieval))
			{
				try
				{
					this.CallProcessingAndRendering(processingContext, renderingContext, out onDemandProcessingResult);
				}
				finally
				{
					if (subreportRetrieval != null)
					{
						subreportRetrieval.Dispose();
					}
				}
			}
			RSTrace.CatalogTrace.Assert(onDemandProcessingResult != null, "processingResult");
			return onDemandProcessingResult;
		}

		// Token: 0x040004BD RID: 1213
		private bool m_sessionSnapshotUpdated;
	}
}
