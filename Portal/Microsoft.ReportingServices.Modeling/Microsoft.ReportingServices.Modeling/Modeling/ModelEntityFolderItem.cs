using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000086 RID: 134
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelEntityFolderItem : ModelItem
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00013790 File Offset: 0x00011990
		protected ModelEntityFolderItem()
		{
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00013798 File Offset: 0x00011998
		internal ModelEntityFolderItem(ModelEntityFolderItem baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000137A2 File Offset: 0x000119A2
		public override bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			if (newParentItem is SemanticModel || newParentItem is ModelEntityFolder)
			{
				message = null;
				return true;
			}
			return base.IsValidParentItem(newParentItem, out message);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000137C4 File Offset: 0x000119C4
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelEntityFolderItem.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00013828 File Offset: 0x00011A28
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ModelEntityFolderItem.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001388A File Offset: 0x00011A8A
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelEntityFolderItem.__declaration, ModelEntityFolderItem.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.ModelEntityFolderItem, ObjectType.ModelItem, list);
				});
			}
		}

		// Token: 0x04000321 RID: 801
		private static Declaration __declaration;

		// Token: 0x04000322 RID: 802
		private static readonly object __declarationLock = new object();
	}
}
