using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Annotations;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Utils;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000025 RID: 37
	internal static class EdmExtensions
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006950 File Offset: 0x00004B50
		internal static XElement GetXmlElement(this IEdmEntitySet edmEntitySet, IEdmModel edmModel)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmEntitySet, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntitySet");
			if (annotationValue == null)
			{
				return null;
			}
			return XElement.Parse(annotationValue.Value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000697F File Offset: 0x00004B7F
		internal static string GetFullName(this IEdmEntitySet edmEntitySet)
		{
			return edmEntitySet.Container.Name + "." + edmEntitySet.Name;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000699C File Offset: 0x00004B9C
		internal static bool IsActive(IReadOnlyList<XElement> annotations)
		{
			using (IEnumerator<string> enumerator = EdmExtensions.GetNavigationAttributeValues(annotations, EdmConstants.StateAttr).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!StringUtil.EqualsOrdinalIgnoreCase(enumerator.Current, "Active"))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000069FC File Offset: 0x00004BFC
		internal static ConceptualNavigationBehavior GetNavigationBehavior(IReadOnlyList<XElement> annotations)
		{
			using (IEnumerator<string> enumerator = EdmExtensions.GetNavigationAttributeValues(annotations, EdmConstants.BehaviorAttr).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (StringUtil.EqualsOrdinalIgnoreCase(enumerator.Current, "Weak"))
					{
						return ConceptualNavigationBehavior.Weak;
					}
				}
			}
			return ConceptualNavigationBehavior.Default;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006A5C File Offset: 0x00004C5C
		internal static CrossFilterDirection GetCrossFilterDirection(IReadOnlyList<XElement> annotations)
		{
			using (IEnumerator<string> enumerator = EdmExtensions.GetNavigationAttributeValues(annotations, EdmConstants.CrossFilterDirection).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (StringUtil.EqualsOrdinalIgnoreCase(enumerator.Current, "Both"))
					{
						return CrossFilterDirection.Both;
					}
				}
			}
			return CrossFilterDirection.Single;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006ABC File Offset: 0x00004CBC
		private static IEnumerable<string> GetNavigationAttributeValues(IReadOnlyList<XElement> annotationElements, XName attributeName)
		{
			foreach (XElement xelement in annotationElements)
			{
				string text = (string)xelement.Attribute(attributeName);
				if (!string.IsNullOrEmpty(text))
				{
					yield return text;
				}
			}
			IEnumerator<XElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006AD4 File Offset: 0x00004CD4
		internal static EdmExtensions.EntityTypeExtensionObjects GetEntityTypeExtensionObjects(this XElement entityTypeElement, string edmName, ITracer tracer)
		{
			if (entityTypeElement == null)
			{
				return EdmExtensions.EntityTypeExtensionObjects.Empty;
			}
			IEnumerable<XAttribute> enumerable = entityTypeElement.Elements(EdmConstants.DefaultFieldSet).Elements(EdmConstants.MemberRef).Attributes(EdmConstants.NameAttr);
			Func<XAttribute, string> func;
			if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			IEnumerable<string> enumerable2 = enumerable.Select(func);
			IEnumerable<XAttribute> enumerable3 = entityTypeElement.Elements(EdmConstants.DefaultLabel).Elements(EdmConstants.MemberRef).Attributes(EdmConstants.NameAttr);
			Func<XAttribute, string> func2;
			if ((func2 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func2 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text = enumerable3.Select(func2).FirstOrDefault<string>();
			IEnumerable<XAttribute> enumerable4 = entityTypeElement.Elements(EdmConstants.DefaultImage).Elements(EdmConstants.MemberRef).Attributes(EdmConstants.NameAttr);
			Func<XAttribute, string> func3;
			if ((func3 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func3 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text2 = enumerable4.Select(func3).FirstOrDefault<string>();
			XAttribute xattribute = entityTypeElement.Elements(EdmConstants.Statistics).Attributes(EdmConstants.RowCountAttr).FirstOrDefault<XAttribute>();
			int? num = null;
			if (xattribute != null)
			{
				try
				{
					num = new int?(XmlConvert.ToInt32(xattribute.Value));
				}
				catch (FormatException ex)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing row count for entity set {1}", ex.GetType().Name, edmName.MarkAsModelInfo()));
				}
				catch (OverflowException ex2)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing row count for entity set {1}", ex2.GetType().Name, edmName.MarkAsModelInfo()));
				}
			}
			XAttribute xattribute2 = entityTypeElement.Attribute(EdmConstants.ContentsAttr);
			bool flag = xattribute2 != null && xattribute2.Value == "Time";
			return new EdmExtensions.EntityTypeExtensionObjects(text, text2, enumerable2, num, flag);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C8C File Offset: 0x00004E8C
		internal static string GetReferenceName(this IEdmNavigationProperty edmNavigationProperty, IEdmModel edmModel)
		{
			return EdmExtensions.GetReferenceNameCore(edmModel, edmNavigationProperty, "NavigationProperty");
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006C9A File Offset: 0x00004E9A
		internal static bool IsMeasure(this IEdmStructuralProperty edmProperty, IEdmModel edmModel)
		{
			return ExtensionMethods.GetAnnotationValue(edmModel, edmProperty, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "Measure") != null;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006CB0 File Offset: 0x00004EB0
		internal static string GetPropertyRefNameForElement(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			IEnumerable<XAttribute> enumerable = element.Element(EdmConstants.PropertyRef).Attributes(EdmConstants.NameAttr);
			Func<XAttribute, string> func;
			if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			return enumerable.Select(func).FirstOrDefault<string>();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006CFC File Offset: 0x00004EFC
		internal static ParsedEdmStructuralProperty Parse(this IEdmStructuralProperty edmProperty, IEdmModel edmModel, ITracer tracer, bool skipYearColumnDetectionHeuristics)
		{
			EdmDirectValueAnnotationBinding[] array = new EdmDirectValueAnnotationBinding[]
			{
				new EdmDirectValueAnnotationBinding(edmProperty, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "Property"),
				new EdmDirectValueAnnotationBinding(edmProperty, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "Measure")
			};
			object[] annotationValues = ExtensionMethods.GetAnnotationValues(edmModel, array);
			EdmStringConstant edmStringConstant = annotationValues[0] as EdmStringConstant;
			EdmStringConstant edmStringConstant2 = annotationValues[1] as EdmStringConstant;
			return ParsedEdmStructuralProperty.Parse(tracer, edmProperty, (edmStringConstant != null) ? edmStringConstant.Value : null, (edmStringConstant2 != null) ? edmStringConstant2.Value : null, skipYearColumnDetectionHeuristics);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006D70 File Offset: 0x00004F70
		internal static bool IsRowNumber(this IEdmStructuralProperty edmProperty, IEdmModel edmModel)
		{
			XAttribute extensionElementAttribute = edmProperty.GetExtensionElementAttribute(edmModel, EdmConstants.StabilityAttr);
			return extensionElementAttribute != null && extensionElementAttribute.Value == "RowNumber";
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public static string GetFullName(this IEdmStructuralProperty property)
		{
			IEdmSchemaElement edmSchemaElement = property.DeclaringType as IEdmSchemaElement;
			if (edmSchemaElement != null)
			{
				return StringUtil.FormatInvariant("{0}.{1}", ExtensionMethods.FullName(edmSchemaElement), property.Name);
			}
			return property.Name;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006DDC File Offset: 0x00004FDC
		internal static DataType GetDataType(this IEdmStructuralProperty property, PropertyDataCategory dataCategory)
		{
			if (dataCategory.IsYear())
			{
				return DataType.Year;
			}
			if (dataCategory.IsDecade())
			{
				return DataType.Decade;
			}
			IEdmTypeReference type = property.Type;
			if (EdmTypeSemantics.IsIntegral(type))
			{
				return DataType.Integer;
			}
			if (EdmTypeSemantics.IsFloating(type) || EdmTypeSemantics.IsDecimal(type))
			{
				return DataType.Number;
			}
			if (EdmTypeSemantics.IsString(type))
			{
				return DataType.Text;
			}
			if (EdmTypeSemantics.IsDateTime(type))
			{
				return DataType.DateTime;
			}
			if (EdmTypeSemantics.IsBoolean(type))
			{
				return DataType.Boolean;
			}
			return DataType.None;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006E50 File Offset: 0x00005050
		internal static ConceptualPrimitiveType GetConceptualDataType(this IEdmStructuralProperty edmProperty)
		{
			IEdmTypeReference type = edmProperty.Type;
			if (EdmTypeSemantics.IsString(type))
			{
				return ConceptualPrimitiveType.Text;
			}
			if (EdmTypeSemantics.IsDecimal(type))
			{
				return ConceptualPrimitiveType.Decimal;
			}
			if (EdmTypeSemantics.IsDouble(type))
			{
				return ConceptualPrimitiveType.Double;
			}
			if (EdmTypeSemantics.IsInt64(type))
			{
				return ConceptualPrimitiveType.Integer;
			}
			if (EdmTypeSemantics.IsBoolean(type))
			{
				return ConceptualPrimitiveType.Boolean;
			}
			if (EdmTypeSemantics.IsDateTime(type))
			{
				return ConceptualPrimitiveType.DateTime;
			}
			if (EdmTypeSemantics.IsDateTimeOffset(type))
			{
				return ConceptualPrimitiveType.DateTimeZone;
			}
			if (EdmTypeSemantics.IsTime(type))
			{
				return ConceptualPrimitiveType.Time;
			}
			if (EdmTypeSemantics.IsBinary(type))
			{
				return ConceptualPrimitiveType.Binary;
			}
			return ConceptualPrimitiveType.Null;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006EC4 File Offset: 0x000050C4
		internal static PrimitiveValue GetDefaultValue(this IDictionary<string, XElement> elems, string edmName, IEdmTypeReference edmType, ITracer tracer)
		{
			XElement xelement;
			if (!elems.TryGetValue(EdmConstants.DefaultValue.LocalName, out xelement))
			{
				return null;
			}
			if (xelement.GetBooleanAttributeValueOrDefault(EdmConstants.NilAttr, tracer, false))
			{
				return PrimitiveValue.Null;
			}
			PrimitiveValue primitiveValue;
			Contract.Check(EdmExtensions.TryParseDefaultValueForProperty(xelement.Value, edmType, tracer, edmName, out primitiveValue), "Invalid DefaultValue for property.");
			return primitiveValue;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006F18 File Offset: 0x00005118
		internal static AggregateBehavior GetAggregateBehavior(this IDictionary<string, XAttribute> attrs, string edmName, ITracer tracer)
		{
			XAttribute xattribute;
			bool flag = attrs.TryGetValue(EdmConstants.AggregateBehavior.LocalName, out xattribute);
			bool flag2;
			try
			{
				flag2 = !flag || XmlConvert.ToBoolean(xattribute.Value);
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing boolean attribute value {1} for attribute {2}", ex.GetType().Name, xattribute, edmName.ToString().MarkAsModelInfo()));
				flag2 = true;
			}
			if (!flag2)
			{
				return AggregateBehavior.DiscourageAcrossGroups;
			}
			return AggregateBehavior.Default;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FAC File Offset: 0x000051AC
		internal static string GetStringAttributeValueOrDefault(this XElement element, XName name, string defaultValue = null)
		{
			if (element == null)
			{
				return null;
			}
			XAttribute xattribute = element.Attribute(name);
			if (xattribute == null)
			{
				return defaultValue;
			}
			return xattribute.Value;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006FD4 File Offset: 0x000051D4
		internal static bool GetBooleanAttributeValueOrDefault(this XElement element, XName name, ITracer tracer, bool defaultValue = false)
		{
			string stringAttributeValueOrDefault = element.GetStringAttributeValueOrDefault(name, null);
			if (stringAttributeValueOrDefault != null)
			{
				try
				{
					return XmlConvert.ToBoolean(stringAttributeValueOrDefault);
				}
				catch (FormatException ex)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing boolean attribute value {1} for attribute {2}", ex.GetType().Name, stringAttributeValueOrDefault, name.ToString().MarkAsModelInfo()));
				}
				return defaultValue;
			}
			return defaultValue;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007034 File Offset: 0x00005234
		internal static string GetValueForAttribute(XAttribute attribute)
		{
			return attribute.Value;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000703C File Offset: 0x0000523C
		internal static IEnumerable<EdmExtensions.EdmHierarchy> GetHierarchies(this XElement entityTypeElement, ITracer tracer)
		{
			if (entityTypeElement == null)
			{
				return Util.EmptyReadOnlyCollection<EdmExtensions.EdmHierarchy>();
			}
			List<XElement> list = entityTypeElement.Elements(EdmConstants.Hierarchy).ToList<XElement>();
			List<EdmExtensions.EdmHierarchy> list2 = new List<EdmExtensions.EdmHierarchy>(list.Count);
			foreach (XElement xelement in list)
			{
				IEnumerable<XAttribute> enumerable = xelement.Attributes(EdmConstants.NameAttr);
				Func<XAttribute, string> func;
				if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				string text = enumerable.Select(func).FirstOrDefault<string>();
				IEnumerable<XAttribute> enumerable2 = xelement.Attributes(EdmConstants.ReferenceNameAttr);
				Func<XAttribute, string> func2;
				if ((func2 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func2 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				string text2 = enumerable2.Select(func2).FirstOrDefault<string>();
				text2 = text2 ?? text;
				IEnumerable<XAttribute> enumerable3 = xelement.Attributes(EdmConstants.CaptionAttr);
				Func<XAttribute, string> func3;
				if ((func3 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func3 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				string text3 = enumerable3.Select(func3).FirstOrDefault<string>();
				XElement xelement2 = xelement.Elements(EdmConstants.Documentation).FirstOrDefault<XElement>();
				string text4;
				if (xelement2 == null)
				{
					text4 = null;
				}
				else
				{
					XElement xelement3 = xelement2.Elements(EdmConstants.Summary).FirstOrDefault<XElement>();
					text4 = ((xelement3 != null) ? xelement3.Value : null);
				}
				string text5 = text4;
				bool booleanAttributeValueOrDefault = xelement.GetBooleanAttributeValueOrDefault(EdmConstants.HiddenAttr, tracer, false);
				List<XElement> list3 = xelement.Elements(EdmConstants.Level).ToList<XElement>();
				List<EdmExtensions.EdmLevel> list4 = new List<EdmExtensions.EdmLevel>(list3.Count);
				foreach (XElement xelement4 in list3)
				{
					IEnumerable<XAttribute> enumerable4 = xelement4.Attributes(EdmConstants.NameAttr);
					Func<XAttribute, string> func4;
					if ((func4 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
					{
						func4 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
					}
					string text6 = enumerable4.Select(func4).FirstOrDefault<string>();
					IEnumerable<XAttribute> enumerable5 = xelement4.Attributes(EdmConstants.ReferenceNameAttr);
					Func<XAttribute, string> func5;
					if ((func5 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
					{
						func5 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
					}
					string text7 = enumerable5.Select(func5).FirstOrDefault<string>();
					text7 = text7 ?? text6;
					IEnumerable<XAttribute> enumerable6 = xelement4.Attributes(EdmConstants.CaptionAttr);
					Func<XAttribute, string> func6;
					if ((func6 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
					{
						func6 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
					}
					string text8 = enumerable6.Select(func6).FirstOrDefault<string>();
					IEnumerable<XAttribute> enumerable7 = xelement4.Element(EdmConstants.Source).Element(EdmConstants.PropertyRef).Attributes(EdmConstants.NameAttr);
					Func<XAttribute, string> func7;
					if ((func7 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
					{
						func7 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
					}
					string text9 = enumerable7.Select(func7).FirstOrDefault<string>();
					List<EdmExtensions.EdmLevel> list5 = list4;
					EdmExtensions.EdmLevel edmLevel = new EdmExtensions.EdmLevel();
					edmLevel.EdmName = text6;
					edmLevel.ReferenceName = text7;
					edmLevel.DisplayName = text8 ?? text7;
					edmLevel.Description = null;
					edmLevel.SourceRef = text9;
					edmLevel.StableName = xelement4.GetStringAttributeValueOrDefault(EdmConstants.StableNameAttr, null);
					XElement xelement5 = xelement4.Element(EdmConstants.Cultures);
					edmLevel.Translations = ((xelement5 != null) ? xelement5.GetTranslations() : null);
					list5.Add(edmLevel);
				}
				List<EdmExtensions.EdmHierarchy> list6 = list2;
				EdmExtensions.EdmHierarchy edmHierarchy = new EdmExtensions.EdmHierarchy();
				edmHierarchy.EdmName = text;
				edmHierarchy.ReferenceName = text2;
				edmHierarchy.DisplayName = text3 ?? text2;
				edmHierarchy.Description = text5;
				edmHierarchy.IsHidden = booleanAttributeValueOrDefault;
				edmHierarchy.Levels = list4;
				edmHierarchy.StableName = xelement.GetStringAttributeValueOrDefault(EdmConstants.StableNameAttr, null);
				XElement xelement6 = xelement.Element(EdmConstants.Cultures);
				edmHierarchy.Translations = ((xelement6 != null) ? xelement6.GetTranslations() : null);
				list6.Add(edmHierarchy);
			}
			return list2;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000073C4 File Offset: 0x000055C4
		internal static IEnumerable<EdmExtensions.EdmDisplayFolder> GetDisplayFolders(this XElement entityTypeElement)
		{
			if (entityTypeElement == null)
			{
				return Util.EmptyReadOnlyCollection<EdmExtensions.EdmDisplayFolder>();
			}
			List<XElement> list = entityTypeElement.Elements(EdmConstants.DisplayFolders).Elements(EdmConstants.DisplayFolder).ToList<XElement>();
			if (list == null)
			{
				return Util.EmptyReadOnlyCollection<EdmExtensions.EdmDisplayFolder>();
			}
			List<EdmExtensions.EdmDisplayFolder> list2 = new List<EdmExtensions.EdmDisplayFolder>(list.Count);
			foreach (XElement xelement in list)
			{
				list2.Add(EdmExtensions.GetDisplayFolder(xelement));
			}
			return list2;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007454 File Offset: 0x00005654
		internal static List<ConceptualTranslation> GetTranslations(this XElement culturesElement)
		{
			return culturesElement.Elements().Select(delegate(XElement element)
			{
				IEnumerable<XAttribute> enumerable = element.Attributes(EdmConstants.NameAttr);
				Func<XAttribute, string> func;
				if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				string text = enumerable.Select(func).FirstOrDefault<string>();
				IEnumerable<XAttribute> enumerable2 = element.Attributes(EdmConstants.CaptionAttr);
				Func<XAttribute, string> func2;
				if ((func2 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func2 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				string text2 = enumerable2.Select(func2).FirstOrDefault<string>();
				IEnumerable<XAttribute> enumerable3 = element.Attributes(EdmConstants.Description);
				Func<XAttribute, string> func3;
				if ((func3 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func3 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				return new ConceptualTranslation(text, text2, enumerable3.Select(func3).FirstOrDefault<string>());
			}).ToList<ConceptualTranslation>();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007488 File Offset: 0x00005688
		private static EdmExtensions.EdmDisplayFolder GetDisplayFolder(XElement displayFolderElement)
		{
			IEnumerable<XAttribute> enumerable = displayFolderElement.Attributes(EdmConstants.NameAttr);
			Func<XAttribute, string> func;
			if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text = enumerable.Select(func).FirstOrDefault<string>();
			IEnumerable<XAttribute> enumerable2 = displayFolderElement.Attributes(EdmConstants.CaptionAttr);
			Func<XAttribute, string> func2;
			if ((func2 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
			{
				func2 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
			}
			string text2 = enumerable2.Select(func2).FirstOrDefault<string>();
			List<XElement> list = displayFolderElement.Elements().ToList<XElement>();
			List<EdmExtensions.EdmDisplayItem> list2 = new List<EdmExtensions.EdmDisplayItem>(list.Count);
			List<ConceptualTranslation> list3 = new List<ConceptualTranslation>();
			foreach (XElement xelement in list)
			{
				if (xelement.Name == EdmConstants.Cultures)
				{
					list3 = xelement.GetTranslations();
				}
				else
				{
					EdmExtensions.EdmDisplayItem displayItem = EdmExtensions.GetDisplayItem(xelement);
					if (displayItem != null)
					{
						list2.Add(displayItem);
					}
				}
			}
			return new EdmExtensions.EdmDisplayFolder
			{
				EdmName = text,
				DisplayName = text2,
				DisplayItems = list2,
				Translations = list3
			};
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000075A4 File Offset: 0x000057A4
		private static EdmExtensions.EdmDisplayItem GetDisplayItem(XElement displayItemElement)
		{
			XName name = displayItemElement.Name;
			if (name == EdmConstants.PropertyRef || name == EdmConstants.KpiRef)
			{
				EdmExtensions.EdmDisplayItem edmDisplayItem = new EdmExtensions.EdmDisplayItem();
				IEnumerable<XAttribute> enumerable = displayItemElement.Attributes(EdmConstants.NameAttr);
				Func<XAttribute, string> func;
				if ((func = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				edmDisplayItem.PropertyRef = enumerable.Select(func).FirstOrDefault<string>();
				return edmDisplayItem;
			}
			if (name == EdmConstants.HierarchyRef)
			{
				EdmExtensions.EdmDisplayItem edmDisplayItem2 = new EdmExtensions.EdmDisplayItem();
				IEnumerable<XAttribute> enumerable2 = displayItemElement.Attributes(EdmConstants.NameAttr);
				Func<XAttribute, string> func2;
				if ((func2 = EdmExtensions.<>O.<0>__GetValueForAttribute) == null)
				{
					func2 = (EdmExtensions.<>O.<0>__GetValueForAttribute = new Func<XAttribute, string>(EdmExtensions.GetValueForAttribute));
				}
				edmDisplayItem2.HierarchyRef = enumerable2.Select(func2).FirstOrDefault<string>();
				return edmDisplayItem2;
			}
			if (name == EdmConstants.DisplayFolder)
			{
				return new EdmExtensions.EdmDisplayItem
				{
					DisplayFolder = EdmExtensions.GetDisplayFolder(displayItemElement)
				};
			}
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007678 File Offset: 0x00005878
		internal static IReadOnlyList<EdmVariation> GetVariations(this IDictionary<string, XElement> elems, ITracer tracer)
		{
			XElement xelement;
			if (!elems.TryGetValue(EdmConstants.Variations.LocalName, out xelement))
			{
				return null;
			}
			IEnumerable<XElement> enumerable = xelement.Elements(EdmConstants.Variation);
			List<EdmVariation> list = new List<EdmVariation>();
			foreach (XElement xelement2 in enumerable)
			{
				string stringAttributeValueOrDefault = xelement2.GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
				string stringAttributeValueOrDefault2 = xelement2.GetStringAttributeValueOrDefault(EdmConstants.ReferenceNameAttr, null);
				bool booleanAttributeValueOrDefault = xelement2.GetBooleanAttributeValueOrDefault(EdmConstants.DefaultAttr, tracer, false);
				string stringAttributeValueOrDefault3 = xelement2.Element(EdmConstants.NavigationPropertyRef).GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
				string stringAttributeValueOrDefault4 = xelement2.Element(EdmConstants.DefaultHierarchyRef).GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
				string stringAttributeValueOrDefault5 = xelement2.Element(EdmConstants.DefaultPropertyRef).GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
				list.Add(new EdmVariation(stringAttributeValueOrDefault, stringAttributeValueOrDefault2, stringAttributeValueOrDefault3, stringAttributeValueOrDefault4, stringAttributeValueOrDefault5, booleanAttributeValueOrDefault));
			}
			return list;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000776C File Offset: 0x0000596C
		internal static IReadOnlyList<ExtendedProperty> GetExtendedPropertyList(XElement element)
		{
			List<XElement> list = (from a in element.Elements()
				where a.Name.LocalName.Equals(EdmConstants.ExtendedProperty.LocalName)
				select a).ToList<XElement>();
			if (list.Count == 0)
			{
				return Util.EmptyReadOnlyCollection<ExtendedProperty>();
			}
			return list.Select((XElement item) => EdmExtensions.GetExtendedProperty(item)).WhereNonNull<ExtendedProperty>().AsReadOnlyList<ExtendedProperty>();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000077E8 File Offset: 0x000059E8
		internal static ExtendedProperty GetExtendedProperty(XElement element)
		{
			string stringAttributeValueOrDefault = element.GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
			string value = element.Value;
			ExtendedPropertyType extendedPropertyType = ExtendedPropertyType.String;
			string stringAttributeValueOrDefault2 = element.GetStringAttributeValueOrDefault(EdmConstants.TypeAttr, null);
			if (!string.IsNullOrWhiteSpace(stringAttributeValueOrDefault2))
			{
				extendedPropertyType = (ExtendedPropertyType)Enum.Parse(typeof(ExtendedPropertyType), stringAttributeValueOrDefault2);
			}
			return new ExtendedProperty(stringAttributeValueOrDefault, value, extendedPropertyType);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000783C File Offset: 0x00005A3C
		internal static Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> GetMappedMParameterList(this IEdmEntityContainer edmEntityContainer, IEdmModel edmModel, ITracer tracer)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmEntityContainer, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityContainer");
			if (annotationValue == null)
			{
				tracer.TraceError(StringUtil.FormatInvariant("EntityContainer element not found.", new object[0]));
				return null;
			}
			XElement xelement = XElement.Parse(annotationValue.Value).Element(EdmConstants.MParameters);
			if (xelement == null)
			{
				return null;
			}
			Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> dictionary = new Dictionary<string, Dictionary<string, List<ConceptualMParameter>>>(EdmNameComparer.Instance);
			foreach (XElement xelement2 in (from e in xelement.Elements()
				where e.Name.LocalName.Equals(EdmConstants.MParameter.LocalName)
				select e).ToList<XElement>())
			{
				string stringAttributeValueOrDefault = xelement2.GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
				if (string.IsNullOrWhiteSpace(stringAttributeValueOrDefault))
				{
					tracer.TraceError(StringUtil.FormatInvariant("Parameter name can't be null or empty.", new object[0]));
				}
				else
				{
					string stringAttributeValueOrDefault2 = xelement2.GetStringAttributeValueOrDefault(EdmConstants.ActualTypeAttr, null);
					XElement xelement3 = xelement2.Element(EdmConstants.ParameterValuesColumn);
					if (xelement3 == null)
					{
						tracer.TraceError(StringUtil.FormatInvariant("{0} element not found for mapped M parameter {1}", EdmConstants.ParameterValuesColumn, stringAttributeValueOrDefault.MarkAsModelInfo()));
					}
					else
					{
						XElement xelement4 = xelement3.Element(EdmConstants.EntityRef);
						XElement xelement5 = xelement3.Element(EdmConstants.PropertyRef);
						if (xelement4 == null || xelement5 == null)
						{
							tracer.TraceError(StringUtil.FormatInvariant("EntityRef and PropertyRef can't be null for mapped M parameter {0}", stringAttributeValueOrDefault.MarkAsModelInfo()));
						}
						else
						{
							string stringAttributeValueOrDefault3 = xelement4.GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
							string stringAttributeValueOrDefault4 = xelement5.GetStringAttributeValueOrDefault(EdmConstants.NameAttr, null);
							if (string.IsNullOrWhiteSpace(stringAttributeValueOrDefault3) || string.IsNullOrWhiteSpace(stringAttributeValueOrDefault4))
							{
								tracer.TraceError(StringUtil.FormatInvariant("EntityEdm name and propertyEdm name can't be null or empty for parameter {0}", stringAttributeValueOrDefault.MarkAsModelInfo()));
							}
							else
							{
								Dictionary<string, List<ConceptualMParameter>> dictionary2;
								if (!dictionary.TryGetValue(stringAttributeValueOrDefault3, out dictionary2))
								{
									dictionary2 = new Dictionary<string, List<ConceptualMParameter>>(EdmNameComparer.Instance);
									dictionary.Add(stringAttributeValueOrDefault3, dictionary2);
								}
								List<ConceptualMParameter> list;
								if (!dictionary2.TryGetValue(stringAttributeValueOrDefault4, out list))
								{
									list = new List<ConceptualMParameter>();
									dictionary2.Add(stringAttributeValueOrDefault4, list);
								}
								list.Add(new ConceptualMParameter(stringAttributeValueOrDefault, stringAttributeValueOrDefault2));
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007A68 File Offset: 0x00005C68
		internal static bool TryGetPropertyStatistics(this IDictionary<string, XElement> elems, IEdmTypeReference edmType, string edmName, ITracer tracer, PropertyDataCategory propertyDataCategory, out ConceptualColumnStatistics stats)
		{
			stats = null;
			XElement xelement;
			if (!elems.TryGetValue(EdmConstants.Statistics.LocalName, out xelement))
			{
				return false;
			}
			XAttribute xattribute = xelement.Attributes(EdmConstants.DistinctValueCountAttr).FirstOrDefault<XAttribute>();
			if (xattribute == null)
			{
				return false;
			}
			int num;
			try
			{
				num = XmlConvert.ToInt32(xattribute.Value);
			}
			catch (FormatException ex)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing distinct value count for property {1}. Distinct value count: {2}", ex.GetType().Name, edmName.MarkAsModelInfo(), xattribute.Value));
				return false;
			}
			catch (OverflowException ex2)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing distinct value count for property {1}. Distinct value count: {2}", ex2.GetType().Name, edmName.MarkAsModelInfo(), xattribute.Value));
				return false;
			}
			PrimitiveValue primitiveValue;
			PrimitiveValue primitiveValue2;
			if (!xelement.TryParseMinAndMaxValueForProperty(edmType, edmName, out primitiveValue, out primitiveValue2, tracer))
			{
				return false;
			}
			if (!EdmTypeSemantics.IsString(edmType))
			{
				stats = new ConceptualColumnStatistics(num, primitiveValue, primitiveValue2);
				return true;
			}
			XAttribute xattribute2 = xelement.Attributes(EdmConstants.StringValueMaxLengthAttr).FirstOrDefault<XAttribute>();
			if (xattribute2 != null)
			{
				try
				{
					int num2 = XmlConvert.ToInt32(xattribute2.Value);
					stats = new ConceptualStringColumnStatistics(num, primitiveValue, primitiveValue2, num2);
					return true;
				}
				catch (FormatException ex3)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing max value length for property {1}. Max value length: {2}", ex3.GetType().Name, edmName.MarkAsModelInfo(), xattribute2.Value));
				}
				catch (OverflowException ex4)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing max value length for property {1}. Max value length: {2}", ex4.GetType().Name, edmName.MarkAsModelInfo(), xattribute2.Value));
				}
			}
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007C08 File Offset: 0x00005E08
		internal static bool IsRecognizedAsYearColumn(this IEdmStructuralProperty edmProperty, bool isMeasure, ConceptualColumnStatistics statistics)
		{
			long num;
			long num2;
			return EdmTypeSemantics.IsIntegral(edmProperty.Type) && !isMeasure && EdmConstants.YearColumnApprovedList.Any((string e) => edmProperty.Name.IndexOf(e, StringComparison.OrdinalIgnoreCase) >= 0) && !EdmConstants.YearColumnExcludeList.Any((string e) => edmProperty.Name.IndexOf(e, StringComparison.OrdinalIgnoreCase) >= 0) && (statistics == null || statistics.MinValue == null || (statistics.MinValue.TryGetValue(out num) && statistics.MaxValue != null && statistics.MaxValue.TryGetValue(out num2) && YearRecognizer.IsLikelyYearValue(num, CultureInfo.InvariantCulture) && YearRecognizer.IsLikelyYearValue(num2, CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007CC4 File Offset: 0x00005EC4
		internal static XElement GetEntityTypeXmlElement(this IEdmEntityType entityType, IEdmModel edmModel)
		{
			IEdmStringValue edmStringValue = ExtensionMethods.GetAnnotationValue(edmModel, entityType, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityType") as IEdmStringValue;
			if (edmStringValue == null)
			{
				return null;
			}
			return XElement.Parse(edmStringValue.Value);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007CF8 File Offset: 0x00005EF8
		internal static bool TryGetLanguageIdentifier(this IEdmElement edmElement, IEdmModel edmModel, out LanguageIdentifier language)
		{
			XAttribute extensionElementAttribute = EdmExtensions.GetExtensionElementAttribute(edmModel, edmElement, "EntityContainer", EdmConstants.Culture);
			if (extensionElementAttribute == null)
			{
				language = (LanguageIdentifier)0;
				return false;
			}
			return LanguageIdentifierUtil.TryAsLanguageIdentifier(extensionElementAttribute.Value, out language);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007D2C File Offset: 0x00005F2C
		internal static ConceptualCapabilities GetCapabilities(this IEdmEntityContainer edmEntityContainer, IEdmModel edmModel, ConceptualSchemaBuilderOptions options)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmEntityContainer, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityContainer");
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			bool flag16 = false;
			bool flag17 = false;
			bool flag18 = false;
			bool flag19 = false;
			bool flag20 = false;
			bool flag21 = false;
			List<string> list = null;
			if (annotationValue != null)
			{
				XElement xelement = XElement.Parse(annotationValue.Value).Element(EdmConstants.ModelCapabilities);
				if (xelement != null)
				{
					XElement xelement2 = xelement.Element(EdmConstants.GroupByValidation);
					if (xelement2 != null)
					{
						xelement2.Value == "Enforced";
					}
					XElement xelement3 = xelement.Element(EdmConstants.QueryAggregateUsage);
					if (xelement3 != null)
					{
						flag = xelement3.Value == "Discourage";
					}
					XElement xelement4 = xelement.Element(EdmConstants.DiscourageCompositeModels);
					if (xelement4 != null)
					{
						flag2 = xelement4.Value == "1";
					}
					XElement xelement5 = xelement.Element(EdmConstants.FiveStateKpiRange);
					if (xelement5 != null)
					{
						flag5 = xelement5.Value == "1";
					}
					XElement xelement6 = xelement.Element(EdmConstants.MultiColumnFiltering);
					if (xelement6 != null)
					{
						flag11 = xelement6.Value == "LimitedToGroupByColumns";
					}
					XElement xelement7 = xelement.Element(EdmConstants.VisualCalculations);
					if (xelement7 != null)
					{
						flag21 = xelement7.Value == "1";
					}
					flag13 = EdmExtensions.HasValue(xelement, EdmConstants.DataSourceVariables, "1");
					bool flag22 = flag5;
					flag18 = EdmExtensions.HasValue(xelement, EdmConstants.VirtualColumns, "1");
					XElement xelement8 = xelement.Element(EdmConstants.DaxFunctions);
					if (xelement8 != null)
					{
						XElement xelement9 = xelement8.Element(EdmConstants.BinaryMinMax);
						if (xelement9 != null && xelement9.Value == "1")
						{
							flag3 = true;
							flag4 = true;
						}
						XElement xelement10 = xelement8.Element(EdmConstants.SummarizeColumns);
						if (xelement10 != null && xelement10.Value == "1")
						{
							XElement xelement11 = xelement8.Element(EdmConstants.SampleAxisWithLocalMinMax);
							if (xelement11 != null)
							{
								flag9 = xelement11.Value == "1";
							}
							XElement xelement12 = xelement8.Element(EdmConstants.SampleCartesianPointsByCover);
							if (xelement12 != null)
							{
								flag10 = xelement12.Value == "1";
							}
							flag12 = true;
							flag14 = true;
							flag19 = true;
							flag15 = true;
							flag17 = true;
							bool flag23 = false;
							XElement xelement13 = xelement8.Element(EdmConstants.FormatByLocale);
							if (xelement13 != null)
							{
								flag23 = xelement13.Value == "1";
							}
							flag20 = options.SparklineDataEnabled && flag23 && !flag22;
							flag6 = !flag22;
							XElement xelement14 = xelement8.Element(EdmConstants.TreatAs);
							if (xelement14 != null)
							{
								flag8 = xelement14.Value == "1" && !flag11;
							}
						}
						XElement xelement15 = xelement8.Element(EdmConstants.StringMinMax);
						if (xelement15 != null)
						{
							flag7 = xelement15.Value == "1";
						}
						XElement xelement16 = xelement8.Element(EdmConstants.TopNPerLevel);
						if (xelement16 != null)
						{
							flag16 = xelement16.Value == "1";
						}
						list = CsdlParserUtil.ParseDaxExtensionFunctionsNode(xelement8.Element(EdmConstants.DaxExtensionFunctions), EdmConstants.DaxExtensionFunction);
					}
				}
			}
			return new ConceptualCapabilities(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, flag21, new TransformCapabilities(list));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008080 File Offset: 0x00006280
		internal static ModelDaxCapabilities GetDaxCapabilities(this IEdmEntityContainer edmEntityContainer, IEdmModel edmModel)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmEntityContainer, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityContainer");
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (annotationValue != null)
			{
				XElement xelement = XElement.Parse(annotationValue.Value).Element(EdmConstants.ModelCapabilities);
				if (xelement != null)
				{
					flag = xelement.Element(EdmConstants.Variables) != null && !EdmExtensions.HasValue(xelement, EdmConstants.Variables, "0");
					flag2 = EdmExtensions.HasValue(xelement, EdmConstants.InOperator, "1");
					flag3 = EdmExtensions.HasValue(xelement, EdmConstants.TableConstructor, "1");
					flag4 = EdmExtensions.HasValue(xelement, EdmConstants.VirtualColumns, "1");
				}
			}
			return new ModelDaxCapabilities(flag, flag2, flag3, flag4);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008130 File Offset: 0x00006330
		private static bool HasValue(XElement containingElement, XName elementName, string expectedValue)
		{
			XElement xelement = containingElement.Element(elementName);
			return xelement != null && xelement.Value == expectedValue;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008156 File Offset: 0x00006356
		private static XAttribute GetExtensionElementAttribute(this IEdmStructuralProperty edmProperty, IEdmModel edmModel, XName extensionAttributeName)
		{
			return EdmExtensions.GetExtensionElementAttribute(edmModel, edmProperty, EdmExtensions.GetExtensionNameFromProperty(edmModel, edmProperty), extensionAttributeName);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008168 File Offset: 0x00006368
		private static XAttribute GetExtensionElementAttribute(IEdmModel edmModel, IEdmElement edmElement, string extensionElementName, XName extensionAttributeName)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(edmModel, edmElement, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", extensionElementName);
			if (annotationValue != null)
			{
				return XElement.Parse(annotationValue.Value).Attribute(extensionAttributeName);
			}
			return null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008199 File Offset: 0x00006399
		private static string GetExtensionNameFromProperty(IEdmModel edmModel, IEdmStructuralProperty edmProperty)
		{
			if (!edmProperty.IsMeasure(edmModel))
			{
				return "Property";
			}
			return "Measure";
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000081B0 File Offset: 0x000063B0
		private static string GetReferenceNameCore(IEdmModel edmModel, IEdmNamedElement edmElement, string extensionName)
		{
			XAttribute extensionElementAttribute = EdmExtensions.GetExtensionElementAttribute(edmModel, edmElement, extensionName, EdmConstants.ReferenceNameAttr);
			if (extensionElementAttribute == null)
			{
				return edmElement.Name;
			}
			return extensionElementAttribute.Value;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000081DC File Offset: 0x000063DC
		private static string GetDisplayNameCore(IEdmModel edmModel, IEdmNamedElement edmElement, string referenceName, string extensionName)
		{
			XAttribute extensionElementAttribute = EdmExtensions.GetExtensionElementAttribute(edmModel, edmElement, extensionName, EdmConstants.CaptionAttr);
			if (extensionElementAttribute != null)
			{
				return extensionElementAttribute.Value;
			}
			return referenceName;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008204 File Offset: 0x00006404
		private static bool TryParseMinAndMaxValueForProperty(this XElement statisticsElement, IEdmTypeReference edmType, string edmName, out PrimitiveValue minValue, out PrimitiveValue maxValue, ITracer tracer)
		{
			XElement xelement = statisticsElement.Element(EdmConstants.MinValue);
			PrimitiveValue primitiveValue;
			maxValue = (primitiveValue = null);
			minValue = primitiveValue;
			if (xelement == null)
			{
				return true;
			}
			XElement xelement2 = statisticsElement.Element(EdmConstants.MaxValue);
			if (xelement2 == null)
			{
				return false;
			}
			try
			{
				if (EdmTypeSemantics.IsDateTime(edmType))
				{
					minValue = XmlConvert.ToDateTime(xelement.Value, XmlDateTimeSerializationMode.Utc);
					maxValue = XmlConvert.ToDateTime(xelement2.Value, XmlDateTimeSerializationMode.Utc);
					return true;
				}
				if (EdmTypeSemantics.IsString(edmType))
				{
					minValue = xelement.Value;
					maxValue = xelement2.Value;
					return true;
				}
				if (EdmTypeSemantics.IsIntegral(edmType))
				{
					minValue = XmlConvert.ToInt64(xelement.Value);
					maxValue = XmlConvert.ToInt64(xelement2.Value);
					return true;
				}
				if (EdmTypeSemantics.IsBoolean(edmType))
				{
					minValue = (XmlConvert.ToBoolean(xelement.Value) ? BooleanPrimitiveValue.True : BooleanPrimitiveValue.False);
					maxValue = (XmlConvert.ToBoolean(xelement2.Value) ? BooleanPrimitiveValue.True : BooleanPrimitiveValue.False);
					return true;
				}
				if (EdmTypeSemantics.IsFloating(edmType) || EdmTypeSemantics.IsDouble(edmType))
				{
					minValue = XmlConvert.ToDouble(xelement.Value);
					maxValue = XmlConvert.ToDouble(xelement2.Value);
					return true;
				}
				if (EdmTypeSemantics.IsDecimal(edmType))
				{
					minValue = XmlConvert.ToDecimal(xelement.Value);
					maxValue = XmlConvert.ToDecimal(xelement2.Value);
					return true;
				}
			}
			catch (FormatException ex)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing min and max value for property {1}", ex.GetType().Name, edmName.MarkAsModelInfo()));
				return false;
			}
			catch (OverflowException ex2)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing min and max value for property {1}", ex2.GetType().Name, edmName.MarkAsModelInfo()));
				return false;
			}
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008410 File Offset: 0x00006610
		internal static bool TryParseDefaultValueForProperty(string defaultValueString, IEdmTypeReference type, ITracer tracer, string propertyName, out PrimitiveValue value)
		{
			value = null;
			if (defaultValueString == null)
			{
				return true;
			}
			try
			{
				if (EdmTypeSemantics.IsDateTime(type))
				{
					value = XmlConvert.ToDateTime(defaultValueString, XmlDateTimeSerializationMode.Unspecified);
					return true;
				}
				if (EdmTypeSemantics.IsString(type))
				{
					value = defaultValueString;
					return true;
				}
				if (EdmTypeSemantics.IsIntegral(type))
				{
					value = XmlConvert.ToInt64(defaultValueString);
					return true;
				}
				if (EdmTypeSemantics.IsBoolean(type))
				{
					value = XmlConvert.ToBoolean(defaultValueString);
					return true;
				}
				if (EdmTypeSemantics.IsFloating(type) || EdmTypeSemantics.IsDouble(type))
				{
					value = XmlConvert.ToDouble(defaultValueString);
					return true;
				}
				if (EdmTypeSemantics.IsDecimal(type))
				{
					value = XmlConvert.ToDecimal(defaultValueString);
					return true;
				}
			}
			catch (FormatException ex)
			{
				tracer.TraceError("{0} occurred when parsing default value for property {1}", ex.GetType().Name, propertyName.MarkAsModelInfo());
				return false;
			}
			catch (OverflowException ex2)
			{
				tracer.TraceError("{0} occurred when parsing default value for property {1}", ex2.GetType().Name, propertyName.MarkAsModelInfo());
				return false;
			}
			return false;
		}

		// Token: 0x02000043 RID: 67
		[ImmutableObject(true)]
		internal sealed class EntityTypeExtensionObjects
		{
			// Token: 0x060001DE RID: 478 RVA: 0x0000947D File Offset: 0x0000767D
			internal EntityTypeExtensionObjects(string defaultLabel, string defaultImage, IEnumerable<string> defaultFieldSet, int? rowCount, bool isDateTable)
			{
				this.DefaultImage = defaultImage;
				this.DefaultLabel = defaultLabel;
				this.DefaultFieldSet = defaultFieldSet.AsReadOnlyCollection<string>();
				this.RowCount = rowCount;
				this.IsDateTable = isDateTable;
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060001DF RID: 479 RVA: 0x000094AF File Offset: 0x000076AF
			internal string DefaultLabel { get; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060001E0 RID: 480 RVA: 0x000094B7 File Offset: 0x000076B7
			internal string DefaultImage { get; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060001E1 RID: 481 RVA: 0x000094BF File Offset: 0x000076BF
			internal ReadOnlyCollection<string> DefaultFieldSet { get; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x000094C7 File Offset: 0x000076C7
			internal int? RowCount { get; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060001E3 RID: 483 RVA: 0x000094CF File Offset: 0x000076CF
			internal bool IsDateTable { get; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x000094D7 File Offset: 0x000076D7
			internal static EdmExtensions.EntityTypeExtensionObjects Empty
			{
				get
				{
					return EdmExtensions.EntityTypeExtensionObjects._empty;
				}
			}

			// Token: 0x040001BB RID: 443
			private static readonly EdmExtensions.EntityTypeExtensionObjects _empty = new EdmExtensions.EntityTypeExtensionObjects(null, null, Util.EmptyReadOnlyCollection<string>(), null, false);
		}

		// Token: 0x02000044 RID: 68
		internal sealed class EdmLevel
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009508 File Offset: 0x00007708
			// (set) Token: 0x060001E7 RID: 487 RVA: 0x00009510 File Offset: 0x00007710
			internal string EdmName { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009519 File Offset: 0x00007719
			// (set) Token: 0x060001E9 RID: 489 RVA: 0x00009521 File Offset: 0x00007721
			internal string ReferenceName { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060001EA RID: 490 RVA: 0x0000952A File Offset: 0x0000772A
			// (set) Token: 0x060001EB RID: 491 RVA: 0x00009532 File Offset: 0x00007732
			internal string DisplayName { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060001EC RID: 492 RVA: 0x0000953B File Offset: 0x0000773B
			// (set) Token: 0x060001ED RID: 493 RVA: 0x00009543 File Offset: 0x00007743
			internal string Description { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060001EE RID: 494 RVA: 0x0000954C File Offset: 0x0000774C
			// (set) Token: 0x060001EF RID: 495 RVA: 0x00009554 File Offset: 0x00007754
			internal string SourceRef { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000955D File Offset: 0x0000775D
			// (set) Token: 0x060001F1 RID: 497 RVA: 0x00009565 File Offset: 0x00007765
			internal string StableName { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000956E File Offset: 0x0000776E
			// (set) Token: 0x060001F3 RID: 499 RVA: 0x00009576 File Offset: 0x00007776
			internal List<ConceptualTranslation> Translations { get; set; }
		}

		// Token: 0x02000045 RID: 69
		internal sealed class EdmHierarchy
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009587 File Offset: 0x00007787
			// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000958F File Offset: 0x0000778F
			internal string EdmName { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009598 File Offset: 0x00007798
			// (set) Token: 0x060001F8 RID: 504 RVA: 0x000095A0 File Offset: 0x000077A0
			internal string ReferenceName { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x000095A9 File Offset: 0x000077A9
			// (set) Token: 0x060001FA RID: 506 RVA: 0x000095B1 File Offset: 0x000077B1
			internal string DisplayName { get; set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060001FB RID: 507 RVA: 0x000095BA File Offset: 0x000077BA
			// (set) Token: 0x060001FC RID: 508 RVA: 0x000095C2 File Offset: 0x000077C2
			internal bool IsHidden { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060001FD RID: 509 RVA: 0x000095CB File Offset: 0x000077CB
			// (set) Token: 0x060001FE RID: 510 RVA: 0x000095D3 File Offset: 0x000077D3
			internal string Description { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060001FF RID: 511 RVA: 0x000095DC File Offset: 0x000077DC
			// (set) Token: 0x06000200 RID: 512 RVA: 0x000095E4 File Offset: 0x000077E4
			internal List<EdmExtensions.EdmLevel> Levels { get; set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000201 RID: 513 RVA: 0x000095ED File Offset: 0x000077ED
			// (set) Token: 0x06000202 RID: 514 RVA: 0x000095F5 File Offset: 0x000077F5
			internal string StableName { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000203 RID: 515 RVA: 0x000095FE File Offset: 0x000077FE
			// (set) Token: 0x06000204 RID: 516 RVA: 0x00009606 File Offset: 0x00007806
			internal List<ConceptualTranslation> Translations { get; set; }
		}

		// Token: 0x02000046 RID: 70
		internal sealed class EdmDisplayFolder
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000206 RID: 518 RVA: 0x00009617 File Offset: 0x00007817
			// (set) Token: 0x06000207 RID: 519 RVA: 0x0000961F File Offset: 0x0000781F
			internal string EdmName { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000208 RID: 520 RVA: 0x00009628 File Offset: 0x00007828
			// (set) Token: 0x06000209 RID: 521 RVA: 0x00009630 File Offset: 0x00007830
			internal string DisplayName { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00009639 File Offset: 0x00007839
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00009641 File Offset: 0x00007841
			internal List<EdmExtensions.EdmDisplayItem> DisplayItems { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600020C RID: 524 RVA: 0x0000964A File Offset: 0x0000784A
			// (set) Token: 0x0600020D RID: 525 RVA: 0x00009652 File Offset: 0x00007852
			internal List<ConceptualTranslation> Translations { get; set; }
		}

		// Token: 0x02000047 RID: 71
		internal sealed class EdmDisplayItem
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600020F RID: 527 RVA: 0x00009663 File Offset: 0x00007863
			// (set) Token: 0x06000210 RID: 528 RVA: 0x0000966B File Offset: 0x0000786B
			internal string PropertyRef { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000211 RID: 529 RVA: 0x00009674 File Offset: 0x00007874
			// (set) Token: 0x06000212 RID: 530 RVA: 0x0000967C File Offset: 0x0000787C
			internal string HierarchyRef { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000213 RID: 531 RVA: 0x00009685 File Offset: 0x00007885
			// (set) Token: 0x06000214 RID: 532 RVA: 0x0000968D File Offset: 0x0000788D
			internal EdmExtensions.EdmDisplayFolder DisplayFolder { get; set; }
		}

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001D7 RID: 471
			public static Func<XAttribute, string> <0>__GetValueForAttribute;
		}
	}
}
