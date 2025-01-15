using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200072E RID: 1838
	[Serializable]
	internal sealed class ImageMapAreaInstance
	{
		// Token: 0x06006639 RID: 26169 RVA: 0x0019117C File Offset: 0x0018F37C
		internal ImageMapAreaInstance()
		{
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00191184 File Offset: 0x0018F384
		internal ImageMapAreaInstance(ReportProcessing.ProcessingContext processingContext)
		{
			this.m_uniqueName = processingContext.CreateUniqueName();
		}

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x0600663B RID: 26171 RVA: 0x00191198 File Offset: 0x0018F398
		// (set) Token: 0x0600663C RID: 26172 RVA: 0x001911A0 File Offset: 0x0018F3A0
		public string ID
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

		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x001911A9 File Offset: 0x0018F3A9
		// (set) Token: 0x0600663E RID: 26174 RVA: 0x001911B1 File Offset: 0x0018F3B1
		public ImageMapArea.ImageMapAreaShape Shape
		{
			get
			{
				return this.m_shape;
			}
			set
			{
				this.m_shape = value;
			}
		}

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x0600663F RID: 26175 RVA: 0x001911BA File Offset: 0x0018F3BA
		// (set) Token: 0x06006640 RID: 26176 RVA: 0x001911C2 File Offset: 0x0018F3C2
		public float[] Coordinates
		{
			get
			{
				return this.m_coordinates;
			}
			set
			{
				this.m_coordinates = value;
			}
		}

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06006641 RID: 26177 RVA: 0x001911CB File Offset: 0x0018F3CB
		// (set) Token: 0x06006642 RID: 26178 RVA: 0x001911D3 File Offset: 0x0018F3D3
		public Microsoft.ReportingServices.ReportProcessing.Action Action
		{
			get
			{
				return this.m_actionDef;
			}
			set
			{
				this.m_actionDef = value;
			}
		}

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06006643 RID: 26179 RVA: 0x001911DC File Offset: 0x0018F3DC
		// (set) Token: 0x06006644 RID: 26180 RVA: 0x001911E4 File Offset: 0x0018F3E4
		public ActionInstance ActionInstance
		{
			get
			{
				return this.m_actionInstance;
			}
			set
			{
				this.m_actionInstance = value;
			}
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x001911ED File Offset: 0x0018F3ED
		// (set) Token: 0x06006646 RID: 26182 RVA: 0x001911F5 File Offset: 0x0018F3F5
		public int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x00191200 File Offset: 0x0018F400
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Id, Token.String),
				new MemberInfo(MemberName.Shape, Token.Enum),
				new MemberInfo(MemberName.Coordinates, Token.Array, ObjectType.Single),
				new MemberInfo(MemberName.Action, ObjectType.Action),
				new MemberInfo(MemberName.ActionInstance, ObjectType.ActionInstance),
				new MemberInfo(MemberName.UniqueName, Token.Int32)
			});
		}

		// Token: 0x040032ED RID: 13037
		private string m_id;

		// Token: 0x040032EE RID: 13038
		private ImageMapArea.ImageMapAreaShape m_shape;

		// Token: 0x040032EF RID: 13039
		private float[] m_coordinates;

		// Token: 0x040032F0 RID: 13040
		private Microsoft.ReportingServices.ReportProcessing.Action m_actionDef;

		// Token: 0x040032F1 RID: 13041
		private ActionInstance m_actionInstance;

		// Token: 0x040032F2 RID: 13042
		private int m_uniqueName;
	}
}
