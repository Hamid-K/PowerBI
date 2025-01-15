using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A0 RID: 1184
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class ParseableAttribute : Attribute
	{
		// Token: 0x06001A93 RID: 6803 RVA: 0x00050060 File Offset: 0x0004E260
		public ParseableAttribute(string parseXML)
		{
			this.ParseXML = parseXML;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0005006F File Offset: 0x0004E26F
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x00050077 File Offset: 0x0004E277
		public string ParseHumanReadableString { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00050080 File Offset: 0x0004E280
		public string ParseXML { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00050088 File Offset: 0x0004E288
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00050090 File Offset: 0x0004E290
		public Type DeclaringType { get; set; }
	}
}
