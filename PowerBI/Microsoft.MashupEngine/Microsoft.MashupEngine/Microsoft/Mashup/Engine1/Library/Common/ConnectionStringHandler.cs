using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200103D RID: 4157
	internal class ConnectionStringHandler
	{
		// Token: 0x06006C73 RID: 27763 RVA: 0x0017590C File Offset: 0x00173B0C
		public ConnectionStringHandler(bool isOdbc, bool supportsImpersonation, string userNameKey, string passwordKey, string integratedSecurityKey, string[] credentialOnlyProperties, string[] sourceOnlyProperties, string[] disallowedProperties)
		{
			this.isOdbc = isOdbc;
			this.supportsImpersonation = supportsImpersonation;
			this.userNameKey = userNameKey;
			this.passwordKey = passwordKey;
			this.integratedSecurityKey = integratedSecurityKey;
			this.sourceOnlyProperties = new HashSet<string>(sourceOnlyProperties, StringComparer.OrdinalIgnoreCase);
			this.credentialOnlyProperties = new HashSet<string>(credentialOnlyProperties, StringComparer.OrdinalIgnoreCase);
			this.credentialOnlyProperties.Add(userNameKey);
			this.credentialOnlyProperties.Add(passwordKey);
			this.credentialOnlyProperties.Add(integratedSecurityKey);
			this.disallowedProperties = new HashSet<string>(disallowedProperties, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x06006C74 RID: 27764 RVA: 0x001759A3 File Offset: 0x00173BA3
		public string UserNameKey
		{
			get
			{
				return this.userNameKey;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x06006C75 RID: 27765 RVA: 0x001759AB File Offset: 0x00173BAB
		public string PasswordKey
		{
			get
			{
				return this.passwordKey;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x001759B3 File Offset: 0x00173BB3
		public string IntegratedSecurityKey
		{
			get
			{
				return this.integratedSecurityKey;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06006C77 RID: 27767 RVA: 0x001759BB File Offset: 0x00173BBB
		public bool SupportsImpersonation
		{
			get
			{
				return this.supportsImpersonation;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x06006C78 RID: 27768 RVA: 0x001759C3 File Offset: 0x00173BC3
		protected virtual IEnumerable<string> HostNameKeys
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x001759CC File Offset: 0x00173BCC
		public string GetValidatedString(Value value, string dataSourceName, IEngineHost host)
		{
			return ConnectionStringHandler.HandleFormatExceptions<string>(dataSourceName, value, delegate
			{
				string @string = this.GetString(value);
				if (host.QueryService<IExtensibilityService>() == null)
				{
					this.ValidateSource(@string);
				}
				return @string;
			});
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x00175A0C File Offset: 0x00173C0C
		public string GetString(Value value)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Text)
			{
				return value.AsString;
			}
			if (kind == ValueKind.Record)
			{
				return this.GetString(value.AsRecord);
			}
			throw new FormatException(Strings.GenericProviders_UnsupportedConnectionStringType("connectionString", Enum.GetName(typeof(ValueKind), value.Type.TypeKind), Enum.GetName(typeof(ValueKind), ValueKind.Text), Enum.GetName(typeof(ValueKind), ValueKind.Record)));
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x00175A9C File Offset: 0x00173C9C
		public string GetString(RecordValue record)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(record.Count);
			foreach (NamedValue namedValue in record.GetFields())
			{
				Value value = namedValue.Value;
				ValueKind kind = value.Kind;
				string text;
				if (kind != ValueKind.Number)
				{
					if (kind != ValueKind.Text)
					{
						throw new FormatException(Strings.GenericProviders_UnsupportedConnectionStringValueType(namedValue.Key, Enum.GetName(typeof(ValueKind), value.Type.TypeKind), Enum.GetName(typeof(ValueKind), ValueKind.Number), Enum.GetName(typeof(ValueKind), ValueKind.Text)));
					}
					text = value.AsString;
				}
				else
				{
					text = value.AsNumber.ToSource();
				}
				dictionary[namedValue.Key] = text;
			}
			return this.GetString(dictionary);
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x00175BAC File Offset: 0x00173DAC
		public string GetString(Dictionary<string, string> keyValuePairs)
		{
			string connectionString;
			try
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder();
				foreach (KeyValuePair<string, string> keyValuePair in keyValuePairs)
				{
					dbConnectionStringBuilder[keyValuePair.Key] = this.EscapeInboundValue(keyValuePair.Value);
				}
				connectionString = dbConnectionStringBuilder.ConnectionString;
			}
			catch (ArgumentException ex)
			{
				throw new FormatException(ex.Message, ex);
			}
			return connectionString;
		}

		// Token: 0x06006C7D RID: 27773 RVA: 0x00175C3C File Offset: 0x00173E3C
		public Dictionary<string, string> GetKeyValuePairs(string connectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder(connectionString);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in dbConnectionStringBuilder.Keys)
			{
				string text = (string)obj;
				string text2 = this.EscapeOutboundValue(dbConnectionStringBuilder[text].ToString());
				dictionary.Add(text, text2);
			}
			return dictionary;
		}

		// Token: 0x06006C7E RID: 27774 RVA: 0x00175CC0 File Offset: 0x00173EC0
		public void ValidateSource(string connectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder(connectionString);
			this.ValidateSource(dbConnectionStringBuilder);
		}

		// Token: 0x06006C7F RID: 27775 RVA: 0x00175CDC File Offset: 0x00173EDC
		public void ValidateSource(DbConnectionStringBuilder builder)
		{
			foreach (object obj in builder.Keys)
			{
				string text = (string)obj;
				this.ValidateSourceProperty(text, builder[text]);
			}
		}

		// Token: 0x06006C80 RID: 27776 RVA: 0x00175D3C File Offset: 0x00173F3C
		public void ValidateSourceWithPermission(string connectionString, IResource resource)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder(connectionString);
			this.ValidateSourceWithPermission(dbConnectionStringBuilder, resource);
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00175D5C File Offset: 0x00173F5C
		public void ValidateSourceWithPermission(DbConnectionStringBuilder builder, IResource resource)
		{
			foreach (object obj in builder.Keys)
			{
				string text = (string)obj;
				this.ValidateSourcePropertyWithPermission(text, builder[text], resource);
			}
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x00175DC0 File Offset: 0x00173FC0
		public void ValidateCredential(DbConnectionStringBuilder builder)
		{
			foreach (object obj in builder.Keys)
			{
				string text = (string)obj;
				this.ValidateCredentialProperty(text, builder[text]);
			}
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x00175E20 File Offset: 0x00174020
		public DbConnectionStringBuilder NewBuilder(string connectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder2;
			try
			{
				if (connectionString.Trim().Length == 0)
				{
					throw new FormatException(Strings.GenericProviders_EmptyConnectionString);
				}
				DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder();
				dbConnectionStringBuilder.ConnectionString = connectionString;
				dbConnectionStringBuilder2 = dbConnectionStringBuilder;
			}
			catch (ArgumentException ex)
			{
				throw new FormatException(ex.Message, ex);
			}
			return dbConnectionStringBuilder2;
		}

		// Token: 0x06006C84 RID: 27780 RVA: 0x00175E7C File Offset: 0x0017407C
		public string ResourcePathNormalize(string connectionString)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder(connectionString);
			DbConnectionStringBuilder dbConnectionStringBuilder2 = this.NewBuilder();
			foreach (string text in from string k in dbConnectionStringBuilder.Keys
				orderby k
				select k)
			{
				object obj = dbConnectionStringBuilder[text];
				string text2 = obj as string;
				if (text2 != null)
				{
					obj = this.EscapeInboundValue(this.EscapeOutboundValue(text2));
				}
				if (text2 == null || text2.Length > 0)
				{
					dbConnectionStringBuilder2[text.ToLowerInvariant()] = obj;
				}
			}
			return dbConnectionStringBuilder2.ConnectionString;
		}

		// Token: 0x06006C85 RID: 27781 RVA: 0x00175F44 File Offset: 0x00174144
		public virtual void ValidateSourceProperty(string key, object value)
		{
			this.ValidatePropertyAllowed(key);
			if (this.credentialOnlyProperties.Contains(key))
			{
				throw new FormatException(Strings.GenericProviders_InvalidSourceProperty(key));
			}
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ValidateSourcePropertyWithPermission(string key, object value, IResource resource)
		{
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x00175F6C File Offset: 0x0017416C
		public virtual void ValidateCredentialProperty(string key, object value)
		{
			this.ValidatePropertyAllowed(key);
			if (this.sourceOnlyProperties.Contains(key))
			{
				throw new FormatException(Strings.GenericProviders_InvalidCredentialProperty(key));
			}
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x00175F94 File Offset: 0x00174194
		public DbConnectionStringBuilder NewBuilder()
		{
			return new DbConnectionStringBuilder(this.isOdbc);
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x00175FA4 File Offset: 0x001741A4
		public bool TryGetHostName(string connectionString, out string hostName)
		{
			hostName = null;
			DbConnectionStringBuilder dbConnectionStringBuilder = this.NewBuilder(connectionString);
			foreach (string text in this.HostNameKeys)
			{
				object obj;
				if (dbConnectionStringBuilder.TryGetValue(text, out obj))
				{
					if (hostName != null)
					{
						hostName = null;
						return false;
					}
					hostName = obj.ToString();
				}
			}
			return hostName != null;
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x00176020 File Offset: 0x00174220
		private void ValidatePropertyAllowed(string key)
		{
			if (this.disallowedProperties.Contains(key))
			{
				throw new FormatException(Strings.GenericProviders_PropertyNotSupported(key));
			}
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual string EscapeInboundValue(string value)
		{
			return value;
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual string EscapeOutboundValue(string value)
		{
			return value;
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x00176044 File Offset: 0x00174244
		public static void HandleFormatExceptions(string dataSourceName, Value connectionStringValue, Action action)
		{
			ConnectionStringHandler.HandleFormatExceptions<int>(dataSourceName, connectionStringValue, delegate
			{
				action();
				return 0;
			});
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x00176074 File Offset: 0x00174274
		public static T HandleFormatExceptions<T>(string dataSourceName, Value connectionStringValue, Func<T> func)
		{
			T t;
			try
			{
				t = func();
			}
			catch (FormatException ex)
			{
				string text = Strings.GenericProviders_InvalidConnectionString(ex.Message);
				text = DataSourceException.DataSourceMessage(dataSourceName, text);
				throw ValueException.NewExpressionError(text, connectionStringValue, null);
			}
			return t;
		}

		// Token: 0x04003C57 RID: 15447
		public const string ConnectionStringParameterName = "connectionString";

		// Token: 0x04003C58 RID: 15448
		private readonly bool isOdbc;

		// Token: 0x04003C59 RID: 15449
		private readonly string userNameKey;

		// Token: 0x04003C5A RID: 15450
		private readonly string passwordKey;

		// Token: 0x04003C5B RID: 15451
		private readonly string integratedSecurityKey;

		// Token: 0x04003C5C RID: 15452
		private readonly bool supportsImpersonation;

		// Token: 0x04003C5D RID: 15453
		private readonly HashSet<string> credentialOnlyProperties;

		// Token: 0x04003C5E RID: 15454
		private readonly HashSet<string> sourceOnlyProperties;

		// Token: 0x04003C5F RID: 15455
		private readonly HashSet<string> disallowedProperties;
	}
}
