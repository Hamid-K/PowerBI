using System;

namespace Microsoft.OData
{
	// Token: 0x02000066 RID: 102
	public sealed class ODataCollectionStartSerializationInfo
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A5EB File Offset: 0x000087EB
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A5F3 File Offset: 0x000087F3
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

		// Token: 0x06000397 RID: 919 RVA: 0x0000A60E File Offset: 0x0000880E
		internal static ODataCollectionStartSerializationInfo Validate(ODataCollectionStartSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.CollectionTypeName, "serializationInfo.CollectionTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x04000184 RID: 388
		private string collectionTypeName;
	}
}
