using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200047B RID: 1147
	internal class SapHanaSessionService : OdbcDelegatingService
	{
		// Token: 0x0600261F RID: 9759 RVA: 0x0006E51C File Offset: 0x0006C71C
		public SapHanaSessionService(IOdbcService service, string impersonatedWindowsUsername, SapHanaAdditionalTracesProvider additionalTracesProvider, bool tracerEnabled)
			: base(service)
		{
			this.impersonatedWindowsUsername = impersonatedWindowsUsername;
			this.additionalTracesProvider = additionalTracesProvider;
			this.tracerEnabled = tracerEnabled;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0006E53B File Offset: 0x0006C73B
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new SapHanaSessionService.SapHanaSetStatementConnection(base.CreateConnection(args), this.impersonatedWindowsUsername, this.additionalTracesProvider, this.tracerEnabled);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0006E55C File Offset: 0x0006C75C
		internal static string RemoveDomain(string username)
		{
			int num = username.IndexOf('\\');
			if (num > 0)
			{
				username = username.Substring(num + 1);
			}
			num = username.IndexOf('@');
			if (num > 0)
			{
				username = username.Substring(0, num);
			}
			return username;
		}

		// Token: 0x04000FEF RID: 4079
		private readonly string impersonatedWindowsUsername;

		// Token: 0x04000FF0 RID: 4080
		private readonly SapHanaAdditionalTracesProvider additionalTracesProvider;

		// Token: 0x04000FF1 RID: 4081
		private readonly bool tracerEnabled;

		// Token: 0x0200047C RID: 1148
		private class SapHanaSetStatementConnection : OdbcDelegatingConnection
		{
			// Token: 0x06002622 RID: 9762 RVA: 0x0006E59C File Offset: 0x0006C79C
			public SapHanaSetStatementConnection(IOdbcConnection connection, string impersonatedWindowsUsername, SapHanaAdditionalTracesProvider additionalTracesProvider, bool tracerEnabled)
				: base(connection)
			{
				this.setQueries = new List<string> { "SET SESSION 'APPLICATION' = 'Mashup Engine'" };
				if (!string.IsNullOrEmpty(impersonatedWindowsUsername))
				{
					this.setQueries.Add(string.Format(CultureInfo.InvariantCulture, "SET SESSION 'APPLICATIONUSER' = '{0}'", SapHanaSessionService.RemoveDomain(impersonatedWindowsUsername)));
				}
				this.additionalTracesProvider = additionalTracesProvider;
				this.tracerEnabled = tracerEnabled;
			}

			// Token: 0x06002623 RID: 9763 RVA: 0x0006E600 File Offset: 0x0006C800
			public override void Open()
			{
				base.Open();
				using (List<string>.Enumerator enumerator = this.setQueries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string query = enumerator.Current;
						SapHanaSessionService.SapHanaSetStatementConnection.IgnoreOdbcException(delegate
						{
							this.<>n__0(query, EmptyArray<OdbcParameter>.Instance, RowRange.All, OdbcStatementRegistrar.DummyRegistrar);
						});
					}
				}
				if (this.tracerEnabled)
				{
					SapHanaSessionService.SapHanaSetStatementConnection.IgnoreOdbcException(delegate
					{
						using (IPageReader pageReader = this.ExecuteDirect("SELECT connection_id FROM m_connections WHERE OWN = 'TRUE'", EmptyArray<OdbcParameter>.Instance, RowRange.All, OdbcStatementRegistrar.DummyRegistrar))
						{
							using (IDataReader dataReader = new PageReaderDataReader(pageReader, new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties)))
							{
								if (dataReader.Read())
								{
									this.additionalTracesProvider.SessionId = new int?(dataReader.GetInt32(0));
								}
							}
						}
					});
				}
			}

			// Token: 0x06002624 RID: 9764 RVA: 0x0006E68C File Offset: 0x0006C88C
			private static void IgnoreOdbcException(Action action)
			{
				try
				{
					action();
				}
				catch (OdbcException)
				{
				}
			}

			// Token: 0x04000FF2 RID: 4082
			private const string setApplicationNameQuery = "SET SESSION 'APPLICATION' = 'Mashup Engine'";

			// Token: 0x04000FF3 RID: 4083
			private const string setApplicationUserQuery = "SET SESSION 'APPLICATIONUSER' = '{0}'";

			// Token: 0x04000FF4 RID: 4084
			private const string sessionIdQuery = "SELECT connection_id FROM m_connections WHERE OWN = 'TRUE'";

			// Token: 0x04000FF5 RID: 4085
			private readonly List<string> setQueries;

			// Token: 0x04000FF6 RID: 4086
			private readonly SapHanaAdditionalTracesProvider additionalTracesProvider;

			// Token: 0x04000FF7 RID: 4087
			private readonly bool tracerEnabled;
		}
	}
}
