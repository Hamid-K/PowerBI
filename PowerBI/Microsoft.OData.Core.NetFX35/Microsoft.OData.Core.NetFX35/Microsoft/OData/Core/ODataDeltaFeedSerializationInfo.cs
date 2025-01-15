using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000165 RID: 357
	public sealed class ODataDeltaFeedSerializationInfo
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x000310AD File Offset: 0x0002F2AD
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x000310B5 File Offset: 0x0002F2B5
		public string EntitySetName
		{
			get
			{
				return this.entitySetName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "EntitySetName");
				this.entitySetName = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x000310C9 File Offset: 0x0002F2C9
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x000310D1 File Offset: 0x0002F2D1
		public string EntityTypeName
		{
			get
			{
				return this.entityTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "EntityTypeName");
				this.entityTypeName = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x000310E5 File Offset: 0x0002F2E5
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x000310F7 File Offset: 0x0002F2F7
		public string ExpectedTypeName
		{
			get
			{
				return this.expectedEntityTypeName ?? this.EntityTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "ExpectedTypeName");
				this.expectedEntityTypeName = value;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003110B File Offset: 0x0002F30B
		internal static ODataDeltaFeedSerializationInfo Validate(ODataDeltaFeedSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetName, "serializationInfo.EntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntityTypeName, "serializationInfo.EntityTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040005B7 RID: 1463
		private string entitySetName;

		// Token: 0x040005B8 RID: 1464
		private string entityTypeName;

		// Token: 0x040005B9 RID: 1465
		private string expectedEntityTypeName;
	}
}
