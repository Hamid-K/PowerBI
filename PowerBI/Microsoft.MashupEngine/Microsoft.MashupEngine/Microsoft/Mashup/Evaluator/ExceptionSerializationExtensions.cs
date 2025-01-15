using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CA9 RID: 7337
	public static class ExceptionSerializationExtensions
	{
		// Token: 0x0600B658 RID: 46680 RVA: 0x0025050C File Offset: 0x0024E70C
		public static void WriteException(this BinaryWriter writer, Exception exception)
		{
			writer.WriteAny(exception, ExceptionSerializationExtensions.identifiers, ExceptionSerializationExtensions.writers);
		}

		// Token: 0x0600B659 RID: 46681 RVA: 0x0025051F File Offset: 0x0024E71F
		public static Exception ReadException(this BinaryReader reader)
		{
			return reader.ReadAny(ExceptionSerializationExtensions.readers);
		}

		// Token: 0x0600B65A RID: 46682 RVA: 0x0025052C File Offset: 0x0024E72C
		private static void SerializeExceptionData(BinaryWriter writer, Exception exception)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (object obj in exception.Data)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				if (text != null && (dictionaryEntry.Value is string || dictionaryEntry.Value == null))
				{
					list.Add(new KeyValuePair<string, string>(text, (string)dictionaryEntry.Value));
				}
			}
			writer.WriteInt32(list.Count);
			foreach (KeyValuePair<string, string> keyValuePair in list)
			{
				writer.WriteString(keyValuePair.Key);
				writer.WriteNullableString(keyValuePair.Value);
			}
		}

		// Token: 0x0600B65B RID: 46683 RVA: 0x00250624 File Offset: 0x0024E824
		private static void DeserializeExceptionData(BinaryReader reader, Exception exception)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				string text2 = reader.ReadNullableString();
				exception.Data.Add(text, text2);
			}
		}

		// Token: 0x0600B65C RID: 46684 RVA: 0x0025065F File Offset: 0x0024E85F
		private static void WriteSerializedValueException(BinaryWriter writer, SerializedValueException exception)
		{
			writer.WriteString(exception.SerializedException);
			ExceptionSerializationExtensions.SerializeExceptionData(writer, exception);
		}

		// Token: 0x0600B65D RID: 46685 RVA: 0x00250674 File Offset: 0x0024E874
		private static SerializedValueException ReadSerializedValueException(BinaryReader reader)
		{
			SerializedValueException ex = new SerializedValueException(reader.ReadString());
			ExceptionSerializationExtensions.DeserializeExceptionData(reader, ex);
			return ex;
		}

		// Token: 0x0600B65E RID: 46686 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteUnpermittedResourceAccessException(BinaryWriter writer, UnpermittedResourceAccessException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B65F RID: 46687 RVA: 0x002506A0 File Offset: 0x0024E8A0
		private static UnpermittedResourceAccessException ReadUnpermittedResourceAccessException(BinaryReader reader)
		{
			IResource resource;
			IResource resource2;
			string text;
			string text2;
			string text3;
			string text4;
			ExceptionSerializationExtensions.ReadResourceSecurityException(reader, out resource, out resource2, out text, out text2, out text3, out text4);
			UnpermittedResourceAccessException ex = new UnpermittedResourceAccessException(text2, resource, text, resource2, text3, text4, null);
			ExceptionSerializationExtensions.DeserializeExceptionData(reader, ex);
			return ex;
		}

		// Token: 0x0600B660 RID: 46688 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteInvalidResourceCredentialsException(BinaryWriter writer, InvalidResourceCredentialsException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B661 RID: 46689 RVA: 0x002506D9 File Offset: 0x0024E8D9
		private static InvalidResourceCredentialsException ReadInvalidResourceCredentialsException(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<InvalidResourceCredentialsException>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new InvalidResourceCredentialsException(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B662 RID: 46690 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteResourceEncryptedConnectionException(BinaryWriter writer, ResourceEncryptedConnectionException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B663 RID: 46691 RVA: 0x00250700 File Offset: 0x0024E900
		private static ResourceEncryptedConnectionException ReadResourceEncryptedConnectionException(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<ResourceEncryptedConnectionException>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new ResourceEncryptedConnectionException(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B664 RID: 46692 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteResourceEncryptionPrincipalNameMismatch(BinaryWriter writer, ResourceEncryptionPrincipalNameMismatch exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B665 RID: 46693 RVA: 0x00250727 File Offset: 0x0024E927
		private static ResourceEncryptionPrincipalNameMismatch ReadResourceEncryptionPrincipalNameMismatch(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<ResourceEncryptionPrincipalNameMismatch>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new ResourceEncryptionPrincipalNameMismatch(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B666 RID: 46694 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteResourceAccessAuthorizationException(BinaryWriter writer, ResourceAccessAuthorizationException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B667 RID: 46695 RVA: 0x0025074E File Offset: 0x0024E94E
		private static ResourceAccessAuthorizationException ReadResourceAccessAuthorizationException(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<ResourceAccessAuthorizationException>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new ResourceAccessAuthorizationException(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B668 RID: 46696 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteResourceAccessForbiddenException(BinaryWriter writer, ResourceAccessForbiddenException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B669 RID: 46697 RVA: 0x00250775 File Offset: 0x0024E975
		private static ResourceAccessForbiddenException ReadResourceAccessForbiddenException(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<ResourceAccessForbiddenException>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new ResourceAccessForbiddenException(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B66A RID: 46698 RVA: 0x0025079C File Offset: 0x0024E99C
		private static void WriteQueryPermissionException(BinaryWriter writer, QueryPermissionException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception.ResourceOrigin, exception.Resource, exception.DataSourceLocation, exception.DataSourceLocationOrigin, exception.Message, exception.UserMessage);
			writer.WriteInt32((int)exception.Type);
			writer.WriteInt32(exception.ParameterCount);
			writer.WriteNullable(exception.ParameterNames, delegate(BinaryWriter w, string[] names)
			{
				w.WriteArray(names, delegate(BinaryWriter itemWriter, string item)
				{
					itemWriter.WriteString(item);
				});
			});
			ExceptionSerializationExtensions.SerializeExceptionData(writer, exception);
		}

		// Token: 0x0600B66B RID: 46699 RVA: 0x00250820 File Offset: 0x0024EA20
		private static QueryPermissionException ReadQueryPermissionException(BinaryReader reader)
		{
			IResource resource;
			IResource resource2;
			string text;
			string text2;
			string text3;
			string text4;
			ExceptionSerializationExtensions.ReadResourceSecurityException(reader, out resource, out resource2, out text, out text2, out text3, out text4);
			QueryPermissionChallengeType queryPermissionChallengeType = (QueryPermissionChallengeType)reader.ReadInt32();
			int num = reader.ReadInt32();
			string[] array = reader.ReadNullable((BinaryReader r) => r.ReadArray((BinaryReader itemReader) => itemReader.ReadString()));
			QueryPermissionException ex = new QueryPermissionException(resource2, queryPermissionChallengeType, text3, num, array);
			ExceptionSerializationExtensions.DeserializeExceptionData(reader, ex);
			return ex;
		}

		// Token: 0x0600B66C RID: 46700 RVA: 0x00250695 File Offset: 0x0024E895
		private static void WriteUnpermittedResourceActionException(BinaryWriter writer, UnpermittedResourceActionException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception);
		}

		// Token: 0x0600B66D RID: 46701 RVA: 0x00250890 File Offset: 0x0024EA90
		private static UnpermittedResourceActionException ReadUnpermittedResourceActionException(BinaryReader reader)
		{
			return ExceptionSerializationExtensions.ReadResourceSecurityException<UnpermittedResourceActionException>(reader, (ExceptionSerializationExtensions.ResourceSecurityExceptionArguments a) => new UnpermittedResourceActionException(a.resourceOrigin, a.resource, a.message, a.userMessage, null));
		}

		// Token: 0x0600B66E RID: 46702 RVA: 0x002508B8 File Offset: 0x0024EAB8
		private static T ReadResourceSecurityException<T>(BinaryReader reader, Func<ExceptionSerializationExtensions.ResourceSecurityExceptionArguments, T> ctor) where T : ResourceSecurityException
		{
			IResource resource;
			IResource resource2;
			string text;
			string text2;
			string text3;
			string text4;
			ExceptionSerializationExtensions.ReadResourceSecurityException(reader, out resource, out resource2, out text, out text2, out text3, out text4);
			T t = ctor(new ExceptionSerializationExtensions.ResourceSecurityExceptionArguments
			{
				resource = resource2,
				resourceOrigin = resource,
				message = text3,
				userMessage = text4
			});
			ExceptionSerializationExtensions.DeserializeExceptionData(reader, t);
			return t;
		}

		// Token: 0x0600B66F RID: 46703 RVA: 0x0025091A File Offset: 0x0024EB1A
		private static void ReadResourceSecurityException(BinaryReader reader, out IResource origin, out IResource resource, out string dataSourceLocation, out string dataSourceLocationOrigin, out string message, out string userMessage)
		{
			resource = reader.ReadIResource();
			origin = (reader.ReadBool() ? reader.ReadIResource() : null);
			dataSourceLocation = reader.ReadNullableString();
			dataSourceLocationOrigin = reader.ReadNullableString();
			message = reader.ReadNullableString();
			userMessage = reader.ReadNullableString();
		}

		// Token: 0x0600B670 RID: 46704 RVA: 0x0025095A File Offset: 0x0024EB5A
		private static void WriteResourceSecurityException(BinaryWriter writer, ResourceSecurityException exception)
		{
			ExceptionSerializationExtensions.WriteResourceSecurityException(writer, exception.ResourceOrigin, exception.Resource, exception.DataSourceLocation, exception.DataSourceLocationOrigin, exception.Message, exception.UserMessage);
			ExceptionSerializationExtensions.SerializeExceptionData(writer, exception);
		}

		// Token: 0x0600B671 RID: 46705 RVA: 0x00250990 File Offset: 0x0024EB90
		private static void WriteResourceSecurityException(BinaryWriter writer, IResource origin, IResource resource, string dataSourceLocation, string dataSourceLocationOrigin, string message, string userMessage)
		{
			writer.WriteIResource(resource);
			if (origin == null)
			{
				writer.WriteBool(false);
			}
			else
			{
				writer.WriteBool(true);
				writer.WriteIResource(origin);
			}
			writer.WriteNullableString(dataSourceLocation);
			writer.WriteNullableString(dataSourceLocationOrigin);
			writer.WriteNullableString(message);
			writer.WriteNullableString(userMessage);
		}

		// Token: 0x0600B672 RID: 46706 RVA: 0x002509DD File Offset: 0x0024EBDD
		private static void WriteFirewallRuleException2(BinaryWriter writer, FirewallRuleException2 exception)
		{
			writer.WriteString(exception.Message);
			writer.WriteArray(exception.Resources.ToArray<IResource>(), delegate(BinaryWriter w, IResource r)
			{
				w.WriteIResource(r);
			});
		}

		// Token: 0x0600B673 RID: 46707 RVA: 0x00250A1C File Offset: 0x0024EC1C
		private static FirewallRuleException2 ReadFirewallRuleException2(BinaryReader reader)
		{
			string text = reader.ReadString();
			return new FirewallRuleException2(reader.ReadArray((BinaryReader r) => r.ReadIResource()), text);
		}

		// Token: 0x0600B674 RID: 46708 RVA: 0x00250A5B File Offset: 0x0024EC5B
		private static void WriteFirewallFlowException2(BinaryWriter writer, FirewallFlowException2 exception)
		{
			writer.WriteString(exception.Message);
			writer.WriteArray(exception.Resources, delegate(BinaryWriter w, IResource r)
			{
				w.WriteIResource(r);
			});
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x00250A94 File Offset: 0x0024EC94
		private static FirewallFlowException2 ReadFirewallFlowException2(BinaryReader reader)
		{
			string text = reader.ReadString();
			return new FirewallFlowException2(reader.ReadArray((BinaryReader r) => r.ReadIResource()), text);
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x00250AD3 File Offset: 0x0024ECD3
		private static void WriteFirewallException2(BinaryWriter writer, FirewallException2 exception)
		{
			writer.WriteString(exception.Message);
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x00250AE1 File Offset: 0x0024ECE1
		private static FirewallException2 ReadFirewallException2(BinaryReader reader)
		{
			return new FirewallException2(reader.ReadString(), null);
		}

		// Token: 0x0600B678 RID: 46712 RVA: 0x00250AD3 File Offset: 0x0024ECD3
		private static void WriteDeadlockException(BinaryWriter writer, DeadlockException exception)
		{
			writer.WriteString(exception.Message);
		}

		// Token: 0x0600B679 RID: 46713 RVA: 0x00250AEF File Offset: 0x0024ECEF
		private static DeadlockException ReadDeadlockException(BinaryReader reader)
		{
			return new DeadlockException(reader.ReadString());
		}

		// Token: 0x0600B67A RID: 46714 RVA: 0x00250AFC File Offset: 0x0024ECFC
		private static void WriteHostingException(BinaryWriter writer, HostingException exception)
		{
			writer.WriteString(exception.Message);
			writer.WriteString(exception.Reason);
		}

		// Token: 0x0600B67B RID: 46715 RVA: 0x00250B18 File Offset: 0x0024ED18
		private static HostingException ReadHostingException(BinaryReader reader)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadString();
			return new HostingException(text, text2);
		}

		// Token: 0x0600B67C RID: 46716 RVA: 0x00250AD3 File Offset: 0x0024ECD3
		private static void WritePersistentCacheException(BinaryWriter writer, PersistentCacheException exception)
		{
			writer.WriteString(exception.Message);
		}

		// Token: 0x0600B67D RID: 46717 RVA: 0x00250B38 File Offset: 0x0024ED38
		private static PersistentCacheException ReadPersistentCacheException(BinaryReader reader)
		{
			return new PersistentCacheException(reader.ReadString(), null);
		}

		// Token: 0x0600B67E RID: 46718 RVA: 0x00250B48 File Offset: 0x0024ED48
		private static void WriteErrorException(BinaryWriter writer, ErrorException exception)
		{
			writer.WriteString(exception.Message);
			writer.WriteNullableString(exception.StackTrace);
			writer.WriteNullableString(exception.ClassName);
			writer.WriteBool(exception.IsRecoverable);
			writer.WriteBool(exception.IsExpected);
			writer.WriteBool(exception.InnerException != null);
			if (exception.InnerException != null)
			{
				ExceptionSerializationExtensions.WriteErrorException(writer, exception.InnerException);
			}
		}

		// Token: 0x0600B67F RID: 46719 RVA: 0x00250BB4 File Offset: 0x0024EDB4
		private static ErrorException ReadErrorException(BinaryReader reader)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadNullableString();
			string text3 = reader.ReadNullableString();
			bool flag = reader.ReadBool();
			bool flag2 = reader.ReadBool();
			ErrorException ex = null;
			if (reader.ReadBool())
			{
				ex = ExceptionSerializationExtensions.ReadErrorException(reader);
			}
			return new ErrorException(text, text2, text3, flag, flag2, ex);
		}

		// Token: 0x0600B680 RID: 46720 RVA: 0x00250C01 File Offset: 0x0024EE01
		private static void WriteUnexpectedException(BinaryWriter writer, Exception exception)
		{
			ExceptionSerializationExtensions.WriteErrorException(writer, exception.ToErrorException());
		}

		// Token: 0x04005D2B RID: 23851
		private static readonly Func<Exception, bool>[] identifiers = new Func<Exception, bool>[]
		{
			(Exception exception) => exception is SerializedValueException,
			(Exception exception) => exception is UnpermittedResourceAccessException,
			(Exception exception) => exception is InvalidResourceCredentialsException,
			(Exception exception) => exception is ResourceEncryptedConnectionException,
			(Exception exception) => exception is ResourceEncryptionPrincipalNameMismatch,
			(Exception exception) => exception is ResourceAccessAuthorizationException,
			(Exception exception) => exception is ResourceAccessForbiddenException,
			(Exception exception) => exception is QueryPermissionException,
			(Exception exception) => exception is UnpermittedResourceActionException,
			(Exception exception) => exception is FirewallRuleException2,
			(Exception exception) => exception is FirewallFlowException2,
			(Exception exception) => exception is FirewallException2,
			(Exception exception) => exception is DeadlockException,
			(Exception exception) => exception is HostingException,
			(Exception exception) => exception is ErrorException,
			(Exception exception) => exception is PersistentCacheException,
			(Exception exception) => exception != null
		};

		// Token: 0x04005D2C RID: 23852
		private static readonly Func<BinaryReader, Exception>[] readers = new Func<BinaryReader, Exception>[]
		{
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadSerializedValueException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadUnpermittedResourceAccessException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadInvalidResourceCredentialsException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadResourceEncryptedConnectionException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadResourceEncryptionPrincipalNameMismatch(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadResourceAccessAuthorizationException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadResourceAccessForbiddenException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadQueryPermissionException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadUnpermittedResourceActionException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadFirewallRuleException2(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadFirewallFlowException2(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadFirewallException2(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadDeadlockException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadHostingException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadErrorException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadPersistentCacheException(reader),
			(BinaryReader reader) => ExceptionSerializationExtensions.ReadErrorException(reader)
		};

		// Token: 0x04005D2D RID: 23853
		private static readonly Action<BinaryWriter, Exception>[] writers = new Action<BinaryWriter, Exception>[]
		{
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteSerializedValueException(writer, (SerializedValueException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteUnpermittedResourceAccessException(writer, (UnpermittedResourceAccessException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteInvalidResourceCredentialsException(writer, (InvalidResourceCredentialsException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteResourceEncryptedConnectionException(writer, (ResourceEncryptedConnectionException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteResourceEncryptionPrincipalNameMismatch(writer, (ResourceEncryptionPrincipalNameMismatch)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteResourceAccessAuthorizationException(writer, (ResourceAccessAuthorizationException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteResourceAccessForbiddenException(writer, (ResourceAccessForbiddenException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteQueryPermissionException(writer, (QueryPermissionException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteUnpermittedResourceActionException(writer, (UnpermittedResourceActionException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteFirewallRuleException2(writer, (FirewallRuleException2)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteFirewallFlowException2(writer, (FirewallFlowException2)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteFirewallException2(writer, (FirewallException2)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteDeadlockException(writer, (DeadlockException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteHostingException(writer, (HostingException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteErrorException(writer, (ErrorException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WritePersistentCacheException(writer, (PersistentCacheException)exception);
			},
			delegate(BinaryWriter writer, Exception exception)
			{
				ExceptionSerializationExtensions.WriteUnexpectedException(writer, exception);
			}
		};

		// Token: 0x02001CAA RID: 7338
		private struct ResourceSecurityExceptionArguments
		{
			// Token: 0x04005D2E RID: 23854
			public IResource resource;

			// Token: 0x04005D2F RID: 23855
			public IResource resourceOrigin;

			// Token: 0x04005D30 RID: 23856
			public string message;

			// Token: 0x04005D31 RID: 23857
			public string userMessage;
		}
	}
}
