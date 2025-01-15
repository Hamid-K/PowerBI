using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020F4 RID: 8436
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	internal sealed class OfficeAvailabilityAttribute : Attribute
	{
		// Token: 0x170031DA RID: 12762
		// (get) Token: 0x0600CF89 RID: 53129 RVA: 0x00294DBE File Offset: 0x00292FBE
		// (set) Token: 0x0600CF8A RID: 53130 RVA: 0x00294DC6 File Offset: 0x00292FC6
		public FileFormatVersions OfficeVersion { get; internal set; }

		// Token: 0x0600CF8B RID: 53131 RVA: 0x00294DCF File Offset: 0x00292FCF
		public OfficeAvailabilityAttribute(FileFormatVersions officeVersion)
		{
			this.OfficeVersion = officeVersion;
		}
	}
}
