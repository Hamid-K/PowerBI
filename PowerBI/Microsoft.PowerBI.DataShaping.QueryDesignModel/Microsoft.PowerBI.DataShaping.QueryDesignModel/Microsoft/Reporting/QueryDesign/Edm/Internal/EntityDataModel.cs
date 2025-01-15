using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023A RID: 570
	public sealed class EntityDataModel : IConceptualModel
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x00044668 File Offset: 0x00042868
		internal EntityDataModel(EdmItemCollection edmItems, EntityContainer entityContainer, ModelCapabilities overrideModelCapabilities)
		{
			this.EdmItems = edmItems;
			this.EntityContainer = new EntityContainer(entityContainer, overrideModelCapabilities);
			this.Version = new global::System.Version(entityContainer.GetStringMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Version", "1.0"));
			this.DaxCapabilities = DaxCapabilitiesBuilder.BuildCapabilities(this.ModelCapabilities, this.Version > EntityDataModel.VersionOnePointZero);
			EntityDataModel.EdmTypeDictionary<EntityType> entityTypes = new EntityDataModel.EdmTypeDictionary<EntityType>(from type in edmItems.GetItems<EntityType>()
				select EntityType.Create(type, this.Version));
			this.EntitySets = new EntitySetCollection(from set in entityContainer.BaseEntitySets.OfType<EntitySet>()
				select new EntitySet(set, entityTypes.GetItemFromEdmType(set.ElementType)));
			EntityDataModel.EdmTypeDictionary<AssociationType> assocTypes = new EntityDataModel.EdmTypeDictionary<AssociationType>(from type in edmItems.GetItems<AssociationType>()
				select AssociationType.Create(type));
			this.AssociationSets = new AssociationSetCollection(from set in entityContainer.BaseEntitySets.OfType<AssociationSet>()
				select AssociationSet.Create(set, assocTypes.GetItemFromEdmType(set.ElementType), this.EntitySets));
			this.AssocsFromOneGraph = this.GetAssociationsGraph(true, false, false, true);
			this.StrongAssocsFromOneGraph = this.GetAssociationsGraph(true, false, false, false);
			this.AssocsFromOneAndDirectedOneHopManyToManyGraph = this.GetAssociationsGraph(true, false, true, true);
			this.AssocsFromManyGraph = this.GetAssociationsGraph(false, false, false, true);
			this.AssocsFromOneWithBidirCrossFilteringGraph = this.GetAssociationsGraph(true, true, false, true);
			this.AssocsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph = this.GetAssociationsGraph(true, true, true, true);
			this._defaultMeasure = null;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x000447E9 File Offset: 0x000429E9
		public string Caption
		{
			get
			{
				return this.EntityContainer.Caption;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x000447F6 File Offset: 0x000429F6
		public string Culture
		{
			get
			{
				return this.EntityContainer.Culture;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00044803 File Offset: 0x00042A03
		public CompareOptions CompareOptions
		{
			get
			{
				return this.EntityContainer.CompareOptions;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x00044810 File Offset: 0x00042A10
		public EdmMeasureInstance? DefaultMeasure
		{
			get
			{
				if (this._defaultMeasure == null)
				{
					string defaultMeasureName = this.EntityContainer.DefaultMeasureName;
					if (defaultMeasureName != null)
					{
						Func<EdmPropertyInstance, bool> <>9__0;
						foreach (EntitySet entitySet in this.EntitySets)
						{
							IEnumerable<EdmPropertyInstance> properties = entitySet.GetProperties();
							Func<EdmPropertyInstance, bool> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (EdmPropertyInstance p) => EdmItem.IdentityComparer.Equals(p.Property.Name, defaultMeasureName));
							}
							EdmPropertyInstance edmPropertyInstance = properties.SingleOrDefault(func);
							if (edmPropertyInstance.IsValid)
							{
								this._defaultMeasure = new EdmMeasureInstance?(edmPropertyInstance.ToEdmMeasureInstance());
								break;
							}
						}
						if (this._defaultMeasure == null)
						{
							throw new InvalidOperationException(DevErrors.EntityType.UnknownMemberReference(defaultMeasureName));
						}
					}
				}
				return this._defaultMeasure;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x000448F4 File Offset: 0x00042AF4
		public bool PreferOrdinalStringEquality
		{
			get
			{
				return this.EntityContainer.PreferOrdinalStringEquality;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00044901 File Offset: 0x00042B01
		public ModelCapabilities ModelCapabilities
		{
			get
			{
				return this.EntityContainer.ModelCapabilities;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0004490E File Offset: 0x00042B0E
		public DaxCapabilities DaxCapabilities { get; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00044916 File Offset: 0x00042B16
		public EntitySetCollection EntitySets { get; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0004491E File Offset: 0x00042B1E
		public AssociationSetCollection AssociationSets { get; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00044926 File Offset: 0x00042B26
		public global::System.Version Version { get; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0004492E File Offset: 0x00042B2E
		public string EntityContainerName
		{
			get
			{
				return this.EntityContainer.Name;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0004493B File Offset: 0x00042B3B
		public IDataComparer Comparer
		{
			get
			{
				if (this._comparer == null)
				{
					this._comparer = ComparerAnnotation.BuildComparer(this.Culture, this.CompareOptions);
				}
				return this._comparer;
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00044962 File Offset: 0x00042B62
		internal ModelTelemetry GetModelTelemetry(IConceptualSchema schema = null)
		{
			if (this._modelTelemetry == null)
			{
				this._modelTelemetry = ModelTelemetryBuilder.BuildModelTelemetry(this, schema);
			}
			return this._modelTelemetry;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0004497F File Offset: 0x00042B7F
		internal EdmItemCollection EdmItems { get; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00044987 File Offset: 0x00042B87
		internal EntityContainer EntityContainer { get; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0004498F File Offset: 0x00042B8F
		private IDirectedGraph<EntitySet> AssocsFromOneGraph { get; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00044997 File Offset: 0x00042B97
		private IDirectedGraph<EntitySet> AssocsFromOneAndDirectedOneHopManyToManyGraph { get; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0004499F File Offset: 0x00042B9F
		private IDirectedGraph<EntitySet> AssocsFromManyGraph { get; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x000449A7 File Offset: 0x00042BA7
		private IDirectedGraph<EntitySet> AssocsFromOneWithBidirCrossFilteringGraph { get; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x000449AF File Offset: 0x00042BAF
		internal IDirectedGraph<EntitySet> StrongAssocsFromOneGraph { get; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x000449B7 File Offset: 0x00042BB7
		private IDirectedGraph<EntitySet> AssocsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph { get; }

		// Token: 0x06001922 RID: 6434 RVA: 0x000449C0 File Offset: 0x00042BC0
		internal static EntityDataModel Load(XmlReader reader)
		{
			ArgumentValidation.CheckNotNull<XmlReader>(reader, "reader");
			EntityDataModel entityDataModel;
			try
			{
				EdmItemCollection edmItemCollection = new EdmItemCollection(new XmlReader[] { reader });
				EntityContainer entityContainer = edmItemCollection.GetItems<EntityContainer>().Single<EntityContainer>();
				entityDataModel = new EntityDataModel(edmItemCollection, entityContainer, null);
			}
			catch (Exception ex)
			{
				throw new EdmException(ex.Message.MarkAsModelInfo(), ex);
			}
			return entityDataModel;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00044A24 File Offset: 0x00042C24
		internal EntityDataModel OverrideModelCapabilities(ModelCapabilities newCapabilities)
		{
			return new EntityDataModel(this.EdmItems, (EntityContainer)this.EntityContainer.InternalEdmItem, newCapabilities);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00044A42 File Offset: 0x00042C42
		public bool IsQueryAggregateUsageEncouraged()
		{
			return this.ModelCapabilities.QueryAggregateUsage == QueryAggregateUsageType.Encourage;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00044A52 File Offset: 0x00042C52
		public bool DiscourageCountRowsOverTables()
		{
			return this.ModelCapabilities.IsMultidimensional();
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00044A60 File Offset: 0x00042C60
		internal bool ShouldAddAsMeasure(EdmProperty property)
		{
			if (!this.CanAddAsMeasure(property))
			{
				return false;
			}
			EdmField edmField;
			EdmMeasure edmMeasure;
			property.GetKnownSubtypes(out edmField, out edmMeasure);
			return edmMeasure != null || edmField.DefaultAggregateFunction != null;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00044A98 File Offset: 0x00042C98
		internal bool CanAddAsMeasure(EdmProperty property)
		{
			return EntityDataModel.IsModelMeasure(property) || this.IsQueryAggregateUsageEncouraged();
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00044AAA File Offset: 0x00042CAA
		internal bool CanAddAsMeasure(IEdmItemInstance instance)
		{
			return instance.InvokeTypeSpecificFunction(new Func<EdmPropertyInstance, bool>(this.CanAddAsMeasure), new Func<EdmHierarchyInstance, bool>(this.CanAddAsMeasure));
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00044ACA File Offset: 0x00042CCA
		private bool CanAddAsMeasure(EdmPropertyInstance propertyInstance)
		{
			return this.CanAddAsMeasure(propertyInstance.Property);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00044ADC File Offset: 0x00042CDC
		private bool CanAddAsMeasure(EdmHierarchyInstance hierarchyInstance)
		{
			foreach (EdmPropertyInstance edmPropertyInstance in hierarchyInstance.GetLevelInstances())
			{
				if (!this.CanAddAsMeasure(edmPropertyInstance))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00044B34 File Offset: 0x00042D34
		internal static bool IsModelMeasure(EdmProperty property)
		{
			return property is EdmMeasure;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00044B3F File Offset: 0x00042D3F
		internal IReadOnlyList<EntitySet> GetRelatedToOneEntitySets(EntitySet source)
		{
			return this.GetRelatedEntitiesManyToOne(source).Union(this.GetRelatedEntitiesOneToOne(source)).ToReadOnlyList<EntitySet>();
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00044B59 File Offset: 0x00042D59
		private IList<EntitySet> GetRelatedEntitiesManyToOne(EntitySet source)
		{
			return QueryAlgorithms.DetectRelatedManyToOneEntities(source, this);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00044B64 File Offset: 0x00042D64
		private IList<EntitySet> GetRelatedEntitiesOneToOne(EntitySet source)
		{
			IDirectedGraph<EntitySet> associationsFromOneGraph = this.GetAssociationsFromOneGraph(false);
			IDirectedGraph<EntitySet> fromManyAssociations = this.GetAssociationsFromManyGraph();
			Func<EntitySet, bool> func = (EntitySet e) => !fromManyAssociations.GetEdgesFromVertex(e).Contains(source);
			return associationsFromOneGraph.GetEdgesFromVertex(source).Where(func).ToList<EntitySet>();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00044BB4 File Offset: 0x00042DB4
		public IDirectedGraph<EntitySet> GetAssociationsFromOneGraph(bool includeDirectManyToMany)
		{
			if (!includeDirectManyToMany)
			{
				return this.AssocsFromOneGraph;
			}
			return this.AssocsFromOneAndDirectedOneHopManyToManyGraph;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00044BC6 File Offset: 0x00042DC6
		internal IDirectedGraph<EntitySet> GetAssociationsFromManyGraph()
		{
			return this.AssocsFromManyGraph;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00044BCE File Offset: 0x00042DCE
		public IDirectedGraph<EntitySet> GetAssociationsFromOneWithBidirCrossFilteringGraph(bool includeDirectManyToMany)
		{
			if (!includeDirectManyToMany)
			{
				return this.AssocsFromOneWithBidirCrossFilteringGraph;
			}
			return this.AssocsFromOneWithBidirCrossFilteringAndDirectedOneHopManyToManyGraph;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00044BE0 File Offset: 0x00042DE0
		public IDirectedGraph<EntitySet> GetStrongAssociationsFromOneGraph()
		{
			return this.StrongAssocsFromOneGraph;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00044BE8 File Offset: 0x00042DE8
		private IDirectedGraph<EntitySet> GetAssociationsGraph(bool fromOne, bool respectModelBidi, bool includeDirectManyToMany, bool includeWeakRelationships)
		{
			DirectedGraph<EntitySet> directedGraph = new DirectedGraph<EntitySet>();
			foreach (AssociationSet associationSet in this.AssociationSets.Where((AssociationSet a) => a.State == AssociationState.Active))
			{
				AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[0];
				AssociationSetEnd associationSetEnd2 = associationSet.AssociationSetEnds[1];
				bool flag = associationSet.CrossFilterDirection == CrossFilterDirection.Both;
				if (includeWeakRelationships || associationSet.Behavior == AssociationBehavior.Default)
				{
					bool flag2 = respectModelBidi && flag;
					EntityDataModel.AddAssociationGraphEdge(directedGraph, associationSetEnd, associationSetEnd2, fromOne, flag2, includeDirectManyToMany && flag);
					EntityDataModel.AddAssociationGraphEdge(directedGraph, associationSetEnd2, associationSetEnd, fromOne, flag2, includeDirectManyToMany);
				}
			}
			return directedGraph;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00044CB0 File Offset: 0x00042EB0
		private static void AddAssociationGraphEdge(DirectedGraph<EntitySet> graph, AssociationSetEnd x, AssociationSetEnd y, bool fromOne, bool bypassCardinalityChecks, bool includeDirectManyToMany)
		{
			if (bypassCardinalityChecks || (!fromOne && EntityDataModel.HasCardinalityMany(x) && !EntityDataModel.HasCardinalityMany(y)) || (includeDirectManyToMany && EntityDataModel.HasCardinalityMany(x) && EntityDataModel.HasCardinalityMany(y)) || (fromOne && !EntityDataModel.HasCardinalityMany(x)))
			{
				graph.AddEdge(x.EntitySet, y.EntitySet);
				graph.AddEdges(y.EntitySet, null);
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00044D14 File Offset: 0x00042F14
		private static bool HasCardinalityMany(AssociationSetEnd x)
		{
			return x.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00044D24 File Offset: 0x00042F24
		internal object ConceptualSchemaCache
		{
			get
			{
				return this._conceptualSchema;
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00044D2C File Offset: 0x00042F2C
		internal object SetConceptualSchemaCache(object candidate)
		{
			Interlocked.CompareExchange(ref this._conceptualSchema, candidate, null);
			return this._conceptualSchema;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00044D42 File Offset: 0x00042F42
		internal void ClearConceptualSchemaCache()
		{
			this._conceptualSchema = null;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00044D4B File Offset: 0x00042F4B
		internal void ClearModelTelemetry()
		{
			this._modelTelemetry = null;
		}

		// Token: 0x04000DB1 RID: 3505
		private const string DefaultVersion = "1.0";

		// Token: 0x04000DB2 RID: 3506
		internal static readonly global::System.Version VersionOnePointZero = new global::System.Version("1.0");

		// Token: 0x04000DB3 RID: 3507
		internal static readonly global::System.Version VersionOnePointOne = new global::System.Version("1.1");

		// Token: 0x04000DB4 RID: 3508
		private object _conceptualSchema;

		// Token: 0x04000DB5 RID: 3509
		private EdmMeasureInstance? _defaultMeasure;

		// Token: 0x04000DB6 RID: 3510
		private IDataComparer _comparer;

		// Token: 0x04000DB7 RID: 3511
		private ModelTelemetry _modelTelemetry;

		// Token: 0x020003D1 RID: 977
		private sealed class EdmTypeDictionary<T> : Dictionary<string, T>, IEdmItemLookup where T : EdmType
		{
			// Token: 0x060020CF RID: 8399 RVA: 0x0005961C File Offset: 0x0005781C
			internal EdmTypeDictionary(IEnumerable<T> items)
			{
				foreach (T t in items)
				{
					base.Add(t.FullName, t);
				}
			}

			// Token: 0x060020D0 RID: 8400 RVA: 0x00059678 File Offset: 0x00057878
			internal T GetItemFromEdmType(EdmType edmType)
			{
				ArgumentValidation.CheckNotNull<EdmType>(edmType, "edmType");
				return base[edmType.FullName];
			}

			// Token: 0x060020D1 RID: 8401 RVA: 0x00059694 File Offset: 0x00057894
			EdmType IEdmItemLookup.LookupEdmType(EdmType edmType)
			{
				T t;
				if (base.TryGetValue(edmType.FullName, out t))
				{
					return t;
				}
				return null;
			}
		}
	}
}
