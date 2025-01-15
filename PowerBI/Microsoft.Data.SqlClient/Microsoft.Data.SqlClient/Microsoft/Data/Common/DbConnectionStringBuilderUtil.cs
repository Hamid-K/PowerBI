using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.Common
{
	// Token: 0x02000179 RID: 377
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x06001C81 RID: 7297 RVA: 0x000738DC File Offset: 0x00071ADC
		internal static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000739D4 File Offset: 0x00071BD4
		internal static bool ConvertToIntegratedSecurity(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00073AF0 File Offset: 0x00071CF0
		internal static int ConvertToInt32(object value)
		{
			int num;
			try
			{
				num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(int), ex);
			}
			return num;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00073B34 File Offset: 0x00071D34
		internal static string ConvertToString(object value)
		{
			string text;
			try
			{
				text = Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(string), ex);
			}
			return text;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00073B78 File Offset: 0x00071D78
		internal static bool TryConvertToPoolBlockingPeriod(string value, out PoolBlockingPeriod result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "Auto"))
			{
				result = PoolBlockingPeriod.Auto;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "AlwaysBlock"))
			{
				result = PoolBlockingPeriod.AlwaysBlock;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "NeverBlock"))
			{
				result = PoolBlockingPeriod.NeverBlock;
				return true;
			}
			result = PoolBlockingPeriod.Auto;
			return false;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00073BCE File Offset: 0x00071DCE
		internal static bool IsValidPoolBlockingPeriodValue(PoolBlockingPeriod value)
		{
			return value == PoolBlockingPeriod.Auto || value == PoolBlockingPeriod.AlwaysBlock || value == PoolBlockingPeriod.NeverBlock;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00073BE0 File Offset: 0x00071DE0
		internal static string PoolBlockingPeriodToString(PoolBlockingPeriod value)
		{
			string text;
			if (value != PoolBlockingPeriod.AlwaysBlock)
			{
				if (value != PoolBlockingPeriod.NeverBlock)
				{
					text = "Auto";
				}
				else
				{
					text = "NeverBlock";
				}
			}
			else
			{
				text = "AlwaysBlock";
			}
			return text;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x00073C10 File Offset: 0x00071E10
		internal static PoolBlockingPeriod ConvertToPoolBlockingPeriod(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				PoolBlockingPeriod poolBlockingPeriod;
				if (DbConnectionStringBuilderUtil.TryConvertToPoolBlockingPeriod(text, out poolBlockingPeriod))
				{
					return poolBlockingPeriod;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToPoolBlockingPeriod(text, out poolBlockingPeriod))
				{
					return poolBlockingPeriod;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				PoolBlockingPeriod poolBlockingPeriod3;
				if (value is PoolBlockingPeriod)
				{
					PoolBlockingPeriod poolBlockingPeriod2 = (PoolBlockingPeriod)value;
					poolBlockingPeriod3 = poolBlockingPeriod2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(PoolBlockingPeriod), null);
					}
					try
					{
						poolBlockingPeriod3 = (PoolBlockingPeriod)Enum.ToObject(typeof(PoolBlockingPeriod), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(PoolBlockingPeriod), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidPoolBlockingPeriodValue(poolBlockingPeriod3))
				{
					return poolBlockingPeriod3;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)poolBlockingPeriod3);
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00073CE4 File Offset: 0x00071EE4
		internal static bool TryConvertToApplicationIntent(string value, out ApplicationIntent result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadOnly"))
			{
				result = ApplicationIntent.ReadOnly;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadWrite"))
			{
				result = ApplicationIntent.ReadWrite;
				return true;
			}
			result = ApplicationIntent.ReadWrite;
			return false;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00073D18 File Offset: 0x00071F18
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00073D24 File Offset: 0x00071F24
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00073D38 File Offset: 0x00071F38
		internal static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				ApplicationIntent applicationIntent;
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				ApplicationIntent applicationIntent3;
				if (value is ApplicationIntent)
				{
					ApplicationIntent applicationIntent2 = (ApplicationIntent)value;
					applicationIntent3 = applicationIntent2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), null);
					}
					try
					{
						applicationIntent3 = (ApplicationIntent)Enum.ToObject(typeof(ApplicationIntent), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(applicationIntent3))
				{
					return applicationIntent3;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)applicationIntent3);
			}
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00073E0C File Offset: 0x0007200C
		internal static bool TryConvertToAuthenticationType(string value, out SqlAuthenticationMethod result)
		{
			bool flag = false;
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Sql Password") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.SqlPassword, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.SqlPassword;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Password") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryPassword, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryPassword;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Integrated") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryIntegrated, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryIntegrated;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Interactive") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryInteractive, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryInteractive;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Service Principal") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryServicePrincipal, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryServicePrincipal;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Device Code Flow") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Managed Identity") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryManagedIdentity, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryManagedIdentity;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory MSI") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryMSI, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryMSI;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Default") || StringComparer.InvariantCultureIgnoreCase.Equals(value, Convert.ToString(SqlAuthenticationMethod.ActiveDirectoryDefault, CultureInfo.InvariantCulture)))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryDefault;
				flag = true;
			}
			else
			{
				result = DbConnectionStringDefaults.Authentication;
			}
			return flag;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00074020 File Offset: 0x00072220
		internal static bool TryConvertToColumnEncryptionSetting(string value, out SqlConnectionColumnEncryptionSetting result)
		{
			bool flag = false;
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Enabled"))
			{
				result = SqlConnectionColumnEncryptionSetting.Enabled;
				flag = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Disabled"))
			{
				result = SqlConnectionColumnEncryptionSetting.Disabled;
				flag = true;
			}
			else
			{
				result = SqlConnectionColumnEncryptionSetting.Disabled;
			}
			return flag;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00073D18 File Offset: 0x00071F18
		internal static bool IsValidColumnEncryptionSetting(SqlConnectionColumnEncryptionSetting value)
		{
			return value == SqlConnectionColumnEncryptionSetting.Enabled || value == SqlConnectionColumnEncryptionSetting.Disabled;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00074068 File Offset: 0x00072268
		internal static string ColumnEncryptionSettingToString(SqlConnectionColumnEncryptionSetting value)
		{
			string text;
			if (value != SqlConnectionColumnEncryptionSetting.Disabled)
			{
				if (value == SqlConnectionColumnEncryptionSetting.Enabled)
				{
					text = "Enabled";
				}
				else
				{
					text = null;
				}
			}
			else
			{
				text = "Disabled";
			}
			return text;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0007408F File Offset: 0x0007228F
		internal static bool IsValidAuthenticationTypeValue(SqlAuthenticationMethod value)
		{
			return value == SqlAuthenticationMethod.SqlPassword || value == SqlAuthenticationMethod.ActiveDirectoryPassword || value == SqlAuthenticationMethod.ActiveDirectoryIntegrated || value == SqlAuthenticationMethod.ActiveDirectoryInteractive || value == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal || value == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow || value == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity || value == SqlAuthenticationMethod.ActiveDirectoryMSI || value == SqlAuthenticationMethod.ActiveDirectoryDefault || value == SqlAuthenticationMethod.NotSpecified;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000740BC File Offset: 0x000722BC
		internal static string AuthenticationTypeToString(SqlAuthenticationMethod value)
		{
			string text;
			switch (value)
			{
			case SqlAuthenticationMethod.SqlPassword:
				text = "Sql Password";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryPassword:
				text = "Active Directory Password";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
				text = "Active Directory Integrated";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryInteractive:
				text = "Active Directory Interactive";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryServicePrincipal:
				text = "Active Directory Service Principal";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow:
				text = "Active Directory Device Code Flow";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryManagedIdentity:
				text = "Active Directory Managed Identity";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryMSI:
				text = "Active Directory MSI";
				break;
			case SqlAuthenticationMethod.ActiveDirectoryDefault:
				text = "Active Directory Default";
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00074144 File Offset: 0x00072344
		internal static SqlAuthenticationMethod ConvertToAuthenticationType(string keyword, object value)
		{
			if (value == null)
			{
				return DbConnectionStringDefaults.Authentication;
			}
			string text = value as string;
			if (text != null)
			{
				SqlAuthenticationMethod sqlAuthenticationMethod;
				if (DbConnectionStringBuilderUtil.TryConvertToAuthenticationType(text, out sqlAuthenticationMethod))
				{
					return sqlAuthenticationMethod;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToAuthenticationType(text, out sqlAuthenticationMethod))
				{
					return sqlAuthenticationMethod;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlAuthenticationMethod sqlAuthenticationMethod3;
				if (value is SqlAuthenticationMethod)
				{
					SqlAuthenticationMethod sqlAuthenticationMethod2 = (SqlAuthenticationMethod)value;
					sqlAuthenticationMethod3 = sqlAuthenticationMethod2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlAuthenticationMethod), null);
					}
					try
					{
						sqlAuthenticationMethod3 = (SqlAuthenticationMethod)Enum.ToObject(typeof(SqlAuthenticationMethod), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlAuthenticationMethod), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidAuthenticationTypeValue(sqlAuthenticationMethod3))
				{
					return sqlAuthenticationMethod3;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlAuthenticationMethod), (int)sqlAuthenticationMethod3);
			}
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00074220 File Offset: 0x00072420
		internal static SqlConnectionColumnEncryptionSetting ConvertToColumnEncryptionSetting(string keyword, object value)
		{
			if (value == null)
			{
				return SqlConnectionColumnEncryptionSetting.Disabled;
			}
			string text = value as string;
			if (text != null)
			{
				SqlConnectionColumnEncryptionSetting sqlConnectionColumnEncryptionSetting;
				if (DbConnectionStringBuilderUtil.TryConvertToColumnEncryptionSetting(text, out sqlConnectionColumnEncryptionSetting))
				{
					return sqlConnectionColumnEncryptionSetting;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToColumnEncryptionSetting(text, out sqlConnectionColumnEncryptionSetting))
				{
					return sqlConnectionColumnEncryptionSetting;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlConnectionColumnEncryptionSetting sqlConnectionColumnEncryptionSetting3;
				if (value is SqlConnectionColumnEncryptionSetting)
				{
					SqlConnectionColumnEncryptionSetting sqlConnectionColumnEncryptionSetting2 = (SqlConnectionColumnEncryptionSetting)value;
					sqlConnectionColumnEncryptionSetting3 = sqlConnectionColumnEncryptionSetting2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionColumnEncryptionSetting), null);
					}
					try
					{
						sqlConnectionColumnEncryptionSetting3 = (SqlConnectionColumnEncryptionSetting)Enum.ToObject(typeof(SqlConnectionColumnEncryptionSetting), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionColumnEncryptionSetting), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidColumnEncryptionSetting(sqlConnectionColumnEncryptionSetting3))
				{
					return sqlConnectionColumnEncryptionSetting3;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlConnectionColumnEncryptionSetting), (int)sqlConnectionColumnEncryptionSetting3);
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000742F8 File Offset: 0x000724F8
		internal static bool TryConvertToAttestationProtocol(string value, out SqlConnectionAttestationProtocol result)
		{
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "HGS"))
			{
				result = SqlConnectionAttestationProtocol.HGS;
				return true;
			}
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "AAS"))
			{
				result = SqlConnectionAttestationProtocol.AAS;
				return true;
			}
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "None"))
			{
				result = SqlConnectionAttestationProtocol.None;
				return true;
			}
			result = SqlConnectionAttestationProtocol.NotSpecified;
			return false;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0007434E File Offset: 0x0007254E
		internal static bool IsValidAttestationProtocol(SqlConnectionAttestationProtocol value)
		{
			return value == SqlConnectionAttestationProtocol.NotSpecified || value == SqlConnectionAttestationProtocol.HGS || value == SqlConnectionAttestationProtocol.AAS || value == SqlConnectionAttestationProtocol.None;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00074364 File Offset: 0x00072564
		internal static string AttestationProtocolToString(SqlConnectionAttestationProtocol value)
		{
			string text;
			switch (value)
			{
			case SqlConnectionAttestationProtocol.AAS:
				text = "AAS";
				break;
			case SqlConnectionAttestationProtocol.None:
				text = "None";
				break;
			case SqlConnectionAttestationProtocol.HGS:
				text = "HGS";
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000743A4 File Offset: 0x000725A4
		internal static SqlConnectionAttestationProtocol ConvertToAttestationProtocol(string keyword, object value)
		{
			if (value == null)
			{
				return SqlConnectionAttestationProtocol.NotSpecified;
			}
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				SqlConnectionAttestationProtocol sqlConnectionAttestationProtocol;
				if (DbConnectionStringBuilderUtil.TryConvertToAttestationProtocol(text, out sqlConnectionAttestationProtocol))
				{
					return sqlConnectionAttestationProtocol;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlConnectionAttestationProtocol sqlConnectionAttestationProtocol3;
				if (value is SqlConnectionAttestationProtocol)
				{
					SqlConnectionAttestationProtocol sqlConnectionAttestationProtocol2 = (SqlConnectionAttestationProtocol)value;
					sqlConnectionAttestationProtocol3 = sqlConnectionAttestationProtocol2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionAttestationProtocol), null);
					}
					try
					{
						sqlConnectionAttestationProtocol3 = (SqlConnectionAttestationProtocol)Enum.ToObject(typeof(SqlConnectionAttestationProtocol), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionAttestationProtocol), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidAttestationProtocol(sqlConnectionAttestationProtocol3))
				{
					return sqlConnectionAttestationProtocol3;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlConnectionAttestationProtocol), (int)sqlConnectionAttestationProtocol3);
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00074470 File Offset: 0x00072670
		internal static SqlConnectionEncryptOption ConvertToSqlConnectionEncryptOption(string keyword, object value)
		{
			if (value == null)
			{
				return DbConnectionStringDefaults.Encrypt;
			}
			SqlConnectionEncryptOption sqlConnectionEncryptOption = value as SqlConnectionEncryptOption;
			if (sqlConnectionEncryptOption != null)
			{
				return sqlConnectionEncryptOption;
			}
			string text = value as string;
			if (text != null)
			{
				return SqlConnectionEncryptOption.Parse(text);
			}
			if (value is bool)
			{
				bool flag = (bool)value;
				return SqlConnectionEncryptOption.Parse(flag);
			}
			throw ADP.InvalidConnectionOptionValue(keyword);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000744C0 File Offset: 0x000726C0
		static DbConnectionStringBuilderUtil()
		{
			foreach (object obj in Enum.GetValues(typeof(SqlConnectionIPAddressPreference)))
			{
				SqlConnectionIPAddressPreference sqlConnectionIPAddressPreference = (SqlConnectionIPAddressPreference)obj;
				DbConnectionStringBuilderUtil.s_preferenceNames.Add(sqlConnectionIPAddressPreference.ToString(), sqlConnectionIPAddressPreference);
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00074544 File Offset: 0x00072744
		internal static bool TryConvertToIPAddressPreference(string value, out SqlConnectionIPAddressPreference result)
		{
			if (!DbConnectionStringBuilderUtil.s_preferenceNames.TryGetValue(value, out result))
			{
				result = SqlConnectionIPAddressPreference.IPv4First;
				return false;
			}
			return true;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00073BCE File Offset: 0x00071DCE
		internal static bool IsValidIPAddressPreference(SqlConnectionIPAddressPreference value)
		{
			return value == SqlConnectionIPAddressPreference.IPv4First || value == SqlConnectionIPAddressPreference.IPv6First || value == SqlConnectionIPAddressPreference.UsePlatformDefault;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0007455A File Offset: 0x0007275A
		internal static string IPAddressPreferenceToString(SqlConnectionIPAddressPreference value)
		{
			return Enum.GetName(typeof(SqlConnectionIPAddressPreference), value);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00074574 File Offset: 0x00072774
		internal static SqlConnectionIPAddressPreference ConvertToIPAddressPreference(string keyword, object value)
		{
			if (value == null)
			{
				return SqlConnectionIPAddressPreference.IPv4First;
			}
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				SqlConnectionIPAddressPreference sqlConnectionIPAddressPreference;
				if (DbConnectionStringBuilderUtil.TryConvertToIPAddressPreference(text, out sqlConnectionIPAddressPreference))
				{
					return sqlConnectionIPAddressPreference;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlConnectionIPAddressPreference sqlConnectionIPAddressPreference3;
				if (value is SqlConnectionIPAddressPreference)
				{
					SqlConnectionIPAddressPreference sqlConnectionIPAddressPreference2 = (SqlConnectionIPAddressPreference)value;
					sqlConnectionIPAddressPreference3 = sqlConnectionIPAddressPreference2;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionIPAddressPreference), null);
					}
					try
					{
						sqlConnectionIPAddressPreference3 = (SqlConnectionIPAddressPreference)Enum.ToObject(typeof(SqlConnectionIPAddressPreference), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionIPAddressPreference), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidIPAddressPreference(sqlConnectionIPAddressPreference3))
				{
					return sqlConnectionIPAddressPreference3;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlConnectionIPAddressPreference), (int)sqlConnectionIPAddressPreference3);
			}
		}

		// Token: 0x04000B75 RID: 2933
		private const string SqlPasswordString = "Sql Password";

		// Token: 0x04000B76 RID: 2934
		private const string ActiveDirectoryPasswordString = "Active Directory Password";

		// Token: 0x04000B77 RID: 2935
		private const string ActiveDirectoryIntegratedString = "Active Directory Integrated";

		// Token: 0x04000B78 RID: 2936
		private const string ActiveDirectoryInteractiveString = "Active Directory Interactive";

		// Token: 0x04000B79 RID: 2937
		private const string ActiveDirectoryServicePrincipalString = "Active Directory Service Principal";

		// Token: 0x04000B7A RID: 2938
		private const string ActiveDirectoryDeviceCodeFlowString = "Active Directory Device Code Flow";

		// Token: 0x04000B7B RID: 2939
		internal const string ActiveDirectoryManagedIdentityString = "Active Directory Managed Identity";

		// Token: 0x04000B7C RID: 2940
		internal const string ActiveDirectoryMSIString = "Active Directory MSI";

		// Token: 0x04000B7D RID: 2941
		internal const string ActiveDirectoryDefaultString = "Active Directory Default";

		// Token: 0x04000B7E RID: 2942
		private const string SqlCertificateString = "Sql Certificate";

		// Token: 0x04000B7F RID: 2943
		private static readonly Dictionary<string, SqlConnectionIPAddressPreference> s_preferenceNames = new Dictionary<string, SqlConnectionIPAddressPreference>(StringComparer.InvariantCultureIgnoreCase);
	}
}
