using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000011 RID: 17
	internal static class ISectionDocumentExtensions
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000031D0 File Offset: 0x000013D0
		public static bool IsSharedResource(this IList<ISectionDocument> documents, string resourceName)
		{
			return documents.GetSharedResourceNames().Any((string sectionMemberName) => string.Equals(sectionMemberName, resourceName, StringComparison.Ordinal));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003204 File Offset: 0x00001404
		public static IEnumerable<string> GetSharedResourceNames(this IList<ISectionDocument> documents)
		{
			return from m in documents.SelectMany((ISectionDocument d) => d.Section.Members)
				where m.Export
				select m.Name.Name;
		}
	}
}
