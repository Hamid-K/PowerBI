using System;
using System.ComponentModel;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000067 RID: 103
	[TypeConverter(typeof(SqlConnectionEncryptOptionConverter))]
	public sealed class SqlConnectionEncryptOption
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x00017BC4 File Offset: 0x00015DC4
		private SqlConnectionEncryptOption(string value)
		{
			this._value = value;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public static SqlConnectionEncryptOption Parse(string value)
		{
			SqlConnectionEncryptOption sqlConnectionEncryptOption;
			if (SqlConnectionEncryptOption.TryParse(value, out sqlConnectionEncryptOption))
			{
				return sqlConnectionEncryptOption;
			}
			throw ADP.InvalidConnectionOptionValue("Encrypt");
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00017BF7 File Offset: 0x00015DF7
		internal static SqlConnectionEncryptOption Parse(bool value)
		{
			if (!value)
			{
				return SqlConnectionEncryptOption.Optional;
			}
			return SqlConnectionEncryptOption.Mandatory;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00017C08 File Offset: 0x00015E08
		public static bool TryParse(string value, out SqlConnectionEncryptOption result)
		{
			string text = ((value != null) ? value.ToLower() : null);
			if (text != null)
			{
				switch (text.Length)
				{
				case 2:
					if (!(text == "no"))
					{
						goto IL_00CB;
					}
					goto IL_00B9;
				case 3:
					if (!(text == "yes"))
					{
						goto IL_00CB;
					}
					break;
				case 4:
					if (!(text == "true"))
					{
						goto IL_00CB;
					}
					break;
				case 5:
					if (!(text == "false"))
					{
						goto IL_00CB;
					}
					goto IL_00B9;
				case 6:
					if (!(text == "strict"))
					{
						goto IL_00CB;
					}
					result = SqlConnectionEncryptOption.Strict;
					return true;
				case 7:
					goto IL_00CB;
				case 8:
					if (!(text == "optional"))
					{
						goto IL_00CB;
					}
					goto IL_00B9;
				case 9:
					if (!(text == "mandatory"))
					{
						goto IL_00CB;
					}
					break;
				default:
					goto IL_00CB;
				}
				result = SqlConnectionEncryptOption.Mandatory;
				return true;
				IL_00B9:
				result = SqlConnectionEncryptOption.Optional;
				return true;
			}
			IL_00CB:
			result = null;
			return false;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00017CE4 File Offset: 0x00015EE4
		public static SqlConnectionEncryptOption Optional
		{
			get
			{
				return SqlConnectionEncryptOption.s_optional;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00017CEB File Offset: 0x00015EEB
		public static SqlConnectionEncryptOption Mandatory
		{
			get
			{
				return SqlConnectionEncryptOption.s_mandatory;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00017CF2 File Offset: 0x00015EF2
		public static SqlConnectionEncryptOption Strict
		{
			get
			{
				return SqlConnectionEncryptOption.s_strict;
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00017BF7 File Offset: 0x00015DF7
		public static implicit operator SqlConnectionEncryptOption(bool value)
		{
			if (!value)
			{
				return SqlConnectionEncryptOption.Optional;
			}
			return SqlConnectionEncryptOption.Mandatory;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00017CF9 File Offset: 0x00015EF9
		public static implicit operator bool(SqlConnectionEncryptOption value)
		{
			return !SqlConnectionEncryptOption.Optional.Equals(value);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00017D09 File Offset: 0x00015F09
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00017D14 File Offset: 0x00015F14
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				SqlConnectionEncryptOption sqlConnectionEncryptOption = obj as SqlConnectionEncryptOption;
				if (sqlConnectionEncryptOption != null)
				{
					return this.ToString().Equals(sqlConnectionEncryptOption.ToString());
				}
			}
			return false;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00017D41 File Offset: 0x00015F41
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04000182 RID: 386
		private const string TRUE = "True";

		// Token: 0x04000183 RID: 387
		private const string FALSE = "False";

		// Token: 0x04000184 RID: 388
		private const string STRICT = "Strict";

		// Token: 0x04000185 RID: 389
		private const string TRUE_LOWER = "true";

		// Token: 0x04000186 RID: 390
		private const string YES_LOWER = "yes";

		// Token: 0x04000187 RID: 391
		private const string MANDATORY_LOWER = "mandatory";

		// Token: 0x04000188 RID: 392
		private const string FALSE_LOWER = "false";

		// Token: 0x04000189 RID: 393
		private const string NO_LOWER = "no";

		// Token: 0x0400018A RID: 394
		private const string OPTIONAL_LOWER = "optional";

		// Token: 0x0400018B RID: 395
		private const string STRICT_LOWER = "strict";

		// Token: 0x0400018C RID: 396
		private readonly string _value;

		// Token: 0x0400018D RID: 397
		private static readonly SqlConnectionEncryptOption s_optional = new SqlConnectionEncryptOption("False");

		// Token: 0x0400018E RID: 398
		private static readonly SqlConnectionEncryptOption s_mandatory = new SqlConnectionEncryptOption("True");

		// Token: 0x0400018F RID: 399
		private static readonly SqlConnectionEncryptOption s_strict = new SqlConnectionEncryptOption("Strict");
	}
}
