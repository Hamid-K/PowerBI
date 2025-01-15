using System;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AA3 RID: 6819
	internal class RemoteLibraryServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ABB8 RID: 43960 RVA: 0x00235E7F File Offset: 0x0023407F
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteLibraryServiceFactory.Stub(engineHost.QueryService<ILibraryService>(), messenger);
		}

		// Token: 0x0600ABB9 RID: 43961 RVA: 0x00235E8D File Offset: 0x0023408D
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteLibraryServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001AA4 RID: 6820
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, ILibraryService
		{
			// Token: 0x0600ABBB RID: 43963 RVA: 0x00235E95 File Offset: 0x00234095
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600ABBC RID: 43964 RVA: 0x00235EA4 File Offset: 0x002340A4
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ILibraryService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600ABBD RID: 43965 RVA: 0x00235EDC File Offset: 0x002340DC
			public string[] GetLoadedVersions(string[] moduleNames)
			{
				string[] loadVersions;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.LibraryVersionRequestMessage
					{
						ModuleNames = moduleNames
					});
					loadVersions = messageChannel.WaitFor<RemoteLibraryServiceFactory.LibraryVersionResponseMessage>().LoadVersions;
				}
				return loadVersions;
			}

			// Token: 0x0600ABBE RID: 43966 RVA: 0x00235F30 File Offset: 0x00234130
			public string GetSource(string moduleName)
			{
				string @string;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.LibrarySourceRequestMessage
					{
						ModuleName = moduleName
					});
					@string = RemoteLibraryServiceFactory.Proxy.GetString(messageChannel.WaitFor<RemoteLibraryServiceFactory.LibraryContentResponseMessage>().Bytes);
				}
				return @string;
			}

			// Token: 0x0600ABBF RID: 43967 RVA: 0x00235F8C File Offset: 0x0023418C
			public string GetResourceString(string moduleName, string cultureName, string stringName)
			{
				string @string;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.LibraryStringRequestMessage
					{
						ModuleName = moduleName,
						CultureName = cultureName,
						StringName = stringName
					});
					@string = RemoteLibraryServiceFactory.Proxy.GetString(messageChannel.WaitFor<RemoteLibraryServiceFactory.LibraryContentResponseMessage>().Bytes);
				}
				return @string;
			}

			// Token: 0x0600ABC0 RID: 43968 RVA: 0x00235FF4 File Offset: 0x002341F4
			public byte[] GetResourceFile(string moduleName, string filename)
			{
				byte[] bytes;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.LibraryFileRequestMessage
					{
						ModuleName = moduleName,
						FileName = filename
					});
					bytes = messageChannel.WaitFor<RemoteLibraryServiceFactory.LibraryContentResponseMessage>().Bytes;
				}
				return bytes;
			}

			// Token: 0x0600ABC1 RID: 43969 RVA: 0x00236050 File Offset: 0x00234250
			public ModuleTrustLevel GetTrustLevel(string moduleName)
			{
				ModuleTrustLevel trustLevel;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.LibraryTrustLevelRequestMessage
					{
						ModuleName = moduleName
					});
					trustLevel = messageChannel.WaitFor<RemoteLibraryServiceFactory.LibraryTrustLevelResponseMessage>().TrustLevel;
				}
				return trustLevel;
			}

			// Token: 0x0600ABC2 RID: 43970 RVA: 0x002360A4 File Offset: 0x002342A4
			public ISerializableValue GetLibraries(string cultureName)
			{
				ISerializableValue table;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage
					{
						Kind = "Libraries",
						Culture = cultureName,
						Scope = null
					});
					table = messageChannel.WaitFor<RemoteLibraryServiceFactory.SerializedTableResponseMessage>().Table;
				}
				return table;
			}

			// Token: 0x0600ABC3 RID: 43971 RVA: 0x0023610C File Offset: 0x0023430C
			public ISerializableValue GetLibraryExports(string cultureName, string libraryIdentifier)
			{
				ISerializableValue table;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage
					{
						Kind = "Exports",
						Culture = cultureName,
						Scope = libraryIdentifier
					});
					table = messageChannel.WaitFor<RemoteLibraryServiceFactory.SerializedTableResponseMessage>().Table;
				}
				return table;
			}

			// Token: 0x0600ABC4 RID: 43972 RVA: 0x00236174 File Offset: 0x00234374
			public ISerializableValue GetLibraryDataSources(string cultureName, string libraryIdentifier)
			{
				ISerializableValue table;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage
					{
						Kind = "DataSources",
						Culture = cultureName,
						Scope = libraryIdentifier
					});
					table = messageChannel.WaitFor<RemoteLibraryServiceFactory.SerializedTableResponseMessage>().Table;
				}
				return table;
			}

			// Token: 0x0600ABC5 RID: 43973 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600ABC6 RID: 43974 RVA: 0x002361DC File Offset: 0x002343DC
			private static string GetString(byte[] bytes)
			{
				if (bytes != null)
				{
					return Encoding.UTF8.GetString(bytes);
				}
				return null;
			}

			// Token: 0x040058FB RID: 22779
			private readonly IMessenger messenger;
		}

		// Token: 0x02001AA5 RID: 6821
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600ABC7 RID: 43975 RVA: 0x002361F0 File Offset: 0x002343F0
			public Stub(ILibraryService libraryService, IMessenger messenger)
			{
				this.libraryService = libraryService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.LibraryVersionRequestMessage>(this.OnLibraryVersionRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.LibrarySourceRequestMessage>(this.OnLibrarySourceRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.LibraryStringRequestMessage>(this.OnLibraryStringRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.LibraryFileRequestMessage>(this.OnLibraryFileRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.LibraryTrustLevelRequestMessage>(this.OnLibraryTrustLevelRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage>(this.OnLibrariesTableRequest));
			}

			// Token: 0x0600ABC8 RID: 43976 RVA: 0x0023629B File Offset: 0x0023449B
			private void OnLibraryVersionRequest(IMessageChannel channel, RemoteLibraryServiceFactory.LibraryVersionRequestMessage message)
			{
				channel.Post(new RemoteLibraryServiceFactory.LibraryVersionResponseMessage
				{
					LoadVersions = this.libraryService.GetLoadedVersions(message.ModuleNames)
				});
			}

			// Token: 0x0600ABC9 RID: 43977 RVA: 0x002362C0 File Offset: 0x002344C0
			private void OnLibrarySourceRequest(IMessageChannel channel, RemoteLibraryServiceFactory.LibrarySourceRequestMessage message)
			{
				byte[] bytes = RemoteLibraryServiceFactory.Stub.GetBytes(this.libraryService.GetSource(message.ModuleName));
				channel.Post(new RemoteLibraryServiceFactory.LibraryContentResponseMessage
				{
					Bytes = bytes
				});
			}

			// Token: 0x0600ABCA RID: 43978 RVA: 0x002362F8 File Offset: 0x002344F8
			private void OnLibraryStringRequest(IMessageChannel channel, RemoteLibraryServiceFactory.LibraryStringRequestMessage message)
			{
				byte[] bytes = RemoteLibraryServiceFactory.Stub.GetBytes(this.libraryService.GetResourceString(message.ModuleName, message.CultureName, message.StringName));
				channel.Post(new RemoteLibraryServiceFactory.LibraryContentResponseMessage
				{
					Bytes = bytes
				});
			}

			// Token: 0x0600ABCB RID: 43979 RVA: 0x0023633C File Offset: 0x0023453C
			private void OnLibraryFileRequest(IMessageChannel channel, RemoteLibraryServiceFactory.LibraryFileRequestMessage message)
			{
				byte[] resourceFile = this.libraryService.GetResourceFile(message.ModuleName, message.FileName);
				channel.Post(new RemoteLibraryServiceFactory.LibraryContentResponseMessage
				{
					Bytes = resourceFile
				});
			}

			// Token: 0x0600ABCC RID: 43980 RVA: 0x00236374 File Offset: 0x00234574
			private void OnLibraryTrustLevelRequest(IMessageChannel channel, RemoteLibraryServiceFactory.LibraryTrustLevelRequestMessage message)
			{
				ModuleTrustLevel trustLevel = this.libraryService.GetTrustLevel(message.ModuleName);
				channel.Post(new RemoteLibraryServiceFactory.LibraryTrustLevelResponseMessage
				{
					TrustLevel = trustLevel
				});
			}

			// Token: 0x0600ABCD RID: 43981 RVA: 0x002363A8 File Offset: 0x002345A8
			private void OnLibrariesTableRequest(IMessageChannel channel, RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage message)
			{
				string kind = message.Kind;
				ISerializableValue serializableValue;
				if (!(kind == "Libraries"))
				{
					if (!(kind == "Exports"))
					{
						if (!(kind == "DataSources"))
						{
							serializableValue = null;
						}
						else
						{
							serializableValue = this.libraryService.GetLibraryDataSources(message.Culture, message.Scope);
						}
					}
					else
					{
						serializableValue = this.libraryService.GetLibraryExports(message.Culture, message.Scope);
					}
				}
				else
				{
					serializableValue = this.libraryService.GetLibraries(message.Culture);
				}
				if (serializableValue != null)
				{
					channel.Post(new RemoteLibraryServiceFactory.SerializedTableResponseMessage
					{
						Table = serializableValue
					});
				}
			}

			// Token: 0x0600ABCE RID: 43982 RVA: 0x00236444 File Offset: 0x00234644
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.LibraryVersionRequestMessage>();
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.LibrarySourceRequestMessage>();
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.LibraryStringRequestMessage>();
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.LibraryFileRequestMessage>();
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.LibraryTrustLevelRequestMessage>();
				this.messenger.RemoveHandler<RemoteLibraryServiceFactory.GetLibrariesTableRequestMessage>();
				this.messenger = null;
				this.libraryService = null;
			}

			// Token: 0x0600ABCF RID: 43983 RVA: 0x002364A1 File Offset: 0x002346A1
			private static byte[] GetBytes(string @string)
			{
				if (@string != null)
				{
					return Encoding.UTF8.GetBytes(@string);
				}
				return null;
			}

			// Token: 0x040058FC RID: 22780
			private ILibraryService libraryService;

			// Token: 0x040058FD RID: 22781
			private IMessenger messenger;
		}

		// Token: 0x02001AA6 RID: 6822
		public sealed class LibraryVersionRequestMessage : BufferedMessage
		{
			// Token: 0x17002B5A RID: 11098
			// (get) Token: 0x0600ABD0 RID: 43984 RVA: 0x002364B3 File Offset: 0x002346B3
			// (set) Token: 0x0600ABD1 RID: 43985 RVA: 0x002364BB File Offset: 0x002346BB
			public string[] ModuleNames { get; set; }

			// Token: 0x0600ABD2 RID: 43986 RVA: 0x002364C4 File Offset: 0x002346C4
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.ModuleNames, delegate(BinaryWriter w, string s)
				{
					w.WriteString(s);
				});
			}

			// Token: 0x0600ABD3 RID: 43987 RVA: 0x002364F1 File Offset: 0x002346F1
			public override void Deserialize(BinaryReader reader)
			{
				this.ModuleNames = reader.ReadArray((BinaryReader r) => r.ReadString());
			}
		}

		// Token: 0x02001AA8 RID: 6824
		public sealed class LibraryVersionResponseMessage : BufferedMessage
		{
			// Token: 0x17002B5B RID: 11099
			// (get) Token: 0x0600ABD9 RID: 43993 RVA: 0x0023652A File Offset: 0x0023472A
			// (set) Token: 0x0600ABDA RID: 43994 RVA: 0x00236532 File Offset: 0x00234732
			public string[] LoadVersions { get; set; }

			// Token: 0x0600ABDB RID: 43995 RVA: 0x0023653B File Offset: 0x0023473B
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.LoadVersions, new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteNullableString));
			}

			// Token: 0x0600ABDC RID: 43996 RVA: 0x00236555 File Offset: 0x00234755
			public override void Deserialize(BinaryReader reader)
			{
				this.LoadVersions = reader.ReadArray(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadNullableString));
			}
		}

		// Token: 0x02001AA9 RID: 6825
		public abstract class LibraryContentRequestMessage : BufferedMessage
		{
			// Token: 0x17002B5C RID: 11100
			// (get) Token: 0x0600ABDE RID: 43998 RVA: 0x0023656F File Offset: 0x0023476F
			// (set) Token: 0x0600ABDF RID: 43999 RVA: 0x00236577 File Offset: 0x00234777
			public string ModuleName { get; set; }

			// Token: 0x0600ABE0 RID: 44000 RVA: 0x00236580 File Offset: 0x00234780
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.ModuleName);
			}

			// Token: 0x0600ABE1 RID: 44001 RVA: 0x0023658E File Offset: 0x0023478E
			public override void Deserialize(BinaryReader reader)
			{
				this.ModuleName = reader.ReadString();
			}
		}

		// Token: 0x02001AAA RID: 6826
		public sealed class LibrarySourceRequestMessage : RemoteLibraryServiceFactory.LibraryContentRequestMessage
		{
		}

		// Token: 0x02001AAB RID: 6827
		public sealed class LibraryStringRequestMessage : RemoteLibraryServiceFactory.LibraryContentRequestMessage
		{
			// Token: 0x17002B5D RID: 11101
			// (get) Token: 0x0600ABE4 RID: 44004 RVA: 0x002365A4 File Offset: 0x002347A4
			// (set) Token: 0x0600ABE5 RID: 44005 RVA: 0x002365AC File Offset: 0x002347AC
			public string CultureName { get; set; }

			// Token: 0x17002B5E RID: 11102
			// (get) Token: 0x0600ABE6 RID: 44006 RVA: 0x002365B5 File Offset: 0x002347B5
			// (set) Token: 0x0600ABE7 RID: 44007 RVA: 0x002365BD File Offset: 0x002347BD
			public string StringName { get; set; }

			// Token: 0x0600ABE8 RID: 44008 RVA: 0x002365C6 File Offset: 0x002347C6
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteNullableString(this.CultureName);
				writer.WriteString(this.StringName);
			}

			// Token: 0x0600ABE9 RID: 44009 RVA: 0x002365E7 File Offset: 0x002347E7
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.CultureName = reader.ReadNullableString();
				this.StringName = reader.ReadString();
			}
		}

		// Token: 0x02001AAC RID: 6828
		public sealed class LibraryFileRequestMessage : RemoteLibraryServiceFactory.LibraryContentRequestMessage
		{
			// Token: 0x17002B5F RID: 11103
			// (get) Token: 0x0600ABEB RID: 44011 RVA: 0x00236608 File Offset: 0x00234808
			// (set) Token: 0x0600ABEC RID: 44012 RVA: 0x00236610 File Offset: 0x00234810
			public string FileName { get; set; }

			// Token: 0x0600ABED RID: 44013 RVA: 0x00236619 File Offset: 0x00234819
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteString(this.FileName);
			}

			// Token: 0x0600ABEE RID: 44014 RVA: 0x0023662E File Offset: 0x0023482E
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.FileName = reader.ReadString();
			}
		}

		// Token: 0x02001AAD RID: 6829
		public sealed class LibraryContentResponseMessage : BufferedMessage
		{
			// Token: 0x17002B60 RID: 11104
			// (get) Token: 0x0600ABF0 RID: 44016 RVA: 0x00236643 File Offset: 0x00234843
			// (set) Token: 0x0600ABF1 RID: 44017 RVA: 0x0023664B File Offset: 0x0023484B
			public byte[] Bytes { get; set; }

			// Token: 0x0600ABF2 RID: 44018 RVA: 0x00236654 File Offset: 0x00234854
			public override void Serialize(BinaryWriter writer)
			{
				if (this.Bytes == null)
				{
					writer.WriteInt32(-1);
					return;
				}
				writer.WriteInt32(this.Bytes.Length);
				writer.Write(this.Bytes);
			}

			// Token: 0x0600ABF3 RID: 44019 RVA: 0x00236680 File Offset: 0x00234880
			public override void Deserialize(BinaryReader reader)
			{
				int num = reader.ReadInt32();
				this.Bytes = ((num < 0) ? null : reader.ReadBytes(num));
			}
		}

		// Token: 0x02001AAE RID: 6830
		public sealed class LibraryTrustLevelRequestMessage : RemoteLibraryServiceFactory.LibraryContentRequestMessage
		{
		}

		// Token: 0x02001AAF RID: 6831
		public sealed class LibraryTrustLevelResponseMessage : BufferedMessage
		{
			// Token: 0x17002B61 RID: 11105
			// (get) Token: 0x0600ABF6 RID: 44022 RVA: 0x002366A8 File Offset: 0x002348A8
			// (set) Token: 0x0600ABF7 RID: 44023 RVA: 0x002366B0 File Offset: 0x002348B0
			public ModuleTrustLevel TrustLevel { get; set; }

			// Token: 0x0600ABF8 RID: 44024 RVA: 0x002366B9 File Offset: 0x002348B9
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32((int)this.TrustLevel);
			}

			// Token: 0x0600ABF9 RID: 44025 RVA: 0x002366C7 File Offset: 0x002348C7
			public override void Deserialize(BinaryReader reader)
			{
				this.TrustLevel = (ModuleTrustLevel)reader.ReadInt32();
			}
		}

		// Token: 0x02001AB0 RID: 6832
		public sealed class GetLibrariesTableRequestMessage : BufferedMessage
		{
			// Token: 0x17002B62 RID: 11106
			// (get) Token: 0x0600ABFB RID: 44027 RVA: 0x002366D5 File Offset: 0x002348D5
			// (set) Token: 0x0600ABFC RID: 44028 RVA: 0x002366DD File Offset: 0x002348DD
			public string Kind { get; set; }

			// Token: 0x17002B63 RID: 11107
			// (get) Token: 0x0600ABFD RID: 44029 RVA: 0x002366E6 File Offset: 0x002348E6
			// (set) Token: 0x0600ABFE RID: 44030 RVA: 0x002366EE File Offset: 0x002348EE
			public string Culture { get; set; }

			// Token: 0x17002B64 RID: 11108
			// (get) Token: 0x0600ABFF RID: 44031 RVA: 0x002366F7 File Offset: 0x002348F7
			// (set) Token: 0x0600AC00 RID: 44032 RVA: 0x002366FF File Offset: 0x002348FF
			public string Scope { get; set; }

			// Token: 0x0600AC01 RID: 44033 RVA: 0x00236708 File Offset: 0x00234908
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Kind);
				writer.WriteNullableString(this.Culture);
				writer.WriteNullableString(this.Scope);
			}

			// Token: 0x0600AC02 RID: 44034 RVA: 0x0023672E File Offset: 0x0023492E
			public override void Deserialize(BinaryReader reader)
			{
				this.Kind = reader.ReadString();
				this.Culture = reader.ReadNullableString();
				this.Scope = reader.ReadNullableString();
			}
		}

		// Token: 0x02001AB1 RID: 6833
		public sealed class SerializedTableResponseMessage : BufferedMessage
		{
			// Token: 0x17002B65 RID: 11109
			// (get) Token: 0x0600AC04 RID: 44036 RVA: 0x00236754 File Offset: 0x00234954
			// (set) Token: 0x0600AC05 RID: 44037 RVA: 0x0023675C File Offset: 0x0023495C
			public ISerializableValue Table { get; set; }

			// Token: 0x0600AC06 RID: 44038 RVA: 0x00236765 File Offset: 0x00234965
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteByteArray(this.Table.GetBytes());
			}

			// Token: 0x0600AC07 RID: 44039 RVA: 0x00236778 File Offset: 0x00234978
			public override void Deserialize(BinaryReader reader)
			{
				this.Table = new SerializableValue(reader.ReadByteArray());
			}
		}
	}
}
