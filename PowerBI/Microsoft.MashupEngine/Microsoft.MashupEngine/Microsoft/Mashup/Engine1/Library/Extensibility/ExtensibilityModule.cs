using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Extensibility
{
	// Token: 0x02000B9A RID: 2970
	internal sealed class ExtensibilityModule : Module
	{
		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x00112829 File Offset: 0x00110A29
		public override string Name
		{
			get
			{
				return "Extensibility";
			}
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x00112830 File Offset: 0x00110A30
		public override Keys ExportKeys
		{
			get
			{
				if (ExtensibilityModule.exportKeys == null)
				{
					ExtensibilityModule.exportKeys = Keys.New(17, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Extension.Module";
						case 1:
							return "Extension.CurrentCredential";
						case 2:
							return "Extension.CurrentApplication";
						case 3:
							return "Extension.CredentialError";
						case 4:
							return "Extension.LoadString";
						case 5:
							return "Extension.Contents";
						case 6:
							return "Extension.InvokeWithCredentials";
						case 7:
							return "Extension.InvokeWithPermissions";
						case 8:
							return "Extension.InvokeVolatileFunction";
						case 9:
							return "Extension.HasPermission";
						case 10:
							return "Extension.Cache";
						case 11:
							return "Credential.AccessDenied";
						case 12:
							return "Credential.AccessForbidden";
						case 13:
							return "Credential.EncryptionNotSupported";
						case 14:
							return "Credential.NativeQueryPermission";
						case 15:
							return "Error.Unexpected";
						case 16:
							return "Web.DefaultProxy";
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return ExtensibilityModule.exportKeys;
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x00112869 File Offset: 0x00110A69
		public override Keys SectionKeys
		{
			get
			{
				if (ExtensibilityModule.sectionKeys == null)
				{
					ExtensibilityModule.sectionKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "DataSource.Function";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return ExtensibilityModule.sectionKeys;
			}
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x001128A4 File Offset: 0x00110AA4
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new ExtensibilityModule.ExtensionModuleFunctionValue();
				case 1:
					return new ExtensibilityModule.CurrentCredentialFunctionValue(hostEnvironment);
				case 2:
					return new ExtensibilityModule.CurrentApplicationFunctionValue(hostEnvironment);
				case 3:
					return new ExtensibilityModule.CredentialErrorFunctionValue(hostEnvironment);
				case 4:
					return new ExtensibilityModule.LoadStringFunctionValue(hostEnvironment);
				case 5:
					return new ExtensibilityModule.ContentsFunctionValue(hostEnvironment);
				case 6:
					return new ExtensibilityModule.InvokeWithCredentialsFunctionValue(hostEnvironment);
				case 7:
					return new ExtensibilityModule.InvokeWithPermissionsFunctionValue(hostEnvironment);
				case 8:
					return new ExtensibilityModule.InvokeVolatileFunctionFunctionValue(hostEnvironment);
				case 9:
					return new ExtensibilityModule.HasPermissionFunctionValue(hostEnvironment);
				case 10:
					return new ExtensibilityModule.CacheFunctionValue(hostEnvironment);
				case 11:
					return ExtensibilityModule.Credential.AccessDenied;
				case 12:
					return ExtensibilityModule.Credential.AccessForbidden;
				case 13:
					return ExtensibilityModule.Credential.EncryptionNotSupported;
				case 14:
					return ExtensibilityModule.Credential.NativeQueryPermission;
				case 15:
					return new ExtensibilityModule.UnexpectedErrorFunctionValue(hostEnvironment);
				case 16:
					return new ExtensibilityModule.WebDefaultProxyFunctionValue();
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x001128D8 File Offset: 0x00110AD8
		protected override RecordValue GetSectionExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.SectionKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ExtensibilityModule.DataSource.FunctionFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0011290C File Offset: 0x00110B0C
		private static string GetResourceKind(RecordValue resource)
		{
			string text = null;
			Value value;
			if (resource.TryGetValue("Kind", out value) && value.IsText)
			{
				text = value.AsText.String;
			}
			string text2 = null;
			if (resource.TryGetValue("Description", out value) && value.IsText)
			{
				text2 = value.AsText.String;
			}
			if ((text == null && text2 == null) || !string.Equals(text ?? text2, text2 ?? text, StringComparison.OrdinalIgnoreCase))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Extensibility_MissingOrInvalidResourceKind, value, null);
			}
			return text ?? text2;
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x00112990 File Offset: 0x00110B90
		public static FunctionValue MakeExtensionFunctionInfo(ExtensionModule extensionModule, string resourceKind, string publicFunctionName, string privateFunctionName, string handlerFunctionName, string invocationKind)
		{
			return new ExtensibilityModule.DataSource.ExtensionInfoFunctionValue(extensionModule, resourceKind, publicFunctionName, privateFunctionName, handlerFunctionName, invocationKind);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x001129A0 File Offset: 0x00110BA0
		public static ResourceKindInfo[] GetDataSources(ExtensionModule module, RecordValue resourceDescriptors)
		{
			ResourceKindInfo[] array = new ResourceKindInfo[resourceDescriptors.Count];
			for (int i = 0; i < resourceDescriptors.Count; i++)
			{
				RecordValue asRecord = resourceDescriptors[i].AsList[0].AsRecord;
				RecordValue asRecord2 = resourceDescriptors[i].AsList[1].AsRecord;
				array[i] = ExtensibilityModule.ResourceKindInfoLoader.FromRecord(module, asRecord, asRecord2, resourceDescriptors.Keys[i]);
			}
			return array;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00112A14 File Offset: 0x00110C14
		private static string GetModuleName(IEngineHost engineHost)
		{
			string text;
			if (ExtensionModule.TryGetModuleName(engineHost, out text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x04002C97 RID: 11415
		private static Keys exportKeys;

		// Token: 0x04002C98 RID: 11416
		private static Keys sectionKeys;

		// Token: 0x04002C99 RID: 11417
		public const string AadKeyOld = "Aad";

		// Token: 0x04002C9A RID: 11418
		private const string ApplicationProperties = "ApplicationProperties";

		// Token: 0x04002C9B RID: 11419
		private const string AuthenticationKey = "Authentication";

		// Token: 0x04002C9C RID: 11420
		public const string AuthorizationUriKey = "AuthorizationUri";

		// Token: 0x04002C9D RID: 11421
		private const string CustomKey = "Custom";

		// Token: 0x04002C9E RID: 11422
		public const string DataSourcePathKey = "DataSource.Path";

		// Token: 0x04002C9F RID: 11423
		private const string DefaultClientApplicationKey = "DefaultClientApplication";

		// Token: 0x04002CA0 RID: 11424
		private const string DescriptionKey = "Description";

		// Token: 0x04002CA1 RID: 11425
		private const string DSRHandlersKey = "DSRHandlers";

		// Token: 0x04002CA2 RID: 11426
		private const string ExportsKey = "Exports";

		// Token: 0x04002CA3 RID: 11427
		private const string FieldsKey = "Fields";

		// Token: 0x04002CA4 RID: 11428
		public const string FinishLoginKey = "FinishLogin";

		// Token: 0x04002CA5 RID: 11429
		private const string GetHostNameKey = "GetHostName";

		// Token: 0x04002CA6 RID: 11430
		private const string Icon16Key = "Icon16";

		// Token: 0x04002CA7 RID: 11431
		private const string Icon32Key = "Icon32";

		// Token: 0x04002CA8 RID: 11432
		private const string IconsKey = "Icons";

		// Token: 0x04002CA9 RID: 11433
		private const string IsRequiredKey = "IsRequired";

		// Token: 0x04002CAA RID: 11434
		private const string IsSecretKey = "IsSecret";

		// Token: 0x04002CAB RID: 11435
		private const string KeyLabelKey = "KeyLabel";

		// Token: 0x04002CAC RID: 11436
		private const string KindKey = "Kind";

		// Token: 0x04002CAD RID: 11437
		private const string LabelKey = "Label";

		// Token: 0x04002CAE RID: 11438
		public const string LogoutKey = "Logout";

		// Token: 0x04002CAF RID: 11439
		private const string MakeResourcePathKey = "MakeResourcePath";

		// Token: 0x04002CB0 RID: 11440
		private const string NameKey = "Name";

		// Token: 0x04002CB1 RID: 11441
		private const string NativeQueryKey = "NativeQuery";

		// Token: 0x04002CB2 RID: 11442
		public const string OAuthKeyOld = "OAuth";

		// Token: 0x04002CB3 RID: 11443
		private const string ParseResourcePathKey = "ParseResourcePath";

		// Token: 0x04002CB4 RID: 11444
		private const string PasswordKey = "password";

		// Token: 0x04002CB5 RID: 11445
		private const string PasswordLabelKey = "PasswordLabel";

		// Token: 0x04002CB6 RID: 11446
		private const string PropertiesKey = "Properties";

		// Token: 0x04002CB7 RID: 11447
		private const string PropertyTypeKey = "PropertyType";

		// Token: 0x04002CB8 RID: 11448
		public const string RefreshKey = "Refresh";

		// Token: 0x04002CB9 RID: 11449
		public const string ResourceKey = "Resource";

		// Token: 0x04002CBA RID: 11450
		public const string ScopeKey = "Scope";

		// Token: 0x04002CBB RID: 11451
		private const string SingletonKey = "Singleton";

		// Token: 0x04002CBC RID: 11452
		public const string StartLoginKey = "StartLogin";

		// Token: 0x04002CBD RID: 11453
		private const string SupportsAlternateCredentialsKey = "SupportsAlternateCredentials";

		// Token: 0x04002CBE RID: 11454
		private const string SupportsEncryptionKey = "SupportsEncryption";

		// Token: 0x04002CBF RID: 11455
		private const string TestConnectionKey = "TestConnection";

		// Token: 0x04002CC0 RID: 11456
		private const string TypeKey = "Type";

		// Token: 0x04002CC1 RID: 11457
		private const string UrlKey = "Url";

		// Token: 0x04002CC2 RID: 11458
		private const string UserExperienceKey = "ExportsUX";

		// Token: 0x04002CC3 RID: 11459
		private const string UsernameKey = "username";

		// Token: 0x04002CC4 RID: 11460
		private const string UsernameLabelKey = "UsernameLabel";

		// Token: 0x04002CC5 RID: 11461
		private const string ExtensionModuleFunctionName = "Extension.Module";

		// Token: 0x04002CC6 RID: 11462
		public const string InvokeWithCredentialsFunctionName = "Extension.InvokeWithCredentials";

		// Token: 0x04002CC7 RID: 11463
		public const string InvokeWithPermissionsFunctionName = "Extension.InvokeWithPermissions";

		// Token: 0x04002CC8 RID: 11464
		private const string GetDsrFunctionName = "GetDSR";

		// Token: 0x04002CC9 RID: 11465
		private const string GetFormulaFunctionName = "GetFormula";

		// Token: 0x04002CCA RID: 11466
		private const string GetFriendlyNameFunctionName = "GetFriendlyName";

		// Token: 0x04002CCB RID: 11467
		private const string NormalizeFunctionName = "Normalize";

		// Token: 0x04002CCC RID: 11468
		public const string DataSourceInvocationKind = "Extension.DataSource";

		// Token: 0x04002CCD RID: 11469
		private static readonly HashSet<Type> supportedPropertyTypes = new HashSet<Type>
		{
			typeof(string),
			typeof(int),
			typeof(double),
			typeof(bool),
			typeof(DateTime),
			typeof(short),
			typeof(long),
			typeof(sbyte),
			typeof(byte),
			typeof(float),
			typeof(decimal)
		};

		// Token: 0x02000B9B RID: 2971
		private enum Exports
		{
			// Token: 0x04002CCF RID: 11471
			Extension_Module,
			// Token: 0x04002CD0 RID: 11472
			Extension_CurrentCredential,
			// Token: 0x04002CD1 RID: 11473
			Extension_CurrentApplication,
			// Token: 0x04002CD2 RID: 11474
			Extension_CredentialError,
			// Token: 0x04002CD3 RID: 11475
			Extension_LoadString,
			// Token: 0x04002CD4 RID: 11476
			Extension_Contents,
			// Token: 0x04002CD5 RID: 11477
			Extension_InvokeWithCredentials,
			// Token: 0x04002CD6 RID: 11478
			Extension_InvokeWithPermissions,
			// Token: 0x04002CD7 RID: 11479
			Extension_InvokeVolatileFunction,
			// Token: 0x04002CD8 RID: 11480
			Extension_HasPermission,
			// Token: 0x04002CD9 RID: 11481
			Extension_Cache,
			// Token: 0x04002CDA RID: 11482
			Credential_AccessDenied,
			// Token: 0x04002CDB RID: 11483
			Credential_AccessForbidden,
			// Token: 0x04002CDC RID: 11484
			Credential_EncryptionNotSupported,
			// Token: 0x04002CDD RID: 11485
			Credential_NativeQueryPermission,
			// Token: 0x04002CDE RID: 11486
			Error_Unexpected,
			// Token: 0x04002CDF RID: 11487
			Web_DefaultProxy,
			// Token: 0x04002CE0 RID: 11488
			Count
		}

		// Token: 0x02000B9C RID: 2972
		private enum SectionExports
		{
			// Token: 0x04002CE2 RID: 11490
			DataSource_Function,
			// Token: 0x04002CE3 RID: 11491
			Count
		}

		// Token: 0x02000B9D RID: 2973
		private static class ResourceKindInfoLoader
		{
			// Token: 0x060051B9 RID: 20921 RVA: 0x00112B08 File Offset: 0x00110D08
			public static ResourceKindInfo FromRecord(ExtensionModule module, RecordValue properties, RecordValue exports, string resourceKind = null)
			{
				object obj = new object();
				List<AuthenticationInfo> list = new List<AuthenticationInfo>();
				List<CredentialProperty> list2 = new List<CredentialProperty>();
				resourceKind = resourceKind ?? ExtensibilityModule.GetResourceKind(properties);
				FunctionValue function = ExtensibilityModule.ResourceKindInfoLoader.GetFunction(properties, "GetHostName", resourceKind, true);
				Value value7;
				string text;
				if (properties.TryGetValue("Label", out value7) && value7.IsText)
				{
					text = value7.AsString;
				}
				else
				{
					text = null;
				}
				Value value2;
				if (properties.TryGetValue("Type", out value2) && !value2.IsText)
				{
					throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Type", resourceKind), value2 ?? Value.Null, null);
				}
				FunctionValue function2 = ExtensibilityModule.ResourceKindInfoLoader.GetFunction(properties, "MakeResourcePath", resourceKind, true);
				FunctionValue function3 = ExtensibilityModule.ResourceKindInfoLoader.GetFunction(properties, "ParseResourcePath", resourceKind, true);
				FunctionValue function4 = ExtensibilityModule.ResourceKindInfoLoader.GetFunction(properties, "NativeQuery", resourceKind, true);
				FunctionValue function5 = ExtensibilityModule.ResourceKindInfoLoader.GetFunction(properties, "TestConnection", resourceKind, true);
				if (value2.IsNull || function2 == null)
				{
					ExtensibilityModule.ResourceKindInfoLoader.TryInferResourcePathFunctions(resourceKind, exports, ref value2, ref function2, ref function3);
				}
				Value value3;
				string text2;
				if (properties.TryGetValue("Authentication", out value3) && value3.IsRecord)
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					foreach (NamedValue namedValue in value3.AsRecord.GetFields())
					{
						RecordValue recordValue = (namedValue.Value.IsRecord ? namedValue.Value.AsRecord : null);
						List<CredentialProperty> list3 = null;
						List<CredentialProperty> list4 = new List<CredentialProperty>();
						text2 = namedValue.Key;
						if (text2 == null)
						{
							goto IL_07E2;
						}
						int length = text2.Length;
						AuthenticationInfo authenticationInfo;
						switch (length)
						{
						case 3:
						{
							char c = text2[1];
							if (c != 'A')
							{
								if (c != 'a')
								{
									if (c != 'e')
									{
										goto IL_07E2;
									}
									if (!(text2 == "Key"))
									{
										goto IL_07E2;
									}
									KeyAuthenticationInfo keyAuthenticationInfo = new KeyAuthenticationInfo();
									if (recordValue != null)
									{
										if (recordValue.TryGetValue("KeyLabel", out value7))
										{
											keyAuthenticationInfo.KeyLabel = value7.AsText.AsString;
										}
										list3 = ExtensibilityModule.ResourceKindInfoLoader.ValidateNoProperties(resourceKind, namedValue.Key, recordValue);
									}
									authenticationInfo = keyAuthenticationInfo;
									goto IL_0824;
								}
								else if (!(text2 == "Aad"))
								{
									goto IL_07E2;
								}
							}
							else if (!(text2 == "AAD"))
							{
								goto IL_07E2;
							}
							if (flag)
							{
								throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Aad", resourceKind), Value.Null, null);
							}
							flag = true;
							recordValue = namedValue.Value.AsRecord;
							ExtensionAadFactory.Validate(recordValue, value2.AsString == "Url");
							OAuthClientApplication oauthClientApplication = null;
							if (recordValue.TryGetValue("DefaultClientApplication", out value7))
							{
								RecordValue asRecord = value7.AsRecord;
								Value value4;
								Value value5;
								Value value6;
								if (asRecord.Count != 3 || !asRecord.TryGetValue("ClientId", out value4) || !value4.IsText || !asRecord.TryGetValue("ClientSecret", out value5) || !value5.IsText || !asRecord.TryGetValue("CallbackUrl", out value6) || !value6.IsText)
								{
									throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("DefaultClientApplication", resourceKind), value7, null);
								}
								oauthClientApplication = new OAuthClientApplication(value4.AsString, value5.AsString, value6.AsString, ClientApplicationSecretType.Default);
							}
							list3 = ExtensibilityModule.ResourceKindInfoLoader.GetAdditionalProperties(resourceKind, namedValue.Key, recordValue);
							if (list3 != null)
							{
								string[] array = new string[] { "ClientId", "ClientSecret", "CallbackUrl" };
								ExtensibilityModule.ResourceKindInfoLoader.ValidateAllowedProperties(resourceKind, list3, array);
							}
							authenticationInfo = new AadAuthenticationInfo
							{
								ClientApplicationType = ((oauthClientApplication == null) ? OAuthClientApplicationType.Required : OAuthClientApplicationType.Optional),
								ProviderFactory = new ExtensionAadFactory(obj, module, resourceKind, oauthClientApplication)
							};
							goto IL_0824;
						}
						case 4:
							goto IL_07E2;
						case 5:
							if (!(text2 == "OAuth"))
							{
								goto IL_07E2;
							}
							break;
						case 6:
							if (!(text2 == "OAuth2"))
							{
								goto IL_07E2;
							}
							break;
						case 7:
							if (!(text2 == "Windows"))
							{
								goto IL_07E2;
							}
							goto IL_04AB;
						case 8:
							if (!(text2 == "Implicit"))
							{
								goto IL_07E2;
							}
							goto IL_07AA;
						case 9:
							if (!(text2 == "Anonymous"))
							{
								goto IL_07E2;
							}
							goto IL_07AA;
						default:
							if (length != 16)
							{
								goto IL_07E2;
							}
							if (!(text2 == "UsernamePassword"))
							{
								goto IL_07E2;
							}
							goto IL_04AB;
						}
						if (flag3)
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("OAuth", resourceKind), Value.Null, null);
						}
						flag3 = true;
						recordValue = namedValue.Value.AsRecord;
						ExtensibilityModule.ResourceKindInfoLoader.GetFunction(recordValue, "StartLogin", resourceKind, false);
						ExtensibilityModule.ResourceKindInfoLoader.GetFunction(recordValue, "FinishLogin", resourceKind, false);
						ExtensibilityModule.ResourceKindInfoLoader.GetFunction(recordValue, "Refresh", resourceKind, true);
						ExtensibilityModule.ResourceKindInfoLoader.GetFunction(recordValue, "Logout", resourceKind, true);
						list3 = ExtensibilityModule.ResourceKindInfoLoader.GetAdditionalProperties(resourceKind, namedValue.Key, recordValue);
						if (list3 != null)
						{
							string[] array2 = new string[] { "StartLogin", "FinishLogin", "Refresh", "Logout" };
							ExtensibilityModule.ResourceKindInfoLoader.ValidateAllowedProperties(resourceKind, list3, array2);
						}
						authenticationInfo = new OAuth2AuthenticationInfo
						{
							ClientApplicationType = OAuthClientApplicationType.Ignored,
							ProviderFactory = new ExtensionOAuthFactory(obj, module, resourceKind)
						};
						goto IL_0824;
						IL_04AB:
						WindowsAuthenticationInfo windowsAuthenticationInfo = null;
						UsernamePasswordAuthenticationInfo usernamePasswordAuthenticationInfo = null;
						if (namedValue.Key == "Windows")
						{
							windowsAuthenticationInfo = new WindowsAuthenticationInfo();
							authenticationInfo = windowsAuthenticationInfo;
						}
						else
						{
							usernamePasswordAuthenticationInfo = new UsernamePasswordAuthenticationInfo();
							authenticationInfo = usernamePasswordAuthenticationInfo;
						}
						if (recordValue == null)
						{
							goto IL_0824;
						}
						string text3 = null;
						string text4 = null;
						string text5 = null;
						bool flag4 = false;
						if (recordValue.TryGetValue("UsernameLabel", out value7) && !value7.IsNull)
						{
							text4 = value7.AsText.AsString;
							text3 = "UsernameLabel";
						}
						if (recordValue.TryGetValue("PasswordLabel", out value7) && !value7.IsNull)
						{
							text5 = value7.AsText.AsString;
							text3 = "PasswordLabel";
						}
						if (namedValue.Key == "Windows" && recordValue.TryGetValue("SupportsAlternateCredentials", out value7) && !value7.IsNull)
						{
							flag4 = value7.AsLogical.AsBoolean;
							text3 = "SupportsAlternateCredentials";
						}
						list3 = ExtensibilityModule.ResourceKindInfoLoader.GetAdditionalProperties(resourceKind, namedValue.Key, recordValue);
						if (list3 != null)
						{
							if (text3 != null)
							{
								throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty(text3, resourceKind), Value.Null, null);
							}
							int num = list3.IndexOf((CredentialProperty p) => string.Equals("username", p.Name, StringComparison.OrdinalIgnoreCase));
							int num2 = list3.IndexOf((CredentialProperty p) => string.Equals("password", p.Name, StringComparison.OrdinalIgnoreCase));
							if (num >= 0 && num2 < 0)
							{
								throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("password", resourceKind), Value.Null, null);
							}
							if (num < 0 && num2 >= 0)
							{
								throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("username", resourceKind), Value.Null, null);
							}
							if (num < 0 && num2 < 0)
							{
								flag4 = false;
							}
							else
							{
								CredentialProperty credentialProperty = list3[num];
								CredentialProperty credentialProperty2 = list3[num2];
								if (credentialProperty.PropertyType != typeof(string) || credentialProperty.IsSecret)
								{
									throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("username", resourceKind), Value.Null, null);
								}
								if (credentialProperty2.PropertyType != typeof(string) || !credentialProperty2.IsSecret)
								{
									throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("password", resourceKind), Value.Null, null);
								}
								text4 = credentialProperty.Label;
								text5 = credentialProperty2.Label;
								flag4 = true;
								list3.RemoveAt(Math.Max(num, num2));
								list3.RemoveAt(Math.Min(num, num2));
							}
						}
						if (namedValue.Key == "Windows")
						{
							windowsAuthenticationInfo.UsernameLabel = text4;
							windowsAuthenticationInfo.PasswordLabel = text5;
							windowsAuthenticationInfo.SupportsAlternateCredentials = flag4;
							goto IL_0824;
						}
						usernamePasswordAuthenticationInfo.UsernameLabel = text4;
						usernamePasswordAuthenticationInfo.PasswordLabel = text5;
						goto IL_0824;
						IL_07AA:
						if (flag2)
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Implicit", resourceKind), Value.Null, null);
						}
						flag2 = true;
						list3 = ExtensibilityModule.ResourceKindInfoLoader.ValidateNoProperties(resourceKind, namedValue.Key, recordValue);
						authenticationInfo = new ImplicitAuthenticationInfo();
						IL_0824:
						if (recordValue != null)
						{
							if (recordValue.TryGetValue("Label", out value7))
							{
								authenticationInfo.Label = value7.AsText.AsString;
							}
							if (recordValue.TryGetValue("Description", out value7))
							{
								authenticationInfo.Description = value7.AsText.AsString;
							}
							authenticationInfo.Properties = list3;
							ExtensibilityModule.ResourceKindInfoLoader.AddApplicationProperties(recordValue, list4);
						}
						authenticationInfo.ApplicationProperties = list4;
						list.Add(authenticationInfo);
						continue;
						IL_07E2:
						authenticationInfo = new ParameterizedAuthenticationInfo(namedValue.Key, null);
						list3 = ExtensibilityModule.ResourceKindInfoLoader.GetAdditionalProperties(resourceKind, namedValue.Key, recordValue);
						if (list3 == null)
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Properties", resourceKind), TextValue.New(namedValue.Key), null);
						}
						goto IL_0824;
					}
				}
				if (list.Count == 0)
				{
					throw ValueException.NewDataFormatError<Message1>(Strings.Extensibility_NoAuthenticationTypes(resourceKind), Value.Null, null);
				}
				ExtensibilityModule.ResourceKindInfoLoader.AddApplicationProperties(properties, list2);
				bool flag5 = false;
				if (properties.TryGetValue("SupportsEncryption", out value7))
				{
					if (!value7.IsLogical)
					{
						throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("SupportsEncryption", resourceKind), value7, null);
					}
					flag5 = value7.AsBoolean;
				}
				if (properties.TryGetValue("Icons", out value7))
				{
					if (!value7.IsRecord)
					{
						throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Icons", resourceKind), value7, null);
					}
					RecordValue asRecord2 = value7.AsRecord;
					if (asRecord2.TryGetValue("Icon16", out value7) && value7.IsList)
					{
						if (!value7.AsList.Any((IValueReference value) => !value.Value.IsBinary))
						{
							if (asRecord2.TryGetValue("Icon32", out value7) && value7.IsList)
							{
								if (!value7.AsList.Any((IValueReference value) => !value.Value.IsBinary))
								{
									goto IL_0A0C;
								}
							}
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Icon32", resourceKind), value7.IsNull ? asRecord2 : value7, null);
						}
					}
					throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Icon16", resourceKind), value7.IsNull ? asRecord2 : value7, null);
				}
				IL_0A0C:
				List<ExtensionDataSourceLocationFactory> list5 = new List<ExtensionDataSourceLocationFactory>();
				if (properties.TryGetValue("DSRHandlers", out value7))
				{
					if (!value7.IsRecord)
					{
						throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("DSRHandlers", resourceKind), value7, null);
					}
					foreach (NamedValue namedValue2 in value7.AsRecord.GetFields())
					{
						if (!namedValue2.Value.IsRecord)
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty(namedValue2.Key, resourceKind), namedValue2.Value, null);
						}
						RecordValue asRecord3 = namedValue2.Value.AsRecord;
						ExtensionDataSourceLocationFactory extensionDataSourceLocationFactory = new ExtensionDataSourceLocationFactory(obj, resourceKind, namedValue2.Key, ExtensibilityModule.ResourceKindInfoLoader.GetFunction(asRecord3, "GetDSR", resourceKind, false), ExtensibilityModule.ResourceKindInfoLoader.GetFunction(asRecord3, "GetFormula", resourceKind, false), ExtensibilityModule.ResourceKindInfoLoader.GetFunction(asRecord3, "GetFriendlyName", resourceKind, true), ExtensibilityModule.ResourceKindInfoLoader.GetFunction(asRecord3, "Normalize", resourceKind, true), function2, function3, exports);
						list5.Add(extensionDataSourceLocationFactory);
					}
				}
				ExtensionResourceKindInfo extensionResourceKindInfo = new ExtensionResourceKindInfo(obj, resourceKind, function2, function3, function4, function5, function, list5, properties);
				text2 = value2.AsString;
				ResourceKindInfo resourceKindInfo;
				if (!(text2 == "Singleton"))
				{
					if (!(text2 == "Url"))
					{
						if (!(text2 == "Custom"))
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Type", resourceKind), value2, null);
						}
						resourceKindInfo = new ExtensionCustomResource(module.Name, resourceKind, text, flag5, (function4 != null) ? new bool?(true) : null, list, list2, extensionResourceKindInfo);
					}
					else
					{
						resourceKindInfo = new ExtensionUriResource(module.Name, resourceKind, text, flag5, (function4 != null) ? new bool?(true) : null, list, list2, extensionResourceKindInfo);
					}
				}
				else
				{
					resourceKindInfo = new ExtensionSingletonResource(module.Name, resourceKind, text, flag5, (function4 != null) ? new bool?(true) : null, list, list2, extensionResourceKindInfo);
				}
				return resourceKindInfo;
			}

			// Token: 0x060051BA RID: 20922 RVA: 0x00113748 File Offset: 0x00111948
			private static FunctionValue GetFunction(RecordValue record, string member, string resourceKind, bool optional)
			{
				Value @null = Value.Null;
				if (!record.TryGetValue(member, out @null) && !optional)
				{
					throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty(member, resourceKind), @null, null);
				}
				if (!@null.IsNull)
				{
					return @null.AsFunction;
				}
				return null;
			}

			// Token: 0x060051BB RID: 20923 RVA: 0x00113788 File Offset: 0x00111988
			private static List<CredentialProperty> ValidateNoProperties(string resourceKind, string authKind, RecordValue authRecord)
			{
				Value value;
				if (authRecord == null || !authRecord.TryGetValue("Properties", out value) || value.IsNull)
				{
					return null;
				}
				throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_PropertiesNotSupported(authKind, resourceKind), TextValue.New(authKind), null);
			}

			// Token: 0x060051BC RID: 20924 RVA: 0x001137C4 File Offset: 0x001119C4
			private static bool ValidateAllowedProperties(string resourceKind, List<CredentialProperty> additionalProperties, string[] disallowedProperties)
			{
				for (int i = 0; i < disallowedProperties.Length; i++)
				{
					string disallowedProperty = disallowedProperties[i];
					if (additionalProperties.IndexOf((CredentialProperty p) => string.Equals(p.Name, disallowedProperty, StringComparison.OrdinalIgnoreCase)) >= 0)
					{
						throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty(disallowedProperty, resourceKind), Value.Null, null);
					}
				}
				return true;
			}

			// Token: 0x060051BD RID: 20925 RVA: 0x00113820 File Offset: 0x00111A20
			private static List<CredentialProperty> GetAdditionalProperties(string resourceKind, string authKind, RecordValue authRecord)
			{
				Value value;
				if (authRecord == null || !authRecord.TryGetValue("Properties", out value) || value.IsNull)
				{
					return null;
				}
				if (!value.IsType || !value.AsType.IsFunctionType)
				{
					throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Properties", resourceKind), value, null);
				}
				List<CredentialProperty> list = new List<CredentialProperty>();
				ExtensibilityModule.ResourceKindInfoLoader.AddProperties(value.AsType.AsFunctionType, list);
				return list;
			}

			// Token: 0x060051BE RID: 20926 RVA: 0x0011388C File Offset: 0x00111A8C
			private static void TryInferResourcePathFunctions(string resourceKind, RecordValue exports, ref Value type, ref FunctionValue makeResourcePath, ref FunctionValue parseResourcePath)
			{
				if (!type.IsNull || makeResourcePath != null || parseResourcePath != null)
				{
					Message2 message = Strings.Extensibility_MissingOrInvalidProperty("Type", resourceKind);
					Value value;
					if ((value = type) == null)
					{
						value = makeResourcePath ?? parseResourcePath;
					}
					throw ValueException.NewDataFormatError<Message2>(message, value, null);
				}
				string[] array = null;
				HashSet<string> hashSet = null;
				for (int i = 0; i < exports.Count; i++)
				{
					if (exports[i].IsFunction)
					{
						FunctionTypeValue asFunctionType = exports[i].Type.AsFunctionType;
						List<string> list = new List<string>(asFunctionType.Max);
						HashSet<string> hashSet2 = new HashSet<string>();
						for (int j = 0; j < asFunctionType.Max; j++)
						{
							TypeValue typeValue = asFunctionType.ParameterType(j);
							bool flag = j < asFunctionType.Min;
							Value value2;
							if (typeValue.TryGetMetaField("DataSource.Path", out value2))
							{
								if (!value2.IsLogical)
								{
									throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("DataSource.Path", resourceKind), value2, null);
								}
								flag = value2.AsBoolean;
							}
							if (flag)
							{
								string text = asFunctionType.ParameterName(j);
								list.Add(text);
								if (typeValue.NonNullable == TypeValue.Uri)
								{
									hashSet2.Add(text);
								}
							}
						}
						if (array == null)
						{
							if (hashSet2.Count > 1 || (hashSet2.Count == 1 && list.Count > 1))
							{
								throw ValueException.NewDataFormatError<Message1>(Strings.UnableToInferResourcePathInfo(resourceKind), Value.Null, null);
							}
							array = list.ToArray();
							hashSet = hashSet2;
						}
						else
						{
							if (list.Count > array.Length || !hashSet.SetEquals(hashSet2))
							{
								throw ValueException.NewDataFormatError<Message1>(Strings.UnableToInferResourcePathInfo(resourceKind), Value.Null, null);
							}
							for (int k = 0; k < array.Length; k++)
							{
								if (!list.Contains(array[k]))
								{
									throw ValueException.NewDataFormatError<Message1>(Strings.UnableToInferResourcePathInfo(resourceKind), Value.Null, null);
								}
							}
						}
					}
				}
				if (array == null || array.Length == 0)
				{
					type = TextValue.New("Singleton");
					makeResourcePath = new ConstantFunctionValue(TextValue.New(resourceKind));
					parseResourcePath = new ConstantFunctionValue(ListValue.Empty, 1, 1);
					return;
				}
				if (hashSet.Count == 0)
				{
					type = TextValue.New("Custom");
					makeResourcePath = new ExtensibilityModule.MakeJsonResourcePathFunctionValue(array);
					parseResourcePath = ExtensibilityModule.ParseJsonResourcePathFunctionValue.Instance;
					return;
				}
				type = TextValue.New("Url");
				string urlParameter = hashSet.Single<string>();
				int num = array.IndexOf((string p) => p == urlParameter);
				makeResourcePath = new ExtensibilityModule.MakeUriResourcePathFunctionValue(array, num);
				parseResourcePath = new ExtensibilityModule.ParseUriResourcePathFunctionValue(array, num);
			}

			// Token: 0x060051BF RID: 20927 RVA: 0x00113AEC File Offset: 0x00111CEC
			private static void AddApplicationProperties(RecordValue properties, IList<CredentialProperty> applicationPropertiesList)
			{
				Value value;
				if (properties.TryGetValue("ApplicationProperties", out value))
				{
					if (value.IsRecord)
					{
						ExtensibilityModule.ResourceKindInfoLoader.AddProperties(value.AsRecord, applicationPropertiesList);
						return;
					}
					if (value.IsType && value.AsType.IsFunctionType)
					{
						ExtensibilityModule.ResourceKindInfoLoader.AddProperties(value.AsType.AsFunctionType, applicationPropertiesList);
					}
				}
			}

			// Token: 0x060051C0 RID: 20928 RVA: 0x00113B44 File Offset: 0x00111D44
			private static void AddProperties(RecordValue applicationPropertiesRecord, IList<CredentialProperty> applicationPropertiesList)
			{
				foreach (NamedValue namedValue in applicationPropertiesRecord.GetFields())
				{
					RecordValue asRecord = namedValue.Value.AsRecord;
					string text = null;
					bool flag = false;
					bool flag2 = false;
					Type type = typeof(string);
					Value value;
					if (asRecord.TryGetValue("Label", out value))
					{
						text = value.AsString;
					}
					if (asRecord.TryGetValue("IsRequired", out value))
					{
						flag = value.AsBoolean;
					}
					if (asRecord.TryGetValue("IsSecret", out value))
					{
						flag2 = value.AsBoolean;
					}
					if (asRecord.TryGetValue("PropertyType", out value))
					{
						type = value.AsType.ToClrType();
						if (!ExtensibilityModule.supportedPropertyTypes.Contains(type))
						{
							throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_PropertyInvalidType(namedValue.Key, TypeValue.GetTypeName(value.AsType)), applicationPropertiesRecord, null);
						}
					}
					applicationPropertiesList.Add(new CredentialProperty
					{
						Name = namedValue.Key,
						Label = text,
						IsRequired = flag,
						IsSecret = flag2,
						PropertyType = type
					});
				}
			}

			// Token: 0x060051C1 RID: 20929 RVA: 0x00113C8C File Offset: 0x00111E8C
			private static void AddProperties(FunctionTypeValue functionType, IList<CredentialProperty> properties)
			{
				for (int i = 0; i < functionType.ParameterCount; i++)
				{
					string text = functionType.ParameterName(i);
					TypeValue typeValue = functionType.ParameterType(i);
					string text2 = null;
					Value value;
					if (typeValue.TryGetMetaField("Documentation.FieldCaption", out value) && value.IsText)
					{
						text2 = value.AsString;
					}
					typeValue = typeValue.SubtractMetaValue.AsType;
					if (typeValue.NonNullable.Equals(TypeValue.Password))
					{
						properties.Add(new CredentialProperty
						{
							Name = text,
							Label = text2,
							IsRequired = (i < functionType.Min && !typeValue.IsNullable),
							IsSecret = true,
							PropertyType = typeof(string)
						});
					}
					else
					{
						if (!typeValue.NonNullable.Equals(TypeValue.Text))
						{
							throw ValueException.NewDataFormatError<Message1>(Strings.AuthenticationPropertyTypeNotSupported(text), typeValue, null);
						}
						properties.Add(new CredentialProperty
						{
							Name = text,
							Label = text2,
							IsRequired = (i < functionType.Min && !typeValue.IsNullable),
							IsSecret = false,
							PropertyType = typeof(string)
						});
					}
				}
			}
		}

		// Token: 0x02000BA1 RID: 2977
		private sealed class ExtensionModuleFunctionValue : NativeFunctionValue2<Value, TextValue, ListValue>
		{
			// Token: 0x060051CC RID: 20940 RVA: 0x00113E20 File Offset: 0x00112020
			public ExtensionModuleFunctionValue()
				: base(TypeValue.Any, "moduleName", TypeValue.Text, "resourceKinds", TypeValue.List)
			{
			}

			// Token: 0x060051CD RID: 20941 RVA: 0x00113E44 File Offset: 0x00112044
			public override Value TypedInvoke(TextValue moduleName, ListValue resourceKinds)
			{
				List<string> list = new List<string>();
				List<Value> list2 = new List<Value>();
				RecordBuilder recordBuilder = new RecordBuilder(resourceKinds.Count);
				foreach (IValueReference valueReference in resourceKinds)
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					string resourceKind = ExtensibilityModule.GetResourceKind(asRecord);
					TextValue textValue = TextValue.New(resourceKind);
					recordBuilder.Add(resourceKind, asRecord, TypeValue.Record);
					Value value;
					if (!asRecord.TryGetValue("Exports", out value) || !value.IsRecord)
					{
						throw ValueException.NewDataFormatError<Message2>(Strings.Extensibility_MissingOrInvalidProperty("Exports", moduleName.AsString), value ?? Value.Null, null);
					}
					Value value2;
					if (asRecord.TryGetValue("ExportsUX", out value2))
					{
						value2 = value2.AsRecord;
					}
					else
					{
						value2 = RecordValue.Empty;
					}
					foreach (NamedValue namedValue in value.AsRecord.GetFields())
					{
						Value value3 = namedValue.Value;
						if (value3.IsFunction)
						{
							Value @null;
							if (!value2.TryGetValue(namedValue.Key, out @null) || !@null.IsRecord)
							{
								@null = Value.Null;
							}
							value3 = value3.NewMeta(RecordValue.New(ExtensibilityModule.ExtensionModuleFunctionValue.functionMetaKeys, new Value[] { textValue, @null }));
						}
						list.Add(namedValue.Key);
						list2.Add(value3);
					}
				}
				RecordValue recordValue = RecordValue.New(ExtensibilityModule.ExtensionModuleFunctionValue.resultMetaKeys, new Value[]
				{
					recordBuilder.ToRecord(),
					moduleName
				});
				return RecordValue.New(Keys.New(list.ToArray()), list2.ToArray()).NewMeta(recordValue);
			}

			// Token: 0x04002CEB RID: 11499
			private static readonly Keys functionMetaKeys = Keys.New("DataSource.Kind", "Publish");

			// Token: 0x04002CEC RID: 11500
			private static readonly Keys resultMetaKeys = Keys.New("DataSource.Kind", "Name");
		}

		// Token: 0x02000BA2 RID: 2978
		private class CurrentCredentialFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x060051CF RID: 20943 RVA: 0x00114066 File Offset: 0x00112266
			public CurrentCredentialFunctionValue(IEngineHost engineHost)
				: base(NullableTypeValue.Record, 0, "forceRefresh", NullableTypeValue.Logical)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051D0 RID: 20944 RVA: 0x00114088 File Offset: 0x00112288
			public override Value TypedInvoke(Value forceRefresh)
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				bool flag = !forceRefresh.IsNull && forceRefresh.AsLogical.Boolean;
				OAuthCredential oauthCredential = extensibilityService.CurrentCredentials.RemoveAdornments() as OAuthCredential;
				if (flag || (oauthCredential != null && oauthCredential.RefreshToken != null))
				{
					extensibilityService.RefreshCredential(flag);
				}
				RecordValue recordValue;
				if (CredentialConversion.TryConvertToRecord(this.engineHost.QueryService<IEngine>(), this.engineHost, extensibilityService.CurrentResource, extensibilityService.CurrentCredentials, out recordValue))
				{
					return recordValue;
				}
				throw new UnpermittedResourceAccessException(extensibilityService.CurrentResource, Strings.Extensibility_InvalidCredentialType, null, null);
			}

			// Token: 0x04002CED RID: 11501
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BA3 RID: 2979
		private class CurrentApplicationFunctionValue : NativeFunctionValue0<Value>
		{
			// Token: 0x060051D1 RID: 20945 RVA: 0x0011412D File Offset: 0x0011232D
			public CurrentApplicationFunctionValue(IEngineHost engineHost)
				: base(NullableTypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051D2 RID: 20946 RVA: 0x00114144 File Offset: 0x00112344
			public override Value TypedInvoke()
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				if (extensibilityService.CurrentCredentials != null)
				{
					ApplicationCredentialPropertiesAdornment applicationCredentialPropertiesAdornment = extensibilityService.CurrentCredentials.OfType<ApplicationCredentialPropertiesAdornment>().FirstOrDefault<ApplicationCredentialPropertiesAdornment>();
					if (applicationCredentialPropertiesAdornment != null)
					{
						RecordBuilder recordBuilder = new RecordBuilder(applicationCredentialPropertiesAdornment.Properties.Count);
						foreach (KeyValuePair<string, object> keyValuePair in applicationCredentialPropertiesAdornment.Properties)
						{
							Value value = ValueMarshaller.MarshalFromClr(keyValuePair.Value);
							recordBuilder.Add(keyValuePair.Key, value, value.Type);
						}
						return recordBuilder.ToRecord();
					}
				}
				return RecordValue.Empty;
			}

			// Token: 0x04002CEE RID: 11502
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BA4 RID: 2980
		private class CacheFunctionValue : NativeFunctionValue0<RecordValue>
		{
			// Token: 0x060051D3 RID: 20947 RVA: 0x00114210 File Offset: 0x00112410
			public CacheFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051D4 RID: 20948 RVA: 0x00114224 File Offset: 0x00112424
			public override RecordValue TypedInvoke()
			{
				ICacheSets cacheSets = this.engineHost.QueryService<ICacheSets>();
				return RecordValue.New(ExtensibilityModule.CacheFunctionValue.cacheKeys, new Value[]
				{
					RecordValue.New(ExtensibilityModule.CacheFunctionValue.cacheSetKeys, new Value[]
					{
						new ExtensibilityModule.CacheFunctionValue.SerializedFunctionValue(this, (cacheSets != null) ? cacheSets.Data : null)
					}),
					RecordValue.New(ExtensibilityModule.CacheFunctionValue.cacheSetKeys, new Value[]
					{
						new ExtensibilityModule.CacheFunctionValue.SerializedFunctionValue(this, (cacheSets != null) ? cacheSets.Metadata : null)
					})
				});
			}

			// Token: 0x060051D5 RID: 20949 RVA: 0x001142A0 File Offset: 0x001124A0
			private string MakeCacheKey(string keyPart)
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				this.credentialsHash = this.credentialsHash ?? extensibilityService.CurrentCredentials.GetHash();
				string kind = extensibilityService.CurrentResource.Kind;
				string path = extensibilityService.CurrentResource.Path;
				string[] array = new string[] { kind, path, this.credentialsHash, keyPart };
				return PersistentCacheKey.Extension.Qualify(array);
			}

			// Token: 0x04002CEF RID: 11503
			private static readonly Keys cacheKeys = Keys.New("Data", "Metadata");

			// Token: 0x04002CF0 RID: 11504
			private static readonly Keys cacheSetKeys = Keys.New("Serialized");

			// Token: 0x04002CF1 RID: 11505
			private readonly IEngineHost engineHost;

			// Token: 0x04002CF2 RID: 11506
			private string credentialsHash;

			// Token: 0x02000BA5 RID: 2981
			private sealed class SerializedFunctionValue : NativeFunctionValue3<BinaryValue, TextValue, FunctionValue, Value>
			{
				// Token: 0x060051D7 RID: 20951 RVA: 0x0011434C File Offset: 0x0011254C
				public SerializedFunctionValue(ExtensibilityModule.CacheFunctionValue cacheFunction, ICacheSet cacheSet)
					: base(TypeValue.Binary, 2, "key", TypeValue.Text, "valueCtor", TypeValue.Function, "overwrite", NullableTypeValue.Logical)
				{
					this.cacheFunction = cacheFunction;
					this.cacheSet = cacheSet;
				}

				// Token: 0x060051D8 RID: 20952 RVA: 0x00114394 File Offset: 0x00112594
				public override BinaryValue TypedInvoke(TextValue key, FunctionValue valueCtor, Value overwrite)
				{
					ICacheSet cacheSet = this.cacheSet;
					IPersistentCache persistentCache = ((cacheSet != null) ? cacheSet.PersistentCache : null);
					if (persistentCache == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
					}
					string text = this.cacheFunction.MakeCacheKey(key.String);
					bool flag = !overwrite.IsNull && overwrite.AsBoolean;
					return new ExtensibilityModule.CacheFunctionValue.CacheBinaryValue(persistentCache, text, valueCtor, flag);
				}

				// Token: 0x04002CF3 RID: 11507
				private readonly ExtensibilityModule.CacheFunctionValue cacheFunction;

				// Token: 0x04002CF4 RID: 11508
				private readonly ICacheSet cacheSet;
			}

			// Token: 0x02000BA6 RID: 2982
			private sealed class CacheBinaryValue : StreamedBinaryValue
			{
				// Token: 0x060051D9 RID: 20953 RVA: 0x001143EF File Offset: 0x001125EF
				public CacheBinaryValue(IPersistentCache persistentCache, string cacheKey, FunctionValue valueCtor, bool refresh)
				{
					this.persistentCache = persistentCache;
					this.cacheKey = cacheKey;
					this.valueCtor = valueCtor;
					this.minVersion = (refresh ? persistentCache.CacheClock.Increment() : null);
				}

				// Token: 0x060051DA RID: 20954 RVA: 0x00114424 File Offset: 0x00112624
				public override Stream Open()
				{
					Stream stream;
					if (!this.persistentCache.TryGetValue(this.cacheKey, DateTime.MinValue, this.minVersion, out stream))
					{
						stream = this.persistentCache.Add(this.cacheKey, this.valueCtor.Invoke().AsBinary.Open());
					}
					return stream;
				}

				// Token: 0x04002CF5 RID: 11509
				private readonly IPersistentCache persistentCache;

				// Token: 0x04002CF6 RID: 11510
				private readonly string cacheKey;

				// Token: 0x04002CF7 RID: 11511
				private readonly FunctionValue valueCtor;

				// Token: 0x04002CF8 RID: 11512
				private readonly CacheVersion minVersion;
			}
		}

		// Token: 0x02000BA7 RID: 2983
		private static class Credential
		{
			// Token: 0x04002CF9 RID: 11513
			public static readonly TextValue AccessDenied = TextValue.New("Credential.AccessDenied");

			// Token: 0x04002CFA RID: 11514
			public static readonly TextValue AccessForbidden = TextValue.New("Credential.AccessForbidden");

			// Token: 0x04002CFB RID: 11515
			public static readonly TextValue EncryptionNotSupported = TextValue.New("Credential.EncryptionNotSupported");

			// Token: 0x04002CFC RID: 11516
			public static readonly TextValue NativeQueryPermission = TextValue.New("Credential.QueryPermission");
		}

		// Token: 0x02000BA8 RID: 2984
		private sealed class CredentialErrorFunctionValue : NativeFunctionValue3<RecordValue, TextValue, Value, Value>
		{
			// Token: 0x060051DC RID: 20956 RVA: 0x001144C4 File Offset: 0x001126C4
			public CredentialErrorFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Record, 1, "reason", TypeValue.Text, "message", NullableTypeValue.Text, "detail", NullableTypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051DD RID: 20957 RVA: 0x00114502 File Offset: 0x00112702
			public override RecordValue TypedInvoke(TextValue reason, Value message, Value detail)
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				return UserCredentialError.New(extensibilityService.CurrentResource, reason, message, detail);
			}

			// Token: 0x04002CFD RID: 11517
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BA9 RID: 2985
		private sealed class UnexpectedErrorFunctionValue : NativeFunctionValue2<Value, TextValue, Value>
		{
			// Token: 0x060051DE RID: 20958 RVA: 0x0011452C File Offset: 0x0011272C
			public UnexpectedErrorFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Record, 1, "message", NullableTypeValue.Text, "options", NullableTypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051DF RID: 20959 RVA: 0x00114558 File Offset: 0x00112758
			public override Value TypedInvoke(TextValue message, Value options)
			{
				if (this.engineHost.QueryService<IExtensibilityService>() == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				InvalidOperationException ex = new InvalidOperationException(message.String);
				if (!options.IsNull)
				{
					string text = options.AsRecord.Keys.FirstOrDefault((string k) => !k.Equals("IsRecoverable"));
					if (text != null)
					{
						throw ValueException.NewExpressionError<Message3>(Strings.InvalidOption(text, "Error.Unexpected", "IsRecoverable"), null, null);
					}
					ex.Data["IsRecoverable"] = options.AsRecord["IsRecoverable"].AsBoolean;
				}
				ex.Data["ModuleName"] = ExtensibilityModule.GetModuleName(this.engineHost);
				throw ex;
			}

			// Token: 0x04002CFE RID: 11518
			private readonly IEngineHost engineHost;

			// Token: 0x04002CFF RID: 11519
			private const string IsRecoverable = "IsRecoverable";
		}

		// Token: 0x02000BAB RID: 2987
		private sealed class LoadStringFunctionValue : NativeFunctionValue1<Value, TextValue>
		{
			// Token: 0x060051E3 RID: 20963 RVA: 0x00114640 File Offset: 0x00112840
			public LoadStringFunctionValue(IEngineHost engineHost)
				: base(NullableTypeValue.Text, 1, "string", TypeValue.Text)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051E4 RID: 20964 RVA: 0x00114660 File Offset: 0x00112860
			public override Value TypedInvoke(TextValue stringId)
			{
				ILibraryService libraryService = this.engineHost.QueryService<ILibraryService>();
				if (libraryService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				string asString = stringId.AsString;
				return TextValue.NewOrNull(libraryService.GetResourceString(ExtensibilityModule.GetModuleName(this.engineHost), CultureInfo.CurrentUICulture.Name, asString));
			}

			// Token: 0x04002D02 RID: 11522
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BAC RID: 2988
		public sealed class ContentsFunctionValue : NativeFunctionValue1<BinaryValue, TextValue>
		{
			// Token: 0x060051E5 RID: 20965 RVA: 0x001146AF File Offset: 0x001128AF
			public ContentsFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Binary, 1, "file", TypeValue.Text)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051E6 RID: 20966 RVA: 0x001146CE File Offset: 0x001128CE
			public override BinaryValue TypedInvoke(TextValue filename)
			{
				ILibraryService libraryService = this.engineHost.QueryService<ILibraryService>();
				if (libraryService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				return new ExtensibilityModule.ContentsFunctionValue.ResourcesBinaryValue(libraryService, ExtensibilityModule.GetModuleName(this.engineHost), filename.AsString);
			}

			// Token: 0x04002D03 RID: 11523
			private readonly IEngineHost engineHost;

			// Token: 0x02000BAD RID: 2989
			private class ResourcesBinaryValue : StreamedBinaryValue
			{
				// Token: 0x060051E7 RID: 20967 RVA: 0x00114701 File Offset: 0x00112901
				public ResourcesBinaryValue(ILibraryService libraryService, string moduleName, string filename)
				{
					this.libraryService = libraryService;
					this.moduleName = moduleName;
					this.filename = filename;
				}

				// Token: 0x060051E8 RID: 20968 RVA: 0x0011471E File Offset: 0x0011291E
				public override Stream Open()
				{
					byte[] resourceFile = this.libraryService.GetResourceFile(this.moduleName, this.filename);
					if (resourceFile == null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.FileNotFound(this.filename), TextValue.New(this.filename), null);
					}
					return new MemoryStream(resourceFile);
				}

				// Token: 0x04002D04 RID: 11524
				private readonly ILibraryService libraryService;

				// Token: 0x04002D05 RID: 11525
				private readonly string moduleName;

				// Token: 0x04002D06 RID: 11526
				private readonly string filename;
			}
		}

		// Token: 0x02000BAE RID: 2990
		public sealed class InvokeWithCredentialsFunctionValue : NativeFunctionValue2<Value, FunctionValue, FunctionValue>
		{
			// Token: 0x060051E9 RID: 20969 RVA: 0x0011475C File Offset: 0x0011295C
			public InvokeWithCredentialsFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, "authenticationHandler", TypeValue.Function, "function", TypeValue.Function)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051EA RID: 20970 RVA: 0x0007E590 File Offset: 0x0007C790
			public override Value TypedInvoke(FunctionValue authenticationHandler, FunctionValue function)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
			}

			// Token: 0x04002D07 RID: 11527
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BAF RID: 2991
		public sealed class InvokeWithPermissionsFunctionValue : NativeFunctionValue2<Value, FunctionValue, FunctionValue>
		{
			// Token: 0x060051EB RID: 20971 RVA: 0x00114784 File Offset: 0x00112984
			public InvokeWithPermissionsFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, "permissionHandler", TypeValue.Function, "function", TypeValue.Function)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051EC RID: 20972 RVA: 0x0007E590 File Offset: 0x0007C790
			public override Value TypedInvoke(FunctionValue permissionHandler, FunctionValue function)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
			}

			// Token: 0x04002D08 RID: 11528
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BB0 RID: 2992
		private sealed class InvokeVolatileFunctionFunctionValue : NativeFunctionValue1<Value, FunctionValue>
		{
			// Token: 0x060051ED RID: 20973 RVA: 0x001147AC File Offset: 0x001129AC
			public InvokeVolatileFunctionFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, "function", TypeValue.Function)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051EE RID: 20974 RVA: 0x001147CA File Offset: 0x001129CA
			public override Value TypedInvoke(FunctionValue function)
			{
				if (this.function == null)
				{
					this.function = this.MakeVolatileFunction(function);
				}
				return this.function.Invoke();
			}

			// Token: 0x060051EF RID: 20975 RVA: 0x001147EC File Offset: 0x001129EC
			private FunctionValue MakeVolatileFunction(FunctionValue function)
			{
				ValueException errorResult = null;
				IExpression expression = function.Expression;
				if (expression != null && expression.Kind == ExpressionKind.Function)
				{
					IFunctionExpression functionExpression = (IFunctionExpression)function.Expression;
					IExpression expression2;
					if (functionExpression.Count == 0 && ExtensibilityModule.InvokeVolatileFunctionFunctionValue.VolatileFunctionVisitor.TryFold(functionExpression.Expression, out expression2))
					{
						IEngineHost engineHost = new ExtensibilityModule.InvokeVolatileFunctionFunctionValue.EnableVolatileFunctionEngineHost(this.engineHost);
						RecordValue library = Modules.GetLibrary(engineHost, Extension.Modules);
						Assembly assembly = Linker.Assemble(new Compiler(CompileOptions.None).Compile(new ExpressionDocumentSyntaxNode(expression2), library), engineHost, delegate(IError e)
						{
							if (errorResult == null)
							{
								errorResult = ValueException.NewExpressionError(e.Message, null, null);
							}
						});
						if (errorResult == null)
						{
							return assembly.Function;
						}
					}
				}
				if (errorResult == null)
				{
					errorResult = ValueException.NewDataSourceError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}
				return new ExtensibilityModule.InvokeVolatileFunctionFunctionValue.ExceptionFunctionValue(errorResult);
			}

			// Token: 0x04002D09 RID: 11529
			private readonly IEngineHost engineHost;

			// Token: 0x04002D0A RID: 11530
			private FunctionValue function;

			// Token: 0x02000BB1 RID: 2993
			private sealed class EnableVolatileFunctionEngineHost : IEngineHost, IFoldingFailureService
			{
				// Token: 0x060051F0 RID: 20976 RVA: 0x001148B5 File Offset: 0x00112AB5
				public EnableVolatileFunctionEngineHost(IEngineHost engineHost)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x17001959 RID: 6489
				// (get) Token: 0x060051F1 RID: 20977 RVA: 0x00002105 File Offset: 0x00000305
				public bool ThrowOnVolatileFunctions
				{
					get
					{
						return false;
					}
				}

				// Token: 0x1700195A RID: 6490
				// (get) Token: 0x060051F2 RID: 20978 RVA: 0x001148C4 File Offset: 0x00112AC4
				public bool ThrowOnFoldingFailure
				{
					get
					{
						IFoldingFailureService foldingFailureService = this.engineHost.QueryService<IFoldingFailureService>();
						return foldingFailureService != null && foldingFailureService.ThrowOnFoldingFailure;
					}
				}

				// Token: 0x060051F3 RID: 20979 RVA: 0x001148DC File Offset: 0x00112ADC
				public T QueryService<T>() where T : class
				{
					if (typeof(T) == typeof(IFoldingFailureService))
					{
						return (T)((object)this);
					}
					return this.engineHost.QueryService<T>();
				}

				// Token: 0x04002D0B RID: 11531
				private readonly IEngineHost engineHost;
			}

			// Token: 0x02000BB2 RID: 2994
			private sealed class ExceptionFunctionValue : NativeFunctionValue0
			{
				// Token: 0x060051F4 RID: 20980 RVA: 0x0011490B File Offset: 0x00112B0B
				public ExceptionFunctionValue(ValueException exception)
				{
					this.exception = exception;
				}

				// Token: 0x060051F5 RID: 20981 RVA: 0x0011491A File Offset: 0x00112B1A
				public override Value Invoke()
				{
					throw this.exception;
				}

				// Token: 0x04002D0C RID: 11532
				private readonly ValueException exception;
			}

			// Token: 0x02000BB3 RID: 2995
			private sealed class VolatileFunctionVisitor : ConstantFoldingVisitor
			{
				// Token: 0x060051F6 RID: 20982 RVA: 0x00114924 File Offset: 0x00112B24
				public static bool TryFold(IExpression node, out IExpression result)
				{
					ExtensibilityModule.InvokeVolatileFunctionFunctionValue.VolatileFunctionVisitor volatileFunctionVisitor = new ExtensibilityModule.InvokeVolatileFunctionFunctionValue.VolatileFunctionVisitor();
					IExpression expression = volatileFunctionVisitor.VisitExpression(node);
					if (!volatileFunctionVisitor.foundUnknownFunction)
					{
						result = expression;
						return true;
					}
					result = null;
					return false;
				}

				// Token: 0x060051F7 RID: 20983 RVA: 0x00114950 File Offset: 0x00112B50
				protected override IExpression VisitExpression(IExpression expression)
				{
					IExpression expression2 = base.VisitExpression(expression);
					int num = 0;
					while (num < 3 && expression != expression2)
					{
						expression = expression2;
						expression2 = base.VisitExpression(expression);
					}
					if (num == 3)
					{
						this.foundUnknownFunction = true;
					}
					return expression2;
				}

				// Token: 0x060051F8 RID: 20984 RVA: 0x00114988 File Offset: 0x00112B88
				protected override IExpression VisitFunction(IFunctionExpression function)
				{
					this.foundUnknownFunction = true;
					return base.VisitFunction(function);
				}

				// Token: 0x060051F9 RID: 20985 RVA: 0x00114998 File Offset: 0x00112B98
				protected override IExpression VisitConstant(IConstantExpression constant)
				{
					if (constant.Value.IsFunction)
					{
						IExpression expression = constant.Value.Expression;
						if (expression != null && expression.Kind == ExpressionKind.Identifier)
						{
							return constant.Value.Expression;
						}
						this.foundUnknownFunction = true;
					}
					return base.VisitConstant(constant);
				}

				// Token: 0x04002D0D RID: 11533
				private bool foundUnknownFunction;
			}
		}

		// Token: 0x02000BB5 RID: 2997
		private sealed class HasPermissionFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>
		{
			// Token: 0x060051FD RID: 20989 RVA: 0x00114A0D File Offset: 0x00112C0D
			public HasPermissionFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Logical, "permission", TypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060051FE RID: 20990 RVA: 0x00114A2C File Offset: 0x00112C2C
			public override LogicalValue TypedInvoke(RecordValue permission)
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				PermissionConversion.VerifyQueryPermission(this.engineHost, extensibilityService.CurrentResource, permission);
				return LogicalValue.True;
			}

			// Token: 0x04002D0F RID: 11535
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000BB6 RID: 2998
		private sealed class MakeJsonResourcePathFunctionValue : NativeFunctionValueN
		{
			// Token: 0x060051FF RID: 20991 RVA: 0x00114A6C File Offset: 0x00112C6C
			public MakeJsonResourcePathFunctionValue(string[] parameters)
				: base(parameters.Length, parameters)
			{
				this.keys = ListValue.New(parameters.Select((string p) => TextValue.New(p)));
			}

			// Token: 0x06005200 RID: 20992 RVA: 0x00114AA8 File Offset: 0x00112CA8
			protected override Value InvokeN(Value[] args)
			{
				return Library.Text.FromBinary.Invoke(JsonModule.Json.FromValue.Invoke(Library.Record.FromList.Invoke(ListValue.New(args), this.keys)));
			}

			// Token: 0x04002D10 RID: 11536
			private readonly ListValue keys;
		}

		// Token: 0x02000BB8 RID: 3000
		private sealed class MakeUriResourcePathFunctionValue : NativeFunctionValueN
		{
			// Token: 0x06005204 RID: 20996 RVA: 0x00114AE0 File Offset: 0x00112CE0
			public MakeUriResourcePathFunctionValue(string[] parameters, int urlPosition)
				: base(parameters.Length, parameters)
			{
				this.urlPosition = urlPosition;
				this.keys = ListValue.New(from p in parameters.Where((string p, int i) => i != urlPosition)
					select TextValue.New(p));
			}

			// Token: 0x06005205 RID: 20997 RVA: 0x00114B54 File Offset: 0x00112D54
			protected override Value InvokeN(Value[] args)
			{
				int num = 0;
				Value[] array = new Value[args.Length - 1];
				for (int i = 0; i < args.Length; i++)
				{
					if (i != this.urlPosition)
					{
						array[num++] = args[i].AsText;
					}
				}
				return TextValue.New(UriHelper.AddQueryRecord(UriHelper.CreateAbsoluteUriFromValue(args[this.urlPosition].AsText), Library.Record.FromList.Invoke(ListValue.New(array), this.keys).AsRecord).AbsoluteUri);
			}

			// Token: 0x04002D13 RID: 11539
			private readonly int urlPosition;

			// Token: 0x04002D14 RID: 11540
			private readonly ListValue keys;
		}

		// Token: 0x02000BBB RID: 3003
		private sealed class ParseJsonResourcePathFunctionValue : NativeFunctionValue1<ListValue, TextValue>
		{
			// Token: 0x0600520B RID: 21003 RVA: 0x00114BEA File Offset: 0x00112DEA
			private ParseJsonResourcePathFunctionValue()
				: base(TypeValue.List, "resource", TypeValue.Text)
			{
			}

			// Token: 0x0600520C RID: 21004 RVA: 0x00114C01 File Offset: 0x00112E01
			public override ListValue TypedInvoke(TextValue resource)
			{
				return Library.Record.ToList.Invoke(JsonModule.Json.Document.Invoke(resource)).AsList;
			}

			// Token: 0x04002D18 RID: 11544
			public static readonly FunctionValue Instance = new ExtensibilityModule.ParseJsonResourcePathFunctionValue();
		}

		// Token: 0x02000BBC RID: 3004
		private sealed class ParseUriResourcePathFunctionValue : NativeFunctionValue1<ListValue, TextValue>
		{
			// Token: 0x0600520E RID: 21006 RVA: 0x00114C29 File Offset: 0x00112E29
			public ParseUriResourcePathFunctionValue(string[] parameters, int urlPosition)
				: base(TypeValue.List, "resource", TypeValue.Text)
			{
				this.parameters = parameters;
				this.urlPosition = urlPosition;
			}

			// Token: 0x0600520F RID: 21007 RVA: 0x00114C50 File Offset: 0x00112E50
			public override ListValue TypedInvoke(TextValue resource)
			{
				UriBuilder uriBuilder = UriHelper.CreateAbsoluteUriBuilderFromValue(resource);
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
				Value[] array = new Value[this.parameters.Length];
				for (int i = 0; i < this.parameters.Length; i++)
				{
					if (i != this.urlPosition)
					{
						array[i] = TextValue.New(nameValueCollection[this.parameters[i]]);
						nameValueCollection.Remove(this.parameters[i]);
					}
				}
				uriBuilder.Query = nameValueCollection.ToString();
				array[this.urlPosition] = TextValue.New(uriBuilder.Uri.AbsoluteUri);
				return ListValue.New(array);
			}

			// Token: 0x04002D19 RID: 11545
			private readonly string[] parameters;

			// Token: 0x04002D1A RID: 11546
			private readonly int urlPosition;
		}

		// Token: 0x02000BBD RID: 3005
		private class WebDefaultProxyFunctionValue : NativeFunctionValue1<RecordValue, TextValue>
		{
			// Token: 0x06005210 RID: 21008 RVA: 0x00114CE8 File Offset: 0x00112EE8
			public WebDefaultProxyFunctionValue()
				: base(ExtensibilityModule.WebDefaultProxyFunctionValue.returnType.Nullable, "uri", TypeValue.Uri)
			{
			}

			// Token: 0x06005211 RID: 21009 RVA: 0x00114D04 File Offset: 0x00112F04
			public override RecordValue TypedInvoke(TextValue uri)
			{
				Uri uri2 = UriHelper.CreateAbsoluteUriFromValue(uri);
				if (Uri.IsWellFormedUriString(uri2.ToString(), UriKind.Absolute))
				{
					Uri proxy = WebRequest.DefaultWebProxy.GetProxy(uri2);
					if (!uri2.AbsoluteUri.Equals(proxy.AbsoluteUri, StringComparison.Ordinal))
					{
						return RecordValue.New(ExtensibilityModule.WebDefaultProxyFunctionValue.returnType, new Value[] { TextValue.New(proxy.OriginalString) });
					}
				}
				return RecordValue.Empty;
			}

			// Token: 0x04002D1B RID: 11547
			private static readonly Keys ReturnKeys = Keys.New("ProxyUri");

			// Token: 0x04002D1C RID: 11548
			private static readonly RecordTypeValue returnType = RecordTypeValue.New(RecordValue.New(ExtensibilityModule.WebDefaultProxyFunctionValue.ReturnKeys, new Value[] { RecordTypeValue.NewField(TypeValue.Uri, LogicalValue.False) }), false);
		}

		// Token: 0x02000BBE RID: 3006
		private static class DataSource
		{
			// Token: 0x02000BBF RID: 3007
			public sealed class ExtensionInfoFunctionValue : NativeFunctionValue0<FunctionValue>
			{
				// Token: 0x06005213 RID: 21011 RVA: 0x00114DA8 File Offset: 0x00112FA8
				public ExtensionInfoFunctionValue(ExtensionModule extensionModule, string resourceKind, string publicFunctionName, string privateFunctionName, string handlerFunctionName, string invocationKind)
					: base(TypeValue.Function)
				{
					this.extensionModule = extensionModule;
					this.resourceKind = resourceKind;
					this.publicFunctionName = publicFunctionName;
					this.privateFunctionName = privateFunctionName;
					this.handlerFunctionName = handlerFunctionName;
					this.invocationKind = invocationKind;
				}

				// Token: 0x1700195B RID: 6491
				// (get) Token: 0x06005214 RID: 21012 RVA: 0x00114DE2 File Offset: 0x00112FE2
				public ExtensionModule Module
				{
					get
					{
						return this.extensionModule;
					}
				}

				// Token: 0x1700195C RID: 6492
				// (get) Token: 0x06005215 RID: 21013 RVA: 0x00114DEA File Offset: 0x00112FEA
				public ResourceKindInfo ResourceKindInfo
				{
					get
					{
						return this.extensionModule.DataSources.FirstOrDefault((ResourceKindInfo ds) => ds.Kind == this.resourceKind);
					}
				}

				// Token: 0x1700195D RID: 6493
				// (get) Token: 0x06005216 RID: 21014 RVA: 0x00114E08 File Offset: 0x00113008
				public string PublicFunctionName
				{
					get
					{
						return this.publicFunctionName;
					}
				}

				// Token: 0x1700195E RID: 6494
				// (get) Token: 0x06005217 RID: 21015 RVA: 0x00114E10 File Offset: 0x00113010
				public string PrivateFunctionName
				{
					get
					{
						return this.privateFunctionName;
					}
				}

				// Token: 0x1700195F RID: 6495
				// (get) Token: 0x06005218 RID: 21016 RVA: 0x00114E18 File Offset: 0x00113018
				public string HandlerFunctionName
				{
					get
					{
						return this.handlerFunctionName;
					}
				}

				// Token: 0x17001960 RID: 6496
				// (get) Token: 0x06005219 RID: 21017 RVA: 0x00114E20 File Offset: 0x00113020
				public string InvocationKind
				{
					get
					{
						return this.invocationKind;
					}
				}

				// Token: 0x0600521A RID: 21018 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
				public override FunctionValue TypedInvoke()
				{
					return this;
				}

				// Token: 0x04002D1D RID: 11549
				private readonly ExtensionModule extensionModule;

				// Token: 0x04002D1E RID: 11550
				private readonly string resourceKind;

				// Token: 0x04002D1F RID: 11551
				private readonly string publicFunctionName;

				// Token: 0x04002D20 RID: 11552
				private readonly string privateFunctionName;

				// Token: 0x04002D21 RID: 11553
				private readonly string handlerFunctionName;

				// Token: 0x04002D22 RID: 11554
				private readonly string invocationKind;
			}

			// Token: 0x02000BC0 RID: 3008
			public sealed class FunctionFunctionValue : NativeFunctionValue4<FunctionValue, FunctionValue, FunctionValue, Value, Value>
			{
				// Token: 0x0600521C RID: 21020 RVA: 0x00114E3C File Offset: 0x0011303C
				public FunctionFunctionValue(IEngineHost engineHost)
					: base(TypeValue.Function, "extensionInfo", TypeValue.Function, "function", TypeValue.Function, "handlerFunction", NullableTypeValue.Function, "publish", NullableTypeValue.Record)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x0600521D RID: 21021 RVA: 0x00114E84 File Offset: 0x00113084
				public override FunctionValue TypedInvoke(FunctionValue extensionInfo, FunctionValue function, Value handlerFunction, Value publish)
				{
					ExtensibilityModule.DataSource.ExtensionInfoFunctionValue extensionInfoFunctionValue = extensionInfo as ExtensibilityModule.DataSource.ExtensionInfoFunctionValue;
					if (extensionInfoFunctionValue == null)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
					}
					return HostRelinkingFunctionValue.New(this.engineHost, extensionInfoFunctionValue.InvocationKind, extensionInfoFunctionValue.ResourceKindInfo, extensionInfoFunctionValue.Module, extensionInfoFunctionValue.PublicFunctionName, extensionInfoFunctionValue.PrivateFunctionName, extensionInfoFunctionValue.HandlerFunctionName, handlerFunction.IsNull ? null : handlerFunction.AsFunction, function.Type.AsFunctionType, publish);
				}

				// Token: 0x04002D23 RID: 11555
				private readonly IEngineHost engineHost;
			}
		}
	}
}
