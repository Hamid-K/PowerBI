using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000046 RID: 70
	internal static class DatabaseErrors
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00006264 File Offset: 0x00004464
		public static int GetErrorCode(SqlException ex)
		{
			int num = ex.Number;
			if (num == 50000)
			{
				MatchCollection matchCollection = DatabaseErrors.s_exceptionParser.Matches(ex.Message);
				if (matchCollection.Count > 0)
				{
					Match match = matchCollection[matchCollection.Count - 1];
					if (match.Success)
					{
						num = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					}
					else
					{
						TraceSourceBase<StorageTrace>.Tracer.TraceWarning("Custom error raised with malformed last match: {0}", new object[] { ex.Message });
					}
				}
				else
				{
					TraceSourceBase<StorageTrace>.Tracer.TraceWarning("Custom error raised with malformed format: {0}", new object[] { ex.Message });
				}
			}
			return num;
		}

		// Token: 0x040000C3 RID: 195
		private static readonly Regex s_exceptionParser = new Regex("Error (\\d+), Level (\\d+), State (\\d+)", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040000C4 RID: 196
		public const int StoredProcedureError = 50000;
	}
}
