using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000014 RID: 20
	internal static class DSRConversionHelper
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000032BE File Offset: 0x000014BE
		public static string CreateCompleteResourcePath(DataSourceReference dsr)
		{
			return dsr.DataSource.Kind + "/" + dsr.DataSource.NormalizedPath;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000032E0 File Offset: 0x000014E0
		public static DataSource ParseCompleteResourcePath(string completeResourcePath)
		{
			int num = completeResourcePath.IndexOf('/');
			if (num < 0)
			{
				throw new ArgumentException("completeResourcePath");
			}
			return new DataSource(completeResourcePath.Substring(0, num), completeResourcePath.Substring(num + 1));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000331C File Offset: 0x0000151C
		private static string GetDssJson(string processedCredential, DataSource ds)
		{
			Credential credential = Credential.FromJson(processedCredential, "");
			credential.StripKindAndPath();
			DataSourceSetting dataSourceSetting = credential.ToDss();
			string text = DataSourceSettings.Create(ds, dataSourceSetting);
			text = text.Trim();
			return text.Substring(1, text.Length - 2);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003360 File Offset: 0x00001560
		private static string FirstCharToUpper(string s)
		{
			if (s.Length > 0)
			{
				return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1);
			}
			return s;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003398 File Offset: 0x00001598
		private static string FirstCharToUpperJsonDictionary(string optionsJson)
		{
			if (string.IsNullOrEmpty(optionsJson))
			{
				return optionsJson;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize<Dictionary<string, object>>(optionsJson);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				dictionary2[DSRConversionHelper.FirstCharToUpper(keyValuePair.Key)] = keyValuePair.Value;
			}
			return javaScriptSerializer.Serialize(dictionary2);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000341C File Offset: 0x0000161C
		public static void ConvertDataSourceToM(string connectionDetailsProp, string contextExpressionProp, string processedCredential, string optionsProp, out string dataSourceExpression, out string dataSourceSettingsJson, out string completeNormalizedResourcePath, out string sessionCredentials, out bool isCredentialKindOAuth)
		{
			DataSourceReference dataSourceReference = new DataSourceReference(connectionDetailsProp);
			optionsProp = (string.IsNullOrEmpty(optionsProp) ? null : optionsProp);
			DataSourceWithExpression dataSourceWithExpression = new DataSourceWithExpression(dataSourceReference, DSRConversionHelper.FirstCharToUpperJsonDictionary(optionsProp), contextExpressionProp);
			dataSourceExpression = MHelper.GetCode(dataSourceWithExpression);
			completeNormalizedResourcePath = DSRConversionHelper.CreateCompleteResourcePath(dataSourceReference);
			dataSourceSettingsJson = DSRConversionHelper.GetDssJson(processedCredential, dataSourceReference.DataSource);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, string> dictionary = new Dictionary<string, string> { { completeNormalizedResourcePath, processedCredential } };
			sessionCredentials = javaScriptSerializer.Serialize(dictionary);
			Credential credential = Credential.FromJson(processedCredential, "");
			isCredentialKindOAuth = credential.AuthenticationKind == "OAuth2";
		}
	}
}
