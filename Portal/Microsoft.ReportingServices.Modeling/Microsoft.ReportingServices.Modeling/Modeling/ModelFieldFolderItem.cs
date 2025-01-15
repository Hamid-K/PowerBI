using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200008B RID: 139
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ModelFieldFolderItem : ModelItem
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00013F2C File Offset: 0x0001212C
		protected ModelFieldFolderItem()
		{
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00013F34 File Offset: 0x00012134
		internal ModelFieldFolderItem(ModelFieldFolderItem baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00013F40 File Offset: 0x00012140
		public ModelEntity Entity
		{
			get
			{
				if (base.ParentItem is ModelEntity)
				{
					return (ModelEntity)base.ParentItem;
				}
				if (base.ParentItem is ModelFieldFolderItem)
				{
					return ((ModelFieldFolderItem)base.ParentItem).Entity;
				}
				if (base.ParentItem != null)
				{
					throw new InternalModelingException("ParentItem is not ModelEntity or ModelFieldFolderItem.");
				}
				return null;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00013F98 File Offset: 0x00012198
		public override bool IsValidParentItem(ModelItem newParentItem, out ValidationMessage message)
		{
			if (newParentItem is ModelEntity || newParentItem is ModelFieldFolder)
			{
				message = null;
				return true;
			}
			return base.IsValidParentItem(newParentItem, out message);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00013FB8 File Offset: 0x000121B8
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelFieldFolderItem.Declaration);
			if (!writer.NextMember())
			{
				return;
			}
			MemberName memberName = writer.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001401C File Offset: 0x0001221C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ModelFieldFolderItem.Declaration);
			if (!reader.NextMember())
			{
				return;
			}
			MemberName memberName = reader.CurrentMember.MemberName;
			throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001407E File Offset: 0x0001227E
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelFieldFolderItem.__declaration, ModelFieldFolderItem.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					return new Declaration(ObjectType.ModelFieldFolderItem, ObjectType.ModelItem, list);
				});
			}
		}

		// Token: 0x0400032C RID: 812
		private static Declaration __declaration;

		// Token: 0x0400032D RID: 813
		private static readonly object __declarationLock = new object();
	}
}
