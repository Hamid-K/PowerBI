using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011B RID: 283
	public sealed class ODataCollectionStartSerializationInfo
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000193FC File Offset: 0x000175FC
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00019404 File Offset: 0x00017604
		public string CollectionTypeName
		{
			get
			{
				return this.collectionTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "CollectionTypeName");
				ValidationUtils.ValidateCollectionTypeName(value);
				this.collectionTypeName = value;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001941F File Offset: 0x0001761F
		internal static ODataCollectionStartSerializationInfo Validate(ODataCollectionStartSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.CollectionTypeName, "serializationInfo.CollectionTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040002D8 RID: 728
		private string collectionTypeName;
	}
}
