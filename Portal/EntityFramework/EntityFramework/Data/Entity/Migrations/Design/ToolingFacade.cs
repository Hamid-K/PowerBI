using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E7 RID: 231
	[Obsolete("Use System.Data.Entity.Infrastructure.Design.Executor instead.")]
	public class ToolingFacade : IDisposable
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0002ABD2 File Offset: 0x00028DD2
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0002ABDA File Offset: 0x00028DDA
		public Action<string> LogInfoDelegate { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0002ABE3 File Offset: 0x00028DE3
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0002ABEB File Offset: 0x00028DEB
		public Action<string> LogWarningDelegate { get; set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public Action<string> LogVerboseDelegate { get; set; }

		// Token: 0x0600116A RID: 4458 RVA: 0x0002AC08 File Offset: 0x00028E08
		public ToolingFacade(string migrationsAssemblyName, string contextAssemblyName, string configurationTypeName, string workingDirectory, string configurationFilePath, string dataDirectory, DbConnectionInfo connectionStringInfo)
		{
			Check.NotEmpty(migrationsAssemblyName, "migrationsAssemblyName");
			this._migrationsAssemblyName = migrationsAssemblyName;
			this._contextAssemblyName = contextAssemblyName;
			this._configurationTypeName = configurationTypeName;
			this._connectionStringInfo = connectionStringInfo;
			AppDomainSetup appDomainSetup = new AppDomainSetup
			{
				ShadowCopyFiles = "true"
			};
			if (!string.IsNullOrWhiteSpace(workingDirectory))
			{
				appDomainSetup.ApplicationBase = workingDirectory;
			}
			this._configurationFile = new ConfigurationFileUpdater().Update(configurationFilePath);
			appDomainSetup.ConfigurationFile = this._configurationFile;
			string text = "MigrationsToolingFacade" + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			this._appDomain = AppDomain.CreateDomain(text, null, appDomainSetup);
			if (!string.IsNullOrWhiteSpace(dataDirectory))
			{
				this._appDomain.SetData("DataDirectory", dataDirectory);
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0002ACCA File Offset: 0x00028ECA
		internal ToolingFacade()
		{
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0002ACD4 File Offset: 0x00028ED4
		~ToolingFacade()
		{
			this.Dispose(false);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0002AD04 File Offset: 0x00028F04
		public IEnumerable<string> GetContextTypes()
		{
			ToolingFacade.GetContextTypesRunner getContextTypesRunner = new ToolingFacade.GetContextTypesRunner();
			this.ConfigureRunner(getContextTypesRunner);
			this.Run(getContextTypesRunner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0002AD3C File Offset: 0x00028F3C
		public string GetContextType(string contextTypeName)
		{
			ToolingFacade.GetContextTypeRunner getContextTypeRunner = new ToolingFacade.GetContextTypeRunner
			{
				ContextTypeName = contextTypeName
			};
			this.ConfigureRunner(getContextTypeRunner);
			this.Run(getContextTypeRunner);
			return (string)this._appDomain.GetData("result");
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0002AD7C File Offset: 0x00028F7C
		public virtual IEnumerable<string> GetDatabaseMigrations()
		{
			ToolingFacade.GetDatabaseMigrationsRunner getDatabaseMigrationsRunner = new ToolingFacade.GetDatabaseMigrationsRunner();
			this.ConfigureRunner(getDatabaseMigrationsRunner);
			this.Run(getDatabaseMigrationsRunner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		public virtual IEnumerable<string> GetPendingMigrations()
		{
			ToolingFacade.GetPendingMigrationsRunner getPendingMigrationsRunner = new ToolingFacade.GetPendingMigrationsRunner();
			this.ConfigureRunner(getPendingMigrationsRunner);
			this.Run(getPendingMigrationsRunner);
			return (IEnumerable<string>)this._appDomain.GetData("result");
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0002ADEC File Offset: 0x00028FEC
		public void Update(string targetMigration, bool force)
		{
			ToolingFacade.UpdateRunner updateRunner = new ToolingFacade.UpdateRunner
			{
				TargetMigration = targetMigration,
				Force = force
			};
			this.ConfigureRunner(updateRunner);
			this.Run(updateRunner);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0002AE1C File Offset: 0x0002901C
		public string ScriptUpdate(string sourceMigration, string targetMigration, bool force)
		{
			ToolingFacade.ScriptUpdateRunner scriptUpdateRunner = new ToolingFacade.ScriptUpdateRunner
			{
				SourceMigration = sourceMigration,
				TargetMigration = targetMigration,
				Force = force
			};
			this.ConfigureRunner(scriptUpdateRunner);
			this.Run(scriptUpdateRunner);
			return (string)this._appDomain.GetData("result");
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0002AE68 File Offset: 0x00029068
		public virtual ScaffoldedMigration Scaffold(string migrationName, string language, string rootNamespace, bool ignoreChanges)
		{
			ToolingFacade.ScaffoldRunner scaffoldRunner = new ToolingFacade.ScaffoldRunner
			{
				MigrationName = migrationName,
				Language = language,
				RootNamespace = rootNamespace,
				IgnoreChanges = ignoreChanges
			};
			this.ConfigureRunner(scaffoldRunner);
			this.Run(scaffoldRunner);
			return (ScaffoldedMigration)this._appDomain.GetData("result");
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0002AEBC File Offset: 0x000290BC
		public ScaffoldedMigration ScaffoldInitialCreate(string language, string rootNamespace)
		{
			ToolingFacade.InitialCreateScaffoldRunner initialCreateScaffoldRunner = new ToolingFacade.InitialCreateScaffoldRunner
			{
				Language = language,
				RootNamespace = rootNamespace
			};
			this.ConfigureRunner(initialCreateScaffoldRunner);
			this.Run(initialCreateScaffoldRunner);
			return (ScaffoldedMigration)this._appDomain.GetData("result");
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0002AF00 File Offset: 0x00029100
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0002AF0F File Offset: 0x0002910F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._appDomain != null)
			{
				AppDomain.Unload(this._appDomain);
				this._appDomain = null;
			}
			if (this._configurationFile != null)
			{
				File.Delete(this._configurationFile);
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0002AF41 File Offset: 0x00029141
		private void ConfigureRunner(ToolingFacade.BaseRunner runner)
		{
			runner.MigrationsAssemblyName = this._migrationsAssemblyName;
			runner.ContextAssemblyName = this._contextAssemblyName;
			runner.ConfigurationTypeName = this._configurationTypeName;
			runner.ConnectionStringInfo = this._connectionStringInfo;
			runner.Log = new ToolingFacade.ToolLogger(this);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0002AF80 File Offset: 0x00029180
		private void Run(ToolingFacade.BaseRunner runner)
		{
			this._appDomain.SetData("error", null);
			this._appDomain.SetData("typeName", null);
			this._appDomain.SetData("stackTrace", null);
			this._appDomain.DoCallBack(new CrossAppDomainDelegate(runner.Run));
			string text = (string)this._appDomain.GetData("error");
			if (text != null)
			{
				string text2 = (string)this._appDomain.GetData("typeName");
				string text3 = (string)this._appDomain.GetData("stackTrace");
				throw new ToolingException(text, text2, text3);
			}
		}

		// Token: 0x040008E1 RID: 2273
		private readonly string _migrationsAssemblyName;

		// Token: 0x040008E2 RID: 2274
		private readonly string _contextAssemblyName;

		// Token: 0x040008E3 RID: 2275
		private readonly string _configurationTypeName;

		// Token: 0x040008E4 RID: 2276
		private readonly string _configurationFile;

		// Token: 0x040008E5 RID: 2277
		private readonly DbConnectionInfo _connectionStringInfo;

		// Token: 0x040008E6 RID: 2278
		private AppDomain _appDomain;

		// Token: 0x020007CC RID: 1996
		private class ToolLogger : MigrationsLogger
		{
			// Token: 0x0600585C RID: 22620 RVA: 0x00138CDA File Offset: 0x00136EDA
			public ToolLogger(ToolingFacade facade)
			{
				this._facade = facade;
			}

			// Token: 0x0600585D RID: 22621 RVA: 0x00138CE9 File Offset: 0x00136EE9
			public override void Info(string message)
			{
				if (this._facade.LogInfoDelegate != null)
				{
					this._facade.LogInfoDelegate(message);
				}
			}

			// Token: 0x0600585E RID: 22622 RVA: 0x00138D09 File Offset: 0x00136F09
			public override void Warning(string message)
			{
				if (this._facade.LogWarningDelegate != null)
				{
					this._facade.LogWarningDelegate(message);
				}
			}

			// Token: 0x0600585F RID: 22623 RVA: 0x00138D29 File Offset: 0x00136F29
			public override void Verbose(string sql)
			{
				if (this._facade.LogVerboseDelegate != null)
				{
					this._facade.LogVerboseDelegate(sql);
				}
			}

			// Token: 0x04002166 RID: 8550
			private readonly ToolingFacade _facade;
		}

		// Token: 0x020007CD RID: 1997
		[Serializable]
		private abstract class BaseRunner
		{
			// Token: 0x17001043 RID: 4163
			// (get) Token: 0x06005860 RID: 22624 RVA: 0x00138D49 File Offset: 0x00136F49
			// (set) Token: 0x06005861 RID: 22625 RVA: 0x00138D51 File Offset: 0x00136F51
			public string MigrationsAssemblyName { get; set; }

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x06005862 RID: 22626 RVA: 0x00138D5A File Offset: 0x00136F5A
			// (set) Token: 0x06005863 RID: 22627 RVA: 0x00138D62 File Offset: 0x00136F62
			public string ContextAssemblyName { get; set; }

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x06005864 RID: 22628 RVA: 0x00138D6B File Offset: 0x00136F6B
			// (set) Token: 0x06005865 RID: 22629 RVA: 0x00138D73 File Offset: 0x00136F73
			public string ConfigurationTypeName { get; set; }

			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x06005866 RID: 22630 RVA: 0x00138D7C File Offset: 0x00136F7C
			// (set) Token: 0x06005867 RID: 22631 RVA: 0x00138D84 File Offset: 0x00136F84
			public DbConnectionInfo ConnectionStringInfo { get; set; }

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005868 RID: 22632 RVA: 0x00138D8D File Offset: 0x00136F8D
			// (set) Token: 0x06005869 RID: 22633 RVA: 0x00138D95 File Offset: 0x00136F95
			public ToolingFacade.ToolLogger Log { get; set; }

			// Token: 0x0600586A RID: 22634 RVA: 0x00138DA0 File Offset: 0x00136FA0
			public void Run()
			{
				try
				{
					this.RunCore();
				}
				catch (Exception ex)
				{
					AppDomain.CurrentDomain.SetData("error", ex.Message);
					AppDomain.CurrentDomain.SetData("typeName", ex.GetType().FullName);
					AppDomain.CurrentDomain.SetData("stackTrace", ex.ToString());
				}
			}

			// Token: 0x0600586B RID: 22635
			protected abstract void RunCore();

			// Token: 0x0600586C RID: 22636 RVA: 0x00138E0C File Offset: 0x0013700C
			protected MigratorBase GetMigrator()
			{
				return this.DecorateMigrator(new DbMigrator(this.GetConfiguration()));
			}

			// Token: 0x0600586D RID: 22637 RVA: 0x00138E20 File Offset: 0x00137020
			protected DbMigrationsConfiguration GetConfiguration()
			{
				DbMigrationsConfiguration dbMigrationsConfiguration = this.FindConfiguration();
				this.OverrideConfiguration(dbMigrationsConfiguration);
				return dbMigrationsConfiguration;
			}

			// Token: 0x0600586E RID: 22638 RVA: 0x00138E3C File Offset: 0x0013703C
			protected virtual void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				if (this.ConnectionStringInfo != null)
				{
					configuration.TargetDatabase = this.ConnectionStringInfo;
				}
			}

			// Token: 0x0600586F RID: 22639 RVA: 0x00138E52 File Offset: 0x00137052
			private MigratorBase DecorateMigrator(DbMigrator migrator)
			{
				return new MigratorLoggingDecorator(migrator, this.Log);
			}

			// Token: 0x06005870 RID: 22640 RVA: 0x00138E60 File Offset: 0x00137060
			private DbMigrationsConfiguration FindConfiguration()
			{
				return new MigrationsConfigurationFinder(new TypeFinder(this.LoadMigrationsAssembly())).FindMigrationsConfiguration(null, this.ConfigurationTypeName, new Func<string, Exception>(Error.AssemblyMigrator_NoConfiguration), (string assembly, IEnumerable<Type> types) => Error.AssemblyMigrator_MultipleConfigurations(assembly), new Func<string, string, Exception>(Error.AssemblyMigrator_NoConfigurationWithName), new Func<string, string, Exception>(Error.AssemblyMigrator_MultipleConfigurationsWithName));
			}

			// Token: 0x06005871 RID: 22641 RVA: 0x00138ECC File Offset: 0x001370CC
			protected Assembly LoadMigrationsAssembly()
			{
				return ToolingFacade.BaseRunner.LoadAssembly(this.MigrationsAssemblyName);
			}

			// Token: 0x06005872 RID: 22642 RVA: 0x00138ED9 File Offset: 0x001370D9
			protected Assembly LoadContextAssembly()
			{
				return ToolingFacade.BaseRunner.LoadAssembly(this.ContextAssemblyName);
			}

			// Token: 0x06005873 RID: 22643 RVA: 0x00138EE8 File Offset: 0x001370E8
			private static Assembly LoadAssembly(string name)
			{
				Assembly assembly;
				try
				{
					assembly = Assembly.Load(name);
				}
				catch (FileNotFoundException ex)
				{
					throw new MigrationsException(Strings.ToolingFacade_AssemblyNotFound(ex.FileName), ex);
				}
				return assembly;
			}
		}

		// Token: 0x020007CE RID: 1998
		[Serializable]
		private class GetDatabaseMigrationsRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x06005875 RID: 22645 RVA: 0x00138F2C File Offset: 0x0013712C
			protected override void RunCore()
			{
				IEnumerable<string> databaseMigrations = base.GetMigrator().GetDatabaseMigrations();
				AppDomain.CurrentDomain.SetData("result", databaseMigrations);
			}
		}

		// Token: 0x020007CF RID: 1999
		[Serializable]
		private class GetPendingMigrationsRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x06005877 RID: 22647 RVA: 0x00138F60 File Offset: 0x00137160
			protected override void RunCore()
			{
				IEnumerable<string> pendingMigrations = base.GetMigrator().GetPendingMigrations();
				AppDomain.CurrentDomain.SetData("result", pendingMigrations);
			}
		}

		// Token: 0x020007D0 RID: 2000
		[Serializable]
		private class UpdateRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005879 RID: 22649 RVA: 0x00138F91 File Offset: 0x00137191
			// (set) Token: 0x0600587A RID: 22650 RVA: 0x00138F99 File Offset: 0x00137199
			public string TargetMigration { get; set; }

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x0600587B RID: 22651 RVA: 0x00138FA2 File Offset: 0x001371A2
			// (set) Token: 0x0600587C RID: 22652 RVA: 0x00138FAA File Offset: 0x001371AA
			public bool Force { get; set; }

			// Token: 0x0600587D RID: 22653 RVA: 0x00138FB3 File Offset: 0x001371B3
			protected override void RunCore()
			{
				base.GetMigrator().Update(this.TargetMigration);
			}

			// Token: 0x0600587E RID: 22654 RVA: 0x00138FC6 File Offset: 0x001371C6
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Force)
				{
					configuration.AutomaticMigrationDataLossAllowed = true;
				}
			}
		}

		// Token: 0x020007D1 RID: 2001
		[Serializable]
		private class ScriptUpdateRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x06005880 RID: 22656 RVA: 0x00138FE6 File Offset: 0x001371E6
			// (set) Token: 0x06005881 RID: 22657 RVA: 0x00138FEE File Offset: 0x001371EE
			public string SourceMigration { get; set; }

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x06005882 RID: 22658 RVA: 0x00138FF7 File Offset: 0x001371F7
			// (set) Token: 0x06005883 RID: 22659 RVA: 0x00138FFF File Offset: 0x001371FF
			public string TargetMigration { get; set; }

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x06005884 RID: 22660 RVA: 0x00139008 File Offset: 0x00137208
			// (set) Token: 0x06005885 RID: 22661 RVA: 0x00139010 File Offset: 0x00137210
			public bool Force { get; set; }

			// Token: 0x06005886 RID: 22662 RVA: 0x0013901C File Offset: 0x0013721C
			protected override void RunCore()
			{
				string text = new MigratorScriptingDecorator(base.GetMigrator()).ScriptUpdate(this.SourceMigration, this.TargetMigration);
				AppDomain.CurrentDomain.SetData("result", text);
			}

			// Token: 0x06005887 RID: 22663 RVA: 0x00139056 File Offset: 0x00137256
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Force)
				{
					configuration.AutomaticMigrationDataLossAllowed = true;
				}
			}
		}

		// Token: 0x020007D2 RID: 2002
		[Serializable]
		private class ScaffoldRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x06005889 RID: 22665 RVA: 0x00139076 File Offset: 0x00137276
			// (set) Token: 0x0600588A RID: 22666 RVA: 0x0013907E File Offset: 0x0013727E
			public string MigrationName { get; set; }

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x0600588B RID: 22667 RVA: 0x00139087 File Offset: 0x00137287
			// (set) Token: 0x0600588C RID: 22668 RVA: 0x0013908F File Offset: 0x0013728F
			public string Language { get; set; }

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x0600588D RID: 22669 RVA: 0x00139098 File Offset: 0x00137298
			// (set) Token: 0x0600588E RID: 22670 RVA: 0x001390A0 File Offset: 0x001372A0
			public string RootNamespace { get; set; }

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x0600588F RID: 22671 RVA: 0x001390A9 File Offset: 0x001372A9
			// (set) Token: 0x06005890 RID: 22672 RVA: 0x001390B1 File Offset: 0x001372B1
			public bool IgnoreChanges { get; set; }

			// Token: 0x06005891 RID: 22673 RVA: 0x001390BC File Offset: 0x001372BC
			protected override void RunCore()
			{
				DbMigrationsConfiguration configuration = base.GetConfiguration();
				MigrationScaffolder migrationScaffolder = new MigrationScaffolder(configuration);
				string text = configuration.MigrationsNamespace;
				if (this.Language == "vb" && !string.IsNullOrWhiteSpace(this.RootNamespace))
				{
					if (this.RootNamespace.EqualsIgnoreCase(text))
					{
						text = null;
					}
					else
					{
						if (text == null || !text.StartsWith(this.RootNamespace + ".", StringComparison.OrdinalIgnoreCase))
						{
							throw Error.MigrationsNamespaceNotUnderRootNamespace(text, this.RootNamespace);
						}
						text = text.Substring(this.RootNamespace.Length + 1);
					}
				}
				migrationScaffolder.Namespace = text;
				ScaffoldedMigration scaffoldedMigration = this.Scaffold(migrationScaffolder);
				AppDomain.CurrentDomain.SetData("result", scaffoldedMigration);
			}

			// Token: 0x06005892 RID: 22674 RVA: 0x0013916B File Offset: 0x0013736B
			protected virtual ScaffoldedMigration Scaffold(MigrationScaffolder scaffolder)
			{
				return scaffolder.Scaffold(this.MigrationName, this.IgnoreChanges);
			}

			// Token: 0x06005893 RID: 22675 RVA: 0x0013917F File Offset: 0x0013737F
			protected override void OverrideConfiguration(DbMigrationsConfiguration configuration)
			{
				base.OverrideConfiguration(configuration);
				if (this.Language == "vb" && configuration.CodeGenerator is CSharpMigrationCodeGenerator)
				{
					configuration.CodeGenerator = new VisualBasicMigrationCodeGenerator();
				}
			}
		}

		// Token: 0x020007D3 RID: 2003
		[Serializable]
		private class InitialCreateScaffoldRunner : ToolingFacade.ScaffoldRunner
		{
			// Token: 0x06005895 RID: 22677 RVA: 0x001391BA File Offset: 0x001373BA
			protected override ScaffoldedMigration Scaffold(MigrationScaffolder scaffolder)
			{
				return scaffolder.ScaffoldInitialCreate();
			}
		}

		// Token: 0x020007D4 RID: 2004
		[Serializable]
		private class GetContextTypesRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x06005897 RID: 22679 RVA: 0x001391CC File Offset: 0x001373CC
			protected override void RunCore()
			{
				List<string> list = (from t in base.LoadContextAssembly().GetAccessibleTypes()
					where !t.IsAbstract && !t.IsGenericType && typeof(DbContext).IsAssignableFrom(t)
					select t.FullName).ToList<string>();
				AppDomain.CurrentDomain.SetData("result", list);
			}
		}

		// Token: 0x020007D5 RID: 2005
		[Serializable]
		private class GetContextTypeRunner : ToolingFacade.BaseRunner
		{
			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06005899 RID: 22681 RVA: 0x0013924A File Offset: 0x0013744A
			// (set) Token: 0x0600589A RID: 22682 RVA: 0x00139252 File Offset: 0x00137452
			public string ContextTypeName { get; set; }

			// Token: 0x0600589B RID: 22683 RVA: 0x0013925C File Offset: 0x0013745C
			protected override void RunCore()
			{
				Type type = new TypeFinder(base.LoadContextAssembly()).FindType(typeof(DbContext), this.ContextTypeName, (IEnumerable<Type> types) => types.Where((Type t) => !typeof(HistoryContext).IsAssignableFrom(t) && !t.IsAbstract && !t.IsGenericType), new Func<string, Exception>(Error.EnableMigrations_NoContext), delegate(string assembly, IEnumerable<Type> types)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Strings.EnableMigrations_MultipleContexts(assembly));
					foreach (Type type2 in types)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append(Strings.EnableMigrationsForContext(type2.FullName));
					}
					return new MigrationsException(stringBuilder.ToString());
				}, new Func<string, string, Exception>(Error.EnableMigrations_NoContextWithName), new Func<string, string, Exception>(Error.EnableMigrations_MultipleContextsWithName));
				AppDomain.CurrentDomain.SetData("result", type.FullName);
			}
		}
	}
}
