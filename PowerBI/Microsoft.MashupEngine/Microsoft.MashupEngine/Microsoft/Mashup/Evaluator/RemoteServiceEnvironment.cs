using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D3C RID: 7484
	public static class RemoteServiceEnvironment
	{
		// Token: 0x0600BA5C RID: 47708 RVA: 0x0025BDBC File Offset: 0x00259FBC
		public static IRemoteServiceStub[] CreateServiceStubs(IMessenger messenger, IEngineHost engineHost)
		{
			IRemoteServiceStub[] array4;
			using (IMessageChannel messageChannel = messenger.CreateChannel())
			{
				using (EvaluatorTracing.CreateTrace("RemoteServiceEnvironment/CreateServiceStubs", engineHost, TraceEventType.Information, null))
				{
					object obj = RemoteServiceEnvironment.syncRoot;
					IRemoteServiceFactory[] factories;
					Guid guid;
					lock (obj)
					{
						if (RemoteServiceEnvironment.remoteFactories != null)
						{
							factories = RemoteServiceEnvironment.remoteFactories;
							guid = RemoteServiceEnvironment.version;
						}
						else
						{
							factories = RemoteServiceFactories.GetFactories(out guid);
						}
					}
					byte[][] array = new byte[factories.Length][];
					IRemoteServiceStub[] array2 = new IRemoteServiceStub[factories.Length];
					for (int i = 0; i < factories.Length; i++)
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
							{
								array2[i] = factories[i].CreateStub(engineHost, messenger, binaryWriter);
								binaryWriter.Flush();
								array[i] = memoryStream.ToArray();
							}
						}
					}
					messageChannel.Post(new RemoteServiceEnvironment.InitializeMessage
					{
						ProxyInitArgs = array,
						Version = guid
					});
					if (messageChannel.WaitFor<RemoteServiceEnvironment.InitializeResponseMessage>().NeedFactories)
					{
						RemoteServiceEnvironment.RemoteServiceFactoryInfo[] array3 = new RemoteServiceEnvironment.RemoteServiceFactoryInfo[factories.Length];
						for (int j = 0; j < array3.Length; j++)
						{
							Type type = factories[j].GetType();
							array3[j] = new RemoteServiceEnvironment.RemoteServiceFactoryInfo
							{
								AssemblyName = type.Assembly.FullName,
								TypeName = type.FullName
							};
						}
						messageChannel.Post(new RemoteServiceEnvironment.RemoteServiceFactoriesMessage
						{
							Factories = array3
						});
					}
					array4 = array2;
				}
			}
			return array4;
		}

		// Token: 0x0600BA5D RID: 47709 RVA: 0x0025BFB4 File Offset: 0x0025A1B4
		public static IEngineHost CreateServiceProxies(IMessenger messenger, out IRemoteServiceProxy[] services)
		{
			RemoteServiceEnvironment.InitializeMessage initMessage = null;
			messenger.WaitFor(delegate(IMessageChannel channel, RemoteServiceEnvironment.InitializeMessage message)
			{
				initMessage = message;
				object obj2 = RemoteServiceEnvironment.syncRoot;
				bool flag3;
				lock (obj2)
				{
					flag3 = RemoteServiceEnvironment.remoteFactories == null || RemoteServiceEnvironment.version != initMessage.Version;
				}
				channel.Post(new RemoteServiceEnvironment.InitializeResponseMessage
				{
					NeedFactories = flag3
				});
				if (flag3)
				{
					RemoteServiceEnvironment.RemoteServiceFactoryInfo[] factories = channel.WaitFor<RemoteServiceEnvironment.RemoteServiceFactoriesMessage>().Factories;
					obj2 = RemoteServiceEnvironment.syncRoot;
					lock (obj2)
					{
						RemoteServiceEnvironment.remoteFactories = new IRemoteServiceFactory[factories.Length];
						RemoteServiceEnvironment.version = initMessage.Version;
						for (int j = 0; j < RemoteServiceEnvironment.remoteFactories.Length; j++)
						{
							Type type = Assembly.Load(new AssemblyName(factories[j].AssemblyName)).GetType(factories[j].TypeName);
							RemoteServiceEnvironment.remoteFactories[j] = (IRemoteServiceFactory)Activator.CreateInstance(type);
						}
					}
				}
			});
			MutableEngineHost mutableEngineHost = new MutableEngineHost();
			object obj = RemoteServiceEnvironment.syncRoot;
			IRemoteServiceFactory[] array;
			lock (obj)
			{
				array = RemoteServiceEnvironment.remoteFactories;
			}
			services = new IRemoteServiceProxy[array.Length];
			for (int i = 0; i < services.Length; i++)
			{
				using (MemoryStream memoryStream = new MemoryStream(initMessage.ProxyInitArgs[i]))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						services[i] = array[i].CreateProxy(mutableEngineHost, messenger, binaryReader);
						if (services[i] != EmptyProxy.Instance)
						{
							mutableEngineHost.Add(services[i]);
						}
					}
				}
			}
			return mutableEngineHost;
		}

		// Token: 0x0600BA5E RID: 47710 RVA: 0x0025C0B4 File Offset: 0x0025A2B4
		public static void DisposeServiceStubs(IMessenger messenger, IRemoteServiceStub[] services, IEngineHost engineHost)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteServiceEnvironment/DisposeServiceStubs", engineHost, TraceEventType.Information, null))
			{
				if (messenger != null)
				{
					SafeExceptions.IgnoreSafeExceptions(engineHost, hostTrace, delegate
					{
						messenger.WaitFor<RemoteServiceEnvironment.CleanupMessage>();
					});
				}
				if (services != null)
				{
					int j;
					int i;
					for (i = services.Length - 1; i >= 0; i = j - 1)
					{
						SafeExceptions.IgnoreSafeExceptions(engineHost, hostTrace, delegate
						{
							services[i].Dispose();
						});
						j = i;
					}
				}
			}
		}

		// Token: 0x0600BA5F RID: 47711 RVA: 0x0025C174 File Offset: 0x0025A374
		public static void DisposeServiceProxies(IMessenger messenger, IRemoteServiceProxy[] services)
		{
			if (services != null)
			{
				for (int i = services.Length - 1; i >= 0; i--)
				{
					IRemoteServiceProxy remoteServiceProxy = services[i];
					if (remoteServiceProxy != null)
					{
						remoteServiceProxy.Dispose();
					}
					services[i] = null;
				}
			}
			if (messenger != null)
			{
				using (IMessageChannel messageChannel = messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteServiceEnvironment.CleanupMessage());
				}
			}
		}

		// Token: 0x04005EDC RID: 24284
		private static readonly object syncRoot = new object();

		// Token: 0x04005EDD RID: 24285
		private static IRemoteServiceFactory[] remoteFactories;

		// Token: 0x04005EDE RID: 24286
		private static Guid version;

		// Token: 0x02001D3D RID: 7485
		private class RemoteServiceFactoryInfo
		{
			// Token: 0x17002E10 RID: 11792
			// (get) Token: 0x0600BA61 RID: 47713 RVA: 0x0025C1E4 File Offset: 0x0025A3E4
			// (set) Token: 0x0600BA62 RID: 47714 RVA: 0x0025C1EC File Offset: 0x0025A3EC
			public string AssemblyName { get; set; }

			// Token: 0x17002E11 RID: 11793
			// (get) Token: 0x0600BA63 RID: 47715 RVA: 0x0025C1F5 File Offset: 0x0025A3F5
			// (set) Token: 0x0600BA64 RID: 47716 RVA: 0x0025C1FD File Offset: 0x0025A3FD
			public string TypeName { get; set; }
		}

		// Token: 0x02001D3E RID: 7486
		private sealed class RemoteServiceFactoriesMessage : UnbufferedMessage
		{
			// Token: 0x17002E12 RID: 11794
			// (get) Token: 0x0600BA66 RID: 47718 RVA: 0x0025C206 File Offset: 0x0025A406
			// (set) Token: 0x0600BA67 RID: 47719 RVA: 0x0025C20E File Offset: 0x0025A40E
			public RemoteServiceEnvironment.RemoteServiceFactoryInfo[] Factories { get; set; }

			// Token: 0x0600BA68 RID: 47720 RVA: 0x0025C217 File Offset: 0x0025A417
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.Factories, delegate(BinaryWriter w, RemoteServiceEnvironment.RemoteServiceFactoryInfo info)
				{
					w.WriteString(info.AssemblyName);
					w.WriteString(info.TypeName);
				});
			}

			// Token: 0x0600BA69 RID: 47721 RVA: 0x0025C244 File Offset: 0x0025A444
			public override void Deserialize(BinaryReader reader)
			{
				this.Factories = reader.ReadArray((BinaryReader r) => new RemoteServiceEnvironment.RemoteServiceFactoryInfo
				{
					AssemblyName = r.ReadString(),
					TypeName = r.ReadString()
				});
			}
		}

		// Token: 0x02001D40 RID: 7488
		private sealed class InitializeMessage : UnbufferedMessage
		{
			// Token: 0x17002E13 RID: 11795
			// (get) Token: 0x0600BA6F RID: 47727 RVA: 0x0025C2B6 File Offset: 0x0025A4B6
			// (set) Token: 0x0600BA70 RID: 47728 RVA: 0x0025C2BE File Offset: 0x0025A4BE
			public byte[][] ProxyInitArgs { get; set; }

			// Token: 0x17002E14 RID: 11796
			// (get) Token: 0x0600BA71 RID: 47729 RVA: 0x0025C2C7 File Offset: 0x0025A4C7
			// (set) Token: 0x0600BA72 RID: 47730 RVA: 0x0025C2CF File Offset: 0x0025A4CF
			public Guid Version { get; set; }

			// Token: 0x0600BA73 RID: 47731 RVA: 0x0025C2D8 File Offset: 0x0025A4D8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.ProxyInitArgs, delegate(BinaryWriter w, byte[] b)
				{
					w.WriteByteArray(b);
				});
				writer.WriteGuid(this.Version);
			}

			// Token: 0x0600BA74 RID: 47732 RVA: 0x0025C311 File Offset: 0x0025A511
			public override void Deserialize(BinaryReader reader)
			{
				this.ProxyInitArgs = reader.ReadArray((BinaryReader r) => r.ReadByteArray());
				this.Version = reader.ReadGuid();
			}
		}

		// Token: 0x02001D42 RID: 7490
		private sealed class InitializeResponseMessage : UnbufferedMessage
		{
			// Token: 0x17002E15 RID: 11797
			// (get) Token: 0x0600BA7A RID: 47738 RVA: 0x0025C35F File Offset: 0x0025A55F
			// (set) Token: 0x0600BA7B RID: 47739 RVA: 0x0025C367 File Offset: 0x0025A567
			public bool NeedFactories { get; set; }

			// Token: 0x0600BA7C RID: 47740 RVA: 0x0025C370 File Offset: 0x0025A570
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.NeedFactories);
			}

			// Token: 0x0600BA7D RID: 47741 RVA: 0x0025C37E File Offset: 0x0025A57E
			public override void Deserialize(BinaryReader reader)
			{
				this.NeedFactories = reader.ReadBool();
			}
		}

		// Token: 0x02001D43 RID: 7491
		private sealed class CleanupMessage : BufferedMessage
		{
			// Token: 0x0600BA7F RID: 47743 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600BA80 RID: 47744 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}
	}
}
