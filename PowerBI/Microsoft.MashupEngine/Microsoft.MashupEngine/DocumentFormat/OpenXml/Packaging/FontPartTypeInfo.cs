using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030BD RID: 12477
	internal static class FontPartTypeInfo
	{
		// Token: 0x0601B1D7 RID: 111063 RVA: 0x0036C1B4 File Offset: 0x0036A3B4
		internal static string GetContentType(FontPartType fontType)
		{
			switch (fontType)
			{
			case FontPartType.FontData:
				return "application/x-fontdata";
			case FontPartType.FontTtf:
				return "application/x-font-ttf";
			case FontPartType.FontOdttf:
				return "application/vnd.openxmlformats-officedocument.obfuscatedFont";
			default:
				throw new ArgumentOutOfRangeException("fontType");
			}
		}

		// Token: 0x0601B1D8 RID: 111064 RVA: 0x0036C1F4 File Offset: 0x0036A3F4
		internal static string GetTargetExtension(FontPartType fontType)
		{
			switch (fontType)
			{
			case FontPartType.FontData:
				return ".fntdata";
			case FontPartType.FontTtf:
				return ".ttf";
			case FontPartType.FontOdttf:
				return ".odttf";
			default:
				return ".font";
			}
		}
	}
}
