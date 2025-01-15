using System;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E45 RID: 7749
	public static class PackageFactory
	{
		// Token: 0x0600BE7F RID: 48767 RVA: 0x0026869E File Offset: 0x0026689E
		public static IPackageSectionConfig NewConfig(string culture, string timeZone, string version, string minVersion)
		{
			return new PackageSectionConfig(culture, timeZone, version, minVersion, null);
		}

		// Token: 0x0600BE80 RID: 48768 RVA: 0x002686AA File Offset: 0x002668AA
		public static IPackageSection NewSection(IPackageSectionConfig config, string uniqueID, SegmentedString text)
		{
			return new PackageSection(config, uniqueID, text);
		}

		// Token: 0x0600BE81 RID: 48769 RVA: 0x002686B4 File Offset: 0x002668B4
		public static IPackage NewPackage(params IPackageSection[] sections)
		{
			return new Package(sections.ToDictionary((IPackageSection s) => s.UniqueID));
		}

		// Token: 0x0600BE82 RID: 48770 RVA: 0x002686E0 File Offset: 0x002668E0
		public static IPackage NewPackage(ISectionDocument[] documents)
		{
			if (documents.Length == 0)
			{
				throw new InvalidOperationException();
			}
			ITokens tokens = documents[0].Tokens;
			IPackageSection[] array = new IPackageSection[documents.Length];
			int num = 0;
			for (int i = 0; i < documents.Length; i++)
			{
				TokenReference end = documents[i].Range.End;
				int offset = tokens.GetOffset(end);
				array[i] = new PackageSection(documents[i].Section.GetSectionMetadata(null), documents[i].Section.SectionName, tokens.GetSegmentedText().Substring(num, offset - num));
				num = offset;
			}
			return PackageFactory.NewPackage(array);
		}
	}
}
