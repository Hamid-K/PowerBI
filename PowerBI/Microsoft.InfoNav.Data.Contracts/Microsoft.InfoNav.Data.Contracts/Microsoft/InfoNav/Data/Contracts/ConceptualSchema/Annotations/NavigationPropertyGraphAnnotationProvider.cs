using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000137 RID: 311
	public sealed class NavigationPropertyGraphAnnotationProvider : IAnnotationProvider<NavigationPropertyGraphAnnotation, IConceptualSchema>
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public NavigationPropertyGraphAnnotationProvider(IConceptualSchema schema)
		{
			this._targetSchema = schema;
			this._annotation = new Lazy<NavigationPropertyGraphAnnotation>(new Func<NavigationPropertyGraphAnnotation>(this.BuildAnnotation));
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00010ADC File Offset: 0x0000ECDC
		public NavigationPropertyGraphAnnotationProvider(NavigationPropertyGraphAnnotation annotation, IConceptualSchema schema)
		{
			this._targetSchema = schema;
			this._annotation = new Lazy<NavigationPropertyGraphAnnotation>(() => annotation);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00010B1A File Offset: 0x0000ED1A
		public bool TryGetAnnotation(IConceptualSchema target, out NavigationPropertyGraphAnnotation annotation)
		{
			annotation = this._annotation.Value;
			return true;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00010B2A File Offset: 0x0000ED2A
		private NavigationPropertyGraphAnnotation BuildAnnotation()
		{
			return new NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder(this._targetSchema).BuildGraphAnnotation();
		}

		// Token: 0x040003B4 RID: 948
		private readonly Lazy<NavigationPropertyGraphAnnotation> _annotation;

		// Token: 0x040003B5 RID: 949
		private readonly IConceptualSchema _targetSchema;

		// Token: 0x0200031B RID: 795
		private sealed class NavigationPropertyGraphAnnotationBuilder
		{
			// Token: 0x06001998 RID: 6552 RVA: 0x0002DF9E File Offset: 0x0002C19E
			public NavigationPropertyGraphAnnotationBuilder(IConceptualSchema schema)
			{
				this._schema = schema;
				this._relationships = this.CollectActiveRelationships(schema);
			}

			// Token: 0x06001999 RID: 6553 RVA: 0x0002DFBC File Offset: 0x0002C1BC
			private List<NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation> CollectActiveRelationships(IConceptualSchema schema)
			{
				List<NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation> list = new List<NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation>();
				foreach (IConceptualEntity conceptualEntity in schema.Entities)
				{
					foreach (IConceptualNavigationProperty conceptualNavigationProperty in conceptualEntity.NavigationProperties)
					{
						if (conceptualNavigationProperty.IsActive)
						{
							list.Add(new NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation(conceptualEntity, conceptualNavigationProperty));
						}
					}
				}
				return list;
			}

			// Token: 0x0600199A RID: 6554 RVA: 0x0002E058 File Offset: 0x0002C258
			public NavigationPropertyGraphAnnotation BuildGraphAnnotation()
			{
				IDirectedGraph<IConceptualEntity> associationsGraph = this.GetAssociationsGraph(true, false, false, true);
				IDirectedGraph<IConceptualEntity> associationsGraph2 = this.GetAssociationsGraph(true, false, false, false);
				IDirectedGraph<IConceptualEntity> associationsGraph3 = this.GetAssociationsGraph(true, false, true, true);
				IDirectedGraph<IConceptualEntity> associationsGraph4 = this.GetAssociationsGraph(false, false, false, true);
				IDirectedGraph<IConceptualEntity> associationsGraph5 = this.GetAssociationsGraph(true, true, false, true);
				IDirectedGraph<IConceptualEntity> associationsGraph6 = this.GetAssociationsGraph(true, true, true, true);
				return new NavigationPropertyGraphAnnotation(associationsGraph, associationsGraph2, associationsGraph3, associationsGraph4, associationsGraph5, associationsGraph6);
			}

			// Token: 0x0600199B RID: 6555 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
			private IDirectedGraph<IConceptualEntity> GetAssociationsGraph(bool fromOne, bool respectModelBidi, bool includeDirectManyToMany, bool includeWeakRelationships)
			{
				DirectedGraph<IConceptualEntity> directedGraph = new DirectedGraph<IConceptualEntity>();
				foreach (NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation relationshipAnnotation in this._relationships)
				{
					IConceptualEntity sourceEntity = relationshipAnnotation.SourceEntity;
					IConceptualNavigationProperty relationship = relationshipAnnotation.Relationship;
					IConceptualEntity targetEntity = relationship.TargetEntity;
					ConceptualMultiplicity sourceMultiplicity = relationship.SourceMultiplicity;
					ConceptualMultiplicity targetMultiplicity = relationship.TargetMultiplicity;
					bool flag = relationship.CrossFilterDirection == CrossFilterDirection.Both;
					if (includeWeakRelationships || relationship.Behavior == ConceptualNavigationBehavior.Default)
					{
						bool flag2 = respectModelBidi && flag;
						NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.AddAssociationGraphEdge(directedGraph, sourceMultiplicity, sourceEntity, targetMultiplicity, targetEntity, fromOne, flag2, includeDirectManyToMany && flag);
						NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.AddAssociationGraphEdge(directedGraph, targetMultiplicity, targetEntity, sourceMultiplicity, sourceEntity, fromOne, flag2, includeDirectManyToMany);
					}
				}
				return directedGraph;
			}

			// Token: 0x0600199C RID: 6556 RVA: 0x0002E17C File Offset: 0x0002C37C
			private static void AddAssociationGraphEdge(DirectedGraph<IConceptualEntity> graph, ConceptualMultiplicity x, IConceptualEntity xEntity, ConceptualMultiplicity y, IConceptualEntity yEntity, bool fromOne, bool bypassCardinalityChecks, bool includeDirectManyToMany)
			{
				if (bypassCardinalityChecks || (!fromOne && NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.HasCardinalityMany(x) && !NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.HasCardinalityMany(y)) || (includeDirectManyToMany && NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.HasCardinalityMany(x) && NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.HasCardinalityMany(y)) || (fromOne && !NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.HasCardinalityMany(x)))
				{
					graph.AddEdge(xEntity, yEntity);
					graph.AddEdges(yEntity, null);
				}
			}

			// Token: 0x0600199D RID: 6557 RVA: 0x0002E1D5 File Offset: 0x0002C3D5
			private static bool HasCardinalityMany(ConceptualMultiplicity x)
			{
				return x == ConceptualMultiplicity.Many;
			}

			// Token: 0x0600199E RID: 6558 RVA: 0x0002E1DB File Offset: 0x0002C3DB
			private static bool HasCardinalityOne(ConceptualMultiplicity x)
			{
				return x == ConceptualMultiplicity.One || x == ConceptualMultiplicity.ZeroOrOne;
			}

			// Token: 0x04000983 RID: 2435
			private readonly IConceptualSchema _schema;

			// Token: 0x04000984 RID: 2436
			private readonly List<NavigationPropertyGraphAnnotationProvider.NavigationPropertyGraphAnnotationBuilder.RelationshipAnnotation> _relationships;

			// Token: 0x02000364 RID: 868
			private struct RelationshipAnnotation
			{
				// Token: 0x06001AB7 RID: 6839 RVA: 0x0002FDCD File Offset: 0x0002DFCD
				internal RelationshipAnnotation(IConceptualEntity sourceEntity, IConceptualNavigationProperty relationship)
				{
					this.SourceEntity = sourceEntity;
					this.Relationship = relationship;
				}

				// Token: 0x17000553 RID: 1363
				// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0002FDDD File Offset: 0x0002DFDD
				internal IConceptualEntity SourceEntity { get; }

				// Token: 0x17000554 RID: 1364
				// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0002FDE5 File Offset: 0x0002DFE5
				internal IConceptualNavigationProperty Relationship { get; }
			}
		}
	}
}
