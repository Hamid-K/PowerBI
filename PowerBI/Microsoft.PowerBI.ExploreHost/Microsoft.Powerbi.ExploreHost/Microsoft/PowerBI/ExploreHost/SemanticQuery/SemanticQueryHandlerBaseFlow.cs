using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost.ServiceContracts;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000045 RID: 69
	internal class SemanticQueryHandlerBaseFlow : ExploreClientHandlerBaseFlow
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000716D File Offset: 0x0000536D
		internal SemanticQueryHandlerBaseFlow(ExploreClientHandlerContext context, string databaseID)
			: base(context, databaseID)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007177 File Offset: 0x00005377
		protected virtual void InternalValidateRequest(ExecuteSemanticQueryRequest request)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000717C File Offset: 0x0000537C
		protected ExecuteSemanticQueryRequest DeserializeExecuteSemanticQueryRequest(string jsonRequest)
		{
			if (string.IsNullOrWhiteSpace(jsonRequest))
			{
				throw new ArgumentException("SemanticQuery is null or whitespace.");
			}
			ExecuteSemanticQueryRequest executeSemanticQueryRequest;
			try
			{
				executeSemanticQueryRequest = ExecuteSemanticQueryRequestDeserializer.Instance.Deserialize(jsonRequest);
			}
			catch (JsonException ex)
			{
				throw new ArgumentException("Failed to deserialize SemanticQueryDataShapeCommands list from json", ex);
			}
			this.InternalValidateRequest(executeSemanticQueryRequest);
			return executeSemanticQueryRequest;
		}

		// Token: 0x040000DA RID: 218
		protected ServiceErrorExtractor serviceErrorExtractor;
	}
}
