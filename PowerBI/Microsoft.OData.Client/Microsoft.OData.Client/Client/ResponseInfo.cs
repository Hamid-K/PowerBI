using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000064 RID: 100
	internal class ResponseInfo
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000D431 File Offset: 0x0000B631
		internal ResponseInfo(RequestInfo requestInfo, MergeOption mergeOption)
		{
			this.requestInfo = requestInfo;
			this.mergeOption = mergeOption;
			this.ReadHelper = new ODataMessageReadingHelper(this);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000D453 File Offset: 0x0000B653
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000D45B File Offset: 0x0000B65B
		public ODataMessageReadingHelper ReadHelper { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000D464 File Offset: 0x0000B664
		internal bool IsContinuation
		{
			get
			{
				return this.requestInfo.IsContinuation;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000D471 File Offset: 0x0000B671
		internal MergeOption MergeOption
		{
			get
			{
				return this.mergeOption;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000D479 File Offset: 0x0000B679
		internal bool ThrowOnUndeclaredPropertyForNonOpenType
		{
			get
			{
				return this.Context.UndeclaredPropertyBehavior != UndeclaredPropertyBehavior.Support;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000D48B File Offset: 0x0000B68B
		internal EntityTracker EntityTracker
		{
			get
			{
				return this.Context.EntityTracker;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000D498 File Offset: 0x0000B698
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000D4A5 File Offset: 0x0000B6A5
		internal bool ApplyingChanges
		{
			get
			{
				return this.Context.ApplyingChanges;
			}
			set
			{
				this.Context.ApplyingChanges = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000D4B3 File Offset: 0x0000B6B3
		internal TypeResolver TypeResolver
		{
			get
			{
				return this.requestInfo.TypeResolver;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.requestInfo.BaseUriResolver;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000D4CD File Offset: 0x0000B6CD
		internal ODataProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.Context.MaxProtocolVersion;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		internal ClientEdmModel Model
		{
			get
			{
				return this.requestInfo.Model;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		internal DataServiceContext Context
		{
			get
			{
				return this.requestInfo.Context;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		internal DataServiceClientResponsePipelineConfiguration ResponsePipeline
		{
			get
			{
				return this.requestInfo.Configurations.ResponsePipeline;
			}
		}

		// Token: 0x04000115 RID: 277
		private readonly RequestInfo requestInfo;

		// Token: 0x04000116 RID: 278
		private readonly MergeOption mergeOption;
	}
}
