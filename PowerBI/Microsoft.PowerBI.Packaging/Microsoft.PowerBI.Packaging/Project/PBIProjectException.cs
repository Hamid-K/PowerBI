using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	public abstract class PBIProjectException : FileFormatException
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000865B File Offset: 0x0000685B
		public PBIProjectException(string message, string anonymizedErrorDescription, PBIProjectException.PBIProjectErrorCode errorCode, Exception innerException = null, string learnMoreLinkUrl = null)
			: base(message, innerException)
		{
			this.LearnMoreLinkUrl = learnMoreLinkUrl;
			this.Data["PowerBINonFatalError_ErrorDescription"] = errorCode.ToString() + ": " + anonymizedErrorDescription;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008696 File Offset: 0x00006896
		public PBIProjectException(string message, PBIProjectException.PBIProjectErrorCode errorCode, Exception innerException = null, string learnMoreLinkUrl = null)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
			this.LearnMoreLinkUrl = learnMoreLinkUrl;
			this.Data["PowerBINonFatalError_ErrorDescription"] = errorCode.ToString();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000086CC File Offset: 0x000068CC
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000086D4 File Offset: 0x000068D4
		public PBIProjectException.PBIProjectErrorCode ErrorCode { get; private set; }

		// Token: 0x060002FD RID: 765 RVA: 0x000086E0 File Offset: 0x000068E0
		public static string TryGetKnownFilenameFromPath(string path)
		{
			string fileName = Path.GetFileName(path);
			string text;
			if (PBIProjectException.KnownFiles.TryGetValue(fileName, out text))
			{
				return text;
			}
			string extension = Path.GetExtension(path);
			if (PBIProjectException.KnownFiles.TryGetValue(extension, out text))
			{
				return text;
			}
			return "Unknown";
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008722 File Offset: 0x00006922
		public static string GetErrorWithPath(string path)
		{
			return "Path: " + PBIProjectException.TryGetKnownFilenameFromPath(path);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00008734 File Offset: 0x00006934
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000873C File Offset: 0x0000693C
		public string LearnMoreLinkUrl { get; private set; }

		// Token: 0x06000301 RID: 769 RVA: 0x00008745 File Offset: 0x00006945
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("learnMoreLinkUrl", this.LearnMoreLinkUrl);
		}

		// Token: 0x040001B7 RID: 439
		private const string learnMoreLinkUrlTag = "learnMoreLinkUrl";

		// Token: 0x040001B8 RID: 440
		private static readonly Dictionary<string, string> KnownFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ ".platform", ".platform" },
			{ "item.config.json", "item.config.json" },
			{ "item.metadata.json", "item.metadata.json" },
			{ "localSettings.json", "localSettings.json" },
			{ "definition.pbir", "definition.pbir" },
			{ "report.json", "report.json" },
			{ "mobileState.json", "mobileState.json" },
			{ "datasetDiagramLayout.json", "datasetDiagramLayout.json" },
			{ "semanticModelDiagramLayout.json", "semanticModelDiagramLayout.json" },
			{ "CustomVisuals", "CustomVisuals" },
			{ "StaticResources", "StaticResources" },
			{ "thumbnail", "thumbnail" },
			{ "package.json", "package.json" },
			{ "custom.xml", "custom.xml" },
			{ "definition.pbism", "definition.pbism" },
			{ "model.bim", "model.bim" },
			{ "definition.pbidataset", "definition.pbidataset" },
			{ "modelReference.json", "modelReference.json" },
			{ "diagramLayout.json", "diagramLayout.json" },
			{ "cache.abf", "cache.abf" },
			{ "editorSettings.json", "editorSettings.json" },
			{ "unappliedChanges.json", "unappliedChanges.json" },
			{ ".pbip", "PBIP Shortcut" },
			{ ".pbir", "PBIR Shortcut" }
		};

		// Token: 0x020000D3 RID: 211
		public enum PBIProjectErrorCode
		{
			// Token: 0x04000344 RID: 836
			MissingVersion,
			// Token: 0x04000345 RID: 837
			InvalidVersion,
			// Token: 0x04000346 RID: 838
			ByPathAndByConnectionMissing,
			// Token: 0x04000347 RID: 839
			ByPathAndByConnectionNotBoth,
			// Token: 0x04000348 RID: 840
			UnrecognizedVersion,
			// Token: 0x04000349 RID: 841
			RequiredArtifactMissing,
			// Token: 0x0400034A RID: 842
			ReadAllByteRequiredArtifact,
			// Token: 0x0400034B RID: 843
			MultipleThumbnailsException,
			// Token: 0x0400034C RID: 844
			ThumbnailsFolderError,
			// Token: 0x0400034D RID: 845
			RelativeContentError,
			// Token: 0x0400034E RID: 846
			ReportPathInvalid,
			// Token: 0x0400034F RID: 847
			ReportPathNotRelative,
			// Token: 0x04000350 RID: 848
			ByPathNotRelative,
			// Token: 0x04000351 RID: 849
			ProjectShredderError,
			// Token: 0x04000352 RID: 850
			ProjectReadError,
			// Token: 0x04000353 RID: 851
			ObjectNotPerSchema,
			// Token: 0x04000354 RID: 852
			FilePathTooLongError,
			// Token: 0x04000355 RID: 853
			AmbiguityResolvingModelSchema,
			// Token: 0x04000356 RID: 854
			UnsupportedFormatFiles,
			// Token: 0x04000357 RID: 855
			AmbiguityResolvingArtifactDetails,
			// Token: 0x04000358 RID: 856
			AmbiguityResolvingReport,
			// Token: 0x04000359 RID: 857
			FeatureNotEnabled,
			// Token: 0x0400035A RID: 858
			AmbiguityResolvingReportDiagramLayout
		}
	}
}
