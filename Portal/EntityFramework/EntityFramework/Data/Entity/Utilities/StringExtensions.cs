using System;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008F RID: 143
	internal static class StringExtensions
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00010E1C File Offset: 0x0000F01C
		public static bool EqualsIgnoreCase(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00010E26 File Offset: 0x0000F026
		internal static bool EqualsOrdinal(this string s1, string s2)
		{
			return string.Equals(s1, s2, StringComparison.Ordinal);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00010E30 File Offset: 0x0000F030
		public static string MigrationName(this string migrationId)
		{
			return migrationId.Substring(16);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00010E3A File Offset: 0x0000F03A
		public static string RestrictTo(this string s, int size)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= size)
			{
				return s;
			}
			return s.Substring(0, size);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010E57 File Offset: 0x0000F057
		public static void EachLine(this string s, Action<string> action)
		{
			s.Split(StringExtensions._lineEndings, StringSplitOptions.None).Each(action);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00010E6B File Offset: 0x0000F06B
		public static bool IsValidMigrationId(this string migrationId)
		{
			return StringExtensions._migrationIdPattern.IsMatch(migrationId) || migrationId == "0";
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00010E87 File Offset: 0x0000F087
		public static bool IsAutomaticMigration(this string migrationId)
		{
			return migrationId.EndsWith(Strings.AutomaticMigration, StringComparison.Ordinal);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00010E98 File Offset: 0x0000F098
		public static string ToAutomaticMigrationId(this string migrationId)
		{
			return (Convert.ToInt64(migrationId.Substring(0, 15), CultureInfo.InvariantCulture) - 1L).ToString() + migrationId.Substring(15) + "_" + Strings.AutomaticMigration;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00010EDA File Offset: 0x0000F0DA
		public static bool IsValidUndottedName(this string name)
		{
			return !string.IsNullOrEmpty(name) && StringExtensions._undottedNameValidator.IsMatch(name);
		}

		// Token: 0x04000118 RID: 280
		private const string StartCharacterExp = "[\\p{L}\\p{Nl}_]";

		// Token: 0x04000119 RID: 281
		private const string OtherCharacterExp = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]";

		// Token: 0x0400011A RID: 282
		private const string NameExp = "[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x0400011B RID: 283
		private static readonly Regex _undottedNameValidator = new Regex("^[\\p{L}\\p{Nl}_][\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x0400011C RID: 284
		private static readonly Regex _migrationIdPattern = new Regex("\\d{15}_.+");

		// Token: 0x0400011D RID: 285
		private static readonly string[] _lineEndings = new string[] { "\r\n", "\n" };
	}
}
