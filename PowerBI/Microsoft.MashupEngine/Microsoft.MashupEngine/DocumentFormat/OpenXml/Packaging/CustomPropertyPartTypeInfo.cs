using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030BF RID: 12479
	internal static class CustomPropertyPartTypeInfo
	{
		// Token: 0x0601B1D9 RID: 111065 RVA: 0x0036C230 File Offset: 0x0036A430
		internal static string GetContentType(CustomPropertyPartType partType)
		{
			switch (partType)
			{
			case CustomPropertyPartType.Spreadsheet:
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.customProperty";
			case CustomPropertyPartType.Xml:
				return "application/xml";
			default:
				throw new ArgumentOutOfRangeException("partType");
			}
		}

		// Token: 0x0601B1DA RID: 111066 RVA: 0x0036C268 File Offset: 0x0036A468
		internal static string GetTargetExtension(CustomPropertyPartType partType)
		{
			switch (partType)
			{
			case CustomPropertyPartType.Spreadsheet:
				return ".xml";
			case CustomPropertyPartType.Xml:
				return ".xml";
			default:
				return ".xml";
			}
		}
	}
}
