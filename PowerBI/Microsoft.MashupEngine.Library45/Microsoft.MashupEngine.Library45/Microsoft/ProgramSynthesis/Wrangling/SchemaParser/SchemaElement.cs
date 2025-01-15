using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000150 RID: 336
	public abstract class SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000172C2 File Offset: 0x000154C2
		protected IDictionary<Tuple<TRegion, int>, IEnumerable<TRegion>> ExecutionCache { get; } = new Dictionary<Tuple<TRegion, int>, IEnumerable<TRegion>>();

		// Token: 0x0600077B RID: 1915 RVA: 0x000172CC File Offset: 0x000154CC
		protected SchemaElement(string name, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
		{
			this.Name = name;
			this.IsNullable = nullable;
			this.Parent = parent;
			this.Learner = learner;
			SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement = this;
			while (schemaElement != null)
			{
				schemaElement = schemaElement.Parent;
				if (schemaElement is BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> || schemaElement is UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
				{
					break;
				}
				if (schemaElement is SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
				{
					schemaElement = schemaElement.Parent;
					break;
				}
				if (schemaElement is StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> && schemaElement.Programs.IsAny<IExtractionProgram<TRegion>>())
				{
					break;
				}
			}
			this.ReferencedElement = BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Instance;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00017356 File Offset: 0x00015556
		protected SchemaElement()
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00017369 File Offset: 0x00015569
		private ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> Learner { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00017371 File Offset: 0x00015571
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00017379 File Offset: 0x00015579
		public SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Parent { get; internal set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00017382 File Offset: 0x00015582
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001738A File Offset: 0x0001558A
		public string Name { get; protected set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00017393 File Offset: 0x00015593
		public bool IsNullable { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000783 RID: 1923
		public abstract List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001739B File Offset: 0x0001559B
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x000173A3 File Offset: 0x000155A3
		public SchemaElement<TSequenceProgram, TRegionProgram, TRegion> ReferencedElement { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000173AC File Offset: 0x000155AC
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x000173B4 File Offset: 0x000155B4
		public ExtractionKind ExtractionKind { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000173BD File Offset: 0x000155BD
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000173C5 File Offset: 0x000155C5
		public IReadOnlyCollection<IExtractionProgram<TRegion>> Programs { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000173CE File Offset: 0x000155CE
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000173D6 File Offset: 0x000155D6
		public ProgramSet AllPrograms { get; private set; }

		// Token: 0x0600078C RID: 1932
		public abstract void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element);

		// Token: 0x0600078D RID: 1933 RVA: 0x000173E0 File Offset: 0x000155E0
		public void AddExecutionCache(TRegion s, int k, IEnumerable<TRegion> value)
		{
			Tuple<TRegion, int> tuple = Tuple.Create<TRegion, int>(s, k);
			this.ExecutionCache[tuple] = value;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00017402 File Offset: 0x00015602
		public void ResetExecutionCache()
		{
			this.ExecutionCache.Clear();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00017410 File Offset: 0x00015610
		public virtual void ResetExecutionCacheAndChildren()
		{
			this.ResetExecutionCache();
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in this.Children)
			{
				schemaElement.ResetExecutionCacheAndChildren();
			}
		}

		// Token: 0x06000790 RID: 1936
		public abstract T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor);

		// Token: 0x06000791 RID: 1937 RVA: 0x00017468 File Offset: 0x00015668
		public static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Parse(XElement element, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters = null, ASTSerializationFormat formatPrograms = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext), Func<XElement, ProgramSet> getProgramSet = null)
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse) == null)
			{
				programNodeParser = (SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Parse(element, parent, learner, converters, formatPrograms, programNodeParser, context, getProgramSet);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00017494 File Offset: 0x00015694
		public static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Parse(XElement element, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters, ASTSerializationFormat formatPrograms, ProgramNodeParser programNodeParser, DeserializationContext context = default(DeserializationContext), Func<XElement, ProgramSet> getProgramSet = null)
		{
			if (element == null)
			{
				return null;
			}
			XAttribute xattribute = element.Attribute("name");
			string text = ((xattribute != null) ? xattribute.Value : null) ?? "";
			XAttribute xattribute2 = element.Attribute("nullable");
			bool flag = xattribute2 != null && xattribute2.Value == "true";
			XElement xelement = element.Elements("program").FirstOrDefault<XElement>();
			SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement = null;
			XElement xelement2 = element.Elements("SequenceProgram").FirstOrDefault<XElement>();
			XElement xelement3 = element.Elements("SubstringProgram").FirstOrDefault<XElement>();
			if (element.Name == "field")
			{
				schemaElement = new FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(text, parent, flag, learner);
				IExtractionProgram<TRegion> extractionProgram;
				if (xelement2 != null)
				{
					schemaElement.ExtractionKind = ExtractionKind.Sequence;
					xelement = xelement2;
					extractionProgram = learner.Loader.Load(xelement.ToString(), formatPrograms, context, programNodeParser);
				}
				else
				{
					if (xelement3 == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program kind! Expected 'SequenceProgram' or 'SubstringProgram', but found {0}.", new object[] { schemaElement.ExtractionKind })));
					}
					schemaElement.ExtractionKind = ExtractionKind.Region;
					xelement = xelement3;
					extractionProgram = learner.Loader.Load(xelement.ToString(), formatPrograms, context, programNodeParser);
				}
				schemaElement.Programs = new IExtractionProgram<TRegion>[] { extractionProgram };
			}
			else if (element.Name == "convert")
			{
				XAttribute xattribute3 = element.Attribute("converterName");
				string converterName = ((xattribute3 != null) ? xattribute3.Value : null) ?? "";
				ConvertSchemaElementFactoryGeneric2<TSequenceProgram, TRegionProgram, TRegion> convertSchemaElementFactoryGeneric = ((converters != null) ? converters.FirstOrDefault((ConvertSchemaElementInterface c) => c.ConverterName == converterName) : null) as ConvertSchemaElementFactoryGeneric2<TSequenceProgram, TRegionProgram, TRegion>;
				XElement xelement4 = element.Elements().FirstOrDefault((XElement elem) => elem.Name != "SubstringProgram" && elem.Name != "SequenceProgram" && elem.Name != "UnionProgram" && elem.Name != "program");
				if (convertSchemaElementFactoryGeneric != null && xelement4 != null)
				{
					schemaElement = convertSchemaElementFactoryGeneric.CreateAndParseChild(text, parent, flag, learner, xelement4, converters, formatPrograms, context);
				}
				else if (convertSchemaElementFactoryGeneric != null)
				{
					schemaElement = convertSchemaElementFactoryGeneric.Create(text, parent, flag, learner);
				}
			}
			else
			{
				if (element.Name == "struct")
				{
					schemaElement = new StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(text, parent, flag, learner);
					if (xelement2 != null)
					{
						schemaElement.ExtractionKind = ExtractionKind.Sequence;
						xelement = xelement2;
					}
					else
					{
						if (xelement3 == null)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid program kind! Expected 'SequenceProgram' or 'SubstringProgram', but found {0}.", new object[] { schemaElement.ExtractionKind })));
						}
						schemaElement.ExtractionKind = ExtractionKind.Region;
						xelement = xelement3;
					}
				}
				else if (element.Name == "sequence")
				{
					schemaElement = new SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(parent, text, flag, learner);
					XElement xelement5;
					if ((xelement5 = xelement3) == null)
					{
						xelement5 = xelement2 ?? xelement;
					}
					xelement = xelement5;
				}
				else
				{
					if (!(element.Name == "union"))
					{
						string text2 = "Invalid program name ";
						XName name = element.Name;
						throw new InvalidOperationException(text2 + ((name != null) ? name.ToString() : null) + ". Expected union, convert, field, sequence or struct");
					}
					schemaElement = new UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion>(text, parent, flag, learner);
					xelement = element.Elements("UnionProgram").FirstOrDefault<XElement>() ?? xelement;
				}
				SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement2 = schemaElement;
				IExtractionProgram<TRegion>[] array;
				if (xelement == null || !(xelement.Value.Trim() != ""))
				{
					array = new IExtractionProgram<TRegion>[0];
				}
				else
				{
					(array = new IExtractionProgram<TRegion>[1])[0] = learner.Loader.Load(xelement.ToString(), formatPrograms, context, programNodeParser);
				}
				schemaElement2.Programs = array;
				foreach (XElement xelement6 in from elem in element.Elements()
					where elem.Name != "SubstringProgram" && elem.Name != "SequenceProgram" && elem.Name != "UnionProgram" && elem.Name != "program"
					select elem)
				{
					schemaElement.AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Parse(xelement6, schemaElement, learner, converters, formatPrograms, programNodeParser, context, getProgramSet));
				}
			}
			if (getProgramSet != null && schemaElement != null)
			{
				schemaElement.AllPrograms = getProgramSet(element);
			}
			return schemaElement;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000178B8 File Offset: 0x00015AB8
		public static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Parse(string code, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters = null, ASTSerializationFormat formatPrograms = ASTSerializationFormat.XML, DeserializationContext context = default(DeserializationContext), Func<XElement, ProgramSet> getProgramSet = null)
		{
			ProgramNodeParser programNodeParser;
			if ((programNodeParser = SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse) == null)
			{
				programNodeParser = (SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.<>O.<0>__Parse = new ProgramNodeParser(ProgramNode.Parse));
			}
			return SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Parse(code, learner, converters, formatPrograms, programNodeParser, context, getProgramSet);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000178E4 File Offset: 0x00015AE4
		public static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> Parse(string code, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner, ConvertSchemaElementInterface[] converters, ASTSerializationFormat formatPrograms, ProgramNodeParser programNodeParser, DeserializationContext context = default(DeserializationContext), Func<XElement, ProgramSet> getProgramSet = null)
		{
			XElement xelement;
			try
			{
				xelement = XElement.Parse(code);
			}
			catch (XmlException ex)
			{
				throw new InvalidOperationException("Invalid program text!", ex);
			}
			return SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Parse(xelement, null, learner, converters, formatPrograms, programNodeParser, context, getProgramSet);
		}

		// Token: 0x06000795 RID: 1941
		public abstract void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false);

		// Token: 0x06000796 RID: 1942 RVA: 0x00017928 File Offset: 0x00015B28
		public void ComputeReferencedElement()
		{
			SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement = this;
			bool flag = false;
			for (;;)
			{
				schemaElement = schemaElement.Parent;
				if (schemaElement is BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion> || schemaElement is UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion> || schemaElement == null)
				{
					goto IL_00C6;
				}
				if (schemaElement is SequenceSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
				{
					break;
				}
				if (schemaElement is StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion> && (schemaElement.Programs.IsAny<IExtractionProgram<TRegion>>() || (schemaElement.Children[0] != this && schemaElement.Children[0].Programs.IsAny<IExtractionProgram<TRegion>>())))
				{
					goto IL_00C6;
				}
			}
			bool flag2 = (this is FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion> && this == this.Parent.Children.FirstOrDefault<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>()) || this is StructSchemaElement<TSequenceProgram, TRegionProgram, TRegion>;
			flag = !schemaElement.Programs.IsAny<IExtractionProgram<TRegion>>() && flag2;
			schemaElement = (flag2 ? schemaElement.Parent : schemaElement.Children.FirstOrDefault<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>());
			IL_00C6:
			if (schemaElement == null)
			{
				schemaElement = BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>.Instance;
			}
			this.ReferencedElement = schemaElement;
			this.ExtractionKind = (flag ? ExtractionKind.Sequence : ExtractionKind.Region);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00017A18 File Offset: 0x00015C18
		private IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>>> GetRegionConstraints(IEnumerable<DocumentSpecInterface> specs)
		{
			List<Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>>> list = new List<Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>>>();
			foreach (DocumentSpecInterface documentSpecInterface in specs)
			{
				if (documentSpecInterface is DocumentSpec<TRegion>)
				{
					DocumentSpec<TRegion> documentSpec = documentSpecInterface as DocumentSpec<TRegion>;
					TRegion document = documentSpec.Document;
					IEnumerable<TRegion> enumerable = this.TopLevelRunOfReferencedElement(document).ToList<TRegion>();
					IEnumerable<TRegion> fieldPositiveExamples;
					if (documentSpec.PositiveExamples.TryGetValue(this.Name, out fieldPositiveExamples) && fieldPositiveExamples.Any<TRegion>() && enumerable != null)
					{
						list.AddRange(enumerable.SelectMany((TRegion refRegion) => from positive in fieldPositiveExamples
							where positive.IntersectNonEmpty(refRegion)
							select this.Learner.Region.BuildPositiveConstraint(Seq.Of<TRegion>(new TRegion[] { refRegion }), Seq.Of<TRegion>(new TRegion[] { positive }), false)));
						IEnumerable<TRegion> fieldNegativeExamples;
						if (documentSpec.NegativeExamples.TryGetValue(this.Name, out fieldNegativeExamples))
						{
							list.AddRange(enumerable.SelectMany((TRegion refRegion) => from negative in fieldNegativeExamples
								where negative.IntersectNonEmpty(refRegion)
								select this.Learner.Region.BuildNegativeConstraint(Seq.Of<TRegion>(new TRegion[] { refRegion }), Seq.Of<TRegion>(new TRegion[] { negative }), false)));
						}
					}
				}
			}
			if (!list.OfType<CorrespondingMemberEquals<TRegion, TRegion>>().IsAny<CorrespondingMemberEquals<TRegion, TRegion>>())
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00017B24 File Offset: 0x00015D24
		private IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> GetSequenceConstraints(IEnumerable<DocumentSpecInterface> specs)
		{
			List<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> list = new List<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>>();
			bool flag = false;
			foreach (DocumentSpecInterface documentSpecInterface in specs)
			{
				if (documentSpecInterface is DocumentSpec<TRegion>)
				{
					DocumentSpec<TRegion> documentSpec = documentSpecInterface as DocumentSpec<TRegion>;
					TRegion document = documentSpec.Document;
					IEnumerable<TRegion> enumerable = this.TopLevelRunOfReferencedElement(document).ToList<TRegion>();
					IEnumerable<TRegion> fieldPositiveExamples;
					if (documentSpec.PositiveExamples.TryGetValue(this.Name, out fieldPositiveExamples) && fieldPositiveExamples.Any<TRegion>() && enumerable != null)
					{
						List<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> list2 = (from refRegion in enumerable
							select this.Learner.Sequence.BuildPositiveConstraint(Seq.Of<TRegion>(new TRegion[] { refRegion }), Seq.Of<List<TRegion>>(new List<TRegion>[] { fieldPositiveExamples.Where((TRegion positive) => positive.IntersectNonEmpty(refRegion)).ToList<TRegion>() }), false) into constraint
							where constraint != null
							select constraint).ToList<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>>();
						list.AddRange(list2);
						flag |= list2.Any<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>>();
						IEnumerable<TRegion> fieldNegativeExamples;
						if (documentSpec.NegativeExamples.TryGetValue(this.Name, out fieldNegativeExamples))
						{
							list.AddRange(enumerable.SelectMany((TRegion refRegion) => this.Learner.BuildNegativeSequenceConstraints(refRegion, fieldNegativeExamples.Where((TRegion negative) => negative.IntersectNonEmpty(refRegion)))));
						}
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00017C7C File Offset: 0x00015E7C
		public IEnumerable<SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>> LearnElementField(IEnumerable<DocumentSpecInterface> specs, int k, bool learnAll)
		{
			IEnumerable<SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>> enumerable2;
			try
			{
				IEnumerable<IExtractionProgram<TRegion>> enumerable;
				if (learnAll)
				{
					ProgramSet programSet;
					if (this.ExtractionKind == ExtractionKind.Sequence)
					{
						IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> sequenceConstraints = this.GetSequenceConstraints(specs);
						if (sequenceConstraints == null)
						{
							return new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
						}
						programSet = this.Learner.Sequence.LearnAll(sequenceConstraints, null, default(CancellationToken));
					}
					else
					{
						IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>>> regionConstraints = this.GetRegionConstraints(specs);
						if (regionConstraints == null)
						{
							return new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
						}
						programSet = this.Learner.Region.LearnAll(regionConstraints, null, default(CancellationToken));
					}
					ProgramSet programSet2 = programSet;
					if (this.AllPrograms != null)
					{
						programSet2 = this.AllPrograms.Intersect(programSet2);
					}
					this.AllPrograms = programSet2;
					enumerable = from programNode in programSet2.TopK(this.Learner.ScoreFeature, k, null, null)
						select this.Learner.Loader.CreateProgram(programNode, this.ExtractionKind, ReferenceKind.Parent, new double?(0.0));
				}
				else if (this.ExtractionKind == ExtractionKind.Sequence)
				{
					IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>> sequenceConstraints2 = this.GetSequenceConstraints(specs);
					if (sequenceConstraints2 == null)
					{
						return new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
					}
					enumerable = this.Learner.Sequence.LearnTopK(sequenceConstraints2, k, null, default(CancellationToken));
				}
				else
				{
					IReadOnlyList<Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>>> regionConstraints2 = this.GetRegionConstraints(specs);
					if (regionConstraints2 == null)
					{
						return new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
					}
					enumerable = this.Learner.Region.LearnTopK(regionConstraints2, k, null, default(CancellationToken));
				}
				IExtractionProgram<TRegion>[] array = (enumerable as IExtractionProgram<TRegion>[]) ?? enumerable.ToArray<IExtractionProgram<TRegion>>();
				if (!array.IsAny<IExtractionProgram<TRegion>>())
				{
					enumerable2 = new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
				}
				else
				{
					this.Programs = array;
					enumerable2 = this.Programs.Select((IExtractionProgram<TRegion> p, int i) => new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>(this, i));
				}
			}
			catch (Exception)
			{
				enumerable2 = new SchemaElementProgram<TSequenceProgram, TRegionProgram, TRegion>[0];
			}
			return enumerable2;
		}

		// Token: 0x0600079A RID: 1946
		public abstract TreeElement<TRegion> RunTree(TRegion s);

		// Token: 0x0600079B RID: 1947 RVA: 0x00017E38 File Offset: 0x00016038
		private IEnumerable<TRegion> TopLevelRunOfReferencedElement(TRegion str)
		{
			if (this.ReferencedElement == null)
			{
				return new TRegion[] { str };
			}
			return this.ReferencedElement.TopLevelRun(str);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00017E6C File Offset: 0x0001606C
		public IEnumerable<TRegion> TopLevelRun(TRegion str)
		{
			if (this is BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
			{
				return new TRegion[] { str };
			}
			return from absoluteParentRun in this.TopLevelRunOfReferencedElement(str)
				where absoluteParentRun != null
				from absoluteRun in this.Run(absoluteParentRun, 0)
				where absoluteRun != null
				select absoluteRun;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00017F34 File Offset: 0x00016134
		public IEnumerable<InputOutputs<TRegion>> TopLevelRunPlusParent(TRegion inputRegion)
		{
			if (this is BotSchemaElement<TSequenceProgram, TRegionProgram, TRegion>)
			{
				return new InputOutputs<TRegion>(default(TRegion), null, inputRegion.Yield<TRegion>()).Yield<InputOutputs<TRegion>>();
			}
			return (from absoluteParentRun in this.TopLevelRunOfReferencedElement(inputRegion)
				where absoluteParentRun != null
				select absoluteParentRun).Select(delegate(TRegion absoluteParentRun)
			{
				SchemaElement<TSequenceProgram, TRegionProgram, TRegion> referencedElement = this.ReferencedElement;
				return new InputOutputs<TRegion>(absoluteParentRun, (referencedElement != null) ? referencedElement.Name : null, from absoluteRun in this.Run(absoluteParentRun, 0)
					where absoluteRun != null
					select absoluteRun);
			});
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00017FA0 File Offset: 0x000161A0
		public IEnumerable<TRegion> Run(TRegion s, int k = 0)
		{
			IEnumerable<TRegion> enumerable;
			if (!this.ExecutionCache.TryGetValue(Tuple.Create<TRegion, int>(s, k), out enumerable))
			{
				return this.RunWithoutCache(s, k);
			}
			return enumerable;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00017FD0 File Offset: 0x000161D0
		protected virtual IEnumerable<TRegion> RunWithoutCache(TRegion s, int k)
		{
			if (this.Programs == null || this.Programs.Count <= k)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Execution fails. Element {0} does not have at least {1} programs.", new object[]
				{
					this.Name,
					k + 1
				})));
			}
			if (!this.Programs.IsAny<IExtractionProgram<TRegion>>() || this.Programs.ElementAt(k).ProgramNode is Hole)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Execution fails. Element {0} does not have a valid program.", new object[] { this.Name })));
			}
			IEnumerable<IEnumerable<TRegion>> enumerable = this.Programs.ElementAt(k).RunExtraction(Seq.Of<TRegion>(new TRegion[] { s }));
			IEnumerable<TRegion> enumerable2 = ((enumerable != null) ? enumerable.SingleOrDefault<IEnumerable<TRegion>>() : null);
			TRegion[] array;
			if ((array = enumerable2 as TRegion[]) == null)
			{
				array = ((enumerable2 != null) ? enumerable2.ToArray<TRegion>() : null) ?? new TRegion[0];
			}
			TRegion[] array2 = array;
			array2 = this.RemoveIntersectingRegions(array2);
			if (array2.IsAny<TRegion>())
			{
				this.ExecutionCache[Tuple.Create<TRegion, int>(s, k)] = array2;
			}
			return array2;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000E945 File Offset: 0x0000CB45
		protected virtual TRegion[] RemoveIntersectingRegions(TRegion[] eleRunArray)
		{
			return eleRunArray;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000180E1 File Offset: 0x000162E1
		public override string ToString()
		{
			return this.Serialize(ASTSerializationFormat.HumanReadable);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000180EA File Offset: 0x000162EA
		public virtual string Serialize(ASTSerializationFormat format = ASTSerializationFormat.XML)
		{
			if (format == ASTSerializationFormat.XML)
			{
				return this.Serialize(SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.XMLSchemaPrintVisitor);
			}
			if (format == ASTSerializationFormat.HumanReadable)
			{
				return this.Serialize(SchemaElement<TSequenceProgram, TRegionProgram, TRegion>.HumanReadableSchemaPrintVisitor);
			}
			throw new ArgumentOutOfRangeException("format");
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00018115 File Offset: 0x00016315
		public string Serialize(SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> schemaPrintVisitor)
		{
			return this.AcceptVisitor<string>(schemaPrintVisitor);
		}

		// Token: 0x04000344 RID: 836
		private static readonly SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> HumanReadableSchemaPrintVisitor = new HumanReadableSchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion>();

		// Token: 0x04000345 RID: 837
		private static readonly SchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion> XMLSchemaPrintVisitor = new XmlSchemaPrintVisitor<TSequenceProgram, TRegionProgram, TRegion>();

		// Token: 0x02000151 RID: 337
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400034F RID: 847
			public static ProgramNodeParser <0>__Parse;
		}
	}
}
