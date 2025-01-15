using System;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F0E RID: 3854
	internal sealed class AnalysisServicesImpersonatingConnectionFactory : IAnalysisServicesConnectionFactory
	{
		// Token: 0x06006613 RID: 26131 RVA: 0x0015F78D File Offset: 0x0015D98D
		public AnalysisServicesImpersonatingConnectionFactory(IEngineHost host, IAnalysisServicesConnectionFactory factory, Func<IDisposable> impersonate, string serverName, IResource resource)
		{
			this.host = host;
			this.factory = factory;
			this.impersonate = impersonate;
			this.serverName = serverName;
			this.resource = resource;
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x0015F7BC File Offset: 0x0015D9BC
		public IAnalysisServicesConnection CreateConnection(string connectionString)
		{
			return this.ImpersonateAndHandleExceptions<AnalysisServicesImpersonatingConnectionFactory.ImpersonatingConnection>(delegate
			{
				AnalysisServicesImpersonatingConnectionFactory.ImpersonatingConnection impersonatingConnection;
				try
				{
					impersonatingConnection = new AnalysisServicesImpersonatingConnectionFactory.ImpersonatingConnection(this, this.factory.CreateConnection(connectionString));
				}
				catch (ArgumentException ex)
				{
					throw AnalysisServicesExceptions.NewInvalidServerException(TextValue.New(this.serverName), ex);
				}
				catch (FileNotFoundException ex2)
				{
					if (ex2.FileName.Contains("Microsoft.AnalysisServices.AdomdClient"))
					{
						throw AnalysisServicesExceptions.NewProviderMissingException(this.host, ex2, this.resource);
					}
					throw;
				}
				return impersonatingConnection;
			});
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x0015F7F0 File Offset: 0x0015D9F0
		private T ImpersonateAndHandleExceptions<T>(Func<T> func)
		{
			T t;
			try
			{
				using (this.impersonate())
				{
					t = func();
				}
			}
			catch (AnalysisServicesException ex)
			{
				AdomdConnectionException ex2 = ex.InnerException as AdomdConnectionException;
				if (ex2 != null && ex2.ExceptionCause == ConnectionExceptionCause.AuthenticationFailed)
				{
					throw DataSourceException.NewAccessAuthorizationError(this.host, this.resource, null, null, ex);
				}
				if (ex.InnerException != null && ex.InnerException.InnerException is WebException)
				{
					HttpServices.ThrowIfAuthorizationError(this.host, (WebException)ex.InnerException.InnerException, this.resource);
				}
				throw AnalysisServicesExceptions.NewDataSourceError(this.host, ex, this.resource);
			}
			catch (XmlException ex3)
			{
				throw AnalysisServicesExceptions.NewInvalidResponseFromService(this.host, ex3, this.resource);
			}
			return t;
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x0015F8DC File Offset: 0x0015DADC
		private void Wrap(Action action)
		{
			this.ImpersonateAndHandleExceptions<int>(delegate
			{
				action();
				return 0;
			});
		}

		// Token: 0x0400380E RID: 14350
		private const string ProviderAssemblyName = "Microsoft.AnalysisServices.AdomdClient";

		// Token: 0x0400380F RID: 14351
		private static readonly Version sqlServer2008R2Rtm = new Version(10, 50, 1600, 1);

		// Token: 0x04003810 RID: 14352
		private readonly IEngineHost host;

		// Token: 0x04003811 RID: 14353
		private readonly IAnalysisServicesConnectionFactory factory;

		// Token: 0x04003812 RID: 14354
		private readonly Func<IDisposable> impersonate;

		// Token: 0x04003813 RID: 14355
		private readonly string serverName;

		// Token: 0x04003814 RID: 14356
		private readonly IResource resource;

		// Token: 0x02000F0F RID: 3855
		private class ImpersonatingConnection : IAnalysisServicesConnection, IDisposable
		{
			// Token: 0x06006618 RID: 26136 RVA: 0x0015F91F File Offset: 0x0015DB1F
			public ImpersonatingConnection(AnalysisServicesImpersonatingConnectionFactory factory, IAnalysisServicesConnection connection)
			{
				this.connection = connection;
				this.factory = factory;
			}

			// Token: 0x17001DA0 RID: 7584
			// (get) Token: 0x06006619 RID: 26137 RVA: 0x0015F935 File Offset: 0x0015DB35
			public string ProviderVersion
			{
				get
				{
					return this.factory.ImpersonateAndHandleExceptions<string>(() => this.connection.ProviderVersion);
				}
			}

			// Token: 0x17001DA1 RID: 7585
			// (get) Token: 0x0600661A RID: 26138 RVA: 0x0015F94E File Offset: 0x0015DB4E
			public string ServerVersion
			{
				get
				{
					return this.factory.ImpersonateAndHandleExceptions<string>(() => this.connection.ServerVersion);
				}
			}

			// Token: 0x17001DA2 RID: 7586
			// (get) Token: 0x0600661B RID: 26139 RVA: 0x0015F967 File Offset: 0x0015DB67
			public ConnectionState State
			{
				get
				{
					return this.factory.ImpersonateAndHandleExceptions<ConnectionState>(() => this.connection.State);
				}
			}

			// Token: 0x0600661C RID: 26140 RVA: 0x0015F980 File Offset: 0x0015DB80
			public void Open()
			{
				this.factory.Wrap(delegate
				{
					this.connection.Open();
					this.EnsureSupportedVersion(this.connection);
				});
			}

			// Token: 0x0600661D RID: 26141 RVA: 0x0015F999 File Offset: 0x0015DB99
			public IAnalysisServicesCommand CreateCommand()
			{
				return this.factory.ImpersonateAndHandleExceptions<AnalysisServicesImpersonatingConnectionFactory.ImpersonatingCommand>(() => new AnalysisServicesImpersonatingConnectionFactory.ImpersonatingCommand(this.factory, this.connection.CreateCommand()));
			}

			// Token: 0x0600661E RID: 26142 RVA: 0x0015F9B4 File Offset: 0x0015DBB4
			public DataSet GetSchemaDataSet(string name, AdomdRestrictionCollection restrictions)
			{
				return this.factory.ImpersonateAndHandleExceptions<DataSet>(() => this.connection.GetSchemaDataSet(name, restrictions));
			}

			// Token: 0x0600661F RID: 26143 RVA: 0x0015F9F3 File Offset: 0x0015DBF3
			public void Dispose()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "AnalysisServicesConnectionFactoryWrapper/ImpersonatingConnection/Dispose", delegate
				{
					using (this.factory.impersonate())
					{
						if (this.connection != null)
						{
							this.connection.Dispose();
						}
					}
				});
				this.connection = null;
			}

			// Token: 0x06006620 RID: 26144 RVA: 0x0015FA1D File Offset: 0x0015DC1D
			private void EnsureSupportedVersion(IAnalysisServicesConnection connection)
			{
				if (new Version(this.ServerVersion) < AnalysisServicesImpersonatingConnectionFactory.sqlServer2008R2Rtm)
				{
					throw AnalysisServicesExceptions.NewServiceVersionNotSupported(this.factory.host, this.factory.resource);
				}
			}

			// Token: 0x04003815 RID: 14357
			private IAnalysisServicesConnection connection;

			// Token: 0x04003816 RID: 14358
			private readonly AnalysisServicesImpersonatingConnectionFactory factory;
		}

		// Token: 0x02000F11 RID: 3857
		private class ImpersonatingCommand : IAnalysisServicesCommand, IDisposable
		{
			// Token: 0x06006629 RID: 26153 RVA: 0x0015FB1A File Offset: 0x0015DD1A
			public ImpersonatingCommand(AnalysisServicesImpersonatingConnectionFactory factory, IAnalysisServicesCommand command)
			{
				this.command = command;
				this.factory = factory;
			}

			// Token: 0x17001DA3 RID: 7587
			// (set) Token: 0x0600662A RID: 26154 RVA: 0x0015FB30 File Offset: 0x0015DD30
			public string CommandText
			{
				set
				{
					this.factory.Wrap(delegate
					{
						this.command.CommandText = value;
					});
				}
			}

			// Token: 0x17001DA4 RID: 7588
			// (set) Token: 0x0600662B RID: 26155 RVA: 0x0015FB68 File Offset: 0x0015DD68
			public int CommandTimeout
			{
				set
				{
					this.factory.Wrap(delegate
					{
						this.command.CommandTimeout = value;
					});
				}
			}

			// Token: 0x0600662C RID: 26156 RVA: 0x0015FBA0 File Offset: 0x0015DDA0
			public IDataReader ExecuteReader()
			{
				return this.factory.ImpersonateAndHandleExceptions<AnalysisServicesImpersonatingConnectionFactory.ImpersonatingDataReader>(() => new AnalysisServicesImpersonatingConnectionFactory.ImpersonatingDataReader(this.factory, this.command.ExecuteReader().WithTableSchema()));
			}

			// Token: 0x0600662D RID: 26157 RVA: 0x0015FBBC File Offset: 0x0015DDBC
			public void AddParameter(string name, object value)
			{
				this.factory.Wrap(delegate
				{
					this.command.AddParameter(name, value);
				});
			}

			// Token: 0x0600662E RID: 26158 RVA: 0x0015FBFB File Offset: 0x0015DDFB
			public void Cancel()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "AnalysisServicesConnectionFactoryWrapper/ImpersonatingCommand/Cancel", delegate
				{
					using (this.factory.impersonate())
					{
						this.command.Cancel();
					}
				});
			}

			// Token: 0x0600662F RID: 26159 RVA: 0x0015FC1E File Offset: 0x0015DE1E
			public void Dispose()
			{
				this.Cancel();
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "AnalysisServicesConnectionFactoryWrapper/ImpersonatingCommand/Dispose", delegate
				{
					using (this.factory.impersonate())
					{
						this.command.Dispose();
					}
				});
				this.command = null;
			}

			// Token: 0x0400381A RID: 14362
			private IAnalysisServicesCommand command;

			// Token: 0x0400381B RID: 14363
			private readonly AnalysisServicesImpersonatingConnectionFactory factory;
		}

		// Token: 0x02000F15 RID: 3861
		private class ImpersonatingDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x06006639 RID: 26169 RVA: 0x0015FD4A File Offset: 0x0015DF4A
			public ImpersonatingDataReader(AnalysisServicesImpersonatingConnectionFactory factory, IDataReaderWithTableSchema reader)
				: base(reader)
			{
				this.factory = factory;
			}

			// Token: 0x0600663A RID: 26170 RVA: 0x0015FD5A File Offset: 0x0015DF5A
			public override bool Read()
			{
				return this.factory.ImpersonateAndHandleExceptions<bool>(() => base.Read());
			}

			// Token: 0x0600663B RID: 26171 RVA: 0x0015FD73 File Offset: 0x0015DF73
			public override void Dispose()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "AnalysisServicesConnectionFactoryWrapper/ImpersonatingDataReader/Dispose", delegate
				{
					base.Dispose();
				});
			}

			// Token: 0x0600663C RID: 26172 RVA: 0x0015FD96 File Offset: 0x0015DF96
			public override void Close()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "AnalysisServicesConnectionFactoryWrapper/ImpersonatingDataReader/Close", delegate
				{
					base.Close();
				});
			}

			// Token: 0x04003823 RID: 14371
			private readonly AnalysisServicesImpersonatingConnectionFactory factory;
		}
	}
}
