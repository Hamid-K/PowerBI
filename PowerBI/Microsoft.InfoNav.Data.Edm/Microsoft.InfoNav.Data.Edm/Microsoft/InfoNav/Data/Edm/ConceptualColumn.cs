using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000D RID: 13
	[DebuggerDisplay("{Name}")]
	[ImmutableObject(true)]
	internal class ConceptualColumn : ConceptualProperty, IConceptualColumn, IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002A34 File Offset: 0x00000C34
		internal ConceptualColumn(ParsedEdmStructuralProperty parsedEdmStructuralProperty, DataType dataType, ConceptualPrimitiveType conceptualDataType, int ordinal, string description)
			: base(parsedEdmStructuralProperty, dataType, conceptualDataType, ordinal, description)
		{
			this._conceptualDefaultAggregate = parsedEdmStructuralProperty.ConceptualDefaultAggregate;
			this._defaultValue = parsedEdmStructuralProperty.DefaultValue;
			this._aggregateBehavior = parsedEdmStructuralProperty.AggregateBehavior;
			this._isRowNumber = parsedEdmStructuralProperty.IsRowNumber;
			this.ConceptualTypeColumn = ConceptualPrimitiveResultType.FromPrimitive(conceptualDataType).Column(parsedEdmStructuralProperty.ReferenceName, parsedEdmStructuralProperty.EdmName);
			this._nullable = parsedEdmStructuralProperty.Nullable;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002AA7 File Offset: 0x00000CA7
		public ConceptualColumnGrouping Grouping
		{
			get
			{
				return this._grouping;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002AAF File Offset: 0x00000CAF
		public IReadOnlyList<IConceptualColumn> OrderByColumns
		{
			get
			{
				return this._orderByColumns;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002AB7 File Offset: 0x00000CB7
		public ConceptualDefaultAggregate ConceptualDefaultAggregate
		{
			get
			{
				return this._conceptualDefaultAggregate;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002ABF File Offset: 0x00000CBF
		public PrimitiveValue DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public AggregateBehavior AggregateBehavior
		{
			get
			{
				return this._aggregateBehavior;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002ACF File Offset: 0x00000CCF
		public bool IsRowNumber
		{
			get
			{
				return this._isRowNumber;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public IReadOnlyList<IConceptualVariationSource> VariationSources
		{
			get
			{
				return this._variations;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002ADF File Offset: 0x00000CDF
		public ConceptualGroupingMetadata GroupingMetadata
		{
			get
			{
				return this._groupingMetadata;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AE7 File Offset: 0x00000CE7
		public ConceptualParameterMetadata ParameterMetadata
		{
			get
			{
				return this._parameterMetadata;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002AEF File Offset: 0x00000CEF
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public ConceptualTypeColumn ConceptualTypeColumn { get; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002AFF File Offset: 0x00000CFF
		public bool TryGetVariationSource(string name, out IConceptualVariationSource conceptualVariationSource)
		{
			return this._variationSourceNamesToProps.TryGetValue(name, out conceptualVariationSource);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B0E File Offset: 0x00000D0E
		internal void CompleteInitialization(IConceptualEntity parentEntity, IReadOnlyList<IConceptualColumn> orderByColumns, ConceptualColumnGrouping grouping, IReadOnlyList<ConceptualVariationSource> variations, ConceptualGroupingMetadata groupingMetadata, ConceptualParameterMetadata parameterMetadata)
		{
			base.CompleteInitialization(parentEntity);
			this._orderByColumns = orderByColumns;
			this._grouping = grouping;
			this._variations = variations;
			this._variationSourceNamesToProps = ConceptualColumn.BuildVariationSourcesByName(this._variations);
			this._groupingMetadata = groupingMetadata;
			this._parameterMetadata = parameterMetadata;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B50 File Offset: 0x00000D50
		private static IReadOnlyDictionary<string, IConceptualVariationSource> BuildVariationSourcesByName(IReadOnlyList<IConceptualVariationSource> variationSources)
		{
			Dictionary<string, IConceptualVariationSource> dictionary = new Dictionary<string, IConceptualVariationSource>(variationSources.Count, ConceptualNameComparer.Instance);
			foreach (IConceptualVariationSource conceptualVariationSource in variationSources)
			{
				dictionary.Add(conceptualVariationSource.Name, conceptualVariationSource);
			}
			return dictionary;
		}

		// Token: 0x0400003A RID: 58
		private readonly ConceptualDefaultAggregate _conceptualDefaultAggregate;

		// Token: 0x0400003B RID: 59
		private readonly PrimitiveValue _defaultValue;

		// Token: 0x0400003C RID: 60
		private readonly AggregateBehavior _aggregateBehavior;

		// Token: 0x0400003D RID: 61
		private readonly bool _isRowNumber;

		// Token: 0x0400003E RID: 62
		private ConceptualColumnGrouping _grouping;

		// Token: 0x0400003F RID: 63
		private IReadOnlyList<IConceptualColumn> _orderByColumns;

		// Token: 0x04000040 RID: 64
		private IReadOnlyList<ConceptualVariationSource> _variations;

		// Token: 0x04000041 RID: 65
		private IReadOnlyDictionary<string, IConceptualVariationSource> _variationSourceNamesToProps;

		// Token: 0x04000042 RID: 66
		private ConceptualGroupingMetadata _groupingMetadata;

		// Token: 0x04000043 RID: 67
		private ConceptualParameterMetadata _parameterMetadata;

		// Token: 0x04000044 RID: 68
		private bool _nullable;
	}
}
