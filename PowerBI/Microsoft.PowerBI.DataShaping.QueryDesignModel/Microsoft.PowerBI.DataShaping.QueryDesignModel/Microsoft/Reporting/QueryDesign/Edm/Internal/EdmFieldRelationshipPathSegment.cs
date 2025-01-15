using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F4 RID: 500
	public struct EdmFieldRelationshipPathSegment
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x00041E10 File Offset: 0x00040010
		public EdmFieldRelationshipPathSegment(IEdmFieldInstance highest, IEdmFieldInstance pathIdentifier, RelationshipPathIntersectionBehavior intersectionBehavior)
		{
			this._highest = highest;
			this._pathIdentifier = pathIdentifier;
			this._intersectionBehavior = intersectionBehavior;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00041E27 File Offset: 0x00040027
		public IEdmFieldInstance Highest
		{
			get
			{
				return this._highest;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00041E2F File Offset: 0x0004002F
		public IEdmFieldInstance PathIdentifier
		{
			get
			{
				return this._pathIdentifier;
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00041E38 File Offset: 0x00040038
		internal static IEnumerable<EdmFieldRelationshipPathSegment> GroupThenIntersectPathSegments(IEnumerable<EdmFieldRelationshipPathSegment> pathSegments)
		{
			return from pathGroup in pathSegments.GroupBy(delegate(EdmFieldRelationshipPathSegment pathSegment)
				{
					EdmFieldRelationshipPathSegment edmFieldRelationshipPathSegment = pathSegment;
					return edmFieldRelationshipPathSegment.PathIdentifier;
				})
				select EdmFieldRelationshipPathSegment.IntersectPathSegments(pathGroup);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00041E90 File Offset: 0x00040090
		internal static IEnumerable<EdmFieldRelationshipPathSegment> GetAllPathSegmentsForField(IEdmFieldInstance fieldInstance, RelationshipPathDirection pathDirection, RelationshipPathIntersectionBehavior intersectionBehavior)
		{
			EntitySet entitySet = fieldInstance.Entity;
			EdmField field = fieldInstance.Field;
			IEdmFieldInstance alternateTop = null;
			if (pathDirection == RelationshipPathDirection.Lower)
			{
				if (field.Relationship.RelatedToSource != null)
				{
					alternateTop = entitySet.PropertyInstance(field.Relationship.RelatedToSource).ToIEdmFieldInstance();
				}
				else
				{
					alternateTop = EdmFieldInstance.Empty;
				}
			}
			else if (pathDirection == RelationshipPathDirection.CurrentOrLower)
			{
				alternateTop = fieldInstance;
			}
			return from pathMembershipField in field.Relationship.PathMemberships
				select entitySet.PropertyInstance(pathMembershipField).ToIEdmFieldInstance() into relationFieldInstance
				select new EdmFieldRelationshipPathSegment(alternateTop ?? relationFieldInstance, relationFieldInstance, intersectionBehavior);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00041F48 File Offset: 0x00040148
		private static EdmFieldRelationshipPathSegment IntersectPathSegments(IEnumerable<EdmFieldRelationshipPathSegment> pathSegments)
		{
			IEnumerable<EdmFieldRelationshipPathSegment> enumerable = pathSegments.Where((EdmFieldRelationshipPathSegment p) => p._intersectionBehavior == RelationshipPathIntersectionBehavior.NonIntersectable);
			if (enumerable.Any<EdmFieldRelationshipPathSegment>())
			{
				return enumerable.First<EdmFieldRelationshipPathSegment>();
			}
			return EdmFieldRelationship.GetLowestAttributeRelationshipField<EdmFieldRelationshipPathSegment>(pathSegments, (EdmFieldRelationshipPathSegment it) => it.Highest.Field);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00041FAF File Offset: 0x000401AF
		public bool ContainsField(IEdmFieldInstance fieldInstance)
		{
			return this._highest.IsValid && this._highest.Field.Relationship.DistanceToLowestRelatedField > fieldInstance.Field.Relationship.DistanceToLowestRelatedField;
		}

		// Token: 0x04000CC9 RID: 3273
		private readonly IEdmFieldInstance _highest;

		// Token: 0x04000CCA RID: 3274
		private readonly IEdmFieldInstance _pathIdentifier;

		// Token: 0x04000CCB RID: 3275
		private readonly RelationshipPathIntersectionBehavior _intersectionBehavior;
	}
}
