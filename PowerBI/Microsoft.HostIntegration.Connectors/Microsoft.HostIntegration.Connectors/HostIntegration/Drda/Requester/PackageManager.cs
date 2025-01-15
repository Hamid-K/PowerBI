using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.CounterTelemetry;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.StaticSqlUtil;
using Microsoft.HostIntegration.Tracing;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200093D RID: 2365
	internal class PackageManager : Manager
	{
		// Token: 0x06004A09 RID: 18953 RVA: 0x0011656C File Offset: 0x0011476C
		static PackageManager()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server\\Data Integration", RegistryKeyPermissionCheck.Default, RegistryRights.QueryValues))
				{
					if (registryKey != null && registryKey.GetValueKind("NumberOfPackages") == RegistryValueKind.DWord)
					{
						object value = registryKey.GetValue("NumberOfPackages");
						if (value != null)
						{
							PackageManager._sectionNum = (int)value;
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			PackageManager.ProcedureSectionNumuber = PackageManager._sectionNum + 1;
			PackageManager.PhantomSectionNumuber = PackageManager._sectionNum + 2;
			PackageManager.CreatePackageAndOptions("AUTOCOMMITTED", "MSNC001           ", PackageManager.PackageToken_NC, IsolationLevel.IsolationNC);
			PackageManager.CreatePackageAndOptions("READ COMMITTED", "MSCS001           ", PackageManager.PackageToken_CS, IsolationLevel.IsolationCS);
			PackageManager.CreatePackageAndOptions("READ UNCOMMITTED", "MSUR001           ", PackageManager.PackageToken_UR, IsolationLevel.IsolationCHG);
			PackageManager.CreatePackageAndOptions("SERIALIZABLE", "MSRS001           ", PackageManager.PackageToken_RS, IsolationLevel.IsolationALL);
			PackageManager.CreatePackageAndOptions("REPEATABLE READ", "MSRR001           ", PackageManager.PackageToken_RR, IsolationLevel.IsolationRR);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x00116750 File Offset: 0x00114950
		private static void CreatePackageAndOptions(string title, string packageId, byte[] packageToken, IsolationLevel isoLevel)
		{
			Options options = new Options();
			Options options2 = new Options();
			foreach (Options options3 in new Options[] { options, options2 })
			{
				options3.DefaultRdbCollection = PackageManager._connectionValue;
				options3.PackageIsolationLevel = (OptionsPackageIsolationLevel)isoLevel;
				options3.BindCheck = true;
				options3.KeepPreparedStatement = OptionsKeepPreparedStatement.Commit;
				options3.BindReplace = true;
				options3.PackageExecuteAuthorization = OptionsPackageExecuteAuthorization.Requester;
				options3.PackageDecimalPrecision = 0;
				options3.BindAuthorizationKeep = true;
				options3.StatementDateFormat = OptionsStatementDateFormat.Iso;
			}
			options2.ReleaseDatabaseResources = OptionsReleaseDatabaseResources.Deallocation;
			string text = "DECLARE SQLCURNC{0} CURSOR WITH HOLD FOR MSSTTNC{1}";
			switch (isoLevel)
			{
			case IsolationLevel.IsolationCHG:
				text = "DECLARE SQLCURUR{0} CURSOR WITH HOLD FOR MSSTTUR{1}";
				break;
			case IsolationLevel.IsolationCS:
				text = "DECLARE SQLCURCS{0} CURSOR WITH HOLD FOR MSSTTCS{1}";
				break;
			case IsolationLevel.IsolationALL:
				text = "DECLARE SQLCURRS{0} CURSOR WITH HOLD FOR MSSTTRS{1}";
				break;
			case IsolationLevel.IsolationRR:
				text = "DECLARE SQLCURRR{0} CURSOR WITH HOLD FOR MSSTTRR{1}";
				break;
			}
			Package[] array2 = new Package[]
			{
				new Package(),
				new Package()
			};
			foreach (Package package in array2)
			{
				package.PackageIdentifier = packageId;
				package.Title = title;
				package.ConsistencyToken = null;
				package.CollectionIdentifier = PackageManager._connectionValue;
				for (int j = 1; j <= PackageManager._sectionNum; j++)
				{
					Section section = new Section();
					section.PackageSectionNumber = j;
					section.Statement = new Statement();
					section.Statement.SqlStatementNumber = 1;
					section.Statement.SqlStatement = string.Format(text, j, j);
					package.Sections.Add(section);
				}
			}
			PackageManager._packageOptionsUdbList.Add(new Tuple<Package, Options, byte[]>(array2[0], options, packageToken));
			PackageManager._packageOptionsDeallocUdbList.Add(new Tuple<Package, Options, byte[]>(array2[0], options2, packageToken));
			Section section2 = new Section();
			section2.PackageSectionNumber = PackageManager.ProcedureSectionNumuber;
			section2.Statement = new Statement();
			section2.Statement.SqlStatementNumber = 1;
			section2.Statement.SqlStatement = "CALL :P USING DESCRIPTOR :P";
			Parameter parameter = new Parameter();
			parameter.Precision = 0;
			parameter.Ccsid = 0;
			parameter.Scale = 0;
			parameter.Length = 128;
			parameter.Type = ParameterTypes.VarChar;
			parameter.Name = "PROC";
			section2.Parameters.Add(parameter);
			parameter = new Parameter();
			parameter.Precision = 0;
			parameter.Ccsid = 0;
			parameter.Scale = 0;
			parameter.Length = 128;
			parameter.Type = ParameterTypes.VarChar;
			parameter.Name = "DSCRPTR";
			section2.Parameters.Add(parameter);
			array2[1].Sections.Add(section2);
			PackageManager._packageOptionsList.Add(new Tuple<Package, Options, byte[]>(array2[1], options, packageToken));
			PackageManager._packageOptionsDeallocList.Add(new Tuple<Package, Options, byte[]>(array2[1], options2, packageToken));
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x00116A29 File Offset: 0x00114C29
		// (set) Token: 0x06004A0C RID: 18956 RVA: 0x00116A30 File Offset: 0x00114C30
		public static int ProcedureSectionNumuber { get; private set; }

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x00116A38 File Offset: 0x00114C38
		// (set) Token: 0x06004A0E RID: 18958 RVA: 0x00116A3F File Offset: 0x00114C3F
		public static int PhantomSectionNumuber { get; private set; }

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x00116A47 File Offset: 0x00114C47
		internal Dictionary<string, string> AliasMappings
		{
			get
			{
				return this._aliasMapping;
			}
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00116A4F File Offset: 0x00114C4F
		public PackageManager(Requester requester)
			: base(requester)
		{
			this._tracePoint = requester.TracePoint;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x00116A6F File Offset: 0x00114C6F
		public override void Initialize()
		{
			base.Initialize();
			this._sectionArray = new BitArray(PackageManager._sectionNum, false);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x00116A88 File Offset: 0x00114C88
		public override void Reset()
		{
			this._tracePoint = this._requester.TracePoint;
			this._sectionArray.SetAll(false);
			this._aliasMapping.Clear();
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00116AB4 File Offset: 0x00114CB4
		public int GetSection()
		{
			for (int i = 0; i < PackageManager._sectionNum; i++)
			{
				if (!this._sectionArray[i])
				{
					this._sectionArray[i] = true;
					return i + 1;
				}
			}
			return -1;
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x00116AF1 File Offset: 0x00114CF1
		public void ReleaseSection(int section)
		{
			if (section <= PackageManager._sectionNum)
			{
				this._sectionArray[section - 1] = false;
			}
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00116B0C File Offset: 0x00114D0C
		public async Task SetupHostPackagesAsync(bool releaseCommit, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._isInSetupHostPackage)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "Re-entry SetupHostPackagesAsync");
				}
			}
			else
			{
				try
				{
					this._isInSetupHostPackage = true;
					List<Tuple<Package, Options, byte[]>> list = (this._requester.IsUdb ? PackageManager._packageOptionsUdbList : PackageManager._packageOptionsList);
					if (!releaseCommit)
					{
						list = (this._requester.IsUdb ? PackageManager._packageOptionsDeallocUdbList : PackageManager._packageOptionsDeallocList);
					}
					SqlStatement sqlStatement = new SqlStatement(this._requester);
					foreach (Tuple<Package, Options, byte[]> tuple in list)
					{
						if (this._requester.HostType == HostType.AS400 || tuple.Item2.PackageIsolationLevel != OptionsPackageIsolationLevel.NoCommit)
						{
							await this.BindPackageAsync(tuple.Item1, tuple.Item2, tuple.Item3, PackageManager.PhantomSectionNumuber, sqlStatement, progress, isAsync, cancellationToken);
						}
					}
					List<Tuple<Package, Options, byte[]>>.Enumerator enumerator = default(List<Tuple<Package, Options, byte[]>>.Enumerator);
					sqlStatement.Reset();
					sqlStatement = null;
				}
				finally
				{
					this._isInSetupHostPackage = false;
				}
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x00116B74 File Offset: 0x00114D74
		internal async Task SetupHostPackagesAsync(IsolationLevel isoLevel, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._isInSetupHostPackage)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "Re-entry SetupHostPackagesAsync");
				}
			}
			else
			{
				try
				{
					this._isInSetupHostPackage = true;
					List<Tuple<Package, Options, byte[]>> list = (this._requester.IsUdb ? PackageManager._packageOptionsUdbList : PackageManager._packageOptionsList);
					SqlStatement sqlStatement = new SqlStatement(this._requester);
					foreach (Tuple<Package, Options, byte[]> tuple in list)
					{
						if (tuple.Item2.PackageIsolationLevel == (OptionsPackageIsolationLevel)isoLevel)
						{
							await this.BindPackageAsync(tuple.Item1, tuple.Item2, tuple.Item3, PackageManager.PhantomSectionNumuber, sqlStatement, null, isAsync, cancellationToken);
							break;
						}
					}
					List<Tuple<Package, Options, byte[]>>.Enumerator enumerator = default(List<Tuple<Package, Options, byte[]>>.Enumerator);
					sqlStatement.Reset();
					sqlStatement = null;
				}
				finally
				{
					this._isInSetupHostPackage = false;
				}
			}
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00116BD4 File Offset: 0x00114DD4
		public async Task RebindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (progress != null)
			{
				progress.Report(RequesterResource.RebindingPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, this._requester.RdbName));
			}
			try
			{
				await this.SubmitRebind(package, options, isAsync, cancellationToken);
				if (this._requester.AutoCommit)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
				}
			}
			catch (Exception ex)
			{
				if (progress != null)
				{
					progress.Report(RequesterResource.RebindingPackageFailed(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, this._requester.RdbName));
					progress.Report(ex.ToString());
				}
				throw ex;
			}
			if (progress != null)
			{
				progress.Report(RequesterResource.RebindedPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, this._requester.RdbName));
			}
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00116C44 File Offset: 0x00114E44
		public async Task CopyPackageAsync(Package package, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (progress != null)
			{
				progress.Report(RequesterResource.CopyingPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, targetRdbName, targetCollectionId));
			}
			try
			{
				await this.SubmitBndcpy(package, options, targetRdbName, targetCollectionId, isAsync, cancellationToken);
				if (this._requester.AutoCommit)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
				}
			}
			catch (Exception ex)
			{
				if (progress != null)
				{
					progress.Report(RequesterResource.CopyingPackageFailed(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, targetRdbName, targetCollectionId));
					progress.Report(ex.ToString());
				}
				throw ex;
			}
			if (progress != null)
			{
				progress.Report(RequesterResource.CopiedPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken, targetRdbName, targetCollectionId));
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x00116CC8 File Offset: 0x00114EC8
		public async Task DropPackageAsync(Package package, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			if (progress != null)
			{
				progress.Report(RequesterResource.DroppingPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken));
			}
			try
			{
				await this.SubmitDrppkg(package, isAsync, cancellationToken);
				if (this._requester.AutoCommit)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
				}
			}
			catch (Exception ex)
			{
				if (progress != null)
				{
					progress.Report(RequesterResource.DroppingPackageFailed(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken));
					progress.Report(ex.ToString());
				}
				throw ex;
			}
			if (progress != null)
			{
				progress.Report(RequesterResource.DroppedPackage(package.CollectionIdentifier, package.PackageIdentifier, package.ConsistencyToken));
			}
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00116D30 File Offset: 0x00114F30
		public async Task BindPackageAsync(Package package, Options options, byte[] overrideToken, int maxSectionNumber, ISqlStatement grantSqlStatement, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			try
			{
				if (progress != null)
				{
					progress.Report(RequesterResource.CreatingPackage(this.GetPackageCollectionId(package), package.PackageIdentifier, package.ConsistencyToken));
				}
				await this.SubmitBgnbnd(package, options, overrideToken, isAsync, cancellationToken);
				foreach (Section section in package.Sections)
				{
					if (progress != null)
					{
						progress.Report(RequesterResource.CreatingSection(section.PackageSectionNumber));
					}
					await this.SubmitBndsqlstt(package, section, overrideToken, isAsync, cancellationToken);
				}
				List<Section>.Enumerator enumerator = default(List<Section>.Enumerator);
				await this.SubmitEndbnd(package, overrideToken, maxSectionNumber, isAsync, cancellationToken);
				if (this._requester.AutoCommit)
				{
					await this._requester.LocalTransactionManager.CommitAsync(isAsync, cancellationToken);
				}
				if (grantSqlStatement != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("GRANT EXECUTE ON PACKAGE ");
					string packageCollectionId = this.GetPackageCollectionId(package);
					if (!string.IsNullOrWhiteSpace(packageCollectionId))
					{
						stringBuilder.Append(packageCollectionId.Trim());
						stringBuilder.Append('.');
					}
					stringBuilder.Append(package.PackageIdentifier.Trim());
					stringBuilder.Append(" TO PUBLIC");
					await ((SqlStatement)grantSqlStatement).InternalExecuteAsync(stringBuilder.ToString(), null, false, false, isAsync, cancellationToken);
				}
				if (progress != null)
				{
					progress.Report(RequesterResource.CreatedPackage(this.GetPackageCollectionId(package), package.PackageIdentifier, package.ConsistencyToken));
				}
			}
			catch (Exception ex)
			{
				if (progress != null)
				{
					progress.Report(RequesterResource.CreatingPackageFailed(this.GetPackageCollectionId(package), package.PackageIdentifier, package.ConsistencyToken));
					progress.Report(ex.ToString());
				}
				throw ex;
			}
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00116DBC File Offset: 0x00114FBC
		private async Task SubmitDrppkg(Package package, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitDrppkg");
			}
			DdmWriter writer = this._requester.ConnectionManager.DdmWriter;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "PackageManager::SubmitDrppkg:  sending DRPPKG...");
				}
				writer.CreateDssRequest(1);
				writer.WriteBeginDdm(CodePoint.DRPPKG);
				writer.WriteScalarString(CodePoint.RDBNAM, this._requester.RdbName.PadRight(18), this._requester.IsUnicodeMgrSupported ? 1208 : 500);
				if (package.PackageIdentifier == "*")
				{
					writer.WriteScalarHeader(CodePoint.PKGIDANY, 0);
				}
				else if (!string.IsNullOrEmpty(package.PackageIdentifier))
				{
					writer.WriteScalarPaddedString(CodePoint.PKGID, package.PackageIdentifier, 18);
				}
				if (package.CollectionIdentifier == "*")
				{
					writer.WriteScalarHeader(CodePoint.RDBCOLIDANY, 0);
				}
				else if (!string.IsNullOrEmpty(package.CollectionIdentifier))
				{
					writer.WriteScalarPaddedString(CodePoint.RDBCOLID, package.CollectionIdentifier, 18);
				}
				if (package.VersionName == "*")
				{
					writer.WriteScalarHeader(CodePoint.VRSNAMANY, 0);
				}
				else if (!string.IsNullOrWhiteSpace(package.VersionName))
				{
					writer.WriteScalar(CodePoint.VRSNAM, package.VersionName);
				}
				writer.WriteEndDdm();
				writer.WriteEndDss();
				await writer.WriteEndChainAsync(0, isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager::SubmitDrppkg: " + ex.ToString());
				}
				if (writer.Offset > 0)
				{
					writer.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this.ProcessReplyMessage(1, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitDrppkg");
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x00116E1C File Offset: 0x0011501C
		private async Task SubmitBgnbnd(Package package, Options options, byte[] overrideToken, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitBgnbnd");
			}
			Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.Processes, DrdaAsProcess.BeginBind);
			DdmWriter writer = this._requester.ConnectionManager.DdmWriter;
			try
			{
				writer.CreateDssRequest(1);
				writer.WriteBeginDdm(CodePoint.BGNBND);
				writer.WriteScalarString(CodePoint.RDBNAM, this._requester.RdbName.PadRight(18), this._requester.IsUnicodeMgrSupported ? 1208 : 500);
				PKGNAMCT pkgnamct = new PKGNAMCT(this._requester.IsUnicodeMgrSupported ? 1208 : 500);
				pkgnamct.Rdbcolid = this.GetPackageCollectionId(package);
				pkgnamct.Pkgcnstkn = overrideToken ?? this.ConvertToPackageToken(package.ConsistencyToken);
				pkgnamct.Pkgid = package.PackageIdentifier;
				pkgnamct.RDBNAM = this._requester.RdbName;
				writer.WriteBeginDdm(CodePoint.PKGNAMCT);
				pkgnamct.Write(writer);
				writer.WriteEndDdm();
				if (string.IsNullOrWhiteSpace(package.VersionName))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Package doesn't have version name when begin binding.");
					}
				}
				else
				{
					writer.WriteScalar(CodePoint.VRSNAM, package.VersionName);
				}
				if (string.IsNullOrWhiteSpace(package.Title))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Package doesn't have title when begin binding.");
					}
				}
				else
				{
					writer.WriteScalar(CodePoint.TITLE, package.Title);
				}
				this.WriteOptions(options, CodePoint.BGNBND);
				writer.WriteEndDdm();
				writer.WriteEndDss();
				await writer.WriteEndChainAsync(0, isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "SqlManager::SubmitBgnbnd: " + ex.ToString());
				}
				if (writer.Offset > 0)
				{
					writer.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this.ProcessReplyMessage(1, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitBgnbnd");
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x00116E8C File Offset: 0x0011508C
		private async Task SubmitBndsqlstt(Package package, Section section, byte[] overrideToken, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitBndsqlstt");
			}
			DdmWriter writer = this._requester.ConnectionManager.DdmWriter;
			BNDSQLSTT bndsqlstt = new BNDSQLSTT(null, this._requester.SqlManager.Level, this._requester.TypeDefinitionName, this._requester.IsUnicodeMgrSupported ? 1208 : 500);
			base.InitializeCodepoint(bndsqlstt);
			bndsqlstt.Pkgnamcsn = new PKGNAMCSN(this._requester.IsUnicodeMgrSupported ? 1208 : 500);
			bndsqlstt.Pkgnamcsn.Rdbcolid = this.GetPackageCollectionId(package);
			bndsqlstt.Pkgnamcsn.Pkgcnstkn = new ConsistencyToken(overrideToken ?? this.ConvertToPackageToken(package.ConsistencyToken));
			bndsqlstt.Pkgnamcsn.Pkgid = package.PackageIdentifier;
			bndsqlstt.Pkgnamcsn.RDBNAM = this._requester.RdbName;
			bndsqlstt.Pkgnamcsn.Pkgsn = section.PackageSectionNumber;
			bndsqlstt.Rdbnam = new RDBNAM(this._requester.RdbName);
			bndsqlstt.Sqlstt = null;
			bndsqlstt.Sqlsttnbr = section.Statement.SqlStatementNumber;
			bndsqlstt.IsSqlStatementAssumptionClassified = section.Statement.SqlStatementAssumptions;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "PackageManager::SubmitBndsqlstt:  sending BNDSQLSTT...");
				}
				await bndsqlstt.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 80, isAsync, cancellationToken);
				SQLSTT sqlstt = new SQLSTT(null, this._level, this._requester.TypeDefinitionName);
				base.InitializeCodepoint(sqlstt);
				sqlstt.AutoFlush = false;
				sqlstt.Sqlstt = section.Statement.SqlStatement;
				byte b = 80;
				if (section.Parameters.Count == 0)
				{
					b = 0;
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "PackageManager::SubmitBndsqlstt:  sending SQLSTT... " + sqlstt.Sqlstt);
				}
				await sqlstt.WriteObjectDssAsync(this._requester.ConnectionManager.DdmWriter, 1, b, isAsync, cancellationToken);
				if (section.Parameters.Count > 0)
				{
					writer.CreateDssObject(1);
					writer.WriteBeginDdm(CodePoint.SQLSTTVRB);
					writer.WriteInt16(section.Parameters.Count);
					foreach (Parameter parameter in section.Parameters)
					{
						writer.WriteInt16((int)parameter.Precision);
						writer.WriteInt16((int)parameter.Scale);
						writer.WriteInt64((long)parameter.Length);
						SQLType sqltype = (SQLType)Enum.Parse(typeof(SQLType), parameter.Type.ToString());
						if (parameter.Nullable)
						{
							sqltype++;
						}
						writer.WriteInt16((int)sqltype);
						writer.WriteInt16((int)parameter.Ccsid);
						if (this._requester.SqlManager.Level >= 9)
						{
							writer.WriteInt64(0L);
						}
						if (string.IsNullOrEmpty(parameter.Name))
						{
							writer.WriteInt32(0);
						}
						else
						{
							this._requester.ConnectionManager.DdmWriter.WriteInt32(parameter.Name.Length, EndianType.BigEndian);
							writer.WriteStringSBCS(parameter.Name);
						}
						writer.WriteInt32(0);
						if (this._requester.SqlManager.Level >= 11)
						{
							writer.WriteInt16(0);
						}
						writer.WriteByte(255);
					}
					writer.WriteEndDdm();
					writer.WriteEndDss();
					await this._requester.ConnectionManager.DdmWriter.WriteEndChainAsync(0, isAsync, cancellationToken);
				}
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager::SubmitBndsqlstt: " + ex.ToString());
				}
				if (writer.Offset > 0)
				{
					writer.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
			await this.ProcessReplyMessage(1, true, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitBndsqlstt");
			}
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00116EFC File Offset: 0x001150FC
		private async Task SubmitEndbnd(Package package, byte[] overrideToken, int maxSectionNumber, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitEndbnd");
			}
			DdmWriter ddmWriter = this._requester.ConnectionManager.DdmWriter;
			ENDBND endbnd = new ENDBND(null, this._requester.SqlManager.Level, this._requester.TypeDefinitionName);
			base.InitializeCodepoint(endbnd);
			endbnd.Rdbnam = new RDBNAM(this._requester.RdbName);
			endbnd.Pkgnamct = new PKGNAMCT(this._requester.IsUnicodeMgrSupported ? 1208 : 500);
			endbnd.Pkgnamct.Rdbcolid = this.GetPackageCollectionId(package);
			endbnd.Pkgnamct.Pkgcnstkn = overrideToken ?? this.ConvertToPackageToken(package.ConsistencyToken);
			endbnd.Pkgnamct.Pkgid = package.PackageIdentifier;
			endbnd.Pkgnamct.RDBNAM = this._requester.RdbName;
			endbnd.Maxsctnbr = maxSectionNumber;
			try
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "PackageManager::SubmitEndbnd:  sending ENDBND...");
				}
				await endbnd.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager::SubmitEndbnd: " + ex.ToString());
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this.ProcessReplyMessage(1, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitEndbnd");
			}
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x00116F6C File Offset: 0x0011516C
		private async Task SubmitBndcpy(Package package, Options options, string targetRdbName, string targetCollectionId, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitBndcpy");
			}
			DdmWriter writer = this._requester.ConnectionManager.DdmWriter;
			try
			{
				writer.CreateDssRequest(1);
				writer.WriteBeginDdm(CodePoint.BNDCPY);
				PKGNAM pkgnam = new PKGNAM(writer.Ccsid._ccsidsbc);
				pkgnam.Rdbcolid = targetCollectionId;
				pkgnam.Pkgid = package.PackageIdentifier;
				pkgnam.RDBNAM = targetRdbName;
				writer.WriteBeginDdm(CodePoint.PKGNAM);
				pkgnam.Write(writer);
				writer.WriteEndDdm();
				if (string.IsNullOrWhiteSpace(package.VersionName))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Source package doesn't have version name when copy binding.");
					}
				}
				else
				{
					writer.WriteScalar(CodePoint.VRSNAM, package.VersionName);
				}
				if (string.IsNullOrWhiteSpace(package.Title))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Package doesn't have title when copy binding.");
					}
				}
				else
				{
					writer.WriteScalar(CodePoint.TITLE, package.Title);
				}
				if (string.IsNullOrWhiteSpace(package.CollectionIdentifier))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "SourceCollectionId is empty when bndcpy.");
					}
				}
				else
				{
					writer.WriteScalarPaddedString(CodePoint.RDBSRCCOLID, package.CollectionIdentifier, 18);
				}
				this.WriteOptions(options, CodePoint.BNDCPY);
				writer.WriteEndDdm();
				writer.WriteEndDss();
				await writer.WriteEndChainAsync(0, isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager::SubmitBndcpy: " + ex.ToString());
				}
				if (writer.Offset > 0)
				{
					writer.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this.ProcessReplyMessage(1, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitBndcpy");
			}
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00116FE4 File Offset: 0x001151E4
		private async Task SubmitRebind(Package package, Options options, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter PackageManager::SubmitRebind");
			}
			DdmWriter writer = this._requester.ConnectionManager.DdmWriter;
			try
			{
				writer.CreateDssRequest(1);
				writer.WriteBeginDdm(CodePoint.REBIND);
				PKGNAM pkgnam = new PKGNAM(writer.Ccsid._ccsidsbc);
				pkgnam.Rdbcolid = package.CollectionIdentifier;
				pkgnam.Pkgid = package.PackageIdentifier;
				pkgnam.RDBNAM = this._requester.RdbName;
				writer.WriteBeginDdm(CodePoint.PKGNAM);
				pkgnam.Write(writer);
				writer.WriteEndDdm();
				writer.WriteScalarString(CodePoint.RDBNAM, this._requester.RdbName.PadRight(18), this._requester.IsUnicodeMgrSupported ? 1208 : 500);
				if (string.IsNullOrWhiteSpace(package.VersionName))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Package doesn't have version name when rebinding.");
					}
				}
				else
				{
					writer.WriteScalar(CodePoint.VRSNAM, package.VersionName);
				}
				this.WriteOptions(options, CodePoint.REBIND);
				writer.WriteEndDdm();
				writer.WriteEndDss();
				await writer.WriteEndChainAsync(0, isAsync, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager::SubmitRebind: " + ex.ToString());
				}
				if (writer.Offset > 0)
				{
					writer.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			await this.ProcessReplyMessage(1, false, isAsync, cancellationToken);
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Exit PackageManager::SubmitRebind");
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0011704C File Offset: 0x0011524C
		private void WriteOptions(Options options, CodePoint codePoint)
		{
			DdmWriter ddmWriter = this._requester.ConnectionManager.DdmWriter;
			if (string.IsNullOrWhiteSpace(options.DefaultRdbCollection))
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Options doesn't have DFTRDBCOL.");
				}
			}
			else if (options.DefaultRdbCollection == PackageManager._connectionValue)
			{
				ddmWriter.WriteScalar(CodePoint.DFTRDBCOL, this._requester.ConnectionInfo[13]);
			}
			else
			{
				ddmWriter.WriteScalar(CodePoint.DFTRDBCOL, options.DefaultRdbCollection);
			}
			if (string.IsNullOrWhiteSpace(options.PackageOwnerIdentifier))
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Debug))
				{
					this._tracePoint.Trace(TraceFlags.Debug, "Options doesn't have PKGOWNID.");
				}
			}
			else
			{
				ddmWriter.WriteScalar(CodePoint.PKGOWNID, options.PackageOwnerIdentifier);
			}
			if (codePoint != CodePoint.REBIND)
			{
				if (!options.BindReplace)
				{
					ddmWriter.WriteScalar2Bytes(CodePoint.PKGRPLOPT, 9248);
				}
				if (string.IsNullOrWhiteSpace(options.BindReplaceVersion))
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, "Options doesn't have PKGRPLVRS.");
					}
				}
				else
				{
					ddmWriter.WriteScalar(CodePoint.PKGRPLVRS, options.BindReplaceVersion);
				}
			}
			if (options.BindCheck)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.BNDCHKEXS, 9244);
			}
			if (options.BindExplain != OptionsBindExplain.ExplainNone)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.BNDEXPOPT, (int)options.BindExplain);
			}
			if (options.ParallelProcessDegree != 1)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.DGRIOPRL, options.ParallelProcessDegree);
			}
			if (options.PackageExecuteAuthorization != OptionsPackageExecuteAuthorization.Requester)
			{
				ddmWriter.WriteScalar1Byte(CodePoint.PKGATHRUL, (int)options.PackageExecuteAuthorization);
			}
			ddmWriter.WriteScalar2Bytes(CodePoint.PKGISOLVL, (int)options.PackageIsolationLevel);
			if (options.ReleaseDatabaseResources != OptionsReleaseDatabaseResources.Commit)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.RDBRLSOPT, (int)options.ReleaseDatabaseResources);
			}
			if (codePoint == CodePoint.REBIND)
			{
				return;
			}
			if (options.BindAllowErrors != OptionsBindAllowErrors.No)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.BNDCRTCTL, (int)options.BindAllowErrors);
			}
			if (options.PackageDecimalPrecision > 0)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.DECPRC, options.PackageDecimalPrecision);
			}
			Ccsid ccsid = ((ddmWriter.Ccsid == null) ? PackageManager._defaultCcsid : ddmWriter.Ccsid);
			if (options.PackageCcsidDbc != ccsid._ccsiddbc || options.PackageCcsidMbc != ccsid._ccsidmbc || options.PackageCcsidSbc != ccsid._ccsidsbc)
			{
				ddmWriter.WriteBeginDdm(CodePoint.PKGDFTCC);
				if (options.PackageCcsidDbc != ccsid._ccsiddbc)
				{
					ddmWriter.WriteScalar2Bytes(CodePoint.CCSIDDBC, options.PackageCcsidDbc);
				}
				if (options.PackageCcsidMbc != ccsid._ccsidmbc)
				{
					ddmWriter.WriteScalar2Bytes(CodePoint.CCSIDMBC, options.PackageCcsidMbc);
				}
				if (options.PackageCcsidSbc != ccsid._ccsidsbc)
				{
					ddmWriter.WriteScalar2Bytes(CodePoint.CCSIDSBC, options.PackageCcsidSbc);
				}
				ddmWriter.WriteEndDdm();
			}
			if (options.PackageCharacterSubtype != OptionsPackageCharacterSubtype.Default)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.PKGDFTCST, (int)options.PackageCharacterSubtype);
			}
			if (!options.BindAuthorizationKeep)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.PKGATHOPT, 9252);
			}
			if (options.KeepPreparedStatement != OptionsKeepPreparedStatement.None && !this._requester.IsUdb && !this._requester.IsDb2Gateway)
			{
				ddmWriter.WriteScalar1Byte(CodePoint.PRPSTTKP, (int)options.KeepPreparedStatement);
			}
			ddmWriter.WriteScalar2Bytes(CodePoint.QRYBLKCTL, 9239);
			if (options.StatementDateFormat != OptionsStatementDateFormat.Iso)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.STTDATFMT, (int)options.StatementDateFormat);
			}
			if (options.StatementDecimalDelimiter != OptionsStatementDecimalDelimiter.System)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.STTDECDEL, (int)options.StatementDecimalDelimiter);
			}
			if (options.StatementStringDelimiter != OptionsStatementStringDelimiter.System)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.STTSTRDEL, (int)options.StatementStringDelimiter);
			}
			if (options.StatementTimeFormat != OptionsStatementTimeFormat.Iso)
			{
				ddmWriter.WriteScalar2Bytes(CodePoint.STTTIMFMT, (int)options.StatementTimeFormat);
			}
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x001173F0 File Offset: 0x001155F0
		private async Task ProcessReplyMessage(int correlationId, bool needRollbackIfFailed, bool isAsync, CancellationToken cancellationToken)
		{
			PackageManager.<>c__DisplayClass51_0 CS$<>8__locals1 = new PackageManager.<>c__DisplayClass51_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.isAsync = isAsync;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			SQLCAGRP sqlcagrp = null;
			Manager.ReplyInfo replyInfo = null;
			try
			{
				do
				{
					CodePoint codePoint = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
					CodePoint currentCP = codePoint;
					if (this._tracePoint.IsEnabled(TraceFlags.Information))
					{
						this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
					}
					if (currentCP == CodePoint.SQLCARD)
					{
						sqlcagrp = new SQLCAGRP(null, this._requester.SqlManager.Level);
						base.InitializeCodepoint(sqlcagrp);
						await sqlcagrp.ReadAsync(this._requester.ConnectionManager.DdmReader, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
						if (sqlcagrp.IsNull)
						{
							sqlcagrp = null;
						}
					}
					else
					{
						Manager.ReplyInfo replyInfo2 = await base.ProcessReplyCodepointsAsync(currentCP, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Warning))
						{
							this._tracePoint.Trace(TraceFlags.Warning, "PackageManager: Read unexpected CodePoint: " + currentCP.ToString());
						}
						if (replyInfo2 != null)
						{
							replyInfo = replyInfo2;
						}
					}
				}
				while (base.NeedReadMoreDdmCodepoint(correlationId));
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "PackageManager: " + ex.ToString());
				}
				throw this._requester.MakeException(ex.Message, "HY000", -343);
			}
			Func<Task> func = null;
			if (needRollbackIfFailed)
			{
				func = delegate
				{
					PackageManager.<>c__DisplayClass51_0.<<ProcessReplyMessage>b__0>d <<ProcessReplyMessage>b__0>d;
					<<ProcessReplyMessage>b__0>d.<>4__this = CS$<>8__locals1;
					<<ProcessReplyMessage>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<ProcessReplyMessage>b__0>d.<>1__state = -1;
					AsyncTaskMethodBuilder <>t__builder = <<ProcessReplyMessage>b__0>d.<>t__builder;
					<>t__builder.Start<PackageManager.<>c__DisplayClass51_0.<<ProcessReplyMessage>b__0>d>(ref <<ProcessReplyMessage>b__0>d);
					return <<ProcessReplyMessage>b__0>d.<>t__builder.Task;
				};
			}
			await base.ProcessSqlCa(sqlcagrp, null, func, CS$<>8__locals1.isAsync, CS$<>8__locals1.cancellationToken);
			base.ProcessReplyInfo(null, replyInfo, "PackageManager");
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00117456 File Offset: 0x00115656
		private string GetPackageCollectionId(Package package)
		{
			if (package.CollectionIdentifier == PackageManager._connectionValue)
			{
				return this._requester.PackageCollection;
			}
			return package.CollectionIdentifier;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0011747C File Offset: 0x0011567C
		internal byte[] ConvertToPackageToken(string tokenString)
		{
			int num = 0;
			byte[] array = new byte[8];
			this._requester.ConnectionManager.DdmWriter.Converter.PackString(tokenString, 1208, array, ref num, 8, DrdaTypes.DRDA_TYPE_CHAR);
			return array;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x001174BC File Offset: 0x001156BC
		internal void BuildAliasMapping(XmlReader packageData)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "PackageManager cleared alias mappings.");
			}
			this._aliasMapping.Clear();
			if (packageData == null)
			{
				return;
			}
			StaticSql staticSql = new StaticSql();
			staticSql.LoadPackage(packageData, PackageFormat.v85);
			foreach (Package package in staticSql.Packages)
			{
				foreach (Section section in package.Sections)
				{
					if (!string.IsNullOrWhiteSpace(section.PackageSectionAlias))
					{
						string text = string.Concat(new string[]
						{
							package.CollectionIdentifier,
							".",
							package.PackageIdentifier,
							".",
							package.ConsistencyToken,
							".",
							section.PackageSectionNumber.ToString()
						});
						if (this._tracePoint.IsEnabled(TraceFlags.Debug))
						{
							this._tracePoint.Trace(TraceFlags.Debug, "PackageManager is adding package section alias: " + section.PackageSectionAlias + " -- " + text);
						}
						this._aliasMapping.Add(section.PackageSectionAlias.ToUpperInvariant(), text);
					}
				}
			}
		}

		// Token: 0x0400380E RID: 14350
		private static int _sectionNum = 128;

		// Token: 0x0400380F RID: 14351
		private static Ccsid _defaultCcsid = new Ccsid();

		// Token: 0x04003810 RID: 14352
		private static string _connectionValue = "<Connection>";

		// Token: 0x04003811 RID: 14353
		private static List<Tuple<Package, Options, byte[]>> _packageOptionsList = new List<Tuple<Package, Options, byte[]>>();

		// Token: 0x04003812 RID: 14354
		private static List<Tuple<Package, Options, byte[]>> _packageOptionsUdbList = new List<Tuple<Package, Options, byte[]>>();

		// Token: 0x04003813 RID: 14355
		private static List<Tuple<Package, Options, byte[]>> _packageOptionsDeallocList = new List<Tuple<Package, Options, byte[]>>();

		// Token: 0x04003814 RID: 14356
		private static List<Tuple<Package, Options, byte[]>> _packageOptionsDeallocUdbList = new List<Tuple<Package, Options, byte[]>>();

		// Token: 0x04003815 RID: 14357
		private BitArray _sectionArray;

		// Token: 0x04003816 RID: 14358
		private Dictionary<string, string> _aliasMapping = new Dictionary<string, string>();

		// Token: 0x04003819 RID: 14361
		public static readonly byte[] PackageToken_NC = new byte[] { 245, 240, 240, 240, 240, 240, 240, 241 };

		// Token: 0x0400381A RID: 14362
		public static readonly byte[] PackageToken_UR = new byte[] { 245, 240, 240, 240, 240, 240, 240, 242 };

		// Token: 0x0400381B RID: 14363
		public static readonly byte[] PackageToken_CS = new byte[] { 245, 240, 240, 240, 240, 240, 240, 243 };

		// Token: 0x0400381C RID: 14364
		public static readonly byte[] PackageToken_RS = new byte[] { 245, 240, 240, 240, 240, 240, 240, 244 };

		// Token: 0x0400381D RID: 14365
		public static readonly byte[] PackageToken_RR = new byte[] { 245, 240, 240, 240, 240, 240, 240, 245 };

		// Token: 0x0400381E RID: 14366
		public const string PackageIdRR = "MSRR001           ";

		// Token: 0x0400381F RID: 14367
		public const string PackageIdRS = "MSRS001           ";

		// Token: 0x04003820 RID: 14368
		public const string PackageIdUR = "MSUR001           ";

		// Token: 0x04003821 RID: 14369
		public const string PackageIdNC = "MSNC001           ";

		// Token: 0x04003822 RID: 14370
		public const string PackageIdCS = "MSCS001           ";

		// Token: 0x04003823 RID: 14371
		public const string PackageIdInformix = "SYSSN500          ";

		// Token: 0x04003824 RID: 14372
		private bool _isInSetupHostPackage;
	}
}
