using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.Linguistics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000AF RID: 175
	public sealed class InheritancePathItem : PathItem
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x0001F05C File Offset: 0x0001D25C
		internal InheritancePathItem(IQueryEntity sourceEntity, IQueryEntity targetEntity)
		{
			if (sourceEntity == null || targetEntity == null)
			{
				throw new InternalModelingException("sourceEntity or targetEntity is null");
			}
			if (sourceEntity.InheritsFrom != targetEntity && targetEntity.InheritsFrom != sourceEntity)
			{
				throw new InternalModelingException("Either source or target entity must inherit from other entity");
			}
			this.m_sourceEntity = sourceEntity;
			this.m_targetEntity = targetEntity;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001F0AB File Offset: 0x0001D2AB
		public override string Name
		{
			get
			{
				return this.m_targetEntity.Name;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
		public override ILinguisticInfo Linguistics
		{
			get
			{
				return this.m_targetEntity.Linguistics;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001F0C5 File Offset: 0x0001D2C5
		public override Cardinality Cardinality
		{
			get
			{
				return Cardinality.One;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001F0C8 File Offset: 0x0001D2C8
		public override Optionality Optionality
		{
			get
			{
				if (this.m_sourceEntity.InheritsFrom != this.m_targetEntity)
				{
					return Optionality.Optional;
				}
				return Optionality.Required;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public override Cardinality ReverseCardinality
		{
			get
			{
				return Cardinality.One;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		public override Optionality ReverseOptionality
		{
			get
			{
				if (this.m_sourceEntity.InheritsFrom != this.m_targetEntity)
				{
					return Optionality.Required;
				}
				return Optionality.Optional;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001F0FB File Offset: 0x0001D2FB
		public override IQueryEntity TargetEntity
		{
			get
			{
				return this.m_targetEntity;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001F103 File Offset: 0x0001D303
		public override IQueryEntity SourceEntity
		{
			get
			{
				return this.m_sourceEntity;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0001F10B File Offset: 0x0001D30B
		internal override string PropertyName
		{
			get
			{
				throw new InternalModelingException("InheritancePathItem.PropertyName should not be called");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001F117 File Offset: 0x0001D317
		internal override bool ShouldSerialize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001F11C File Offset: 0x0001D31C
		public override bool Equals(object obj)
		{
			InheritancePathItem inheritancePathItem = obj as InheritancePathItem;
			return inheritancePathItem != null && inheritancePathItem.TargetEntity == this.m_targetEntity && inheritancePathItem.SourceEntity == this.m_sourceEntity;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001F151 File Offset: 0x0001D351
		public override int GetHashCode()
		{
			return this.m_targetEntity.GetHashCode();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001F15E File Offset: 0x0001D35E
		public override PathItem CreateReverse()
		{
			return new InheritancePathItem(this.m_targetEntity, this.m_sourceEntity);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001F171 File Offset: 0x0001D371
		internal override void AddOutOfContextError(CompilationContext ctx)
		{
			throw new InternalModelingException("InheritancePathItem.AddOutOfContextError should not be called.");
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001F17D File Offset: 0x0001D37D
		internal override bool CheckInvalidRefs(CompilationContext ctx, bool forceCheck)
		{
			return (!ctx.ShouldCheckInvalidRefsDuringCompilation && !forceCheck) || !this.HasInvalidRefs();
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001F195 File Offset: 0x0001D395
		internal override bool HasInvalidRefs()
		{
			return this.SourceEntity.IsInvalidRefTarget || this.TargetEntity.IsInvalidRefTarget;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001F1B1 File Offset: 0x0001D3B1
		internal override void Load(ModelingXmlReader xr)
		{
			throw new InternalModelingException("InheritancePathItem.Load should not be called");
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001F1BD File Offset: 0x0001D3BD
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			throw new InternalModelingException("InheritancePathItem.WriteTo should not be called");
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001F1C9 File Offset: 0x0001D3C9
		internal override PathItem CloneFor(SemanticModel newModel)
		{
			throw new InternalModelingException("InheritancePathItem.CloneFor should not be called");
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001F1D5 File Offset: 0x0001D3D5
		internal InheritancePathItem()
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(InheritancePathItem.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.SourceEntity)
				{
					if (memberName != MemberName.TargetEntity)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					PersistenceHelper.WriteIQueryEntityReference(ref writer, this.m_targetEntity);
				}
				else
				{
					PersistenceHelper.WriteIQueryEntityReference(ref writer, this.m_sourceEntity);
				}
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001F26C File Offset: 0x0001D46C
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			InheritancePathItem.InheritancePathItemResolveReferences inheritancePathItemResolveReferences = new InheritancePathItem.InheritancePathItemResolveReferences(this);
			reader.RegisterDeclaration(InheritancePathItem.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.SourceEntity)
				{
					if (memberName != MemberName.TargetEntity)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_targetEntity = PersistenceHelper.ReadIQueryEntityReference(ref reader, inheritancePathItemResolveReferences);
				}
				else
				{
					this.m_sourceEntity = PersistenceHelper.ReadIQueryEntityReference(ref reader, inheritancePathItemResolveReferences);
				}
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001F301 File Offset: 0x0001D501
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001F30D File Offset: 0x0001D50D
		internal override ObjectType GetObjectType()
		{
			return ObjectType.InheritancePathItem;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001F311 File Offset: 0x0001D511
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref InheritancePathItem.__declaration, InheritancePathItem.__declarationLock, () => new Declaration(ObjectType.InheritancePathItem, ObjectType.PathItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.SourceEntity, ObjectType.ModelingObject, Token.Reference),
					new MemberInfo(MemberName.TargetEntity, ObjectType.ModelingObject, Token.Reference)
				}));
			}
		}

		// Token: 0x040003EE RID: 1006
		private IQueryEntity m_sourceEntity;

		// Token: 0x040003EF RID: 1007
		private IQueryEntity m_targetEntity;

		// Token: 0x040003F0 RID: 1008
		private static Declaration __declaration;

		// Token: 0x040003F1 RID: 1009
		private static readonly object __declarationLock = new object();

		// Token: 0x020001A8 RID: 424
		private sealed class InheritancePathItemResolveReferences : IPersistable
		{
			// Token: 0x060010B9 RID: 4281 RVA: 0x000347A1 File Offset: 0x000329A1
			internal InheritancePathItemResolveReferences(InheritancePathItem owner)
			{
				this.m_owner = owner;
			}

			// Token: 0x060010BA RID: 4282 RVA: 0x000347B0 File Offset: 0x000329B0
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				throw new InternalModelingException("Serialize is not supported.");
			}

			// Token: 0x060010BB RID: 4283 RVA: 0x000347BC File Offset: 0x000329BC
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x060010BC RID: 4284 RVA: 0x000347C8 File Offset: 0x000329C8
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(InheritancePathItem.Declaration.ObjectType, out list))
				{
					foreach (MemberReference memberReference in list)
					{
						MemberName memberName = memberReference.MemberName;
						if (memberName != MemberName.SourceEntity)
						{
							if (memberName != MemberName.TargetEntity)
							{
								throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
							}
							this.m_owner.m_targetEntity = PersistenceHelper.ResolveIQueryEntityReference(referenceableItems[memberReference.RefID]);
						}
						else
						{
							this.m_owner.m_sourceEntity = PersistenceHelper.ResolveIQueryEntityReference(referenceableItems[memberReference.RefID]);
						}
					}
				}
			}

			// Token: 0x060010BD RID: 4285 RVA: 0x000348A0 File Offset: 0x00032AA0
			ObjectType IPersistable.GetObjectType()
			{
				throw new InternalModelingException("GetObjectType is not supported.");
			}

			// Token: 0x040006F9 RID: 1785
			private readonly InheritancePathItem m_owner;
		}
	}
}
