using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200004C RID: 76
	internal static class ODataPathExtensions
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public static IEdmTypeReference EdmType(this ODataPath path)
		{
			return path.LastSegment.EdmType.ToTypeReference();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007CFA File Offset: 0x00005EFA
		public static IEdmEntitySet EntitySet(this ODataPath path)
		{
			return path.LastSegment.Translate<IEdmEntitySet>(new DetermineEntitySetTranslator());
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007D0C File Offset: 0x00005F0C
		public static bool IsCollection(this ODataPath path)
		{
			return path.LastSegment.Translate<bool>(new IsCollectionTranslator());
		}
	}
}
