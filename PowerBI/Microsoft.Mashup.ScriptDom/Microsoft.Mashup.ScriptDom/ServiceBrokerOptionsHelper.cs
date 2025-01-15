using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200016C RID: 364
	internal class ServiceBrokerOptionsHelper : OptionsHelper<ServiceBrokerOption>
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x0015C8A4 File Offset: 0x0015AAA4
		private ServiceBrokerOptionsHelper()
		{
			base.AddOptionMapping(ServiceBrokerOption.NewBroker, "NEW_BROKER");
			base.AddOptionMapping(ServiceBrokerOption.EnableBroker, "ENABLE_BROKER");
			base.AddOptionMapping(ServiceBrokerOption.ErrorBrokerConversations, "ERROR_BROKER_CONVERSATIONS");
		}

		// Token: 0x040018CC RID: 6348
		internal static readonly ServiceBrokerOptionsHelper Instance = new ServiceBrokerOptionsHelper();
	}
}
