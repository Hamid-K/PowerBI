using System;
using System.Globalization;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020F3 RID: 8435
	internal static class FileFormatExtension
	{
		// Token: 0x0600CF87 RID: 53127 RVA: 0x0026941A File Offset: 0x0026761A
		internal static bool Includes(this FileFormatVersions source, FileFormatVersions target)
		{
			return (source & target) == target;
		}

		// Token: 0x0600CF88 RID: 53128 RVA: 0x00294D80 File Offset: 0x00292F80
		internal static void ThrowExceptionIfNot2007Or2010(this FileFormatVersions fileFormat, string parameterName)
		{
			if (fileFormat != FileFormatVersions.Office2007 && fileFormat != FileFormatVersions.Office2010)
			{
				string text = string.Format(CultureInfo.CurrentUICulture, ExceptionMessages.FileFormatShouldBe2007Or2010, new object[] { fileFormat });
				throw new ArgumentOutOfRangeException(parameterName, text);
			}
		}
	}
}
