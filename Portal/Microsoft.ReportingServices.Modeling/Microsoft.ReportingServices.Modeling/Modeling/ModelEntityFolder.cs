using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000085 RID: 133
	public sealed class ModelEntityFolder : ModelEntityFolderItem, IFolderItem
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00013570 File Offset: 0x00011770
		public ModelEntityFolder()
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00013578 File Offset: 0x00011778
		internal override void Reset()
		{
			base.Reset();
			this.__entities = new ModelEntityFolderItemCollection(this);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001358C File Offset: 0x0001178C
		internal ModelEntityFolder(ModelEntityFolder baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00013596 File Offset: 0x00011796
		public ModelEntityFolderItemCollection Entities
		{
			get
			{
				if (this.__entities == null)
				{
					this.__entities = this.BaseItem.Entities.CloneFor(this);
				}
				return this.__entities;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000135BD File Offset: 0x000117BD
		internal override string ElementName
		{
			get
			{
				return "EntityFolder";
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000135C4 File Offset: 0x000117C4
		internal new ModelEntityFolder BaseItem
		{
			get
			{
				return (ModelEntityFolder)base.BaseItem;
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000135D1 File Offset: 0x000117D1
		public override IEnumerable<ModelItem> GetOwnedItems()
		{
			return this.Entities;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000135D9 File Offset: 0x000117D9
		IOwnedModelItemCollection IFolderItem.Items
		{
			get
			{
				return this.Entities;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000135E1 File Offset: 0x000117E1
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "Entities")
			{
				this.Entities.Load(xr);
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00013612 File Offset: 0x00011812
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			this.Entities.WriteTo(xw, "Entities");
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001362C File Offset: 0x0001182C
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new ModelEntityFolder(this, markAsHidden);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00013638 File Offset: 0x00011838
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelEntityFolder.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Entities)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				((IPersistable)this.Entities).Serialize(writer);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000136AC File Offset: 0x000118AC
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelEntityFolder.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.Entities)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					((IPersistable)this.Entities).Deserialize(reader);
				}
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00013744 File Offset: 0x00011944
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00013750 File Offset: 0x00011950
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ModelEntityFolder;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00013754 File Offset: 0x00011954
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelEntityFolder.__declaration, ModelEntityFolder.__declarationLock, () => new Declaration(ObjectType.ModelEntityFolder, ObjectType.ModelEntityFolderItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Entities, ObjectType.OwnedModelItemCollection)
				}));
			}
		}

		// Token: 0x0400031D RID: 797
		internal const string EntityFolderElem = "EntityFolder";

		// Token: 0x0400031E RID: 798
		private ModelEntityFolderItemCollection __entities;

		// Token: 0x0400031F RID: 799
		private static Declaration __declaration;

		// Token: 0x04000320 RID: 800
		private static readonly object __declarationLock = new object();
	}
}
