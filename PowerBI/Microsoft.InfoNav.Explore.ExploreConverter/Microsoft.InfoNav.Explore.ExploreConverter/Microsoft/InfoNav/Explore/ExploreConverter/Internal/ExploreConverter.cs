using System;
using System.IO;
using Microsoft.PowerBI.ExplorationContracts;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000A RID: 10
	internal static class ExploreConverter
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002424 File Offset: 0x00000624
		internal static Exploration Convert(Stream documentStream, ExploreConverterContext context)
		{
			PVDocumentRoot pvdocumentRoot = JsonToPVDocumentConverter.ParseFromJson(documentStream);
			Exploration exploration = PVDocumentToExplorationConverter.Convert(pvdocumentRoot.Document, context);
			exploration.CompatibilityInfo = ExploreCompatibilityChecker.VerifyPVDocumentCompatibilityForExploration(pvdocumentRoot.Document);
			return exploration;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002458 File Offset: 0x00000658
		internal static Exploration ConvertFromRdlxStream(Stream rdlxStream, ExploreConverterContext context)
		{
			PVDocumentRoot pvdocumentRoot = RdlxToDocumentConverter.ConvertRdlxToDocument(rdlxStream);
			Exploration exploration = PVDocumentToExplorationConverter.Convert(pvdocumentRoot.Document, context);
			exploration.CompatibilityInfo = ExploreCompatibilityChecker.VerifyPVDocumentCompatibilityForExploration(pvdocumentRoot.Document);
			return exploration;
		}
	}
}
