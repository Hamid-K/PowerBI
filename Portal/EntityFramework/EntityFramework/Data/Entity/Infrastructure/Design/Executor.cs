using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200029C RID: 668
	public class Executor : MarshalByRefObject
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x0005D784 File Offset: 0x0005B984
		public Executor(string assemblyFile, IDictionary<string, object> anonymousArguments)
		{
			Check.NotEmpty(assemblyFile, "assemblyFile");
			this._reporter = new Reporter(new WrappedReportHandler((anonymousArguments != null) ? anonymousArguments["reportHandler"] : null));
			this._language = (string)((anonymousArguments != null) ? anonymousArguments["language"] : null);
			this._rootNamespace = (string)((anonymousArguments != null) ? anonymousArguments["rootNamespace"] : null);
			this._assembly = Assembly.Load(AssemblyName.GetAssemblyName(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyFile)));
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0005D81C File Offset: 0x0005BA1C
		private Assembly LoadAssembly(string assemblyName)
		{
			if (string.IsNullOrEmpty(assemblyName))
			{
				return null;
			}
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException ex)
			{
				throw new MigrationsException(Strings.ToolingFacade_AssemblyNotFound(ex.FileName), ex);
			}
			return assembly;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0005D864 File Offset: 0x0005BA64
		private string GetContextTypeInternal(string contextTypeName, string contextAssemblyName)
		{
			return new TypeFinder(this.LoadAssembly(contextAssemblyName) ?? this._assembly).FindType(typeof(DbContext), contextTypeName, (IEnumerable<Type> types) => types.Where((Type t) => !typeof(HistoryContext).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType), new Func<string, Exception>(Error.EnableMigrations_NoContext), delegate(string assembly, IEnumerable<Type> types)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Strings.EnableMigrations_MultipleContexts(assembly));
				foreach (Type type in types)
				{
					stringBuilder.AppendLine();
					stringBuilder.Append(Strings.EnableMigrationsForContext(type.FullName));
				}
				return new MigrationsException(stringBuilder.ToString());
			}, new Func<string, string, Exception>(Error.EnableMigrations_NoContextWithName), new Func<string, string, Exception>(Error.EnableMigrations_MultipleContextsWithName)).FullName;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0005D900 File Offset: 0x0005BB00
		internal virtual string GetProviderServicesInternal(string invariantName)
		{
			DbConfiguration.LoadConfiguration(this._assembly);
			IDbDependencyResolver dependencyResolver = DbConfiguration.DependencyResolver;
			DbProviderServices dbProviderServices = null;
			try
			{
				dbProviderServices = dependencyResolver.GetService(invariantName);
			}
			catch
			{
			}
			if (dbProviderServices == null)
			{
				return null;
			}
			return dbProviderServices.GetType().AssemblyQualifiedName;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0005D950 File Offset: 0x0005BB50
		private void OverrideConfiguration(DbMigrationsConfiguration configuration, DbConnectionInfo connectionInfo, bool force = false)
		{
			if (connectionInfo != null)
			{
				configuration.TargetDatabase = connectionInfo;
			}
			if (string.Equals(this._language, "VB", StringComparison.OrdinalIgnoreCase) && configuration.CodeGenerator is CSharpMigrationCodeGenerator)
			{
				configuration.CodeGenerator = new VisualBasicMigrationCodeGenerator();
			}
			if (force)
			{
				configuration.AutomaticMigrationDataLossAllowed = true;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0005D99C File Offset: 0x0005BB9C
		private MigrationScaffolder CreateMigrationScaffolder(DbMigrationsConfiguration configuration)
		{
			MigrationScaffolder migrationScaffolder = new MigrationScaffolder(configuration);
			string text = configuration.MigrationsNamespace;
			if (string.Equals(this._language, "VB", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(this._rootNamespace))
			{
				if (this._rootNamespace.EqualsIgnoreCase(text))
				{
					text = null;
				}
				else
				{
					if (text == null || !text.StartsWith(this._rootNamespace + ".", StringComparison.OrdinalIgnoreCase))
					{
						throw Error.MigrationsNamespaceNotUnderRootNamespace(text, this._rootNamespace);
					}
					text = text.Substring(this._rootNamespace.Length + 1);
				}
			}
			migrationScaffolder.Namespace = text;
			return migrationScaffolder;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0005DA30 File Offset: 0x0005BC30
		private static IDictionary ToHashtable(ScaffoldedMigration result)
		{
			if (result != null)
			{
				Hashtable hashtable = new Hashtable();
				object obj = "MigrationId";
				hashtable[obj] = result.MigrationId;
				object obj2 = "UserCode";
				hashtable[obj2] = result.UserCode;
				object obj3 = "DesignerCode";
				hashtable[obj3] = result.DesignerCode;
				object obj4 = "Language";
				hashtable[obj4] = result.Language;
				object obj5 = "Directory";
				hashtable[obj5] = result.Directory;
				object obj6 = "Resources";
				hashtable[obj6] = result.Resources;
				object obj7 = "IsRescaffold";
				hashtable[obj7] = result.IsRescaffold;
				return hashtable;
			}
			return null;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0005DADC File Offset: 0x0005BCDC
		internal virtual IDictionary ScaffoldInitialCreateInternal(DbConnectionInfo connectionInfo, string contextTypeName, string contextAssemblyName, string migrationsNamespace, bool auto, string migrationsDir)
		{
			Assembly assembly = this.LoadAssembly(contextAssemblyName) ?? this._assembly;
			DbMigrationsConfiguration dbMigrationsConfiguration = new DbMigrationsConfiguration
			{
				ContextType = assembly.GetType(contextTypeName, true),
				MigrationsAssembly = this._assembly,
				MigrationsNamespace = migrationsNamespace,
				AutomaticMigrationsEnabled = auto,
				MigrationsDirectory = migrationsDir
			};
			this.OverrideConfiguration(dbMigrationsConfiguration, connectionInfo, false);
			return Executor.ToHashtable(this.CreateMigrationScaffolder(dbMigrationsConfiguration).ScaffoldInitialCreate());
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0005DB50 File Offset: 0x0005BD50
		private DbMigrationsConfiguration GetMigrationsConfiguration(string migrationsConfigurationName)
		{
			return new MigrationsConfigurationFinder(new TypeFinder(this._assembly)).FindMigrationsConfiguration(null, migrationsConfigurationName, new Func<string, Exception>(Error.AssemblyMigrator_NoConfiguration), (string assembly, IEnumerable<Type> types) => Error.AssemblyMigrator_MultipleConfigurations(assembly), new Func<string, string, Exception>(Error.AssemblyMigrator_NoConfigurationWithName), new Func<string, string, Exception>(Error.AssemblyMigrator_MultipleConfigurationsWithName));
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0005DBB8 File Offset: 0x0005BDB8
		internal virtual IDictionary ScaffoldInternal(string name, DbConnectionInfo connectionInfo, string migrationsConfigurationName, bool ignoreChanges)
		{
			DbMigrationsConfiguration migrationsConfiguration = this.GetMigrationsConfiguration(migrationsConfigurationName);
			this.OverrideConfiguration(migrationsConfiguration, connectionInfo, false);
			return Executor.ToHashtable(this.CreateMigrationScaffolder(migrationsConfiguration).Scaffold(name, ignoreChanges));
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0005DBEC File Offset: 0x0005BDEC
		internal IEnumerable<string> GetDatabaseMigrationsInternal(DbConnectionInfo connectionInfo, string migrationsConfigurationName)
		{
			DbMigrationsConfiguration migrationsConfiguration = this.GetMigrationsConfiguration(migrationsConfigurationName);
			this.OverrideConfiguration(migrationsConfiguration, connectionInfo, false);
			return this.CreateMigrator(migrationsConfiguration).GetDatabaseMigrations();
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0005DC18 File Offset: 0x0005BE18
		internal string ScriptUpdateInternal(string sourceMigration, string targetMigration, bool force, DbConnectionInfo connectionInfo, string migrationsConfigurationName)
		{
			DbMigrationsConfiguration migrationsConfiguration = this.GetMigrationsConfiguration(migrationsConfigurationName);
			this.OverrideConfiguration(migrationsConfiguration, connectionInfo, force);
			return new MigratorScriptingDecorator(this.CreateMigrator(migrationsConfiguration)).ScriptUpdate(sourceMigration, targetMigration);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		internal void UpdateInternal(string targetMigration, bool force, DbConnectionInfo connectionInfo, string migrationsConfigurationName)
		{
			DbMigrationsConfiguration migrationsConfiguration = this.GetMigrationsConfiguration(migrationsConfigurationName);
			this.OverrideConfiguration(migrationsConfiguration, connectionInfo, force);
			this.CreateMigrator(migrationsConfiguration).Update(targetMigration);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005DC78 File Offset: 0x0005BE78
		private MigratorBase CreateMigrator(DbMigrationsConfiguration configuration)
		{
			return new MigratorLoggingDecorator(new DbMigrator(configuration), new Executor.ToolLogger(this._reporter));
		}

		// Token: 0x04000B9A RID: 2970
		private readonly Assembly _assembly;

		// Token: 0x04000B9B RID: 2971
		private readonly Reporter _reporter;

		// Token: 0x04000B9C RID: 2972
		private readonly string _language;

		// Token: 0x04000B9D RID: 2973
		private readonly string _rootNamespace;

		// Token: 0x0200098C RID: 2444
		public class GetContextType : Executor.OperationBase
		{
			// Token: 0x06005EAC RID: 24236 RVA: 0x001465AC File Offset: 0x001447AC
			public GetContextType(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string contextTypeName = (string)args["contextTypeName"];
				string contextAssemblyName = (string)args["contextAssemblyName"];
				this.Execute<string>(() => executor.GetContextTypeInternal(contextTypeName, contextAssemblyName));
			}
		}

		// Token: 0x0200098D RID: 2445
		internal class GetProviderServices : Executor.OperationBase
		{
			// Token: 0x06005EAD RID: 24237 RVA: 0x00146634 File Offset: 0x00144834
			public GetProviderServices(Executor executor, object handler, string invariantName, IDictionary<string, object> anonymousArguments)
				: base(handler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotEmpty(invariantName, "invariantName");
				this.Execute<string>(() => executor.GetProviderServicesInternal(invariantName));
			}
		}

		// Token: 0x0200098E RID: 2446
		public class ScaffoldInitialCreate : Executor.OperationBase
		{
			// Token: 0x06005EAE RID: 24238 RVA: 0x00146690 File Offset: 0x00144890
			public ScaffoldInitialCreate(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string connectionStringName = (string)args["connectionStringName"];
				string connectionString = (string)args["connectionString"];
				string connectionProviderName = (string)args["connectionProviderName"];
				string contextTypeName = (string)args["contextTypeName"];
				string contextAssemblyName = (string)args["contextAssemblyName"];
				string migrationsNamespace = (string)args["migrationsNamespace"];
				bool auto = (bool)args["auto"];
				string migrationsDir = (string)args["migrationsDir"];
				this.Execute<IDictionary>(() => executor.ScaffoldInitialCreateInternal(Executor.OperationBase.CreateConnectionInfo(connectionStringName, connectionString, connectionProviderName), contextTypeName, contextAssemblyName, migrationsNamespace, auto, migrationsDir));
			}
		}

		// Token: 0x0200098F RID: 2447
		public class Scaffold : Executor.OperationBase
		{
			// Token: 0x06005EAF RID: 24239 RVA: 0x0014679C File Offset: 0x0014499C
			public Scaffold(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string name = (string)args["name"];
				string connectionStringName = (string)args["connectionStringName"];
				string connectionString = (string)args["connectionString"];
				string connectionProviderName = (string)args["connectionProviderName"];
				string migrationsConfigurationName = (string)args["migrationsConfigurationName"];
				bool ignoreChanges = (bool)args["ignoreChanges"];
				this.Execute<IDictionary>(() => executor.ScaffoldInternal(name, Executor.OperationBase.CreateConnectionInfo(connectionStringName, connectionString, connectionProviderName), migrationsConfigurationName, ignoreChanges));
			}
		}

		// Token: 0x02000990 RID: 2448
		public class GetDatabaseMigrations : Executor.OperationBase
		{
			// Token: 0x06005EB0 RID: 24240 RVA: 0x0014687C File Offset: 0x00144A7C
			public GetDatabaseMigrations(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string connectionStringName = (string)args["connectionStringName"];
				string connectionString = (string)args["connectionString"];
				string connectionProviderName = (string)args["connectionProviderName"];
				string migrationsConfigurationName = (string)args["migrationsConfigurationName"];
				this.Execute<string>(() => executor.GetDatabaseMigrationsInternal(Executor.OperationBase.CreateConnectionInfo(connectionStringName, connectionString, connectionProviderName), migrationsConfigurationName));
			}
		}

		// Token: 0x02000991 RID: 2449
		public class ScriptUpdate : Executor.OperationBase
		{
			// Token: 0x06005EB1 RID: 24241 RVA: 0x00146930 File Offset: 0x00144B30
			public ScriptUpdate(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string sourceMigration = (string)args["sourceMigration"];
				string targetMigration = (string)args["targetMigration"];
				bool force = (bool)args["force"];
				string connectionStringName = (string)args["connectionStringName"];
				string connectionString = (string)args["connectionString"];
				string connectionProviderName = (string)args["connectionProviderName"];
				string migrationsConfigurationName = (string)args["migrationsConfigurationName"];
				this.Execute<string>(() => executor.ScriptUpdateInternal(sourceMigration, targetMigration, force, Executor.OperationBase.CreateConnectionInfo(connectionStringName, connectionString, connectionProviderName), migrationsConfigurationName));
			}
		}

		// Token: 0x02000992 RID: 2450
		public class Update : Executor.OperationBase
		{
			// Token: 0x06005EB2 RID: 24242 RVA: 0x00146A28 File Offset: 0x00144C28
			public Update(Executor executor, object resultHandler, IDictionary args)
				: base(resultHandler)
			{
				Check.NotNull<Executor>(executor, "executor");
				Check.NotNull<object>(resultHandler, "resultHandler");
				Check.NotNull<IDictionary>(args, "args");
				string targetMigration = (string)args["targetMigration"];
				bool force = (bool)args["force"];
				string connectionStringName = (string)args["connectionStringName"];
				string connectionString = (string)args["connectionString"];
				string connectionProviderName = (string)args["connectionProviderName"];
				string migrationsConfigurationName = (string)args["migrationsConfigurationName"];
				this.Execute(delegate
				{
					executor.UpdateInternal(targetMigration, force, Executor.OperationBase.CreateConnectionInfo(connectionStringName, connectionString, connectionProviderName), migrationsConfigurationName);
				});
			}
		}

		// Token: 0x02000993 RID: 2451
		public abstract class OperationBase : MarshalByRefObject
		{
			// Token: 0x06005EB3 RID: 24243 RVA: 0x00146B08 File Offset: 0x00144D08
			protected OperationBase(object handler)
			{
				Check.NotNull<object>(handler, "handler");
				this._handler = new WrappedResultHandler(handler);
			}

			// Token: 0x06005EB4 RID: 24244 RVA: 0x00146B28 File Offset: 0x00144D28
			protected static DbConnectionInfo CreateConnectionInfo(string connectionStringName, string connectionString, string connectionProviderName)
			{
				if (!string.IsNullOrWhiteSpace(connectionStringName))
				{
					return new DbConnectionInfo(connectionStringName);
				}
				if (!string.IsNullOrWhiteSpace(connectionString))
				{
					return new DbConnectionInfo(connectionString, connectionProviderName);
				}
				return null;
			}

			// Token: 0x06005EB5 RID: 24245 RVA: 0x00146B4C File Offset: 0x00144D4C
			protected virtual void Execute(Action action)
			{
				Check.NotNull<Action>(action, "action");
				try
				{
					action();
				}
				catch (Exception ex)
				{
					if (!this._handler.SetError(ex.GetType().FullName, ex.Message, ex.ToString()))
					{
						throw;
					}
				}
			}

			// Token: 0x06005EB6 RID: 24246 RVA: 0x00146BA8 File Offset: 0x00144DA8
			protected virtual void Execute<T>(Func<T> action)
			{
				Check.NotNull<Func<T>>(action, "action");
				this.Execute(delegate
				{
					this._handler.SetResult(action());
				});
			}

			// Token: 0x06005EB7 RID: 24247 RVA: 0x00146BEC File Offset: 0x00144DEC
			protected virtual void Execute<T>(Func<IEnumerable<T>> action)
			{
				Check.NotNull<Func<IEnumerable<T>>>(action, "action");
				this.Execute(delegate
				{
					this._handler.SetResult(action().ToArray<T>());
				});
			}

			// Token: 0x04002785 RID: 10117
			private readonly WrappedResultHandler _handler;
		}

		// Token: 0x02000994 RID: 2452
		private class ToolLogger : MigrationsLogger
		{
			// Token: 0x06005EB8 RID: 24248 RVA: 0x00146C30 File Offset: 0x00144E30
			public ToolLogger(Reporter reporter)
			{
				this._reporter = reporter;
			}

			// Token: 0x06005EB9 RID: 24249 RVA: 0x00146C3F File Offset: 0x00144E3F
			public override void Info(string message)
			{
				this._reporter.WriteInformation(message);
			}

			// Token: 0x06005EBA RID: 24250 RVA: 0x00146C4D File Offset: 0x00144E4D
			public override void Warning(string message)
			{
				this._reporter.WriteWarning(message);
			}

			// Token: 0x06005EBB RID: 24251 RVA: 0x00146C5B File Offset: 0x00144E5B
			public override void Verbose(string sql)
			{
				this._reporter.WriteVerbose(sql);
			}

			// Token: 0x04002786 RID: 10118
			private readonly Reporter _reporter;
		}
	}
}
