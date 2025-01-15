using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048C RID: 1164
	public class NamedPipeBindingData : IBindingData
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x00080FD4 File Offset: 0x0007F1D4
		public NamedPipeBindingData([NotNull] EndpointInfo endpointInfo)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EndpointInfo>(endpointInfo, "endpointInfo");
			string text = endpointInfo.SecurityMode.ToString();
			NetNamedPipeSecurityMode netNamedPipeSecurityMode;
			ExtendedDiagnostics.EnsureOperation(Enum.TryParse<NetNamedPipeSecurityMode>(text, out netNamedPipeSecurityMode), "Invalid endpoint security configuration in '{0}'- could not parse {1} as {2}".FormatWithInvariantCulture(new object[]
			{
				endpointInfo.SpnIdentityName,
				text,
				typeof(NetNamedPipeSecurity)
			}));
			this.Binding = new NetNamedPipeBinding(netNamedPipeSecurityMode)
			{
				ReaderQuotas = 
				{
					MaxDepth = endpointInfo.MaxDepth,
					MaxStringContentLength = endpointInfo.MaxStringContentLength,
					MaxArrayLength = endpointInfo.MaxArrayLength
				},
				MaxReceivedMessageSize = endpointInfo.MaxMessageSize,
				MaxBufferSize = endpointInfo.MaxMessageSize.ToInt32(),
				TransferMode = endpointInfo.TransferDataMode,
				MaxBufferPoolSize = endpointInfo.MaxBufferPoolSize
			};
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000810B2 File Offset: 0x0007F2B2
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000810BA File Offset: 0x0007F2BA
		public Binding Binding { get; private set; }

		// Token: 0x060023F4 RID: 9204 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
		}
	}
}
