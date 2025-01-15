using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200026C RID: 620
	internal static class ODataAtomWriterMetadataEpmMergeUtils
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x000486FC File Offset: 0x000468FC
		internal static AtomEntryMetadata MergeCustomAndEpmEntryMetadata(AtomEntryMetadata customEntryMetadata, AtomEntryMetadata epmEntryMetadata, ODataWriterBehavior writerBehavior)
		{
			if (customEntryMetadata != null && writerBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				if (customEntryMetadata.Updated != null)
				{
					customEntryMetadata.UpdatedString = ODataAtomConvert.ToAtomString(customEntryMetadata.Updated.Value);
				}
				if (customEntryMetadata.Published != null)
				{
					customEntryMetadata.PublishedString = ODataAtomConvert.ToAtomString(customEntryMetadata.Published.Value);
				}
			}
			AtomEntryMetadata atomEntryMetadata;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<AtomEntryMetadata>(customEntryMetadata, epmEntryMetadata, out atomEntryMetadata))
			{
				return atomEntryMetadata;
			}
			epmEntryMetadata.Title = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Title, epmEntryMetadata.Title, "Title");
			epmEntryMetadata.Summary = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Summary, epmEntryMetadata.Summary, "Summary");
			epmEntryMetadata.Rights = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Rights, epmEntryMetadata.Rights, "Rights");
			if (writerBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				epmEntryMetadata.PublishedString = ODataAtomWriterMetadataEpmMergeUtils.MergeTextValue(customEntryMetadata.PublishedString, epmEntryMetadata.PublishedString, "PublishedString");
				epmEntryMetadata.UpdatedString = ODataAtomWriterMetadataEpmMergeUtils.MergeTextValue(customEntryMetadata.UpdatedString, epmEntryMetadata.UpdatedString, "UpdatedString");
			}
			else
			{
				epmEntryMetadata.Published = ODataAtomWriterMetadataEpmMergeUtils.MergeDateTimeValue(customEntryMetadata.Published, epmEntryMetadata.Published, "Published");
				epmEntryMetadata.Updated = ODataAtomWriterMetadataEpmMergeUtils.MergeDateTimeValue(customEntryMetadata.Updated, epmEntryMetadata.Updated, "Updated");
			}
			epmEntryMetadata.Authors = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomPersonMetadata>(customEntryMetadata.Authors, epmEntryMetadata.Authors);
			epmEntryMetadata.Contributors = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomPersonMetadata>(customEntryMetadata.Contributors, epmEntryMetadata.Contributors);
			epmEntryMetadata.Categories = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomCategoryMetadata>(customEntryMetadata.Categories, epmEntryMetadata.Categories);
			epmEntryMetadata.Links = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomLinkMetadata>(customEntryMetadata.Links, epmEntryMetadata.Links);
			epmEntryMetadata.Source = customEntryMetadata.Source;
			return epmEntryMetadata;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000488B0 File Offset: 0x00046AB0
		private static IEnumerable<T> MergeSyndicationMapping<T>(IEnumerable<T> customValues, IEnumerable<T> epmValues)
		{
			IEnumerable<T> enumerable;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<IEnumerable<T>>(customValues, epmValues, out enumerable))
			{
				return enumerable;
			}
			List<T> list = (List<T>)epmValues;
			foreach (T t in customValues)
			{
				list.Add(t);
			}
			return list;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00048910 File Offset: 0x00046B10
		private static AtomTextConstruct MergeAtomTextValue(AtomTextConstruct customValue, AtomTextConstruct epmValue, string propertyName)
		{
			AtomTextConstruct atomTextConstruct;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<AtomTextConstruct>(customValue, epmValue, out atomTextConstruct))
			{
				return atomTextConstruct;
			}
			if (customValue.Kind != epmValue.Kind)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextKindConflict(propertyName, customValue.Kind.ToString(), epmValue.Kind.ToString()));
			}
			if (string.CompareOrdinal(customValue.Text, epmValue.Text) != 0)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue.Text, epmValue.Text));
			}
			return epmValue;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00048990 File Offset: 0x00046B90
		private static string MergeTextValue(string customValue, string epmValue, string propertyName)
		{
			string text;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<string>(customValue, epmValue, out text))
			{
				return text;
			}
			if (string.CompareOrdinal(customValue, epmValue) != 0)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue, epmValue));
			}
			return epmValue;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000489C4 File Offset: 0x00046BC4
		private static DateTimeOffset? MergeDateTimeValue(DateTimeOffset? customValue, DateTimeOffset? epmValue, string propertyName)
		{
			DateTimeOffset? dateTimeOffset;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<DateTimeOffset>(customValue, epmValue, out dateTimeOffset))
			{
				return dateTimeOffset;
			}
			if (customValue != epmValue)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue.ToString(), epmValue.ToString()));
			}
			return epmValue;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00048A3D File Offset: 0x00046C3D
		private static bool TryMergeIfNull<T>(T customValue, T epmValue, out T result) where T : class
		{
			if (customValue == null)
			{
				result = epmValue;
				return true;
			}
			if (epmValue == null)
			{
				result = customValue;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00048A69 File Offset: 0x00046C69
		private static bool TryMergeIfNull<T>(T? customValue, T? epmValue, out T? result) where T : struct
		{
			if (customValue == null)
			{
				result = epmValue;
				return true;
			}
			if (epmValue == null)
			{
				result = customValue;
				return true;
			}
			result = default(T?);
			return false;
		}
	}
}
