using System;
using System.Text;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006A RID: 106
	public class PBIProjectConstants
	{
		// Token: 0x04000172 RID: 370
		public const string PbiVersionPropertyName = "version";

		// Token: 0x04000173 RID: 371
		public const string PbiIntellisenseSchemaPropertyName = "$schema";

		// Token: 0x04000174 RID: 372
		public const string PbiSupportedVersionsPropertyName = "SupportedVersions";

		// Token: 0x04000175 RID: 373
		public const string PbiSchemaValidatorName = "ProjectSchemaValidator";

		// Token: 0x04000176 RID: 374
		public const string ReportDefName = "definition.pbir";

		// Token: 0x04000177 RID: 375
		public const string ArtifactDetailsName = ".platform";

		// Token: 0x04000178 RID: 376
		public const string ArtifactConfigName = "item.config.json";

		// Token: 0x04000179 RID: 377
		public const string ArtifactMetadataName = "item.metadata.json";

		// Token: 0x0400017A RID: 378
		public const string LocalSettingsName = "localSettings.json";

		// Token: 0x0400017B RID: 379
		public const string ReportArtifactType = "report";

		// Token: 0x0400017C RID: 380
		public const string ReportArtifactTypeInDetails = "Report";

		// Token: 0x0400017D RID: 381
		public const string ReportContentName = "report.json";

		// Token: 0x0400017E RID: 382
		public const string ReportMobilestateName = "mobileState.json";

		// Token: 0x0400017F RID: 383
		public const string ReportDiagramLayoutName = "datasetDiagramLayout.json";

		// Token: 0x04000180 RID: 384
		public const string ReportForSemanticModelDiagramLayoutName = "semanticModelDiagramLayout.json";

		// Token: 0x04000181 RID: 385
		public const string ReportCustomVisualsName = "CustomVisuals";

		// Token: 0x04000182 RID: 386
		public const string ReportStaticResourcesName = "StaticResources";

		// Token: 0x04000183 RID: 387
		public const string ReportThumbnailName = "thumbnail";

		// Token: 0x04000184 RID: 388
		public const string ReportCustomVisualPackageName = "package.json";

		// Token: 0x04000185 RID: 389
		public const string ReportCustomXmlName = "custom.xml";

		// Token: 0x04000186 RID: 390
		public const string SemanticModelArtifactType = "SemanticModel";

		// Token: 0x04000187 RID: 391
		public const string DatasetArtifactType = "dataset";

		// Token: 0x04000188 RID: 392
		public const string DatasetModelSchemaName = "model.bim";

		// Token: 0x04000189 RID: 393
		public const string DatasetDefinitionName = "definition.pbidataset";

		// Token: 0x0400018A RID: 394
		public const string SemanticModelDefinitionName = "definition.pbism";

		// Token: 0x0400018B RID: 395
		public const string DatasetModelRefName = "modelReference.json";

		// Token: 0x0400018C RID: 396
		public const string DatasetDiagramLayoutName = "diagramLayout.json";

		// Token: 0x0400018D RID: 397
		public const string DatasetModelName = "cache.abf";

		// Token: 0x0400018E RID: 398
		public const string DatasetEditorSettingsName = "editorSettings.json";

		// Token: 0x0400018F RID: 399
		public const string DatasetUnappliedChangesName = "unappliedChanges.json";

		// Token: 0x04000190 RID: 400
		public const string DatasetCustomXmlName = "custom.xml";

		// Token: 0x04000191 RID: 401
		public const string ReportDefinintionFolderName = "definition";

		// Token: 0x04000192 RID: 402
		public const string DaxQueriesFolderName = "DAXQueries";

		// Token: 0x04000193 RID: 403
		public const string SettingsDirName = ".pbi";

		// Token: 0x04000194 RID: 404
		public const string GitIgnoreFileName = ".gitignore";

		// Token: 0x04000195 RID: 405
		public const string TmdlDirName = "definition";

		// Token: 0x04000196 RID: 406
		public const string TmdlFileExtension = ".tmdl";

		// Token: 0x04000197 RID: 407
		public const bool IsArtifactShortcutRequired = false;

		// Token: 0x04000198 RID: 408
		public const bool IsReportLocalSettingsRequired = false;

		// Token: 0x04000199 RID: 409
		public const bool IsDatasetLocalSettingsRequired = false;

		// Token: 0x0400019A RID: 410
		public const bool IsDatasetDefinitionRequired = true;

		// Token: 0x0400019B RID: 411
		public const bool IsDatasetEditorSettingsRequired = false;

		// Token: 0x0400019C RID: 412
		public const bool IsArtifactConfigRequired = false;

		// Token: 0x0400019D RID: 413
		public const bool IsArtifactMetadataRequired = false;

		// Token: 0x0400019E RID: 414
		public const bool IsArtifactDetailsRequired = false;

		// Token: 0x0400019F RID: 415
		public const bool IsReportDefinitionRequired = true;

		// Token: 0x040001A0 RID: 416
		public const bool IsReportRequired = false;

		// Token: 0x040001A1 RID: 417
		public const bool IsEnhancedReportRequired = false;

		// Token: 0x040001A2 RID: 418
		public const bool IsMobileStateRequired = false;

		// Token: 0x040001A3 RID: 419
		public const bool IsDiagramLayoutRequired = false;

		// Token: 0x040001A4 RID: 420
		public const bool IsReportThumbnailRequired = false;

		// Token: 0x040001A5 RID: 421
		public const bool IsCustomVisualsRequired = false;

		// Token: 0x040001A6 RID: 422
		public const bool IsStaticResourcesRequired = false;

		// Token: 0x040001A7 RID: 423
		public const bool IsDataModelRequired = false;

		// Token: 0x040001A8 RID: 424
		public const bool IsUnappliedChangesRequired = false;

		// Token: 0x040001A9 RID: 425
		public const bool IsDaxQueryViewRequired = false;

		// Token: 0x040001AA RID: 426
		public const string AnonymizedErrorDescriptionKey = "PowerBINonFatalError_ErrorDescription";

		// Token: 0x040001AB RID: 427
		public static readonly UTF8Encoding SafeUtf8NoBom = new UTF8Encoding(false, true);
	}
}
