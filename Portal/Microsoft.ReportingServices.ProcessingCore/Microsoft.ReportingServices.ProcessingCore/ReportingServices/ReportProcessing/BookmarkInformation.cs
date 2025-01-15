using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000712 RID: 1810
	[Serializable]
	internal sealed class BookmarkInformation
	{
		// Token: 0x06006510 RID: 25872 RVA: 0x0018EEFF File Offset: 0x0018D0FF
		internal BookmarkInformation()
		{
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x0018EF07 File Offset: 0x0018D107
		internal BookmarkInformation(string id, int page)
		{
			this.m_id = id;
			this.m_page = page;
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x0018EF1D File Offset: 0x0018D11D
		// (set) Token: 0x06006513 RID: 25875 RVA: 0x0018EF25 File Offset: 0x0018D125
		internal string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x0018EF2E File Offset: 0x0018D12E
		// (set) Token: 0x06006515 RID: 25877 RVA: 0x0018EF36 File Offset: 0x0018D136
		internal int Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x0018EF40 File Offset: 0x0018D140
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Id, Token.String),
				new MemberInfo(MemberName.Page, Token.Int32)
			});
		}

		// Token: 0x0400329B RID: 12955
		private string m_id;

		// Token: 0x0400329C RID: 12956
		private int m_page;
	}
}
