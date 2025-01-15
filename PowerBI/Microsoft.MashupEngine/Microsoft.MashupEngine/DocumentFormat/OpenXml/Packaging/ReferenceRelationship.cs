using System;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020F5 RID: 8437
	internal abstract class ReferenceRelationship
	{
		// Token: 0x0600CF8C RID: 53132 RVA: 0x000020FD File Offset: 0x000002FD
		internal ReferenceRelationship()
		{
		}

		// Token: 0x0600CF8D RID: 53133 RVA: 0x00294DDE File Offset: 0x00292FDE
		protected internal ReferenceRelationship(PackageRelationship packageRelationship)
		{
			this.RelationshipType = packageRelationship.RelationshipType;
			this.Uri = packageRelationship.TargetUri;
			this.IsExternal = packageRelationship.TargetMode == TargetMode.External;
			this.Id = packageRelationship.Id;
		}

		// Token: 0x0600CF8E RID: 53134 RVA: 0x00294E19 File Offset: 0x00293019
		protected internal ReferenceRelationship(Uri targetUri, bool isExternal, string relationshipType, string id)
		{
			this.RelationshipType = relationshipType;
			this.Uri = targetUri;
			this.Id = id;
			this.IsExternal = isExternal;
		}

		// Token: 0x170031DB RID: 12763
		// (get) Token: 0x0600CF8F RID: 53135 RVA: 0x00294E3E File Offset: 0x0029303E
		// (set) Token: 0x0600CF90 RID: 53136 RVA: 0x00294E46 File Offset: 0x00293046
		public OpenXmlPartContainer Container { get; internal set; }

		// Token: 0x170031DC RID: 12764
		// (get) Token: 0x0600CF91 RID: 53137 RVA: 0x00294E4F File Offset: 0x0029304F
		// (set) Token: 0x0600CF92 RID: 53138 RVA: 0x00294E57 File Offset: 0x00293057
		public virtual string RelationshipType { get; private set; }

		// Token: 0x170031DD RID: 12765
		// (get) Token: 0x0600CF93 RID: 53139 RVA: 0x00294E60 File Offset: 0x00293060
		// (set) Token: 0x0600CF94 RID: 53140 RVA: 0x00294E68 File Offset: 0x00293068
		public virtual bool IsExternal { get; private set; }

		// Token: 0x170031DE RID: 12766
		// (get) Token: 0x0600CF95 RID: 53141 RVA: 0x00294E71 File Offset: 0x00293071
		// (set) Token: 0x0600CF96 RID: 53142 RVA: 0x00294E79 File Offset: 0x00293079
		public virtual string Id { get; private set; }

		// Token: 0x170031DF RID: 12767
		// (get) Token: 0x0600CF97 RID: 53143 RVA: 0x00294E82 File Offset: 0x00293082
		// (set) Token: 0x0600CF98 RID: 53144 RVA: 0x00294E8A File Offset: 0x0029308A
		public virtual Uri Uri { get; private set; }

		// Token: 0x0600CF99 RID: 53145 RVA: 0x00294E93 File Offset: 0x00293093
		internal void Initialize(Uri targetUri, bool isExternal, string relationshipType, string id)
		{
			this.RelationshipType = relationshipType;
			this.Uri = targetUri;
			this.Id = id;
			this.IsExternal = isExternal;
		}
	}
}
