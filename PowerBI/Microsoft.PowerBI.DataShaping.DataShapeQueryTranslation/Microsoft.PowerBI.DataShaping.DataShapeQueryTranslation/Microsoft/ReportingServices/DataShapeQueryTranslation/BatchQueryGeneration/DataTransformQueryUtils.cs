using System;
using System.Text;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200014F RID: 335
	internal static class DataTransformQueryUtils
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x000337B8 File Offset: 0x000319B8
		internal static string SanitizeRoleForColumnName(string role)
		{
			StringBuilder stringBuilder = null;
			int num = Math.Min(20, role.Length);
			for (int i = 0; i < num; i++)
			{
				char c = role[i];
				if (!DataTransformQueryUtils.IsValidRoleColumnNameChar(c))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(role, 0, i, num);
					}
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder == null)
			{
				if (role.Length > 20)
				{
					return role.Substring(0, 20);
				}
				if (role.Length == 0)
				{
					return "Out";
				}
				return role;
			}
			else
			{
				if (stringBuilder.Length == 0)
				{
					return "Out";
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00033846 File Offset: 0x00031A46
		private static bool IsValidRoleColumnNameChar(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x0400063A RID: 1594
		private const int MaxRoleColumnNameLength = 20;

		// Token: 0x0400063B RID: 1595
		internal const string DefaultOutputColumnName = "Out";
	}
}
