using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011C RID: 284
	public sealed class ODataEntityReferenceLinksSerializationInfo
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001943D File Offset: 0x0001763D
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x00019445 File Offset: 0x00017645
		public string SourceEntitySetName
		{
			get
			{
				return this.sourceEntitySetName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "SourceEntitySetName");
				this.sourceEntitySetName = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00019459 File Offset: 0x00017659
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00019461 File Offset: 0x00017661
		public string Typecast
		{
			get
			{
				return this.typecast;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotEmpty(value, "Typecast");
				this.typecast = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00019475 File Offset: 0x00017675
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001947D File Offset: 0x0001767D
		public string NavigationPropertyName
		{
			get
			{
				return this.navigationPropertyName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "NavigationPropertyName");
				this.navigationPropertyName = value;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00019491 File Offset: 0x00017691
		internal static ODataEntityReferenceLinksSerializationInfo Validate(ODataEntityReferenceLinksSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.SourceEntitySetName, "serializationInfo.SourceEntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationPropertyName, "serializationInfo.NavigationPropertyName");
			}
			return serializationInfo;
		}

		// Token: 0x040002D9 RID: 729
		private string sourceEntitySetName;

		// Token: 0x040002DA RID: 730
		private string typecast;

		// Token: 0x040002DB RID: 731
		private string navigationPropertyName;
	}
}
