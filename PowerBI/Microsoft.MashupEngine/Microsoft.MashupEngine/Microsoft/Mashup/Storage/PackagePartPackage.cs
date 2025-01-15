using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage.Memory;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002080 RID: 8320
	public sealed class PackagePartPackage : IPackage
	{
		// Token: 0x0600CB8D RID: 52109 RVA: 0x00288984 File Offset: 0x00286B84
		public static IPackage FromBytes(IEngine engine, IPackageSectionConfigValidator configValidator, byte[] packagePartBytes)
		{
			PackagePartStorage packagePartStorage = MemoryPackagePartStorage.Deserialize(packagePartBytes, false, false);
			return PackagePartPackage.FromParts(engine, configValidator, packagePartStorage);
		}

		// Token: 0x0600CB8E RID: 52110 RVA: 0x002889A4 File Offset: 0x00286BA4
		public static IPackage FromParts(IEngine engine, IPackageSectionConfigValidator configValidator, PackagePartStorage parts)
		{
			Dictionary<string, string> dictionary;
			return PackagePartPackage.FromParts(engine, configValidator, parts, out dictionary);
		}

		// Token: 0x0600CB8F RID: 52111 RVA: 0x002889BC File Offset: 0x00286BBC
		public static IPackage FromParts(IEngine engine, IPackageSectionConfigValidator configValidator, PackagePartStorage parts, out Dictionary<string, string> embeddings)
		{
			Dictionary<string, PackagePartPackage.PackageSection> dictionary = new Dictionary<string, PackagePartPackage.PackageSection>();
			List<PackageEdit> list = new List<PackageEdit>();
			long num = 10485760L;
			embeddings = PackagePartPackage.AddSections(engine, configValidator, dictionary, null, parts, list, ref num);
			IPackage package = new PackagePartPackage(engine, dictionary);
			if (list.Count > 0)
			{
				IPackage package2 = package.ApplyEdits(list);
				package = package.TranslateSourceLocationPackage(package2, list, engine);
			}
			return package;
		}

		// Token: 0x0600CB90 RID: 52112 RVA: 0x00288A11 File Offset: 0x00286C11
		public static bool IsInlinedSection(string sectionName)
		{
			return sectionName.StartsWith("__inlined__", StringComparison.Ordinal);
		}

		// Token: 0x0600CB91 RID: 52113 RVA: 0x00288A1F File Offset: 0x00286C1F
		private PackagePartPackage(IEngine engine, Dictionary<string, PackagePartPackage.PackageSection> sections)
		{
			this.engine = engine;
			this.sections = sections;
		}

		// Token: 0x17003108 RID: 12552
		// (get) Token: 0x0600CB92 RID: 52114 RVA: 0x00288A35 File Offset: 0x00286C35
		public IEnumerable<string> SectionNames
		{
			get
			{
				return this.sections.Keys;
			}
		}

		// Token: 0x0600CB93 RID: 52115 RVA: 0x00288A44 File Offset: 0x00286C44
		public static IEnumerable<PackageEdit> ComputeEmbeddingReferenceReplacementEdits(IEngine engine, IDocument document, string sectionName, Dictionary<string, string> embeddings)
		{
			List<PackageEdit> list = new List<PackageEdit>();
			foreach (EmbeddingReference embeddingReference in engine.DiscoverEmbeddingReferences(document))
			{
				string text;
				if (embeddings.TryGetValue(embeddingReference.Embedding, out text))
				{
					list.Add(new PackageEdit(sectionName, document.Tokens.GetOffset(embeddingReference.Range.Start), document.Tokens.GetOffset(embeddingReference.Range.End) - document.Tokens.GetOffset(embeddingReference.Range.Start), SegmentedString.New(text)));
				}
			}
			return list;
		}

		// Token: 0x0600CB94 RID: 52116 RVA: 0x00288B0C File Offset: 0x00286D0C
		public IPackageSection GetSection(string sectionName)
		{
			return this.sections[sectionName];
		}

		// Token: 0x0600CB95 RID: 52117 RVA: 0x00288B1C File Offset: 0x00286D1C
		public IPackage ApplyEdits(IEnumerable<PackageEdit> edits)
		{
			Dictionary<string, PackagePartPackage.PackageSection> dictionary = new Dictionary<string, PackagePartPackage.PackageSection>();
			IDictionary<string, IList<PackageEdit>> sectionEdits = edits.GetSectionEdits();
			foreach (string text in this.sections.Keys)
			{
				PackagePartPackage.PackageSection packageSection = this.sections[text];
				IList<PackageEdit> list;
				if (sectionEdits.TryGetValue(text, out list))
				{
					PackagePartPackage.PackageSection packageSection2 = new PackagePartPackage.ConfigPackageSection((PackageConfig)packageSection.Config, packageSection.UniqueID, this.engine, list.Apply(packageSection.Text));
					dictionary.Add(text, packageSection2);
				}
				else
				{
					dictionary.Add(text, packageSection);
				}
			}
			return new PackagePartPackage(this.engine, dictionary);
		}

		// Token: 0x0600CB96 RID: 52118 RVA: 0x00288BE4 File Offset: 0x00286DE4
		private static Dictionary<string, string> AddSections(IEngine engine, IPackageSectionConfigValidator configValidator, Dictionary<string, PackagePartPackage.PackageSection> sections, string prefix, PackagePartStorage parts, List<PackageEdit> edits, ref long sizeLimit)
		{
			Dictionary<string, string> dictionary = PackagePartPackage.AddEmbeddedSections(engine, configValidator, sections, prefix, parts, edits, ref sizeLimit);
			PackagePartPackage.AddFlatSections(engine, sections, prefix, parts, dictionary, edits, ref sizeLimit);
			return dictionary;
		}

		// Token: 0x0600CB97 RID: 52119 RVA: 0x00288C14 File Offset: 0x00286E14
		private static string GetSectionName(string prefix, string sectionName)
		{
			string text = PackagePartPackage.Prefix(prefix, sectionName);
			if (string.IsNullOrEmpty(prefix))
			{
				return text;
			}
			return "__inlined__" + text;
		}

		// Token: 0x0600CB98 RID: 52120 RVA: 0x00288C40 File Offset: 0x00286E40
		private static void AddFlatSections(IEngine engine, Dictionary<string, PackagePartPackage.PackageSection> sections, string prefix, PackagePartStorage parts, Dictionary<string, string> embeddings, List<PackageEdit> edits, ref long sizeLimit)
		{
			PackageConfig packageConfig = parts.GetPackageConfig();
			foreach (string text in parts.GetPartNames(PackagePartType.Formulas))
			{
				string sectionName = PackagePartPackage.GetSectionName(prefix, PackagePartPackage.GetSectionNameFromFileName(text));
				try
				{
					string text2;
					byte[] array;
					if (!PackagePartPackage.TryGetPartContentLimitSize(parts, PackagePartType.Formulas, text, ref sizeLimit, out text2, out array))
					{
						throw new StorageException(Strings.Package_Unable_To_Load);
					}
					SegmentedString segmentedString = SegmentedString.New(Encoding.UTF8.GetString(array));
					ISectionDocument sectionDocument = PackagePartPackage.ParseSectionDocument(engine, segmentedString);
					if (!string.IsNullOrEmpty(prefix))
					{
						segmentedString = PackagePartPackage.GetRenameEdit(engine, sectionDocument.Tokens, sectionDocument.Section.SectionName, sectionName).Apply(segmentedString);
						sectionDocument = PackagePartPackage.ParseSectionDocument(engine, segmentedString);
					}
					edits.AddRange(PackagePartPackage.ComputeEmbeddingReferenceReplacementEdits(engine, sectionDocument, sectionName, embeddings));
					if (!string.IsNullOrEmpty(prefix))
					{
						foreach (ISectionMember sectionMember in sectionDocument.Section.Members)
						{
							if (sectionMember.Export)
							{
								edits.AddRange(PackagePartPackage.GetUnshareMemberEdit(sectionDocument.Tokens, sectionName, sectionMember));
							}
						}
					}
					PackagePartPackage.PackageSection packageSection = new PackagePartPackage.ConfigPackageSection(packageConfig, sectionName, engine, segmentedString);
					sections.Add(sectionName, packageSection);
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					string text3 = "section " + engine.EscapeIdentifier(sectionName) + ";";
					sections.Add(sectionName, new PackagePartPackage.ErrorPackageSection(ex, sectionName, SegmentedString.New(text3)));
				}
			}
		}

		// Token: 0x0600CB99 RID: 52121 RVA: 0x00288DD8 File Offset: 0x00286FD8
		private static Dictionary<string, string> AddEmbeddedSections(IEngine engine, IPackageSectionConfigValidator configValidator, Dictionary<string, PackagePartPackage.PackageSection> sections, string prefix, PackagePartStorage parts, List<PackageEdit> edits, ref long sizeLimit)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in parts.GetPartNames(PackagePartType.Content))
			{
				string text2 = PackagePartPackage.Prefix(prefix, text);
				try
				{
					string text3;
					byte[] array;
					if (PackagePartPackage.TryGetPartContentLimitSize(parts, PackagePartType.Content, text, ref sizeLimit, out text3, out array) && string.Compare(text3, "application/x-ms-import", StringComparison.Ordinal) == 0)
					{
						PackagePartStorage packagePartStorage = MemoryPackagePartStorage.Deserialize(array, false, false);
						PackageConfig packageConfig = packagePartStorage.GetPackageConfig();
						configValidator.ValidatePackageSectionConfig(PackageConfigPackageSectionConfig.New(packageConfig, engine, default(SegmentedString)));
						PackagePartPackage.AddSections(engine, configValidator, sections, text2, packagePartStorage, edits, ref sizeLimit);
						FormulaConfig formulaConfig;
						if (!packagePartStorage.GetFormulasConfig().TryGetPublishedFormulaConfig(out formulaConfig))
						{
							throw new FirewallException2(Strings.FirewallFlow_BadEmbedding, null);
						}
						string sectionName = PackagePartPackage.GetSectionName(text2, formulaConfig.SectionName);
						string text4 = string.Format(CultureInfo.InvariantCulture, "Embedded.Value({0}!{1}, {2})", engine.EscapeIdentifier(sectionName), engine.EscapeIdentifier(formulaConfig.FormulaName), engine.EscapeString(text));
						dictionary.Add(text, text4);
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					string text5 = "section " + engine.EscapeIdentifier(text2) + ";";
					sections.Add(text2, new PackagePartPackage.ErrorPackageSection(ex, text2, SegmentedString.New(text5)));
				}
			}
			return dictionary;
		}

		// Token: 0x0600CB9A RID: 52122 RVA: 0x00288F30 File Offset: 0x00287130
		private static bool TryGetPartContentLimitSize(PackagePartStorage parts, PackagePartType partType, string contentName, ref long sizeLimit, out string contentType, out byte[] contentBytes)
		{
			if (!parts.TryGetPartContent(partType, contentName, out contentType, out contentBytes))
			{
				return false;
			}
			if (sizeLimit - (long)contentBytes.Length < 0L)
			{
				throw new StorageException(Strings.Package_Unable_To_Load_Too_Large(10L));
			}
			sizeLimit -= (long)contentBytes.Length;
			return true;
		}

		// Token: 0x0600CB9B RID: 52123 RVA: 0x00288F6E File Offset: 0x0028716E
		private static string Prefix(string prefix, string name)
		{
			if (!string.IsNullOrEmpty(prefix))
			{
				name = prefix + "." + name;
			}
			return name;
		}

		// Token: 0x0600CB9C RID: 52124 RVA: 0x00288F88 File Offset: 0x00287188
		private static ISectionDocument ParseSectionDocument(IEngine engine, SegmentedString sectionText)
		{
			ITokens tokens = engine.Tokenize(sectionText);
			ISectionDocument sectionDocument = engine.Parse(tokens, new TextDocumentHost(sectionText), delegate(IError error)
			{
			}) as ISectionDocument;
			if (sectionDocument == null)
			{
				throw new StorageException(Strings.Package_Unable_To_Load);
			}
			return sectionDocument;
		}

		// Token: 0x0600CB9D RID: 52125 RVA: 0x00288FDC File Offset: 0x002871DC
		private static PackageEdit GetRenameEdit(IEngine engine, ITokens tokens, Identifier sectionName, string newSectionName)
		{
			int offset = tokens.GetOffset(sectionName.Range.Start);
			int num = tokens.GetOffset(sectionName.Range.End) + tokens.GetLength(sectionName.Range.End);
			return new PackageEdit(sectionName.Name, offset, num - offset, SegmentedString.New(engine.EscapeIdentifier(newSectionName)));
		}

		// Token: 0x0600CB9E RID: 52126 RVA: 0x00289044 File Offset: 0x00287244
		private static IEnumerable<PackageEdit> GetUnshareMemberEdit(ITokens tokens, string sectionName, ISectionMember member)
		{
			for (TokenReference tokenReference = member.Range.Start; tokenReference < member.Name.Range.Start; tokenReference++)
			{
				if (tokens.GetType(tokenReference) == TokenType.Shared)
				{
					return new PackageEdit[]
					{
						new PackageEdit(sectionName, tokens.GetOffset(tokenReference), tokens.GetLength(tokenReference), SegmentedString.New(string.Empty))
					};
				}
			}
			return new PackageEdit[0];
		}

		// Token: 0x0600CB9F RID: 52127 RVA: 0x002890BC File Offset: 0x002872BC
		private static string GetSectionNameFromFileName(string sectionFileName)
		{
			int num = sectionFileName.LastIndexOf(".", StringComparison.Ordinal);
			if (num >= 0)
			{
				sectionFileName = sectionFileName.Substring(0, num);
			}
			return sectionFileName;
		}

		// Token: 0x04006752 RID: 26450
		private const string inlinedSectionPrefix = "__inlined__";

		// Token: 0x04006753 RID: 26451
		private const bool allowEdits = false;

		// Token: 0x04006754 RID: 26452
		private const bool enableCompression = false;

		// Token: 0x04006755 RID: 26453
		private const string embeddingReferenceTemplate = "Embedded.Value({0}!{1}, {2})";

		// Token: 0x04006756 RID: 26454
		private readonly IEngine engine;

		// Token: 0x04006757 RID: 26455
		private readonly Dictionary<string, PackagePartPackage.PackageSection> sections;

		// Token: 0x02002081 RID: 8321
		private abstract class PackageSection : IPackageSection, IDocumentHost, ICacheableDocumentHost
		{
			// Token: 0x0600CBA0 RID: 52128 RVA: 0x002890E5 File Offset: 0x002872E5
			public PackageSection(string uniqueID, SegmentedString text)
			{
				this.uniqueID = uniqueID;
				this.text = text;
			}

			// Token: 0x17003109 RID: 12553
			// (get) Token: 0x0600CBA1 RID: 52129
			public abstract IPackageSectionConfig Config { get; }

			// Token: 0x1700310A RID: 12554
			// (get) Token: 0x0600CBA2 RID: 52130 RVA: 0x002890FB File Offset: 0x002872FB
			public string UniqueID
			{
				get
				{
					return this.uniqueID;
				}
			}

			// Token: 0x1700310B RID: 12555
			// (get) Token: 0x0600CBA3 RID: 52131 RVA: 0x00289103 File Offset: 0x00287303
			public object CacheIdentity
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x1700310C RID: 12556
			// (get) Token: 0x0600CBA4 RID: 52132 RVA: 0x00289110 File Offset: 0x00287310
			public SegmentedString Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x04006758 RID: 26456
			private readonly string uniqueID;

			// Token: 0x04006759 RID: 26457
			private readonly SegmentedString text;
		}

		// Token: 0x02002082 RID: 8322
		private class ErrorPackageSection : PackagePartPackage.PackageSection
		{
			// Token: 0x0600CBA5 RID: 52133 RVA: 0x00289118 File Offset: 0x00287318
			public ErrorPackageSection(Exception exception, string uniqueID, SegmentedString text)
				: base(uniqueID, text)
			{
				this.exception = exception;
			}

			// Token: 0x1700310D RID: 12557
			// (get) Token: 0x0600CBA6 RID: 52134 RVA: 0x00289129 File Offset: 0x00287329
			public override IPackageSectionConfig Config
			{
				get
				{
					throw this.exception;
				}
			}

			// Token: 0x0400675A RID: 26458
			private readonly Exception exception;
		}

		// Token: 0x02002083 RID: 8323
		private class ConfigPackageSection : PackagePartPackage.PackageSection
		{
			// Token: 0x0600CBA7 RID: 52135 RVA: 0x00289131 File Offset: 0x00287331
			public ConfigPackageSection(PackageConfig config, string uniqueID, IEngine engine, SegmentedString text)
				: base(uniqueID, text)
			{
				this.config = PackageConfigPackageSectionConfig.New(config, engine, text);
			}

			// Token: 0x1700310E RID: 12558
			// (get) Token: 0x0600CBA8 RID: 52136 RVA: 0x0028914B File Offset: 0x0028734B
			public override IPackageSectionConfig Config
			{
				get
				{
					return this.config;
				}
			}

			// Token: 0x0400675B RID: 26459
			private readonly PackageConfigPackageSectionConfig config;
		}
	}
}
