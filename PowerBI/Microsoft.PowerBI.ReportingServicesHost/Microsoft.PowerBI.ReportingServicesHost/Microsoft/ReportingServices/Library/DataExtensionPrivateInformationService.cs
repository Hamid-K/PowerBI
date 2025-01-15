using System;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000013 RID: 19
	internal sealed class DataExtensionPrivateInformationService : IPrivateInformationService
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000027C3 File Offset: 0x000009C3
		private DataExtensionPrivateInformationService()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027CB File Offset: 0x000009CB
		public string MarkAsPrivate(string message)
		{
			return message.MarkAsCustomerContent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027D3 File Offset: 0x000009D3
		public string RemovePrivateAndInternalMarkup(string message)
		{
			return message.RemovePrivateAndInternalMarkup();
		}

		// Token: 0x04000058 RID: 88
		public static readonly DataExtensionPrivateInformationService Instance = new DataExtensionPrivateInformationService();
	}
}
