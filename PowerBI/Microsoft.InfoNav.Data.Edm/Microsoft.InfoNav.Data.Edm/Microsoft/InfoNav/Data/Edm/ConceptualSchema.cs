using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Library;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000017 RID: 23
	[ImmutableObject(true)]
	internal sealed class ConceptualSchema : IConceptualSchema, IAnnotatableRoot, IOverridableConceptualSchema
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000036CC File Offset: 0x000018CC
		internal ConceptualSchema(LanguageIdentifier language, IReadOnlyList<IConceptualEntity> entities, string schemaId = null, string displayName = null, ConceptualCapabilities capabilities = null, ConceptualCollation conceptualCollation = null, IConceptualMeasure defaultMeasure = null)
		{
			this._language = language;
			this._entities = entities;
			this._schemaId = schemaId;
			this.DisplayName = displayName;
			this.ConceptualCollation = conceptualCollation ?? ConceptualCollation.Default;
			this._capabilities = capabilities;
			this.DefaultMeasure = defaultMeasure;
			this._referenceNameToEntity = ConceptualSchema.BuildEntitiesByName(entities);
			this._qualifiedNameToEntity = ConceptualSchema.BuildEntitiesByEdmName(entities);
			this._edmPropertyRefToConceptualProperty = ConceptualSchema.BuildEdmPropertyRefToProperty(entities);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000374C File Offset: 0x0000194C
		public string SchemaId
		{
			get
			{
				return this._schemaId;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003754 File Offset: 0x00001954
		public string DisplayName { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000375C File Offset: 0x0000195C
		public LanguageIdentifier Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003764 File Offset: 0x00001964
		public ConceptualCollation ConceptualCollation { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000376C File Offset: 0x0000196C
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000376F File Offset: 0x0000196F
		public IReadOnlyList<IConceptualEntity> Entities
		{
			get
			{
				return this._entities;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003777 File Offset: 0x00001977
		public ConceptualSchemaStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000377F File Offset: 0x0000197F
		public ConceptualCapabilities Capabilities
		{
			get
			{
				return this._capabilities;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003787 File Offset: 0x00001987
		public IConceptualMeasure DefaultMeasure { get; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00003790 File Offset: 0x00001990
		public bool TryGetEntity(string referenceName, out IConceptualEntity entity)
		{
			IConceptualEntity conceptualEntity;
			if (this._referenceNameToEntity.TryGetValue(referenceName, out conceptualEntity))
			{
				entity = conceptualEntity;
				return true;
			}
			entity = null;
			return false;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000037B8 File Offset: 0x000019B8
		public bool TryGetEntityByEdmName(string qualifiedName, out IConceptualEntity entity)
		{
			IConceptualEntity conceptualEntity;
			if (this._qualifiedNameToEntity.TryGetValue(qualifiedName, out conceptualEntity))
			{
				entity = conceptualEntity;
				return true;
			}
			entity = null;
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000037DE File Offset: 0x000019DE
		public bool TryGetPropertyByEdmName(EdmPropertyRef edmPropertyRef, out IConceptualProperty property)
		{
			return this._edmPropertyRefToConceptualProperty.TryGetValue(edmPropertyRef, out property);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000037ED File Offset: 0x000019ED
		public bool RegisterAnnotationProvider<TAnnotation, TTarget>(IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProvider);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000037FB File Offset: 0x000019FB
		public IAnnotationProvider<TAnnotation, TTarget> RegisterAnnotationProvider<TAnnotation, TTarget>(Func<IAnnotationProvider<TAnnotation, TTarget>> annotationProviderCreator)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProviderCreator);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003809 File Offset: 0x00001A09
		public bool TryGetAnnotationProvider<TAnnotation, TTarget>(out IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.TryGetAnnotationProvider<TAnnotation, TTarget>(out annotationProvider);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003817 File Offset: 0x00001A17
		public IConceptualSchema OverrideConceptualCapabilities(ConceptualCapabilities newCapabilities)
		{
			return new ConceptualSchema(this._language, this._entities, this._schemaId, this.DisplayName, newCapabilities, this.ConceptualCollation, this.DefaultMeasure);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003843 File Offset: 0x00001A43
		internal void CompleteInitialization(ConceptualSchemaStatistics statistics)
		{
			this._statistics = statistics;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000384C File Offset: 0x00001A4C
		private static IReadOnlyDictionary<string, IConceptualEntity> BuildEntitiesByName(IReadOnlyList<IConceptualEntity> entities)
		{
			Dictionary<string, IConceptualEntity> dictionary = new Dictionary<string, IConceptualEntity>(entities.Count, ConceptualNameComparer.Instance);
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				dictionary.Add(conceptualEntity.Name, conceptualEntity);
			}
			return dictionary;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000038AC File Offset: 0x00001AAC
		private static IReadOnlyDictionary<string, IConceptualEntity> BuildEntitiesByEdmName(IReadOnlyList<IConceptualEntity> entities)
		{
			return entities.ToDictionary((IConceptualEntity entity) => entity.GetFullName(), EdmNameComparer.Instance);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000038D8 File Offset: 0x00001AD8
		private static IReadOnlyDictionary<EdmPropertyRef, IConceptualProperty> BuildEdmPropertyRefToProperty(IReadOnlyList<IConceptualEntity> entities)
		{
			Dictionary<EdmPropertyRef, IConceptualProperty> dictionary = new Dictionary<EdmPropertyRef, IConceptualProperty>(entities.Count * 2);
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					EdmPropertyRef edmPropertyRef = new EdmPropertyRef(conceptualEntity.EntityContainerName, conceptualEntity.Name, conceptualProperty.Name);
					dictionary.Add(edmPropertyRef, conceptualProperty);
				}
			}
			return dictionary;
		}

		// Token: 0x0400009B RID: 155
		private readonly LanguageIdentifier _language;

		// Token: 0x0400009C RID: 156
		private readonly IReadOnlyList<IConceptualEntity> _entities;

		// Token: 0x0400009D RID: 157
		private readonly IReadOnlyDictionary<string, IConceptualEntity> _qualifiedNameToEntity;

		// Token: 0x0400009E RID: 158
		private readonly IReadOnlyDictionary<string, IConceptualEntity> _referenceNameToEntity;

		// Token: 0x0400009F RID: 159
		private readonly IReadOnlyDictionary<EdmPropertyRef, IConceptualProperty> _edmPropertyRefToConceptualProperty;

		// Token: 0x040000A0 RID: 160
		private readonly string _schemaId;

		// Token: 0x040000A1 RID: 161
		private readonly ConceptualCapabilities _capabilities;

		// Token: 0x040000A2 RID: 162
		private readonly ConceptualAnnotationManager _annotationManager = new ConceptualAnnotationManager();

		// Token: 0x040000A3 RID: 163
		private ConceptualSchemaStatistics _statistics;
	}
}
