using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F4 RID: 4340
	internal static class OleDbCellErrorHandler
	{
		// Token: 0x06007184 RID: 29060 RVA: 0x001863E0 File Offset: 0x001845E0
		public static ISerializedException ConvertError(DBSTATUS status)
		{
			ISerializedException ex = new SerializedException(3);
			ex["Reason"] = "DataSource.Error";
			ex["Detail"] = status.ToString();
			string text;
			if (OleDbCellErrorHandler.errorMessages.TryGetValue(status, out text))
			{
				ex["Message"] = text;
			}
			else
			{
				ex["Message"] = Strings.OleDbCellUnknownStatus;
			}
			return ex;
		}

		// Token: 0x04003ECA RID: 16074
		private static readonly Dictionary<DBSTATUS, string> errorMessages = new Dictionary<DBSTATUS, string>
		{
			{
				DBSTATUS.S_TRUNCATED,
				Strings.OleDbCellTruncated
			},
			{
				DBSTATUS.E_BADACCESSOR,
				Strings.OleDbCellBadAccessor
			},
			{
				DBSTATUS.E_CANTCONVERTVALUE,
				Strings.OleDbCellCantConvertValue
			},
			{
				DBSTATUS.E_CANTCREATE,
				Strings.OleDbCellCantCreate
			},
			{
				DBSTATUS.E_DATAOVERFLOW,
				Strings.OleDbCellDataOverflow
			},
			{
				DBSTATUS.E_SIGNMISMATCH,
				Strings.OleDbCellSignMismatch
			},
			{
				DBSTATUS.E_UNAVAILABLE,
				Strings.OleDbCellUnavailable
			}
		};
	}
}
