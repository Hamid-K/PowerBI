using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200044E RID: 1102
	internal sealed class SapHanaModule : Module
	{
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x0006A86F File Offset: 0x00068A6F
		public override string Name
		{
			get
			{
				return "SapHana";
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x0006A876 File Offset: 0x00068A76
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(13, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "SapHana.Database";
						case 1:
							return SapHanaModule.RangeOperator.Type.GetName();
						case 2:
							return SapHanaModule.RangeOperator.GreaterThan.GetName();
						case 3:
							return SapHanaModule.RangeOperator.LessThan.GetName();
						case 4:
							return SapHanaModule.RangeOperator.GreaterThanOrEquals.GetName();
						case 5:
							return SapHanaModule.RangeOperator.LessThanOrEquals.GetName();
						case 6:
							return SapHanaModule.RangeOperator._Equals.GetName();
						case 7:
							return SapHanaModule.RangeOperator.NotEquals.GetName();
						case 8:
							return SapHanaDistribution.Type.GetName();
						case 9:
							return SapHanaDistribution.Off.GetName();
						case 10:
							return SapHanaDistribution.Connection.GetName();
						case 11:
							return SapHanaDistribution.Statement.GetName();
						case 12:
							return SapHanaDistribution.All.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x0006A8B2 File Offset: 0x00068AB2
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SapHanaModule.resourceKindInfo };
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0006A8C4 File Offset: 0x00068AC4
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new SapHanaModule.DatabaseFunctionValue(host);
				case 1:
					return SapHanaModule.RangeOperator.Type;
				case 2:
					return SapHanaModule.RangeOperator.GreaterThan;
				case 3:
					return SapHanaModule.RangeOperator.LessThan;
				case 4:
					return SapHanaModule.RangeOperator.GreaterThanOrEquals;
				case 5:
					return SapHanaModule.RangeOperator.LessThanOrEquals;
				case 6:
					return SapHanaModule.RangeOperator._Equals;
				case 7:
					return SapHanaModule.RangeOperator.NotEquals;
				case 8:
					return SapHanaDistribution.Type;
				case 9:
					return SapHanaDistribution.Off;
				case 10:
					return SapHanaDistribution.Connection;
				case 11:
					return SapHanaDistribution.Statement;
				case 12:
					return SapHanaDistribution.All;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x04000F22 RID: 3874
		private const string downloadLink = "https://go.microsoft.com/fwlink/?LinkID=698287";

		// Token: 0x04000F23 RID: 3875
		private const int protocolError = 1033;

		// Token: 0x04000F24 RID: 3876
		private const string DatabaseFunctionName = "SapHana.Database";

		// Token: 0x04000F25 RID: 3877
		private const string ValueDotNativeQuery = "Value.NativeQuery";

		// Token: 0x04000F26 RID: 3878
		public const string DataSourceName = "SAPHANA";

		// Token: 0x04000F27 RID: 3879
		public const string DoNotSetApplicationUser = "PBI_SapHanaDoNotSetApplicationUser";

		// Token: 0x04000F28 RID: 3880
		private static readonly HashSet<string> sslCryptoProviderValidValues = new HashSet<string> { "COMMONCRYPTO", "SAPCRYPTO", "OPENSSL", "MSCRYPTO" };

		// Token: 0x04000F29 RID: 3881
		private static readonly Keys connectionStringKeys = Keys.New("Driver", "ServerNode", "DISTRIBUTION");

		// Token: 0x04000F2A RID: 3882
		private static readonly TextValue driverName = (X64Helper.Is64BitProcess ? TextValue.New("HDBODBC") : TextValue.New("HDBODBC32"));

		// Token: 0x04000F2B RID: 3883
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("Distribution", NullableTypeValue.Int32, SapHanaDistribution.All, OptionItemOption.None, null, null),
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "HANA"),
			new OptionItem("EnableColumnBinding", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "HANA"),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, "ODBC"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration)
		});

		// Token: 0x04000F2C RID: 3884
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("SapHana", Strings.SapHanaChallengeTitle, true, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.SqlAuth,
			new WindowsAuthenticationInfo
			{
				Description = Strings.SapHanaWindowsAuth,
				SupportsAlternateCredentials = true
			}
		}, new CredentialProperty[]
		{
			new CredentialProperty
			{
				Name = "SSLValidateServerCertificate",
				Label = Strings.ValidateServerCertificate,
				PropertyType = typeof(string),
				IsSecret = false,
				IsRequired = false
			},
			new CredentialProperty
			{
				Name = "SSLCryptoProvider",
				Label = Strings.SslCryptoProvider,
				PropertyType = typeof(string),
				IsSecret = false,
				IsRequired = false
			},
			new CredentialProperty
			{
				Name = "SSLKeyStore",
				Label = Strings.SslKeyStore,
				PropertyType = typeof(string),
				IsSecret = false,
				IsRequired = false
			},
			new CredentialProperty
			{
				Name = "SSLTrustStore",
				Label = Strings.SslTrustStore,
				PropertyType = typeof(string),
				IsSecret = false,
				IsRequired = false
			}
		}, null, new DataSourceLocationFactory[] { SapHanaSqlDataSourceLocation.Factory });

		// Token: 0x04000F2D RID: 3885
		private Keys exportKeys;

		// Token: 0x0200044F RID: 1103
		private enum Exports
		{
			// Token: 0x04000F2F RID: 3887
			Database,
			// Token: 0x04000F30 RID: 3888
			SapHanaRangeOperator_Type,
			// Token: 0x04000F31 RID: 3889
			SapHanaRangeOperator_GreaterThan,
			// Token: 0x04000F32 RID: 3890
			SapHanaRangeOperator_LessThan,
			// Token: 0x04000F33 RID: 3891
			SapHanaRangeOperator_GreaterThanOrEquals,
			// Token: 0x04000F34 RID: 3892
			SapHanaRangeOperator_LessThanOrEquals,
			// Token: 0x04000F35 RID: 3893
			SapHanaRangeOperator_Equals,
			// Token: 0x04000F36 RID: 3894
			SapHanaRangeOperator_NotEquals,
			// Token: 0x04000F37 RID: 3895
			SapHanaDistribution_Type,
			// Token: 0x04000F38 RID: 3896
			SapHanaDistribution_Off,
			// Token: 0x04000F39 RID: 3897
			SapHanaDistribution_Connection,
			// Token: 0x04000F3A RID: 3898
			SapHanaDistribution_Statement,
			// Token: 0x04000F3B RID: 3899
			SapHanaDistribution_All,
			// Token: 0x04000F3C RID: 3900
			Count
		}

		// Token: 0x02000450 RID: 1104
		public static class RangeOperator
		{
			// Token: 0x0600253E RID: 9534 RVA: 0x0006AB90 File Offset: 0x00068D90
			private static NumberValue New(string name, SapHanaRangeOperator value, string caption)
			{
				return SapHanaModule.RangeOperator.Type.NewEnumValue(name, (int)value, value, caption);
			}

			// Token: 0x04000F3D RID: 3901
			public static readonly IntEnumTypeValue<SapHanaRangeOperator> Type = new IntEnumTypeValue<SapHanaRangeOperator>("SapHanaRangeOperator.Type");

			// Token: 0x04000F3E RID: 3902
			public static readonly NumberValue GreaterThan = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.GreaterThan", SapHanaRangeOperator.GreaterThan, Strings.SapHanaRangeOperator_GreaterThanCaption);

			// Token: 0x04000F3F RID: 3903
			public static readonly NumberValue LessThan = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.LessThan", SapHanaRangeOperator.LessThan, Strings.SapHanaRangeOperator_LessThanCaption);

			// Token: 0x04000F40 RID: 3904
			public static readonly NumberValue GreaterThanOrEquals = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.GreaterThanOrEquals", SapHanaRangeOperator.GreaterThanOrEquals, Strings.SapHanaRangeOperator_GreaterThanOrEqualsCaption);

			// Token: 0x04000F41 RID: 3905
			public static readonly NumberValue LessThanOrEquals = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.LessThanOrEquals", SapHanaRangeOperator.LessThanOrEquals, Strings.SapHanaRangeOperator_LessThanOrEqualsCaption);

			// Token: 0x04000F42 RID: 3906
			public static readonly NumberValue _Equals = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.Equals", SapHanaRangeOperator.Equals, Strings.SapHanaRangeOperator_EqualsCaption);

			// Token: 0x04000F43 RID: 3907
			public static readonly NumberValue NotEquals = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.NotEquals", SapHanaRangeOperator.NotEquals, Strings.SapHanaRangeOperator_NotEqualsCaption);

			// Token: 0x04000F44 RID: 3908
			public static readonly NumberValue Between = SapHanaModule.RangeOperator.New("SapHanaRangeOperator.Between", SapHanaRangeOperator.Between, Strings.SapHanaRangeOperator_BetweenCaption);
		}

		// Token: 0x02000451 RID: 1105
		private sealed class DatabaseFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06002540 RID: 9536 RVA: 0x0006AC72 File Offset: 0x00068E72
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "server", TypeValue.Text, "options", SapHanaModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17000F16 RID: 3862
			// (get) Token: 0x06002541 RID: 9537 RVA: 0x0006A86F File Offset: 0x00068A6F
			public override string PrimaryResourceKind
			{
				get
				{
					return "SapHana";
				}
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x0006AC9C File Offset: 0x00068E9C
			public override TableValue TypedInvoke(TextValue server, Value options)
			{
				IResource resource = Resource.New("SapHana", server.AsString);
				server = SapHanaModule.DatabaseFunctionValue.EnsurePortNumber(server);
				OptionsRecord optionsRecord = SapHanaModule.OptionRecord.CreateOptions("SAPHANA", options);
				object obj;
				optionsRecord.TryGetValue("Distribution", out obj);
				string text;
				SapHanaDistribution.TryGetConnectionStringValue((SapHanaDistributionOption)obj, out text);
				bool flag = (SapHanaDistributionOption)obj > SapHanaDistributionOption.Off;
				RecordValue recordValue = RecordValue.New(SapHanaModule.connectionStringKeys, new Value[]
				{
					SapHanaModule.driverName,
					server,
					TextValue.New(text)
				});
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
				bool flag2;
				Func<IDisposable> func;
				string text2;
				resourceCredentialCollection = SapHanaModule.DatabaseFunctionValue.ProcessCredentials(this.host, resource, resourceCredentialCollection, out flag2, out func, out text2);
				IEngineHost engineHost = new SapHanaModule.EngineHost(this.host, new SapHanaModule.ExtensibilityService(resource, resourceCredentialCollection));
				IOdbcService odbcService = this.host.Hook(() => OdbcService.WindowsInstance);
				if (func != null)
				{
					odbcService = new OdbcImpersonatingService(odbcService, func);
				}
				bool flag3;
				string text3 = ((bool.TryParse(Environment.GetEnvironmentVariable("PBI_SapHanaDoNotSetApplicationUser"), out flag3) && flag3) ? null : text2);
				SapHanaAdditionalTracesProvider sapHanaAdditionalTracesProvider = new SapHanaAdditionalTracesProvider();
				odbcService = new SapHanaSessionService(odbcService, text3, sapHanaAdditionalTracesProvider, TracingService.GetService(engineHost).Enabled());
				OdbcConnectionStringHandler odbcConnectionStringHandler = new OdbcConnectionStringHandler(odbcService);
				bool @bool = optionsRecord.GetBool("EnableColumnBinding", false);
				RecordValue recordValue2 = (options.IsNull ? RecordValue.Empty : Library.Record.SelectFields.Invoke(options.AsRecord, ListValue.New(new Value[]
				{
					TextValue.New("CommandTimeout"),
					TextValue.New("ConnectionTimeout")
				}), Library.MissingField.Ignore).AsRecord);
				SapHanaOdbcDataSource sapHanaOdbcDataSource = new SapHanaOdbcDataSource(engineHost, resource, new SapHanaOdbcService(odbcService), odbcConnectionStringHandler, recordValue, SapHanaOdbcOptions.Get(flag, @bool, recordValue2), new SapHanaOdbcExceptionHandler(engineHost, flag2), sapHanaAdditionalTracesProvider, @bool);
				this.EnsureDriverInstalled(sapHanaOdbcDataSource);
				string text4;
				if (optionsRecord.TryGetString("Query", out text4))
				{
					return new OdbcNativeQueryTableValue("SAPHANA", this.host, sapHanaOdbcDataSource, text4, null, false, null, null);
				}
				try
				{
					sapHanaOdbcDataSource.TestConnectionAndGetVersion(null);
				}
				catch (ValueException ex)
				{
					OdbcException ex2 = ex.InnerException as OdbcException;
					if (ex2 != null)
					{
						if (ex2.Errors.Any((OdbcError err) => err.NativeError == 1033))
						{
							throw DataSourceException.NewInvalidCredentialsError(this.host, resource, null, null, ex2);
						}
					}
					throw;
				}
				return new SapHanaModule.DatabaseFunctionValue.DatabaseTableValue(this.host, resource, sapHanaOdbcDataSource, optionsRecord, sapHanaAdditionalTracesProvider);
			}

			// Token: 0x06002543 RID: 9539 RVA: 0x0006AF14 File Offset: 0x00069114
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (SapHanaModule.DatabaseFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("server", out value))
				{
					SapHanaSqlDataSourceLocation sapHanaSqlDataSourceLocation = new SapHanaSqlDataSourceLocation
					{
						Server = value.AsString
					};
					Value value2;
					Value value3;
					if (dictionary.TryGetConstant("package", out value2) && dictionary.TryGetConstant("view", out value3))
					{
						sapHanaSqlDataSourceLocation.Schema = "_SYS_BIC";
						sapHanaSqlDataSourceLocation.Object = value2.AsString + "/" + value3.AsString;
					}
					IExpression @null;
					if (!dictionary.TryGetValue("options", out @null))
					{
						@null = ConstantExpressionSyntaxNode.Null;
					}
					location = sapHanaSqlDataSourceLocation;
					bool? flag;
					StaticAnalysisResolver.HandleOptions(@null, location, out foundOptions, out unknownOptions, out flag);
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06002544 RID: 9540 RVA: 0x0006AFD4 File Offset: 0x000691D4
			private static ResourceCredentialCollection ProcessCredentials(IEngineHost engineHost, IResource resource, ResourceCredentialCollection credentialCollection, out bool requireEncryption, out Func<IDisposable> impersonationWrapper, out string windowsUsername)
			{
				IResourceCredential resourceCredential;
				EncryptedConnectionAdornment encryptedConnectionAdornment;
				if (!credentialCollection.TryGetDatabasePattern(out resourceCredential, out encryptedConnectionAdornment))
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, null, null, null);
				}
				impersonationWrapper = null;
				windowsUsername = null;
				ArrayBuilder<IResourceCredential> arrayBuilder = new ArrayBuilder<IResourceCredential>(2);
				if (resourceCredential is SqlAuthCredential)
				{
					SqlAuthCredential sqlAuthCredential = resourceCredential as SqlAuthCredential;
					SqlAuthCredential sqlAuthCredential2 = new SqlAuthCredential(sqlAuthCredential.Username, SapHanaModule.DatabaseFunctionValue.EscapePassword(sqlAuthCredential.Password));
					arrayBuilder.Add(sqlAuthCredential2);
				}
				else
				{
					if (!(resourceCredential is WindowsCredential))
					{
						throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, null, null, null);
					}
					WindowsCredential windowsCredential = (WindowsCredential)resourceCredential;
					if (windowsCredential.OverrideCurrentUser)
					{
						impersonationWrapper = windowsCredential.GetImpersonationWrapper(engineHost, resource);
						windowsUsername = windowsCredential.Username;
					}
				}
				requireEncryption = encryptedConnectionAdornment != null && encryptedConnectionAdornment.RequireEncryption;
				if (requireEncryption)
				{
					ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment = credentialCollection.OfType<ApplicationCredentialPropertiesAdornment>().FirstOrDefault<ApplicationCredentialPropertiesAdornment>();
					ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment = credentialCollection.OfType<ConnectionStringPropertiesAdornment>().FirstOrDefault<ConnectionStringPropertiesAdornment>();
					bool flag = SapHanaModule.DatabaseFunctionValue.EnsureValidateServerCertificateValue(engineHost, applicationCredentialPropertiesAdornment, connectionStringPropertiesAdornment, resource);
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{ "encrypt", "TRUE" },
						{
							"sslValidateCertificate",
							flag.ToString()
						}
					};
					if (flag)
					{
						string text = SapHanaModule.DatabaseFunctionValue.ValidateSslCryptoProviderValue(engineHost, applicationCredentialPropertiesAdornment, connectionStringPropertiesAdornment, resource);
						string text2 = SapHanaModule.DatabaseFunctionValue.ValidateSslKeyStoreValue(engineHost, applicationCredentialPropertiesAdornment, connectionStringPropertiesAdornment, resource);
						string text3 = SapHanaModule.DatabaseFunctionValue.ValidateSslTrustStoreValue(engineHost, applicationCredentialPropertiesAdornment, connectionStringPropertiesAdornment, resource);
						dictionary["sslCryptoProvider"] = text;
						if (text2 != null)
						{
							dictionary["sslKeyStore"] = text2;
						}
						if (text3 != null)
						{
							dictionary["sslTrustStore"] = text3;
						}
					}
					arrayBuilder.Add(new ConnectionStringAdornment(string.Join(";", dictionary.Select((KeyValuePair<string, string> p) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", p.Key, p.Value)).ToArray<string>())));
				}
				return new ResourceCredentialCollection(resource, arrayBuilder.ToArray());
			}

			// Token: 0x06002545 RID: 9541 RVA: 0x0006B188 File Offset: 0x00069388
			private static string CredentialPropertyFromAdornment(ApplicationCredentialPropertiesAdornment applicationAdornment, ConnectionStringPropertiesAdornment connectionAdornment, string credentialPropertyName)
			{
				object obj;
				if (applicationAdornment != null && applicationAdornment.TryGetAdornment(credentialPropertyName, out obj))
				{
					return (string)obj;
				}
				string text;
				if (connectionAdornment != null && connectionAdornment.TryGetAdornment(credentialPropertyName, out text))
				{
					return text;
				}
				return null;
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x0006B1BC File Offset: 0x000693BC
			private static bool EnsureValidateServerCertificateValue(IEngineHost engineHost, ApplicationCredentialPropertiesAdornment applicationAdornment, ConnectionStringPropertiesAdornment connectionAdornment, IResource resource)
			{
				string text = SapHanaModule.DatabaseFunctionValue.CredentialPropertyFromAdornment(applicationAdornment, connectionAdornment, "SSLValidateServerCertificate");
				if (text == null)
				{
					return true;
				}
				bool flag;
				if (bool.TryParse(text, out flag))
				{
					return flag;
				}
				throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, Strings.SapHanaAuthInvalidSslValidateServerCertificate, Strings.SapHanaAuthInvalidSslValidateServerCertificate, null);
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x0006B204 File Offset: 0x00069404
			private static string EscapePassword(string password)
			{
				if (password.Any((char c) => "[]{}(),;?*=!@".Contains(c)))
				{
					return string.Format(CultureInfo.InvariantCulture, "{{{0}}}", password.Replace("}", "}}"));
				}
				return password;
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x0006B25C File Offset: 0x0006945C
			private static bool IsPathRooted(string path)
			{
				bool flag;
				try
				{
					flag = Path.IsPathRooted(path);
				}
				catch (ArgumentException)
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x0006B288 File Offset: 0x00069488
			private static string ValidateSslCryptoProviderValue(IEngineHost engineHost, ApplicationCredentialPropertiesAdornment applicationAdornment, ConnectionStringPropertiesAdornment connectionAdornment, IResource resource)
			{
				string text = SapHanaModule.DatabaseFunctionValue.CredentialPropertyFromAdornment(applicationAdornment, connectionAdornment, "SSLCryptoProvider");
				if (text != null && !SapHanaModule.sslCryptoProviderValidValues.Contains(text.ToUpperInvariant()))
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, Strings.SapHanaAuthInvalidSslCryptoProvider, Strings.SapHanaAuthInvalidSslCryptoProvider, null);
				}
				return text;
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x0006B2D8 File Offset: 0x000694D8
			private static string ValidateSslKeyStoreValue(IEngineHost engineHost, ApplicationCredentialPropertiesAdornment applicationAdornment, ConnectionStringPropertiesAdornment connectionAdornment, IResource resource)
			{
				string text = SapHanaModule.DatabaseFunctionValue.CredentialPropertyFromAdornment(applicationAdornment, connectionAdornment, "SSLKeyStore");
				if (text != null && (!SapHanaModule.DatabaseFunctionValue.IsPathRooted(text) || !File.Exists(text)))
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, Strings.SapHanaAuthInvalidSslKeyStore, Strings.SapHanaAuthInvalidSslKeyStore, null);
				}
				return text;
			}

			// Token: 0x0600254B RID: 9547 RVA: 0x0006B324 File Offset: 0x00069524
			private static string ValidateSslTrustStoreValue(IEngineHost engineHost, ApplicationCredentialPropertiesAdornment applicationAdornment, ConnectionStringPropertiesAdornment connectionAdornment, IResource resource)
			{
				string text = SapHanaModule.DatabaseFunctionValue.CredentialPropertyFromAdornment(applicationAdornment, connectionAdornment, "SSLTrustStore");
				if (text != null && (!SapHanaModule.DatabaseFunctionValue.IsPathRooted(text) || !File.Exists(text)))
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, Strings.SapHanaAuthInvalidSslTrustStore, Strings.SapHanaAuthInvalidSslTrustStore, null);
				}
				return text;
			}

			// Token: 0x0600254C RID: 9548 RVA: 0x0006B370 File Offset: 0x00069570
			private void EnsureDriverInstalled(SapHanaOdbcDataSource dataSource)
			{
				if (!dataSource.GetInstalledDrivers().Contains(SapHanaModule.driverName.AsString))
				{
					string text = (X64Helper.Is64BitProcess ? Strings.Odbc_MissingDriver64bit(SapHanaModule.driverName.AsString, "https://go.microsoft.com/fwlink/?LinkID=698287") : Strings.Odbc_MissingDriver32bit(SapHanaModule.driverName.AsString, "https://go.microsoft.com/fwlink/?LinkID=698287"));
					throw DataSourceException.NewMissingClientLibraryError<Message0>(dataSource.Host, new Message0(text), dataSource.Resource, SapHanaModule.driverName.AsString, "https://go.microsoft.com/fwlink/?LinkID=698287", null);
				}
			}

			// Token: 0x0600254D RID: 9549 RVA: 0x0006B3F3 File Offset: 0x000695F3
			private static TextValue EnsurePortNumber(TextValue server)
			{
				if (!server.AsString.Contains(":"))
				{
					return TextValue.New(server.AsString + ":30015");
				}
				return server;
			}

			// Token: 0x04000F45 RID: 3909
			private static readonly TypeValue optionsType = SapHanaModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000F46 RID: 3910
			private const string versionTwoString = "2.0";

			// Token: 0x04000F47 RID: 3911
			private static readonly TextValue contents = TextValue.New("Contents");

			// Token: 0x04000F48 RID: 3912
			private static readonly RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false)
			}));

			// Token: 0x04000F49 RID: 3913
			private static readonly TableTypeValue tableType = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(SapHanaModule.DatabaseFunctionValue.recordType, new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x04000F4A RID: 3914
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, _o_options){[Name=\"Contents\"]}[Data]{[Name=__package]}[Data]{[Name=__view]}[Data]", "__func(__server, _o_options)" });

			// Token: 0x04000F4B RID: 3915
			private const string portSeparator = ":";

			// Token: 0x04000F4C RID: 3916
			private const string defaultPort = "30015";

			// Token: 0x04000F4D RID: 3917
			private readonly IEngineHost host;

			// Token: 0x02000452 RID: 1106
			private class DatabaseTableValue : TableValue
			{
				// Token: 0x0600254F RID: 9551 RVA: 0x0006B4DA File Offset: 0x000696DA
				public DatabaseTableValue(IEngineHost host, IResource resource, SapHanaOdbcDataSource datasource, OptionsRecord optionValues, SapHanaAdditionalTracesProvider additionalTracesProvider)
				{
					this.host = host;
					this.resource = resource;
					this.datasource = datasource;
					this.optionValues = optionValues;
					this.additionalTracesProvider = additionalTracesProvider;
				}

				// Token: 0x17000F17 RID: 3863
				// (get) Token: 0x06002550 RID: 9552 RVA: 0x0006B507 File Offset: 0x00069707
				public override TypeValue Type
				{
					get
					{
						return SapHanaModule.DatabaseFunctionValue.tableType;
					}
				}

				// Token: 0x06002551 RID: 9553 RVA: 0x0006B50E File Offset: 0x0006970E
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					Version version;
					this.datasource.TryGetSapHanaVersion(out version);
					bool flag = false;
					string text;
					if (this.optionValues.TryGetString("Implementation", out text))
					{
						flag = text == "2.0";
						if (!flag)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Implementation"), null, null);
						}
					}
					this.additionalTracesProvider.Implementation = new int?(flag ? 2 : 1);
					yield return RecordValue.New(SapHanaModule.DatabaseFunctionValue.recordType, new Value[]
					{
						SapHanaModule.DatabaseFunctionValue.contents,
						new SapHanaModule.ContentsTableValue(this.datasource, new SapHanaService(this.resource, this.datasource, SapHanaModule.DatabaseFunctionValue.DatabaseTableValue.DetermineCubesTableName(version), version, flag))
					});
					yield break;
				}

				// Token: 0x06002552 RID: 9554 RVA: 0x0006B520 File Offset: 0x00069720
				public override Value NativeQuery(TextValue query, Value parameters, Value options)
				{
					List<OdbcParameter> list = null;
					if (!parameters.IsNull)
					{
						if (!parameters.IsList)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.OdbcParametersMustBeList, parameters, null);
						}
						list = new List<OdbcParameter>();
						foreach (IValueReference valueReference in parameters.AsList)
						{
							list.Add(OdbcParameter.FromValue(valueReference.Value, this.datasource.Types));
						}
					}
					if (!options.IsNull)
					{
						if (!options.AsRecord.IsEmpty)
						{
							Keys keys = options.AsRecord.Keys;
							if (!keys.Contains("EnableFolding") || keys.Length > 1)
							{
								throw ValueException.NewExpressionError<Message3>(Strings.InvalidOption(keys.ToString(), "Value.NativeQuery", "EnableFolding"), options, null);
							}
						}
						Value @null = Value.Null;
						if (options.AsRecord.TryGetValue("EnableFolding", out @null) && !@null.IsLogical)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("EnableFolding"), options, null);
						}
						if (@null.IsLogical && @null.AsBoolean)
						{
							bool throwOnFoldingFailure = this.host.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure;
							OdbcOptions options2 = this.datasource.Options;
							OdbcSqlExpressionGenerator odbcSqlExpressionGenerator = new UserOverrideOdbcSqlExpressionGenerator(this.datasource, options2);
							OdbcQueryExpressionVisitorFactory odbcQueryExpressionVisitorFactory = UserOverrideOdbcQueryExpressionFactory.New(odbcSqlExpressionGenerator);
							OdbcQueryDomain odbcQueryDomain = new OdbcQueryDomain(this.datasource, odbcSqlExpressionGenerator, odbcQueryExpressionVisitorFactory, options2.UseSoftNumbers, options2.HideNativeQuery, throwOnFoldingFailure, options2.TolerateConcatOverflow, null);
							return new OdbcFoldingNativeQueryTableValue("SAPHANA", this.host, this.datasource, query.AsString, null, options, odbcQueryDomain, list).CreateOdbcQueryTableValue();
						}
					}
					return new OdbcNativeQueryTableValue("SAPHANA", this.host, this.datasource, query.AsString, null, false, null, list);
				}

				// Token: 0x06002553 RID: 9555 RVA: 0x0006B6F4 File Offset: 0x000698F4
				private static string DetermineCubesTableName(Version version)
				{
					if (!(version != null) || version.Major != 1 || version.Minor != 0 || version.Build >= 100)
					{
						return "BIMC_ALL_AUTHORIZED_CUBES";
					}
					if (version.Build < 90)
					{
						return "BIMC_CUBES";
					}
					return "BIMC_ALL_CUBES";
				}

				// Token: 0x04000F4E RID: 3918
				private readonly IEngineHost host;

				// Token: 0x04000F4F RID: 3919
				private readonly IResource resource;

				// Token: 0x04000F50 RID: 3920
				private readonly SapHanaOdbcDataSource datasource;

				// Token: 0x04000F51 RID: 3921
				private readonly OptionsRecord optionValues;

				// Token: 0x04000F52 RID: 3922
				private readonly SapHanaAdditionalTracesProvider additionalTracesProvider;
			}
		}

		// Token: 0x02000455 RID: 1109
		private sealed class ContentsTableValue : TableValue
		{
			// Token: 0x06002560 RID: 9568 RVA: 0x0006B887 File Offset: 0x00069A87
			public ContentsTableValue(SapHanaOdbcDataSource dataSource, SapHanaService service)
			{
				this.dataSource = dataSource;
				this.service = service;
			}

			// Token: 0x17000F1A RID: 3866
			// (get) Token: 0x06002561 RID: 9569 RVA: 0x0006B89D File Offset: 0x00069A9D
			public override TypeValue Type
			{
				get
				{
					return SapHanaModule.ContentsTableValue.type;
				}
			}

			// Token: 0x06002562 RID: 9570 RVA: 0x0006B8A4 File Offset: 0x00069AA4
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (SapHanaCatalog sapHanaCatalog in this.service.Catalogs)
				{
					yield return this.GetRow(sapHanaCatalog);
				}
				IEnumerator<SapHanaCatalog> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002563 RID: 9571 RVA: 0x0006B8B4 File Offset: 0x00069AB4
			public override bool TryGetValue(Value key, out Value value)
			{
				Value value2;
				if (!key.IsRecord || !key.AsRecord.TryGetValue("Name", out value2) || !value2.IsText)
				{
					return base.TryGetValue(key, out value);
				}
				SapHanaCatalog sapHanaCatalog;
				if (this.service.Catalogs.TryGetCatalog(value2.AsString, out sapHanaCatalog))
				{
					value = this.GetRow(sapHanaCatalog);
					return true;
				}
				value = Value.Null;
				return false;
			}

			// Token: 0x06002564 RID: 9572 RVA: 0x0006B91B File Offset: 0x00069B1B
			private RecordValue GetRow(SapHanaCatalog catalog)
			{
				return RecordValue.New(SapHanaModule.ContentsTableValue.recordType, new Value[]
				{
					TextValue.New(catalog.Name),
					new SapHanaModule.CatalogTableValue(this.dataSource, this.service, catalog)
				});
			}

			// Token: 0x04000F5B RID: 3931
			private static readonly RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false)
			}));

			// Token: 0x04000F5C RID: 3932
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(SapHanaModule.ContentsTableValue.recordType, new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x04000F5D RID: 3933
			private readonly SapHanaOdbcDataSource dataSource;

			// Token: 0x04000F5E RID: 3934
			private readonly SapHanaService service;
		}

		// Token: 0x02000457 RID: 1111
		private class CatalogTableValue : TableValue
		{
			// Token: 0x0600256D RID: 9581 RVA: 0x0006BAE4 File Offset: 0x00069CE4
			public CatalogTableValue(SapHanaOdbcDataSource dataSource, SapHanaService service, SapHanaCatalog catalog)
			{
				this.dataSource = dataSource;
				this.service = service;
				this.catalog = catalog;
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x0600256E RID: 9582 RVA: 0x0006BB01 File Offset: 0x00069D01
			public override TypeValue Type
			{
				get
				{
					return SapHanaModule.CatalogTableValue.type;
				}
			}

			// Token: 0x0600256F RID: 9583 RVA: 0x0006BB08 File Offset: 0x00069D08
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.catalog.Cubes.Select(new Func<SapHanaCubeBase, RecordValue>(this.GetRow)).Cast<IValueReference>().GetEnumerator();
			}

			// Token: 0x06002570 RID: 9584 RVA: 0x0006BB30 File Offset: 0x00069D30
			public override bool TryGetValue(Value key, out Value value)
			{
				if (key.IsRecord)
				{
					RecordValue asRecord = key.AsRecord;
					Value value2;
					if (asRecord.Keys.Length == 1 && asRecord.TryGetValue("Name", out value2) && value2.IsText)
					{
						SapHanaCubeBase sapHanaCubeBase;
						if (this.catalog.Cubes.TryGetCube(value2.AsString, out sapHanaCubeBase))
						{
							value = this.GetRow(sapHanaCubeBase);
							return true;
						}
						value = Value.Null;
						return false;
					}
				}
				return base.TryGetValue(key, out value);
			}

			// Token: 0x06002571 RID: 9585 RVA: 0x0006BBA8 File Offset: 0x00069DA8
			private RecordValue GetRow(SapHanaCubeBase cube)
			{
				return RecordValue.New(SapHanaModule.CatalogTableValue.keys, new IValueReference[]
				{
					TextValue.New(cube.Name),
					new DelayedValue(() => this.GetCubeValue(cube)),
					cube.HasParameters ? SapHanaModule.CatalogTableValue.parameterizedCubeKind : SapHanaModule.CatalogTableValue.cubeKind
				});
			}

			// Token: 0x06002572 RID: 9586 RVA: 0x0006BC1C File Offset: 0x00069E1C
			private Value GetCubeValue(SapHanaCubeBase cube)
			{
				SapHanaCubeContextProvider sapHanaCubeContextProvider = new SapHanaCubeContextProvider(this.dataSource, cube, this.service.UseSemanticSet);
				CubeContext cubeContext;
				sapHanaCubeContextProvider.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(cube.ViewName)), EmptyArray<ParameterArguments>.Instance, out cubeContext);
				return CubeContextCubeValue.New(sapHanaCubeContextProvider, cubeContext, Keys.Empty);
			}

			// Token: 0x04000F63 RID: 3939
			private static readonly TextValue cubeKind = TextValue.New("Cube");

			// Token: 0x04000F64 RID: 3940
			private static readonly TextValue parameterizedCubeKind = TextValue.New("ParameterizedCube");

			// Token: 0x04000F65 RID: 3941
			private static readonly Keys keys = NavigationTableServices.MetadataValuesWithKind;

			// Token: 0x04000F66 RID: 3942
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(SapHanaModule.CatalogTableValue.keys, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Cube", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x04000F67 RID: 3943
			private readonly SapHanaOdbcDataSource dataSource;

			// Token: 0x04000F68 RID: 3944
			private readonly SapHanaService service;

			// Token: 0x04000F69 RID: 3945
			private readonly SapHanaCatalog catalog;
		}

		// Token: 0x02000459 RID: 1113
		private class EngineHost : IEngineHost
		{
			// Token: 0x06002576 RID: 9590 RVA: 0x0006BD22 File Offset: 0x00069F22
			public EngineHost(IEngineHost host, IExtensibilityService extensibilitySerivce)
			{
				this.host = host;
				this.extensibilitySerivce = extensibilitySerivce;
			}

			// Token: 0x06002577 RID: 9591 RVA: 0x0006BD38 File Offset: 0x00069F38
			public T QueryService<T>() where T : class
			{
				if (typeof(IExtensibilityService) == typeof(T))
				{
					return (T)((object)this.extensibilitySerivce);
				}
				return this.host.QueryService<T>();
			}

			// Token: 0x04000F6C RID: 3948
			private readonly IEngineHost host;

			// Token: 0x04000F6D RID: 3949
			private readonly IExtensibilityService extensibilitySerivce;
		}

		// Token: 0x0200045A RID: 1114
		private class ExtensibilityService : IExtensibilityService
		{
			// Token: 0x06002578 RID: 9592 RVA: 0x0006BD6C File Offset: 0x00069F6C
			public ExtensibilityService(IResource resource, ResourceCredentialCollection credentials)
			{
				this.resource = resource;
				this.credentials = credentials;
			}

			// Token: 0x17000F1E RID: 3870
			// (get) Token: 0x06002579 RID: 9593 RVA: 0x0006BD82 File Offset: 0x00069F82
			public IResource CurrentResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17000F1F RID: 3871
			// (get) Token: 0x0600257A RID: 9594 RVA: 0x0006BD8A File Offset: 0x00069F8A
			public ResourceCredentialCollection CurrentCredentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x17000F20 RID: 3872
			// (get) Token: 0x0600257B RID: 9595 RVA: 0x00002139 File Offset: 0x00000339
			public bool ImpersonateResource
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600257C RID: 9596 RVA: 0x0000336E File Offset: 0x0000156E
			public void RefreshCredential(bool forceRefresh)
			{
			}

			// Token: 0x04000F6E RID: 3950
			private readonly IResource resource;

			// Token: 0x04000F6F RID: 3951
			private readonly ResourceCredentialCollection credentials;
		}
	}
}
