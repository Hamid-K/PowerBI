using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000174 RID: 372
	public class SafeWarmedUpObject : IDisposable
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00021DE1 File Offset: 0x0001FFE1
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00021DE9 File Offset: 0x0001FFE9
		public ISafeWarmedUpObjectMarshaler SafeWarmedUpObjectMarshaler { get; private set; }

		// Token: 0x060009C4 RID: 2500 RVA: 0x00021DF2 File Offset: 0x0001FFF2
		public SafeWarmedUpObject(ITraceSource tracer, SafeWarmedUpMultiUsageAppDomain multiUsageAppDomain, ISafeWarmedUpObjectMarshaler safeWarmedUpObjectMarshaler)
		{
			this.m_tracer = tracer;
			this.m_multiUsageAppDomain = multiUsageAppDomain;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00021E08 File Offset: 0x00020008
		public bool InitializeMarshaler(object creationData)
		{
			if (this.SafeWarmedUpObjectMarshaler != null)
			{
				throw new InvalidOperationException("SafeWarmedUpObjectMarshaler already initialized");
			}
			this.SafeWarmedUpObjectMarshaler = this.m_multiUsageAppDomain.GetNewMarshaler(creationData);
			return this.SafeWarmedUpObjectMarshaler != null;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00021E38 File Offset: 0x00020038
		public void Dispose()
		{
			if (this.SafeWarmedUpObjectMarshaler != null)
			{
				ISafeWarmedUpObjectMarshaler marshalerToDispose = this.SafeWarmedUpObjectMarshaler;
				this.SafeWarmedUpObjectMarshaler = null;
				Exception exception = null;
				UtilsContext.Current.RunWithClearContext(delegate
				{
					try
					{
						marshalerToDispose.Dispose();
					}
					catch (Exception ex) when (!ex.IsFatal())
					{
						exception = ex;
					}
				});
				if (exception != null)
				{
					this.m_tracer.TraceError(string.Format("SafeWarmedUpObjectManager Marshaler.Dispose() in domain {0} failed with exception {1}", this.m_multiUsageAppDomain.Identification, exception.Message));
				}
			}
			if (this.m_multiUsageAppDomain != null)
			{
				SafeWarmedUpMultiUsageAppDomain multiUsageAppDomain = this.m_multiUsageAppDomain;
				this.m_multiUsageAppDomain = null;
				multiUsageAppDomain.Release();
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00021ED0 File Offset: 0x000200D0
		public SafeWarmedUpObjectManager.ExecuteStatus ExecuteFunction(int operationId, out string result, params string[] args)
		{
			string localResult = null;
			SafeWarmedUpObjectManager.ExecuteStatus status = SafeWarmedUpObjectManager.ExecuteStatus.Failed;
			UtilsContext.Current.RunWithClearContext(delegate
			{
				status = this.SafeWarmedUpObjectMarshaler.ExecuteFunction(operationId, out localResult, args);
			});
			result = localResult;
			return status;
		}

		// Token: 0x040003C1 RID: 961
		private SafeWarmedUpMultiUsageAppDomain m_multiUsageAppDomain;

		// Token: 0x040003C2 RID: 962
		private readonly ITraceSource m_tracer;
	}
}
