using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200024F RID: 591
	public abstract class StructuralType : EdmType
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00047688 File Offset: 0x00045888
		public EdmMemberCollection<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00047690 File Offset: 0x00045890
		public EdmMemberCollection<EdmField> Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060019EA RID: 6634
		internal abstract StructuralType InternalStructuralType { get; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00047698 File Offset: 0x00045898
		internal sealed override EdmType InternalEdmType
		{
			get
			{
				return this.InternalStructuralType;
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000476A0 File Offset: 0x000458A0
		internal void InternalInit()
		{
			this.InternalInit(null);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000476AC File Offset: 0x000458AC
		internal virtual void InternalInit(Version version)
		{
			List<KeyValuePair<EdmProperty, StructuralType.PropertyLateBoundState>> lateBoundStates = new List<KeyValuePair<EdmProperty, StructuralType.PropertyLateBoundState>>();
			this._members = new EdmMemberCollection<EdmMember>((from edmMember in this.InternalStructuralType.Members.OfType<EdmProperty>()
				select this.CreateMember(edmMember, lateBoundStates)).Concat(from edmMember in this.InternalStructuralType.Members.OfType<AssociationEndMember>()
				select this.CreateMember(edmMember, lateBoundStates)));
			this._fields = new EdmMemberCollection<EdmField>(this._members.OfType<EdmField>());
			foreach (KeyValuePair<EdmProperty, StructuralType.PropertyLateBoundState> keyValuePair in lateBoundStates)
			{
				keyValuePair.Value.Apply(this, keyValuePair.Key);
			}
			foreach (EdmField edmField in this._members.OfType<EdmField>())
			{
				edmField.CompleteInitialization();
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000477CC File Offset: 0x000459CC
		private EdmMember CreateMember(EdmMember edmMember, IList<KeyValuePair<EdmProperty, StructuralType.PropertyLateBoundState>> propertyLateBoundStates)
		{
			EdmProperty edmProperty = edmMember as EdmProperty;
			if (edmProperty != null)
			{
				ConceptualPrimitiveResultType conceptualPrimitiveResultType = EdmConceptualTypeConverter.ConvertTypeForPrimitive(edmMember.TypeUsage);
				XElement xelementMetadataProperty = edmProperty.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Measure");
				XElement xelementMetadataProperty2 = edmProperty.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Property");
				EdmProperty edmProperty2;
				StructuralType.PropertyLateBoundState propertyLateBoundState;
				if (xelementMetadataProperty != null)
				{
					edmProperty2 = new EdmMeasure(edmProperty, conceptualPrimitiveResultType, this, xelementMetadataProperty);
					propertyLateBoundState = StructuralType.MeasureLateBoundState.Create(xelementMetadataProperty);
				}
				else
				{
					edmProperty2 = new EdmField(edmProperty, conceptualPrimitiveResultType, this, xelementMetadataProperty2);
					propertyLateBoundState = StructuralType.FieldLateBoundState.Create(xelementMetadataProperty2);
				}
				if (propertyLateBoundState != null)
				{
					propertyLateBoundStates.Add(Microsoft.DataShaping.Util.ToKeyValuePair<EdmProperty, StructuralType.PropertyLateBoundState>(edmProperty2, propertyLateBoundState));
				}
				return edmProperty2;
			}
			AssociationEndMember associationEndMember = edmMember as AssociationEndMember;
			if (associationEndMember != null)
			{
				return new AssociationEndMember(associationEndMember, this);
			}
			throw new NotSupportedException();
		}

		// Token: 0x04000E6F RID: 3695
		private EdmMemberCollection<EdmMember> _members;

		// Token: 0x04000E70 RID: 3696
		private EdmMemberCollection<EdmField> _fields;

		// Token: 0x020003E4 RID: 996
		private abstract class PropertyLateBoundState
		{
			// Token: 0x06002102 RID: 8450
			internal abstract void Apply(StructuralType type, EdmProperty property);

			// Token: 0x06002103 RID: 8451 RVA: 0x00059A60 File Offset: 0x00057C60
			protected static string GetNestedPropertyRef(XElement parentElement)
			{
				if (parentElement != null)
				{
					XElement elementOrNull = parentElement.GetElementOrNull(Extensions.PropertyRefElem);
					if (elementOrNull != null)
					{
						return StructuralType.PropertyLateBoundState.GetMandatoryAttribute(elementOrNull, Extensions.NameAttr);
					}
				}
				return null;
			}

			// Token: 0x06002104 RID: 8452 RVA: 0x00059A8C File Offset: 0x00057C8C
			protected static IEnumerable<string> GetNestedPropertyRefs(XElement parentElement)
			{
				if (parentElement != null)
				{
					foreach (XElement xelement in parentElement.Elements(Extensions.PropertyRefElem))
					{
						yield return StructuralType.PropertyLateBoundState.GetMandatoryAttribute(xelement, Extensions.NameAttr);
					}
					IEnumerator<XElement> enumerator = null;
				}
				yield break;
				yield break;
			}

			// Token: 0x06002105 RID: 8453 RVA: 0x00059A9C File Offset: 0x00057C9C
			protected static string GetMandatoryAttribute(XElement element, XName attribute)
			{
				ArgumentValidation.CheckNotNull<XElement>(element, "element");
				XAttribute xattribute = element.Attribute(attribute);
				ArgumentValidation.CheckCondition(xattribute != null, "element");
				return xattribute.Value;
			}

			// Token: 0x06002106 RID: 8454 RVA: 0x00059AC4 File Offset: 0x00057CC4
			protected static string GetOptionalAttribute(XElement element, XName attribute)
			{
				ArgumentValidation.CheckNotNull<XElement>(element, "element");
				XAttribute xattribute = element.Attribute(attribute);
				if (xattribute == null)
				{
					return null;
				}
				return xattribute.Value;
			}
		}

		// Token: 0x020003E5 RID: 997
		private sealed class FieldLateBoundState : StructuralType.PropertyLateBoundState
		{
			// Token: 0x06002108 RID: 8456 RVA: 0x00059AF8 File Offset: 0x00057CF8
			private FieldLateBoundState(string[] groupByFields, string[] orderByFields, string[] filterNullsByFieldRefs, string[] relatedToFieldRefs)
			{
				this._groupByFieldRefs = ArgumentValidation.CheckNotNull<string[]>(groupByFields, "groupByFields");
				this._orderByFieldRefs = ArgumentValidation.CheckNotNull<string[]>(orderByFields, "orderByFields");
				this._filterNullsByFieldRefs = ArgumentValidation.CheckNotNull<string[]>(filterNullsByFieldRefs, "filterNullsByFieldRefs");
				this._relatedToFieldRefs = relatedToFieldRefs;
			}

			// Token: 0x06002109 RID: 8457 RVA: 0x00059B48 File Offset: 0x00057D48
			internal static StructuralType.FieldLateBoundState Create(XElement fieldElem)
			{
				string[] array = StructuralType.PropertyLateBoundState.GetNestedPropertyRefs(fieldElem.GetElementOrNull(Extensions.GroupByElem)).ToArray<string>();
				string[] array2 = StructuralType.PropertyLateBoundState.GetNestedPropertyRefs(fieldElem.GetElementOrNull(Extensions.OrderByElem)).ToArray<string>();
				string[] array3 = StructuralType.PropertyLateBoundState.GetNestedPropertyRefs(fieldElem.GetElementOrNull(Extensions.FilterNullsByElem)).ToArray<string>();
				string[] array4 = StructuralType.PropertyLateBoundState.GetNestedPropertyRefs(fieldElem.GetElementOrNull(Extensions.RelatedToElem)).ToArray<string>();
				if (array.Length != 0 || array2.Length != 0 || array3.Length != 0 || array4.Length != 0)
				{
					return new StructuralType.FieldLateBoundState(array, array2, array3, array4);
				}
				return null;
			}

			// Token: 0x0600210A RID: 8458 RVA: 0x00059BC8 File Offset: 0x00057DC8
			internal override void Apply(StructuralType type, EdmProperty property)
			{
				EdmField edmField = ArgumentValidation.CheckAs<EdmField>(property, "property");
				ReadOnlyCollection<EdmField> readOnlyCollection = StructuralType.FieldLateBoundState.ResolveFieldReferences(type, this._groupByFieldRefs, (EdmField f) => f.CanGroupOnValue());
				string[] orderByFieldRefs = this._orderByFieldRefs;
				Predicate<EdmField> predicate;
				if ((predicate = StructuralType.FieldLateBoundState.<>O.<0>__IsSortable) == null)
				{
					predicate = (StructuralType.FieldLateBoundState.<>O.<0>__IsSortable = new Predicate<EdmField>(Extensions.IsSortable));
				}
				ReadOnlyCollection<EdmField> readOnlyCollection2 = StructuralType.FieldLateBoundState.ResolveFieldReferences(type, orderByFieldRefs, predicate);
				string[] filterNullsByFieldRefs = this._filterNullsByFieldRefs;
				Predicate<EdmField> predicate2;
				if ((predicate2 = StructuralType.FieldLateBoundState.<>O.<0>__IsSortable) == null)
				{
					predicate2 = (StructuralType.FieldLateBoundState.<>O.<0>__IsSortable = new Predicate<EdmField>(Extensions.IsSortable));
				}
				edmField.InitializeDeferredModelProperties(readOnlyCollection, readOnlyCollection2, StructuralType.FieldLateBoundState.ResolveFieldReferences(type, filterNullsByFieldRefs, predicate2), StructuralType.FieldLateBoundState.ResolveFieldReferences(type, this._relatedToFieldRefs, null));
			}

			// Token: 0x0600210B RID: 8459 RVA: 0x00059C6C File Offset: 0x00057E6C
			private static ReadOnlyCollection<EdmField> ResolveFieldReferences(StructuralType type, string[] fieldRefs, Predicate<EdmField> acceptField)
			{
				if (fieldRefs.Length == 0)
				{
					return null;
				}
				List<EdmField> list = new List<EdmField>(fieldRefs.Length);
				foreach (string text in fieldRefs)
				{
					EdmField edmField = type.Fields[text];
					if (acceptField != null && !acceptField(edmField))
					{
						return null;
					}
					if (!list.Contains(edmField))
					{
						list.Add(edmField);
					}
				}
				return list.AsReadOnly();
			}

			// Token: 0x040013F3 RID: 5107
			private readonly string[] _groupByFieldRefs;

			// Token: 0x040013F4 RID: 5108
			private readonly string[] _orderByFieldRefs;

			// Token: 0x040013F5 RID: 5109
			private readonly string[] _filterNullsByFieldRefs;

			// Token: 0x040013F6 RID: 5110
			private readonly string[] _relatedToFieldRefs;

			// Token: 0x02000454 RID: 1108
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040014F9 RID: 5369
				public static Predicate<EdmField> <0>__IsSortable;
			}
		}

		// Token: 0x020003E6 RID: 998
		private sealed class MeasureLateBoundState : StructuralType.PropertyLateBoundState
		{
			// Token: 0x0600210C RID: 8460 RVA: 0x00059CD3 File Offset: 0x00057ED3
			private MeasureLateBoundState(StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState measureKpiLateboundState, string formatByProperty, string applyCultureProperty)
			{
				this._kpi = measureKpiLateboundState;
				this._formatBy = formatByProperty;
				this._applyCulture = applyCultureProperty;
			}

			// Token: 0x0600210D RID: 8461 RVA: 0x00059CF0 File Offset: 0x00057EF0
			internal static StructuralType.MeasureLateBoundState Create(XElement measureElem)
			{
				StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState measureKpiLateBoundState = StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState.Create(measureElem);
				string nestedPropertyRef = StructuralType.PropertyLateBoundState.GetNestedPropertyRef(measureElem.GetElementOrNull(Extensions.FormatByElem));
				string nestedPropertyRef2 = StructuralType.PropertyLateBoundState.GetNestedPropertyRef(measureElem.GetElementOrNull(Extensions.ApplyCultureElem));
				if (measureKpiLateBoundState != null || !string.IsNullOrEmpty(nestedPropertyRef) || !string.IsNullOrEmpty(nestedPropertyRef2))
				{
					return new StructuralType.MeasureLateBoundState(measureKpiLateBoundState, nestedPropertyRef, nestedPropertyRef2);
				}
				return null;
			}

			// Token: 0x0600210E RID: 8462 RVA: 0x00059D44 File Offset: 0x00057F44
			internal override void Apply(StructuralType type, EdmProperty property)
			{
				EdmMeasure edmMeasure = ArgumentValidation.CheckAs<EdmMeasure>(property, "property");
				if (this._kpi != null)
				{
					this._kpi.Apply(type, property);
				}
				if (!string.IsNullOrEmpty(this._formatBy))
				{
					EdmMeasure edmMeasure2 = ArgumentValidation.CheckAs<EdmMeasure>(type.Members[this._formatBy], "formatBy");
					edmMeasure.FormatBy = edmMeasure2;
				}
				if (!string.IsNullOrEmpty(this._applyCulture))
				{
					EdmMeasure edmMeasure3 = ArgumentValidation.CheckAs<EdmMeasure>(type.Members[this._applyCulture], "ApplyCulture");
					edmMeasure.ApplyCulture = edmMeasure3;
				}
			}

			// Token: 0x040013F7 RID: 5111
			private readonly StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState _kpi;

			// Token: 0x040013F8 RID: 5112
			private readonly string _formatBy;

			// Token: 0x040013F9 RID: 5113
			private readonly string _applyCulture;

			// Token: 0x02000456 RID: 1110
			private class MeasureKpiLateBoundState : StructuralType.PropertyLateBoundState
			{
				// Token: 0x06002269 RID: 8809 RVA: 0x0005BB7F File Offset: 0x00059D7F
				private MeasureKpiLateBoundState(string kpiStatusGraphic, string kpiTrendGraphic, string kpiGoal, string kpiStatus, string kpiTrend, string description)
				{
					this._kpiGoal = kpiGoal;
					this._kpiStatus = kpiStatus;
					this._kpiTrend = kpiTrend;
					this._kpiStatusGraphic = kpiStatusGraphic;
					this._kpiTrendGraphic = kpiTrendGraphic;
					this._description = description;
				}

				// Token: 0x0600226A RID: 8810 RVA: 0x0005BBB4 File Offset: 0x00059DB4
				internal static StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState Create(XElement measureElem)
				{
					XElement elementOrNull = measureElem.GetElementOrNull(Extensions.KpiElem);
					if (elementOrNull != null)
					{
						string text = null;
						string text2 = null;
						string nestedPropertyRef = StructuralType.PropertyLateBoundState.GetNestedPropertyRef(elementOrNull.GetElementOrNull(Extensions.KpiGoalElem));
						string nestedPropertyRef2 = StructuralType.PropertyLateBoundState.GetNestedPropertyRef(elementOrNull.GetElementOrNull(Extensions.KpiStatusElem));
						string nestedPropertyRef3 = StructuralType.PropertyLateBoundState.GetNestedPropertyRef(elementOrNull.GetElementOrNull(Extensions.KpiTrendElem));
						string text3 = null;
						XElement elementOrNull2 = elementOrNull.GetElementOrNull(Extensions.Documentation);
						if (elementOrNull2 != null)
						{
							XElement elementOrNull3 = elementOrNull2.GetElementOrNull(Extensions.Summary);
							text3 = ((elementOrNull3 != null) ? elementOrNull3.Value : null);
						}
						if (!string.IsNullOrEmpty(nestedPropertyRef2))
						{
							text = StructuralType.PropertyLateBoundState.GetOptionalAttribute(elementOrNull, Extensions.StatusGraphicAttr);
						}
						if (!string.IsNullOrEmpty(nestedPropertyRef3))
						{
							text2 = StructuralType.PropertyLateBoundState.GetOptionalAttribute(elementOrNull, Extensions.TrendGraphicAttr);
						}
						return new StructuralType.MeasureLateBoundState.MeasureKpiLateBoundState(text, text2, nestedPropertyRef, nestedPropertyRef2, nestedPropertyRef3, text3);
					}
					return null;
				}

				// Token: 0x0600226B RID: 8811 RVA: 0x0005BC78 File Offset: 0x00059E78
				internal override void Apply(StructuralType type, EdmProperty property)
				{
					EdmMeasure edmMeasure = ArgumentValidation.CheckAs<EdmMeasure>(property, "property");
					EdmMeasure edmMeasure2 = null;
					EdmMeasure edmMeasure3 = null;
					EdmMeasure edmMeasure4 = null;
					if (!string.IsNullOrEmpty(this._kpiTrend))
					{
						edmMeasure2 = ArgumentValidation.CheckAsOrNull<EdmMeasure>(type.Members[this._kpiTrend], "trend");
					}
					if (!string.IsNullOrEmpty(this._kpiGoal))
					{
						edmMeasure3 = ArgumentValidation.CheckAsOrNull<EdmMeasure>(type.Members[this._kpiGoal], "goal");
					}
					if (!string.IsNullOrEmpty(this._kpiStatus))
					{
						edmMeasure4 = ArgumentValidation.CheckAsOrNull<EdmMeasure>(type.Members[this._kpiStatus], "status");
					}
					Kpi kpi = new Kpi(this._kpiStatusGraphic, this._kpiTrendGraphic, edmMeasure, edmMeasure3, edmMeasure4, edmMeasure2, property.Description);
					edmMeasure.Kpi = kpi;
					if (edmMeasure3 != null)
					{
						edmMeasure3.Kpi = kpi;
					}
					if (edmMeasure2 != null)
					{
						edmMeasure2.Kpi = kpi;
					}
					if (edmMeasure4 != null)
					{
						edmMeasure4.Kpi = kpi;
					}
				}

				// Token: 0x040014FC RID: 5372
				private readonly string _kpiStatusGraphic;

				// Token: 0x040014FD RID: 5373
				private readonly string _kpiTrendGraphic;

				// Token: 0x040014FE RID: 5374
				private readonly string _kpiGoal;

				// Token: 0x040014FF RID: 5375
				private readonly string _kpiStatus;

				// Token: 0x04001500 RID: 5376
				private readonly string _kpiTrend;

				// Token: 0x04001501 RID: 5377
				private readonly string _description;
			}
		}
	}
}
