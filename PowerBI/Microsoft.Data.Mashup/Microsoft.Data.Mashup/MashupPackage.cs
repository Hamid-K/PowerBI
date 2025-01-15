using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;
using Microsoft.Mashup.Storage.Memory;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004C RID: 76
	public static class MashupPackage
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000DC42 File Offset: 0x0000BE42
		internal static string DefaultSectionName
		{
			get
			{
				return "Section1";
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DC49 File Offset: 0x0000BE49
		public static byte[] ToBytes(string queryName, string queryText)
		{
			MashupPackage.EnsureNotNull(queryName, "queryName");
			MashupPackage.EnsureNotNull(queryText, "queryText");
			return MashupPackage.CreatePackagePartsForModule(MashupPackage.CreateDocumentText(queryName, queryText), null, false).Serialize();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DC74 File Offset: 0x0000BE74
		internal static IEnumerable<IDocument> GetDocuments(string package, string mashup)
		{
			IEnumerable<IDocument> enumerable;
			if (package != null)
			{
				enumerable = MashupPackage.GetSectionDocuments(MashupPackage.CreatePackage(MashupPackage.FromBase64String(package))).Cast<IDocument>();
			}
			else
			{
				enumerable = MashupPackage.GetSectionDocuments(mashup);
			}
			return enumerable;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		internal static IPackage GetPackage(string package, string mashup)
		{
			if (package != null)
			{
				return MashupPackage.CreatePackage(MashupPackage.FromBase64String(package));
			}
			ISectionDocument[] array;
			return MashupPackage.CreatePackage(mashup, out array);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		internal static string CreateDocumentText(string queryName, string queryText)
		{
			MashupPackage.ValidateQueryText(queryText);
			return "section " + MashupEngines.Version1.EscapeIdentifier(MashupPackage.DefaultSectionName) + ";\r\n\r\nshared " + MashupPackage.GenerateQueryContent(queryName, queryText);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		internal static MemoryPackagePartStorage CreatePackagePartsForModule(string documentText, FormulaConfig formulaConfig = null, bool allowSyntaxErrors = false)
		{
			MemoryPackagePartStorage memoryPackagePartStorage = new MemoryPackagePartStorage();
			ITokens tokens = MashupEngines.Version1.Tokenize(documentText);
			Action<IError> action = (allowSyntaxErrors ? new Action<IError>(Util.SwallowError) : new Action<IError>(Util.ThrowParseError));
			IDocument document = MashupEngines.Version1.Parse(tokens, new TextDocumentHost(MashupPackage.DefaultSectionName), action);
			byte[] bytes = Encoding.UTF8.GetBytes(document.Tokens.Text);
			string text = MashupPackage.DefaultSectionName + ".m";
			memoryPackagePartStorage.TryAddPart(PackagePartType.Formulas, text, "application/x-ms-m", bytes);
			FormulasConfig formulasConfig = new FormulasConfig();
			if (formulaConfig != null)
			{
				formulasConfig.Formulas.Add(formulaConfig);
			}
			byte[] array = Xml<FormulasConfig>.SerializeBytes(formulasConfig);
			memoryPackagePartStorage.TryAddPart(PackagePartType.Config, "formulas.xml", "text/xml", array);
			byte[] array2 = Xml<PackageConfig>.SerializeBytes(new PackageConfig
			{
				Version = MashupPackage.currentVersion,
				MinVersion = MashupPackage.minVersionForNewPackages.ToString(),
				Culture = CultureInfo.CurrentCulture.Name
			});
			memoryPackagePartStorage.TryAddPart(PackagePartType.Config, "Package.xml", "text/xml", array2);
			return memoryPackagePartStorage;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DE01 File Offset: 0x0000C001
		public static string ToBase64String(string queryName, string queryText)
		{
			return Convert.ToBase64String(MashupPackage.ToBytes(queryName, queryText));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000DE0F File Offset: 0x0000C00F
		private static void EnsureNotNull(string obj, string paramName)
		{
			if (string.IsNullOrEmpty(obj))
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000DE20 File Offset: 0x0000C020
		private static void ValidateQueryText(string queryText)
		{
			if (!(MashupEngines.Version1.ParseText(SegmentedString.New(queryText)) is IExpressionDocument))
			{
				throw new MashupExpressionException(ProviderErrorStrings.ExpressionOnlySupported);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DE44 File Offset: 0x0000C044
		private static string GenerateQueryContent(string queryName, string queryText)
		{
			IDocument document = MashupEngines.Version1.ParseText(SegmentedString.New(queryText));
			string text;
			if (!(document is IExpressionDocument))
			{
				queryText = "#!" + MashupEngines.Version1.EscapeString(queryText);
			}
			else if (MashupPackage.TryGetCommentClosingSuffix(document, out text))
			{
				queryText += text;
			}
			return MashupEngines.Version1.EscapeIdentifier(queryName) + " = " + queryText + ";";
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		private static bool TryGetCommentClosingSuffix(IDocument document, out string commentClosingSuffix)
		{
			commentClosingSuffix = null;
			for (int i = document.Tokens.Count - 2; i >= 0; i--)
			{
				TokenReference tokenReference = (TokenReference)i;
				if (document.Tokens.GetType(tokenReference) != TokenType.Whitespace)
				{
					return false;
				}
				int offset = document.Tokens.GetOffset(tokenReference);
				int num = document.Tokens.GetOffset(tokenReference) + document.Tokens.GetLength(tokenReference);
				SegmentedString segmentedString = document.Tokens.GetSegmentedText().Substring(offset, num - offset);
				if (segmentedString.Length > 0 && segmentedString[0] == '/')
				{
					return MashupPackage.TryGetCommentClosingSuffix(segmentedString.ToString(), out commentClosingSuffix);
				}
			}
			return false;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DF5C File Offset: 0x0000C15C
		private static bool TryGetCommentClosingSuffix(string text, out string commentClosingSuffix)
		{
			commentClosingSuffix = null;
			if (text.StartsWith("//", StringComparison.Ordinal))
			{
				commentClosingSuffix = "\r\n";
				return true;
			}
			if (text.StartsWith("/*", StringComparison.Ordinal) && !text.EndsWith("*/", StringComparison.Ordinal))
			{
				commentClosingSuffix = "*/";
				return true;
			}
			return false;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		internal static ISectionDocument[] GetSectionDocuments(string mashup)
		{
			return MashupEngines.Version1.ParseSectionDocuments(mashup);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		internal static ISectionDocument[] GetSectionDocuments(IPackage package)
		{
			List<ISectionDocument> list = new List<ISectionDocument>();
			foreach (string text in package.SectionNames)
			{
				IPackageSection section = package.GetSection(text);
				MashupPackage.sectionConfigValidator.ValidatePackageSectionConfig(section.Config);
				SegmentedString text2 = section.Text;
				ISectionDocument sectionDocument = MashupEngines.Version1.ParseText(text2) as ISectionDocument;
				list.Add(sectionDocument);
			}
			return list.ToArray();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000E048 File Offset: 0x0000C248
		internal static byte[] FromBase64String(string mashup)
		{
			byte[] array;
			try
			{
				array = Convert.FromBase64String(mashup);
			}
			catch (ArgumentException)
			{
				throw MashupPackage.NewUnrecognizedFormatException();
			}
			catch (FormatException)
			{
				throw MashupPackage.NewUnrecognizedFormatException();
			}
			return array;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E08C File Offset: 0x0000C28C
		internal static IPackage CreatePackage(byte[] packageBytes)
		{
			IPackage package2;
			try
			{
				IPackage package = PackagePartPackage.FromBytes(MashupEngines.Version1, MashupPackage.sectionConfigValidator, packageBytes);
				foreach (string text in package.SectionNames)
				{
					MashupPackage.sectionConfigValidator.ValidatePackageSectionConfig(package.GetSection(text).Config);
				}
				package2 = package;
			}
			catch (DepthLimitExceededException ex)
			{
				throw new MashupExpressionException(ProviderErrorStrings.Expression_TooDeep, ex);
			}
			catch (StorageException ex2)
			{
				throw new FormatException(ex2.Message);
			}
			return package2;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E130 File Offset: 0x0000C330
		internal static IPackage CreatePackage(string mashup, out ISectionDocument[] documents)
		{
			documents = MashupEngines.Version1.ParseSectionDocuments(mashup);
			return PackageFactory.NewPackage(documents);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E146 File Offset: 0x0000C346
		private static FormatException NewUnrecognizedFormatException()
		{
			return new FormatException(ProviderErrorStrings.MashupUnrecognizedFormat);
		}

		// Token: 0x040001D3 RID: 467
		private static readonly Version minVersionForNewPackages = new Version("1.5.3296.0");

		// Token: 0x040001D4 RID: 468
		private static readonly string currentVersion = MashupEngines.Version1Version;

		// Token: 0x040001D5 RID: 469
		private static readonly IPackageSectionConfigValidator sectionConfigValidator = new MashupPackage.PackageSectionConfigValidator();

		// Token: 0x02000084 RID: 132
		private sealed class PackageSectionConfigValidator : IPackageSectionConfigValidator
		{
			// Token: 0x060004FD RID: 1277 RVA: 0x00012990 File Offset: 0x00010B90
			public void ValidatePackageSectionConfig(IPackageSectionConfig packageSectionConfig)
			{
				Version version = null;
				if (packageSectionConfig.MinVersion != null)
				{
					version = MashupPackage.PackageSectionConfigValidator.ParseVersion(packageSectionConfig.MinVersion);
				}
				VersionRange coreDependency = packageSectionConfig.Dependencies.GetCoreDependency();
				if (coreDependency != null && coreDependency.MinVersion < MashupPackage.PackageSectionConfigValidator.oldestSupportedPackageVersionNumber)
				{
					throw new MashupVersionException(ProviderErrorStrings.MashupIncompatibleVersion);
				}
				if (version != null && !MashupPackage.PackageSectionConfigValidator.IsVersionAboveMinimum(version, MashupPackage.PackageSectionConfigValidator.currentVersionNumber))
				{
					throw new MashupVersionException(ProviderErrorStrings.MashupIncompatibleVersion);
				}
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x00012A00 File Offset: 0x00010C00
			private static bool IsVersionAboveMinimum(Version packageMinVersionNumber, Version versionNumber)
			{
				if (versionNumber.Build == 0 && versionNumber.Revision == 0)
				{
					Version version = new Version(packageMinVersionNumber.Major, packageMinVersionNumber.Minor);
					return versionNumber >= version;
				}
				return versionNumber >= packageMinVersionNumber;
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00012A40 File Offset: 0x00010C40
			private static Version ParseVersion(string version)
			{
				Version version2;
				if (Versioning.TryParseVersion(version, out version2))
				{
					return version2;
				}
				throw new FormatException(ProviderErrorStrings.MashupUnrecognizedFormat);
			}

			// Token: 0x0400029E RID: 670
			private static readonly Version oldestSupportedPackageVersionNumber = new Version("1.0.3207.0");

			// Token: 0x0400029F RID: 671
			private static readonly Version currentVersionNumber = MashupPackage.PackageSectionConfigValidator.ParseVersion(MashupPackage.currentVersion);
		}
	}
}
