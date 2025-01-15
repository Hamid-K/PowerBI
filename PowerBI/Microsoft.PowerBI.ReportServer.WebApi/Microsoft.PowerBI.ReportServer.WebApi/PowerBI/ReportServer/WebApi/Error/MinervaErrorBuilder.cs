using System;
using Microsoft.PowerBI.ReportServer.AsServer;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.Error
{
	// Token: 0x02000039 RID: 57
	public class MinervaErrorBuilder
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00006EB0 File Offset: 0x000050B0
		public static JObject FromAsConnectionException(AsConnectionException ex, string requestId)
		{
			string rs_MINERVA_ERROR_CODE_GENERAL = MinervaErrorBuilder.RS_MINERVA_ERROR_CODE_GENERAL;
			string text = MinervaErrorBuilder.RS_MINERVA_ERROR_CODE_DETAIL_ASUNKNOWN;
			switch (ex.ErrorCode)
			{
			case AsConnectionExceptionErrorCode.Unknown:
				text = MinervaErrorBuilder.RS_MINERVA_ERROR_CODE_DETAIL_ASUNKNOWN;
				break;
			case AsConnectionExceptionErrorCode.OutOfMemory:
				text = MinervaErrorBuilder.RS_MINERVA_ERROR_CODE_DETAIL_NOMEMORY;
				break;
			case AsConnectionExceptionErrorCode.LostConnection:
				text = MinervaErrorBuilder.RS_MINERVA_ERROR_CODE_DETAIL_ASCONNECTION;
				break;
			}
			return MinervaErrorBuilder.FromErrorCode(rs_MINERVA_ERROR_CODE_GENERAL, text, requestId);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006F04 File Offset: 0x00005104
		public static JObject FromErrorCode(string errorCode, string detailCode, string requestId)
		{
			return JObject.FromObject(new MinervaErrorBuilder.ExplorationProxyErrorObject
			{
				requestId = requestId,
				error = new MinervaErrorBuilder.PowerBIApiErrorObject
				{
					code = errorCode,
					message = "",
					target = "",
					details = new string[] { detailCode }
				}
			});
		}

		// Token: 0x040000A3 RID: 163
		private static string RS_MINERVA_ERROR_CODE_GENERAL = "RS_DataModel_Failure_Generic";

		// Token: 0x040000A4 RID: 164
		private static string RS_MINERVA_ERROR_CODE_DETAIL_NOMEMORY = "RS_DataModel_Failure_NoMemory";

		// Token: 0x040000A5 RID: 165
		private static string RS_MINERVA_ERROR_CODE_DETAIL_ASUNKNOWN = "RS_DataModel_Failure_ASUnknown";

		// Token: 0x040000A6 RID: 166
		private static string RS_MINERVA_ERROR_CODE_DETAIL_ASCONNECTION = "RS_DataModel_Failure_ASConnection";

		// Token: 0x0200006C RID: 108
		internal class PowerBIApiErrorObject
		{
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C472 File Offset: 0x0000A672
			// (set) Token: 0x060001AA RID: 426 RVA: 0x0000C47A File Offset: 0x0000A67A
			public string code { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060001AB RID: 427 RVA: 0x0000C483 File Offset: 0x0000A683
			// (set) Token: 0x060001AC RID: 428 RVA: 0x0000C48B File Offset: 0x0000A68B
			public string message { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001AD RID: 429 RVA: 0x0000C494 File Offset: 0x0000A694
			// (set) Token: 0x060001AE RID: 430 RVA: 0x0000C49C File Offset: 0x0000A69C
			public string target { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001AF RID: 431 RVA: 0x0000C4A5 File Offset: 0x0000A6A5
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000C4AD File Offset: 0x0000A6AD
			public string[] details { get; set; }
		}

		// Token: 0x0200006D RID: 109
		internal class ExplorationProxyErrorObject
		{
			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
			// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000C4BE File Offset: 0x0000A6BE
			public string requestId { get; set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000C4C7 File Offset: 0x0000A6C7
			// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000C4CF File Offset: 0x0000A6CF
			public MinervaErrorBuilder.PowerBIApiErrorObject error { get; set; }
		}
	}
}
