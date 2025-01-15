using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000181 RID: 385
	public abstract class ContextMemberProxy<T> : IDisposable
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002281D File Offset: 0x00020A1D
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00022825 File Offset: 0x00020A25
		protected IDisposable ContextMember { get; set; }

		// Token: 0x060009FE RID: 2558 RVA: 0x0002282E File Offset: 0x00020A2E
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00022838 File Offset: 0x00020A38
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Verbose))
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SyncActivity before dispose: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
				}
				this.ContextMember.Dispose();
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Verbose))
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SyncActivity after dispose: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
				}
			}
		}
	}
}
