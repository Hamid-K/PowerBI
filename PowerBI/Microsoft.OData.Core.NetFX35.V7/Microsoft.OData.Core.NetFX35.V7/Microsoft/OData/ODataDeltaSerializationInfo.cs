using System;

namespace Microsoft.OData
{
	// Token: 0x02000056 RID: 86
	public sealed class ODataDeltaSerializationInfo
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00009C10 File Offset: 0x00007E10
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00009C18 File Offset: 0x00007E18
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

		// Token: 0x060002AB RID: 683 RVA: 0x00009C2C File Offset: 0x00007E2C
		internal static ODataDeltaSerializationInfo Validate(ODataDeltaSerializationInfo serializationInfo)
		{
			if (serializationInfo != null)
			{
				ExceptionUtils.CheckArgumentNotNull<string>(serializationInfo.NavigationSourceName, "serializationInfo.EntitySetName");
			}
			return serializationInfo;
		}

		// Token: 0x04000190 RID: 400
		private string navigationSourceName;
	}
}
