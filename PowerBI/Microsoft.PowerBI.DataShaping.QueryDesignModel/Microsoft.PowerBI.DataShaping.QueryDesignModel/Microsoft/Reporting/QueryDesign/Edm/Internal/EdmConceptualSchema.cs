using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Library;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001EA RID: 490
	public sealed class EdmConceptualSchema : IConceptualSchema, IAnnotatableRoot, IOverridableConceptualSchema
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x0003FC64 File Offset: 0x0003DE64
		internal EdmConceptualSchema(IReadOnlyList<IConceptualEntity> entities, IReadOnlyDictionary<string, IConceptualEntity> entitySetsByName, IReadOnlyDictionary<string, IConceptualEntity> entitySetsByReferenceName, ConceptualCapabilities capabilities, string culture, string displayName, ConceptualCollation conceptualCollation, IConceptualMeasure defaultMeasure)
		{
			this._entities = entities;
			this._entitySetsByName = entitySetsByName;
			this._entitySetsByReferenceName = entitySetsByReferenceName;
			this._capabilities = capabilities;
			this._annotationManager = new ConceptualAnnotationManager();
			this._displayName = displayName;
			this._conceptualCollation = conceptualCollation;
			this._defaultMeasure = defaultMeasure;
			LanguageIdentifierUtil.TryAsLanguageIdentifier(culture, out this._language);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x0003FCC5 File Offset: 0x0003DEC5
		public string SchemaId
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0003FCCC File Offset: 0x0003DECC
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		public LanguageIdentifier Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0003FCDC File Offset: 0x0003DEDC
		public ConceptualCollation ConceptualCollation
		{
			get
			{
				return this._conceptualCollation;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0003FCE4 File Offset: 0x0003DEE4
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0003FCE7 File Offset: 0x0003DEE7
		public IReadOnlyList<IConceptualEntity> Entities
		{
			get
			{
				return this._entities;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0003FCEF File Offset: 0x0003DEEF
		public ConceptualSchemaStatistics Statistics
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0003FCF6 File Offset: 0x0003DEF6
		public ConceptualCapabilities Capabilities
		{
			get
			{
				return this._capabilities;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0003FCFE File Offset: 0x0003DEFE
		public IConceptualMeasure DefaultMeasure
		{
			get
			{
				return this._defaultMeasure;
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0003FD06 File Offset: 0x0003DF06
		public bool TryGetEntityByEdmName(string qualifiedName, out IConceptualEntity entity)
		{
			if (qualifiedName == null)
			{
				entity = null;
				return false;
			}
			return this._entitySetsByName.TryGetValue(qualifiedName, out entity);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0003FD1D File Offset: 0x0003DF1D
		public bool TryGetEntity(string referenceName, out IConceptualEntity entity)
		{
			if (referenceName == null)
			{
				entity = null;
				return false;
			}
			return this._entitySetsByReferenceName.TryGetValue(referenceName, out entity);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0003FD34 File Offset: 0x0003DF34
		public bool TryGetPropertyByEdmName(EdmPropertyRef edmPropertyRef, out IConceptualProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0003FD3B File Offset: 0x0003DF3B
		public IConceptualSchema OverrideConceptualCapabilities(ConceptualCapabilities newCapabilities)
		{
			return new EdmConceptualSchema(this._entities, this._entitySetsByName, this._entitySetsByReferenceName, newCapabilities, this._language.ToLanguageName(), this._displayName, this._conceptualCollation, this._defaultMeasure);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0003FD72 File Offset: 0x0003DF72
		public bool RegisterAnnotationProvider<TAnnotation, TTarget>(IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProvider);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0003FD80 File Offset: 0x0003DF80
		public IAnnotationProvider<TAnnotation, TTarget> RegisterAnnotationProvider<TAnnotation, TTarget>(Func<IAnnotationProvider<TAnnotation, TTarget>> annotationProviderCreator)
		{
			return this._annotationManager.RegisterAnnotationProvider<TAnnotation, TTarget>(annotationProviderCreator);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0003FD8E File Offset: 0x0003DF8E
		public bool TryGetAnnotationProvider<TAnnotation, TTarget>(out IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationManager.TryGetAnnotationProvider<TAnnotation, TTarget>(out annotationProvider);
		}

		// Token: 0x04000C6A RID: 3178
		private readonly IReadOnlyList<IConceptualEntity> _entities;

		// Token: 0x04000C6B RID: 3179
		private readonly IReadOnlyDictionary<string, IConceptualEntity> _entitySetsByName;

		// Token: 0x04000C6C RID: 3180
		private readonly IReadOnlyDictionary<string, IConceptualEntity> _entitySetsByReferenceName;

		// Token: 0x04000C6D RID: 3181
		private readonly ConceptualCapabilities _capabilities;

		// Token: 0x04000C6E RID: 3182
		private readonly LanguageIdentifier _language;

		// Token: 0x04000C6F RID: 3183
		private readonly ConceptualAnnotationManager _annotationManager;

		// Token: 0x04000C70 RID: 3184
		private readonly string _displayName;

		// Token: 0x04000C71 RID: 3185
		private readonly ConceptualCollation _conceptualCollation;

		// Token: 0x04000C72 RID: 3186
		private readonly IConceptualMeasure _defaultMeasure;
	}
}
