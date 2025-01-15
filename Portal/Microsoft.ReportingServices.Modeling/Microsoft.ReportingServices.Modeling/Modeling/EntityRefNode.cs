using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A5 RID: 165
	public sealed class EntityRefNode : ExpressionNode
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public EntityRefNode(IQueryEntity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001BEB7 File Offset: 0x0001A0B7
		internal EntityRefNode()
		{
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001BEBF File Offset: 0x0001A0BF
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return this.m_entity;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001BEC7 File Offset: 0x0001A0C7
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001BECF File Offset: 0x0001A0CF
		public IQueryEntity Entity
		{
			get
			{
				return this.m_entity;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (EntityRefNode.IsBadIQueryEntity(value))
				{
					throw new ArgumentOutOfRangeException(DevExceptionMessages.EntityRefNode_UnexpectedIQueryEntity);
				}
				base.CheckWriteable();
				this.m_entity = (IQueryEntityInternal)value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001BF04 File Offset: 0x0001A104
		public ModelEntity ModelEntity
		{
			get
			{
				return this.m_entity.ModelEntity;
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001BF11 File Offset: 0x0001A111
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			return new EntityRefNode(this.m_entity);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001BF20 File Offset: 0x0001A120
		public override bool IsSameAs(ExpressionNode other)
		{
			EntityRefNode entityRefNode = other as EntityRefNode;
			return entityRefNode != null && this.m_entity == entityRefNode.Entity;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001BF47 File Offset: 0x0001A147
		public override bool IsSubtreeAnchored()
		{
			return true;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001BF4A File Offset: 0x0001A14A
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			return this.m_entity.IsInvalidRefTarget;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001BF57 File Offset: 0x0001A157
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "EntityID")
			{
				xr.Context.AddReference(this, xr.ReadReferenceByID("EntityRef.EntityID", true));
				return true;
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001BF94 File Offset: 0x0001A194
		internal override bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "EntityRef.EntityID")
			{
				this.m_entity = ctx.CurrentModel.TryGetModelItem<ModelEntity>(reference, ctx.Validation);
				return true;
			}
			return base.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001BFCB File Offset: 0x0001A1CB
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("EntityRef");
			if (this.m_entity.ModelEntity != null)
			{
				xw.WriteReferenceElement("EntityID", this.m_entity.ModelEntity);
			}
			xw.WriteEndElement();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001C004 File Offset: 0x0001A204
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			replacementExpr = null;
			if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.m_entity.IsInvalidRefTarget)
			{
				if (this.m_entity.ModelEntity != null)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("EntityRef.EntityID", ctx.CurrentObjectDescriptor, this.m_entity.ModelEntity.ID.ToString()));
					return null;
				}
				throw new InternalModelingException("Null or unrecognized IQueryEntity");
			}
			else
			{
				if (!ctx.CheckContextEntityMatch(this.m_entity, "EntityRef", !topLevel))
				{
					return null;
				}
				return new ResultType?(new ResultType(this.m_entity, this.m_entity.EntityRefIsNullable));
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			throw new InternalModelingException("SetEntityKeyTarget is called on EntityRefNode.");
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001C0D7 File Offset: 0x0001A2D7
		internal override Expression VisitAggregationFloatPoints(ExpressionNode.FloatPointVisitor visitor, bool allowExprModification)
		{
			throw new InternalModelingException("VisitAggregationFloatPoints called on EntityRefNode");
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001C0E3 File Offset: 0x0001A2E3
		internal static bool IsBadIQueryEntity(IQueryEntity qe)
		{
			return qe.ModelEntity == null || (qe.ModelEntity != null && qe.ModelEntity != qe) || !(qe is IQueryEntityInternal);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001C10C File Offset: 0x0001A30C
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("EntityRefNode is not compiled");
			}
			if (this.m_entity.ModelEntity == null)
			{
				throw new InternalModelingException("Null or unrecognized IQueryEntity");
			}
			IQueryEntity queryEntity = newModel.LookupItemByID(this.m_entity.ModelEntity.ID) as ModelEntity;
			if (queryEntity == null)
			{
				return null;
			}
			EntityRefNode entityRefNode = new EntityRefNode(queryEntity);
			entityRefNode.SetCompiledIndicator();
			return entityRefNode;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001C174 File Offset: 0x0001A374
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(EntityRefNode.Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName != MemberName.Entity)
				{
					throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
				}
				PersistenceHelper.WriteIQueryEntityReference(ref writer, this.m_entity);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(EntityRefNode.Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName != MemberName.Entity)
				{
					throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
				}
				this.m_entity = PersistenceHelper.ReadIQueryEntityReference(ref reader, this);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C260 File Offset: 0x0001A460
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			List<MemberReference> list;
			if (memberReferencesCollection.TryGetValue(EntityRefNode.Declaration.ObjectType, out list))
			{
				foreach (MemberReference memberReference in list)
				{
					if (memberReference.MemberName != MemberName.Entity)
					{
						throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
					}
					this.m_entity = PersistenceHelper.ResolveIQueryEntityReference(referenceableItems[memberReference.RefID]);
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001C304 File Offset: 0x0001A504
		internal override ObjectType GetObjectType()
		{
			return ObjectType.EntityRefNode;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001C308 File Offset: 0x0001A508
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref EntityRefNode.__declaration, EntityRefNode.__declarationLock, () => new Declaration(ObjectType.EntityRefNode, ObjectType.ExpressionNode, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Entity, ObjectType.ModelingObject, Token.Reference)
				}));
			}
		}

		// Token: 0x040003C8 RID: 968
		internal const string EntityRefElem = "EntityRef";

		// Token: 0x040003C9 RID: 969
		private const string EntityIdElem = "EntityID";

		// Token: 0x040003CA RID: 970
		private const string EntityIdProperty = "EntityRef.EntityID";

		// Token: 0x040003CB RID: 971
		private IQueryEntityInternal m_entity;

		// Token: 0x040003CC RID: 972
		private static Declaration __declaration;

		// Token: 0x040003CD RID: 973
		private static readonly object __declarationLock = new object();
	}
}
