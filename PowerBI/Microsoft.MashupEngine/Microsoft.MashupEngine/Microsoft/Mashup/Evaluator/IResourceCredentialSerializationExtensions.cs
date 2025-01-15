using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD8 RID: 7384
	public static class IResourceCredentialSerializationExtensions
	{
		// Token: 0x0600B7DC RID: 47068 RVA: 0x00254928 File Offset: 0x00252B28
		public static void WriteIResourceCredential(this BinaryWriter writer, IResourceCredential credential)
		{
			writer.WriteAny(credential, IResourceCredentialSerializationExtensions.identifiers, IResourceCredentialSerializationExtensions.writers);
		}

		// Token: 0x0600B7DD RID: 47069 RVA: 0x0025493B File Offset: 0x00252B3B
		public static IResourceCredential ReadIResourceCredential(this BinaryReader reader)
		{
			return reader.ReadAny(IResourceCredentialSerializationExtensions.readers);
		}

		// Token: 0x0600B7DE RID: 47070 RVA: 0x00254948 File Offset: 0x00252B48
		public static void WriteResourceCredentialCollection(this BinaryWriter writer, ResourceCredentialCollection credentials)
		{
			writer.WriteIResource(credentials.Resource);
			writer.WriteArray(credentials.ToArray<IResourceCredential>(), new Action<BinaryWriter, IResourceCredential>(IResourceCredentialSerializationExtensions.WriteIResourceCredential));
		}

		// Token: 0x0600B7DF RID: 47071 RVA: 0x00254970 File Offset: 0x00252B70
		public static ResourceCredentialCollection ReadResourceCredentialCollection(this BinaryReader reader)
		{
			IResource resource = reader.ReadIResource();
			IResourceCredential[] array = reader.ReadArray(new Func<BinaryReader, IResourceCredential>(IResourceCredentialSerializationExtensions.ReadIResourceCredential));
			return new ResourceCredentialCollection(resource, array);
		}

		// Token: 0x0600B7E0 RID: 47072 RVA: 0x0025499C File Offset: 0x00252B9C
		private static void WriteUsernamePasswordCredential(BinaryWriter writer, UsernamePasswordCredential credential)
		{
			writer.WriteNullableString(credential.Username);
			writer.WriteNullableString(credential.Password);
		}

		// Token: 0x0600B7E1 RID: 47073 RVA: 0x002549B6 File Offset: 0x00252BB6
		private static string ReadUsernamePasswordCredential(BinaryReader reader, out string password)
		{
			string text = reader.ReadNullableString();
			password = reader.ReadNullableString();
			return text;
		}

		// Token: 0x0600B7E2 RID: 47074 RVA: 0x002549C6 File Offset: 0x00252BC6
		private static void WriteBasicAuthCredential(BinaryWriter writer, BasicAuthCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
		}

		// Token: 0x0600B7E3 RID: 47075 RVA: 0x002549CF File Offset: 0x00252BCF
		private static string ReadBasicAuthCredential(BinaryReader reader, out string password)
		{
			return IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out password);
		}

		// Token: 0x0600B7E4 RID: 47076 RVA: 0x002549D8 File Offset: 0x00252BD8
		private static BasicAuthCredential ReadBasicAuthCredential(BinaryReader reader)
		{
			string text;
			return new BasicAuthCredential(IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text), text);
		}

		// Token: 0x0600B7E5 RID: 47077 RVA: 0x002549C6 File Offset: 0x00252BC6
		private static void WriteSqlAuthCredential(BinaryWriter writer, SqlAuthCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
		}

		// Token: 0x0600B7E6 RID: 47078 RVA: 0x002549F4 File Offset: 0x00252BF4
		private static SqlAuthCredential ReadSqlAuthCredential(BinaryReader reader)
		{
			string text;
			return new SqlAuthCredential(IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text), text);
		}

		// Token: 0x0600B7E7 RID: 47079 RVA: 0x002549C6 File Offset: 0x00252BC6
		private static void WriteFeedKeyCredential(BinaryWriter writer, FeedKeyCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
		}

		// Token: 0x0600B7E8 RID: 47080 RVA: 0x00254A10 File Offset: 0x00252C10
		private static FeedKeyCredential ReadFeedKeyCredential(BinaryReader reader)
		{
			string text;
			IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text);
			return new FeedKeyCredential(text);
		}

		// Token: 0x0600B7E9 RID: 47081 RVA: 0x002549C6 File Offset: 0x00252BC6
		private static void WriteSharedKeyAuthCredential(BinaryWriter writer, SharedKeyAuthCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
		}

		// Token: 0x0600B7EA RID: 47082 RVA: 0x00254A2C File Offset: 0x00252C2C
		private static SharedKeyAuthCredential ReadSharedKeyAuthCredential(BinaryReader reader)
		{
			string text;
			IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text);
			return new SharedKeyAuthCredential(text);
		}

		// Token: 0x0600B7EB RID: 47083 RVA: 0x00254A48 File Offset: 0x00252C48
		private static void WriteSapBasicAuthCredential(BinaryWriter writer, SapBasicAuthCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
			writer.WriteString(credential.Authentication);
		}

		// Token: 0x0600B7EC RID: 47084 RVA: 0x00254A60 File Offset: 0x00252C60
		private static SapBasicAuthCredential ReadSapBasicAuthCredential(BinaryReader reader)
		{
			string text2;
			string text = IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text2);
			string text3 = reader.ReadString();
			return new SapBasicAuthCredential(text, text2, text3);
		}

		// Token: 0x0600B7ED RID: 47085 RVA: 0x002549C6 File Offset: 0x00252BC6
		private static void WriteFtpLoginAuthCredential(BinaryWriter writer, FtpLoginAuthCredential credential)
		{
			IResourceCredentialSerializationExtensions.WriteUsernamePasswordCredential(writer, credential);
		}

		// Token: 0x0600B7EE RID: 47086 RVA: 0x00254A84 File Offset: 0x00252C84
		private static FtpLoginAuthCredential ReadFtpLoginAuthCredential(BinaryReader reader)
		{
			string text;
			return new FtpLoginAuthCredential(IResourceCredentialSerializationExtensions.ReadUsernamePasswordCredential(reader, out text), text);
		}

		// Token: 0x0600B7EF RID: 47087 RVA: 0x00254AA0 File Offset: 0x00252CA0
		private static void WriteOAuthCredential(BinaryWriter writer, OAuthCredential credential)
		{
			writer.WriteString(credential.AccessToken);
			writer.WriteNullableString(credential.Expires);
			writer.WriteNullableString(credential.RefreshToken);
			writer.WriteNullable(credential.Properties, delegate(BinaryWriter w, Dictionary<string, string> dict)
			{
				w.WriteDictionary(dict, new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
			});
		}

		// Token: 0x0600B7F0 RID: 47088 RVA: 0x00254AFC File Offset: 0x00252CFC
		private static OAuthCredential ReadOAuthCredential(BinaryReader reader)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadNullableString();
			string text3 = reader.ReadNullableString();
			Dictionary<string, string> dictionary = reader.ReadNullable(delegate(BinaryReader r)
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				r.ReadDictionary(dictionary2, new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString), new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
				return dictionary2;
			});
			return new OAuthCredential(text, text2, text3, dictionary);
		}

		// Token: 0x0600B7F1 RID: 47089 RVA: 0x00254B4B File Offset: 0x00252D4B
		private static void WriteWebApiKeyCredential(BinaryWriter writer, WebApiKeyCredential credential)
		{
			writer.WriteString(credential.ApiKeyValue);
		}

		// Token: 0x0600B7F2 RID: 47090 RVA: 0x00254B59 File Offset: 0x00252D59
		private static WebApiKeyCredential ReadWebApiKeyCredential(BinaryReader reader)
		{
			return new WebApiKeyCredential(reader.ReadString());
		}

		// Token: 0x0600B7F3 RID: 47091 RVA: 0x00254B68 File Offset: 0x00252D68
		private static void WriteWindowsCredential(BinaryWriter writer, WindowsCredential credential)
		{
			if (credential.Token != null)
			{
				writer.WriteInt32(2);
				writer.WriteInt64((long)ProcessHelpers.ProcessHandle.DangerousGetHandle());
				writer.WriteInt64((long)credential.Token.DangerousGetHandle());
				writer.WriteString(credential.Username);
				return;
			}
			if (credential.OverrideCurrentUser)
			{
				writer.WriteInt32(1);
				writer.WriteString(credential.Username);
				writer.WriteNullableString(credential.Password);
				return;
			}
			writer.WriteInt32(0);
		}

		// Token: 0x0600B7F4 RID: 47092 RVA: 0x00254BEC File Offset: 0x00252DEC
		private static WindowsCredential ReadWindowsCredential(BinaryReader reader)
		{
			switch (reader.ReadInt32())
			{
			case 0:
				return new WindowsCredential();
			case 1:
			{
				string text = reader.ReadString();
				string text2 = reader.ReadNullableString();
				return new WindowsCredential(text, text2);
			}
			case 2:
			{
				IntPtr intPtr = (IntPtr)reader.ReadInt64();
				IntPtr intPtr2 = (IntPtr)reader.ReadInt64();
				string text = reader.ReadString();
				return new WindowsCredential(ProcessHelpers.DuplicateRemoteHandle(intPtr, intPtr2), text);
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600B7F5 RID: 47093 RVA: 0x00254C60 File Offset: 0x00252E60
		private static void WriteParameterizedCredential(BinaryWriter writer, ParameterizedCredential credential)
		{
			writer.WriteString(credential.Name);
			if (credential.Values == null)
			{
				writer.WriteInt32(0);
				return;
			}
			writer.WriteInt32(credential.Values.Count);
			foreach (KeyValuePair<string, string> keyValuePair in credential.Values)
			{
				writer.WriteString(keyValuePair.Key);
				writer.WriteString(keyValuePair.Value);
			}
		}

		// Token: 0x0600B7F6 RID: 47094 RVA: 0x00254CF4 File Offset: 0x00252EF4
		private static ParameterizedCredential ReadParameterizedCredential(BinaryReader reader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text = reader.ReadString();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text2 = reader.ReadString();
				string text3 = reader.ReadString();
				dictionary[text2] = text3;
			}
			return new ParameterizedCredential(text, dictionary);
		}

		// Token: 0x0600B7F7 RID: 47095 RVA: 0x00254D42 File Offset: 0x00252F42
		private static void WriteEncryptedConnectionAdornment(BinaryWriter writer, EncryptedConnectionAdornment credential)
		{
			writer.WriteBool(credential.RequireEncryption);
		}

		// Token: 0x0600B7F8 RID: 47096 RVA: 0x00254D50 File Offset: 0x00252F50
		private static EncryptedConnectionAdornment ReadEncryptedConnectionAdornment(BinaryReader reader)
		{
			return new EncryptedConnectionAdornment(reader.ReadBool());
		}

		// Token: 0x0600B7F9 RID: 47097 RVA: 0x00254D5D File Offset: 0x00252F5D
		private static void WriteConnectionStringPropertyAdornment(BinaryWriter writer, ConnectionStringPropertiesAdornment credential)
		{
			writer.WriteDictionary(credential.Properties, new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteNullableString));
		}

		// Token: 0x0600B7FA RID: 47098 RVA: 0x00254D84 File Offset: 0x00252F84
		private static ConnectionStringPropertiesAdornment ReadConnectionStringPropertyAdornment(BinaryReader reader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			reader.ReadDictionary(dictionary, new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString), new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadNullableString));
			return new ConnectionStringPropertiesAdornment(dictionary);
		}

		// Token: 0x0600B7FB RID: 47099 RVA: 0x00254DBC File Offset: 0x00252FBC
		private static void WriteApplicationCredentialPropertiesAdornment(BinaryWriter writer, ApplicationCredentialPropertiesAdornment credential)
		{
			writer.WriteDictionary(credential.Properties, new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString), delegate(BinaryWriter w, object o)
			{
				w.WriteAny(o, IResourceCredentialSerializationExtensions.objectIdentifiers, IResourceCredentialSerializationExtensions.objectWriters);
			});
		}

		// Token: 0x0600B7FC RID: 47100 RVA: 0x00254DF8 File Offset: 0x00252FF8
		private static ApplicationCredentialPropertiesAdornment ReadApplicationCredentialPropertiesAdornment(BinaryReader reader)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			reader.ReadDictionary(dictionary, new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString), (BinaryReader r) => r.ReadAny(IResourceCredentialSerializationExtensions.objectReaders));
			return new ApplicationCredentialPropertiesAdornment(dictionary);
		}

		// Token: 0x0600B7FD RID: 47101 RVA: 0x00254E43 File Offset: 0x00253043
		private static void WriteExchangeSettingsAdornment(BinaryWriter writer, ExchangeCredentialAdornment credential)
		{
			writer.WriteNullableString(credential.EwsUrl);
			writer.WriteNullableString(credential.EwsSupportedSchema);
			writer.WriteNullableString(credential.EmailAddress);
		}

		// Token: 0x0600B7FE RID: 47102 RVA: 0x00254E6C File Offset: 0x0025306C
		private static ExchangeCredentialAdornment ReadExchangeSettingsAdornment(BinaryReader reader)
		{
			string text = reader.ReadNullableString();
			string text2 = reader.ReadNullableString();
			string text3 = reader.ReadNullableString();
			return new ExchangeCredentialAdornment(text, text2, text3);
		}

		// Token: 0x0600B7FF RID: 47103 RVA: 0x00254E94 File Offset: 0x00253094
		private static void WriteConnectionStringAdornment(BinaryWriter writer, ConnectionStringAdornment credential)
		{
			writer.WriteString(credential.ConnectionString);
		}

		// Token: 0x0600B800 RID: 47104 RVA: 0x00254EA2 File Offset: 0x002530A2
		private static ConnectionStringAdornment ReadConnectionStringAdornment(BinaryReader reader)
		{
			return new ConnectionStringAdornment(reader.ReadString());
		}

		// Token: 0x04005DC4 RID: 24004
		private static readonly Func<IResourceCredential, bool>[] identifiers = new Func<IResourceCredential, bool>[]
		{
			(IResourceCredential credential) => credential is SqlAuthCredential,
			(IResourceCredential credential) => credential is SapBasicAuthCredential,
			(IResourceCredential credential) => credential is FeedKeyCredential,
			(IResourceCredential credential) => credential is SharedKeyAuthCredential,
			(IResourceCredential credential) => credential is BasicAuthCredential,
			(IResourceCredential credential) => credential is FtpLoginAuthCredential,
			(IResourceCredential credential) => credential is OAuthCredential,
			(IResourceCredential credential) => credential is WebApiKeyCredential,
			(IResourceCredential credential) => credential is WindowsCredential,
			(IResourceCredential credential) => credential is ParameterizedCredential,
			(IResourceCredential credential) => credential is EncryptedConnectionAdornment,
			(IResourceCredential credential) => credential is ConnectionStringAdornment,
			(IResourceCredential credential) => credential is ExchangeCredentialAdornment,
			(IResourceCredential credential) => credential is ConnectionStringPropertiesAdornment,
			(IResourceCredential credential) => credential is ApplicationCredentialPropertiesAdornment
		};

		// Token: 0x04005DC5 RID: 24005
		private static readonly Func<BinaryReader, IResourceCredential>[] readers = new Func<BinaryReader, IResourceCredential>[]
		{
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadSqlAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadSapBasicAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadFeedKeyCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadSharedKeyAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadBasicAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadFtpLoginAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadOAuthCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadWebApiKeyCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadWindowsCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadParameterizedCredential(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadEncryptedConnectionAdornment(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadConnectionStringAdornment(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadExchangeSettingsAdornment(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadConnectionStringPropertyAdornment(reader),
			(BinaryReader reader) => IResourceCredentialSerializationExtensions.ReadApplicationCredentialPropertiesAdornment(reader)
		};

		// Token: 0x04005DC6 RID: 24006
		private static readonly Action<BinaryWriter, IResourceCredential>[] writers = new Action<BinaryWriter, IResourceCredential>[]
		{
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteSqlAuthCredential(writer, (SqlAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteSapBasicAuthCredential(writer, (SapBasicAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteFeedKeyCredential(writer, (FeedKeyCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteSharedKeyAuthCredential(writer, (SharedKeyAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteBasicAuthCredential(writer, (BasicAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteFtpLoginAuthCredential(writer, (FtpLoginAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteOAuthCredential(writer, (OAuthCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteWebApiKeyCredential(writer, (WebApiKeyCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteWindowsCredential(writer, (WindowsCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteParameterizedCredential(writer, (ParameterizedCredential)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteEncryptedConnectionAdornment(writer, (EncryptedConnectionAdornment)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteConnectionStringAdornment(writer, (ConnectionStringAdornment)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteExchangeSettingsAdornment(writer, (ExchangeCredentialAdornment)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteConnectionStringPropertyAdornment(writer, (ConnectionStringPropertiesAdornment)credential);
			},
			delegate(BinaryWriter writer, IResourceCredential credential)
			{
				IResourceCredentialSerializationExtensions.WriteApplicationCredentialPropertiesAdornment(writer, (ApplicationCredentialPropertiesAdornment)credential);
			}
		};

		// Token: 0x04005DC7 RID: 24007
		private static readonly Func<object, bool>[] objectIdentifiers = new Func<object, bool>[]
		{
			(object obj) => obj is string,
			(object obj) => obj is Date,
			(object obj) => obj is DateTime,
			(object obj) => obj is DateTimeOffset,
			(object obj) => obj is TimeSpan,
			(object obj) => obj is bool,
			(object obj) => obj is short,
			(object obj) => obj is int,
			(object obj) => obj is long,
			(object obj) => obj is sbyte,
			(object obj) => obj is byte,
			(object obj) => obj is float,
			(object obj) => obj is decimal,
			(object obj) => obj is Currency,
			(object obj) => obj is double,
			(object obj) => obj is Time,
			(object obj) => obj is byte[]
		};

		// Token: 0x04005DC8 RID: 24008
		private static readonly Func<BinaryReader, object>[] objectReaders = new Func<BinaryReader, object>[]
		{
			(BinaryReader reader) => reader.ReadString(),
			(BinaryReader reader) => new Date(reader.ReadDateTime()),
			(BinaryReader reader) => reader.ReadDateTime(),
			(BinaryReader reader) => reader.ReadDateTimeOffset(),
			(BinaryReader reader) => reader.ReadTimeSpan(),
			(BinaryReader reader) => reader.ReadBool(),
			(BinaryReader reader) => reader.ReadInt16(),
			(BinaryReader reader) => reader.ReadInt32(),
			(BinaryReader reader) => reader.ReadInt64(),
			(BinaryReader reader) => reader.ReadSByte(),
			(BinaryReader reader) => reader.ReadByte(),
			(BinaryReader reader) => reader.ReadSingle(),
			(BinaryReader reader) => reader.ReadDecimal(),
			(BinaryReader reader) => new Currency(reader.ReadDecimal()),
			(BinaryReader reader) => reader.ReadDouble(),
			(BinaryReader reader) => new Time(reader.ReadTimeSpan()),
			(BinaryReader reader) => reader.ReadByteArray()
		};

		// Token: 0x04005DC9 RID: 24009
		private static readonly Action<BinaryWriter, object>[] objectWriters = new Action<BinaryWriter, object>[]
		{
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((string)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.WriteDateTime(((Date)obj).DateTime);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.WriteDateTime((DateTime)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.WriteDateTimeOffset((DateTimeOffset)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.WriteTimeSpan((TimeSpan)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((bool)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((short)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((int)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((long)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((sbyte)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((byte)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((float)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((decimal)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write(((Currency)obj).Value);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((double)obj);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.WriteTimeSpan(((Time)obj).TimeSpan);
			},
			delegate(BinaryWriter writer, object obj)
			{
				writer.Write((byte[])obj);
			}
		};
	}
}
