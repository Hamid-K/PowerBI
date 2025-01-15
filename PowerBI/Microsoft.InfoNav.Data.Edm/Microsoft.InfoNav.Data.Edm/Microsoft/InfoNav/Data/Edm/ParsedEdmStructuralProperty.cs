using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200002A RID: 42
	[ImmutableObject(true)]
	internal sealed class ParsedEdmStructuralProperty
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00008638 File Offset: 0x00006838
		private ParsedEdmStructuralProperty(bool isMeasure, bool isHidden, bool isPrivate, bool shouldKeepUniqueRows, bool isRowNumber, bool isStable, bool isVariant, bool isError, bool nullable, ConceptualDataCategory conceptualDataCategory, PropertyDataCategory propertyDataCategory, AggregateBehavior aggregateBehavior, PrimitiveValue defaultValue, ConceptualDefaultAggregate conceptualDefaultAggregate, string edmName, string fullName, string formatString, string referenceName, string displayName, string dynamicFormatPropRef, string dynamicCulturePropRef, IEnumerable<string> orderByProperties, IEnumerable<string> groupByProperties, IEnumerable<string> relatedToProperties, ConceptualColumnStatistics statistics, EdmKpi edmKpi, IReadOnlyList<EdmVariation> edmVariations, IReadOnlyList<ExtendedProperty> edmExtendedProperties, EdmDistributiveBy edmDistributiveBy, string stableName, IReadOnlyList<ConceptualTranslation> translations)
		{
			this._isMeasure = isMeasure;
			this._isHidden = isHidden;
			this._isPrivate = isPrivate;
			this._shouldKeepUniqueRows = shouldKeepUniqueRows;
			this._isRowNumber = isRowNumber;
			this._isStable = isStable;
			this._isVariant = isVariant;
			this._isError = isError;
			this._nullable = nullable;
			this._conceptualDataCategory = conceptualDataCategory;
			this._propertyDataCategory = propertyDataCategory;
			this._aggregateBehavior = aggregateBehavior;
			this._defaultValue = defaultValue;
			this._conceptualDefaultAggregate = conceptualDefaultAggregate;
			this._edmName = edmName;
			this._fullName = fullName;
			this._formatString = formatString;
			this._referenceName = referenceName;
			this._displayName = displayName;
			this._dynamicFormatPropRef = dynamicFormatPropRef;
			this._dynamicCulturePropRef = dynamicCulturePropRef;
			this._orderByProperties = orderByProperties;
			this._groupByProperties = groupByProperties;
			this._relatedToProperties = relatedToProperties;
			this._statistics = statistics;
			this._edmKpi = edmKpi;
			this._edmVariations = edmVariations;
			this._edmExtendedProperties = edmExtendedProperties;
			this._edmDistributiveBy = edmDistributiveBy;
			this._stableName = stableName;
			this._translations = translations;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00008740 File Offset: 0x00006940
		internal bool IsMeasure
		{
			get
			{
				return this._isMeasure;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008748 File Offset: 0x00006948
		internal bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008750 File Offset: 0x00006950
		internal bool IsPrivate
		{
			get
			{
				return this._isPrivate;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00008758 File Offset: 0x00006958
		internal bool ShouldKeepUniqueRows
		{
			get
			{
				return this._shouldKeepUniqueRows;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008760 File Offset: 0x00006960
		internal bool IsRowNumber
		{
			get
			{
				return this._isRowNumber;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008768 File Offset: 0x00006968
		internal bool IsStable
		{
			get
			{
				return this._isStable;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008770 File Offset: 0x00006970
		internal bool IsVariant
		{
			get
			{
				return this._isVariant;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008778 File Offset: 0x00006978
		internal bool IsError
		{
			get
			{
				return this._isError;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008780 File Offset: 0x00006980
		internal bool Nullable
		{
			get
			{
				return this._nullable;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008788 File Offset: 0x00006988
		internal ConceptualDataCategory ConceptualDataCategory
		{
			get
			{
				return this._conceptualDataCategory;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008790 File Offset: 0x00006990
		internal PropertyDataCategory PropertyDataCategory
		{
			get
			{
				return this._propertyDataCategory;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008798 File Offset: 0x00006998
		internal AggregateBehavior AggregateBehavior
		{
			get
			{
				return this._aggregateBehavior;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000087A0 File Offset: 0x000069A0
		internal PrimitiveValue DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000087A8 File Offset: 0x000069A8
		internal ConceptualDefaultAggregate ConceptualDefaultAggregate
		{
			get
			{
				return this._conceptualDefaultAggregate;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000087B0 File Offset: 0x000069B0
		internal string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000087B8 File Offset: 0x000069B8
		internal string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000087C0 File Offset: 0x000069C0
		internal string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000087C8 File Offset: 0x000069C8
		internal string ReferenceName
		{
			get
			{
				return this._referenceName;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000087D0 File Offset: 0x000069D0
		internal string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000087D8 File Offset: 0x000069D8
		internal string DynamicFormatPropRef
		{
			get
			{
				return this._dynamicFormatPropRef;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000087E0 File Offset: 0x000069E0
		internal string DynamicCulturePropRef
		{
			get
			{
				return this._dynamicCulturePropRef;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000087E8 File Offset: 0x000069E8
		internal IEnumerable<string> OrderByProperties
		{
			get
			{
				return this._orderByProperties;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000087F0 File Offset: 0x000069F0
		internal IEnumerable<string> GroupByProperties
		{
			get
			{
				return this._groupByProperties;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000087F8 File Offset: 0x000069F8
		internal IEnumerable<string> RelatedToProperties
		{
			get
			{
				return this._relatedToProperties;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008800 File Offset: 0x00006A00
		internal ConceptualColumnStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00008808 File Offset: 0x00006A08
		internal EdmKpi EdmKpi
		{
			get
			{
				return this._edmKpi;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008810 File Offset: 0x00006A10
		internal IReadOnlyList<EdmVariation> EdmVariations
		{
			get
			{
				return this._edmVariations;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008818 File Offset: 0x00006A18
		internal IReadOnlyList<ExtendedProperty> EdmExtendedProperties
		{
			get
			{
				return this._edmExtendedProperties;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008820 File Offset: 0x00006A20
		internal EdmDistributiveBy EdmDistributiveBy
		{
			get
			{
				return this._edmDistributiveBy;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008828 File Offset: 0x00006A28
		internal string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00008830 File Offset: 0x00006A30
		internal IReadOnlyList<ConceptualTranslation> Translations
		{
			get
			{
				return this._translations;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008838 File Offset: 0x00006A38
		internal static ParsedEdmStructuralProperty Parse(ITracer tracer, IEdmStructuralProperty edmStructuralProperty, string propertyAnnotationString, string measureAnnotationString, bool skipYearColumnDetectionHeuristics)
		{
			IEdmTypeReference type = edmStructuralProperty.Type;
			string name = edmStructuralProperty.Name;
			string fullName = edmStructuralProperty.GetFullName();
			bool flag = false;
			XElement xelement = null;
			if (measureAnnotationString != null)
			{
				xelement = XElement.Parse(measureAnnotationString);
				flag = true;
			}
			else if (propertyAnnotationString != null)
			{
				xelement = XElement.Parse(propertyAnnotationString);
			}
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = true;
			bool flag7 = false;
			bool flag8 = false;
			ConceptualDataCategory conceptualDataCategory = ConceptualDataCategory.None;
			PropertyDataCategory propertyDataCategory = PropertyDataCategory.None;
			AggregateBehavior aggregateBehavior = AggregateBehavior.Default;
			PrimitiveValue primitiveValue = null;
			ConceptualDefaultAggregate conceptualDefaultAggregate = ConceptualDefaultAggregate.Default;
			string text = null;
			string text2 = name;
			string text3 = text2;
			string text4 = null;
			string text5 = null;
			IEnumerable<string> enumerable = Util.EmptyReadOnlyCollection<string>();
			IEnumerable<string> enumerable2 = Util.EmptyReadOnlyCollection<string>();
			IEnumerable<string> enumerable3 = Util.EmptyReadOnlyCollection<string>();
			ConceptualColumnStatistics conceptualColumnStatistics = null;
			EdmKpi edmKpi = null;
			IReadOnlyList<EdmVariation> readOnlyList = null;
			IReadOnlyList<ExtendedProperty> readOnlyList2 = null;
			EdmDistributiveBy edmDistributiveBy = null;
			string stringAttributeValueOrDefault = xelement.GetStringAttributeValueOrDefault(EdmConstants.StableNameAttr, null);
			IReadOnlyList<ConceptualTranslation> readOnlyList3;
			if (xelement == null)
			{
				readOnlyList3 = null;
			}
			else
			{
				XElement xelement2 = xelement.Element(EdmConstants.Cultures);
				readOnlyList3 = ((xelement2 != null) ? xelement2.GetTranslations() : null);
			}
			IReadOnlyList<ConceptualTranslation> readOnlyList4 = readOnlyList3;
			if (xelement != null)
			{
				Dictionary<string, XAttribute> dictionary = xelement.Attributes().ToDictionary((XAttribute a) => a.Name.LocalName);
				Dictionary<string, XElement> dictionary2 = (from a in xelement.Elements()
					where !a.Name.LocalName.Equals(EdmConstants.ExtendedProperty.LocalName)
					select a).ToDictionary((XElement e) => e.Name.LocalName);
				XAttribute xattribute;
				if (dictionary.TryGetValue(EdmConstants.HiddenAttr.LocalName, out xattribute))
				{
					flag2 = XmlConvert.ToBoolean(xattribute.Value);
				}
				if (dictionary.TryGetValue(EdmConstants.PrivateAttr.LocalName, out xattribute))
				{
					flag3 = XmlConvert.ToBoolean(xattribute.Value);
				}
				if (dictionary.TryGetValue(EdmConstants.GroupingBehaviorAttr.LocalName, out xattribute) && xattribute.Value == "GroupOnEntityKey")
				{
					flag4 = true;
				}
				if (dictionary.TryGetValue(EdmConstants.IsErrorAttr.LocalName, out xattribute))
				{
					flag8 = XmlConvert.ToBoolean(xattribute.Value);
				}
				bool flag9 = false;
				if (dictionary.TryGetValue(EdmConstants.ContentsAttr.LocalName, out xattribute))
				{
					if (!Enum.TryParse<ConceptualDataCategory>(xattribute.Value, out conceptualDataCategory))
					{
						conceptualDataCategory = ConceptualDataCategory.None;
					}
					if (!(flag9 = Enum.TryParse<PropertyDataCategory>(xattribute.Value, true, out propertyDataCategory)))
					{
						propertyDataCategory = PropertyDataCategory.None;
					}
				}
				if (dictionary.TryGetValue(EdmConstants.StabilityAttr.LocalName, out xattribute))
				{
					if (xattribute.Value == "RowNumber")
					{
						flag5 = true;
					}
					flag6 = xattribute.Value == "Stable";
				}
				if (dictionary.TryGetValue(EdmConstants.FormatStringAttr.LocalName, out xattribute))
				{
					text = xattribute.Value;
				}
				if (dictionary.TryGetValue(EdmConstants.ReferenceNameAttr.LocalName, out xattribute))
				{
					text2 = xattribute.Value;
				}
				if (dictionary.TryGetValue(EdmConstants.ActualTypeAttr.LocalName, out xattribute))
				{
					flag7 = xattribute.Value.Equals("Any");
				}
				text3 = (dictionary.TryGetValue(EdmConstants.CaptionAttr.LocalName, out xattribute) ? xattribute.Value : text2);
				if (flag)
				{
					aggregateBehavior = AggregateBehavior.Default;
					primitiveValue = null;
					XElement xelement3;
					if (dictionary2.TryGetValue(EdmConstants.Kpi.LocalName, out xelement3))
					{
						edmKpi = ParsedEdmStructuralProperty.GetKpi(xelement3);
					}
					if (dictionary2.TryGetValue(EdmConstants.FormatBy.LocalName, out xelement3))
					{
						text4 = EdmExtensions.GetPropertyRefNameForElement(xelement3);
					}
					if (dictionary2.TryGetValue(EdmConstants.ApplyCulture.LocalName, out xelement3))
					{
						text5 = EdmExtensions.GetPropertyRefNameForElement(xelement3);
					}
					if (dictionary2.TryGetValue(EdmConstants.DistributiveBy.LocalName, out xelement3))
					{
						edmDistributiveBy = ParsedEdmStructuralProperty.GetDistributiveBy(xelement3);
					}
				}
				else
				{
					aggregateBehavior = dictionary.GetAggregateBehavior(name, tracer);
					primitiveValue = dictionary2.GetDefaultValue(name, type, tracer);
					XElement xelement4;
					if (dictionary2.TryGetValue(EdmConstants.OrderBy.LocalName, out xelement4))
					{
						IEnumerable<XAttribute> enumerable4 = xelement4.Elements(EdmConstants.PropertyRef).Attributes(EdmConstants.NameAttr);
						Func<XAttribute, string> func;
						if ((func = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
						{
							func = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
						}
						enumerable = enumerable4.Select(func);
					}
					XElement xelement5;
					if (dictionary2.TryGetValue(EdmConstants.GroupBy.LocalName, out xelement5))
					{
						IEnumerable<XAttribute> enumerable5 = xelement5.Elements(EdmConstants.PropertyRef).Attributes(EdmConstants.NameAttr);
						Func<XAttribute, string> func2;
						if ((func2 = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
						{
							func2 = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
						}
						enumerable2 = enumerable5.Select(func2);
					}
					XElement xelement6;
					if (dictionary2.TryGetValue(EdmConstants.RelatedTo.LocalName, out xelement6))
					{
						IEnumerable<XAttribute> enumerable6 = xelement6.Elements(EdmConstants.PropertyRef).Attributes(EdmConstants.NameAttr);
						Func<XAttribute, string> func3;
						if ((func3 = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
						{
							func3 = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
						}
						enumerable3 = enumerable6.Select(func3);
					}
					readOnlyList = dictionary2.GetVariations(tracer);
				}
				if (dictionary.TryGetValue(EdmConstants.DefaultAggregateFunction.LocalName, out xattribute) && !Enum.TryParse<ConceptualDefaultAggregate>(xattribute.Value, out conceptualDefaultAggregate))
				{
					conceptualDefaultAggregate = ConceptualDefaultAggregate.Default;
				}
				if (flag || !dictionary2.TryGetPropertyStatistics(type, name, tracer, propertyDataCategory, out conceptualColumnStatistics))
				{
					conceptualColumnStatistics = null;
				}
				if (!skipYearColumnDetectionHeuristics && !flag9 && edmStructuralProperty.IsRecognizedAsYearColumn(flag, conceptualColumnStatistics))
				{
					propertyDataCategory = PropertyDataCategory.Year;
				}
				readOnlyList2 = EdmExtensions.GetExtendedPropertyList(xelement);
			}
			return new ParsedEdmStructuralProperty(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, type.IsNullable, conceptualDataCategory, propertyDataCategory, aggregateBehavior, primitiveValue, conceptualDefaultAggregate, name, fullName, text, text2, text3, text4, text5, enumerable, enumerable2, enumerable3, conceptualColumnStatistics, edmKpi, readOnlyList, readOnlyList2, edmDistributiveBy, stringAttributeValueOrDefault, readOnlyList4);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008D58 File Offset: 0x00006F58
		private static EdmKpi GetKpi(XElement kpiElement)
		{
			IEnumerable<XAttribute> enumerable = kpiElement.Attributes(EdmConstants.StatusGraphicAttr);
			Func<XAttribute, string> func;
			if ((func = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
			{
				func = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text = enumerable.Select(func).FirstOrDefault<string>();
			IEnumerable<XAttribute> enumerable2 = kpiElement.Attributes(EdmConstants.TrendGraphicAttr);
			Func<XAttribute, string> func2;
			if ((func2 = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
			{
				func2 = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text2 = enumerable2.Select(func2).FirstOrDefault<string>();
			string propertyRefNameForElement = EdmExtensions.GetPropertyRefNameForElement(kpiElement.Elements(EdmConstants.KpiGoal).FirstOrDefault<XElement>());
			string propertyRefNameForElement2 = EdmExtensions.GetPropertyRefNameForElement(kpiElement.Elements(EdmConstants.KpiStatus).FirstOrDefault<XElement>());
			string propertyRefNameForElement3 = EdmExtensions.GetPropertyRefNameForElement(kpiElement.Elements(EdmConstants.KpiTrend).FirstOrDefault<XElement>());
			XElement xelement = kpiElement.Element(EdmConstants.Documentation);
			string text3;
			if (xelement == null)
			{
				text3 = null;
			}
			else
			{
				IEnumerable<XElement> enumerable3 = xelement.Elements(EdmConstants.Summary);
				if (enumerable3 == null)
				{
					text3 = null;
				}
				else
				{
					XElement xelement2 = enumerable3.FirstOrDefault<XElement>();
					text3 = ((xelement2 != null) ? xelement2.Value : null);
				}
			}
			string text4 = text3;
			return new EdmKpi(text, text2, propertyRefNameForElement, propertyRefNameForElement2, propertyRefNameForElement3, text4);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008E4C File Offset: 0x0000704C
		private static EdmDistributiveBy GetDistributiveBy(XElement distributivityElement)
		{
			IEnumerable<XAttribute> enumerable = distributivityElement.Attributes(EdmConstants.AggregationKindAttr);
			Func<XAttribute, string> func;
			if ((func = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
			{
				func = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text = enumerable.Select(func).FirstOrDefault<string>();
			IReadOnlyList<string> readOnlyList = null;
			IEnumerable<XElement> enumerable2 = from e in distributivityElement.Elements()
				where e.Name.LocalName.Equals(EdmConstants.EntityRef.LocalName)
				select e;
			if (enumerable2 != null)
			{
				readOnlyList = enumerable2.Select(delegate(XElement e)
				{
					IEnumerable<XAttribute> enumerable3 = e.Attributes(EdmConstants.NameAttr);
					Func<XAttribute, string> func2;
					if ((func2 = ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute) == null)
					{
						func2 = (ParsedEdmStructuralProperty.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
					}
					return enumerable3.Select(func2).FirstOrDefault<string>();
				}).ToList<string>();
			}
			return new EdmDistributiveBy(text, readOnlyList);
		}

		// Token: 0x04000173 RID: 371
		private readonly bool _isMeasure;

		// Token: 0x04000174 RID: 372
		private readonly bool _isHidden;

		// Token: 0x04000175 RID: 373
		private readonly bool _isPrivate;

		// Token: 0x04000176 RID: 374
		private readonly bool _shouldKeepUniqueRows;

		// Token: 0x04000177 RID: 375
		private readonly bool _isRowNumber;

		// Token: 0x04000178 RID: 376
		private readonly bool _isStable;

		// Token: 0x04000179 RID: 377
		private readonly bool _isVariant;

		// Token: 0x0400017A RID: 378
		private readonly bool _isError;

		// Token: 0x0400017B RID: 379
		private readonly bool _nullable;

		// Token: 0x0400017C RID: 380
		private readonly ConceptualDataCategory _conceptualDataCategory;

		// Token: 0x0400017D RID: 381
		private readonly PropertyDataCategory _propertyDataCategory;

		// Token: 0x0400017E RID: 382
		private readonly AggregateBehavior _aggregateBehavior;

		// Token: 0x0400017F RID: 383
		private readonly PrimitiveValue _defaultValue;

		// Token: 0x04000180 RID: 384
		private readonly ConceptualDefaultAggregate _conceptualDefaultAggregate;

		// Token: 0x04000181 RID: 385
		private readonly string _edmName;

		// Token: 0x04000182 RID: 386
		private readonly string _fullName;

		// Token: 0x04000183 RID: 387
		private readonly string _formatString;

		// Token: 0x04000184 RID: 388
		private readonly string _referenceName;

		// Token: 0x04000185 RID: 389
		private readonly string _displayName;

		// Token: 0x04000186 RID: 390
		private readonly string _dynamicFormatPropRef;

		// Token: 0x04000187 RID: 391
		private readonly string _dynamicCulturePropRef;

		// Token: 0x04000188 RID: 392
		private readonly IEnumerable<string> _orderByProperties;

		// Token: 0x04000189 RID: 393
		private readonly IEnumerable<string> _groupByProperties;

		// Token: 0x0400018A RID: 394
		private readonly IEnumerable<string> _relatedToProperties;

		// Token: 0x0400018B RID: 395
		private readonly ConceptualColumnStatistics _statistics;

		// Token: 0x0400018C RID: 396
		private readonly EdmKpi _edmKpi;

		// Token: 0x0400018D RID: 397
		private readonly IReadOnlyList<EdmVariation> _edmVariations;

		// Token: 0x0400018E RID: 398
		private readonly IReadOnlyList<ExtendedProperty> _edmExtendedProperties;

		// Token: 0x0400018F RID: 399
		private readonly EdmDistributiveBy _edmDistributiveBy;

		// Token: 0x04000190 RID: 400
		private readonly string _stableName;

		// Token: 0x04000191 RID: 401
		private readonly IReadOnlyList<ConceptualTranslation> _translations;

		// Token: 0x0200004C RID: 76
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001E6 RID: 486
			public static Func<XAttribute, string> <0>__GetValueForAttribute;
		}
	}
}
