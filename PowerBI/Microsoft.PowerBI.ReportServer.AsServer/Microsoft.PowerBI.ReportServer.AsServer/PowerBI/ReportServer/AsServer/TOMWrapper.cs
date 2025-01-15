using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.BIServer.HostingEnvironment.Contracts;
using Microsoft.PowerBI.ReportServer.AsServer.Exceptions;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000013 RID: 19
	internal class TOMWrapper : IDisposable
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003C0C File Offset: 0x00001E0C
		public TOMWrapper(Server server)
		{
			this._asServer = server;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003C1B File Offset: 0x00001E1B
		public virtual DatabaseCollection GetDatabases()
		{
			return this._asServer.Databases;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C28 File Offset: 0x00001E28
		public virtual bool GetConnected()
		{
			return this._asServer.Connected;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C35 File Offset: 0x00001E35
		public virtual void Connect(string connectionString)
		{
			this._asServer.Connect(connectionString);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C43 File Offset: 0x00001E43
		public virtual void ImageLoad(string databaseName, string databaseId, Stream sourceDbStream)
		{
			this._asServer.ImageLoad(databaseName, databaseId, sourceDbStream);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C53 File Offset: 0x00001E53
		public virtual void ImageSave(string databaseId, Stream targetDbStream)
		{
			this._asServer.ImageSave(databaseId, targetDbStream);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003C64 File Offset: 0x00001E64
		public virtual void DeleteDatabase(string databaseId)
		{
			Database database = this._asServer.Databases[databaseId];
			if (database != null)
			{
				database.Drop();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C8C File Offset: 0x00001E8C
		public virtual void AddRole(string roleName, string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			if (databaseOrThrow.Model.Roles.Find(roleName) == null)
			{
				ModelRole modelRole = new ModelRole
				{
					Name = roleName,
					ModelPermission = ModelPermission.Read
				};
				databaseOrThrow.Model.Roles.Add(modelRole);
				databaseOrThrow.Model.SaveChanges();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public virtual void RemoveRoleIfExists(string roleName, string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			if (databaseOrThrow.Model.Roles.Find(roleName) != null)
			{
				databaseOrThrow.Model.Roles.Remove(roleName);
				databaseOrThrow.Model.SaveChanges();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003D30 File Offset: 0x00001F30
		public virtual void AddUserToRole(string roleName, string user, string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			ModelRole modelRole = databaseOrThrow.Model.Roles.Find(roleName);
			if (modelRole == null)
			{
				throw new InvalidOperationException(string.Format("Cannot add '{0}' to '{1}'. Role not found in '{2}'.", user, roleName, databaseName));
			}
			if (modelRole.Members.Find(user) == null)
			{
				modelRole.Members.Add(new WindowsModelRoleMember
				{
					MemberName = user
				});
				databaseOrThrow.Model.SaveChanges();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public virtual bool UserExistInRole(string roleName, string userName, string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			if (databaseOrThrow.Model == null || databaseOrThrow.Model.Roles == null)
			{
				return false;
			}
			ModelRole modelRole = databaseOrThrow.Model.Roles.Find(roleName);
			return modelRole != null && modelRole.Members != null && modelRole.Members.Find(userName) != null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003DFA File Offset: 0x00001FFA
		public virtual IEnumerable<ProviderDataSource> GetProviderDataSources(string databaseName)
		{
			return this.GetDatabaseOrThrow(databaseName).Model.DataSources.OfType<ProviderDataSource>();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E12 File Offset: 0x00002012
		public virtual IEnumerable<ModelRole> GetModelRoles(string databaseName)
		{
			return this.GetDatabaseOrThrow(databaseName).Model.Roles;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E28 File Offset: 0x00002028
		public virtual IEnumerable<KeyValuePair<string, string>> GetModelParameters(string databaseName)
		{
			Model model = this.GetDatabaseOrThrow(databaseName).Model;
			IEnumerable<KeyValuePair<string, string>> enumerable;
			if (model == null)
			{
				enumerable = null;
			}
			else
			{
				NamedExpressionCollection expressions = model.Expressions;
				if (expressions == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = from p in expressions
						where p.Kind == ExpressionKind.M
						select new KeyValuePair<string, string>(p.Name, p.Expression);
				}
			}
			return enumerable ?? new List<KeyValuePair<string, string>>();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003EA8 File Offset: 0x000020A8
		public virtual void SetModelParameters(string databaseName, Dictionary<string, string> parameters)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			Model model = databaseOrThrow.Model;
			if (((model != null) ? model.Expressions : null) != null)
			{
				foreach (string text in parameters.Keys)
				{
					databaseOrThrow.Model.Expressions.Find(text).Expression = parameters[text];
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F30 File Offset: 0x00002130
		public virtual PowerBIDataSourceVersion GetPowerBIDataSourceVersion(string databaseName)
		{
			return this.GetDatabaseOrThrow(databaseName).Model.DefaultPowerBIDataSourceVersion;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F43 File Offset: 0x00002143
		public virtual void SaveChanges(string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			databaseOrThrow.Model.SaveChanges();
			databaseOrThrow.Update(UpdateOptions.ExpandFull);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F5E File Offset: 0x0000215E
		public virtual void RefreshModel(Database database)
		{
			ContractExtensions.NotNull<Database>(database, "database");
			database.Model.RequestRefresh(Microsoft.AnalysisServices.Tabular.RefreshType.Full);
			database.Model.SaveChanges();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F83 File Offset: 0x00002183
		public virtual ModeType GetDefaultDatabaseMode(string databaseName)
		{
			return this.GetDatabaseOrThrow(databaseName).Model.DefaultMode;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F98 File Offset: 0x00002198
		public virtual IReadOnlyDictionary<string, IEnumerable<Partition>> GetTablePartitions(string databaseName)
		{
			Database databaseOrThrow = this.GetDatabaseOrThrow(databaseName);
			Dictionary<string, IEnumerable<Partition>> dictionary = new Dictionary<string, IEnumerable<Partition>>();
			Model model = databaseOrThrow.Model;
			bool flag;
			if (model == null)
			{
				flag = false;
			}
			else
			{
				TableCollection tables = model.Tables;
				flag = ((tables != null) ? new bool?(tables.Any<Table>()) : null) == true;
			}
			if (flag)
			{
				foreach (Table table in databaseOrThrow.Model.Tables)
				{
					Dictionary<string, IEnumerable<Partition>> dictionary2 = dictionary;
					string name = table.Name;
					IEnumerable<Partition> partitions = table.Partitions;
					dictionary2.Add(name, partitions ?? Enumerable.Empty<Partition>());
				}
			}
			return dictionary;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
			this._asServer.Dispose();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004070 File Offset: 0x00002270
		public virtual void Refresh(bool full)
		{
			if (this._asServer.Connected)
			{
				try
				{
					this._asServer.Refresh(full);
				}
				catch (ArgumentException ex)
				{
					throw new TOMClientServerRefreshException("Refresh on Analysis Services Server failed with exception: {0}", ex);
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000040B8 File Offset: 0x000022B8
		private Database GetDatabaseOrThrow(string databaseName)
		{
			Database database = this._asServer.Databases.FindByName(databaseName);
			if (database == null)
			{
				throw new DatabaseNotFoundException(string.Format("Couldn't find database: {0} in AS server ", databaseName));
			}
			return database;
		}

		// Token: 0x0400004B RID: 75
		protected readonly Server _asServer;
	}
}
