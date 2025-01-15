using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200032F RID: 815
	public static class ColumnParsingUtils
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00065AF4 File Offset: 0x00063CF4
		public static bool TryParse(string str, out string name, out string source)
		{
			Contracts.CheckNonWhiteSpace(str, "str");
			int num = str.IndexOf(':');
			if (num < 0)
			{
				name = str;
				source = str;
				return true;
			}
			if (0 < num && num < str.Length - 1)
			{
				name = str.Substring(0, num);
				source = str.Substring(num + 1);
				return !source.Contains(":");
			}
			name = null;
			source = null;
			return false;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00065B60 File Offset: 0x00063D60
		public static bool TryParse(string str, out string name, out string source, out string extra)
		{
			Contracts.CheckNonWhiteSpace(str, "str");
			extra = null;
			int num = str.IndexOf(':');
			if (num < 0)
			{
				name = str;
				source = str;
				return true;
			}
			if (num == 0 || num >= str.Length - 1)
			{
				name = null;
				source = null;
				return false;
			}
			name = str.Substring(0, num);
			int num2 = num + 1;
			num = str.LastIndexOf(':');
			if (num == num2 - 1)
			{
				source = str.Substring(num2);
				return true;
			}
			if (num == num2 || num >= str.Length - 1)
			{
				name = null;
				source = null;
				return false;
			}
			extra = str.Substring(num2, num - num2);
			source = str.Substring(num + 1);
			return true;
		}
	}
}
