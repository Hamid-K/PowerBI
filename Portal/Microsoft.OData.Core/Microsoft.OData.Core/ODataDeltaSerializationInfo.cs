using System;

namespace Microsoft.OData
{
	// Token: 0x02000079 RID: 121
	public sealed class ODataDeltaSerializationInfo
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000BAAD File Offset: 0x00009CAD
		public string NavigationSourceName
		{
			get
			{
				return this.navigationSourceName;
			}
			set
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(value, "NavigationSourceName");
				this.navigationSourceName = value;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		internal static ODataDeltaSerializationInfo Validate(ODataDeltaSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceName, "serializationInfo.EntitySetName");
			}
			return serializationInfo;
		}

		// Token: 0x040001F2 RID: 498
		private string navigationSourceName;
	}
}
