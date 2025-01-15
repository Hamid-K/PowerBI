using System;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000665 RID: 1637
	internal static class ExtensionMethods
	{
		// Token: 0x06005ABA RID: 23226 RVA: 0x00175608 File Offset: 0x00173808
		public static bool HasFlag(this CompareOptions value, CompareOptions flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x00175610 File Offset: 0x00173810
		public static bool IsNullOrWhiteSpace(this string str)
		{
			if (str == null || str.Length <= 0)
			{
				return true;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsWhiteSpace(str[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
