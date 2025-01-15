using System;
using Microsoft.PowerBI.Packaging.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000027 RID: 39
	public static class ReportSettingsUtils
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00005360 File Offset: 0x00003560
		public static bool TryDeserializeReportSettings(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent, Version pbixFileVersion, out ReportSettingsContainer reportSettingsContainer)
		{
			byte[] contentAsBytes = PowerBIPackagingUtils.GetContentAsBytes(streamablePowerBIPackagePartContent, PowerBIPackager.IsReportSettingsOptional);
			if (contentAsBytes != null && contentAsBytes.Length != 0)
			{
				if (pbixFileVersion >= PowerBIPackager.V3ModelsVersion)
				{
					JObject jobject = PowerBIPackagingUtils.ReadJsonPackagePartContent(streamablePowerBIPackagePartContent, false);
					reportSettingsContainer = PowerBIPackagingUtils.DeserializeSettingsJson<ReportSettingsContainer>(jobject, 4);
				}
				else
				{
					reportSettingsContainer = new ReportSettingsContainer(BinarySerializationReader.DeserializeBytes<ReportSettings>(contentAsBytes), null, 1);
				}
				return true;
			}
			reportSettingsContainer = null;
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000053B5 File Offset: 0x000035B5
		public static IStreamablePowerBIPackagePartContent SerializeReportSettings(ReportSettingsContainer reportSettingsContainer)
		{
			return new StreamablePowerBIPackagePartContent(JsonConvert.SerializeObject(reportSettingsContainer, new JsonSerializerSettings
			{
				DefaultValueHandling = DefaultValueHandling.Ignore
			}), "application/json");
		}

		// Token: 0x04000095 RID: 149
		private const string JsonContentType = "application/json";
	}
}
