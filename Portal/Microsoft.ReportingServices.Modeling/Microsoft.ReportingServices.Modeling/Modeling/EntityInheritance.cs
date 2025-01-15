using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000084 RID: 132
	public sealed class EntityInheritance : IBindingContext, IXmlLoadable, IDeserializationCallback, IPersistable
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x00012EA8 File Offset: 0x000110A8
		public EntityInheritance(ModelEntity inheritsFrom)
		{
			this.InheritsFrom = inheritsFrom;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012EB7 File Offset: 0x000110B7
		private EntityInheritance(ModelingXmlReader xr)
		{
			xr.LoadObject("Inheritance", this);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00012ECB File Offset: 0x000110CB
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00012ED3 File Offset: 0x000110D3
		public ModelEntity InheritsFrom
		{
			get
			{
				return this.m_inheritsFrom;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.CheckWriteable();
				EntityInheritance.ValidateInheritance(value, this.m_owner);
				this.m_inheritsFrom = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00012EFC File Offset: 0x000110FC
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00012F04 File Offset: 0x00011104
		public RelationBinding Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				this.CheckWriteable();
				if (value != null)
				{
					value.SetContext(this);
				}
				if (this.m_binding != null)
				{
					this.m_binding.SetContext(null);
				}
				this.m_binding = value;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00012F31 File Offset: 0x00011131
		DataSourceView IBindingContext.GetDataSourceView()
		{
			if (this.m_owner == null)
			{
				return null;
			}
			return ((IBindingContext)this.m_owner).GetDataSourceView();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00012F48 File Offset: 0x00011148
		Binding IBindingContext.GetParentBinding()
		{
			if (this.m_owner == null)
			{
				return null;
			}
			return this.m_owner.Binding;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012F5F File Offset: 0x0001115F
		public static IEnumerable<RelationBinding> ListPotentialBindings(ModelEntity entity, ModelEntity inheritsFromEntity)
		{
			return RelationBinding.ListBindings((entity != null) ? entity.Binding : null, (inheritsFromEntity != null) ? inheritsFromEntity.Binding : null, false);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012F7F File Offset: 0x0001117F
		internal static EntityInheritance FromReader(ModelingXmlReader xr)
		{
			return new EntityInheritance(xr);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012F87 File Offset: 0x00011187
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00012F8C File Offset: 0x0001118C
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "InheritsFromEntityID")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByID("Inheritance.InheritsFromEntityID", false));
					return true;
				}
				if (localName == "Relation")
				{
					this.Binding = RelationBinding.FromReader(xr, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00012FEE File Offset: 0x000111EE
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "Inheritance.InheritsFromEntityID")
			{
				this.InheritsFrom = ctx.CurrentModel.TryGetModelItem<ModelEntity>(reference, ctx.Validation);
				return true;
			}
			return false;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013020 File Offset: 0x00011220
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Inheritance");
			xw.WriteReferenceElement("InheritsFromEntityID", this.m_inheritsFrom);
			if (this.m_binding != null && xw.ShouldWriteBindings)
			{
				this.m_binding.WriteTo(xw, false);
			}
			xw.WriteEndElement();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001306C File Offset: 0x0001126C
		internal void Compile(CompilationContext ctx)
		{
			if (this.m_owner == null)
			{
				throw new InternalModelingException("m_owner is null");
			}
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_inheritsFrom.IsInvalidRefTarget)
			{
				ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("Inheritance.InheritsFromEntityID", ctx.CurrentObjectDescriptor, this.m_inheritsFrom.ID.ToString()));
				return;
			}
			if (this.m_owner.Model != this.m_inheritsFrom.Model)
			{
				ctx.AddScopedError(ModelingErrorCode.WrongSemanticModel, SRErrors.WrongSemanticModel_ModelItem("Inheritance.InheritsFromEntityID", ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_inheritsFrom)));
				return;
			}
			if (ctx.ShouldCheckBindings)
			{
				if (this.m_binding == null)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingBinding, SRErrors.MissingBinding_Inheritance(ctx.CurrentObjectDescriptor));
					return;
				}
				DsvRelation dsvRelation;
				if (this.m_binding.CheckBinding(ctx, "Inheritance.Relation", out dsvRelation))
				{
					DsvTable dsvTable = ((this.m_owner.Binding != null) ? this.m_owner.Binding.GetTable() : null);
					DsvTable dsvTable2 = ((this.m_inheritsFrom.Binding != null) ? this.m_inheritsFrom.Binding.GetTable() : null);
					if (dsvTable != null && dsvTable2 != null)
					{
						if (dsvTable != dsvRelation.SourceTable || dsvTable2 != dsvRelation.TargetTable)
						{
							ctx.AddScopedError(ModelingErrorCode.InvalidInheritanceRelationTable, SRErrors.InvalidInheritanceRelationTable("Inheritance.Relation", ctx.CurrentObjectDescriptor, this.m_binding.GetRelationDescriptor(), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable.Name), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable2.Name)));
							return;
						}
						if (Microsoft.ReportingServices.Modeling.Binding.CheckColumns(dsvRelation.SourceColumns, this.m_owner.Binding, ctx, "Inheritance.Relation") && Microsoft.ReportingServices.Modeling.Binding.CheckColumns(dsvRelation.TargetColumns, this.m_inheritsFrom.Binding, ctx, "Inheritance.Relation"))
						{
							if (!Microsoft.ReportingServices.Modeling.Binding.AreColumnsUnique(dsvRelation.SourceColumns, this.m_owner.Binding))
							{
								ctx.AddScopedWarning(ModelingErrorCode.NonUniqueInheritanceRelationColumns, SRErrors.NonUniqueInheritanceRelationColumns("Inheritance.Relation", ctx.CurrentObjectDescriptor, this.m_binding.GetRelationDescriptor(), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable.Name)));
							}
							if (!Microsoft.ReportingServices.Modeling.Binding.AreColumnsUnique(dsvRelation.TargetColumns, this.m_inheritsFrom.Binding))
							{
								ctx.AddScopedWarning(ModelingErrorCode.NonUniqueInheritanceRelationColumns, SRErrors.NonUniqueInheritanceRelationColumns("Inheritance.Relation", ctx.CurrentObjectDescriptor, this.m_binding.GetRelationDescriptor(), Microsoft.ReportingServices.Modeling.Binding.GetTableDescriptor(dsvTable2.Name)));
							}
						}
					}
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000132B8 File Offset: 0x000114B8
		internal EntityInheritance CloneFor(SemanticModel newModel)
		{
			ModelEntity modelEntity = newModel.LookupItemByID(this.m_inheritsFrom.ID) as ModelEntity;
			if (modelEntity == null)
			{
				return null;
			}
			return new EntityInheritance(modelEntity);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000132E7 File Offset: 0x000114E7
		internal void SetOwner(ModelEntity newOwner)
		{
			if (this.m_owner != null && newOwner != null)
			{
				throw new InvalidOperationException(DevExceptionMessages.ExistingOwner);
			}
			EntityInheritance.ValidateInheritance(this.m_inheritsFrom, newOwner);
			this.m_owner = newOwner;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013312 File Offset: 0x00011512
		private void CheckWriteable()
		{
			if (this.m_owner != null)
			{
				this.m_owner.CheckWriteable();
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00013328 File Offset: 0x00011528
		private static void ValidateInheritance(ModelEntity inheritsFrom, ModelEntity owner)
		{
			if (owner == null)
			{
				return;
			}
			for (ModelEntity modelEntity = inheritsFrom; modelEntity != null; modelEntity = ((modelEntity.Inheritance != null) ? modelEntity.Inheritance.InheritsFrom : null))
			{
				if (modelEntity == owner)
				{
					throw new ValidationException(ModelingErrorCode.CircularInheritance, owner, SRErrors.CircularInheritance("Inheritance.InheritsFromEntityID", SRObjectDescriptor.FromScope(owner)));
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00013374 File Offset: 0x00011574
		internal EntityInheritance()
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001337C File Offset: 0x0001157C
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(EntityInheritance.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Binding)
				{
					if (memberName != MemberName.InheritsFrom)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteModelingObjectReference<ModelEntity>(ref writer, this.m_inheritsFrom);
				}
				else
				{
					writer.Write(this.m_binding);
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00013400 File Offset: 0x00011600
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(EntityInheritance.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Binding)
				{
					if (memberName != MemberName.InheritsFrom)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_inheritsFrom = PersistenceHelper.ReadModelingObjectReference<ModelEntity>(ref reader, this);
				}
				else
				{
					this.Binding = (RelationBinding)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001348C File Offset: 0x0001168C
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list;
			if (memberReferencesCollection.TryGetValue(EntityInheritance.Declaration.ObjectType, out list))
			{
				foreach (MemberReference memberReference in list)
				{
					if (memberReference.MemberName != MemberName.InheritsFrom)
					{
						throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
					}
					this.m_inheritsFrom = PersistenceHelper.ResolveModelingObjectReference<ModelEntity>(referenceableItems[memberReference.RefID]);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00013530 File Offset: 0x00011730
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.EntityInheritance;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00013534 File Offset: 0x00011734
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref EntityInheritance.__declaration, EntityInheritance.__declarationLock, () => new Declaration(ObjectType.EntityInheritance, ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.InheritsFrom, ObjectType.ModelEntity, Token.Reference),
					new MemberInfo(MemberName.Binding, ObjectType.RelationBinding)
				}));
			}
		}

		// Token: 0x04000314 RID: 788
		internal const string InheritanceElem = "Inheritance";

		// Token: 0x04000315 RID: 789
		private const string InheritsFromEntityIdElem = "InheritsFromEntityID";

		// Token: 0x04000316 RID: 790
		private const string InheritsFromEntityIdProperty = "Inheritance.InheritsFromEntityID";

		// Token: 0x04000317 RID: 791
		private const string RelationProperty = "Inheritance.Relation";

		// Token: 0x04000318 RID: 792
		private ModelEntity m_inheritsFrom;

		// Token: 0x04000319 RID: 793
		private ModelEntity m_owner;

		// Token: 0x0400031A RID: 794
		private RelationBinding m_binding;

		// Token: 0x0400031B RID: 795
		private static Declaration __declaration;

		// Token: 0x0400031C RID: 796
		private static readonly object __declarationLock = new object();
	}
}
