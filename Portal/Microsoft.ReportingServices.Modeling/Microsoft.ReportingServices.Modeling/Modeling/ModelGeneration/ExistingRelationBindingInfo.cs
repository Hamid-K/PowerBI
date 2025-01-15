using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DD RID: 221
	internal sealed class ExistingRelationBindingInfo : ExistingBindingInfo
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00026A19 File Offset: 0x00024C19
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00026A21 File Offset: 0x00024C21
		public ModelRole SourceRole
		{
			get
			{
				return this.m_sourceRole;
			}
			set
			{
				this.m_sourceRole = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00026A2A File Offset: 0x00024C2A
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00026A32 File Offset: 0x00024C32
		public ModelRole TargetRole
		{
			get
			{
				return this.m_targetRole;
			}
			set
			{
				this.m_targetRole = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00026A3B File Offset: 0x00024C3B
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00026A43 File Offset: 0x00024C43
		public ModelEntity InheritanceEntity
		{
			get
			{
				return this.m_inheritanceEntity;
			}
			set
			{
				this.m_inheritanceEntity = value;
			}
		}

		// Token: 0x040004D5 RID: 1237
		private ModelRole m_sourceRole;

		// Token: 0x040004D6 RID: 1238
		private ModelRole m_targetRole;

		// Token: 0x040004D7 RID: 1239
		private ModelEntity m_inheritanceEntity;
	}
}
