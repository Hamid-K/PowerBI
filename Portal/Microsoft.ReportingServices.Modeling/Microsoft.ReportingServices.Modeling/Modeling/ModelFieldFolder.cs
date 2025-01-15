using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200008A RID: 138
	public sealed class ModelFieldFolder : ModelFieldFolderItem, IFolderItem
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00013D0C File Offset: 0x00011F0C
		public ModelFieldFolder()
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00013D14 File Offset: 0x00011F14
		internal override void Reset()
		{
			base.Reset();
			this.__fields = new ModelFieldFolderItemCollection(this);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00013D28 File Offset: 0x00011F28
		internal ModelFieldFolder(ModelFieldFolder baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00013D32 File Offset: 0x00011F32
		public ModelFieldFolderItemCollection Fields
		{
			get
			{
				if (this.__fields == null)
				{
					this.__fields = this.BaseItem.Fields.CloneFor(this);
				}
				return this.__fields;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00013D59 File Offset: 0x00011F59
		internal override string ElementName
		{
			get
			{
				return "FieldFolder";
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00013D60 File Offset: 0x00011F60
		internal new ModelFieldFolder BaseItem
		{
			get
			{
				return (ModelFieldFolder)base.BaseItem;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013D6D File Offset: 0x00011F6D
		public override IEnumerable<ModelItem> GetOwnedItems()
		{
			return this.Fields;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00013D75 File Offset: 0x00011F75
		IOwnedModelItemCollection IFolderItem.Items
		{
			get
			{
				return this.Fields;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013D7D File Offset: 0x00011F7D
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "Fields")
			{
				this.Fields.Load(xr);
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00013DAE File Offset: 0x00011FAE
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			this.Fields.WriteTo(xw, "Fields");
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00013DC8 File Offset: 0x00011FC8
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new ModelFieldFolder(this, markAsHidden);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00013DD4 File Offset: 0x00011FD4
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelFieldFolder.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Fields)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				((IPersistable)this.Fields).Serialize(writer);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00013E48 File Offset: 0x00012048
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelFieldFolder.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Fields)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					((IPersistable)this.Fields).Deserialize(reader);
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00013EE0 File Offset: 0x000120E0
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00013EEC File Offset: 0x000120EC
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ModelFieldFolder;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00013EF0 File Offset: 0x000120F0
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelFieldFolder.__declaration, ModelFieldFolder.__declarationLock, () => new Declaration(ObjectType.ModelFieldFolder, ObjectType.ModelFieldFolderItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Fields, ObjectType.OwnedModelItemCollection)
				}));
			}
		}

		// Token: 0x04000328 RID: 808
		internal const string FieldFolderElem = "FieldFolder";

		// Token: 0x04000329 RID: 809
		private ModelFieldFolderItemCollection __fields;

		// Token: 0x0400032A RID: 810
		private static Declaration __declaration;

		// Token: 0x0400032B RID: 811
		private static readonly object __declarationLock = new object();
	}
}
