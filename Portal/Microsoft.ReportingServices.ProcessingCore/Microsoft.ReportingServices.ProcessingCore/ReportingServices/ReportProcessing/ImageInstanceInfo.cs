using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072F RID: 1839
	[Serializable]
	internal class ImageInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006648 RID: 26184 RVA: 0x0019129B File Offset: 0x0018F49B
		internal ImageInstanceInfo(ReportProcessing.ProcessingContext pc, Image reportItemDef, ReportItemInstance owner, int index, bool customCreated)
			: base(pc, reportItemDef, owner, index, customCreated)
		{
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x001912AA File Offset: 0x0018F4AA
		internal ImageInstanceInfo(Image reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x001912B3 File Offset: 0x0018F4B3
		// (set) Token: 0x0600664B RID: 26187 RVA: 0x001912C0 File Offset: 0x0018F4C0
		internal string ImageValue
		{
			get
			{
				return this.m_data as string;
			}
			set
			{
				this.m_data = value;
			}
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x001912C9 File Offset: 0x0018F4C9
		// (set) Token: 0x0600664D RID: 26189 RVA: 0x001912D6 File Offset: 0x0018F4D6
		internal ImageData Data
		{
			get
			{
				return this.m_data as ImageData;
			}
			set
			{
				this.m_data = value;
			}
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x001912DF File Offset: 0x0018F4DF
		internal object ValueObject
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x0600664F RID: 26191 RVA: 0x001912E7 File Offset: 0x0018F4E7
		// (set) Token: 0x06006650 RID: 26192 RVA: 0x001912EF File Offset: 0x0018F4EF
		internal ActionInstance Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x06006651 RID: 26193 RVA: 0x001912F8 File Offset: 0x0018F4F8
		// (set) Token: 0x06006652 RID: 26194 RVA: 0x00191300 File Offset: 0x0018F500
		internal bool BrokenImage
		{
			get
			{
				return this.m_brokenImage;
			}
			set
			{
				this.m_brokenImage = value;
			}
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x00191309 File Offset: 0x0018F509
		// (set) Token: 0x06006654 RID: 26196 RVA: 0x00191311 File Offset: 0x0018F511
		internal ImageMapAreaInstanceList ImageMapAreas
		{
			get
			{
				return this.m_imageMapAreas;
			}
			set
			{
				this.m_imageMapAreas = value;
			}
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x0019131C File Offset: 0x0018F51C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ImageValue, Token.String),
				new MemberInfo(MemberName.Action, ObjectType.ActionInstance),
				new MemberInfo(MemberName.BrokenImage, Token.Boolean),
				new MemberInfo(MemberName.ImageMapAreas, ObjectType.ImageMapAreaInstanceList)
			});
		}

		// Token: 0x040032F3 RID: 13043
		private object m_data;

		// Token: 0x040032F4 RID: 13044
		private ActionInstance m_action;

		// Token: 0x040032F5 RID: 13045
		private bool m_brokenImage;

		// Token: 0x040032F6 RID: 13046
		private ImageMapAreaInstanceList m_imageMapAreas;
	}
}
