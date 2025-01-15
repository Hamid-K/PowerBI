using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Mathml;
using AngleSharp.Dom.Svg;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Parser.Html
{
	// Token: 0x0200006A RID: 106
	internal static class HtmlForeignExtensions
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0001332C File Offset: 0x0001152C
		public static string SanatizeSvgTagName(this string localName)
		{
			string text = null;
			if (HtmlForeignExtensions.svgAdjustedTagNames.TryGetValue(localName, out text))
			{
				return text;
			}
			return localName;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00013350 File Offset: 0x00011550
		public static MathElement Setup(this MathElement element, HtmlTagToken tag)
		{
			int count = tag.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				string key = tag.Attributes[i].Key;
				string value = tag.Attributes[i].Value;
				element.AdjustAttribute(key.AdjustToMathAttribute(), value);
			}
			return element;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000133B0 File Offset: 0x000115B0
		public static SvgElement Setup(this SvgElement element, HtmlTagToken tag)
		{
			int count = tag.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				string key = tag.Attributes[i].Key;
				string value = tag.Attributes[i].Value;
				element.AdjustAttribute(key.AdjustToSvgAttribute(), value);
			}
			return element;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00013410 File Offset: 0x00011610
		public static void AdjustAttribute(this Element element, string name, string value)
		{
			if (HtmlForeignExtensions.IsXLinkAttribute(name))
			{
				element.SetAttribute(NamespaceNames.XLinkUri, name.Substring(name.IndexOf(':') + 1), value);
				return;
			}
			if (HtmlForeignExtensions.IsXmlAttribute(name))
			{
				element.SetAttribute(NamespaceNames.XmlUri, name, value);
				return;
			}
			if (HtmlForeignExtensions.IsXmlNamespaceAttribute(name))
			{
				element.SetAttribute(NamespaceNames.XmlNsUri, name, value);
				return;
			}
			element.SetOwnAttribute(name, value, false);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00013477 File Offset: 0x00011677
		public static string AdjustToMathAttribute(this string attributeName)
		{
			if (attributeName.Is("definitionurl"))
			{
				return "definitionURL";
			}
			return attributeName;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00013490 File Offset: 0x00011690
		public static string AdjustToSvgAttribute(this string attributeName)
		{
			string text = null;
			if (HtmlForeignExtensions.svgAttributeNames.TryGetValue(attributeName, out text))
			{
				return text;
			}
			return attributeName;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000134B1 File Offset: 0x000116B1
		private static bool IsXmlNamespaceAttribute(string name)
		{
			return name.Length > 4 && (name.Is(NamespaceNames.XmlNsPrefix) || name.Is("xmlns:xlink"));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000134D8 File Offset: 0x000116D8
		private static bool IsXmlAttribute(string name)
		{
			return name.Length > 7 && "xml:".EqualsSubset(name, 0, 4) && (TagNames.Base.EqualsSubset(name, 4, 4) || AttributeNames.Lang.EqualsSubset(name, 4, 4) || AttributeNames.Space.EqualsSubset(name, 4, 5));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001352C File Offset: 0x0001172C
		private static bool IsXLinkAttribute(string name)
		{
			return name.Length > 9 && "xlink:".EqualsSubset(name, 0, 6) && (AttributeNames.Actuate.EqualsSubset(name, 6, 7) || AttributeNames.Arcrole.EqualsSubset(name, 6, 7) || AttributeNames.Href.EqualsSubset(name, 6, 4) || AttributeNames.Role.EqualsSubset(name, 6, 4) || AttributeNames.Show.EqualsSubset(name, 6, 4) || AttributeNames.Type.EqualsSubset(name, 6, 4) || AttributeNames.Title.EqualsSubset(name, 6, 5));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000135BD File Offset: 0x000117BD
		private static bool EqualsSubset(this string a, string b, int index, int length)
		{
			return string.Compare(a, 0, b, index, length, StringComparison.Ordinal) == 0;
		}

		// Token: 0x04000240 RID: 576
		private static readonly Dictionary<string, string> svgAttributeNames = new Dictionary<string, string>(StringComparer.Ordinal)
		{
			{ "attributename", "attributeName" },
			{ "attributetype", "attributeType" },
			{ "basefrequency", "baseFrequency" },
			{ "baseprofile", "baseProfile" },
			{ "calcmode", "calcMode" },
			{ "clippathunits", "clipPathUnits" },
			{ "contentscripttype", "contentScriptType" },
			{ "contentstyletype", "contentStyleType" },
			{ "diffuseconstant", "diffuseConstant" },
			{ "edgemode", "edgeMode" },
			{ "externalresourcesrequired", "externalResourcesRequired" },
			{ "filterres", "filterRes" },
			{ "filterunits", "filterUnits" },
			{ "glyphref", "glyphRef" },
			{ "gradienttransform", "gradientTransform" },
			{ "gradientunits", "gradientUnits" },
			{ "kernelmatrix", "kernelMatrix" },
			{ "kernelunitlength", "kernelUnitLength" },
			{ "keypoints", "keyPoints" },
			{ "keysplines", "keySplines" },
			{ "keytimes", "keyTimes" },
			{ "lengthadjust", "lengthAdjust" },
			{ "limitingconeangle", "limitingConeAngle" },
			{ "markerheight", "markerHeight" },
			{ "markerunits", "markerUnits" },
			{ "markerwidth", "markerWidth" },
			{ "maskcontentunits", "maskContentUnits" },
			{ "maskunits", "maskUnits" },
			{ "numoctaves", "numOctaves" },
			{ "pathlength", "pathLength" },
			{ "patterncontentunits", "patternContentUnits" },
			{ "patterntransform", "patternTransform" },
			{ "patternunits", "patternUnits" },
			{ "pointsatx", "pointsAtX" },
			{ "pointsaty", "pointsAtY" },
			{ "pointsatz", "pointsAtZ" },
			{ "preservealpha", "preserveAlpha" },
			{ "preserveaspectratio", "preserveAspectRatio" },
			{ "primitiveunits", "primitiveUnits" },
			{ "refx", "refX" },
			{ "refy", "refY" },
			{ "repeatcount", "repeatCount" },
			{ "repeatdur", "repeatDur" },
			{ "requiredextensions", "requiredExtensions" },
			{ "requiredfeatures", "requiredFeatures" },
			{ "specularconstant", "specularConstant" },
			{ "specularexponent", "specularExponent" },
			{ "spreadmethod", "spreadMethod" },
			{ "startoffset", "startOffset" },
			{ "stddeviation", "stdDeviation" },
			{ "stitchtiles", "stitchTiles" },
			{ "surfacescale", "surfaceScale" },
			{ "systemlanguage", "systemLanguage" },
			{ "tablevalues", "tableValues" },
			{ "targetx", "targetX" },
			{ "targety", "targetY" },
			{ "textlength", "textLength" },
			{ "viewbox", "viewBox" },
			{ "viewtarget", "viewTarget" },
			{ "xchannelselector", "xChannelSelector" },
			{ "ychannelselector", "yChannelSelector" },
			{ "zoomandpan", "zoomAndPan" }
		};

		// Token: 0x04000241 RID: 577
		private static readonly Dictionary<string, string> svgAdjustedTagNames = new Dictionary<string, string>(StringComparer.Ordinal)
		{
			{ "altglyph", "altGlyph" },
			{ "altglyphdef", "altGlyphDef" },
			{ "altglyphitem", "altGlyphItem" },
			{ "animatecolor", "animateColor" },
			{ "animatemotion", "animateMotion" },
			{ "animatetransform", "animateTransform" },
			{ "clippath", "clipPath" },
			{ "feblend", "feBlend" },
			{ "fecolormatrix", "feColorMatrix" },
			{ "fecomponenttransfer", "feComponentTransfer" },
			{ "fecomposite", "feComposite" },
			{ "feconvolvematrix", "feConvolveMatrix" },
			{ "fediffuselighting", "feDiffuseLighting" },
			{ "fedisplacementmap", "feDisplacementMap" },
			{ "fedistantlight", "feDistantLight" },
			{ "feflood", "feFlood" },
			{ "fefunca", "feFuncA" },
			{ "fefuncb", "feFuncB" },
			{ "fefuncg", "feFuncG" },
			{ "fefuncr", "feFuncR" },
			{ "fegaussianblur", "feGaussianBlur" },
			{ "feimage", "feImage" },
			{ "femerge", "feMerge" },
			{ "femergenode", "feMergeNode" },
			{ "femorphology", "feMorphology" },
			{ "feoffset", "feOffset" },
			{ "fepointlight", "fePointLight" },
			{ "fespecularlighting", "feSpecularLighting" },
			{ "fespotlight", "feSpotLight" },
			{ "fetile", "feTile" },
			{ "feturbulence", "feTurbulence" },
			{ "foreignobject", "foreignObject" },
			{ "glyphref", "glyphRef" },
			{ "lineargradient", "linearGradient" },
			{ "radialgradient", "radialGradient" },
			{ "textpath", "textPath" }
		};
	}
}
