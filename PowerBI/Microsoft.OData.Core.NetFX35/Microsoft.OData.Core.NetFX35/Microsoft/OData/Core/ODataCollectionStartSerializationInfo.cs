using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000154 RID: 340
	public sealed class ODataCollectionStartSerializationInfo
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0003052C File Offset: 0x0002E72C
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00030534 File Offset: 0x0002E734
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

		// Token: 0x06000CDD RID: 3293 RVA: 0x0003054F File Offset: 0x0002E74F
		internal static ODataCollectionStartSerializationInfo Validate(ODataCollectionStartSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.CollectionTypeName, "serializationInfo.CollectionTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x04000565 RID: 1381
		private string collectionTypeName;
	}
}
