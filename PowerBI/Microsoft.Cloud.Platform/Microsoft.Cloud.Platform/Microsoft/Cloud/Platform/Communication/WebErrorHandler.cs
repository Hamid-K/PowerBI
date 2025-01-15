using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004DA RID: 1242
	public class WebErrorHandler : IErrorHandler
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x00085CE3 File Offset: 0x00083EE3
		public WebErrorHandler(Func<Exception, FaultInformation> provideFaultInformationForException)
			: this(provideFaultInformationForException, null)
		{
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00085CED File Offset: 0x00083EED
		public WebErrorHandler(Func<Exception, FaultInformation> provideFaultInformationForException, XmlObjectSerializer serializer)
		{
			this.m_provideFaultInformationForException = provideFaultInformationForException;
			this.m_serializer = serializer ?? new NetDataContractSerializer();
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00085D0C File Offset: 0x00083F0C
		public bool HandleError(Exception error)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "The server threw an exception: {0}. ", new object[] { error.ToString() });
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, error);
			return false;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00085D38 File Offset: 0x00083F38
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			FaultInformation faultInformation = this.m_provideFaultInformationForException(error);
			MessageFault messageFault = MessageFault.CreateFault(faultInformation.FaultErrorCode, faultInformation.FaultString, faultInformation.FaultDetail, this.m_serializer);
			fault = Message.CreateMessage(version, messageFault, null);
		}

		// Token: 0x04000D5A RID: 3418
		private Func<Exception, FaultInformation> m_provideFaultInformationForException;

		// Token: 0x04000D5B RID: 3419
		private XmlObjectSerializer m_serializer;
	}
}
