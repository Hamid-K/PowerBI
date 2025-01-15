using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000107 RID: 263
	internal static class ODataEntityMaterializerInvoker
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x0002AA7E File Offset: 0x00028C7E
		internal static IEnumerable<T> EnumerateAsElementType<T>(IEnumerable source)
		{
			return ODataEntityMaterializer.EnumerateAsElementType<T>(source);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002AA86 File Offset: 0x00028C86
		internal static List<TTarget> ListAsElementType<T, TTarget>(object materializer, IEnumerable<T> source) where T : TTarget
		{
			return ODataEntityMaterializer.ListAsElementType<T, TTarget>((ODataEntityMaterializer)materializer, source);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002AA94 File Offset: 0x00028C94
		internal static bool ProjectionCheckValueForPathIsNull(object entry, Type expectedType, object path)
		{
			return ODataEntityMaterializer.ProjectionCheckValueForPathIsNull(MaterializerEntry.GetEntry((ODataResource)entry), expectedType, (ProjectionPath)path);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002AAAD File Offset: 0x00028CAD
		internal static IEnumerable ProjectionSelect(object materializer, object entry, Type expectedType, Type resultType, object path, Func<object, object, Type, object> selector)
		{
			return ODataEntityMaterializer.ProjectionSelect((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataResource)entry), expectedType, resultType, (ProjectionPath)path, selector);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002AAD0 File Offset: 0x00028CD0
		internal static object ProjectionGetEntry(object entry, string name)
		{
			return ODataEntityMaterializer.ProjectionGetEntry(MaterializerEntry.GetEntry((ODataResource)entry), name);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002AAE3 File Offset: 0x00028CE3
		internal static object ProjectionInitializeEntity(object materializer, object entry, Type expectedType, Type resultType, string[] properties, Func<object, object, Type, object>[] propertyValues)
		{
			return ODataEntityMaterializer.ProjectionInitializeEntity((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataResource)entry), expectedType, resultType, properties, propertyValues);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002AB01 File Offset: 0x00028D01
		internal static object ProjectionValueForPath(object materializer, object entry, Type expectedType, object path)
		{
			return ((ODataEntityMaterializer)materializer).ProjectionValueForPath(MaterializerEntry.GetEntry((ODataResource)entry), expectedType, (ProjectionPath)path);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002AB20 File Offset: 0x00028D20
		internal static object DirectMaterializePlan(object materializer, object entry, Type expectedEntryType)
		{
			return ODataEntityMaterializer.DirectMaterializePlan((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataResource)entry), expectedEntryType);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002AB39 File Offset: 0x00028D39
		internal static object ShallowMaterializePlan(object materializer, object entry, Type expectedEntryType)
		{
			return ODataEntityMaterializer.ShallowMaterializePlan((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataResource)entry), expectedEntryType);
		}
	}
}
