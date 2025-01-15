using System;
using System.Collections;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public sealed class DatasourceCredentialsCollection : CollectionBase
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00009C6A File Offset: 0x00007E6A
		public DatasourceCredentialsCollection()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009C74 File Offset: 0x00007E74
		public DatasourceCredentialsCollection(NameValueCollection userNameParams, NameValueCollection userPwdParams)
		{
			for (int i = 0; i < userNameParams.Count; i++)
			{
				string key = userNameParams.GetKey(i);
				string text = userNameParams.Get(i);
				if (text != null && text.Trim().Length != 0)
				{
					string text2 = userPwdParams[key];
					DatasourceCredentials datasourceCredentials = new DatasourceCredentials(key, text, text2);
					this.Add(datasourceCredentials);
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009CD3 File Offset: 0x00007ED3
		public int Add(DatasourceCredentials datasourceCred)
		{
			return base.InnerList.Add(datasourceCred);
		}

		// Token: 0x170000D8 RID: 216
		public DatasourceCredentials this[int index]
		{
			get
			{
				return (DatasourceCredentials)base.InnerList[index];
			}
		}
	}
}
