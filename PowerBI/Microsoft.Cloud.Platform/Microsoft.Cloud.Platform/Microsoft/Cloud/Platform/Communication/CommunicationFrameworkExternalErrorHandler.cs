using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BF RID: 1215
	internal class CommunicationFrameworkExternalErrorHandler : IErrorHandler
	{
		// Token: 0x06002520 RID: 9504 RVA: 0x00084183 File Offset: 0x00082383
		public bool HandleError(Exception error)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "The service threw an exception: {0}.", new object[] { error });
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, error);
			return false;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
		}
	}
}
