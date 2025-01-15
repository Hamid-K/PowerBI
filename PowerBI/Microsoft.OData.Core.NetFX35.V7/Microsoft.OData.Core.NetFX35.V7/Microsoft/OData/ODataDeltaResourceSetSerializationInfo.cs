using System;

namespace Microsoft.OData
{
	// Token: 0x02000050 RID: 80
	public sealed class ODataDeltaResourceSetSerializationInfo
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00009B24 File Offset: 0x00007D24
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00009B2C File Offset: 0x00007D2C
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00009B40 File Offset: 0x00007D40
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00009B48 File Offset: 0x00007D48
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00009B5C File Offset: 0x00007D5C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00009B6E File Offset: 0x00007D6E
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

		// Token: 0x06000298 RID: 664 RVA: 0x00009B82 File Offset: 0x00007D82
		internal static ODataDeltaResourceSetSerializationInfo Validate(ODataDeltaResourceSetSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetName, "serializationInfo.EntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntityTypeName, "serializationInfo.EntityTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x04000176 RID: 374
		private string entitySetName;

		// Token: 0x04000177 RID: 375
		private string entityTypeName;

		// Token: 0x04000178 RID: 376
		private string expectedEntityTypeName;
	}
}
