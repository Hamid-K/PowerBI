using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000033 RID: 51
	public static class V2ExplorationUtils
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000058FC File Offset: 0x00003AFC
		public static void ValidateUniqueExplorationObjectIdentities(ExplorationContract explorationContract)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (Page page in explorationContract.Pages.PagesList)
			{
				V2ExplorationUtils.ValidatePageArtifactIdentifiers(dictionary, page);
				if (!V2ExplorationUtils.IsNullJObject(page.Content["pageBinding"]))
				{
					V2ExplorationUtils.ValidatePageBindings(dictionary2, page);
				}
				V2ExplorationUtils.ValidateVisualArtifactIdentifiers(page);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005980 File Offset: 0x00003B80
		public static void ValidateExplorationLimits(ExplorationContract explorationContract)
		{
			Pages pages = explorationContract.Pages;
			int? num;
			if (pages == null)
			{
				num = null;
			}
			else
			{
				NonNulls<Page> pagesList = pages.PagesList;
				num = ((pagesList != null) ? new int?(pagesList.Count) : null);
			}
			int? num2 = num;
			int valueOrDefault = num2.GetValueOrDefault();
			if (valueOrDefault > 1000)
			{
				throw new ExplorationFormatValidationException(string.Format("The exploration has {0} pages which exceeds the maximum allowed number of pages in an exploration({1}).", valueOrDefault, 1000).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.ExplorationLimitExceeded, ExplorationErrorSource.User);
			}
			Pages pages2 = explorationContract.Pages;
			foreach (Page page in V2ExplorationUtils.NotNull<Page>((pages2 != null) ? pages2.PagesList : null))
			{
				NonNulls<ExplorationArtifact> visualContainers = page.VisualContainers;
				int num3 = ((visualContainers != null) ? visualContainers.Count : 0);
				if (num3 > 300)
				{
					throw new ExplorationFormatValidationException(string.Format("The exploration has {0} visuals in {1} which exceeds the maximum allowed number of visuals in a page({2}).", num3, page.FilePath, 300).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.ExplorationLimitExceeded, ExplorationErrorSource.User);
				}
			}
			JArray jarray = V2ExplorationUtils.GetExplorationResourcePackages(explorationContract) as JArray;
			int num4 = (V2ExplorationUtils.IsNullJObject(jarray) ? 0 : jarray.Count<JToken>());
			if (num4 > 1000)
			{
				throw new ExplorationFormatValidationException(string.Format("The exploration has {0} resource packages which exceeds the maximum allowed number of resource packages in an exploration({1}).", num4, 1000).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.ExplorationLimitExceeded, ExplorationErrorSource.User);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public static JToken GetExplorationResourcePackages(ExplorationContract explorationContract)
		{
			return explorationContract.Report.Content["resourcePackages"];
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005B0B File Offset: 0x00003D0B
		public static bool IsNullJObject(JToken token)
		{
			return token == null || token.Type == JTokenType.Null;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005B1C File Offset: 0x00003D1C
		public static int ParseEnumOrDefault<TEnum>(JToken enumToken) where TEnum : struct
		{
			TEnum tenum;
			if (!V2ExplorationUtils.IsNullJObject(enumToken) && Enum.TryParse<TEnum>(enumToken.ToString(), out tenum))
			{
				return (int)Enum.Parse(typeof(TEnum), tenum.ToString());
			}
			return 0;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005B63 File Offset: 0x00003D63
		public static IEnumerable<T> NotNull<T>(IEnumerable<T> input)
		{
			return input ?? new List<T>();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005B6F File Offset: 0x00003D6F
		public static Dictionary<string, Dictionary<Version, string>> SchemasFromResources(Assembly assembly)
		{
			return V2ExplorationUtils.SchemaLoader.SchemasFromResources(assembly);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005B77 File Offset: 0x00003D77
		public static Dictionary<string, Dictionary<Version, string>> SchemasFromFolder(string folder)
		{
			return V2ExplorationUtils.SchemaLoader.SchemasFromFolder(folder);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005B80 File Offset: 0x00003D80
		private static void ValidatePageArtifactIdentifiers(IDictionary<string, string> pageObjectNames, Page page)
		{
			JToken jtoken = page.Content["name"];
			if (V2ExplorationUtils.IsNullJObject(jtoken))
			{
				throw new ExplorationFormatValidationException(("Property 'name' is required.\n\nPlease review the file:\n'" + page.FilePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredProperty, ExplorationErrorSource.User);
			}
			string text = jtoken.ToString();
			string filePath = page.FilePath;
			if (pageObjectNames.ContainsKey(text))
			{
				string text2 = pageObjectNames[text];
				throw new ExplorationFormatValidationException(string.Concat(new string[] { "Values for 'name' property must be unique within the report.\n\nPlease review the files:\n'pages/", filePath, "',\n'pages/", text2, "'." }).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.DuplicateObjectIdentifier, ExplorationErrorSource.User);
			}
			if (!V2ExplorationUtils.AllowedCharactersRegex.IsMatch(text))
			{
				throw new ExplorationFormatValidationException(("Property 'name' has unsupported characters.\n\nPlease review the file:\n'pages/" + filePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.UnsupportedObjectName, ExplorationErrorSource.User);
			}
			pageObjectNames.Add(text, page.FilePath);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005C64 File Offset: 0x00003E64
		private static void ValidatePageBindings(IDictionary<string, string> pageBindingNames, Page page)
		{
			if (V2ExplorationUtils.IsNullJObject(page.Content["pageBinding"]))
			{
				return;
			}
			JToken jtoken = page.Content["pageBinding"]["name"];
			if (V2ExplorationUtils.IsNullJObject(jtoken))
			{
				throw new ExplorationFormatValidationException(("Property 'pageBinding.name' is required.\n\nPlease review the file:\n'pages/" + page.FilePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredProperty, ExplorationErrorSource.User);
			}
			string text = jtoken.ToString();
			string filePath = page.FilePath;
			if (pageBindingNames.ContainsKey(text))
			{
				string text2 = pageBindingNames[text];
				throw new ExplorationFormatValidationException(string.Concat(new string[] { "Values for 'pageBinding.name' property must be unique within the report.\n\nPlease review the files:\n'pages/", filePath, "',\n'pages/", text2, "'." }).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.DuplicateObjectIdentifier, ExplorationErrorSource.User);
			}
			if (!V2ExplorationUtils.AllowedCharactersRegex.IsMatch(text))
			{
				throw new ExplorationFormatValidationException(("Property 'pageBinding.name' has unsupported characters.\n\nPlease review the file:\n'pages/" + filePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.UnsupportedObjectName, ExplorationErrorSource.User);
			}
			pageBindingNames.Add(text, page.FilePath);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005D6C File Offset: 0x00003F6C
		private static void ValidateVisualArtifactIdentifiers(Page page)
		{
			if (page.VisualContainers == null)
			{
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ExplorationArtifact explorationArtifact in page.VisualContainers.Where((ExplorationArtifact visual) => !visual.FilePath.EndsWith(ExplorationSerializer.MobileVisualFileNameInFolder, StringComparison.OrdinalIgnoreCase)))
			{
				JToken jtoken = explorationArtifact.Content["name"];
				if (V2ExplorationUtils.IsNullJObject(jtoken))
				{
					string relativePath = V2ExplorationUtils.GetRelativePath(page, explorationArtifact.FilePath);
					throw new ExplorationFormatValidationException(("Property 'name' is required.\n\nPlease review the file:\n'" + relativePath + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.MissingRequiredProperty, ExplorationErrorSource.User);
				}
				string text = jtoken.ToString();
				string relativePath2 = V2ExplorationUtils.GetRelativePath(page, explorationArtifact.FilePath);
				if (dictionary.ContainsKey(text))
				{
					string relativePath3 = V2ExplorationUtils.GetRelativePath(page, dictionary[text]);
					throw new ExplorationFormatValidationException(string.Concat(new string[] { "Values for 'name' property must be unique within the page.\n\nPlease review the files:\n'", relativePath2, "',\n'", relativePath3, "'." }).ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.DuplicateObjectIdentifier, ExplorationErrorSource.User);
				}
				if (!V2ExplorationUtils.AllowedCharactersRegex.IsMatch(text))
				{
					throw new ExplorationFormatValidationException(("Property 'name' has unsupported characters.\n\nPlease review the file:\n'" + relativePath2 + "'.").ToString(CultureInfo.CurrentCulture), ExplorationFormatErrorCode.UnsupportedObjectName, ExplorationErrorSource.User);
				}
				dictionary.Add(text, explorationArtifact.FilePath);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005EE8 File Offset: 0x000040E8
		internal static string GetRelativePath(Page page, string visualPath)
		{
			return string.Join("/", new string[]
			{
				"pages",
				Path.GetDirectoryName(page.FilePath),
				visualPath
			});
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005F14 File Offset: 0x00004114
		public static string GetV2ExplorationStats(ExplorationContract exploration)
		{
			if (exploration == null)
			{
				return null;
			}
			Pages pages = exploration.Pages;
			int? num;
			if (pages == null)
			{
				num = null;
			}
			else
			{
				NonNulls<Page> pagesList = pages.PagesList;
				num = ((pagesList != null) ? new int?(pagesList.Count) : null);
			}
			Pages pages2 = exploration.Pages;
			int? num2;
			if (pages2 == null)
			{
				num2 = null;
			}
			else
			{
				NonNulls<Page> pagesList2 = pages2.PagesList;
				if (pagesList2 == null)
				{
					num2 = null;
				}
				else
				{
					IEnumerable<Page> enumerable = pagesList2.Where((Page page) => page.VisualContainers != null);
					if (enumerable == null)
					{
						num2 = null;
					}
					else
					{
						IEnumerable<ExplorationArtifact> enumerable2 = enumerable.SelectMany((Page page) => page.VisualContainers);
						num2 = ((enumerable2 != null) ? new int?(enumerable2.Count<ExplorationArtifact>()) : null);
					}
				}
			}
			Bookmarks bookmarks = exploration.Bookmarks;
			int? num3;
			if (bookmarks == null)
			{
				num3 = null;
			}
			else
			{
				NonNulls<ExplorationArtifact> bookmarksList = bookmarks.BookmarksList;
				num3 = ((bookmarksList != null) ? new int?(bookmarksList.Count) : null);
			}
			return JsonConvert.SerializeObject(new
			{
				totalPages = num,
				totalVisuals = num2,
				totalBookmarks = num3
			});
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006032 File Offset: 0x00004232
		public static string GetV2ExplorationStats<T>(IDictionary<Uri, T> exploration)
		{
			if (exploration == null)
			{
				return null;
			}
			return JsonConvert.SerializeObject(new
			{
				totalFiles = exploration.Count
			});
		}

		// Token: 0x040000B3 RID: 179
		private const int MaxExplorationJsonParseDepth = 200;

		// Token: 0x040000B4 RID: 180
		public const string ObjectNameProperty = "name";

		// Token: 0x040000B5 RID: 181
		public const string PagesFolderName = "pages";

		// Token: 0x040000B6 RID: 182
		public const string PageBindingProperty = "pageBinding";

		// Token: 0x040000B7 RID: 183
		public const string PageBindingName = "name";

		// Token: 0x040000B8 RID: 184
		public const string ResourcePackagesPropertyName = "resourcePackages";

		// Token: 0x040000B9 RID: 185
		public const string ResourcePackagePropertyName = "resourcePackage";

		// Token: 0x040000BA RID: 186
		public const string PublicCustomVisualsPropertyName = "publicCustomVisuals";

		// Token: 0x040000BB RID: 187
		public const string PodParamatersName = "parameters";

		// Token: 0x040000BC RID: 188
		public const string PodReferenceScopeName = "referenceScope";

		// Token: 0x040000BD RID: 189
		public const string PodTypeName = "type";

		// Token: 0x040000BE RID: 190
		public const int MaxPagesPerExploration = 1000;

		// Token: 0x040000BF RID: 191
		public const int MaxVisualsPerPage = 300;

		// Token: 0x040000C0 RID: 192
		public const int MaxResourcePackagesPerExploration = 1000;

		// Token: 0x040000C1 RID: 193
		public const string AllowedCharacters = "[\\w\\-]";

		// Token: 0x040000C2 RID: 194
		public static readonly Regex AllowedCharactersRegex = new Regex("^[\\w\\-]+$", RegexOptions.Compiled);

		// Token: 0x040000C3 RID: 195
		public static readonly JsonSerializerSettings ExplorationSerializerSettings = new JsonSerializerSettings
		{
			MaxDepth = new int?(200)
		};

		// Token: 0x020000C9 RID: 201
		private class SchemaLoader
		{
			// Token: 0x060004CC RID: 1228 RVA: 0x0000DA3C File Offset: 0x0000BC3C
			public static Dictionary<string, Dictionary<Version, string>> SchemasFromResources(Assembly assembly)
			{
				Dictionary<string, Dictionary<Version, string>> dictionary = V2ExplorationUtils.SchemaLoader.SchemasFromStreams(assembly.GetManifestResourceNames(), (string resourceName) => assembly.GetManifestResourceStream(resourceName));
				if (dictionary.Count == 0)
				{
					throw new InvalidOperationException("Did not load any Exploration V2 schema from '" + assembly.FullName + "'.");
				}
				return dictionary;
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000DA9C File Offset: 0x0000BC9C
			public static Dictionary<string, Dictionary<Version, string>> SchemasFromFolder(string folder)
			{
				Dictionary<string, string> resourceNameToPathDict = new Dictionary<string, string>();
				foreach (string text in Directory.EnumerateFiles(folder, "schema.pretty.json", SearchOption.AllDirectories))
				{
					string text2 = Path.GetDirectoryName(text) + "/_ExplorationV2Schema_";
					resourceNameToPathDict.Add(text2, text);
				}
				Dictionary<string, Dictionary<Version, string>> dictionary = V2ExplorationUtils.SchemaLoader.SchemasFromStreams(resourceNameToPathDict.Keys, (string resourceName) => File.OpenRead(resourceNameToPathDict[resourceName]));
				if (dictionary.Count == 0)
				{
					throw new InvalidOperationException("Did not load any Exploration V2 schema from '" + folder + "'.");
				}
				return dictionary;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000DB54 File Offset: 0x0000BD54
			private static Dictionary<string, Dictionary<Version, string>> SchemasFromStreams(IEnumerable<string> resourceNames, Func<string, Stream> getResourceStream)
			{
				Dictionary<string, Dictionary<Version, string>> dictionary = new Dictionary<string, Dictionary<Version, string>>();
				foreach (string text in resourceNames)
				{
					string text2 = text.Replace('\\', '/');
					if (text2.EndsWith("/_ExplorationV2Schema_"))
					{
						int num = text2.Length - "/_ExplorationV2Schema_".Length;
						int num2 = text2.LastIndexOf('/', num - 1);
						int num3 = text2.LastIndexOf('/', num2 - 1);
						string text3 = text2.Substring(num3 + 1, num2 - num3 - 1);
						Version version = Version.Parse(text2.Substring(num2 + 1, num - num2 - 1));
						Dictionary<Version, string> dictionary2;
						if (!dictionary.TryGetValue(text3, out dictionary2))
						{
							dictionary2 = new Dictionary<Version, string>();
							dictionary.Add(text3, dictionary2);
						}
						string text4;
						using (Stream stream = getResourceStream(text))
						{
							using (StreamReader streamReader = PBIProjectUtils.MakeStreamReader(stream))
							{
								text4 = streamReader.ReadToEnd();
							}
						}
						dictionary2.Add(version, text4);
					}
				}
				return dictionary;
			}

			// Token: 0x04000323 RID: 803
			private const string ResourceMarkerSuffix = "/_ExplorationV2Schema_";
		}
	}
}
