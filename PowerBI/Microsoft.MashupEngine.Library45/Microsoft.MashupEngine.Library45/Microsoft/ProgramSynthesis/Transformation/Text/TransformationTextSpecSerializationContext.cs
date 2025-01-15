using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Specifications;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BB8 RID: 7096
	public class TransformationTextSpecSerializationContext : SpecSerializationContext
	{
		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x0600E846 RID: 59462 RVA: 0x00313C4B File Offset: 0x00311E4B
		public DeserializationContext DeserializationContext { get; }

		// Token: 0x0600E847 RID: 59463 RVA: 0x00313C53 File Offset: 0x00311E53
		private TransformationTextSpecSerializationContext(DeserializationContext deserializationContext)
			: this()
		{
			this.DeserializationContext = deserializationContext;
		}

		// Token: 0x0600E848 RID: 59464 RVA: 0x00313C64 File Offset: 0x00311E64
		private static Dictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> ConstructDeserializerMap(DeserializationContext deserializationContext)
		{
			Dictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>> dictionary = new Dictionary<string, Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>>();
			string canonicalTypeName = SpecSerializationContext.GetCanonicalTypeName(typeof(ValueSubstringRow));
			dictionary[canonicalTypeName] = (XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx) => ValueSubstringRow.DeserializeFromXML(node, identityCache);
			string canonicalTypeName2 = SpecSerializationContext.GetCanonicalTypeName(typeof(ValueSubstring));
			dictionary[canonicalTypeName2] = (XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx) => ValueSubstring.DeserializeFromXML(node, identityCache);
			string canonicalTypeName3 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<uint?, uint?>?));
			string text = canonicalTypeName3;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func;
			if ((func = TransformationTextSpecSerializationContext.<>O.<0>__DeserializePp) == null)
			{
				func = (TransformationTextSpecSerializationContext.<>O.<0>__DeserializePp = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializePp));
			}
			dictionary[text] = func;
			string canonicalTypeName4 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<uint?, uint?>));
			string text2 = canonicalTypeName4;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func2;
			if ((func2 = TransformationTextSpecSerializationContext.<>O.<0>__DeserializePp) == null)
			{
				func2 = (TransformationTextSpecSerializationContext.<>O.<0>__DeserializePp = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializePp));
			}
			dictionary[text2] = func2;
			string canonicalTypeName5 = SpecSerializationContext.GetCanonicalTypeName(typeof(uint?));
			string text3 = canonicalTypeName5;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func3;
			if ((func3 = TransformationTextSpecSerializationContext.<>O.<1>__DeserializeUint) == null)
			{
				func3 = (TransformationTextSpecSerializationContext.<>O.<1>__DeserializeUint = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeUint));
			}
			dictionary[text3] = func3;
			string canonicalTypeName6 = SpecSerializationContext.GetCanonicalTypeName(typeof(bool));
			string text4 = canonicalTypeName6;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func4;
			if ((func4 = TransformationTextSpecSerializationContext.<>O.<2>__DeserializeBool) == null)
			{
				func4 = (TransformationTextSpecSerializationContext.<>O.<2>__DeserializeBool = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeBool));
			}
			dictionary[text4] = func4;
			string canonicalTypeName7 = SpecSerializationContext.GetCanonicalTypeName(typeof(decimal));
			string text5 = canonicalTypeName7;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func5;
			if ((func5 = TransformationTextSpecSerializationContext.<>O.<3>__DeserializeDecimal) == null)
			{
				func5 = (TransformationTextSpecSerializationContext.<>O.<3>__DeserializeDecimal = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeDecimal));
			}
			dictionary[text5] = func5;
			string canonicalTypeName8 = SpecSerializationContext.GetCanonicalTypeName(typeof(decimal?));
			string text6 = canonicalTypeName8;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func6;
			if ((func6 = TransformationTextSpecSerializationContext.<>O.<3>__DeserializeDecimal) == null)
			{
				func6 = (TransformationTextSpecSerializationContext.<>O.<3>__DeserializeDecimal = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeDecimal));
			}
			dictionary[text6] = func6;
			string canonicalTypeName9 = SpecSerializationContext.GetCanonicalTypeName(typeof(RegularExpression));
			dictionary[canonicalTypeName9] = (XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx) => TransformationTextSpecSerializationContext.DeserializeRegex(node, identityCache, ctx, deserializationContext);
			string canonicalTypeName10 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<RegularExpression, RegularExpression>?));
			dictionary[canonicalTypeName10] = (XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx) => TransformationTextSpecSerializationContext.DeserializeRR(node, identityCache, ctx, deserializationContext);
			string canonicalTypeName11 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<RegularExpression, RegularExpression>));
			dictionary[canonicalTypeName11] = (XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx) => TransformationTextSpecSerializationContext.DeserializeRR(node, identityCache, ctx, deserializationContext);
			string canonicalTypeName12 = SpecSerializationContext.GetCanonicalTypeName(typeof(Dictionary<Optional<string>, string>));
			string text7 = canonicalTypeName12;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func7;
			if ((func7 = TransformationTextSpecSerializationContext.<>O.<4>__DeserializeLookupDict) == null)
			{
				func7 = (TransformationTextSpecSerializationContext.<>O.<4>__DeserializeLookupDict = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeLookupDict));
			}
			dictionary[text7] = func7;
			string canonicalTypeName13 = SpecSerializationContext.GetCanonicalTypeName(typeof(Optional<string>));
			string text8 = canonicalTypeName13;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func8;
			if ((func8 = TransformationTextSpecSerializationContext.<>O.<5>__DeserializeOptionalString) == null)
			{
				func8 = (TransformationTextSpecSerializationContext.<>O.<5>__DeserializeOptionalString = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(TransformationTextSpecSerializationContext.DeserializeOptionalString));
			}
			dictionary[text8] = func8;
			string canonicalTypeName14 = SpecSerializationContext.GetCanonicalTypeName(typeof(BisectSpec));
			string text9 = canonicalTypeName14;
			Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> func9;
			if ((func9 = TransformationTextSpecSerializationContext.<>O.<6>__DeserializeFromXML) == null)
			{
				func9 = (TransformationTextSpecSerializationContext.<>O.<6>__DeserializeFromXML = new Func<XElement, Dictionary<int, object>, SpecSerializationContext, object>(BisectSpec.DeserializeFromXML));
			}
			dictionary[text9] = func9;
			string canonicalTypeName15 = SpecSerializationContext.GetCanonicalTypeName(typeof(RoundingSpec));
			dictionary[canonicalTypeName15] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => RoundingSpec.TryParseXML(node);
			string canonicalTypeName16 = SpecSerializationContext.GetCanonicalTypeName(typeof(StringPrefixSet));
			dictionary[canonicalTypeName16] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => StringPrefixSet.DeserializeFromXML(node, idCache);
			string canonicalTypeName17 = SpecSerializationContext.GetCanonicalTypeName(typeof(NumberFormat));
			dictionary[canonicalTypeName17] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => NumberFormat.TryParseXML(node).OrElseCompute(delegate
			{
				throw new InvalidOperationException();
			});
			string canonicalTypeName18 = SpecSerializationContext.GetCanonicalTypeName(typeof(NumberFormatDetails));
			dictionary[canonicalTypeName18] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => NumberFormatDetails.TryParseXML(node).OrElseCompute(delegate
			{
				throw new InvalidOperationException();
			});
			string canonicalTypeName19 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeRoundingSpec));
			dictionary[canonicalTypeName19] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => DateTimeRoundingSpec.TryParseXML(node);
			string canonicalTypeName20 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeFormat));
			dictionary[canonicalTypeName20] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => DateTimeFormat.TryParseFromXML(node);
			string canonicalTypeName21 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeFormat[]));
			dictionary[canonicalTypeName21] = delegate(XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx)
			{
				IEnumerable<XElement> enumerable = node.Elements();
				Func<XElement, DateTimeFormat> func10;
				if ((func10 = TransformationTextSpecSerializationContext.<>O.<7>__TryParseFromXML) == null)
				{
					func10 = (TransformationTextSpecSerializationContext.<>O.<7>__TryParseFromXML = new Func<XElement, DateTimeFormat>(DateTimeFormat.TryParseFromXML));
				}
				return enumerable.Select(func10).ToArray<DateTimeFormat>();
			};
			string canonicalTypeName22 = SpecSerializationContext.GetCanonicalTypeName(typeof(PartialDateTime));
			dictionary[canonicalTypeName22] = (XElement node, Dictionary<int, object> idCache, SpecSerializationContext ctx) => PartialDateTime.TryParseXML(node);
			return dictionary;
		}

		// Token: 0x0600E849 RID: 59465 RVA: 0x003140F0 File Offset: 0x003122F0
		private TransformationTextSpecSerializationContext()
			: base(Language.Grammar, TransformationTextSpecSerializationContext.ConstructDeserializerMap(default(DeserializationContext)), TransformationTextSpecSerializationContext.SerializerMap)
		{
		}

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x0600E84A RID: 59466 RVA: 0x0031411B File Offset: 0x0031231B
		public static TransformationTextSpecSerializationContext Instance { get; }

		// Token: 0x0600E84B RID: 59467 RVA: 0x00314122 File Offset: 0x00312322
		public static TransformationTextSpecSerializationContext Create(DeserializationContext deserializationContext)
		{
			return new TransformationTextSpecSerializationContext(deserializationContext);
		}

		// Token: 0x0600E84C RID: 59468 RVA: 0x0031412C File Offset: 0x0031232C
		private static XElement SerializePp(object pp, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			Record<uint?, uint?>? record = (Record<uint?, uint?>?)pp;
			if (record == null)
			{
				return new XElement("PP", null);
			}
			return new XElement("PP", new object[]
			{
				new XElement("Start", record.Value.Item1),
				new XElement("End", record.Value.Item2)
			});
		}

		// Token: 0x0600E84D RID: 59469 RVA: 0x003141B8 File Offset: 0x003123B8
		private static object DeserializePp(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (node.Name != "PP")
			{
				throw new InvalidOperationException();
			}
			if (!node.Elements().Any<XElement>())
			{
				return null;
			}
			XElement xelement = node.Element("Start");
			string text = ((xelement != null) ? xelement.Value : null);
			XElement xelement2 = node.Element("End");
			string text2 = ((xelement2 != null) ? xelement2.Value : null);
			uint? num = (string.IsNullOrEmpty(text) ? null : new uint?(uint.Parse(text)));
			uint? num2 = (string.IsNullOrEmpty(text2) ? null : new uint?(uint.Parse(text2)));
			return new Record<uint?, uint?>(num, num2);
		}

		// Token: 0x0600E84E RID: 59470 RVA: 0x00314273 File Offset: 0x00312473
		private static XElement SerializeUint(object i, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			return new XElement("uint?", (uint?)i);
		}

		// Token: 0x0600E84F RID: 59471 RVA: 0x00314290 File Offset: 0x00312490
		private static object DeserializeUint(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (node.Name != "uint?")
			{
				throw new InvalidOperationException();
			}
			return string.IsNullOrEmpty(node.Value) ? null : new uint?(uint.Parse(node.Value));
		}

		// Token: 0x0600E850 RID: 59472 RVA: 0x003142E7 File Offset: 0x003124E7
		private static XElement SerializeBool(object b, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			return new XElement("bool", b);
		}

		// Token: 0x0600E851 RID: 59473 RVA: 0x003142F9 File Offset: 0x003124F9
		private static object DeserializeBool(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (node.Name != "bool")
			{
				throw new InvalidOperationException();
			}
			return bool.Parse(node.Value);
		}

		// Token: 0x0600E852 RID: 59474 RVA: 0x00314328 File Offset: 0x00312528
		private static XElement SerializeDecimal(object d, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			decimal? num = (decimal?)d;
			if (num == null)
			{
				return new XElement("decimal", null);
			}
			return new XElement("decimal", num.Value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600E853 RID: 59475 RVA: 0x0031437C File Offset: 0x0031257C
		private static object DeserializeDecimal(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (node.Name != "decimal")
			{
				throw new InvalidOperationException();
			}
			return string.IsNullOrEmpty(node.Value) ? null : new decimal?(decimal.Parse(node.Value));
		}

		// Token: 0x0600E854 RID: 59476 RVA: 0x003143D4 File Offset: 0x003125D4
		private static XElement SerializeRegex(object d, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			RegularExpression regularExpression = (RegularExpression)d;
			return new XElement("RegularExpression", regularExpression.RenderXML());
		}

		// Token: 0x0600E855 RID: 59477 RVA: 0x00314400 File Offset: 0x00312600
		private static object DeserializeRegex(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx, DeserializationContext deserializationCtx)
		{
			if (node.Name != "RegularExpression")
			{
				throw new InvalidOperationException();
			}
			return RegularExpression.TryParseFromXML(node.Elements().First<XElement>(), deserializationCtx).Value;
		}

		// Token: 0x0600E856 RID: 59478 RVA: 0x00314444 File Offset: 0x00312644
		private static XElement SerializeRR(object d, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			Record<RegularExpression, RegularExpression>? record = (Record<RegularExpression, RegularExpression>?)d;
			if (record == null)
			{
				return new XElement("RR");
			}
			return new XElement("RR", new object[]
			{
				record.Value.Item1.RenderXML(),
				record.Value.Item2.RenderXML()
			});
		}

		// Token: 0x0600E857 RID: 59479 RVA: 0x003144B0 File Offset: 0x003126B0
		private static object DeserializeRR(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx, DeserializationContext deserializationCtx)
		{
			if (node.Name != "RR")
			{
				throw new InvalidOperationException();
			}
			if (!node.Elements().Any<XElement>())
			{
				return null;
			}
			List<XElement> list = node.Elements().ToList<XElement>();
			RegularExpression value = RegularExpression.TryParseFromXML(list[0], deserializationCtx).Value;
			RegularExpression value2 = RegularExpression.TryParseFromXML(list[1], deserializationCtx).Value;
			return new Record<RegularExpression, RegularExpression>(value, value2);
		}

		// Token: 0x0600E858 RID: 59480 RVA: 0x0031452C File Offset: 0x0031272C
		private static XElement SerializeLookupDict(object value, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			Dictionary<Optional<string>, string> dictionary = (Dictionary<Optional<string>, string>)value;
			return new XElement("Dictionary", dictionary.Select((KeyValuePair<Optional<string>, string> kvp) => new XElement("Binding", new object[]
			{
				ctx.SerializeObject(kvp.Key, identityCache),
				ctx.SerializeObject(kvp.Value, identityCache)
			})));
		}

		// Token: 0x0600E859 RID: 59481 RVA: 0x00314578 File Offset: 0x00312778
		private static object DeserializeLookupDict(XElement value, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (value.Name != "Dictionary")
			{
				throw new InvalidOperationException();
			}
			return value.Elements("Binding").ToDictionary((XElement bindingNode) => (Optional<string>)ctx.DeserializeObject(bindingNode.Elements().First<XElement>(), identityCache), (XElement bindingNode) => (string)ctx.DeserializeObject(bindingNode.Elements().Last<XElement>(), identityCache));
		}

		// Token: 0x0600E85A RID: 59482 RVA: 0x003145E4 File Offset: 0x003127E4
		private static XElement SerializeOptionalString(object value, Dictionary<object, int> identityCache, SpecSerializationContext ctx)
		{
			Optional<string> optional = (Optional<string>)value;
			if (optional.HasValue)
			{
				return new XElement("Optional", ctx.SerializeObject(optional.Value, identityCache)).WithAttribute("HasValue", true);
			}
			return new XElement("Optional").WithAttribute("HasValue", false);
		}

		// Token: 0x0600E85B RID: 59483 RVA: 0x00314650 File Offset: 0x00312850
		private static object DeserializeOptionalString(XElement node, Dictionary<int, object> identityCache, SpecSerializationContext ctx)
		{
			if (node.Name != "Optional")
			{
				throw new InvalidOperationException();
			}
			XAttribute xattribute = node.Attribute("HasValue");
			if (((xattribute != null) ? xattribute.Value : null) == "true")
			{
				return ((string)ctx.DeserializeObject(node.Elements().Single<XElement>(), identityCache)).Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600E85C RID: 59484 RVA: 0x003146D0 File Offset: 0x003128D0
		// Note: this type is marked as 'beforefieldinit'.
		static TransformationTextSpecSerializationContext()
		{
			Dictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> dictionary = new Dictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>>();
			string canonicalTypeName = SpecSerializationContext.GetCanonicalTypeName(typeof(ValueSubstringRow));
			dictionary[canonicalTypeName] = (object value, Dictionary<object, int> identityCache, SpecSerializationContext ctx) => ((ValueSubstringRow)value).SerializeToXML(identityCache);
			string canonicalTypeName2 = SpecSerializationContext.GetCanonicalTypeName(typeof(ValueSubstring));
			dictionary[canonicalTypeName2] = (object value, Dictionary<object, int> identityCache, SpecSerializationContext ctx) => ((ValueSubstring)value).SerializeToXML(identityCache);
			string canonicalTypeName3 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<uint?, uint?>?));
			dictionary[canonicalTypeName3] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializePp);
			string canonicalTypeName4 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<uint?, uint?>));
			dictionary[canonicalTypeName4] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializePp);
			string canonicalTypeName5 = SpecSerializationContext.GetCanonicalTypeName(typeof(uint?));
			dictionary[canonicalTypeName5] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeUint);
			string canonicalTypeName6 = SpecSerializationContext.GetCanonicalTypeName(typeof(bool));
			dictionary[canonicalTypeName6] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeBool);
			string canonicalTypeName7 = SpecSerializationContext.GetCanonicalTypeName(typeof(decimal));
			dictionary[canonicalTypeName7] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeDecimal);
			string canonicalTypeName8 = SpecSerializationContext.GetCanonicalTypeName(typeof(decimal?));
			dictionary[canonicalTypeName8] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeDecimal);
			string canonicalTypeName9 = SpecSerializationContext.GetCanonicalTypeName(typeof(RegularExpression));
			dictionary[canonicalTypeName9] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeRegex);
			string canonicalTypeName10 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<RegularExpression, RegularExpression>?));
			dictionary[canonicalTypeName10] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeRR);
			string canonicalTypeName11 = SpecSerializationContext.GetCanonicalTypeName(typeof(Record<RegularExpression, RegularExpression>));
			dictionary[canonicalTypeName11] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeRR);
			string canonicalTypeName12 = SpecSerializationContext.GetCanonicalTypeName(typeof(Dictionary<Optional<string>, string>));
			dictionary[canonicalTypeName12] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeLookupDict);
			string canonicalTypeName13 = SpecSerializationContext.GetCanonicalTypeName(typeof(Optional<string>));
			dictionary[canonicalTypeName13] = new Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>(TransformationTextSpecSerializationContext.SerializeOptionalString);
			string canonicalTypeName14 = SpecSerializationContext.GetCanonicalTypeName(typeof(BisectSpec));
			dictionary[canonicalTypeName14] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((BisectSpec)value).SerializeToXML(idCache, ctx);
			string canonicalTypeName15 = SpecSerializationContext.GetCanonicalTypeName(typeof(RoundingSpec));
			dictionary[canonicalTypeName15] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((RoundingSpec)value).RenderXML();
			string canonicalTypeName16 = SpecSerializationContext.GetCanonicalTypeName(typeof(StringPrefixSet));
			dictionary[canonicalTypeName16] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((StringPrefixSet)value).SerializeToXML(idCache);
			string canonicalTypeName17 = SpecSerializationContext.GetCanonicalTypeName(typeof(NumberFormat));
			dictionary[canonicalTypeName17] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((NumberFormat)value).RenderXML();
			string canonicalTypeName18 = SpecSerializationContext.GetCanonicalTypeName(typeof(NumberFormatDetails));
			dictionary[canonicalTypeName18] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((NumberFormatDetails)value).RenderXML();
			string canonicalTypeName19 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeRoundingSpec));
			dictionary[canonicalTypeName19] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((DateTimeRoundingSpec)value).RenderXML();
			string canonicalTypeName20 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeFormat));
			dictionary[canonicalTypeName20] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((DateTimeFormat)value).RenderXML();
			string canonicalTypeName21 = SpecSerializationContext.GetCanonicalTypeName(typeof(DateTimeFormat[]));
			dictionary[canonicalTypeName21] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => new XElement("DateTimeFormatArray", ((DateTimeFormat[])value).Select((DateTimeFormat format) => format.RenderXML()));
			string canonicalTypeName22 = SpecSerializationContext.GetCanonicalTypeName(typeof(PartialDateTime));
			dictionary[canonicalTypeName22] = (object value, Dictionary<object, int> idCache, SpecSerializationContext ctx) => ((PartialDateTime)value).RenderXML();
			TransformationTextSpecSerializationContext.SerializerMap = dictionary;
			TransformationTextSpecSerializationContext.Instance = new TransformationTextSpecSerializationContext();
		}

		// Token: 0x04005875 RID: 22645
		private static readonly Dictionary<string, Func<object, Dictionary<object, int>, SpecSerializationContext, XElement>> SerializerMap;

		// Token: 0x02001BB9 RID: 7097
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005877 RID: 22647
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <0>__DeserializePp;

			// Token: 0x04005878 RID: 22648
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <1>__DeserializeUint;

			// Token: 0x04005879 RID: 22649
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <2>__DeserializeBool;

			// Token: 0x0400587A RID: 22650
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <3>__DeserializeDecimal;

			// Token: 0x0400587B RID: 22651
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <4>__DeserializeLookupDict;

			// Token: 0x0400587C RID: 22652
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <5>__DeserializeOptionalString;

			// Token: 0x0400587D RID: 22653
			public static Func<XElement, Dictionary<int, object>, SpecSerializationContext, object> <6>__DeserializeFromXML;

			// Token: 0x0400587E RID: 22654
			public static Func<XElement, DateTimeFormat> <7>__TryParseFromXML;
		}
	}
}
