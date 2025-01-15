using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E17 RID: 7703
	public static class IPackageExtensions
	{
		// Token: 0x0600BDF4 RID: 48628 RVA: 0x0026715C File Offset: 0x0026535C
		public static IPackage TranslateSourceLocationPackage(this IPackage beforePackage, IPackage afterPackage, IEnumerable<PackageEdit> edits, IEngine engine)
		{
			Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
			IDictionary<string, IList<PackageEdit>> sectionEdits = edits.GetSectionEdits();
			foreach (string text in afterPackage.SectionNames)
			{
				IPackageSection section = beforePackage.GetSection(text);
				IPackageSection packageSection = afterPackage.GetSection(text);
				IList<PackageEdit> list;
				if (sectionEdits.TryGetValue(text, out list))
				{
					packageSection = new TranslateSourceLocationPackageSection(engine, section, packageSection, list);
				}
				dictionary.Add(text, packageSection);
			}
			return new Package(dictionary);
		}

		// Token: 0x0600BDF5 RID: 48629 RVA: 0x002671EC File Offset: 0x002653EC
		public static IPartitionedDocument PartitionedDocument(this IPackage package, PartitioningScheme partitioningScheme, IEngine engine)
		{
			if (partitioningScheme == PartitioningScheme.MemberLet1)
			{
				return new MemberLetPartitionedDocument(engine, package);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600BDF6 RID: 48630 RVA: 0x00267200 File Offset: 0x00265400
		public static IPackage ReplaceSection(this IPackage package, string sectionName, IPackageSection section)
		{
			Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
			foreach (string text in package.SectionNames)
			{
				IPackageSection packageSection;
				if (text == sectionName)
				{
					packageSection = section;
				}
				else
				{
					packageSection = package.GetSection(text);
				}
				dictionary[text] = packageSection;
			}
			return new Package(dictionary);
		}

		// Token: 0x0600BDF7 RID: 48631 RVA: 0x00267270 File Offset: 0x00265470
		public static IPackageSection ReplaceSectionConfig(this IPackageSection section, IEngine engine, IPackageSectionConfig config)
		{
			SegmentedString segmentedString = section.Text;
			if (config.Dependencies.Length != 0)
			{
				ITokens tokens = engine.Tokenize(section.Text);
				segmentedString = ((ISectionDocument)engine.Parse(tokens, section, delegate(IError error)
				{
				})).UpdateSectionMetadata(engine, config).Apply(segmentedString);
			}
			return new PackageSection(config, section.UniqueID, segmentedString);
		}

		// Token: 0x0600BDF8 RID: 48632 RVA: 0x002672E4 File Offset: 0x002654E4
		public static IPackageSectionConfig AddDependency(this IPackageSectionConfig config, string moduleName, VersionRange moduleVersion)
		{
			KeyValuePair<string, VersionRange>[] array = config.Dependencies;
			KeyValuePair<string, VersionRange> keyValuePair = new KeyValuePair<string, VersionRange>(moduleName, moduleVersion);
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == moduleName)
				{
					array = array.ReplaceAt(i, keyValuePair);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				array = array.Add(keyValuePair);
			}
			return new PackageSectionConfig(config.Culture, config.TimeZone, config.Version, config.MinVersion, array);
		}
	}
}
