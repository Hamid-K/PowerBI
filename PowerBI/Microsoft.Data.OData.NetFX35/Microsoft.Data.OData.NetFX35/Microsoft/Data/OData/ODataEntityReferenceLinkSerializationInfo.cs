using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011D RID: 285
	public sealed class ODataEntityReferenceLinkSerializationInfo
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000194BF File Offset: 0x000176BF
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x000194C7 File Offset: 0x000176C7
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000194DB File Offset: 0x000176DB
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x000194E3 File Offset: 0x000176E3
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000194F7 File Offset: 0x000176F7
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000194FF File Offset: 0x000176FF
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

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00019513 File Offset: 0x00017713
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0001951B File Offset: 0x0001771B
		public bool IsCollectionNavigationProperty { get; set; }

		// Token: 0x0600078C RID: 1932 RVA: 0x00019524 File Offset: 0x00017724
		internal static ODataEntityReferenceLinkSerializationInfo Validate(ODataEntityReferenceLinkSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.SourceEntitySetName, "serializationInfo.SourceEntitySetName");
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationPropertyName, "serializationInfo.NavigationPropertyName");
			}
			return serializationInfo;
		}

		// Token: 0x040002DC RID: 732
		private string sourceEntitySetName;

		// Token: 0x040002DD RID: 733
		private string typecast;

		// Token: 0x040002DE RID: 734
		private string navigationPropertyName;
	}
}
