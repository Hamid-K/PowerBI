using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A58 RID: 2648
	public class StaticSql
	{
		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x0014FC8E File Offset: 0x0014DE8E
		// (set) Token: 0x060052A1 RID: 21153 RVA: 0x0014FC96 File Offset: 0x0014DE96
		public List<Package> Packages { get; private set; }

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x0014FC9F File Offset: 0x0014DE9F
		// (set) Token: 0x060052A3 RID: 21155 RVA: 0x0014FCA7 File Offset: 0x0014DEA7
		public Options Options { get; private set; }

		// Token: 0x060052A4 RID: 21156 RVA: 0x0014FCB0 File Offset: 0x0014DEB0
		private StaticSql(StaticSql staticSql)
		{
			this.Packages = new List<Package>();
			this._staticSql = staticSql;
			this.Sync(true);
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x0014FCD1 File Offset: 0x0014DED1
		public StaticSql()
		{
			this.Packages = new List<Package>();
			this._staticSql = new StaticSql();
			this.Options = new Options(this._staticSql.Options);
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0014FD08 File Offset: 0x0014DF08
		public void Drop(DrdaConnection connection, IProgress<string> progress)
		{
			this.InternalDropAsync(connection, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0014FD30 File Offset: 0x0014DF30
		public async Task DropAsync(DrdaConnection connection, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalDropAsync(connection, progress, true, cancellationToken);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0014FD90 File Offset: 0x0014DF90
		private async Task InternalDropAsync(DrdaConnection connection, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			foreach (Package package in this.Packages)
			{
				await package.InternalDropAsync(connection, progress, isAsync, cancellationToken);
			}
			List<Package>.Enumerator enumerator = default(List<Package>.Enumerator);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0014FDF8 File Offset: 0x0014DFF8
		public void Create(DrdaConnection connection, IProgress<string> progress)
		{
			this.InternalCreateAsync(connection, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0014FE20 File Offset: 0x0014E020
		public async Task CreateAsync(DrdaConnection connection, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this.InternalCreateAsync(connection, progress, true, cancellationToken);
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0014FE80 File Offset: 0x0014E080
		private async Task InternalCreateAsync(DrdaConnection connection, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			foreach (Package package in this.Packages)
			{
				await package.InternalBindAsync(connection, this.Options, progress, isAsync, cancellationToken);
			}
			List<Package>.Enumerator enumerator = default(List<Package>.Enumerator);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0014FEE6 File Offset: 0x0014E0E6
		public static StaticSql LoadPackages(XmlReader xmlReader)
		{
			StaticSql staticSql = new StaticSql();
			StaticSql.InitializeOptions(staticSql.Options);
			staticSql.LoadPackage(xmlReader);
			return new StaticSql(staticSql);
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x0014FF04 File Offset: 0x0014E104
		public static StaticSql LoadPackages(XmlReader xmlReader, PackageFormat packageFormart)
		{
			StaticSql staticSql = new StaticSql();
			StaticSql.InitializeOptions(staticSql.Options);
			staticSql.LoadPackage(xmlReader, (PackageFormat)packageFormart);
			return new StaticSql(staticSql);
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0014FF23 File Offset: 0x0014E123
		private static void InitializeOptions(Options options)
		{
			options.BindCheck = true;
			options.KeepPreparedStatement = OptionsKeepPreparedStatement.Commit;
			options.BindReplace = true;
			options.PackageExecuteAuthorization = OptionsPackageExecuteAuthorization.Requester;
			options.PackageDecimalPrecision = 0;
			options.BindAuthorizationKeep = true;
			options.StatementDateFormat = OptionsStatementDateFormat.Iso;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0014FF5E File Offset: 0x0014E15E
		internal StaticSql ToStaticSql()
		{
			this.Sync(false);
			return this._staticSql;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0014FF70 File Offset: 0x0014E170
		private void Sync(bool isRead)
		{
			if (isRead)
			{
				this.Packages.Clear();
				foreach (Package package in this._staticSql.Packages)
				{
					this.Packages.Add(new Package(package));
				}
				this.Options = new Options(this._staticSql.Options);
				return;
			}
			this._staticSql.Packages.Clear();
			foreach (Package package2 in this.Packages)
			{
				this._staticSql.Packages.Add(package2.ToPackage());
			}
		}

		// Token: 0x0400412E RID: 16686
		private StaticSql _staticSql;
	}
}
