using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AF6 RID: 6902
	internal class RemoteQueryPermissionServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AD28 RID: 44328 RVA: 0x002392D5 File Offset: 0x002374D5
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteQueryPermissionServiceFactory.Stub(engineHost.QueryService<IQueryPermissionService>(), messenger);
		}

		// Token: 0x0600AD29 RID: 44329 RVA: 0x002392E3 File Offset: 0x002374E3
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteQueryPermissionServiceFactory.Proxy(messenger);
		}

		// Token: 0x02001AF7 RID: 6903
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable, IQueryPermissionService
		{
			// Token: 0x0600AD2B RID: 44331 RVA: 0x002392EB File Offset: 0x002374EB
			public Proxy(IMessenger messenger)
			{
				this.messenger = messenger;
			}

			// Token: 0x0600AD2C RID: 44332 RVA: 0x002392FC File Offset: 0x002374FC
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IQueryPermissionService))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x0600AD2D RID: 44333 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0600AD2E RID: 44334 RVA: 0x00239334 File Offset: 0x00237534
			public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
			{
				bool permitted;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemoteQueryPermissionServiceFactory.QueryPermissionRequestMessage
					{
						Resource = resource,
						Type = type,
						Query = query,
						ParameterCount = parameterCount,
						ParameterNames = parameterNames
					});
					permitted = messageChannel.WaitFor<RemoteQueryPermissionServiceFactory.QueryPermissionResponseMessage>().Permitted;
				}
				return permitted;
			}

			// Token: 0x04005975 RID: 22901
			private readonly IMessenger messenger;
		}

		// Token: 0x02001AF8 RID: 6904
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AD2F RID: 44335 RVA: 0x002393A8 File Offset: 0x002375A8
			public Stub(IQueryPermissionService queryPermissionService, IMessenger messenger)
			{
				this.queryPermissionService = queryPermissionService;
				this.messenger = messenger;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteQueryPermissionServiceFactory.QueryPermissionRequestMessage>(this.OnQueryPermissionRequest));
			}

			// Token: 0x0600AD30 RID: 44336 RVA: 0x002393D8 File Offset: 0x002375D8
			private void OnQueryPermissionRequest(IMessageChannel channel, RemoteQueryPermissionServiceFactory.QueryPermissionRequestMessage message)
			{
				bool flag = this.queryPermissionService.IsQueryExecutionPermitted(message.Resource, message.Type, message.Query, message.ParameterCount, message.ParameterNames);
				channel.Post(new RemoteQueryPermissionServiceFactory.QueryPermissionResponseMessage
				{
					Permitted = flag
				});
			}

			// Token: 0x0600AD31 RID: 44337 RVA: 0x00239421 File Offset: 0x00237621
			public void Dispose()
			{
				this.messenger.RemoveHandler<RemoteQueryPermissionServiceFactory.QueryPermissionRequestMessage>();
				this.messenger = null;
				this.queryPermissionService = null;
			}

			// Token: 0x04005976 RID: 22902
			private IQueryPermissionService queryPermissionService;

			// Token: 0x04005977 RID: 22903
			private IMessenger messenger;
		}

		// Token: 0x02001AF9 RID: 6905
		public sealed class QueryPermissionRequestMessage : BufferedMessage
		{
			// Token: 0x17002B8A RID: 11146
			// (get) Token: 0x0600AD32 RID: 44338 RVA: 0x0023943C File Offset: 0x0023763C
			// (set) Token: 0x0600AD33 RID: 44339 RVA: 0x00239444 File Offset: 0x00237644
			public IResource Resource { get; set; }

			// Token: 0x17002B8B RID: 11147
			// (get) Token: 0x0600AD34 RID: 44340 RVA: 0x0023944D File Offset: 0x0023764D
			// (set) Token: 0x0600AD35 RID: 44341 RVA: 0x00239455 File Offset: 0x00237655
			public QueryPermissionChallengeType Type { get; set; }

			// Token: 0x17002B8C RID: 11148
			// (get) Token: 0x0600AD36 RID: 44342 RVA: 0x0023945E File Offset: 0x0023765E
			// (set) Token: 0x0600AD37 RID: 44343 RVA: 0x00239466 File Offset: 0x00237666
			public string Query { get; set; }

			// Token: 0x17002B8D RID: 11149
			// (get) Token: 0x0600AD38 RID: 44344 RVA: 0x0023946F File Offset: 0x0023766F
			// (set) Token: 0x0600AD39 RID: 44345 RVA: 0x00239477 File Offset: 0x00237677
			public int ParameterCount { get; set; }

			// Token: 0x17002B8E RID: 11150
			// (get) Token: 0x0600AD3A RID: 44346 RVA: 0x00239480 File Offset: 0x00237680
			// (set) Token: 0x0600AD3B RID: 44347 RVA: 0x00239488 File Offset: 0x00237688
			public string[] ParameterNames { get; set; }

			// Token: 0x0600AD3C RID: 44348 RVA: 0x00239494 File Offset: 0x00237694
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteIResource(this.Resource);
				writer.WriteInt32((int)this.Type);
				writer.WriteNullableString(this.Query);
				writer.WriteInt32(this.ParameterCount);
				writer.WriteNullable(this.ParameterNames, delegate(BinaryWriter w, string[] names)
				{
					w.WriteArray(this.ParameterNames, delegate(BinaryWriter itemWriter, string name)
					{
						itemWriter.WriteString(name);
					});
				});
			}

			// Token: 0x0600AD3D RID: 44349 RVA: 0x002394EC File Offset: 0x002376EC
			public override void Deserialize(BinaryReader reader)
			{
				this.Resource = reader.ReadIResource();
				this.Type = (QueryPermissionChallengeType)reader.ReadInt32();
				this.Query = reader.ReadNullableString();
				this.ParameterCount = reader.ReadInt32();
				this.ParameterNames = reader.ReadNullable((BinaryReader r) => r.ReadArray((BinaryReader itemReader) => itemReader.ReadString()));
			}
		}

		// Token: 0x02001AFB RID: 6907
		public sealed class QueryPermissionResponseMessage : BufferedMessage
		{
			// Token: 0x17002B8F RID: 11151
			// (get) Token: 0x0600AD45 RID: 44357 RVA: 0x002395B4 File Offset: 0x002377B4
			// (set) Token: 0x0600AD46 RID: 44358 RVA: 0x002395BC File Offset: 0x002377BC
			public bool Permitted { get; set; }

			// Token: 0x0600AD47 RID: 44359 RVA: 0x002395C5 File Offset: 0x002377C5
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Permitted);
			}

			// Token: 0x0600AD48 RID: 44360 RVA: 0x002395D3 File Offset: 0x002377D3
			public override void Deserialize(BinaryReader reader)
			{
				this.Permitted = reader.ReadBool();
			}
		}
	}
}
