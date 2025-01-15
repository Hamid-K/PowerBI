using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling.Linguistics;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000AE RID: 174
	public sealed class RolePathItem : PathItem, IXmlLoadable, IDeserializationCallback
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0001EC39 File Offset: 0x0001CE39
		public RolePathItem(ModelRole role)
		{
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			this.m_role = role;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001EC56 File Offset: 0x0001CE56
		internal RolePathItem()
		{
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001EC5E File Offset: 0x0001CE5E
		public override string Name
		{
			get
			{
				return this.m_role.Name;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001EC6B File Offset: 0x0001CE6B
		public override ILinguisticInfo Linguistics
		{
			get
			{
				return this.m_role.Linguistics;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001EC78 File Offset: 0x0001CE78
		public override Cardinality Cardinality
		{
			get
			{
				return this.m_role.Cardinality;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001EC85 File Offset: 0x0001CE85
		public override Optionality Optionality
		{
			get
			{
				return this.m_role.Optionality;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001EC92 File Offset: 0x0001CE92
		public override Cardinality ReverseCardinality
		{
			get
			{
				return this.m_role.RelatedRole.Cardinality;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		public override Optionality ReverseOptionality
		{
			get
			{
				return this.m_role.RelatedRole.Optionality;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001ECB6 File Offset: 0x0001CEB6
		public override IQueryEntity TargetEntity
		{
			get
			{
				return this.m_role.RelatedEntity;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001ECC3 File Offset: 0x0001CEC3
		public override IQueryEntity SourceEntity
		{
			get
			{
				return this.m_role.Entity;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		public ModelRole Role
		{
			get
			{
				return this.m_role;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		internal override string PropertyName
		{
			get
			{
				return "RolePathItem";
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public override bool Equals(object obj)
		{
			RolePathItem rolePathItem = obj as RolePathItem;
			return rolePathItem != null && rolePathItem.Role == this.m_role;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001ED07 File Offset: 0x0001CF07
		public override int GetHashCode()
		{
			if (this.m_role == null)
			{
				return 0;
			}
			return this.m_role.GetHashCode();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001ED1E File Offset: 0x0001CF1E
		public override PathItem CreateReverse()
		{
			return new RolePathItem(this.m_role.RelatedRole);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001ED30 File Offset: 0x0001CF30
		internal override void AddOutOfContextError(CompilationContext ctx)
		{
			ctx.AddOutOfContextError(this.m_role, this.PropertyName, true);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001ED48 File Offset: 0x0001CF48
		internal override bool CheckInvalidRefs(CompilationContext ctx, bool forceCheck)
		{
			if (!ctx.ShouldCheckInvalidRefsDuringCompilation && !forceCheck)
			{
				return true;
			}
			if (this.HasInvalidRefs())
			{
				if (this.m_role.IsInvalidRefTarget)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("RolePathItem.RoleID", ctx.CurrentObjectDescriptor, this.m_role.ID.ToString()));
				}
				else
				{
					ctx.PushScope(this.m_role);
					try
					{
						this.m_role.ValidateRelatedRoleReference(ctx, forceCheck);
					}
					finally
					{
						ctx.PopScope();
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		internal override bool HasInvalidRefs()
		{
			if (this.m_role.IsInvalidRefTarget)
			{
				return true;
			}
			if (this.m_role.RelatedRole == null || this.m_role.RelatedRole.IsInvalidRefTarget)
			{
				return true;
			}
			if (this.SourceEntity == null || this.SourceEntity.IsInvalidRefTarget || this.TargetEntity == null || this.TargetEntity.IsInvalidRefTarget)
			{
				throw new InternalModelingException("Null or invalid ref entity on a non-invalid ref model role.");
			}
			return false;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001EE53 File Offset: 0x0001D053
		internal override void Load(ModelingXmlReader xr)
		{
			xr.LoadObject("RolePathItem", this);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001EE61 File Offset: 0x0001D061
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001EE64 File Offset: 0x0001D064
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "RoleID")
			{
				xr.Context.AddReference(this, xr.ReadReferenceByID("RolePathItem.RoleID", true));
				return true;
			}
			return false;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001EE9B File Offset: 0x0001D09B
		bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "RolePathItem.RoleID")
			{
				this.m_role = ctx.CurrentModel.TryGetModelItem<ModelRole>(reference, ctx.Validation);
				return true;
			}
			return false;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001EECB File Offset: 0x0001D0CB
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("RolePathItem");
			xw.WriteReferenceElement("RoleID", this.m_role);
			xw.WriteEndElement();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
		internal override PathItem CloneFor(SemanticModel newModel)
		{
			ModelRole modelRole = newModel.LookupItemByID(this.m_role.ID) as ModelRole;
			if (modelRole == null)
			{
				return null;
			}
			return new RolePathItem(modelRole);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001EF20 File Offset: 0x0001D120
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RolePathItem.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Role)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteModelingObjectReference<ModelRole>(ref writer, this.m_role);
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001EF94 File Offset: 0x0001D194
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			RolePathItem.RolePathItemResolveReferences rolePathItemResolveReferences = new RolePathItem.RolePathItemResolveReferences(this);
			reader.RegisterDeclaration(RolePathItem.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.Role)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_role = PersistenceHelper.ReadModelingObjectReference<ModelRole>(ref reader, rolePathItemResolveReferences);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001F010 File Offset: 0x0001D210
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001F01C File Offset: 0x0001D21C
		internal override ObjectType GetObjectType()
		{
			return ObjectType.RolePathItem;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001F020 File Offset: 0x0001D220
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref RolePathItem.__declaration, RolePathItem.__declarationLock, () => new Declaration(ObjectType.RolePathItem, ObjectType.PathItem, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Role, ObjectType.ModelRole, Token.Reference)
				}));
			}
		}

		// Token: 0x040003E8 RID: 1000
		internal const string RolePathItemElem = "RolePathItem";

		// Token: 0x040003E9 RID: 1001
		private const string RoleIdElem = "RoleID";

		// Token: 0x040003EA RID: 1002
		private const string RoleIdProperty = "RolePathItem.RoleID";

		// Token: 0x040003EB RID: 1003
		private ModelRole m_role;

		// Token: 0x040003EC RID: 1004
		private static Declaration __declaration;

		// Token: 0x040003ED RID: 1005
		private static readonly object __declarationLock = new object();

		// Token: 0x020001A6 RID: 422
		private sealed class RolePathItemResolveReferences : IPersistable
		{
			// Token: 0x060010B1 RID: 4273 RVA: 0x00034684 File Offset: 0x00032884
			internal RolePathItemResolveReferences(RolePathItem owner)
			{
				this.m_owner = owner;
			}

			// Token: 0x060010B2 RID: 4274 RVA: 0x00034693 File Offset: 0x00032893
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				throw new InternalModelingException("Serialize is not supported.");
			}

			// Token: 0x060010B3 RID: 4275 RVA: 0x0003469F File Offset: 0x0003289F
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x060010B4 RID: 4276 RVA: 0x000346AC File Offset: 0x000328AC
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(RolePathItem.Declaration.ObjectType, out list))
				{
					foreach (MemberReference memberReference in list)
					{
						if (memberReference.MemberName != MemberName.Role)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						this.m_owner.m_role = PersistenceHelper.ResolveModelingObjectReference<ModelRole>(referenceableItems[memberReference.RefID]);
					}
				}
			}

			// Token: 0x060010B5 RID: 4277 RVA: 0x00034754 File Offset: 0x00032954
			ObjectType IPersistable.GetObjectType()
			{
				throw new InternalModelingException("GetObjectType is not supported.");
			}

			// Token: 0x040006F6 RID: 1782
			private readonly RolePathItem m_owner;
		}
	}
}
