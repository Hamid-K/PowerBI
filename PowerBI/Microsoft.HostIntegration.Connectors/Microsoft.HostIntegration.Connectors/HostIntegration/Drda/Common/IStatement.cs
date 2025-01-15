using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000822 RID: 2082
	public interface IStatement
	{
		// Token: 0x060041D6 RID: 16854
		void SetParameterInfo(int index, DrdaParameterInfo parm);

		// Token: 0x060041D7 RID: 16855
		void ClearParameters();

		// Token: 0x060041D8 RID: 16856
		bool DescribeResultSet(ref SqlException exceptInfo);

		// Token: 0x060041D9 RID: 16857
		bool DescribeParameters(ref SqlException exceptInfo);

		// Token: 0x060041DA RID: 16858
		IDataReader GetCurrentDataReader();

		// Token: 0x060041DB RID: 16859
		IDataReader GetCurrentDataReader(bool creaeNewIfNotExist);

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x060041DC RID: 16860
		// (set) Token: 0x060041DD RID: 16861
		bool NeedsToSendParamData { get; set; }

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x060041DE RID: 16862
		// (set) Token: 0x060041DF RID: 16863
		byte[] SplitQrydta { get; set; }

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x060041E0 RID: 16864
		// (set) Token: 0x060041E1 RID: 16865
		int Qryprctyp { get; set; }

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060041E2 RID: 16866
		// (set) Token: 0x060041E3 RID: 16867
		long RowCount { get; set; }

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x060041E4 RID: 16868
		// (set) Token: 0x060041E5 RID: 16869
		int RowLength { get; set; }

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x060041E6 RID: 16870
		// (set) Token: 0x060041E7 RID: 16871
		bool HasData { get; set; }

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x060041E8 RID: 16872
		// (set) Token: 0x060041E9 RID: 16873
		string CommandText { get; set; }

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x060041EA RID: 16874
		// (set) Token: 0x060041EB RID: 16875
		string Attribute { get; set; }

		// Token: 0x060041EC RID: 16876
		void Prepare();

		// Token: 0x060041ED RID: 16877
		int ExecuteNonQuery();

		// Token: 0x060041EE RID: 16878
		IDataReader ExecuteReader(CommandBehavior behavior, int scrollOrientation = 5, long QRYROWNBR = 0L);

		// Token: 0x060041EF RID: 16879
		IDataReader ExecuteReader(string sqlstt, CommandBehavior behavior, int scrollOrientation = 5, long QRYROWNBR = 0L);

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060041F0 RID: 16880
		ISchemaTable SchemaTable { get; }

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060041F1 RID: 16881
		int ResultSetCount { get; }

		// Token: 0x060041F2 RID: 16882
		IResultSet GetResultSet(int i);

		// Token: 0x060041F3 RID: 16883
		IResultSet GetResultSet(ConsistencyToken tkn);

		// Token: 0x060041F4 RID: 16884
		void Close();

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060041F5 RID: 16885
		QueryOptions QueryOptions { get; }

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060041F6 RID: 16886
		// (set) Token: 0x060041F7 RID: 16887
		Ccsid Ccsid { get; set; }

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060041F8 RID: 16888
		bool IsCurrentReaderClosed { get; }

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060041F9 RID: 16889
		// (set) Token: 0x060041FA RID: 16890
		CommandType CommandType { get; set; }

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060041FB RID: 16891
		// (set) Token: 0x060041FC RID: 16892
		int[] Outovr_drdaType { get; set; }

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060041FD RID: 16893
		// (set) Token: 0x060041FE RID: 16894
		IList ExtDtaObjects { get; set; }

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060041FF RID: 16895
		IResultSet CurrentResultSet { get; }

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06004200 RID: 16896
		bool NextResultSet { get; }

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06004201 RID: 16897
		List<IResultSet> ResultSets { get; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06004202 RID: 16898
		SqlParameterCollection Parameters { get; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06004203 RID: 16899
		bool HasOutputParams { get; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06004204 RID: 16900
		bool HasCursor { get; }

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004205 RID: 16901
		bool CursorWithHold { get; }

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06004206 RID: 16902
		bool CursorForUpdate { get; }

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06004207 RID: 16903
		// (set) Token: 0x06004208 RID: 16904
		bool IsFirstCntQry { get; set; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06004209 RID: 16905
		// (set) Token: 0x0600420A RID: 16906
		bool PrpSqlSttFailed { get; set; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x0600420B RID: 16907
		// (set) Token: 0x0600420C RID: 16908
		PackageProcedure PackageProcedure { get; set; }

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x0600420D RID: 16909
		// (set) Token: 0x0600420E RID: 16910
		bool IsParametersDescribed { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x0600420F RID: 16911
		// (set) Token: 0x06004210 RID: 16912
		bool IsResultSetDescribed { get; set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06004211 RID: 16913
		PKGNAMCSN Pkgnamcsn { get; }

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06004212 RID: 16914
		// (set) Token: 0x06004213 RID: 16915
		int FetchCount { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06004214 RID: 16916
		// (set) Token: 0x06004215 RID: 16917
		bool IsSelectStatement { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06004216 RID: 16918
		// (set) Token: 0x06004217 RID: 16919
		List<ColumnMapping> ColumnMappings { get; set; }

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06004218 RID: 16920
		// (set) Token: 0x06004219 RID: 16921
		bool IsInsertStatement { get; set; }

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x0600421A RID: 16922
		// (set) Token: 0x0600421B RID: 16923
		bool IsBulkCopyCompatible { get; set; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x0600421C RID: 16924
		// (set) Token: 0x0600421D RID: 16925
		string TableName { get; set; }
	}
}
