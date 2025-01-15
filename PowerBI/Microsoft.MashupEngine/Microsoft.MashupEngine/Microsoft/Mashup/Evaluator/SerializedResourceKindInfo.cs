using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D52 RID: 7506
	public class SerializedResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x0600BAB2 RID: 47794 RVA: 0x0025C96C File Offset: 0x0025AB6C
		private SerializedResourceKindInfo(string kind, string label, bool isUri, bool isDatabase, bool isSingleton, bool supportsEncryptedConnection, bool supportsConnectionString, bool? supportsNativeQuery, IList<AuthenticationInfo> authenticationInfo, IList<CredentialProperty> applicationProperties, IList<QueryPermissionChallengeType> permissionKinds, IList<string> connectionStringProperties, IList<IDataSourceLocationFactory> dslFactories, IRecordValue resourceRecord)
			: base(kind, label, isUri, isDatabase, isSingleton, supportsEncryptedConnection, supportsConnectionString, supportsNativeQuery, authenticationInfo, applicationProperties, permissionKinds, connectionStringProperties, dslFactories)
		{
			this.resourceRecord = resourceRecord;
		}

		// Token: 0x17002E19 RID: 11801
		// (get) Token: 0x0600BAB3 RID: 47795 RVA: 0x0025C99E File Offset: 0x0025AB9E
		public override IRecordValue ResourceRecord
		{
			get
			{
				return this.resourceRecord;
			}
		}

		// Token: 0x0600BAB4 RID: 47796 RVA: 0x0025C9A6 File Offset: 0x0025ABA6
		public static SerializedResourceKindInfo Deserialize(IEngine engine, BinaryReader reader)
		{
			return SerializedResourceKindInfo.Deserialize(engine, reader, reader);
		}

		// Token: 0x0600BAB5 RID: 47797 RVA: 0x0025C9B0 File Offset: 0x0025ABB0
		public static SerializedResourceKindInfo Deserialize(IEngine engine, BinaryReader reader, BinaryReader localeReader)
		{
			string text = reader.ReadString();
			string text2 = localeReader.ReadNullableString();
			bool flag = reader.ReadBoolean();
			bool flag2 = reader.ReadBoolean();
			reader.ReadBoolean();
			bool flag3 = reader.ReadBoolean();
			bool flag4 = reader.ReadBoolean();
			bool flag5 = reader.ReadBoolean();
			bool? flag6 = new bool?(reader.ReadBoolean());
			int num = reader.ReadInt32();
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			if (num < 0)
			{
				flag7 = num <= -1;
				flag8 = num <= -2;
				flag9 = num <= -3;
				flag10 = num <= -4;
				num = reader.ReadInt32();
			}
			List<AuthenticationInfo> list = new List<AuthenticationInfo>(num);
			int i = 0;
			while (i < num)
			{
				AuthenticationKind authenticationKind = (AuthenticationKind)reader.ReadInt32();
				string text3 = localeReader.ReadNullableString();
				string text4 = localeReader.ReadNullableString();
				AuthenticationInfo authenticationInfo;
				switch (authenticationKind)
				{
				case AuthenticationKind.Implicit:
					authenticationInfo = new ImplicitAuthenticationInfo();
					break;
				case AuthenticationKind.UsernamePassword:
					authenticationInfo = new UsernamePasswordAuthenticationInfo
					{
						UsernameLabel = localeReader.ReadNullableString(),
						PasswordLabel = localeReader.ReadNullableString()
					};
					break;
				case AuthenticationKind.Windows:
					authenticationInfo = new WindowsAuthenticationInfo
					{
						SupportsAlternateCredentials = reader.ReadBoolean(),
						UsernameLabel = localeReader.ReadNullableString(),
						PasswordLabel = localeReader.ReadNullableString()
					};
					break;
				case AuthenticationKind.WebApi:
					authenticationInfo = new WebApiAuthenticationInfo
					{
						KeyLabel = localeReader.ReadNullableString()
					};
					break;
				case AuthenticationKind.OAuth2:
					authenticationInfo = new OAuth2AuthenticationInfo
					{
						ClientApplicationType = (OAuthClientApplicationType)reader.ReadInt32()
					};
					break;
				case AuthenticationKind.SapBasic:
					goto IL_01AC;
				case AuthenticationKind.Exchange:
					authenticationInfo = new ExchangeAuthenticationInfo();
					break;
				case AuthenticationKind.Key:
					authenticationInfo = new KeyAuthenticationInfo
					{
						KeyLabel = localeReader.ReadNullableString()
					};
					break;
				case AuthenticationKind.Parameterized:
					if (!flag7)
					{
						goto IL_01AC;
					}
					authenticationInfo = new ParameterizedAuthenticationInfo(reader.ReadString(), null);
					break;
				default:
					goto IL_01AC;
				}
				authenticationInfo.Description = text3;
				authenticationInfo.Label = text4;
				if (flag7)
				{
					authenticationInfo.Properties = SerializedResourceKindInfo.DeserializeProperties(reader, localeReader);
					authenticationInfo.ApplicationProperties = SerializedResourceKindInfo.DeserializeProperties(reader, localeReader);
				}
				list.Add(authenticationInfo);
				i++;
				continue;
				IL_01AC:
				throw new IOException("Unsupported authentication kind " + authenticationKind.ToString());
			}
			IList<CredentialProperty> list2 = null;
			IList<QueryPermissionChallengeType> list3 = null;
			IList<string> list4 = null;
			IList<IDataSourceLocationFactory> list5 = null;
			if (flag8)
			{
				list2 = SerializedResourceKindInfo.DeserializeProperties(reader, localeReader);
				list3 = reader.ReadCollection((BinaryReader r) => (QueryPermissionChallengeType)r.ReadInt32());
				list4 = reader.ReadCollection((BinaryReader r) => r.ReadString());
				list5 = reader.ReadCollection((BinaryReader r) => SerializedResourceKindInfo.SerializedDataSourceLocationFactory.New(r.ReadString()));
			}
			IRecordValue recordValue = null;
			if (flag9)
			{
				string text5 = reader.ReadNullableString();
				if (text5 != null)
				{
					recordValue = ValueDeserializer.Deserialize(engine, text5).AsRecord;
				}
			}
			if (flag10 && reader.ReadBoolean())
			{
				flag6 = null;
			}
			return new SerializedResourceKindInfo(text, text2, flag, flag2, flag3, flag4, flag5, flag6, list, list2, list3, list4, list5, recordValue);
		}

		// Token: 0x0600BAB6 RID: 47798 RVA: 0x0025CCB1 File Offset: 0x0025AEB1
		public static void Serialize(IEngine engine, BinaryWriter writer, ResourceKindInfo resourceKind)
		{
			SerializedResourceKindInfo.Serialize(engine, writer, writer, resourceKind);
		}

		// Token: 0x0600BAB7 RID: 47799 RVA: 0x0025CCBC File Offset: 0x0025AEBC
		public static void Serialize(IEngine engine, BinaryWriter writer, BinaryWriter localeWriter, ResourceKindInfo resourceKind)
		{
			writer.Write(resourceKind.Kind);
			localeWriter.WriteNullableString(resourceKind.Label);
			writer.Write(resourceKind.IsUri);
			writer.Write(resourceKind.IsDatabase);
			writer.Write(true);
			writer.Write(resourceKind.IsSingleton);
			writer.Write(resourceKind.SupportsEncryptedConnection);
			writer.Write(resourceKind.SupportsConnectionString);
			writer.Write(resourceKind.SupportsNativeQuery);
			writer.Write(-4);
			writer.Write(resourceKind.AuthenticationInfo.Count);
			foreach (AuthenticationInfo authenticationInfo in resourceKind.AuthenticationInfo)
			{
				writer.Write((int)authenticationInfo.AuthenticationKind);
				localeWriter.WriteNullableString(authenticationInfo.Description);
				localeWriter.WriteNullableString(authenticationInfo.Label);
				switch (authenticationInfo.AuthenticationKind)
				{
				case AuthenticationKind.Implicit:
				case AuthenticationKind.Exchange:
					break;
				case AuthenticationKind.UsernamePassword:
				{
					UsernamePasswordAuthenticationInfo usernamePasswordAuthenticationInfo = (UsernamePasswordAuthenticationInfo)authenticationInfo;
					localeWriter.WriteNullableString(usernamePasswordAuthenticationInfo.UsernameLabel);
					localeWriter.WriteNullableString(usernamePasswordAuthenticationInfo.PasswordLabel);
					break;
				}
				case AuthenticationKind.Windows:
				{
					WindowsAuthenticationInfo windowsAuthenticationInfo = (WindowsAuthenticationInfo)authenticationInfo;
					writer.Write(windowsAuthenticationInfo.SupportsAlternateCredentials);
					localeWriter.WriteNullableString(windowsAuthenticationInfo.UsernameLabel);
					localeWriter.WriteNullableString(windowsAuthenticationInfo.PasswordLabel);
					break;
				}
				case AuthenticationKind.WebApi:
				{
					WebApiAuthenticationInfo webApiAuthenticationInfo = (WebApiAuthenticationInfo)authenticationInfo;
					localeWriter.WriteNullableString(webApiAuthenticationInfo.KeyLabel);
					break;
				}
				case AuthenticationKind.OAuth2:
				{
					OAuth2AuthenticationInfo oauth2AuthenticationInfo = (OAuth2AuthenticationInfo)authenticationInfo;
					writer.Write((int)oauth2AuthenticationInfo.ClientApplicationType);
					break;
				}
				case AuthenticationKind.SapBasic:
					goto IL_01AC;
				case AuthenticationKind.Key:
				{
					KeyAuthenticationInfo keyAuthenticationInfo = (KeyAuthenticationInfo)authenticationInfo;
					localeWriter.WriteNullableString(keyAuthenticationInfo.KeyLabel);
					break;
				}
				case AuthenticationKind.Parameterized:
				{
					ParameterizedAuthenticationInfo parameterizedAuthenticationInfo = (ParameterizedAuthenticationInfo)authenticationInfo;
					writer.Write(parameterizedAuthenticationInfo.Name);
					break;
				}
				default:
					goto IL_01AC;
				}
				SerializedResourceKindInfo.Serialize(writer, localeWriter, authenticationInfo.Properties);
				SerializedResourceKindInfo.Serialize(writer, localeWriter, authenticationInfo.ApplicationProperties);
				continue;
				IL_01AC:
				throw new IOException("Unsupported authentication kind " + authenticationInfo.AuthenticationKind.ToString());
			}
			SerializedResourceKindInfo.Serialize(writer, localeWriter, resourceKind.ApplicationProperties);
			writer.WriteCollection(resourceKind.PermissionKinds, delegate(BinaryWriter w, QueryPermissionChallengeType i)
			{
				w.WriteInt32((int)i);
			});
			writer.WriteCollection(resourceKind.ConnectionStringProperties, delegate(BinaryWriter w, string i)
			{
				w.WriteString(i);
			});
			writer.WriteCollection(resourceKind.DslFactories, delegate(BinaryWriter w, IDataSourceLocationFactory i)
			{
				w.WriteString(i.Protocol);
			});
			string text = ((resourceKind.ResourceRecord == null) ? null : ValueSerializer.SerializePreviewValue(engine, resourceKind.ResourceRecord, null, new ValueSerializerOptions?(SerializedResourceKindInfo.serializationOptions)));
			writer.WriteNullableString(text);
			writer.WriteBool(resourceKind.SupportsNativeQueryChallenge == null);
		}

		// Token: 0x0600BAB8 RID: 47800 RVA: 0x0025CFB8 File Offset: 0x0025B1B8
		private static IList<CredentialProperty> DeserializeProperties(BinaryReader reader, BinaryReader localeReader)
		{
			int num = reader.ReadInt32();
			if (num < 1)
			{
				return null;
			}
			CredentialProperty[] array = new CredentialProperty[num];
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				string text2 = localeReader.ReadNullableString();
				string text3 = reader.ReadString();
				if (!(text3 == "System.String"))
				{
					throw new IOException("Unsupported property type " + text3);
				}
				Type typeFromHandle = typeof(string);
				array[i] = new CredentialProperty
				{
					Name = text,
					Label = text2,
					PropertyType = typeFromHandle,
					IsRequired = reader.ReadBoolean(),
					IsSecret = reader.ReadBoolean()
				};
			}
			return array;
		}

		// Token: 0x0600BAB9 RID: 47801 RVA: 0x0025D068 File Offset: 0x0025B268
		private static void Serialize(BinaryWriter writer, BinaryWriter localeWriter, IList<CredentialProperty> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				writer.Write(0);
				return;
			}
			writer.Write(properties.Count);
			foreach (CredentialProperty credentialProperty in properties)
			{
				writer.Write(credentialProperty.Name);
				localeWriter.WriteNullableString(credentialProperty.Label);
				writer.Write(credentialProperty.PropertyType.FullName);
				writer.Write(credentialProperty.IsRequired);
				writer.Write(credentialProperty.IsSecret);
			}
		}

		// Token: 0x0600BABA RID: 47802 RVA: 0x000091AE File Offset: 0x000073AE
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BABB RID: 47803 RVA: 0x000091AE File Offset: 0x000073AE
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005F01 RID: 24321
		private const int serializationIncludesProperties = -1;

		// Token: 0x04005F02 RID: 24322
		private const int serializationIncludesDslProtocols = -2;

		// Token: 0x04005F03 RID: 24323
		private const int serializationIncludesResourceRecord = -3;

		// Token: 0x04005F04 RID: 24324
		private const int serializationSupportsNullNativeQueryFlag = -4;

		// Token: 0x04005F05 RID: 24325
		private const int currentSerializationFormat = -4;

		// Token: 0x04005F06 RID: 24326
		private const int maxImageLength = 131072;

		// Token: 0x04005F07 RID: 24327
		private static readonly ValueSerializerOptions serializationOptions = new ValueSerializerOptions
		{
			MaxValueDepth = 2,
			NestedRecords = true,
			TruncatedBinaryLength = 131072,
			StripFieldDescriptions = false
		};

		// Token: 0x04005F08 RID: 24328
		private readonly IRecordValue resourceRecord;

		// Token: 0x02001D53 RID: 7507
		private class SerializedDataSourceLocationFactory : IDataSourceLocationFactory
		{
			// Token: 0x0600BABD RID: 47805 RVA: 0x0025D14B File Offset: 0x0025B34B
			private SerializedDataSourceLocationFactory(string protocol)
			{
				this.protocol = protocol;
			}

			// Token: 0x0600BABE RID: 47806 RVA: 0x0025D15A File Offset: 0x0025B35A
			public static IDataSourceLocationFactory New(string protocol)
			{
				return new SerializedResourceKindInfo.SerializedDataSourceLocationFactory(protocol);
			}

			// Token: 0x17002E1A RID: 11802
			// (get) Token: 0x0600BABF RID: 47807 RVA: 0x0025D162 File Offset: 0x0025B362
			public string Protocol
			{
				get
				{
					return this.protocol;
				}
			}

			// Token: 0x0600BAC0 RID: 47808 RVA: 0x000091AE File Offset: 0x000073AE
			public IDataSourceLocation New()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600BAC1 RID: 47809 RVA: 0x000091AE File Offset: 0x000073AE
			public bool TryCreateFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04005F09 RID: 24329
			private readonly string protocol;
		}
	}
}
