using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	internal class ImportanceParameterHelper : OptionsHelper<ImportanceParameterType>
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x00090217 File Offset: 0x0008E417
		private ImportanceParameterHelper()
		{
			base.AddOptionMapping(ImportanceParameterType.Low, "LOW");
			base.AddOptionMapping(ImportanceParameterType.High, "HIGH");
			base.AddOptionMapping(ImportanceParameterType.Medium, "MEDIUM");
		}

		// Token: 0x04000AFC RID: 2812
		internal static readonly ImportanceParameterHelper Instance = new ImportanceParameterHelper();
	}
}
