using System;

namespace Microsoft.OData
{
	// Token: 0x02000073 RID: 115
	public sealed class ODataDeltaResourceSetSerializationInfo
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000B9C1 File Offset: 0x00009BC1
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000B9DD File Offset: 0x00009BDD
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000B9F1 File Offset: 0x00009BF1
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000BA03 File Offset: 0x00009C03
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

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BA17 File Offset: 0x00009C17
		internal static ODataDeltaResourceSetSerializationInfo Validate(ODataDeltaResourceSetSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetName, "serializationInfo.EntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntityTypeName, "serializationInfo.EntityTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040001D8 RID: 472
		private string entitySetName;

		// Token: 0x040001D9 RID: 473
		private string entityTypeName;

		// Token: 0x040001DA RID: 474
		private string expectedEntityTypeName;
	}
}
