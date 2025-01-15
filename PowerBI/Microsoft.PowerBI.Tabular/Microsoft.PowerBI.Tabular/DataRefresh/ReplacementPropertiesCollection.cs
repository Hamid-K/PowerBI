using System;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200021E RID: 542
	internal abstract class ReplacementPropertiesCollection
	{
		// Token: 0x06001E7C RID: 7804
		internal abstract bool IsLinkOverriden(string propertyName, out MetadataObject newValue);

		// Token: 0x06001E7D RID: 7805
		internal abstract bool IsPropertyOverriden(string propertyName, out object newValue);

		// Token: 0x06001E7E RID: 7806 RVA: 0x000CBF14 File Offset: 0x000CA114
		internal bool IsPropertyOverriden<TPropertyValue>(string propertyName, out TPropertyValue newValue)
		{
			object obj;
			if (this.IsPropertyOverriden(propertyName, out obj))
			{
				newValue = (TPropertyValue)((object)obj);
				return true;
			}
			newValue = default(TPropertyValue);
			return false;
		}

		// Token: 0x0200044A RID: 1098
		internal struct OverridenProperty<T>
		{
			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x06002926 RID: 10534 RVA: 0x000F1255 File Offset: 0x000EF455
			// (set) Token: 0x06002927 RID: 10535 RVA: 0x000F125D File Offset: 0x000EF45D
			public T Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
					this.IsSet = true;
				}
			}

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06002928 RID: 10536 RVA: 0x000F126D File Offset: 0x000EF46D
			// (set) Token: 0x06002929 RID: 10537 RVA: 0x000F1275 File Offset: 0x000EF475
			public bool IsSet { get; private set; }

			// Token: 0x04001450 RID: 5200
			private T value;
		}
	}
}
