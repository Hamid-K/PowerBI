using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal class SapBwException : DbException
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005517 File Offset: 0x00003717
		public SapBwException(string message)
			: base(message)
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005520 File Offset: 0x00003720
		public SapBwException(string message, Exception ex)
			: base(message, ex)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000552C File Offset: 0x0000372C
		public static SapBwException FromException(Exception ex)
		{
			SapBwException ex2 = new SapBwException(ex.Message, ex);
			RfcLogonException ex3 = ex as RfcLogonException;
			if (ex3 != null)
			{
				ex2.Data["ExceptionKind"] = "Authorization";
				ex2.Data["ErrorCode"] = ex3.ErrorCode;
				return ex2;
			}
			RfcAbapClassicException ex4 = ex as RfcAbapClassicException;
			if (ex4 != null)
			{
				SapBwException.SetExceptionData(ex2.Data, "AbapMessageClass", new string[] { ex4.AbapMessageClass });
				SapBwException.SetExceptionData(ex2.Data, "AbapMessageNumber", new string[] { ex4.AbapMessageNumber });
				SapBwException.SetExceptionData(ex2.Data, "AbapMessageParameters", ex4.AbapMessageParameters);
				SapBwException.SetExceptionData(ex2.Data, "AbapMessageType", new string[] { ex4.AbapMessageType.ToString() });
				SapBwException.SetExceptionData(ex2.Data, "AbapT100Message", new string[] { ex4.AbapT100Message });
				SapBwException.SetExceptionData(ex2.Data, "Key", new string[] { ex4.Key });
			}
			RfcBaseException ex5 = ex as RfcBaseException;
			if (ex5 != null)
			{
				if (ex5.ErrorCode == RfcBaseException.NCO_ERROR_LOGON_FAILURE)
				{
					ex2.Data["ExceptionKind"] = "Authorization";
				}
				ex2.Data["ErrorCode"] = ex5.ErrorCode;
			}
			return ex2;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005691 File Offset: 0x00003891
		private static void SetExceptionData(IDictionary data, string key, params string[] values)
		{
			if (values != null && values.Length != 0)
			{
				if (values.Length == 1)
				{
					if (!string.IsNullOrWhiteSpace(values[0]))
					{
						data[key] = values[0];
						return;
					}
				}
				else
				{
					data[key] = string.Join(", ", values);
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000056C7 File Offset: 0x000038C7
		public static bool TryExtractExceptionDetails(Exception ex, out SapBwException newException)
		{
			if (ex is RfcBaseException)
			{
				newException = SapBwException.FromException(ex);
				return true;
			}
			newException = null;
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000056DF File Offset: 0x000038DF
		public static SapBwException NewAuthorizationException(string message)
		{
			SapBwException ex = new SapBwException(message);
			ex.Data["ExceptionKind"] = "Authorization";
			return ex;
		}

		// Token: 0x0400006A RID: 106
		public static readonly Dictionary<string, string> KnownErrors = new Dictionary<string, string>
		{
			{ "BRAINOLAPAPI 100", "1084454, 1282785, 401607, 1786009" },
			{ "RH 161", "1273094" },
			{ "BRAINOLAPAPI 011", "2479649" },
			{ "BRAIN 632", "2129621, 1823174" },
			{ "SY 530", "2031796" },
			{ "BRAIN 629", "2239449" },
			{ "01 512", "2320012" },
			{ "BRAIN 299", "1304564, 2632620" }
		};

		// Token: 0x0400006B RID: 107
		private const string ExceptionKindKey = "ExceptionKind";

		// Token: 0x0400006C RID: 108
		private const string ErrorCodeKey = "ErrorCode";
	}
}
