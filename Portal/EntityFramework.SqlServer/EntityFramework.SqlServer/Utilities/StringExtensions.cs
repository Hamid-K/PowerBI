using System;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002B RID: 43
	internal static class StringExtensions
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0001057E File Offset: 0x0000E77E
		public static bool EqualsIgnoreCase(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00010588 File Offset: 0x0000E788
		internal static bool EqualsOrdinal(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.Ordinal);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010592 File Offset: 0x0000E792
		public static string MigrationName(this string migrationId)
		{
			return migrationId.Substring(16);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001059C File Offset: 0x0000E79C
		public static string RestrictTo(this string s, int size)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= size)
			{
				return s;
			}
			return s.Substring(0, size);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000105B9 File Offset: 0x0000E7B9
		public static void EachLine(this string s, Action<string> action)
		{
			s.Split(StringExtensions._lineEndings, StringSplitOptions.None).Each(action);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000105CD File Offset: 0x0000E7CD
		public static bool IsValidMigrationId(this string migrationId)
		{
			return StringExtensions._migrationIdPattern.IsMatch(migrationId) || migrationId == "0";
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000105E9 File Offset: 0x0000E7E9
		public static bool IsAutomaticMigration(this string migrationId)
		{
			return migrationId.EndsWith(Strings.AutomaticMigration, StringComparison.Ordinal);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public static string ToAutomaticMigrationId(this string migrationId)
		{
			return (Convert.ToInt64(migrationId.Substring(0, 15), CultureInfo.InvariantCulture) - 1L).ToString() + migrationId.Substring(15) + "_" + Strings.AutomaticMigration;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001063A File Offset: 0x0000E83A
		public static bool IsValidUndottedName(this string name)
		{
			return !string.IsNullOrEmpty(name) && StringExtensions._undottedNameValidator.IsMatch(name);
		}

		// Token: 0x040000D2 RID: 210
		private const string StartCharacterExp = "[\\p{L}\\p{Nl}_]";

		// Token: 0x040000D3 RID: 211
		private const string OtherCharacterExp = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]";

		// Token: 0x040000D4 RID: 212
		private const string NameExp = "[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x040000D5 RID: 213
		private static readonly Regex _undottedNameValidator = new Regex("^[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040000D6 RID: 214
		private static readonly Regex _migrationIdPattern = new Regex("\\d{15}_.+");

		// Token: 0x040000D7 RID: 215
		private static readonly string[] _lineEndings = new string[] { "\r\n", "\n" };
	}
}
