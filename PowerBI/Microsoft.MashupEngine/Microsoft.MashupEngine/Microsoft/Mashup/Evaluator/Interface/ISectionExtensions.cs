using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E2E RID: 7726
	public static class ISectionExtensions
	{
		// Token: 0x0600BE39 RID: 48697 RVA: 0x002678AC File Offset: 0x00265AAC
		public static bool HasDependencies(this ISection section)
		{
			if (section != null && section.Attribute != null && section.Attribute.Members != null && section.Attribute.Members.Count > 0)
			{
				VariableInitializer variableInitializer = section.Attribute.Members.FirstOrDefault((VariableInitializer v) => v.Name != null && v.Name.Name == "Requires");
				return variableInitializer.Name != null && variableInitializer.Value is IRecordExpression;
			}
			return false;
		}

		// Token: 0x0600BE3A RID: 48698 RVA: 0x00267938 File Offset: 0x00265B38
		public static IPackageSectionConfig GetSectionMetadata(this ISection section, VersionRange defaultCoreVersion = null)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			List<KeyValuePair<string, VersionRange>> list = new List<KeyValuePair<string, VersionRange>>();
			if (section != null && section.Attribute != null && section.Attribute.Members != null && section.Attribute.Members.Count > 0)
			{
				ISectionExtensions.TryGetStringInitializer(section, "Culture", out text);
				ISectionExtensions.TryGetStringInitializer(section, "TimeZone", out text2);
				string text4;
				if (ISectionExtensions.TryGetStringInitializer(section, "Version", out text4))
				{
					Version version;
					if (!Versioning.TryParseVersion(text4, out version))
					{
						throw new InvalidOperationException(Strings.Invalid_Section_Version(section.SectionName, text4));
					}
					text3 = text4;
				}
				VariableInitializer variableInitializer = section.Attribute.Members.FirstOrDefault((VariableInitializer v) => v.Name != null && v.Name.Name == "Requires");
				if (variableInitializer.Name != null)
				{
					IRecordExpression recordExpression = variableInitializer.Value as IRecordExpression;
					if (recordExpression != null)
					{
						using (IEnumerator<VariableInitializer> enumerator = recordExpression.Members.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VariableInitializer variableInitializer2 = enumerator.Current;
								IConstantExpression2 constantExpression = variableInitializer2.Value as IConstantExpression2;
								if (!(variableInitializer2.Name != null) || constantExpression == null || constantExpression.Value == null || !constantExpression.Value.IsText)
								{
									throw new InvalidOperationException(Strings.Invalid_Section_Attribute(section.SectionName, "Requires"));
								}
								VersionRange versionRange;
								if (!VersionRange.TryParse(constantExpression.Value.AsString, out versionRange))
								{
									throw new InvalidOperationException(Strings.Invalid_Section_Dependency(section.SectionName, constantExpression.Value.AsString));
								}
								list.Add(new KeyValuePair<string, VersionRange>(variableInitializer2.Name, versionRange));
							}
							goto IL_01CE;
						}
					}
					throw new InvalidOperationException(Strings.Invalid_Section_Attribute(section.SectionName, "Requires"));
				}
			}
			IL_01CE:
			if (defaultCoreVersion != null)
			{
				if (!list.Any((KeyValuePair<string, VersionRange> d) => d.Key == "Core"))
				{
					list.Add(new KeyValuePair<string, VersionRange>("Core", defaultCoreVersion));
				}
			}
			return new PackageSectionConfig(text, text2, text3, "1.5.3296.0", list.ToArray());
		}

		// Token: 0x0600BE3B RID: 48699 RVA: 0x00267B74 File Offset: 0x00265D74
		public static PackageEdit UpdateSectionMetadata(this ISectionDocument document, IEngine engine, IPackageSectionConfig config)
		{
			ISection section = document.Section;
			ITokens tokens = document.Tokens;
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			int num;
			int num2;
			IList<VariableInitializer> list;
			if (section.Attribute == null || section.Attribute.Members == null)
			{
				num = tokens.GetOffset(section.Range.Start);
				num2 = 0;
				list = EmptyArray<VariableInitializer>.Instance;
			}
			else
			{
				TokenRange range = section.Attribute.Range;
				num = tokens.GetOffset(range.Start);
				num2 = tokens.GetOffset(range.End) + tokens.GetLength(range.End) - num;
				list = section.Attribute.Members;
			}
			bool flag = true;
			string text = ((config.Culture == null) ? "null" : engine.EscapeString(config.Culture));
			bool flag2 = true;
			string text2 = ((config.TimeZone == null) ? "null" : engine.EscapeString(config.TimeZone));
			bool flag3 = true;
			string text3 = ISectionExtensions.CreateDependencyRecord(engine, config.Dependencies);
			segmentedStringBuilder.Append('[');
			foreach (VariableInitializer variableInitializer in list)
			{
				if (segmentedStringBuilder.Length > 1)
				{
					segmentedStringBuilder.Append(',');
				}
				segmentedStringBuilder.Append(engine.EscapeFieldIdentifier(variableInitializer.Name));
				segmentedStringBuilder.Append('=');
				string text4 = variableInitializer.Name;
				if (!(text4 == "Culture"))
				{
					if (!(text4 == "TimeZone"))
					{
						if (!(text4 == "Requires"))
						{
							TokenRange range2 = variableInitializer.Value.Range;
							int offset = tokens.GetOffset(range2.Start);
							int num3 = tokens.GetOffset(range2.End) + tokens.GetLength(range2.End);
							segmentedStringBuilder.Append(tokens.GetSegmentedText().Substring(offset, num3 - offset));
						}
						else
						{
							segmentedStringBuilder.Append(text3);
							flag3 = false;
						}
					}
					else
					{
						segmentedStringBuilder.Append(text2);
						flag2 = false;
					}
				}
				else
				{
					segmentedStringBuilder.Append(text);
					flag = false;
				}
			}
			if (flag)
			{
				if (segmentedStringBuilder.Length > 1)
				{
					segmentedStringBuilder.Append(',');
				}
				segmentedStringBuilder.Append("Culture");
				segmentedStringBuilder.Append('=');
				segmentedStringBuilder.Append(text);
			}
			if (flag2)
			{
				if (segmentedStringBuilder.Length > 1)
				{
					segmentedStringBuilder.Append(',');
				}
				segmentedStringBuilder.Append("TimeZone");
				segmentedStringBuilder.Append('=');
				segmentedStringBuilder.Append(text2);
			}
			if (flag3)
			{
				if (segmentedStringBuilder.Length > 1)
				{
					segmentedStringBuilder.Append(',');
				}
				segmentedStringBuilder.Append("Requires");
				segmentedStringBuilder.Append('=');
				segmentedStringBuilder.Append(text3);
			}
			segmentedStringBuilder.Append(']');
			return new PackageEdit(section.SectionName, num, num2, segmentedStringBuilder.ToSegmentedString());
		}

		// Token: 0x17002ECF RID: 11983
		// (get) Token: 0x0600BE3C RID: 48700 RVA: 0x00267E80 File Offset: 0x00266080
		private static string DefaultVersion
		{
			get
			{
				if (ISectionExtensions.defaultVersion == null)
				{
					ISectionExtensions.defaultVersion = FileVersionInfo.GetVersionInfo(typeof(ISectionExtensions).Assembly.Location).FileVersion;
				}
				return ISectionExtensions.defaultVersion;
			}
		}

		// Token: 0x0600BE3D RID: 48701 RVA: 0x00267EB4 File Offset: 0x002660B4
		private static string CreateDependencyRecord(IEngine engine, IEnumerable<KeyValuePair<string, VersionRange>> dependencies)
		{
			ISourceBuilder sourceBuilder = engine.CreateSourceBuilder();
			sourceBuilder.BeginRecord();
			foreach (KeyValuePair<string, VersionRange> keyValuePair in dependencies)
			{
				sourceBuilder.AddField(keyValuePair.Key);
				sourceBuilder.Primitive(engine.Text(keyValuePair.Value.ToString()));
			}
			sourceBuilder.EndRecord();
			return sourceBuilder.ToSource();
		}

		// Token: 0x0600BE3E RID: 48702 RVA: 0x00267F38 File Offset: 0x00266138
		private static bool TryGetStringInitializer(ISection section, string key, out string value)
		{
			VariableInitializer variableInitializer = section.Attribute.Members.FirstOrDefault((VariableInitializer v) => v.Name != null && v.Name.Name == key);
			if (variableInitializer.Name != null)
			{
				IConstantExpression2 constantExpression = variableInitializer.Value as IConstantExpression2;
				if (constantExpression != null && constantExpression.Value != null)
				{
					if (constantExpression.Value.IsText)
					{
						value = constantExpression.Value.AsString;
						return true;
					}
					if (constantExpression.Value.IsNull)
					{
						value = null;
						return false;
					}
				}
				throw new InvalidOperationException(Strings.Invalid_Section_Attribute(section.SectionName, key));
			}
			value = null;
			return false;
		}

		// Token: 0x040060E8 RID: 24808
		public const string RequiresKey = "Requires";

		// Token: 0x040060E9 RID: 24809
		public const string CultureKey = "Culture";

		// Token: 0x040060EA RID: 24810
		public const string TimeZoneKey = "TimeZone";

		// Token: 0x040060EB RID: 24811
		private const string minVersionForNewPackages = "1.5.3296.0";

		// Token: 0x040060EC RID: 24812
		private static string defaultVersion;
	}
}
