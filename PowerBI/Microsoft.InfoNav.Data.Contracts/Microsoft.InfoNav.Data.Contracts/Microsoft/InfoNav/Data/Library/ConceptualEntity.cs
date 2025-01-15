using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000074 RID: 116
	[DebuggerDisplay("{EntityContainerName}.{Name}")]
	[ImmutableObject(true)]
	public sealed class ConceptualEntity : IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>, IBuiltConceptualType, IExtensionConceptualEntity
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000070D8 File Offset: 0x000052D8
		private ConceptualEntity(string name, string edmName, string displayName, string description, string edmEntityContainerName, IConceptualEntity extendedEntity, ConceptualEntityVisibilityType visibility, string stableName)
		{
			this._name = name;
			this._edmName = edmName;
			this._displayName = displayName;
			this._description = description;
			this._edmEntityContainerName = edmEntityContainerName;
			this._extends = ((extendedEntity != null) ? extendedEntity.Name : null);
			this._extendedEntity = extendedEntity;
			this._visibility = visibility;
			this._stableName = stableName;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007147 File Offset: 0x00005347
		public static ConceptualEntity.Builder Create(string name, string edmName = null, string displayName = null, string description = null, string edmEntityContainerName = null, IConceptualEntity extendedEntity = null, ConceptualEntityVisibilityType visibility = ConceptualEntityVisibilityType.AlwaysVisible, IReadOnlyList<ConceptualMeasure.Builder> measures = null, IReadOnlyList<ConceptualColumn.Builder> columns = null, string stableName = null)
		{
			if (edmName == null)
			{
				edmName = name;
			}
			if (displayName == null)
			{
				displayName = name;
			}
			if (edmEntityContainerName == null)
			{
				edmEntityContainerName = "Sandbox";
			}
			return new ConceptualEntity.Builder(new ConceptualEntity(name, edmName, displayName, description, edmEntityContainerName, extendedEntity, visibility, stableName), measures, columns);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000717A File Offset: 0x0000537A
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00007182 File Offset: 0x00005382
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000718A File Offset: 0x0000538A
		public string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00007192 File Offset: 0x00005392
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000719A File Offset: 0x0000539A
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000071A2 File Offset: 0x000053A2
		public string EntityContainerName
		{
			get
			{
				return this._edmEntityContainerName;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000071AA File Offset: 0x000053AA
		public string Extends
		{
			get
			{
				return this._extends;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000071B2 File Offset: 0x000053B2
		public IConceptualEntity ExtendedEntity
		{
			get
			{
				return this._extendedEntity;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000071BA File Offset: 0x000053BA
		public IConceptualSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000071C2 File Offset: 0x000053C2
		public ConceptualEntityVisibilityType Visibility
		{
			get
			{
				return this._visibility;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000071CA File Offset: 0x000053CA
		public bool IsDateTable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000071D1 File Offset: 0x000053D1
		public IReadOnlyList<IConceptualProperty> Properties
		{
			get
			{
				return this._props;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000071D9 File Offset: 0x000053D9
		public IReadOnlyList<IConceptualNavigationProperty> NavigationProperties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualNavigationProperty>();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000071E0 File Offset: 0x000053E0
		public IReadOnlyList<IConceptualHierarchy> Hierarchies
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualHierarchy>();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000071E7 File Offset: 0x000053E7
		public IReadOnlyList<IConceptualDisplayFolder> DisplayFolders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000071EE File Offset: 0x000053EE
		public IReadOnlyList<IConceptualColumn> KeyColumns
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000071F5 File Offset: 0x000053F5
		public IConceptualColumn DefaultLabelColumn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000071F8 File Offset: 0x000053F8
		public IConceptualColumn DefaultImageColumn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000071FB File Offset: 0x000053FB
		public IReadOnlyList<IConceptualProperty> DefaultFieldProperties
		{
			get
			{
				return Util.EmptyReadOnlyCollection<IConceptualProperty>();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00007202 File Offset: 0x00005402
		public ConceptualEntityStatistics Statistics
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007205 File Offset: 0x00005405
		public ConceptualTableType ConceptualResultType
		{
			get
			{
				if (this._conceptualType == null)
				{
					this._conceptualType = this.BuildConceptualResultType();
				}
				return this._conceptualType;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00007221 File Offset: 0x00005421
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007229 File Offset: 0x00005429
		public bool TryGetPropertyByEdmName(string edmName, out IConceptualProperty conceptualProp)
		{
			return this._edmNamesToProps.TryGetValue(edmName, out conceptualProp);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007238 File Offset: 0x00005438
		public bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProp)
		{
			return this._propNamesToProps.TryGetValue(referenceName, out conceptualProp);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00007247 File Offset: 0x00005447
		public bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy)
		{
			conceptualHierarchy = null;
			return conceptualHierarchy != null;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007251 File Offset: 0x00005451
		public bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy)
		{
			conceptualHierarchy = null;
			return conceptualHierarchy != null;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000725B File Offset: 0x0000545B
		public string GetFullName()
		{
			return this._edmEntityContainerName + "." + this._edmName;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007273 File Offset: 0x00005473
		bool IEquatable<IConceptualEntity>.Equals(IConceptualEntity other)
		{
			return this == other;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000727C File Offset: 0x0000547C
		private ConceptualTableType BuildConceptualResultType()
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(this.Properties.Count);
			foreach (IConceptualProperty conceptualProperty in this.Properties)
			{
				IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
				if (conceptualColumn != null)
				{
					list.Add(conceptualColumn.ConceptualTypeColumn);
				}
			}
			return list.Table();
		}

		// Token: 0x0400017F RID: 383
		private readonly IConceptualEntity _extendedEntity;

		// Token: 0x04000180 RID: 384
		private readonly string _name;

		// Token: 0x04000181 RID: 385
		private readonly string _edmName;

		// Token: 0x04000182 RID: 386
		private readonly string _displayName;

		// Token: 0x04000183 RID: 387
		private readonly string _description;

		// Token: 0x04000184 RID: 388
		private readonly string _edmEntityContainerName;

		// Token: 0x04000185 RID: 389
		private readonly string _extends;

		// Token: 0x04000186 RID: 390
		private readonly ConceptualEntityVisibilityType _visibility;

		// Token: 0x04000187 RID: 391
		private readonly string _stableName;

		// Token: 0x04000188 RID: 392
		private IReadOnlyList<IConceptualProperty> _props = new List<IConceptualProperty>();

		// Token: 0x04000189 RID: 393
		private IReadOnlyDictionary<string, IConceptualProperty> _edmNamesToProps;

		// Token: 0x0400018A RID: 394
		private IReadOnlyDictionary<string, IConceptualProperty> _propNamesToProps;

		// Token: 0x0400018B RID: 395
		private ConceptualTableType _conceptualType;

		// Token: 0x0400018C RID: 396
		private IConceptualSchema _schema;

		// Token: 0x020002FA RID: 762
		public sealed class Builder : ConceptualBuilderBase<ConceptualEntity>
		{
			// Token: 0x0600192E RID: 6446 RVA: 0x0002D353 File Offset: 0x0002B553
			public Builder(ConceptualEntity conceptualEntity, IReadOnlyList<ConceptualMeasure.Builder> measures, IReadOnlyList<ConceptualColumn.Builder> columns)
				: base(conceptualEntity)
			{
				this._measures = measures ?? Util.EmptyReadOnlyCollection<ConceptualMeasure.Builder>();
				this._columns = columns ?? Util.EmptyReadOnlyCollection<ConceptualColumn.Builder>();
				this._completedColumns = new List<IConceptualColumn>();
				this._completedMeasures = new List<IConceptualMeasure>();
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x0600192F RID: 6447 RVA: 0x0002D392 File Offset: 0x0002B592
			public string Name
			{
				get
				{
					return base.ActiveObject.Name;
				}
			}

			// Token: 0x06001930 RID: 6448 RVA: 0x0002D3A0 File Offset: 0x0002B5A0
			public IConceptualColumn AddColumn(ConceptualColumn.Builder columnBuilder)
			{
				IConceptualColumn conceptualColumn = columnBuilder.CompleteInitialization(base.ActiveObject);
				this._completedColumns.Add(conceptualColumn);
				return conceptualColumn;
			}

			// Token: 0x06001931 RID: 6449 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
			public IConceptualMeasure AddMeasure(ConceptualMeasure.Builder measureBuilder)
			{
				IConceptualMeasure conceptualMeasure = measureBuilder.CompleteInitialization(base.ActiveObject);
				this._completedMeasures.Add(conceptualMeasure);
				return conceptualMeasure;
			}

			// Token: 0x06001932 RID: 6450 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
			public IConceptualEntity CompleteInitialization(IConceptualSchema schema)
			{
				Contract.CheckValue<IConceptualSchema>(schema, "schema");
				base.ActiveObject._schema = schema;
				IConceptualProperty[] array = new IConceptualProperty[this._measures.Count + this._completedMeasures.Count + this._columns.Count + this._completedColumns.Count];
				for (int i = 0; i < this._measures.Count; i++)
				{
					array[i] = this._measures[i].CompleteInitialization(base.ActiveObject);
				}
				for (int j = 0; j < this._completedMeasures.Count; j++)
				{
					array[this._measures.Count + j] = this._completedMeasures[j];
				}
				for (int k = 0; k < this._columns.Count; k++)
				{
					array[this._measures.Count + this._completedMeasures.Count + k] = this._columns[k].CompleteInitialization(base.ActiveObject);
				}
				for (int l = 0; l < this._completedColumns.Count; l++)
				{
					array[this._measures.Count + this._completedMeasures.Count + this._columns.Count + l] = this._completedColumns[l];
				}
				base.ActiveObject._props = array;
				this.BuildPropsDictionaries(base.ActiveObject);
				return base.CompleteBaseInitialization();
			}

			// Token: 0x06001933 RID: 6451 RVA: 0x0002D560 File Offset: 0x0002B760
			private void BuildPropsDictionaries(ConceptualEntity entity)
			{
				Dictionary<string, IConceptualProperty> dictionary = new Dictionary<string, IConceptualProperty>(EdmNameComparer.Instance);
				Dictionary<string, IConceptualProperty> dictionary2 = new Dictionary<string, IConceptualProperty>(ConceptualNameComparer.Instance);
				foreach (IConceptualProperty conceptualProperty in entity.Properties)
				{
					dictionary.Add(conceptualProperty.EdmName, conceptualProperty);
					dictionary2.Add(conceptualProperty.Name, conceptualProperty);
				}
				base.ActiveObject._edmNamesToProps = dictionary;
				base.ActiveObject._propNamesToProps = dictionary2;
			}

			// Token: 0x04000940 RID: 2368
			private readonly IReadOnlyList<ConceptualMeasure.Builder> _measures;

			// Token: 0x04000941 RID: 2369
			private readonly IReadOnlyList<ConceptualColumn.Builder> _columns;

			// Token: 0x04000942 RID: 2370
			private List<IConceptualColumn> _completedColumns;

			// Token: 0x04000943 RID: 2371
			private List<IConceptualMeasure> _completedMeasures;
		}
	}
}
