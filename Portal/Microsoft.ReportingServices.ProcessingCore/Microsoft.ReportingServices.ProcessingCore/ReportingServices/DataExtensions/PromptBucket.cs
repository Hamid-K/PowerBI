using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	internal sealed class PromptBucket : ArrayList
	{
		// Token: 0x06005261 RID: 21089 RVA: 0x0015B7AC File Offset: 0x001599AC
		internal PromptBucket()
		{
		}

		// Token: 0x17001EA5 RID: 7845
		internal DataSourceInfo this[int index]
		{
			get
			{
				return (DataSourceInfo)base[index];
			}
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0015B7C2 File Offset: 0x001599C2
		internal DataSourceInfo GetRepresentative()
		{
			Global.Tracer.Assert(this.Count > 0, "Prompt Bucket is empty on get representative");
			return this[0];
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x06005264 RID: 21092 RVA: 0x0015B7E3 File Offset: 0x001599E3
		internal bool NeedPrompt
		{
			get
			{
				return this.GetRepresentative().NeedPrompt;
			}
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0015B7F0 File Offset: 0x001599F0
		internal bool HasItemWithLinkID(Guid linkID)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].LinkID == linkID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0015B828 File Offset: 0x00159A28
		internal bool HasItemWithOriginalName(string originalName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].OriginalName == originalName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0015B860 File Offset: 0x00159A60
		internal void SetCredentials(DatasourceCredentials credentials, IDataProtection dataProtection)
		{
			for (int i = 0; i < this.Count; i++)
			{
				DataSourceInfo dataSourceInfo = this[i];
				if (dataSourceInfo.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt)
				{
					throw new InternalCatalogException("Non-promptable data source appeared in prompt collection!");
				}
				dataSourceInfo.SetUserName(credentials.UserName, dataProtection);
				dataSourceInfo.SetPassword(credentials.Password, dataProtection);
			}
		}
	}
}
