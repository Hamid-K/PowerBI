using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200015C RID: 348
	public class SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000186AC File Offset: 0x000168AC
		public SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Root { get; }

		// Token: 0x060007CD RID: 1997 RVA: 0x000186B4 File Offset: 0x000168B4
		public SchemaGrammar(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> e)
		{
			this.Root = e;
			this.Root.Parent = BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Instance;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000186D3 File Offset: 0x000168D3
		public SchemaElement<TSequenceProgram, TRegionProgram, TRegion> FindElement(string e)
		{
			return this.FindElement(this.Root, e);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000186E4 File Offset: 0x000168E4
		private SchemaElement<TSequenceProgram, TRegionProgram, TRegion> FindElement(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> e, string s)
		{
			if (e.Name == s)
			{
				return e;
			}
			if (e is FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
			{
				return null;
			}
			SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion> sequenceSchemaElement = e as SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>;
			if (sequenceSchemaElement != null)
			{
				return this.FindElement(sequenceSchemaElement.Element, s);
			}
			StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> structSchemaElement = e as StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>;
			if (structSchemaElement != null)
			{
				foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in structSchemaElement.Members)
				{
					SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement2 = this.FindElement(schemaElement, s);
					if (schemaElement2 != null)
					{
						return schemaElement2;
					}
				}
			}
			UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion> unionSchemaElement = e as UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion>;
			if (unionSchemaElement == null)
			{
				return null;
			}
			return unionSchemaElement.Members.Select((SchemaElement<TSequenceProgram, TRegionProgram, TRegion> v) => this.FindElement(v, s)).FirstOrDefault((SchemaElement<TSequenceProgram, TRegionProgram, TRegion> child) => child != null);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000187F0 File Offset: 0x000169F0
		public static SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion> Load(string grammar, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters = null)
		{
			return new SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>(SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.ParseElement(XElement.Parse(grammar), learner, converters));
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00018804 File Offset: 0x00016A04
		internal static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> ParseElement(XElement element, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters = null)
		{
			if (element == null)
			{
				return null;
			}
			XAttribute xattribute = element.Attribute("nullable");
			bool flag = xattribute != null && xattribute.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
			string text = element.Name.ToString().ToLowerInvariant();
			XAttribute xattribute2 = element.Attribute("name");
			if (xattribute2 == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Element {0} must have a name.", new object[] { text })));
			}
			string value = xattribute2.Value;
			if (text == "field")
			{
				XAttribute xattribute3 = element.Attribute("type");
				string text2 = ((xattribute3 != null) ? xattribute3.Value : null) ?? string.Empty;
				return new FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(value, text2, flag, learner);
			}
			if (!(text == "sequence"))
			{
				if (!(text == "struct"))
				{
					if (!(text == "union"))
					{
						if (!(text == "convert"))
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Unknown element type {0}.", new object[] { value })));
						}
						if (element.Parent == null || !element.Parent.Name.ToString().Equals("struct", StringComparison.OrdinalIgnoreCase))
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Convert {0} must be directly nested inside a struct.", new object[] { value })));
						}
						if (element.Elements().Count<XElement>() > 1)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Convert {0} must have exactly zero or one child.", new object[] { value })));
						}
						XAttribute xattribute4 = element.Attribute("converterName");
						string converterName = ((xattribute4 != null) ? xattribute4.Value : null) ?? "";
						ConvertSchemaElementInterface[] converters2 = converters;
						ConvertSchemaElementFactoryGeneric2<TSequenceProgram, TRegionProgram, TRegion> convertSchemaElementFactoryGeneric = ((converters2 != null) ? converters2.FirstOrDefault((ConvertSchemaElementInterface c) => c.ConverterName == converterName) : null) as ConvertSchemaElementFactoryGeneric2<TSequenceProgram, TRegionProgram, TRegion>;
						if (convertSchemaElementFactoryGeneric == null)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Convert {0} refers to unexisting converter named '{1}'.", new object[] { value, converterName })));
						}
						XElement xelement = element.Elements().FirstOrDefault<XElement>();
						return convertSchemaElementFactoryGeneric.CreateAndParseChildGrammar(value, xelement, flag, learner, converters);
					}
					else
					{
						if (element.Parent == null || !element.Parent.Name.ToString().Equals("struct", StringComparison.OrdinalIgnoreCase))
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Union {0} must be directly nested inside a struct.", new object[] { value })));
						}
						if (element.Elements().Count<XElement>() < 2)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Union {0} must have at least two children.", new object[] { value })));
						}
						List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> list = (from x in element.Elements()
							select SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.ParseElement(x, learner, converters)).ToList<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
						return new UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(value, list, flag, learner);
					}
				}
				else
				{
					if (!element.Elements().Any<XElement>())
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Struct {0} must have at least one child.", new object[] { value })));
					}
					List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> list2 = (from x in element.Elements()
						select SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.ParseElement(x, learner, converters)).ToList<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
					return new StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(value, list2, flag, learner);
				}
			}
			else
			{
				if (element.Elements().Count<XElement>() != 1)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Sequence {0} must have exactly one child.", new object[] { value })));
				}
				XElement xelement2 = element.Elements().First<XElement>();
				if (xelement2.Name.ToString().Equals("sequence", StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Schema grammar is invalid. Sequence {0} should not contain sequence {1} directly.", new object[] { value, xelement2.Name })));
				}
				return new SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(value, SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.ParseElement(xelement2, learner, converters), flag, learner);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00018C0A File Offset: 0x00016E0A
		public TreeElement<TRegion> RunTree(TRegion s)
		{
			TreeElement<TRegion> treeElement = this.Root.RunTree(s);
			this.Root.ResetExecutionCacheAndChildren();
			return treeElement;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00018C23 File Offset: 0x00016E23
		public static bool Verify(string grammar, ConvertSchemaElementInterface[] converters = null)
		{
			return SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.Load(grammar, null, converters) != null;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00018C30 File Offset: 0x00016E30
		public static void Load(string grammar)
		{
			SchemaGrammar<TSequenceProgram, TRegionProgram, TRegion>.Load(grammar, null, null);
		}
	}
}
