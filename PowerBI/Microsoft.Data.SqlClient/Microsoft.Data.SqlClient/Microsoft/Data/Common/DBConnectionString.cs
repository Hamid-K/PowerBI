using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Data.Common
{
	// Token: 0x02000182 RID: 386
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06001CE0 RID: 7392 RVA: 0x00075D87 File Offset: 0x00073F87
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool useOdbcRules)
			: this(new DbConnectionOptions(value, synonyms), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00075D9C File Offset: 0x00073F9C
		internal DBConnectionString(DbConnectionOptions connectionOptions)
			: this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00075DAC File Offset: 0x00073FAC
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool mustCloneDictionary)
		{
			if (behavior <= KeyRestrictionBehavior.PreventUsage)
			{
				this._behavior = behavior;
				this._encryptedUsersConnectionString = connectionOptions.UsersConnectionString(false);
				this._hasPassword = connectionOptions._hasPasswordKeyword;
				this._parsetable = connectionOptions.Parsetable;
				this._keychain = connectionOptions._keyChain;
				if (this._hasPassword && !connectionOptions.HasPersistablePassword)
				{
					if (mustCloneDictionary)
					{
						this._parsetable = new Dictionary<string, string>(this._parsetable, this._parsetable.Comparer);
					}
					if (this._parsetable.ContainsKey("Password"))
					{
						this._parsetable["Password"] = "*";
					}
					if (this._parsetable.ContainsKey("pwd"))
					{
						this._parsetable["pwd"] = "*";
					}
					this._keychain = connectionOptions.ReplacePasswordPwd(out this._encryptedUsersConnectionString, true);
				}
				if (!ADP.IsEmpty(restrictions))
				{
					this._restrictionValues = DBConnectionString.ParseRestrictions(restrictions, synonyms);
					this._restrictions = restrictions;
				}
				return;
			}
			throw ADP.InvalidKeyRestrictionBehavior(behavior);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00075EB8 File Offset: 0x000740B8
		private DBConnectionString(DBConnectionString connectionString, string[] restrictionValues, KeyRestrictionBehavior behavior)
		{
			this._encryptedUsersConnectionString = connectionString._encryptedUsersConnectionString;
			this._parsetable = connectionString._parsetable;
			this._keychain = connectionString._keychain;
			this._hasPassword = connectionString._hasPassword;
			this._restrictionValues = restrictionValues;
			this._restrictions = null;
			this._behavior = behavior;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00075F10 File Offset: 0x00074110
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00075F18 File Offset: 0x00074118
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00075F20 File Offset: 0x00074120
		internal bool IsEmpty
		{
			get
			{
				return this._keychain == null;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x00075F2B File Offset: 0x0007412B
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00075F34 File Offset: 0x00074134
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && restrictionValues.Length != 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							if (!ADP.IsEmpty(restrictionValues[i]))
							{
								stringBuilder.Append(restrictionValues[i]);
								stringBuilder.Append("=;");
							}
						}
						text = stringBuilder.ToString();
					}
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		// Token: 0x17000A15 RID: 2581
		internal string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00075FAC File Offset: 0x000741AC
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00075FBC File Offset: 0x000741BC
		internal DBConnectionString Intersect(DBConnectionString entry)
		{
			KeyRestrictionBehavior keyRestrictionBehavior = this._behavior;
			string[] array = null;
			if (entry == null)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
			}
			else if (this._behavior != entry._behavior)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
				if (entry._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (!ADP.IsEmptyArray(this._restrictionValues))
					{
						if (!ADP.IsEmptyArray(entry._restrictionValues))
						{
							array = DBConnectionString.NewRestrictionAllowOnly(entry._restrictionValues, this._restrictionValues);
						}
					}
					else
					{
						array = entry._restrictionValues;
					}
				}
				else if (!ADP.IsEmptyArray(this._restrictionValues))
				{
					if (!ADP.IsEmptyArray(entry._restrictionValues))
					{
						array = DBConnectionString.NewRestrictionAllowOnly(this._restrictionValues, entry._restrictionValues);
					}
					else
					{
						array = this._restrictionValues;
					}
				}
			}
			else if (KeyRestrictionBehavior.PreventUsage == this._behavior)
			{
				if (ADP.IsEmptyArray(this._restrictionValues))
				{
					array = entry._restrictionValues;
				}
				else if (ADP.IsEmptyArray(entry._restrictionValues))
				{
					array = this._restrictionValues;
				}
				else
				{
					array = DBConnectionString.NoDuplicateUnion(this._restrictionValues, entry._restrictionValues);
				}
			}
			else if (!ADP.IsEmptyArray(this._restrictionValues) && !ADP.IsEmptyArray(entry._restrictionValues))
			{
				if (this._restrictionValues.Length <= entry._restrictionValues.Length)
				{
					array = DBConnectionString.NewRestrictionIntersect(this._restrictionValues, entry._restrictionValues);
				}
				else
				{
					array = DBConnectionString.NewRestrictionIntersect(entry._restrictionValues, this._restrictionValues);
				}
			}
			return new DBConnectionString(this, array, keyRestrictionBehavior);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0007611C File Offset: 0x0007431C
		[Conditional("DEBUG")]
		private void ValidateCombinedSet(DBConnectionString componentSet, DBConnectionString combinedSet)
		{
			if (componentSet != null && combinedSet._restrictionValues != null && componentSet._restrictionValues != null)
			{
				if (componentSet._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
					{
						KeyRestrictionBehavior behavior = combinedSet._behavior;
						return;
					}
				}
				else if (componentSet._behavior == KeyRestrictionBehavior.PreventUsage && combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
				{
					KeyRestrictionBehavior behavior2 = combinedSet._behavior;
				}
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00076170 File Offset: 0x00074370
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00076190 File Offset: 0x00074390
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			if (behavior != KeyRestrictionBehavior.AllowOnly)
			{
				if (behavior != KeyRestrictionBehavior.PreventUsage)
				{
					throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
				}
				if (this._restrictionValues != null)
				{
					foreach (string text in this._restrictionValues)
					{
						if (entry.ContainsKey(text))
						{
							return false;
						}
					}
				}
			}
			else
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00076220 File Offset: 0x00074420
		private static string[] NewRestrictionAllowOnly(string[] allowonly, string[] preventusage)
		{
			List<string> list = null;
			for (int i = 0; i < allowonly.Length; i++)
			{
				if (0 > Array.BinarySearch<string>(preventusage, allowonly[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(allowonly[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00076270 File Offset: 0x00074470
		private static string[] NewRestrictionIntersect(string[] a, string[] b)
		{
			List<string> list = null;
			for (int i = 0; i < a.Length; i++)
			{
				if (0 <= Array.BinarySearch<string>(b, a[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(a[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000762C0 File Offset: 0x000744C0
		private static string[] NoDuplicateUnion(string[] a, string[] b)
		{
			List<string> list = new List<string>(a.Length + b.Length);
			for (int i = 0; i < a.Length; i++)
			{
				list.Add(a[i]);
			}
			for (int j = 0; j < b.Length; j++)
			{
				if (0 > Array.BinarySearch<string>(a, b[j], StringComparer.Ordinal))
				{
					list.Add(b[j]);
				}
			}
			string[] array = list.ToArray();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0007632C File Offset: 0x0007452C
		private static string[] ParseRestrictions(string restrictions, Dictionary<string, string> synonyms)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder(restrictions.Length);
			int i = 0;
			int length = restrictions.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(restrictions, num, stringBuilder, false, out text, out text2);
				if (!ADP.IsEmpty(text))
				{
					string text3 = ((synonyms != null) ? synonyms[text] : text);
					if (ADP.IsEmpty(text3))
					{
						throw ADP.KeywordNotSupported(text);
					}
					list.Add(text3);
				}
			}
			return DBConnectionString.RemoveDuplicates(list.ToArray());
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000763AC File Offset: 0x000745AC
		internal static string[] RemoveDuplicates(string[] restrictions)
		{
			int num = restrictions.Length;
			if (0 < num)
			{
				Array.Sort<string>(restrictions, StringComparer.Ordinal);
				for (int i = 1; i < restrictions.Length; i++)
				{
					string text = restrictions[i - 1];
					if (text.Length == 0 || text == restrictions[i])
					{
						restrictions[i - 1] = null;
						num--;
					}
				}
				if (restrictions[restrictions.Length - 1].Length == 0)
				{
					restrictions[restrictions.Length - 1] = null;
					num--;
				}
				if (num != restrictions.Length)
				{
					string[] array = new string[num];
					num = 0;
					for (int j = 0; j < restrictions.Length; j++)
					{
						if (restrictions[j] != null)
						{
							array[num++] = restrictions[j];
						}
					}
					restrictions = array;
				}
			}
			return restrictions;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00076450 File Offset: 0x00074650
		[Conditional("DEBUG")]
		private static void Verify(string[] restrictionValues)
		{
			if (restrictionValues != null)
			{
				for (int i = 1; i < restrictionValues.Length; i++)
				{
				}
			}
		}

		// Token: 0x04000C20 RID: 3104
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x04000C21 RID: 3105
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x04000C22 RID: 3106
		private readonly NameValuePair _keychain;

		// Token: 0x04000C23 RID: 3107
		private readonly bool _hasPassword;

		// Token: 0x04000C24 RID: 3108
		private readonly string[] _restrictionValues;

		// Token: 0x04000C25 RID: 3109
		private readonly string _restrictions;

		// Token: 0x04000C26 RID: 3110
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x04000C27 RID: 3111
		private readonly string _encryptedActualConnectionString;

		// Token: 0x0200027E RID: 638
		private static class KEY
		{
			// Token: 0x040017AC RID: 6060
			internal const string Password = "Password";

			// Token: 0x040017AD RID: 6061
			internal const string PersistSecurityInfo = "Persist Security Info";

			// Token: 0x040017AE RID: 6062
			internal const string Pwd = "pwd";
		}
	}
}
