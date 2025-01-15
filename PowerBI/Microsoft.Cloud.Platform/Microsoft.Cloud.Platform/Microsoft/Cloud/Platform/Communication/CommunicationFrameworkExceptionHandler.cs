using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C0 RID: 1216
	internal class CommunicationFrameworkExceptionHandler : ExceptionHandler
	{
		// Token: 0x06002523 RID: 9507 RVA: 0x000841A8 File Offset: 0x000823A8
		public override bool HandleException(Exception exception)
		{
			Type type = exception.GetType();
			if (typeof(CommunicationException).IsAssignableFrom(type) || typeof(CommunicationFrameworkException).IsAssignableFrom(type))
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "NOT terminating the process after receiving the following exception: '{0}'", new object[] { exception });
				return true;
			}
			return false;
		}
	}
}
