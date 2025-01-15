using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000169 RID: 361
	public sealed class ODataDeltaSerializationInfo
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00031144 File Offset: 0x0002F344
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0003114C File Offset: 0x0002F34C
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

		// Token: 0x06000D44 RID: 3396 RVA: 0x00031160 File Offset: 0x0002F360
		internal static ODataDeltaSerializationInfo Validate(ODataDeltaSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceName, "serializationInfo.EntitySetName");
			}
			return serializationInfo;
		}

		// Token: 0x040005CD RID: 1485
		private string navigationSourceName;
	}
}
