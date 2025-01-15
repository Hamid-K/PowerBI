using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004B RID: 75
	public interface IPathTranslator
	{
		// Token: 0x06000234 RID: 564
		string PathToInternal(string source);

		// Token: 0x06000235 RID: 565
		string PathToExternal(string source);

		// Token: 0x06000236 RID: 566
		void SetExternalRoot(string path);

		// Token: 0x06000237 RID: 567
		void SetExternalRoot(CatalogItemPath path, int zone);

		// Token: 0x06000238 RID: 568
		Uri GetExternalRoot();

		// Token: 0x06000239 RID: 569
		ExternalItemPath CatalogToExternal(string source);

		// Token: 0x0600023A RID: 570
		ExternalItemPath CatalogToExternal(CatalogItemPath source);

		// Token: 0x0600023B RID: 571
		ExternalItemPath CatalogToExternal(CatalogItemPath source, int externalRootZone);

		// Token: 0x0600023C RID: 572
		string ExternalToCatalog(string source);

		// Token: 0x0600023D RID: 573
		int GetExternalRootZone(ExternalItemPath path);

		// Token: 0x0600023E RID: 574
		string GetPublicUrl(string url, bool noThrow);
	}
}
