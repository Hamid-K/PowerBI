using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030C9 RID: 12489
	internal static class CustomXmlPartTypeInfo
	{
		// Token: 0x0601B1E3 RID: 111075 RVA: 0x0036C4C8 File Offset: 0x0036A6C8
		internal static string GetContentType(CustomXmlPartType partType)
		{
			switch (partType)
			{
			case CustomXmlPartType.AdditionalCharacteristics:
				return "application/xml";
			case CustomXmlPartType.Bibliography:
				return "application/xml";
			case CustomXmlPartType.CustomXml:
				return "application/xml";
			case CustomXmlPartType.InkContent:
				return "application/inkml+xml";
			default:
				throw new ArgumentOutOfRangeException("partType");
			}
		}

		// Token: 0x0601B1E4 RID: 111076 RVA: 0x0036C514 File Offset: 0x0036A714
		internal static string GetTargetExtension(CustomXmlPartType partType)
		{
			switch (partType)
			{
			case CustomXmlPartType.AdditionalCharacteristics:
				return ".xml";
			case CustomXmlPartType.Bibliography:
				return ".xml";
			case CustomXmlPartType.CustomXml:
				return ".xml";
			case CustomXmlPartType.InkContent:
				return ".xml";
			default:
				return ".xml";
			}
		}
	}
}
