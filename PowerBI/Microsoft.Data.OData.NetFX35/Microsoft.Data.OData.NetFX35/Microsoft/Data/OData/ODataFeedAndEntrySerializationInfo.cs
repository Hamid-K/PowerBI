using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000120 RID: 288
	public sealed class ODataFeedAndEntrySerializationInfo
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001956B File Offset: 0x0001776B
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00019573 File Offset: 0x00017773
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00019587 File Offset: 0x00017787
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0001958F File Offset: 0x0001778F
		public string EntitySetElementTypeName
		{
			get
			{
				return this.entitySetElementTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "EntitySetElementTypeName");
				this.entitySetElementTypeName = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000195A3 File Offset: 0x000177A3
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x000195B5 File Offset: 0x000177B5
		public string ExpectedTypeName
		{
			get
			{
				return this.expectedTypeName ?? this.EntitySetElementTypeName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "ExpectedTypeName");
				this.expectedTypeName = value;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000195C9 File Offset: 0x000177C9
		internal static ODataFeedAndEntrySerializationInfo Validate(ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetName, "serializationInfo.EntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.EntitySetElementTypeName, "serializationInfo.EntitySetElementTypeName");
			}
			return serializationInfo;
		}

		// Token: 0x040002E6 RID: 742
		private string entitySetName;

		// Token: 0x040002E7 RID: 743
		private string entitySetElementTypeName;

		// Token: 0x040002E8 RID: 744
		private string expectedTypeName;
	}
}
