using System;
using System.ServiceModel.Channels;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000487 RID: 1159
	public static class BindingFactory
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x00080BE8 File Offset: 0x0007EDE8
		public static IBindingData CreateBinding(EndpointInfo endpointInfo)
		{
			IBindingData bindingData = null;
			switch (endpointInfo.BindingType)
			{
			case BindingType.Tcp:
				bindingData = new NetTcpBindingData(endpointInfo);
				break;
			case BindingType.BasicHttp:
				bindingData = new BasicHttpBindingData(endpointInfo);
				break;
			case BindingType.WsHttp:
				bindingData = new WsHttpBindingData(endpointInfo);
				break;
			case BindingType.WebHttp:
				bindingData = new WebHttpBindingData(endpointInfo);
				break;
			case BindingType.HttpsWithSoap12:
				bindingData = new HttpsWithSoap12BindingData(endpointInfo);
				break;
			case BindingType.NamedPipe:
				bindingData = new NamedPipeBindingData(endpointInfo);
				break;
			}
			BindingFactory.UpdateBindingWithConfigurationValues(bindingData.Binding, endpointInfo);
			return bindingData;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00080C5F File Offset: 0x0007EE5F
		private static void UpdateBindingWithConfigurationValues(Binding binding, EndpointInfo endpointInfo)
		{
			binding.SendTimeout = endpointInfo.SendTimeout;
			binding.ReceiveTimeout = endpointInfo.ReceiveTimeout;
			binding.OpenTimeout = endpointInfo.OpenTimeout;
			binding.CloseTimeout = endpointInfo.CloseTimeout;
		}
	}
}
