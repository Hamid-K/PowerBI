using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000032 RID: 50
	internal sealed class ListContentCollection
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000E918 File Offset: 0x0000CB18
		internal ListContentCollection(List owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x170003D6 RID: 982
		public ListContent this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				ListContent listContent = null;
				if (index == 0)
				{
					listContent = this.m_firstListContent;
				}
				else if (this.m_listContents != null)
				{
					listContent = this.m_listContents[index - 1];
				}
				if (listContent == null)
				{
					listContent = new ListContent(this.m_owner, index);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (index == 0)
						{
							this.m_firstListContent = listContent;
						}
						else
						{
							if (this.m_listContents == null)
							{
								this.m_listContents = new ListContent[this.Count - 1];
							}
							this.m_listContents[index - 1] = listContent;
						}
					}
				}
				return listContent;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public int Count
		{
			get
			{
				int num = 0;
				ListInstance listInstance = (ListInstance)this.m_owner.ReportItemInstance;
				if (listInstance != null)
				{
					num = listInstance.ListContents.Count;
				}
				if (num == 0)
				{
					return 1;
				}
				return num;
			}
		}

		// Token: 0x040000EF RID: 239
		private List m_owner;

		// Token: 0x040000F0 RID: 240
		private ListContent[] m_listContents;

		// Token: 0x040000F1 RID: 241
		private ListContent m_firstListContent;
	}
}
