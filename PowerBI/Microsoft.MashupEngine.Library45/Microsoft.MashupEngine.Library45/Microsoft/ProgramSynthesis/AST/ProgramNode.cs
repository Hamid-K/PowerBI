using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST.Visitors;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008D8 RID: 2264
	public abstract class ProgramNode : IEquatable<ProgramNode>
	{
		// Token: 0x060030B6 RID: 12470 RVA: 0x0008F9FB File Offset: 0x0008DBFB
		protected ProgramNode()
		{
			this.Id = Interlocked.Increment(ref ProgramNode._highestId);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0008FA27 File Offset: 0x0008DC27
		internal ProgramNode(int id)
		{
			this.Id = id;
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x0008FA4A File Offset: 0x0008DC4A
		public int Id { get; }

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060030B9 RID: 12473
		public abstract ProgramNode[] Children { get; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x0008FA52 File Offset: 0x0008DC52
		public IEnumerable<ProgramNode> SubPrograms
		{
			get
			{
				return this.Yield<ProgramNode>().Concat(this.Children.SelectMany((ProgramNode c) => c.SubPrograms));
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060030BB RID: 12475
		public abstract Symbol Symbol { get; }

		// Token: 0x060030BC RID: 12476
		protected abstract object Evaluate(State state);

		// Token: 0x060030BD RID: 12477 RVA: 0x0008FA8C File Offset: 0x0008DC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public object Invoke(State state)
		{
			object obj = this.Evaluate(state);
			EventHandler<EvaluationFinishedEventArgs> onEvaluationFinished = this.OnEvaluationFinished;
			if (onEvaluationFinished != null)
			{
				onEvaluationFinished(this, new EvaluationFinishedEventArgs(this, state, obj));
			}
			return obj;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060030BE RID: 12478 RVA: 0x0008FABC File Offset: 0x0008DCBC
		// (remove) Token: 0x060030BF RID: 12479 RVA: 0x0008FAF4 File Offset: 0x0008DCF4
		public event EventHandler<EvaluationFinishedEventArgs> OnEvaluationFinished;

		// Token: 0x060030C0 RID: 12480
		public abstract ProgramNode Clone();

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x0008FB29 File Offset: 0x0008DD29
		public IEnumerable<Record<ProgramNode, int, Hole>> Holes
		{
			get
			{
				int num;
				for (int i = 0; i < this.Children.Length; i = num + 1)
				{
					ProgramNode programNode = this.Children[i];
					if (programNode is Hole)
					{
						yield return Record.Create<ProgramNode, int, Hole>(this, i, programNode as Hole);
					}
					else
					{
						foreach (Record<ProgramNode, int, Hole> record in programNode.Holes)
						{
							yield return record;
						}
						IEnumerator<Record<ProgramNode, int, Hole>> enumerator = null;
					}
					num = i;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x0008FB39 File Offset: 0x0008DD39
		public Grammar Grammar
		{
			get
			{
				return this.Symbol.Grammar;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0008FB46 File Offset: 0x0008DD46
		// (set) Token: 0x060030C4 RID: 12484 RVA: 0x0008FB4E File Offset: 0x0008DD4E
		public object Metadata { get; set; }

		// Token: 0x060030C5 RID: 12485 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(ProgramNode left, ProgramNode right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(ProgramNode left, ProgramNode right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0008FB57 File Offset: 0x0008DD57
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ProgramNode)obj)));
		}

		// Token: 0x060030C8 RID: 12488
		public abstract bool Equals(ProgramNode other);

		// Token: 0x060030C9 RID: 12489
		public abstract override int GetHashCode();

		// Token: 0x060030CA RID: 12490 RVA: 0x0008FB85 File Offset: 0x0008DD85
		public void ClearFeatureCache()
		{
			this._featureCache.Clear();
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0008FB94 File Offset: 0x0008DD94
		public object GetFeatureValue(IFeature feature, LearningInfo learningInfo = null)
		{
			if (this is VariableNode)
			{
				return feature.Calculate(this, (learningInfo != null) ? learningInfo.WithProgramNode(this) : null);
			}
			ProgramNode.FeatureAndContext featureAndContext = new ProgramNode.FeatureAndContext(feature, (learningInfo != null) ? learningInfo.FeatureCalculationContext : null);
			return this._featureCache.LookupOrCompute(featureAndContext, delegate(ProgramNode.FeatureAndContext t)
			{
				IFeature feature2 = t.Feature;
				FeatureCalculationContext context = t.Context;
				return feature2.Calculate(this, (context != null) ? context.WithProgramNode(this) : null);
			});
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0008FBEC File Offset: 0x0008DDEC
		internal object GetFeatureValue(IFeature feature, FeatureCalculationContext context)
		{
			if (this is VariableNode)
			{
				return feature.Calculate(this, (context != null) ? context.WithProgramNode(this) : null);
			}
			ProgramNode.FeatureAndContext featureAndContext = new ProgramNode.FeatureAndContext(feature, context);
			return this._featureCache.LookupOrCompute(featureAndContext, delegate(ProgramNode.FeatureAndContext t)
			{
				IFeature feature2 = t.Feature;
				FeatureCalculationContext context2 = t.Context;
				return feature2.Calculate(this, (context2 != null) ? context2.WithProgramNode(this) : null);
			});
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0008FC36 File Offset: 0x0008DE36
		public T GetFeatureValue<T>(Feature<T> feature, LearningInfo learningInfo = null)
		{
			return (T)((object)this.GetFeatureValue(feature, learningInfo));
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0008FC45 File Offset: 0x0008DE45
		internal T GetFeatureValue<T>(Feature<T> feature, FeatureCalculationContext context)
		{
			return this.GetFeatureValue<T>(feature, (context != null) ? context.WithProgramNode(this) : null);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0008FC5B File Offset: 0x0008DE5B
		public string PrintAST(ASTSerializationFormat format = ASTSerializationFormat.XML)
		{
			return this.PrintAST(format.AsASTSerializationSettings());
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0008FC6C File Offset: 0x0008DE6C
		public string PrintAST(ASTSerializationSettings settings)
		{
			if (settings.HasHumanReadable)
			{
				HumanReadablePrintVisitor humanReadablePrintVisitor = new HumanReadablePrintVisitor(settings);
				return this.AcceptVisitor<CodeBuilder>(humanReadablePrintVisitor).GetCode();
			}
			XElement xelement = this.AcceptVisitor<XElement>(new XmlPrintVisitor(settings));
			return this.XmlToString(xelement, settings);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0008FCAC File Offset: 0x0008DEAC
		private string XmlToString(XElement xml, ASTSerializationSettings settings)
		{
			if (xml == null)
			{
				return null;
			}
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = settings.HasIndent,
				OmitXmlDeclaration = true
			};
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					xml.WriteTo(xmlWriter);
					xmlWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0008FD3C File Offset: 0x0008DF3C
		internal XElement ToInternedXML(Dictionary<object, int> identityCache)
		{
			return this.AcceptVisitor<XElement>(new InternedXmlPrintVisitor(identityCache));
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0008FD4C File Offset: 0x0008DF4C
		protected static ProgramNode TryResolveReference(XElement referenceNode, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (referenceNode.Name.LocalName != "Reference")
			{
				return null;
			}
			if (identityCache == null)
			{
				throw new ArgumentException("Encountered a reference node during program deserialization, but no identity cache was provided");
			}
			int num;
			object obj;
			if (int.TryParse(referenceNode.Value, out num) && identityCache.TryGetValue(num, out obj))
			{
				ProgramNode programNode = obj as ProgramNode;
				if (programNode != null)
				{
					return programNode;
				}
			}
			throw new ArgumentException("Invalid XML encountered during FromInternedXML().");
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0008FDB0 File Offset: 0x0008DFB0
		private static ProgramNode ReturnInternedResult(ProgramNode result, XElement node, Dictionary<int, object> identityCache)
		{
			XAttribute xattribute = node.Attribute("ObjectID");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				return result;
			}
			if (identityCache == null)
			{
				throw new ArgumentException("Encountered object id without identity cache during program deserialization.");
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				throw new ArgumentException("Invalid XML encountered during FromInternedXML().");
			}
			identityCache[num] = result;
			return result;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0008FE0C File Offset: 0x0008E00C
		internal static ProgramNode FromInternedXML(XElement node, Grammar grammar, Dictionary<int, object> identityCache)
		{
			ProgramNode programNode = ProgramNode.TryResolveReference(node, grammar, identityCache);
			if (programNode != null)
			{
				return programNode;
			}
			XAttribute xattribute = node.Attribute("symbol");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				throw new ArgumentException("Invalid XML encountered during FromInternedXML().");
			}
			Symbol symbol = grammar.Symbol(text);
			if (symbol == null)
			{
				throw new ArgumentException("Invalid XML encountered during FromInternedXML().");
			}
			return ProgramNode.ParseXML(symbol, node, default(ParseSettings), identityCache);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0008FE79 File Offset: 0x0008E079
		public override string ToString()
		{
			return this.PrintAST(ASTSerializationFormat.HumanReadable);
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x00002188 File Offset: 0x00000388
		public virtual GrammarRule GrammarRule
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060030D8 RID: 12504
		public abstract T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor);

		// Token: 0x060030D9 RID: 12505
		public abstract TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args);

		// Token: 0x060030DA RID: 12506 RVA: 0x0008FE84 File Offset: 0x0008E084
		public static ProgramNode Parse(string ast, Symbol symbol, ASTSerializationFormat format = ASTSerializationFormat.XML, ParseSettings settings = default(ParseSettings))
		{
			if (string.IsNullOrWhiteSpace(ast) || symbol == null)
			{
				return null;
			}
			if (format != ASTSerializationFormat.XML)
			{
				if (format != ASTSerializationFormat.HumanReadable)
				{
					throw new ArgumentOutOfRangeException("format");
				}
			}
			else
			{
				try
				{
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
					{
						CheckCharacters = false,
						DtdProcessing = DtdProcessing.Prohibit,
						XmlResolver = null
					};
					using (StringReader stringReader = new StringReader(ast))
					{
						using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
						{
							XElement xelement = XElement.Load(xmlReader);
							return ProgramNode.ParseXML(symbol, xelement, settings, null);
						}
					}
				}
				catch (Exception)
				{
					return null;
				}
			}
			throw new NotSupportedException("Deserializing human-readable ASTs is not supported.");
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0008FF48 File Offset: 0x0008E148
		public static ProgramNode ParseXML(Grammar grammar, XElement node, ParseSettings settings = default(ParseSettings))
		{
			return ProgramNode.ParseXML(grammar.StartSymbol, node, settings, null);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0008FF58 File Offset: 0x0008E158
		internal static ProgramNode ParseXML(Symbol symbol, XElement node, ParseSettings settings = default(ParseSettings), Dictionary<int, object> identityCache = null)
		{
			string text;
			if (node == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = node.Attribute("symbol");
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			if (text != symbol.Name)
			{
				return null;
			}
			Grammar grammar = symbol.Grammar.Clone();
			ImmutableStack<ScopeElement> immutableStack = ImmutableStack.Create<ScopeElement>(ScopeElement.Define(grammar.InputSymbol));
			ParseContext parseContext = new ParseContext(grammar, settings, immutableStack, identityCache);
			return ProgramNode.ParseXML(node, null, parseContext);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0008FFC6 File Offset: 0x0008E1C6
		public static ProgramNode Parse(string ast, Grammar grammar, ASTSerializationFormat format = ASTSerializationFormat.XML, ParseSettings settings = default(ParseSettings))
		{
			return ProgramNode.Parse(ast, grammar.StartSymbol, format, settings);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0008FFD8 File Offset: 0x0008E1D8
		internal static ProgramNode ParseXML(XElement node, Type expectedType, ParseContext context)
		{
			string text = node.Name.ToString();
			if (text != null)
			{
				ProgramNode programNode;
				switch (text.Length)
				{
				case 4:
					if (!(text == "Hole"))
					{
						goto IL_0116;
					}
					programNode = Hole.ParseXML(node, expectedType, context);
					break;
				case 5:
				case 6:
				case 8:
				case 13:
				case 14:
					goto IL_0116;
				case 7:
					if (!(text == "LetNode"))
					{
						goto IL_0116;
					}
					programNode = LetNode.ParseXML(node, expectedType, context);
					break;
				case 9:
					if (!(text == "Reference"))
					{
						goto IL_0116;
					}
					return ProgramNode.TryResolveReference(node, context.Grammar, context.IdentityCache);
				case 10:
					if (!(text == "LambdaNode"))
					{
						goto IL_0116;
					}
					return null;
				case 11:
					if (!(text == "LiteralNode"))
					{
						goto IL_0116;
					}
					programNode = LiteralNode.ParseXML(node, expectedType, context);
					break;
				case 12:
					if (!(text == "VariableNode"))
					{
						goto IL_0116;
					}
					programNode = VariableNode.ParseXML(node, expectedType, context);
					break;
				case 15:
					if (!(text == "NonterminalNode"))
					{
						goto IL_0116;
					}
					programNode = NonterminalNode.ParseXML(node, expectedType, context);
					break;
				default:
					goto IL_0116;
				}
				return ProgramNode.ReturnInternedResult(programNode, node, context.IdentityCache);
			}
			IL_0116:
			return null;
		}

		// Token: 0x04001873 RID: 6259
		private static int _highestId;

		// Token: 0x04001877 RID: 6263
		public const int FeatureCacheSize = 64;

		// Token: 0x04001878 RID: 6264
		private readonly LruCache<ProgramNode.FeatureAndContext, object> _featureCache = new ConcurrentLruCache<ProgramNode.FeatureAndContext, object>(64, EqualityComparer<ProgramNode.FeatureAndContext>.Default, null, null);

		// Token: 0x04001879 RID: 6265
		internal const string InternedProgramNodeReferenceKey = "Reference";

		// Token: 0x0400187A RID: 6266
		internal const string XMLObjectIdAttributeName = "ObjectID";

		// Token: 0x020008D9 RID: 2265
		private class FeatureAndContext : IEquatable<ProgramNode.FeatureAndContext>
		{
			// Token: 0x170008A3 RID: 2211
			// (get) Token: 0x060030E1 RID: 12513 RVA: 0x0009012C File Offset: 0x0008E32C
			public IFeature Feature { get; }

			// Token: 0x170008A4 RID: 2212
			// (get) Token: 0x060030E2 RID: 12514 RVA: 0x00090134 File Offset: 0x0008E334
			public FeatureCalculationContext Context { get; }

			// Token: 0x060030E3 RID: 12515 RVA: 0x0009013C File Offset: 0x0008E33C
			public FeatureAndContext(IFeature feature, FeatureCalculationContext context)
			{
				this.Feature = feature;
				this.Context = context;
			}

			// Token: 0x060030E4 RID: 12516 RVA: 0x00090152 File Offset: 0x0008E352
			public bool Equals(ProgramNode.FeatureAndContext other)
			{
				return other != null && (other == this || (object.Equals(this.Feature, other.Feature) && this.Feature.GetFccEqualityComparer().Equals(this.Context, other.Context)));
			}

			// Token: 0x060030E5 RID: 12517 RVA: 0x00090190 File Offset: 0x0008E390
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj == this)
				{
					return true;
				}
				ProgramNode.FeatureAndContext featureAndContext = obj as ProgramNode.FeatureAndContext;
				return featureAndContext != null && this.Equals(featureAndContext);
			}

			// Token: 0x060030E6 RID: 12518 RVA: 0x000901BB File Offset: 0x0008E3BB
			public override int GetHashCode()
			{
				return (this.Feature.GetHashCode() * 11317) ^ ((this.Context == null) ? 0 : (this.Feature.GetFccEqualityComparer().GetHashCode(this.Context) * 15443));
			}
		}
	}
}
