using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.ServiceContracts
{
	// Token: 0x02000007 RID: 7
	public sealed class ExecuteSemanticQueryRequestDeserializer
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020F1 File Offset: 0x000002F1
		private ExecuteSemanticQueryRequestDeserializer()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020FC File Offset: 0x000002FC
		public bool TryGetVersion(string json, out string version)
		{
			JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(json));
			if (jsonTextReader.TokenType == JsonToken.None)
			{
				jsonTextReader.Read();
			}
			if (jsonTextReader.TokenType == JsonToken.StartObject && jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.PropertyName && (string)jsonTextReader.Value == "Version")
			{
				jsonTextReader.Read();
				version = ExecuteSemanticQueryRequestDeserializer.Serializer.Deserialize<string>(jsonTextReader);
				return true;
			}
			version = null;
			return false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000216F File Offset: 0x0000036F
		public ExecuteSemanticQueryRequest Deserialize(string json)
		{
			ExecuteSemanticQueryRequest executeSemanticQueryRequest = this.DeserializeGeneric<ExecuteSemanticQueryRequest>(json, ExecuteSemanticQueryRequestDeserializer.ExecuteSemanticQueryDeserializers, "ExecuteSemanticQueryRequest");
			ExecuteSemanticQueryRequestDeserializer.ValidateRequest(executeSemanticQueryRequest, "ExecuteSemanticQueryRequest");
			return executeSemanticQueryRequest;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		private T DeserializeGeneric<T>(string json, IReadOnlyDictionary<string, Func<string, T>> versionedDeserializers, string typeName) where T : VersionedJsonProtocolBase
		{
			string text;
			if (!this.TryGetVersion(json, out text))
			{
				if (!versionedDeserializers.ContainsKey(ExecuteSemanticQueryRequestDeserializer.UnversionedDeserializerKey))
				{
					throw new ArgumentException(StringUtil.FormatInvariant("Could not find version on json input {0}, and no default deserializer was provided.", typeName));
				}
				return versionedDeserializers[ExecuteSemanticQueryRequestDeserializer.UnversionedDeserializerKey](json);
			}
			else
			{
				if (!versionedDeserializers.ContainsKey(text))
				{
					throw new ArgumentException(StringUtil.FormatInvariant("Unrecognized version {0} of json input {1}.", text, typeName));
				}
				return versionedDeserializers[text](json);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002200 File Offset: 0x00000400
		public static ExecuteSemanticQueryRequest DeserializeV2Request(string json)
		{
			ExecuteSemanticQueryRequest executeSemanticQueryRequest;
			try
			{
				executeSemanticQueryRequest = JsonConvert.DeserializeObject<ExecuteSemanticQueryRequest>(json);
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to deserialize version 2 of json ExecuteSemanticQueryRequest.", ex);
			}
			return executeSemanticQueryRequest;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		private static ExecuteSemanticQueryRequest DeserializeLegacyRequest(string jsonRequest)
		{
			ExecuteSemanticQueryRequest_V1 executeSemanticQueryRequest_V;
			try
			{
				executeSemanticQueryRequest_V = JsonConvert.DeserializeObject<ExecuteSemanticQueryRequest_V1>(jsonRequest);
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to deserialize ExecuteSemanticQueryRequest_V1 from json", ex);
			}
			ExecuteSemanticQueryRequestDeserializer.ValidateRequest(executeSemanticQueryRequest_V);
			ExecuteSemanticQueryRequest executeSemanticQueryRequest = ExecuteSemanticQueryRequest.Upgrade(executeSemanticQueryRequest_V);
			ExecuteSemanticQueryRequestDeserializer.ValidateRequest(executeSemanticQueryRequest, "ExecuteSemanticQueryRequest_V1");
			return executeSemanticQueryRequest;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002280 File Offset: 0x00000480
		private static void ValidateRequest(ExecuteSemanticQueryRequest_V1 request)
		{
			if (request.Queries.IsNullOrEmptyCollection<DataQuery>())
			{
				throw new ArgumentException("The supplied ExecuteSemanticQueryRequest_V1 is invalid.");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000229C File Offset: 0x0000049C
		private static void ValidateRequest(ExecuteSemanticQueryRequest request, string requestFormat)
		{
			bool flag = !request.Queries.IsNullOrEmptyCollection<DataQueryRequest>();
			bool flag2 = !request.CancelQueries.IsNullOrEmptyCollection<CancelQueryRequest>();
			if (!flag && !flag2)
			{
				throw new ArgumentException(StringUtil.FormatInvariant("The supplied {0} has no data queries and no cancel queries.", requestFormat));
			}
			if (flag)
			{
				foreach (DataQueryRequest dataQueryRequest in request.Queries)
				{
					if (dataQueryRequest == null || dataQueryRequest.Query == null)
					{
						throw new ArgumentException(StringUtil.FormatInvariant("The supplied {0} has invalid DataQueryRequests.", requestFormat));
					}
					IEnumerable<ValidationResult> enumerable;
					if (!DataQuery.TryValidate(dataQueryRequest.Query, out enumerable))
					{
						throw new ArgumentException(StringUtil.FormatInvariant("The supplied {0} has invalid DataQueryRequests. The validation failures were: {1}.", requestFormat, enumerable.StringJoin(null)));
					}
				}
			}
			if (flag2)
			{
				if (request.CancelQueries.Any((CancelQueryRequest query) => query == null || string.IsNullOrWhiteSpace(query.QueryId)))
				{
					throw new ArgumentException("The supplied request has invalid CancelQueryRequests.");
				}
			}
		}

		// Token: 0x0400002D RID: 45
		public static readonly string UnversionedDeserializerKey = string.Empty;

		// Token: 0x0400002E RID: 46
		public static readonly ExecuteSemanticQueryRequestDeserializer Instance = new ExecuteSemanticQueryRequestDeserializer();

		// Token: 0x0400002F RID: 47
		private static readonly JsonSerializer Serializer = JsonSerializer.Create();

		// Token: 0x04000030 RID: 48
		private static readonly IReadOnlyDictionary<string, Func<string, ExecuteSemanticQueryRequest>> ExecuteSemanticQueryDeserializers = new Dictionary<string, Func<string, ExecuteSemanticQueryRequest>>
		{
			{
				ExecuteSemanticQueryRequestDeserializer.UnversionedDeserializerKey,
				new Func<string, ExecuteSemanticQueryRequest>(ExecuteSemanticQueryRequestDeserializer.DeserializeLegacyRequest)
			},
			{
				"2.0.0",
				new Func<string, ExecuteSemanticQueryRequest>(ExecuteSemanticQueryRequestDeserializer.DeserializeV2Request)
			}
		};
	}
}
