using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000813 RID: 2067
	public interface IDatabase
	{
		// Token: 0x06004141 RID: 16705
		void Open();

		// Token: 0x06004142 RID: 16706
		IStatement GetStatement(PKGNAMCSN pkg);

		// Token: 0x06004143 RID: 16707
		IStatement GetStatement(PKGNAMCSN pkg, bool createNew);

		// Token: 0x06004144 RID: 16708
		IStatement GetPreparedStatement(PKGNAMCSN pkg);

		// Token: 0x06004145 RID: 16709
		bool Commit();

		// Token: 0x06004146 RID: 16710
		bool Rollback();

		// Token: 0x06004147 RID: 16711
		SqlConnection Close();

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004148 RID: 16712
		// (set) Token: 0x06004149 RID: 16713
		SqlConnection SqlConnection { get; set; }

		// Token: 0x0600414A RID: 16714
		bool StartTransaction(ITransaction transaction);

		// Token: 0x0600414B RID: 16715
		bool EndTransaction();

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x0600414C RID: 16716
		// (set) Token: 0x0600414D RID: 16717
		bool IsLocalTransaction { get; set; }

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x0600414E RID: 16718
		// (set) Token: 0x0600414F RID: 16719
		bool IsXaTransaction { get; set; }

		// Token: 0x06004150 RID: 16720
		IDbExceptionInfo Convert(Exception ex);

		// Token: 0x06004151 RID: 16721
		SqlExceptionInfo ConvertToExcetionInfo(string db2sqlcode, string db2sqlstate, params object[] args);

		// Token: 0x06004152 RID: 16722
		SqlExceptionInfo ConvertToExcetionInfo(string db2sqlcode, string db2sqlstate, string msg);

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004154 RID: 16724
		// (set) Token: 0x06004153 RID: 16723
		SecurityMechanism SecurityMechanism { get; set; }

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004155 RID: 16725
		// (set) Token: 0x06004156 RID: 16726
		string Name { get; set; }

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004157 RID: 16727
		// (set) Token: 0x06004158 RID: 16728
		int AccessCount { get; set; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004159 RID: 16729
		// (set) Token: 0x0600415A RID: 16730
		string DrdaId { get; set; }

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x0600415B RID: 16731
		// (set) Token: 0x0600415C RID: 16732
		Ccsid Ccsid { get; set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x0600415D RID: 16733
		// (set) Token: 0x0600415E RID: 16734
		EndianType ByteOrder { get; set; }

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600415F RID: 16735
		// (set) Token: 0x06004160 RID: 16736
		bool Trgdftrt { get; set; }

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004161 RID: 16737
		// (set) Token: 0x06004162 RID: 16738
		string UserId { get; set; }

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004163 RID: 16739
		// (set) Token: 0x06004164 RID: 16740
		string Password { get; set; }

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004165 RID: 16741
		// (set) Token: 0x06004166 RID: 16742
		byte[] SecurityToken { get; set; }

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004167 RID: 16743
		// (set) Token: 0x06004168 RID: 16744
		string ConnectionString { get; set; }

		// Token: 0x06004169 RID: 16745
		bool PrepareStatementForStoredProc(string storedProcName, ref IStatement stmt, bool isCNTQRY);

		// Token: 0x0600416A RID: 16746
		void CreateStoredProcOrXMLForPackage(PKGNAMCT pkgnamct, VRSNAM vrsnam, ArrayList _bndsqlsttList, bool pkgReplaceAllowed, Hashtable bndOptions);

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600416B RID: 16747
		// (set) Token: 0x0600416C RID: 16748
		string CommandParameterNameCase { get; set; }

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x0600416D RID: 16749
		// (set) Token: 0x0600416E RID: 16750
		string HostInitiatedAffiliateApplication { get; set; }

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x0600416F RID: 16751
		// (set) Token: 0x06004170 RID: 16752
		string WindowsInitiatedAffiliateApplication { get; set; }

		// Token: 0x06004171 RID: 16753
		int RemoveStoredProcedure(string storeProcName);

		// Token: 0x06004172 RID: 16754
		int RemoveStoredProcedures(string[] storeProcNames);

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004173 RID: 16755
		// (set) Token: 0x06004174 RID: 16756
		string StoredProcedureNameSeparator { get; set; }

		// Token: 0x06004175 RID: 16757
		void Dispose();

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004176 RID: 16758
		// (set) Token: 0x06004177 RID: 16759
		IStatement LastSqlStatement { get; set; }

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004178 RID: 16760
		int SessionID { get; }

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004179 RID: 16761
		// (set) Token: 0x0600417A RID: 16762
		string Typdefnam { get; set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600417B RID: 16763
		// (set) Token: 0x0600417C RID: 16764
		string SqlTransforms { get; set; }

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600417D RID: 16765
		// (set) Token: 0x0600417E RID: 16766
		DatabaseAlias DBAliases { get; set; }

		// Token: 0x0600417F RID: 16767
		void LoadCachedPackageProcedures();

		// Token: 0x06004180 RID: 16768
		PackageProcedure GetPackageProcedure(string procedureName);

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004181 RID: 16769
		int HostCodePageOverride { get; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004182 RID: 16770
		// (set) Token: 0x06004183 RID: 16771
		List<ApplicationEncoding> ApplicationEncodings { get; set; }

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004184 RID: 16772
		// (set) Token: 0x06004185 RID: 16773
		int PkgnamcsnCcsid { get; set; }

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004186 RID: 16774
		// (set) Token: 0x06004187 RID: 16775
		Dictionary<string, string> CollationMappings { get; set; }

		// Token: 0x06004188 RID: 16776
		void LoadIgnoreStandardPackages();

		// Token: 0x06004189 RID: 16777
		bool ShouldPackageBeIgnored(PKGNAMCT pkgnamect);

		// Token: 0x0600418A RID: 16778
		bool ShouldPackageBeIgnored(PKGNAMCSN pkgnamecsn);

		// Token: 0x0600418B RID: 16779
		bool ShouldPackageBeIgnored(string collid, string pkgnam);

		// Token: 0x0600418C RID: 16780
		bool IsLocalRdb(ref string serverName);

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x0600418D RID: 16781
		// (set) Token: 0x0600418E RID: 16782
		Dictionary<SqlSetOptions, string> SqlSets { get; set; }

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600418F RID: 16783
		SqlTransaction SqlTransaction { get; }

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06004190 RID: 16784
		// (set) Token: 0x06004191 RID: 16785
		bool IsMsDrdaAr { get; set; }

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004192 RID: 16786
		bool IsTransactionValid { get; }
	}
}
