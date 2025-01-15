using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200021B RID: 539
	public abstract class PartitionSourceOverride
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x000CB9E7 File Offset: 0x000C9BE7
		internal PartitionSourceOverride()
		{
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000CB9EF File Offset: 0x000C9BEF
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x000CB9F7 File Offset: 0x000C9BF7
		internal PartitionOverride Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.OnOwnerChanging(value);
				this.owner = value;
			}
		}

		// Token: 0x06001E6B RID: 7787
		protected abstract void OnOwnerChanging(PartitionOverride owner);

		// Token: 0x06001E6C RID: 7788
		internal abstract bool HandleJsonProperty(JProperty property);

		// Token: 0x06001E6D RID: 7789 RVA: 0x000CBA08 File Offset: 0x000C9C08
		internal void ReadFromJsonObject(JObject jsonObject)
		{
			foreach (JProperty jproperty in jsonObject.Properties())
			{
				if (!this.HandleJsonProperty(jproperty))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty((string)jproperty.Value), null);
				}
			}
		}

		// Token: 0x040006FB RID: 1787
		private PartitionOverride owner;
	}
}
