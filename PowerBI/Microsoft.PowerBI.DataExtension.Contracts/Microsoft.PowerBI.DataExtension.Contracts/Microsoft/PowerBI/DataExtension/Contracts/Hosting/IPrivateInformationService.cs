using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Hosting
{
	// Token: 0x0200000B RID: 11
	public interface IPrivateInformationService
	{
		// Token: 0x0600002D RID: 45
		string MarkAsPrivate(string message);

		// Token: 0x0600002E RID: 46
		string RemovePrivateAndInternalMarkup(string message);
	}
}
