using System;
using Microsoft.PowerBI.Packaging.Storage;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000026 RID: 38
	public static class ReportMetadataUtils
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005300 File Offset: 0x00003500
		public static bool TryDeserializeReportMetadata(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, Version pbixFileVersion, out ReportMetadataContainer reportMetadataContainer)
		{
			byte[] contentAsBytes = PowerBIPackagingUtils.GetContentAsBytes(streamablePowerBIPackagePartContent, PowerBIPackager.IsReportMetadataOptional);
			if (contentAsBytes != null && contentAsBytes.Length != 0)
			{
				if (pbixFileVersion >= PowerBIPackager.V3ModelsVersion)
				{
					reportMetadataContainer = PowerBIPackagingUtils.DeserializeJsonPackagePartContent<ReportMetadataContainer>(streamablePowerBIPackagePartContent, 5, PowerBIPackager.IsReportMetadataOptional);
				}
				else
				{
					reportMetadataContainer = BinarySerializationReader.DeserializeBytes<ReportMetadataContainer>(contentAsBytes);
				}
				return true;
			}
			reportMetadataContainer = null;
			return false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000534B File Offset: 0x0000354B
		public static IStreamablePowerBIPackagePartContent SerializeReportMetadata(ReportMetadataContainer container)
		{
			return new StreamablePowerBIPackagePartContent(JsonConvert.SerializeObject(container), "application/json");
		}

		// Token: 0x04000094 RID: 148
		private const string JsonContentType = "application/json";
	}
}
