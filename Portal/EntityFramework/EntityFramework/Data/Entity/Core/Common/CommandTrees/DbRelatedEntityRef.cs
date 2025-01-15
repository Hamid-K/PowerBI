using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006DB RID: 1755
	internal sealed class DbRelatedEntityRef
	{
		// Token: 0x06005167 RID: 20839 RVA: 0x001237FC File Offset: 0x001219FC
		internal DbRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntityRef)
		{
			if (sourceEnd.DeclaringType != targetEnd.DeclaringType)
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship, "targetEnd");
			}
			if (sourceEnd == targetEnd)
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd, "targetEnd");
			}
			if (targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne, "targetEnd");
			}
			if (!TypeSemantics.IsReferenceType(targetEntityRef.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEntityNotRef, "targetEntityRef");
			}
			EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(targetEnd.TypeUsage).ElementType;
			EntityTypeBase elementType2 = TypeHelpers.GetEdmType<RefType>(targetEntityRef.ResultType).ElementType;
			if (!elementType.EdmEquals(elementType2) && !TypeSemantics.IsSubTypeOf(elementType2, elementType))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEntityNotCompatible, "targetEntityRef");
			}
			this._targetEntityRef = targetEntityRef;
			this._targetEnd = targetEnd;
			this._sourceEnd = sourceEnd;
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005168 RID: 20840 RVA: 0x001238D8 File Offset: 0x00121AD8
		internal RelationshipEndMember SourceEnd
		{
			get
			{
				return this._sourceEnd;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005169 RID: 20841 RVA: 0x001238E0 File Offset: 0x00121AE0
		internal RelationshipEndMember TargetEnd
		{
			get
			{
				return this._targetEnd;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x001238E8 File Offset: 0x00121AE8
		internal DbExpression TargetEntityReference
		{
			get
			{
				return this._targetEntityRef;
			}
		}

		// Token: 0x04001DBE RID: 7614
		private readonly RelationshipEndMember _sourceEnd;

		// Token: 0x04001DBF RID: 7615
		private readonly RelationshipEndMember _targetEnd;

		// Token: 0x04001DC0 RID: 7616
		private readonly DbExpression _targetEntityRef;
	}
}
