using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using dotless.Core.Input;
using dotless.Core.Parser;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Importers
{
	// Token: 0x020000BA RID: 186
	public class Importer : IImporter
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000174B3 File Offset: 0x000156B3
		public static Regex EmbeddedResourceRegex
		{
			get
			{
				return Importer._embeddedResourceRegex;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000174BA File Offset: 0x000156BA
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000174C2 File Offset: 0x000156C2
		public IFileReader FileReader { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000174CB File Offset: 0x000156CB
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x000174D3 File Offset: 0x000156D3
		public List<string> Imports { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000174DC File Offset: 0x000156DC
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x000174E4 File Offset: 0x000156E4
		public Func<Parser> Parser { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000174ED File Offset: 0x000156ED
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000174F5 File Offset: 0x000156F5
		public virtual string CurrentDirectory { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000174FE File Offset: 0x000156FE
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00017506 File Offset: 0x00015706
		public bool IsUrlRewritingDisabled { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001750F File Offset: 0x0001570F
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00017517 File Offset: 0x00015717
		public string RootPath { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00017520 File Offset: 0x00015720
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00017528 File Offset: 0x00015728
		public bool ImportAllFilesAsLess { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00017531 File Offset: 0x00015731
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00017539 File Offset: 0x00015739
		public bool InlineCssFiles { get; set; }

		// Token: 0x0600054F RID: 1359 RVA: 0x00017542 File Offset: 0x00015742
		public Importer()
			: this(new FileReader())
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001754F File Offset: 0x0001574F
		public Importer(IFileReader fileReader)
			: this(fileReader, false, "", false, false)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00017560 File Offset: 0x00015760
		public Importer(IFileReader fileReader, bool disableUrlReWriting, string rootPath, bool inlineCssFiles, bool importAllFilesAsLess)
		{
			this.FileReader = fileReader;
			this.IsUrlRewritingDisabled = disableUrlReWriting;
			this.RootPath = rootPath;
			this.InlineCssFiles = inlineCssFiles;
			this.ImportAllFilesAsLess = importAllFilesAsLess;
			this.Imports = new List<string>();
			this.CurrentDirectory = "";
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000175CF File Offset: 0x000157CF
		private static bool IsProtocolUrl(string url)
		{
			return Regex.IsMatch(url, "^([a-zA-Z]{2,}:)");
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000175DC File Offset: 0x000157DC
		private static bool IsNonRelativeUrl(string url)
		{
			return url.StartsWith("/") || url.StartsWith("~/") || Regex.IsMatch(url, "^[a-zA-Z]:");
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00017605 File Offset: 0x00015805
		private static bool IsEmbeddedResource(string path)
		{
			return Importer._embeddedResourceRegex.IsMatch(path);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017612 File Offset: 0x00015812
		public List<string> GetCurrentPathsClone()
		{
			return new List<string>(this._paths);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001761F File Offset: 0x0001581F
		protected bool CheckIgnoreImport(Import import)
		{
			return this.CheckIgnoreImport(import, import.Path);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00017630 File Offset: 0x00015830
		protected bool CheckIgnoreImport(Import import, string path)
		{
			if (this.IsOptionSet(import.ImportOptions, ImportOptions.Multiple))
			{
				return false;
			}
			if (import.IsReference || this.IsOptionSet(import.ImportOptions, ImportOptions.Reference))
			{
				return this._rawImports.Contains(path, StringComparer.InvariantCultureIgnoreCase) || this.CheckIgnoreImport(this._referenceImports, path);
			}
			return this.CheckIgnoreImport(this._rawImports, path);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017696 File Offset: 0x00015896
		private bool CheckIgnoreImport(List<string> importList, string path)
		{
			if (importList.Contains(path, StringComparer.InvariantCultureIgnoreCase))
			{
				return true;
			}
			importList.Add(path);
			return false;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000176B0 File Offset: 0x000158B0
		public virtual ImportAction Import(Import import)
		{
			if (Importer.IsProtocolUrl(import.Path) && !Importer.IsEmbeddedResource(import.Path))
			{
				if (import.Path.EndsWith(".less"))
				{
					throw new FileNotFoundException(string.Format(".less cannot import non local less files [{0}].", import.Path), import.Path);
				}
				if (this.CheckIgnoreImport(import))
				{
					return ImportAction.ImportNothing;
				}
				return ImportAction.LeaveImport;
			}
			else
			{
				string text = import.Path;
				if (!Importer.IsNonRelativeUrl(text))
				{
					text = this.GetAdjustedFilePath(import.Path, this._paths);
				}
				if (this.CheckIgnoreImport(import, text))
				{
					return ImportAction.ImportNothing;
				}
				if (!this.ImportAllFilesAsLess && !this.IsOptionSet(import.ImportOptions, ImportOptions.Less) && import.Path.EndsWith(".css") && !import.Path.EndsWith(".less.css"))
				{
					if (this.InlineCssFiles || this.IsOptionSet(import.ImportOptions, ImportOptions.Inline))
					{
						if (Importer.IsEmbeddedResource(import.Path) && this.ImportEmbeddedCssContents(text, import))
						{
							return ImportAction.ImportCss;
						}
						if (this.ImportCssFileContents(text, import))
						{
							return ImportAction.ImportCss;
						}
					}
					return ImportAction.LeaveImport;
				}
				if (this.Parser == null)
				{
					throw new InvalidOperationException("Parser cannot be null.");
				}
				if (this.ImportLessFile(text, import))
				{
					return ImportAction.ImportLess;
				}
				if (this.IsOptionSet(import.ImportOptions, ImportOptions.Optional))
				{
					return ImportAction.ImportNothing;
				}
				if (this.IsOptionSet(import.ImportOptions, ImportOptions.Css))
				{
					return ImportAction.LeaveImport;
				}
				if (import.Path.EndsWith(".less", StringComparison.InvariantCultureIgnoreCase))
				{
					throw new FileNotFoundException(string.Format("You are importing a file ending in .less that cannot be found [{0}].", text), text);
				}
				return ImportAction.LeaveImport;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00017825 File Offset: 0x00015A25
		public IDisposable BeginScope(Import parentScope)
		{
			return new Importer.ImportScope(this, Path.GetDirectoryName(parentScope.Path));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017838 File Offset: 0x00015A38
		protected string GetAdjustedFilePath(string path, IEnumerable<string> pathList)
		{
			return pathList.Concat(new string[] { path }).AggregatePaths(this.CurrentDirectory);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00017858 File Offset: 0x00015A58
		protected bool ImportLessFile(string lessPath, Import import)
		{
			string text = null;
			string text2;
			if (Importer.IsEmbeddedResource(lessPath))
			{
				text2 = ResourceLoader.GetResource(lessPath, this.FileReader, out text);
				if (text2 == null)
				{
					return false;
				}
			}
			else
			{
				string text3 = lessPath;
				if (Path.IsPathRooted(lessPath))
				{
					text3 = lessPath;
				}
				else if (!string.IsNullOrEmpty(this.CurrentDirectory))
				{
					text3 = this.CurrentDirectory.Replace("\\", "/").TrimEnd(new char[] { '/' }) + "/" + lessPath;
				}
				bool flag = this.FileReader.DoesFileExist(text3);
				if (!flag && !text3.EndsWith(".less"))
				{
					text3 += ".less";
					flag = this.FileReader.DoesFileExist(text3);
				}
				if (!flag)
				{
					return false;
				}
				text2 = this.FileReader.GetFileContents(text3);
				text = text3;
			}
			this._paths.Add(Path.GetDirectoryName(import.Path));
			try
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.Imports.Add(text);
				}
				import.InnerRoot = this.Parser().Parse(text2, lessPath);
			}
			catch
			{
				this.Imports.Remove(text);
				throw;
			}
			finally
			{
				this._paths.RemoveAt(this._paths.Count - 1);
			}
			return true;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000179A8 File Offset: 0x00015BA8
		private bool ImportEmbeddedCssContents(string file, Import import)
		{
			string resource = ResourceLoader.GetResource(file, this.FileReader, out file);
			if (resource == null)
			{
				return false;
			}
			import.InnerContent = resource;
			return true;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000179D1 File Offset: 0x00015BD1
		protected bool ImportCssFileContents(string file, Import import)
		{
			if (!this.FileReader.DoesFileExist(file))
			{
				return false;
			}
			import.InnerContent = this.FileReader.GetFileContents(file);
			this.Imports.Add(file);
			return true;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017A02 File Offset: 0x00015C02
		public string AlterUrl(string url, List<string> pathList)
		{
			if (!Importer.IsProtocolUrl(url) && !Importer.IsNonRelativeUrl(url))
			{
				if (pathList.Any<string>() && !this.IsUrlRewritingDisabled)
				{
					url = this.GetAdjustedFilePath(url, pathList);
				}
				return this.RootPath + url;
			}
			return url;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017A3C File Offset: 0x00015C3C
		public void ResetImports()
		{
			this.Imports.Clear();
			this._rawImports.Clear();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00017A54 File Offset: 0x00015C54
		public IEnumerable<string> GetImports()
		{
			return this.Imports.Distinct<string>();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00017A61 File Offset: 0x00015C61
		private bool IsOptionSet(ImportOptions options, ImportOptions test)
		{
			return (options & test) == test;
		}

		// Token: 0x04000102 RID: 258
		private static readonly Regex _embeddedResourceRegex = new Regex("^dll://(?<Assembly>.+?)#(?<Resource>.+)$");

		// Token: 0x04000106 RID: 262
		private readonly List<string> _paths = new List<string>();

		// Token: 0x04000107 RID: 263
		protected readonly List<string> _rawImports = new List<string>();

		// Token: 0x04000108 RID: 264
		private readonly List<string> _referenceImports = new List<string>();

		// Token: 0x0200011C RID: 284
		private class ImportScope : IDisposable
		{
			// Token: 0x060006EB RID: 1771 RVA: 0x00019EF3 File Offset: 0x000180F3
			public ImportScope(Importer importer, string path)
			{
				this.importer = importer;
				this.importer._paths.Add(path);
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00019F13 File Offset: 0x00018113
			public void Dispose()
			{
				this.importer._paths.RemoveAt(this.importer._paths.Count - 1);
			}

			// Token: 0x0400020C RID: 524
			private readonly Importer importer;
		}
	}
}
