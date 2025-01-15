using System;

namespace Microsoft.OData
{
	// Token: 0x02000043 RID: 67
	public sealed class ODataCollectionStartSerializationInfo
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008985 File Offset: 0x00006B85
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000898D File Offset: 0x00006B8D
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

		// Token: 0x06000223 RID: 547 RVA: 0x000089A8 File Offset: 0x00006BA8
		internal static ODataCollectionStartSerializationInfo Validate(ODataCollectionStartSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.CollectionTypeName, "serializationInfo.CollectionTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x04000126 RID: 294
		private string collectionTypeName;
	}
}
