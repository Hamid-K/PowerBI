using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F36 RID: 3894
	internal class AnalysisServicesTracingConnectionFactory : IAnalysisServicesConnectionFactory
	{
		// Token: 0x0600670A RID: 26378 RVA: 0x00162F9F File Offset: 0x0016119F
		public AnalysisServicesTracingConnectionFactory(IEngineHost host, IAnalysisServicesConnectionFactory factory, IResource resource)
		{
			this.tracer = new Tracer(host, "Engine/IO/AnalysisServices/", resource, null, null);
			this.factory = factory;
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x00162FC4 File Offset: 0x001611C4
		public IAnalysisServicesConnection CreateConnection(string connectionString)
		{
			return this.tracer.Trace<IAnalysisServicesConnection>("ConnectionFactory/CreateConnection", (IHostTrace t) => new AnalysisServicesTracingConnectionFactory.AnalysisServicesTracingConnection(this.tracer, this.factory.CreateConnection(connectionString)));
		}

		// Token: 0x040038AF RID: 14511
		private readonly Tracer tracer;

		// Token: 0x040038B0 RID: 14512
		private readonly IAnalysisServicesConnectionFactory factory;

		// Token: 0x02000F37 RID: 3895
		private class AnalysisServicesTracingConnection : IAnalysisServicesConnection, IDisposable
		{
			// Token: 0x0600670C RID: 26380 RVA: 0x00163001 File Offset: 0x00161201
			public AnalysisServicesTracingConnection(Tracer tracer, IAnalysisServicesConnection connection)
			{
				this.tracer = tracer;
				this.connection = connection;
			}

			// Token: 0x17001DD4 RID: 7636
			// (get) Token: 0x0600670D RID: 26381 RVA: 0x00163017 File Offset: 0x00161217
			public string ProviderVersion
			{
				get
				{
					return this.connection.ProviderVersion;
				}
			}

			// Token: 0x17001DD5 RID: 7637
			// (get) Token: 0x0600670E RID: 26382 RVA: 0x00163024 File Offset: 0x00161224
			public string ServerVersion
			{
				get
				{
					return this.connection.ServerVersion;
				}
			}

			// Token: 0x17001DD6 RID: 7638
			// (get) Token: 0x0600670F RID: 26383 RVA: 0x00163031 File Offset: 0x00161231
			public ConnectionState State
			{
				get
				{
					return this.connection.State;
				}
			}

			// Token: 0x06006710 RID: 26384 RVA: 0x0016303E File Offset: 0x0016123E
			public void Open()
			{
				this.tracer.Trace("Connection/Open", delegate(IHostTrace trace)
				{
					this.connection.Open();
					trace.Add("ServerVersion", this.ServerVersion, false);
					this.tracer.LogFeature("ADOMD.NET/" + this.ProviderVersion);
				});
			}

			// Token: 0x06006711 RID: 26385 RVA: 0x0016305C File Offset: 0x0016125C
			public IAnalysisServicesCommand CreateCommand()
			{
				return new AnalysisServicesTracingConnectionFactory.AnalysisServicesTracingCommand(this.tracer, this.connection.CreateCommand());
			}

			// Token: 0x06006712 RID: 26386 RVA: 0x00163074 File Offset: 0x00161274
			public DataSet GetSchemaDataSet(string name, AdomdRestrictionCollection restrictions)
			{
				return this.tracer.Trace<DataSet>("Connection/GetSchemaDataSet", delegate(IHostTrace trace)
				{
					trace.Add("Name", name, true);
					return this.connection.GetSchemaDataSet(name, restrictions);
				});
			}

			// Token: 0x06006713 RID: 26387 RVA: 0x001630B8 File Offset: 0x001612B8
			public void Dispose()
			{
				if (this.connection != null)
				{
					this.tracer.Trace("Connection/Dispose", delegate(IHostTrace trace)
					{
						this.connection.Dispose();
						this.connection = null;
					});
				}
			}

			// Token: 0x040038B1 RID: 14513
			private readonly Tracer tracer;

			// Token: 0x040038B2 RID: 14514
			private IAnalysisServicesConnection connection;
		}

		// Token: 0x02000F39 RID: 3897
		private class AnalysisServicesTracingCommand : IAnalysisServicesCommand, IDisposable
		{
			// Token: 0x06006718 RID: 26392 RVA: 0x0016315C File Offset: 0x0016135C
			public AnalysisServicesTracingCommand(Tracer tracer, IAnalysisServicesCommand command)
			{
				this.tracer = tracer;
				this.command = command;
			}

			// Token: 0x17001DD7 RID: 7639
			// (set) Token: 0x06006719 RID: 26393 RVA: 0x00163172 File Offset: 0x00161372
			public string CommandText
			{
				set
				{
					this.command.CommandText = value;
					this.commandText = value;
				}
			}

			// Token: 0x17001DD8 RID: 7640
			// (set) Token: 0x0600671A RID: 26394 RVA: 0x00163187 File Offset: 0x00161387
			public int CommandTimeout
			{
				set
				{
					this.command.CommandTimeout = value;
				}
			}

			// Token: 0x0600671B RID: 26395 RVA: 0x00163195 File Offset: 0x00161395
			public IDataReader ExecuteReader()
			{
				return this.tracer.Trace<IDataReader>("Command/ExecuteReader", delegate(IHostTrace trace)
				{
					trace.Add("CommandText", this.commandText, true);
					if (this.tracer.VerboseEnabled && this.parameters != null)
					{
						trace.AddArray("Parameters", this.parameters, true);
					}
					return this.command.ExecuteReader();
				});
			}

			// Token: 0x0600671C RID: 26396 RVA: 0x001631B4 File Offset: 0x001613B4
			public void AddParameter(string name, object value)
			{
				this.command.AddParameter(name, value);
				if (this.tracer.VerboseEnabled)
				{
					if (this.parameters == null)
					{
						this.parameters = new List<string>();
					}
					this.parameters.Add(name + ":" + ((value != null) ? value.ToString() : null));
				}
			}

			// Token: 0x0600671D RID: 26397 RVA: 0x00163210 File Offset: 0x00161410
			public void Cancel()
			{
				this.tracer.Trace("Command/Cancel", delegate(IHostTrace trace)
				{
					this.command.Cancel();
				});
			}

			// Token: 0x0600671E RID: 26398 RVA: 0x0016322E File Offset: 0x0016142E
			public void Dispose()
			{
				if (this.command != null)
				{
					this.tracer.Trace("Command/Dispose", delegate(IHostTrace trace)
					{
						this.command.Dispose();
						this.command = null;
					});
				}
			}

			// Token: 0x040038B6 RID: 14518
			private readonly Tracer tracer;

			// Token: 0x040038B7 RID: 14519
			private IAnalysisServicesCommand command;

			// Token: 0x040038B8 RID: 14520
			private string commandText;

			// Token: 0x040038B9 RID: 14521
			private List<string> parameters;
		}
	}
}
