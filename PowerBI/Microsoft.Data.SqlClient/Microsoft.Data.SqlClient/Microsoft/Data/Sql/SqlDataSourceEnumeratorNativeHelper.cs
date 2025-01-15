using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.Sql
{
	// Token: 0x0200015B RID: 347
	internal static class SqlDataSourceEnumeratorNativeHelper
	{
		// Token: 0x06001A57 RID: 6743 RVA: 0x0006BBD8 File Offset: 0x00069DD8
		internal static DataTable GetDataSources()
		{
			new NamedPermissionSet("FullTrust").Demand();
			char[] array = null;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1024;
			int num2 = 0;
			array = new char[num];
			bool flag = true;
			bool flag2 = false;
			IntPtr intPtr = ADP.s_ptrZero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				long timeoutSeconds = TdsParserStaticMethods.GetTimeoutSeconds(30);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = SNINativeMethodWrapper.SNIServerEnumOpen();
					SqlClientEventSource.Log.TryTraceEvent<string, string, string, IntPtr>("<sc.{0}.{1}|INFO> {2} returned handle = {3}.", "SqlDataSourceEnumeratorNativeHelper", "GetDataSources", "SNIServerEnumOpen", intPtr);
				}
				if (intPtr != ADP.s_ptrZero)
				{
					while (flag && !TdsParserStaticMethods.TimeoutHasExpired(timeoutSeconds))
					{
						num2 = SNINativeMethodWrapper.SNIServerEnumRead(intPtr, array, num, out flag);
						SqlClientEventSource.Log.TryTraceEvent<string, string, string, int, bool, int>("<sc.{0}.{1}|INFO> {2} returned 'readlength':{3}, and 'more':{4} with 'bufferSize' of {5}", "SqlDataSourceEnumeratorNativeHelper", "GetDataSources", "SNIServerEnumRead", num2, flag, num);
						if (num2 > num)
						{
							flag2 = true;
							flag = false;
						}
						else if (num2 > 0)
						{
							stringBuilder.Append(array, 0, num2);
						}
					}
				}
			}
			finally
			{
				if (intPtr != ADP.s_ptrZero)
				{
					SNINativeMethodWrapper.SNIServerEnumClose(intPtr);
					SqlClientEventSource.Log.TryTraceEvent<string, string, string>("<sc.{0}.{1}|INFO> {2} called.", "SqlDataSourceEnumeratorNativeHelper", "GetDataSources", "SNIServerEnumClose");
				}
			}
			if (flag2)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, int, int>("<sc.{0}.{1}|ERR> {2} returned bad length, requested buffer {3}, received {4}", "SqlDataSourceEnumeratorNativeHelper", "GetDataSources", "SNIServerEnumRead", num, num2);
				throw ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, new object[] { num2 }), "readLength");
			}
			return SqlDataSourceEnumeratorNativeHelper.ParseServerEnumString(stringBuilder.ToString());
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0006BD60 File Offset: 0x00069F60
		private static DataTable ParseServerEnumString(string serverInstances)
		{
			DataTable dataTable = SqlDataSourceEnumeratorUtil.PrepareDataTable();
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string[] array = serverInstances.Split(new char[1]);
			SqlClientEventSource.Log.TryTraceEvent<string, string, int>("<sc.{0}.{1}|INFO> Number of recieved server instances are {2}", "SqlDataSourceEnumeratorNativeHelper", "ParseServerEnumString", array.Length);
			foreach (string text5 in array)
			{
				string text6 = text5.Trim(new char[1]);
				if (text6.Length != 0)
				{
					foreach (string text7 in text6.Split(new char[] { ';' }))
					{
						if (text == null)
						{
							foreach (string text8 in text7.Split(new char[] { '\\' }))
							{
								if (text == null)
								{
									text = text8;
								}
								else
								{
									text2 = text8;
								}
							}
						}
						else if (text3 == null)
						{
							text3 = text7.Substring(SqlDataSourceEnumeratorUtil.s_clusteredLength);
						}
						else
						{
							text4 = text7.Substring(SqlDataSourceEnumeratorUtil.s_versionLength);
						}
					}
					string text9 = "ServerName='" + text + "'";
					if (!ADP.IsEmpty(text2))
					{
						text9 = text9 + " AND InstanceName='" + text2 + "'";
					}
					if (dataTable.Select(text9).Length == 0)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = text;
						dataRow[1] = text2;
						dataRow[2] = text3;
						dataRow[3] = text4;
						dataTable.Rows.Add(dataRow);
					}
					text = null;
					text2 = null;
					text3 = null;
					text4 = null;
				}
			}
			return dataTable.SetColumnsReadOnly();
		}
	}
}
