using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Crypto;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Web
{
	// Token: 0x020002AC RID: 684
	internal static class WebOptionsHelper
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x00038928 File Offset: 0x00036B28
		public static Request GetRequestFromMetadataOptions(IEngineHost host, string resourceKind, TextValue initialUrl, Value content)
		{
			Value value;
			content.TryGetMetaField("Request.Options", out value);
			return WebOptionsHelper.GetRequest(host, resourceKind, null, initialUrl, value, false, null);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00038950 File Offset: 0x00036B50
		public static Request GetRequest(IEngineHost host, string resourceKind, string resourcePath, TextValue url, Value options, bool privilegedMode, Action<MashupHttpWebResponse, IHostTrace> responseErrorHandler = null)
		{
			RecordValue recordValue = (options.IsNull ? RecordValue.Empty : options.AsRecord);
			Options.ValidateOptions(recordValue, privilegedMode ? WebOptionsHelper.validPrivilegedOptionKeys : WebOptionsHelper.validOptionKeys, "Web.Contents", null);
			Value @null = Value.Null;
			Value null2 = Value.Null;
			Value null3 = Value.Null;
			string text = null;
			Value null4 = Value.Null;
			Value null5 = Value.Null;
			Value null6 = Value.Null;
			Value null7 = Value.Null;
			recordValue.TryGetValue("Query", out @null);
			recordValue.TryGetValue("Content", out null2);
			recordValue.TryGetValue("Headers", out null3);
			Value value;
			if (recordValue.TryGetValue("ApiKeyName", out value))
			{
				text = value.AsText.String;
			}
			recordValue.TryGetValue("Timeout", out null4);
			recordValue.TryGetValue("RelativePath", out null5);
			recordValue.TryGetValue("CredentialQuery", out null6);
			List<int> list = new List<int>();
			Value value2;
			if (recordValue.TryGetValue("ManualStatusHandling", out value2))
			{
				for (int i = 0; i < value2.AsList.Count; i++)
				{
					int asInteger = value2.AsList[i].AsInteger32;
					if (privilegedMode || (asInteger != 401 && asInteger != 403))
					{
						list.Add(asInteger);
					}
				}
			}
			Request request = Request.Create(host, resourceKind, resourcePath, url, @null, null2, text, null3, null4, list.ToArray(), null5, null6, responseErrorHandler, null);
			if (request is FileRequest && Modules.DisabledModules.Contains("File"))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.ModuleDisabled("File"), null, null);
			}
			if (recordValue.TryGetValue("IsRetry", out value2))
			{
				request.IsRetry = value2.AsLogical.AsBoolean;
			}
			if (recordValue.TryGetValue("ExcludedFromCacheKey", out value2))
			{
				request.ExcludedHeaders = value2.AsList;
			}
			if (recordValue.TryGetValue("ClientCertificate", out value2))
			{
				request.ClientCertificate = CryptoModule.CertificateFromThumbprint(host, value2.AsText);
			}
			if (recordValue.TryGetValue("Resource", out value2))
			{
				request.OAuthResource = value2.AsText;
			}
			IList<string> list2;
			if (recordValue.TryGetValue("SafeRequestHeaders", out value2) && value2.IsList && value2.AsList.TryGetStringList(100, out list2))
			{
				request.SafeRequestHeaders = list2;
			}
			if (recordValue.TryGetValue("SafeResponseHeaders", out value2) && value2.IsList && value2.AsList.TryGetStringList(100, out list2))
			{
				request.SafeResponseHeaders = list2;
			}
			if (recordValue.TryGetValue("TraceData", out value2) && value2.IsRecord)
			{
				request.TraceData = value2.AsRecord;
			}
			return request;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00038BF4 File Offset: 0x00036DF4
		public static bool TryGetApiKeyName(Value value, out string keyName)
		{
			Value @null = Value.Null;
			if (value.IsNull || (value.IsRecord && !value.AsRecord.TryGetValue("ApiKeyName", out @null)))
			{
				keyName = null;
				return false;
			}
			keyName = (@null.IsText ? @null.AsString : null);
			return true;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00038C44 File Offset: 0x00036E44
		public static bool TryGetHiddenQueryKeys(Value value, out HashSet<string> keys)
		{
			Value @null = Value.Null;
			keys = null;
			if (value.IsRecord && value.AsRecord.TryGetValue("CredentialQuery", out @null))
			{
				keys = new HashSet<string>(@null.AsRecord.Keys);
			}
			string text;
			if (WebOptionsHelper.TryGetApiKeyName(value, out text))
			{
				keys = keys ?? new HashSet<string>();
				keys.Add(text);
			}
			return keys != null;
		}

		// Token: 0x0400086D RID: 2157
		private static readonly HashSet<string> validOptionKeys = new HashSet<string>(new string[] { "ApiKeyName", "Content", "Query", "Headers", "Timeout", "ExcludedFromCacheKey", "ManualStatusHandling", "IsRetry", "RelativePath" });

		// Token: 0x0400086E RID: 2158
		private static readonly HashSet<string> validPrivilegedOptionKeys = new HashSet<string>(WebOptionsHelper.validOptionKeys.Concat(new string[] { "ManualCredentials", "ClientCertificate", "CredentialQuery", "Resource", "SafeRequestHeaders", "SafeResponseHeaders", "TraceData" }));
	}
}
