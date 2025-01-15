using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E24 RID: 7716
	public static class IPartitionedDocumentExtensions
	{
		// Token: 0x0600BE21 RID: 48673 RVA: 0x0026746C File Offset: 0x0026566C
		public static IPartitionedDocument AddExpressionPartition(this IPartitionedDocument document, IEngine engine, string expression, out IPartitionKey partitionKey)
		{
			Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
			IPackageSectionConfig packageSectionConfig = null;
			foreach (string text in document.Package.SectionNames)
			{
				IPackageSection section = document.Package.GetSection(text);
				dictionary.Add(text, section);
				packageSectionConfig = IPartitionedDocumentExtensions.Merge(packageSectionConfig, section.Config);
			}
			expression = expression.Trim();
			string text2 = "Expression";
			int num = 1;
			while (dictionary.ContainsKey(text2))
			{
				text2 = "Expression" + num.ToString();
				num++;
			}
			string text3 = "section " + text2 + "; Expression = ";
			string text4 = text3 + expression + ";";
			packageSectionConfig = packageSectionConfig ?? new PackageSectionConfig(null, null, null, null, null);
			IPackageSection packageSection = new PackageSection(packageSectionConfig, text2, SegmentedString.New(text4));
			dictionary.Add(text2, packageSection);
			document = new Package(dictionary).PartitionedDocument(document.PartitioningScheme, engine);
			int num2;
			partitionKey = document.GetPartitionKeyAndOffset(text2, text3.Length, expression.Length, out num2);
			if (document.PartitioningScheme == PartitioningScheme.MemberLet1)
			{
				partitionKey = ((IMemberLetPartitionedDocument)document).GetSpecificPartitionKey((IMemberLetPartitionKey)partitionKey);
			}
			return document;
		}

		// Token: 0x0600BE22 RID: 48674 RVA: 0x002675B4 File Offset: 0x002657B4
		public static ISectionDocument GetSectionDocument(this IPartitionedDocument document, string sectionName)
		{
			MemberLetPartitionedDocument memberLetPartitionedDocument = document as MemberLetPartitionedDocument;
			if (memberLetPartitionedDocument != null)
			{
				return memberLetPartitionedDocument.GetSectionDocument(sectionName);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600BE23 RID: 48675 RVA: 0x002675D8 File Offset: 0x002657D8
		private static IPackageSectionConfig Merge(IPackageSectionConfig config1, IPackageSectionConfig config2)
		{
			if (config1 == null)
			{
				return config2;
			}
			if (config2 == null)
			{
				return config1;
			}
			return new PackageSectionConfig(IPartitionedDocumentExtensions.Merge(config1.Culture, config2.Culture), IPartitionedDocumentExtensions.Merge(config1.TimeZone, config2.TimeZone), IPartitionedDocumentExtensions.Merge(config1.Version, config2.Version), IPartitionedDocumentExtensions.Merge(config1.MinVersion, config2.MinVersion), IPartitionedDocumentExtensions.MergeDependencies(config1.Dependencies, config2.Dependencies));
		}

		// Token: 0x0600BE24 RID: 48676 RVA: 0x00267649 File Offset: 0x00265849
		private static string Merge(string text1, string text2)
		{
			if (text1 == null || text2 == null || !text1.Equals(text2, StringComparison.Ordinal))
			{
				return null;
			}
			return text1;
		}

		// Token: 0x0600BE25 RID: 48677 RVA: 0x00267660 File Offset: 0x00265860
		private static KeyValuePair<string, VersionRange>[] MergeDependencies(KeyValuePair<string, VersionRange>[] list1, KeyValuePair<string, VersionRange>[] list2)
		{
			if (list1 == null || list2 == null)
			{
				return list1 ?? list2;
			}
			Dictionary<string, VersionRange> dictionary = new Dictionary<string, VersionRange>(list1.Length + list1.Length);
			foreach (KeyValuePair<string, VersionRange> keyValuePair in list1)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, VersionRange> keyValuePair2 in list2)
			{
				VersionRange versionRange;
				if (dictionary.TryGetValue(keyValuePair2.Key, out versionRange))
				{
					dictionary[keyValuePair2.Key] = IPartitionedDocumentExtensions.Merge(versionRange, keyValuePair2.Value);
				}
				else
				{
					dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			return dictionary.ToArray<KeyValuePair<string, VersionRange>>();
		}

		// Token: 0x0600BE26 RID: 48678 RVA: 0x00267714 File Offset: 0x00265914
		private static VersionRange Merge(VersionRange version1, VersionRange version2)
		{
			Version version3 = ((version1.MinVersion > version2.MinVersion) ? version1.MinVersion : version2.MinVersion);
			Version version4 = ((version1.MaxVersion > version2.MaxVersion) ? version2.MaxVersion : version1.MaxVersion);
			if (version3 > version4)
			{
				return new VersionRange(version3);
			}
			return new VersionRange(version3, version4);
		}
	}
}
