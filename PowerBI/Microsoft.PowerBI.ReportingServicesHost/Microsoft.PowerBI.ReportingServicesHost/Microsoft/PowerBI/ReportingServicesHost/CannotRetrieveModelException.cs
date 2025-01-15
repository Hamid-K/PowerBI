using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal sealed class CannotRetrieveModelException : PowerBIExploreException
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00003FA4 File Offset: 0x000021A4
		public CannotRetrieveModelException(DataExtensionException innerException, ServiceErrorStatusCode statusCode, ModelLocation? modelMode = null)
			: base(innerException.ErrorCode, "An error occurred while loading the model. Verify that the connection information is correct and that you have permissions to access the data source.", innerException, innerException.ErrorSource, CannotRetrieveModelException.GetErrorDetails(innerException, modelMode), statusCode)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003FC6 File Offset: 0x000021C6
		public CannotRetrieveModelException(Exception innerException, ErrorSource errorSource, ServiceErrorStatusCode statusCode, ModelLocation? modelMode = null)
			: base("CannotRetrieveModelError", "An error occurred while loading the model. Verify that the connection information is correct and that you have permissions to access the data source.", innerException, errorSource, CannotRetrieveModelException.GetErrorDetails(null, modelMode), statusCode)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FE3 File Offset: 0x000021E3
		public CannotRetrieveModelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003FF0 File Offset: 0x000021F0
		public override string ToTraceString()
		{
			string text = base.ToTraceString();
			if (base.ErrorDetails != null)
			{
				string text2;
				if (base.ErrorDetails.TryGetValue("ProviderCode", out text2))
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}, ProviderErrorCode={1}", text, text2);
				}
				string text3;
				if (base.ErrorDetails.TryGetValue("ProviderMessage", out text3))
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}, ProviderMessage={1}", text, text3.MarkAsPrivate());
				}
			}
			return text;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004060 File Offset: 0x00002260
		private static Dictionary<string, string> GetErrorDetails(DataExtensionException innerException, ModelLocation? modelMode)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (innerException != null)
			{
				string text = innerException.ProviderMessage.MarkAsPrivate();
				uint providerErrorCode = innerException.ProviderErrorCode;
				if (!string.IsNullOrWhiteSpace(text))
				{
					dictionary.Add("ProviderMessage", text);
				}
				if (providerErrorCode != 0U)
				{
					dictionary.Add("ProviderCode", string.Format("0x{0:x8}", providerErrorCode));
				}
			}
			if (modelMode != null)
			{
				dictionary.Add("ModelMode", modelMode.ToString());
			}
			return dictionary;
		}

		// Token: 0x040000D8 RID: 216
		private const string ProviderMessageKey = "ProviderMessage";

		// Token: 0x040000D9 RID: 217
		private const string ProviderCodeKey = "ProviderCode";

		// Token: 0x040000DA RID: 218
		private const string ModelModeKey = "ModelMode";

		// Token: 0x040000DB RID: 219
		private const string CannotRetrieveModelErrorCode = "CannotRetrieveModelError";

		// Token: 0x040000DC RID: 220
		private const string CannotRetrieveModelErrorMessage = "An error occurred while loading the model. Verify that the connection information is correct and that you have permissions to access the data source.";
	}
}
