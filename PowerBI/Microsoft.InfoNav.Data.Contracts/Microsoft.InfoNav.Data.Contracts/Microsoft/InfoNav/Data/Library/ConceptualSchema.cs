using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000078 RID: 120
	[ImmutableObject(true)]
	public sealed class ConceptualSchema : IConceptualSchema, IAnnotatableRoot, IBuiltConceptualType, IOverridableConceptualSchema, IExtensionConceptualSchema
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00007578 File Offset: 0x00005778
		private ConceptualSchema(string schemaId, string displayName, LanguageIdentifier language, ConceptualCollation conceptualCollation, IConceptualSchema extendedSchema, ConceptualCapabilities capabilities, IConceptualMeasure defaultMeasure)
		{
			this._schemaId = schemaId;
			this._displayName = displayName;
			this._language = language;
			this._conceptualCollation = conceptualCollation;
			this._extends = ((extendedSchema != null) ? extendedSchema.SchemaId : null);
			this._extendedSchema = extendedSchema;
			this._capabilities = capabilities;
			this._defaultMeasure = defaultMeasure;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000075DF File Offset: 0x000057DF
		public static ConceptualSchema.Builder CreateBuilder(LanguageIdentifier language, string schemaId = null, string displayName = null, ConceptualCollation conceptualCollation = null, IConceptualSchema extendedSchema = null, IEnumerable<ConceptualEntity.Builder> entities = null, IEnumerable<ConceptualPod.Builder> pods = null, ConceptualCapabilities capabilities = null, IConceptualMeasure defaultMeasure = null)
		{
			if (schemaId == null)
			{
				schemaId = "";
			}
			if (conceptualCollation == null)
			{
				conceptualCollation = ConceptualCollation.Default;
			}
			if (entities == null)
			{
				entities = Util.EmptyReadOnlyCollection<ConceptualEntity.Builder>();
			}
			return new ConceptualSchema.Builder(new ConceptualSchema(schemaId, displayName, language, conceptualCollation, extendedSchema, capabilities, defaultMeasure), entities, pods);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00007618 File Offset: 0x00005818
		public static IConceptualSchema Create(LanguageIdentifier language, string schemaId = null, string displayName = null, ConceptualCollation conceptualCollation = null, IConceptualSchema extendedSchema = null, IEnumerable<ConceptualEntity.Builder> entities = null, IEnumerable<ConceptualPod.Builder> pods = null, ConceptualCapabilities capabilities = null, IConceptualMeasure defaultMeasure = null)
		{
			return ConceptualSchema.CreateBuilder(language, schemaId, displayName, conceptualCollation, extendedSchema, entities, pods, capabilities, defaultMeasure).CompleteInitialization();
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000763D File Offset: 0x0000583D
		public string SchemaId
		{
			get
			{
				return this._schemaId;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00007645 File Offset: 0x00005845
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000764D File Offset: 0x0000584D
		public LanguageIdentifier Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00007655 File Offset: 0x00005855
		public ConceptualCollation ConceptualCollation
		{
			get
			{
				return this._conceptualCollation;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000765D File Offset: 0x0000585D
		public string Extends
		{
			get
			{
				return this._extends;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00007665 File Offset: 0x00005865
		public IConceptualSchema ExtendedSchema
		{
			get
			{
				return this._extendedSchema;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000766D File Offset: 0x0000586D
		public IReadOnlyList<IConceptualEntity> Entities
		{
			get
			{
				return this._entities;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00007675 File Offset: 0x00005875
		public ConceptualSchemaStatistics Statistics
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00007678 File Offset: 0x00005878
		public ConceptualCapabilities Capabilities
		{
			get
			{
				return this._capabilities;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00007680 File Offset: 0x00005880
		public IConceptualMeasure DefaultMeasure
		{
			get
			{
				return this._defaultMeasure;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007688 File Offset: 0x00005888
		public bool TryGetEntityByEdmName(string qualifiedName, out IConceptualEntity entity)
		{
			if (qualifiedName == null)
			{
				entity = null;
				return false;
			}
			return this._entitiesByEdmName.TryGetValue(qualifiedName, out entity);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000769F File Offset: 0x0000589F
		public bool TryGetEntity(string referenceName, out IConceptualEntity entity)
		{
			if (referenceName == null)
			{
				entity = null;
				return false;
			}
			return this._entitiesByReferenceName.TryGetValue(referenceName, out entity);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000076B6 File Offset: 0x000058B6
		public bool TryGetPropertyByEdmName(EdmPropertyRef edmPropertyRef, out IConceptualProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000076C0 File Offset: 0x000058C0
		public IConceptualSchema OverrideConceptualCapabilities(ConceptualCapabilities newCapabilities)
		{
			return ConceptualSchema.Create(this._language, this._schemaId, this._displayName, this._conceptualCollation, this._extendedSchema, null, null, newCapabilities, this._defaultMeasure);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000076F9 File Offset: 0x000058F9
		public bool RegisterAnnotationProvider<TAnnotation, TTarget>(IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProvider);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007707 File Offset: 0x00005907
		public IAnnotationProvider<TAnnotation, TTarget> RegisterAnnotationProvider<TAnnotation, TTarget>(Func<IAnnotationProvider<TAnnotation, TTarget>> annotationProviderCreator)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProviderCreator);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007715 File Offset: 0x00005915
		public bool TryGetAnnotationProvider<TAnnotation, TTarget>(out IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.TryGetAnnotationProvider<TAnnotation, TTarget>(out annotationProvider);
		}

		// Token: 0x04000197 RID: 407
		private readonly IConceptualSchema _extendedSchema;

		// Token: 0x04000198 RID: 408
		private readonly string _schemaId;

		// Token: 0x04000199 RID: 409
		private readonly string _displayName;

		// Token: 0x0400019A RID: 410
		private readonly LanguageIdentifier _language;

		// Token: 0x0400019B RID: 411
		private readonly ConceptualCollation _conceptualCollation;

		// Token: 0x0400019C RID: 412
		private readonly string _extends;

		// Token: 0x0400019D RID: 413
		private readonly ConceptualCapabilities _capabilities;

		// Token: 0x0400019E RID: 414
		private readonly IConceptualMeasure _defaultMeasure;

		// Token: 0x0400019F RID: 415
		private readonly ConceptualAnnotationManager _annotationManager = new ConceptualAnnotationManager();

		// Token: 0x040001A0 RID: 416
		private IReadOnlyList<IConceptualEntity> _entities;

		// Token: 0x040001A1 RID: 417
		private IReadOnlyDictionary<string, IConceptualEntity> _entitiesByReferenceName;

		// Token: 0x040001A2 RID: 418
		private IReadOnlyDictionary<string, IConceptualEntity> _entitiesByEdmName;

		// Token: 0x020002FF RID: 767
		public sealed class Builder : ConceptualBuilderBase<ConceptualSchema>
		{
			// Token: 0x0600193B RID: 6459 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
			internal Builder(ConceptualSchema schema, IEnumerable<ConceptualEntity.Builder> entities, IEnumerable<ConceptualPod.Builder> pods)
				: base(schema)
			{
				this._entities = ((entities != null) ? entities.ToList<ConceptualEntity.Builder>() : null);
				this._pods = pods;
			}

			// Token: 0x0600193C RID: 6460 RVA: 0x0002D704 File Offset: 0x0002B904
			public ConceptualEntity.Builder GetEntityBuilder(string name)
			{
				if (this._entities == null)
				{
					return null;
				}
				return this._entities.SingleOrDefault((ConceptualEntity.Builder e) => e.Name == name);
			}

			// Token: 0x0600193D RID: 6461 RVA: 0x0002D73F File Offset: 0x0002B93F
			public void AddEntityBuilder(ConceptualEntity.Builder entity)
			{
				if (this._entities == null)
				{
					this._entities = new List<ConceptualEntity.Builder>();
				}
				this._entities.Add(entity);
			}

			// Token: 0x0600193E RID: 6462 RVA: 0x0002D760 File Offset: 0x0002B960
			public IConceptualSchema CompleteInitialization()
			{
				List<IConceptualEntity> list = new List<IConceptualEntity>();
				if (this._entities != null)
				{
					foreach (ConceptualEntity.Builder builder in this._entities)
					{
						list.Add(builder.CompleteInitialization(base.ActiveObject));
					}
				}
				if (this._pods != null)
				{
					foreach (ConceptualPod.Builder builder2 in this._pods)
					{
						list.Add(builder2.CompleteInitialization(base.ActiveObject));
					}
				}
				base.ActiveObject._entities = list;
				this.BuildEntitiesDictionaries(list);
				return base.CompleteBaseInitialization();
			}

			// Token: 0x0600193F RID: 6463 RVA: 0x0002D838 File Offset: 0x0002BA38
			private void BuildEntitiesDictionaries(IReadOnlyList<IConceptualEntity> entities)
			{
				Dictionary<string, IConceptualEntity> dictionary = new Dictionary<string, IConceptualEntity>(entities.Count, ConceptualNameComparer.Instance);
				Dictionary<string, IConceptualEntity> dictionary2 = new Dictionary<string, IConceptualEntity>(entities.Count, EdmNameComparer.Instance);
				foreach (IConceptualEntity conceptualEntity in entities)
				{
					dictionary.Add(conceptualEntity.Name, conceptualEntity);
					dictionary2.Add(conceptualEntity.GetFullName(), conceptualEntity);
				}
				base.ActiveObject._entitiesByReferenceName = dictionary;
				base.ActiveObject._entitiesByEdmName = dictionary2;
			}

			// Token: 0x04000951 RID: 2385
			private readonly IEnumerable<ConceptualPod.Builder> _pods;

			// Token: 0x04000952 RID: 2386
			private List<ConceptualEntity.Builder> _entities;
		}
	}
}
