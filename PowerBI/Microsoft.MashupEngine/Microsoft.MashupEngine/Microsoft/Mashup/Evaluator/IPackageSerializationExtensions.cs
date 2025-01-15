using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD5 RID: 7381
	internal static class IPackageSerializationExtensions
	{
		// Token: 0x0600B7CC RID: 47052 RVA: 0x00254668 File Offset: 0x00252868
		public static void WriteIPackage(this BinaryWriter writer, IPackage package)
		{
			string[] array = package.SectionNames.ToArray<string>();
			writer.WriteArray(array, delegate(BinaryWriter w, string s)
			{
				w.WriteString(s);
			});
			foreach (string text in array)
			{
				writer.WriteIPackageSection(package.GetSection(text));
			}
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x002546C8 File Offset: 0x002528C8
		public static IPackage ReadIPackage(this BinaryReader reader)
		{
			Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
			string[] array = reader.ReadArray((BinaryReader r) => r.ReadString());
			for (int i = 0; i < array.Length; i++)
			{
				IPackageSection packageSection = reader.ReadIPackageSection();
				dictionary.Add(array[i], packageSection);
			}
			return new Package(dictionary);
		}

		// Token: 0x0600B7CE RID: 47054 RVA: 0x00254726 File Offset: 0x00252926
		public static void WriteIPackageSection(this BinaryWriter writer, IPackageSection section)
		{
			writer.WriteIPackageSectionConfig(section.Config);
			writer.WriteNullableString(section.UniqueID);
			writer.WriteSegmentedString(section.Text);
		}

		// Token: 0x0600B7CF RID: 47055 RVA: 0x0025474C File Offset: 0x0025294C
		public static IPackageSection ReadIPackageSection(this BinaryReader reader)
		{
			IPackageSectionConfig packageSectionConfig = reader.ReadIPackageSectionConfig();
			string text = reader.ReadNullableString();
			SegmentedString segmentedString = reader.ReadSegmentedString();
			return new PackageSection(packageSectionConfig, text, segmentedString);
		}

		// Token: 0x0600B7D0 RID: 47056 RVA: 0x00254774 File Offset: 0x00252974
		public static void WriteIPackageSectionConfig(this BinaryWriter writer, IPackageSectionConfig config)
		{
			writer.WriteNullableString(config.Culture);
			writer.WriteNullableString(config.TimeZone);
			writer.WriteNullableString(config.Version);
			writer.WriteNullableString(config.MinVersion);
			writer.WriteArray(config.Dependencies, new Action<BinaryWriter, KeyValuePair<string, VersionRange>>(IPackageSerializationExtensions.WriteDependency));
		}

		// Token: 0x0600B7D1 RID: 47057 RVA: 0x002547CC File Offset: 0x002529CC
		public static IPackageSectionConfig ReadIPackageSectionConfig(this BinaryReader reader)
		{
			string text = reader.ReadNullableString();
			string text2 = reader.ReadNullableString();
			string text3 = reader.ReadNullableString();
			string text4 = reader.ReadNullableString();
			KeyValuePair<string, VersionRange>[] array = reader.ReadArray(new Func<BinaryReader, KeyValuePair<string, VersionRange>>(IPackageSerializationExtensions.ReadDependency));
			return new PackageSectionConfig(text, text2, text3, text4, array);
		}

		// Token: 0x0600B7D2 RID: 47058 RVA: 0x00254810 File Offset: 0x00252A10
		public static void WriteDependency(this BinaryWriter writer, KeyValuePair<string, VersionRange> dependency)
		{
			writer.Write(dependency.Key);
			writer.WriteVersion(dependency.Value.MinVersion);
			writer.WriteVersion(dependency.Value.MaxVersion);
		}

		// Token: 0x0600B7D3 RID: 47059 RVA: 0x00254843 File Offset: 0x00252A43
		public static KeyValuePair<string, VersionRange> ReadDependency(this BinaryReader reader)
		{
			return new KeyValuePair<string, VersionRange>(reader.ReadString(), new VersionRange(reader.ReadVersion(), reader.ReadVersion()));
		}

		// Token: 0x0600B7D4 RID: 47060 RVA: 0x00254861 File Offset: 0x00252A61
		public static void WriteVersion(this BinaryWriter writer, Version version)
		{
			writer.Write(version.Major);
			writer.Write(version.Minor);
			writer.Write(version.Build);
			writer.Write(version.Revision);
		}

		// Token: 0x0600B7D5 RID: 47061 RVA: 0x00254894 File Offset: 0x00252A94
		public static Version ReadVersion(this BinaryReader reader)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			int num4 = reader.ReadInt32();
			if (num4 != -1)
			{
				return new Version(num, num2, num3, num4);
			}
			if (num3 != -1)
			{
				return new Version(num, num2, num3);
			}
			return new Version(num, num2);
		}
	}
}
