using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Enums
{
	// Token: 0x020000BE RID: 190
	public enum ErrorCode
	{
		// Token: 0x040002F3 RID: 755
		Unknown,
		// Token: 0x040002F4 RID: 756
		AccessDenied,
		// Token: 0x040002F5 RID: 757
		NotFound,
		// Token: 0x040002F6 RID: 758
		AlreadyExists = 101,
		// Token: 0x040002F7 RID: 759
		InvalidContent,
		// Token: 0x040002F8 RID: 760
		InvalidName,
		// Token: 0x040002F9 RID: 761
		LongPath,
		// Token: 0x040002FA RID: 762
		NotSupportedException,
		// Token: 0x040002FB RID: 763
		InvalidDataFormat,
		// Token: 0x040002FC RID: 764
		WrongItemType,
		// Token: 0x040002FD RID: 765
		ResourceFileUploadNotSupported,
		// Token: 0x040002FE RID: 766
		ResourceFileMimeTypeNotSupported,
		// Token: 0x040002FF RID: 767
		SystemResourcePackageException = 200,
		// Token: 0x04000300 RID: 768
		SystemResourcePackageMetadataNotFound,
		// Token: 0x04000301 RID: 769
		SystemResourcePackageMetadataValidationFailure,
		// Token: 0x04000302 RID: 770
		SystemResourcePackageReferencedItemMissing,
		// Token: 0x04000303 RID: 771
		SystemResourcePackageRequiredItemMissing,
		// Token: 0x04000304 RID: 772
		SystemResourcePackageCannotValidateItemContentType,
		// Token: 0x04000305 RID: 773
		SystemResourcePackageCannotValidateItemExtension,
		// Token: 0x04000306 RID: 774
		SystemResourcePackageValidationFailed,
		// Token: 0x04000307 RID: 775
		SystemResourcePackageWrongType,
		// Token: 0x04000308 RID: 776
		SystemResourceProcessingException,
		// Token: 0x04000309 RID: 777
		DataSetProcessingException = 220,
		// Token: 0x0400030A RID: 778
		DataSetParameterValueNotSetException,
		// Token: 0x0400030B RID: 779
		DataSetProcessingSoapError,
		// Token: 0x0400030C RID: 780
		PowerBIReportNotSupportedVersion = 462,
		// Token: 0x0400030D RID: 781
		ExcelWorkbookWopiInvalidUrlException = 501,
		// Token: 0x0400030E RID: 782
		ExcelFileExtensionChangeNotAllowedException,
		// Token: 0x0400030F RID: 783
		PowerBIMigrateInvalidUrlException = 511
	}
}
