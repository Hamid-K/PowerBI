using System;

namespace Microsoft.BIServer.Owin.Common.Enums
{
	// Token: 0x02000025 RID: 37
	public enum ErrorCode
	{
		// Token: 0x04000065 RID: 101
		Unknown,
		// Token: 0x04000066 RID: 102
		AccessDenied,
		// Token: 0x04000067 RID: 103
		NotFound,
		// Token: 0x04000068 RID: 104
		AlreadyExists = 101,
		// Token: 0x04000069 RID: 105
		InvalidContent,
		// Token: 0x0400006A RID: 106
		InvalidName,
		// Token: 0x0400006B RID: 107
		LongPath,
		// Token: 0x0400006C RID: 108
		NotSupportedException,
		// Token: 0x0400006D RID: 109
		InvalidDataFormat,
		// Token: 0x0400006E RID: 110
		WrongItemType,
		// Token: 0x0400006F RID: 111
		SystemResourcePackageException = 200,
		// Token: 0x04000070 RID: 112
		SystemResourcePackageMetadataNotFound,
		// Token: 0x04000071 RID: 113
		SystemResourcePackageMetadataValidationFailure,
		// Token: 0x04000072 RID: 114
		SystemResourcePackageReferencedItemMissing,
		// Token: 0x04000073 RID: 115
		SystemResourcePackageRequiredItemMissing,
		// Token: 0x04000074 RID: 116
		SystemResourcePackageCannotValidateItemContentType,
		// Token: 0x04000075 RID: 117
		SystemResourcePackageCannotValidateItemExtension,
		// Token: 0x04000076 RID: 118
		SystemResourcePackageValidationFailed,
		// Token: 0x04000077 RID: 119
		SystemResourcePackageWrongType,
		// Token: 0x04000078 RID: 120
		SystemResourceProcessingException,
		// Token: 0x04000079 RID: 121
		DataSetProcessingException = 220,
		// Token: 0x0400007A RID: 122
		DataSetParameterValueNotSetException,
		// Token: 0x0400007B RID: 123
		DataSetProcessingSoapError,
		// Token: 0x0400007C RID: 124
		PowerBIReportNotSupportedVersion = 462
	}
}
