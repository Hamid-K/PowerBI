using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc.Interop
{
	// Token: 0x02000715 RID: 1813
	internal static class OdbcUtils
	{
		// Token: 0x06003613 RID: 13843 RVA: 0x000AC489 File Offset: 0x000AA689
		public static int StringLength(string inputString)
		{
			if (inputString == null)
			{
				return 0;
			}
			return inputString.Length;
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000AC496 File Offset: 0x000AA696
		public static short ShortStringLength(string inputString)
		{
			return checked((short)OdbcUtils.StringLength(inputString));
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000AC49F File Offset: 0x000AA69F
		public static bool IsNull(object value)
		{
			return value == null || Convert.IsDBNull(value);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000AC4AC File Offset: 0x000AA6AC
		public static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (IntPtr.Size == 4)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000AC4D4 File Offset: 0x000AA6D4
		public static Exception HandleError(OdbcHandle hrHandle, Odbc32.RetCode retcode)
		{
			Exception ex = OdbcUtils.HandleErrorNoThrow(hrHandle, retcode);
			if (retcode > Odbc32.RetCode.SUCCESS_WITH_INFO)
			{
				throw ex;
			}
			return ex;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000AC4F0 File Offset: 0x000AA6F0
		public static bool HandleErrorCheckNoData(OdbcHandle hrHandle, Odbc32.RetCode retcode)
		{
			Exception ex = OdbcUtils.HandleErrorNoThrowAllowNoData(hrHandle, retcode);
			if (retcode <= Odbc32.RetCode.SUCCESS_WITH_INFO)
			{
				return true;
			}
			if (retcode != Odbc32.RetCode.NO_DATA)
			{
				throw ex;
			}
			return false;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000AC518 File Offset: 0x000AA718
		public static Exception HandleErrorNoThrow(OdbcHandle hrHandle, Odbc32.RetCode retcode)
		{
			if (retcode > Odbc32.RetCode.SUCCESS_WITH_INFO)
			{
				IList<OdbcError> diagErrors = OdbcUtils.GetDiagErrors(hrHandle, retcode);
				return new OdbcException(retcode, diagErrors);
			}
			return null;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000AC53C File Offset: 0x000AA73C
		public static Exception HandleErrorNoThrowAllowNoData(OdbcHandle hrHandle, Odbc32.RetCode retcode)
		{
			if (retcode > Odbc32.RetCode.SUCCESS_WITH_INFO && retcode != Odbc32.RetCode.NO_DATA)
			{
				IList<OdbcError> diagErrors = OdbcUtils.GetDiagErrors(hrHandle, retcode);
				return new OdbcException(retcode, diagErrors);
			}
			return null;
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000AC563 File Offset: 0x000AA763
		public static ISerializedException SerializeException(Exception e)
		{
			SerializedException ex = new SerializedException(2);
			ex["Reason"] = "DataSource.Error";
			ex["Message"] = e.Message;
			return ex;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000AC58C File Offset: 0x000AA78C
		public static bool IsSuccess(Odbc32.RetCode retcode)
		{
			return retcode == Odbc32.RetCode.SUCCESS || retcode == Odbc32.RetCode.SUCCESS_WITH_INFO;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000AC598 File Offset: 0x000AA798
		private static IList<OdbcError> GetDiagErrors(OdbcHandle handle, Odbc32.RetCode retcode)
		{
			if (retcode == Odbc32.RetCode.INVALID_HANDLE)
			{
				throw new InvalidOperationException("Invalid handle.");
			}
			List<OdbcError> list = new List<OdbcError>();
			if (retcode != Odbc32.RetCode.SUCCESS)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				StringBuilder stringBuilder2 = new StringBuilder(6);
				for (int i = 1; i <= 32767; i++)
				{
					OdbcError odbcError;
					retcode = handle.GetDiagnosticRecord(i, stringBuilder, stringBuilder2, out odbcError);
					if (retcode != Odbc32.RetCode.SUCCESS && retcode != Odbc32.RetCode.SUCCESS_WITH_INFO)
					{
						break;
					}
					list.Add(odbcError);
				}
			}
			return list;
		}

		// Token: 0x04001BCD RID: 7117
		public const int DecimalMaxPrecision = 29;

		// Token: 0x04001BCE RID: 7118
		public const int DecimalMaxPrecision28 = 28;

		// Token: 0x04001BCF RID: 7119
		public const int DefaultConnectionTimeout = 15;
	}
}
