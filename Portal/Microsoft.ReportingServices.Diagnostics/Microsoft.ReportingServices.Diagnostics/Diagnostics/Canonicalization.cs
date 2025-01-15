using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001B RID: 27
	internal sealed class Canonicalization
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002AFD File Offset: 0x00000CFD
		internal static HashSet<string> ReportBuilderFiles
		{
			get
			{
				RSTrace.HttpRuntimeTracer.Assert(Canonicalization.m_reportbuilderFiles != null, "ReportBuilderFiles: null != m_reportbuilderFiles");
				return Canonicalization.m_reportbuilderFiles;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B1B File Offset: 0x00000D1B
		internal static bool IsRBFileListInitialized
		{
			get
			{
				return Canonicalization.m_reportbuilderFiles != null;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B28 File Offset: 0x00000D28
		internal static void InitReportBuilderFileList(string rbRootDir)
		{
			Canonicalization.m_reportbuilderFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in Canonicalization.RBApplicationFiles)
			{
				Canonicalization.m_reportbuilderFiles.Add(Canonicalization.GetRBFilePath(text, string.Empty, false));
			}
			RSTrace.HttpRuntimeTracer.Assert(Canonicalization.RBManifestFiles.Length == Canonicalization.RBSubdirs.Length, "InitReportBuilderFileList: RBManifestFiles.Length == RBSubdirs.Length");
			for (int j = 0; j < Canonicalization.RBManifestFiles.Length; j++)
			{
				Canonicalization.m_reportbuilderFiles.Add(Canonicalization.GetRBFilePath(Canonicalization.RBManifestFiles[j], Canonicalization.RBSubdirs[j], false));
				Canonicalization.AddFilesInManifest(Path.Combine(Path.Combine(rbRootDir, Canonicalization.RBSubdirs[j]), Canonicalization.RBManifestFiles[j]), Canonicalization.RBSubdirs[j]);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private static void AddFilesInManifest(string manifestFile, string subdir)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(manifestFile))
			{
				if (RSTrace.HttpRuntimeTracer.TraceInfo)
				{
					RSTrace.HttpRuntimeTracer.Trace(TraceLevel.Info, "Report Builder manifest file {0} not found.", new object[] { manifestFile });
				}
				return;
			}
			try
			{
				XmlUtil.SafeOpenXmlDocumentFile(xmlDocument, manifestFile);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				XmlNode documentElement = xmlDocument.DocumentElement;
				string @namespace = documentElement.CreateNavigator().GetNamespace(string.Empty);
				xmlNamespaceManager.AddNamespace("default", @namespace);
				foreach (object obj in documentElement.SelectNodes(Canonicalization.XPathAssembly, xmlNamespaceManager))
				{
					string value = ((XmlNode)obj).Attributes["codebase"].Value;
					Canonicalization.m_reportbuilderFiles.Add(Canonicalization.GetRBFilePath(value, subdir, true));
				}
				foreach (object obj2 in documentElement.SelectNodes(Canonicalization.XPathFile, xmlNamespaceManager))
				{
					string value2 = ((XmlNode)obj2).Attributes["name"].Value;
					Canonicalization.m_reportbuilderFiles.Add(Canonicalization.GetRBFilePath(value2, subdir, true));
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.HttpRuntimeTracer.TraceError)
				{
					RSTrace.HttpRuntimeTracer.Trace(TraceLevel.Error, "AddFilesInManifest: unexpected exception when parsing {0}. Exception is {1}.", new object[]
					{
						manifestFile,
						ex.ToString()
					});
				}
				throw ex;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DBC File Offset: 0x00000FBC
		private static string GetRBFilePath(string file, string subdir, bool deployExtension)
		{
			return string.Format(CultureInfo.InvariantCulture, "/{0}{1}/{2}{3}", new object[]
			{
				"ReportBuilder",
				string.IsNullOrEmpty(subdir) ? string.Empty : ("/" + subdir),
				file.Replace(Path.DirectorySeparatorChar, '/'),
				deployExtension ? ".deploy" : string.Empty
			});
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E25 File Offset: 0x00001025
		internal static bool IsValidReportBuilderFile(string file)
		{
			return Canonicalization.m_reportbuilderFiles.Contains(file);
		}

		// Token: 0x0400005B RID: 91
		internal const string ReportBuilderDir = "ReportBuilder";

		// Token: 0x0400005C RID: 92
		private static readonly string[] RBApplicationFiles = new string[] { "ReportBuilder_3_0_0_0.application" };

		// Token: 0x0400005D RID: 93
		private static readonly string[] RBManifestFiles = new string[] { "MSReportBuilder.exe.manifest" };

		// Token: 0x0400005E RID: 94
		private static readonly string[] RBSubdirs = new string[] { "RptBuilder_3" };

		// Token: 0x0400005F RID: 95
		private const string DefaultNamespacePrefix = "default";

		// Token: 0x04000060 RID: 96
		private static readonly string XPathAssembly = string.Format(CultureInfo.InvariantCulture, "/*/{0}:dependency/{0}:dependentAssembly[@dependencyType='install' and @codebase]", "default");

		// Token: 0x04000061 RID: 97
		private static readonly string XPathFile = string.Format(CultureInfo.InvariantCulture, "/*/{0}:file[@name]", "default");

		// Token: 0x04000062 RID: 98
		private static HashSet<string> m_reportbuilderFiles = null;
	}
}
