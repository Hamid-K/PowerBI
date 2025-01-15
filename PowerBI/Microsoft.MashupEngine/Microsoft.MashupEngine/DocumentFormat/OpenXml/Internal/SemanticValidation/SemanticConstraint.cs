using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030E1 RID: 12513
	internal abstract class SemanticConstraint
	{
		// Token: 0x17009889 RID: 39049
		// (get) Token: 0x0601B2DB RID: 111323 RVA: 0x0036FDA8 File Offset: 0x0036DFA8
		// (set) Token: 0x0601B2DC RID: 111324 RVA: 0x0000336E File Offset: 0x0000156E
		internal string ConstratintId
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x1700988A RID: 39050
		// (get) Token: 0x0601B2DD RID: 111325 RVA: 0x0036FDBC File Offset: 0x0036DFBC
		public virtual SemanticValidationLevel StateScope
		{
			get
			{
				return this.SemanticValidationLevel;
			}
		}

		// Token: 0x0601B2DE RID: 111326 RVA: 0x0036FDC4 File Offset: 0x0036DFC4
		public SemanticConstraint(SemanticValidationLevel level)
		{
			this.SemanticValidationLevel = level;
		}

		// Token: 0x0601B2DF RID: 111327 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ClearState(ValidationContext context)
		{
		}

		// Token: 0x0601B2E0 RID: 111328
		public abstract ValidationErrorInfo Validate(ValidationContext context);

		// Token: 0x0601B2E1 RID: 111329 RVA: 0x0036FDD4 File Offset: 0x0036DFD4
		protected static OpenXmlPart GetReferencedPart(ValidationContext context, string path)
		{
			if (path == ".")
			{
				return context.Part;
			}
			string[] array = path.Split(new char[] { '/' });
			if (string.IsNullOrEmpty(array[0]))
			{
				return SemanticConstraint.GetPartThroughPartPath(context.Package.Parts, array.Skip(1).ToArray<string>());
			}
			if (array[0] == "..")
			{
				IEnumerable<OpenXmlPart> enumerable = new OpenXmlPackagePartIterator(context.Package);
				IEnumerable<OpenXmlPart> enumerable2 = enumerable.Where((OpenXmlPart p) => p.Parts.Select((IdPartPair r) => r.OpenXmlPart.PackagePart.Uri).Contains(context.Part.PackagePart.Uri));
				return enumerable2.First<OpenXmlPart>();
			}
			return SemanticConstraint.GetPartThroughPartPath(context.Part.Parts, array);
		}

		// Token: 0x0601B2E2 RID: 111330 RVA: 0x0036FEA8 File Offset: 0x0036E0A8
		protected static XmlQualifiedName GetAttributeQualifiedName(OpenXmlElement element, byte attributeID)
		{
			return new XmlQualifiedName(element.AttributeTagNames[(int)attributeID], NamespaceIdMap.GetNamespaceUri(element.AttributeNamespaceIds[(int)attributeID]));
		}

		// Token: 0x0601B2E3 RID: 111331 RVA: 0x0036FEC4 File Offset: 0x0036E0C4
		private static bool CompareBooleanValue(bool value1, string value2)
		{
			bool flag;
			return bool.TryParse(value2, out flag) && value1 == flag;
		}

		// Token: 0x0601B2E4 RID: 111332 RVA: 0x0036FEE4 File Offset: 0x0036E0E4
		protected static bool AttributeValueEquals(OpenXmlSimpleType type, string value, bool ignoreCase)
		{
			HexBinaryValue hexBinaryValue = type as HexBinaryValue;
			if (hexBinaryValue != null)
			{
				return !hexBinaryValue.HasValue || Convert.ToInt64(hexBinaryValue.Value, 16) == Convert.ToInt64(value, 16);
			}
			BooleanValue booleanValue = type as BooleanValue;
			if (booleanValue != null)
			{
				if (!booleanValue.HasValue)
				{
					return false;
				}
				if (SemanticConstraint.CompareBooleanValue(booleanValue.Value, value))
				{
					return true;
				}
			}
			OnOffValue onOffValue = type as OnOffValue;
			if (onOffValue != null)
			{
				if (!onOffValue.HasValue)
				{
					return false;
				}
				if (SemanticConstraint.CompareBooleanValue(onOffValue.Value, value))
				{
					return true;
				}
			}
			TrueFalseValue trueFalseValue = type as TrueFalseValue;
			if (trueFalseValue != null)
			{
				if (!trueFalseValue.HasValue)
				{
					return false;
				}
				if (SemanticConstraint.CompareBooleanValue(trueFalseValue.Value, value))
				{
					return true;
				}
			}
			TrueFalseBlankValue trueFalseBlankValue = type as TrueFalseBlankValue;
			if (trueFalseBlankValue != null)
			{
				if (!trueFalseBlankValue.HasValue)
				{
					return false;
				}
				if (SemanticConstraint.CompareBooleanValue(trueFalseBlankValue.Value, value))
				{
					return true;
				}
			}
			if (ignoreCase)
			{
				return string.Equals(value, type.InnerText, StringComparison.OrdinalIgnoreCase);
			}
			return string.Equals(value, type.InnerText, StringComparison.Ordinal);
		}

		// Token: 0x0601B2E5 RID: 111333 RVA: 0x0036FFD0 File Offset: 0x0036E1D0
		protected static bool GetAttrNumVal(OpenXmlSimpleType attributeValue, out double value)
		{
			HexBinaryValue hexBinaryValue = attributeValue as HexBinaryValue;
			if (hexBinaryValue != null)
			{
				long num = -1L;
				bool flag = long.TryParse(hexBinaryValue.Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num);
				value = (double)num;
				return flag;
			}
			return double.TryParse(attributeValue.InnerText, out value);
		}

		// Token: 0x0601B2E6 RID: 111334 RVA: 0x00370014 File Offset: 0x0036E214
		private static OpenXmlPart GetPartThroughPartPath(IEnumerable<IdPartPair> pairs, string[] path)
		{
			SemanticConstraint.<>c__DisplayClass9 CS$<>8__locals1 = new SemanticConstraint.<>c__DisplayClass9();
			CS$<>8__locals1.path = path;
			OpenXmlPart openXmlPart = null;
			IEnumerable<IdPartPair> enumerable = pairs;
			int i;
			for (i = 0; i < CS$<>8__locals1.path.Length; i++)
			{
				IEnumerable<OpenXmlPart> enumerable2 = from p in enumerable
					where p.OpenXmlPart.GetType().Name == CS$<>8__locals1.path[i]
					select p into t
					select t.OpenXmlPart;
				if (enumerable2.Count<OpenXmlPart>() == 0)
				{
					return null;
				}
				openXmlPart = enumerable2.First<OpenXmlPart>();
				enumerable = openXmlPart.Parts;
			}
			return openXmlPart;
		}

		// Token: 0x0400B41C RID: 46108
		public readonly SemanticValidationLevel SemanticValidationLevel;
	}
}
