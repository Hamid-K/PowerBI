using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build
{
	// Token: 0x020019FE RID: 6654
	public class GrammarBuilders
	{
		// Token: 0x0600D8C7 RID: 55495 RVA: 0x002E7966 File Offset: 0x002E5B66
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x0600D8C8 RID: 55496 RVA: 0x002E7992 File Offset: 0x002E5B92
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x0600D8C9 RID: 55497 RVA: 0x002E799F File Offset: 0x002E5B9F
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x0600D8CA RID: 55498 RVA: 0x002E79AC File Offset: 0x002E5BAC
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x0600D8CB RID: 55499 RVA: 0x002E79B9 File Offset: 0x002E5BB9
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x0600D8CC RID: 55500 RVA: 0x002E79C6 File Offset: 0x002E5BC6
		// (set) Token: 0x0600D8CD RID: 55501 RVA: 0x002E79CE File Offset: 0x002E5BCE
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x0600D8CE RID: 55502 RVA: 0x002E79D7 File Offset: 0x002E5BD7
		// (set) Token: 0x0600D8CF RID: 55503 RVA: 0x002E79DF File Offset: 0x002E5BDF
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x0600D8D0 RID: 55504 RVA: 0x002E79E8 File Offset: 0x002E5BE8
		public GrammarBuilders(Grammar grammar)
		{
			GrammarBuilders <>4__this = this;
			this._symbol = new Lazy<GrammarBuilders.GrammarSymbols>(() => new GrammarBuilders.GrammarSymbols(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._rule = new Lazy<GrammarBuilders.GrammarRules>(() => new GrammarBuilders.GrammarRules(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._unnamedConversion = new Lazy<GrammarBuilders.GrammarUnnamedConversions>(() => new GrammarBuilders.GrammarUnnamedConversions(grammar), LazyThreadSafetyMode.ExecutionAndPublication);
			this._hole = new Lazy<GrammarBuilders.GrammarHoles>(() => new GrammarBuilders.GrammarHoles(<>4__this), LazyThreadSafetyMode.ExecutionAndPublication);
			this.Node = new GrammarBuilders.Nodes(this);
			this.Set = new GrammarBuilders.Sets(this);
		}

		// Token: 0x04005350 RID: 21328
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x04005351 RID: 21329
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x04005352 RID: 21330
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x04005353 RID: 21331
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x04005354 RID: 21332
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x020019FF RID: 6655
		public class GrammarSymbols
		{
			// Token: 0x17002447 RID: 9287
			// (get) Token: 0x0600D8D2 RID: 55506 RVA: 0x002E7A93 File Offset: 0x002E5C93
			// (set) Token: 0x0600D8D3 RID: 55507 RVA: 0x002E7A9B File Offset: 0x002E5C9B
			public Symbol v { get; private set; }

			// Token: 0x17002448 RID: 9288
			// (get) Token: 0x0600D8D4 RID: 55508 RVA: 0x002E7AA4 File Offset: 0x002E5CA4
			// (set) Token: 0x0600D8D5 RID: 55509 RVA: 0x002E7AAC File Offset: 0x002E5CAC
			public Symbol path { get; private set; }

			// Token: 0x17002449 RID: 9289
			// (get) Token: 0x0600D8D6 RID: 55510 RVA: 0x002E7AB5 File Offset: 0x002E5CB5
			// (set) Token: 0x0600D8D7 RID: 55511 RVA: 0x002E7ABD File Offset: 0x002E5CBD
			public Symbol str { get; private set; }

			// Token: 0x1700244A RID: 9290
			// (get) Token: 0x0600D8D8 RID: 55512 RVA: 0x002E7AC6 File Offset: 0x002E5CC6
			// (set) Token: 0x0600D8D9 RID: 55513 RVA: 0x002E7ACE File Offset: 0x002E5CCE
			public Symbol t { get; private set; }

			// Token: 0x1700244B RID: 9291
			// (get) Token: 0x0600D8DA RID: 55514 RVA: 0x002E7AD7 File Offset: 0x002E5CD7
			// (set) Token: 0x0600D8DB RID: 55515 RVA: 0x002E7ADF File Offset: 0x002E5CDF
			public Symbol output { get; private set; }

			// Token: 0x1700244C RID: 9292
			// (get) Token: 0x0600D8DC RID: 55516 RVA: 0x002E7AE8 File Offset: 0x002E5CE8
			// (set) Token: 0x0600D8DD RID: 55517 RVA: 0x002E7AF0 File Offset: 0x002E5CF0
			public Symbol x { get; private set; }

			// Token: 0x1700244D RID: 9293
			// (get) Token: 0x0600D8DE RID: 55518 RVA: 0x002E7AF9 File Offset: 0x002E5CF9
			// (set) Token: 0x0600D8DF RID: 55519 RVA: 0x002E7B01 File Offset: 0x002E5D01
			public Symbol value { get; private set; }

			// Token: 0x1700244E RID: 9294
			// (get) Token: 0x0600D8E0 RID: 55520 RVA: 0x002E7B0A File Offset: 0x002E5D0A
			// (set) Token: 0x0600D8E1 RID: 55521 RVA: 0x002E7B12 File Offset: 0x002E5D12
			public Symbol @object { get; private set; }

			// Token: 0x1700244F RID: 9295
			// (get) Token: 0x0600D8E2 RID: 55522 RVA: 0x002E7B1B File Offset: 0x002E5D1B
			// (set) Token: 0x0600D8E3 RID: 55523 RVA: 0x002E7B23 File Offset: 0x002E5D23
			public Symbol array { get; private set; }

			// Token: 0x17002450 RID: 9296
			// (get) Token: 0x0600D8E4 RID: 55524 RVA: 0x002E7B2C File Offset: 0x002E5D2C
			// (set) Token: 0x0600D8E5 RID: 55525 RVA: 0x002E7B34 File Offset: 0x002E5D34
			public Symbol property { get; private set; }

			// Token: 0x17002451 RID: 9297
			// (get) Token: 0x0600D8E6 RID: 55526 RVA: 0x002E7B3D File Offset: 0x002E5D3D
			// (set) Token: 0x0600D8E7 RID: 55527 RVA: 0x002E7B45 File Offset: 0x002E5D45
			public Symbol elements { get; private set; }

			// Token: 0x17002452 RID: 9298
			// (get) Token: 0x0600D8E8 RID: 55528 RVA: 0x002E7B4E File Offset: 0x002E5D4E
			// (set) Token: 0x0600D8E9 RID: 55529 RVA: 0x002E7B56 File Offset: 0x002E5D56
			public Symbol key { get; private set; }

			// Token: 0x17002453 RID: 9299
			// (get) Token: 0x0600D8EA RID: 55530 RVA: 0x002E7B5F File Offset: 0x002E5D5F
			// (set) Token: 0x0600D8EB RID: 55531 RVA: 0x002E7B67 File Offset: 0x002E5D67
			public Symbol selectKey { get; private set; }

			// Token: 0x17002454 RID: 9300
			// (get) Token: 0x0600D8EC RID: 55532 RVA: 0x002E7B70 File Offset: 0x002E5D70
			// (set) Token: 0x0600D8ED RID: 55533 RVA: 0x002E7B78 File Offset: 0x002E5D78
			public Symbol selectOrTransformValue { get; private set; }

			// Token: 0x17002455 RID: 9301
			// (get) Token: 0x0600D8EE RID: 55534 RVA: 0x002E7B81 File Offset: 0x002E5D81
			// (set) Token: 0x0600D8EF RID: 55535 RVA: 0x002E7B89 File Offset: 0x002E5D89
			public Symbol selectValue { get; private set; }

			// Token: 0x17002456 RID: 9302
			// (get) Token: 0x0600D8F0 RID: 55536 RVA: 0x002E7B92 File Offset: 0x002E5D92
			// (set) Token: 0x0600D8F1 RID: 55537 RVA: 0x002E7B9A File Offset: 0x002E5D9A
			public Symbol transformValue { get; private set; }

			// Token: 0x17002457 RID: 9303
			// (get) Token: 0x0600D8F2 RID: 55538 RVA: 0x002E7BA3 File Offset: 0x002E5DA3
			// (set) Token: 0x0600D8F3 RID: 55539 RVA: 0x002E7BAB File Offset: 0x002E5DAB
			public Symbol transformLet { get; private set; }

			// Token: 0x17002458 RID: 9304
			// (get) Token: 0x0600D8F4 RID: 55540 RVA: 0x002E7BB4 File Offset: 0x002E5DB4
			// (set) Token: 0x0600D8F5 RID: 55541 RVA: 0x002E7BBC File Offset: 0x002E5DBC
			public Symbol row { get; private set; }

			// Token: 0x17002459 RID: 9305
			// (get) Token: 0x0600D8F6 RID: 55542 RVA: 0x002E7BC5 File Offset: 0x002E5DC5
			// (set) Token: 0x0600D8F7 RID: 55543 RVA: 0x002E7BCD File Offset: 0x002E5DCD
			public Symbol transformString { get; private set; }

			// Token: 0x1700245A RID: 9306
			// (get) Token: 0x0600D8F8 RID: 55544 RVA: 0x002E7BD6 File Offset: 0x002E5DD6
			// (set) Token: 0x0600D8F9 RID: 55545 RVA: 0x002E7BDE File Offset: 0x002E5DDE
			public Symbol selectArray { get; private set; }

			// Token: 0x1700245B RID: 9307
			// (get) Token: 0x0600D8FA RID: 55546 RVA: 0x002E7BE7 File Offset: 0x002E5DE7
			// (set) Token: 0x0600D8FB RID: 55547 RVA: 0x002E7BEF File Offset: 0x002E5DEF
			public Symbol _LFun0 { get; private set; }

			// Token: 0x1700245C RID: 9308
			// (get) Token: 0x0600D8FC RID: 55548 RVA: 0x002E7BF8 File Offset: 0x002E5DF8
			// (set) Token: 0x0600D8FD RID: 55549 RVA: 0x002E7C00 File Offset: 0x002E5E00
			public Symbol _LFun1 { get; private set; }

			// Token: 0x1700245D RID: 9309
			// (get) Token: 0x0600D8FE RID: 55550 RVA: 0x002E7C09 File Offset: 0x002E5E09
			// (set) Token: 0x0600D8FF RID: 55551 RVA: 0x002E7C11 File Offset: 0x002E5E11
			public Symbol _LetB0 { get; private set; }

			// Token: 0x0600D900 RID: 55552 RVA: 0x002E7C1C File Offset: 0x002E5E1C
			public GrammarSymbols(Grammar grammar)
			{
				this.v = grammar.Symbol("v");
				this.path = grammar.Symbol("path");
				this.str = grammar.Symbol("str");
				this.t = grammar.Symbol("t");
				this.output = grammar.Symbol("output");
				this.x = grammar.Symbol("x");
				this.value = grammar.Symbol("value");
				this.@object = grammar.Symbol("object");
				this.array = grammar.Symbol("array");
				this.property = grammar.Symbol("property");
				this.elements = grammar.Symbol("elements");
				this.key = grammar.Symbol("key");
				this.selectKey = grammar.Symbol("selectKey");
				this.selectOrTransformValue = grammar.Symbol("selectOrTransformValue");
				this.selectValue = grammar.Symbol("selectValue");
				this.transformValue = grammar.Symbol("transformValue");
				this.transformLet = grammar.Symbol("transformLet");
				this.row = grammar.Symbol("row");
				this.transformString = grammar.Symbol("transformString");
				this.selectArray = grammar.Symbol("selectArray");
				this._LFun0 = grammar.Symbol("_LFun0");
				this._LFun1 = grammar.Symbol("_LFun1");
				this._LetB0 = grammar.Symbol("_LetB0");
			}
		}

		// Token: 0x02001A00 RID: 6656
		public class GrammarRules
		{
			// Token: 0x1700245E RID: 9310
			// (get) Token: 0x0600D901 RID: 55553 RVA: 0x002E7DB6 File Offset: 0x002E5FB6
			// (set) Token: 0x0600D902 RID: 55554 RVA: 0x002E7DBE File Offset: 0x002E5FBE
			public BlackBoxRule _Value { get; private set; }

			// Token: 0x1700245F RID: 9311
			// (get) Token: 0x0600D903 RID: 55555 RVA: 0x002E7DC7 File Offset: 0x002E5FC7
			// (set) Token: 0x0600D904 RID: 55556 RVA: 0x002E7DCF File Offset: 0x002E5FCF
			public BlackBoxRule ConstValue { get; private set; }

			// Token: 0x17002460 RID: 9312
			// (get) Token: 0x0600D905 RID: 55557 RVA: 0x002E7DD8 File Offset: 0x002E5FD8
			// (set) Token: 0x0600D906 RID: 55558 RVA: 0x002E7DE0 File Offset: 0x002E5FE0
			public BlackBoxRule Object { get; private set; }

			// Token: 0x17002461 RID: 9313
			// (get) Token: 0x0600D907 RID: 55559 RVA: 0x002E7DE9 File Offset: 0x002E5FE9
			// (set) Token: 0x0600D908 RID: 55560 RVA: 0x002E7DF1 File Offset: 0x002E5FF1
			public BlackBoxRule Append { get; private set; }

			// Token: 0x17002462 RID: 9314
			// (get) Token: 0x0600D909 RID: 55561 RVA: 0x002E7DFA File Offset: 0x002E5FFA
			// (set) Token: 0x0600D90A RID: 55562 RVA: 0x002E7E02 File Offset: 0x002E6002
			public BlackBoxRule SelectObject { get; private set; }

			// Token: 0x17002463 RID: 9315
			// (get) Token: 0x0600D90B RID: 55563 RVA: 0x002E7E0B File Offset: 0x002E600B
			// (set) Token: 0x0600D90C RID: 55564 RVA: 0x002E7E13 File Offset: 0x002E6013
			public BlackBoxRule FlattenObject { get; private set; }

			// Token: 0x17002464 RID: 9316
			// (get) Token: 0x0600D90D RID: 55565 RVA: 0x002E7E1C File Offset: 0x002E601C
			// (set) Token: 0x0600D90E RID: 55566 RVA: 0x002E7E24 File Offset: 0x002E6024
			public BlackBoxRule Array { get; private set; }

			// Token: 0x17002465 RID: 9317
			// (get) Token: 0x0600D90F RID: 55567 RVA: 0x002E7E2D File Offset: 0x002E602D
			// (set) Token: 0x0600D910 RID: 55568 RVA: 0x002E7E35 File Offset: 0x002E6035
			public BlackBoxRule Property { get; private set; }

			// Token: 0x17002466 RID: 9318
			// (get) Token: 0x0600D911 RID: 55569 RVA: 0x002E7E3E File Offset: 0x002E603E
			// (set) Token: 0x0600D912 RID: 55570 RVA: 0x002E7E46 File Offset: 0x002E6046
			public BlackBoxRule SelectProperty { get; private set; }

			// Token: 0x17002467 RID: 9319
			// (get) Token: 0x0600D913 RID: 55571 RVA: 0x002E7E4F File Offset: 0x002E604F
			// (set) Token: 0x0600D914 RID: 55572 RVA: 0x002E7E57 File Offset: 0x002E6057
			public BlackBoxRule Key { get; private set; }

			// Token: 0x17002468 RID: 9320
			// (get) Token: 0x0600D915 RID: 55573 RVA: 0x002E7E60 File Offset: 0x002E6060
			// (set) Token: 0x0600D916 RID: 55574 RVA: 0x002E7E68 File Offset: 0x002E6068
			public BlackBoxRule ConstKey { get; private set; }

			// Token: 0x17002469 RID: 9321
			// (get) Token: 0x0600D917 RID: 55575 RVA: 0x002E7E71 File Offset: 0x002E6071
			// (set) Token: 0x0600D918 RID: 55576 RVA: 0x002E7E79 File Offset: 0x002E6079
			public BlackBoxRule SelectKey { get; private set; }

			// Token: 0x1700246A RID: 9322
			// (get) Token: 0x0600D919 RID: 55577 RVA: 0x002E7E82 File Offset: 0x002E6082
			// (set) Token: 0x0600D91A RID: 55578 RVA: 0x002E7E8A File Offset: 0x002E608A
			public BlackBoxRule SelectValue { get; private set; }

			// Token: 0x1700246B RID: 9323
			// (get) Token: 0x0600D91B RID: 55579 RVA: 0x002E7E93 File Offset: 0x002E6093
			// (set) Token: 0x0600D91C RID: 55580 RVA: 0x002E7E9B File Offset: 0x002E609B
			public BlackBoxRule ValueToString { get; private set; }

			// Token: 0x1700246C RID: 9324
			// (get) Token: 0x0600D91D RID: 55581 RVA: 0x002E7EA4 File Offset: 0x002E60A4
			// (set) Token: 0x0600D91E RID: 55582 RVA: 0x002E7EAC File Offset: 0x002E60AC
			public BlackBoxRule ConvertValueTo { get; private set; }

			// Token: 0x1700246D RID: 9325
			// (get) Token: 0x0600D91F RID: 55583 RVA: 0x002E7EB5 File Offset: 0x002E60B5
			// (set) Token: 0x0600D920 RID: 55584 RVA: 0x002E7EBD File Offset: 0x002E60BD
			public BlackBoxRule TransformValue { get; private set; }

			// Token: 0x1700246E RID: 9326
			// (get) Token: 0x0600D921 RID: 55585 RVA: 0x002E7EC6 File Offset: 0x002E60C6
			// (set) Token: 0x0600D922 RID: 55586 RVA: 0x002E7ECE File Offset: 0x002E60CE
			public BlackBoxRule SelectStringValues { get; private set; }

			// Token: 0x1700246F RID: 9327
			// (get) Token: 0x0600D923 RID: 55587 RVA: 0x002E7ED7 File Offset: 0x002E60D7
			// (set) Token: 0x0600D924 RID: 55588 RVA: 0x002E7EDF File Offset: 0x002E60DF
			public BlackBoxRule SelectArray { get; private set; }

			// Token: 0x17002470 RID: 9328
			// (get) Token: 0x0600D925 RID: 55589 RVA: 0x002E7EE8 File Offset: 0x002E60E8
			// (set) Token: 0x0600D926 RID: 55590 RVA: 0x002E7EF0 File Offset: 0x002E60F0
			public BlackBoxRule ToArray { get; private set; }

			// Token: 0x17002471 RID: 9329
			// (get) Token: 0x0600D927 RID: 55591 RVA: 0x002E7EF9 File Offset: 0x002E60F9
			// (set) Token: 0x0600D928 RID: 55592 RVA: 0x002E7F01 File Offset: 0x002E6101
			public ConceptRule Transform { get; private set; }

			// Token: 0x17002472 RID: 9330
			// (get) Token: 0x0600D929 RID: 55593 RVA: 0x002E7F0A File Offset: 0x002E610A
			// (set) Token: 0x0600D92A RID: 55594 RVA: 0x002E7F12 File Offset: 0x002E6112
			public ConceptRule TransformFlatten { get; private set; }

			// Token: 0x17002473 RID: 9331
			// (get) Token: 0x0600D92B RID: 55595 RVA: 0x002E7F1B File Offset: 0x002E611B
			// (set) Token: 0x0600D92C RID: 55596 RVA: 0x002E7F23 File Offset: 0x002E6123
			public ConversionRule SelectOrTransformValue { get; private set; }

			// Token: 0x17002474 RID: 9332
			// (get) Token: 0x0600D92D RID: 55597 RVA: 0x002E7F2C File Offset: 0x002E612C
			// (set) Token: 0x0600D92E RID: 55598 RVA: 0x002E7F34 File Offset: 0x002E6134
			public ConversionRule TransformString { get; private set; }

			// Token: 0x17002475 RID: 9333
			// (get) Token: 0x0600D92F RID: 55599 RVA: 0x002E7F3D File Offset: 0x002E613D
			// (set) Token: 0x0600D930 RID: 55600 RVA: 0x002E7F45 File Offset: 0x002E6145
			public LetRule output { get; private set; }

			// Token: 0x17002476 RID: 9334
			// (get) Token: 0x0600D931 RID: 55601 RVA: 0x002E7F4E File Offset: 0x002E614E
			// (set) Token: 0x0600D932 RID: 55602 RVA: 0x002E7F56 File Offset: 0x002E6156
			public LetRule TransformLet { get; private set; }

			// Token: 0x0600D933 RID: 55603 RVA: 0x002E7F60 File Offset: 0x002E6160
			public GrammarRules(Grammar grammar)
			{
				this._Value = (BlackBoxRule)grammar.Rule("Value");
				this.ConstValue = (BlackBoxRule)grammar.Rule("ConstValue");
				this.Object = (BlackBoxRule)grammar.Rule("Object");
				this.Append = (BlackBoxRule)grammar.Rule("Append");
				this.SelectObject = (BlackBoxRule)grammar.Rule("SelectObject");
				this.FlattenObject = (BlackBoxRule)grammar.Rule("FlattenObject");
				this.Array = (BlackBoxRule)grammar.Rule("Array");
				this.Property = (BlackBoxRule)grammar.Rule("Property");
				this.SelectProperty = (BlackBoxRule)grammar.Rule("SelectProperty");
				this.Key = (BlackBoxRule)grammar.Rule("Key");
				this.ConstKey = (BlackBoxRule)grammar.Rule("ConstKey");
				this.SelectKey = (BlackBoxRule)grammar.Rule("SelectKey");
				this.SelectValue = (BlackBoxRule)grammar.Rule("SelectValue");
				this.ValueToString = (BlackBoxRule)grammar.Rule("ValueToString");
				this.ConvertValueTo = (BlackBoxRule)grammar.Rule("ConvertValueTo");
				this.TransformValue = (BlackBoxRule)grammar.Rule("TransformValue");
				this.SelectStringValues = (BlackBoxRule)grammar.Rule("SelectStringValues");
				this.SelectArray = (BlackBoxRule)grammar.Rule("SelectArray");
				this.ToArray = (BlackBoxRule)grammar.Rule("ToArray");
				this.Transform = (ConceptRule)grammar.Rule("Transform");
				this.TransformFlatten = (ConceptRule)grammar.Rule("TransformFlatten");
				this.SelectOrTransformValue = (ConversionRule)grammar.Rule("SelectOrTransformValue");
				this.TransformString = (ConversionRule)grammar.Rule("TransformString");
				this.output = (LetRule)grammar.Rule("output");
				this.TransformLet = (LetRule)grammar.Rule("TransformLet");
			}
		}

		// Token: 0x02001A01 RID: 6657
		public class GrammarUnnamedConversions
		{
			// Token: 0x17002477 RID: 9335
			// (get) Token: 0x0600D934 RID: 55604 RVA: 0x002E8199 File Offset: 0x002E6399
			// (set) Token: 0x0600D935 RID: 55605 RVA: 0x002E81A1 File Offset: 0x002E63A1
			public ConversionRule output_v { get; private set; }

			// Token: 0x17002478 RID: 9336
			// (get) Token: 0x0600D936 RID: 55606 RVA: 0x002E81AA File Offset: 0x002E63AA
			// (set) Token: 0x0600D937 RID: 55607 RVA: 0x002E81B2 File Offset: 0x002E63B2
			public ConversionRule value_object { get; private set; }

			// Token: 0x17002479 RID: 9337
			// (get) Token: 0x0600D938 RID: 55608 RVA: 0x002E81BB File Offset: 0x002E63BB
			// (set) Token: 0x0600D939 RID: 55609 RVA: 0x002E81C3 File Offset: 0x002E63C3
			public ConversionRule value_array { get; private set; }

			// Token: 0x1700247A RID: 9338
			// (get) Token: 0x0600D93A RID: 55610 RVA: 0x002E81CC File Offset: 0x002E63CC
			// (set) Token: 0x0600D93B RID: 55611 RVA: 0x002E81D4 File Offset: 0x002E63D4
			public ConversionRule array_selectArray { get; private set; }

			// Token: 0x1700247B RID: 9339
			// (get) Token: 0x0600D93C RID: 55612 RVA: 0x002E81DD File Offset: 0x002E63DD
			// (set) Token: 0x0600D93D RID: 55613 RVA: 0x002E81E5 File Offset: 0x002E63E5
			public ConversionRule selectOrTransformValue_selectValue { get; private set; }

			// Token: 0x1700247C RID: 9340
			// (get) Token: 0x0600D93E RID: 55614 RVA: 0x002E81EE File Offset: 0x002E63EE
			// (set) Token: 0x0600D93F RID: 55615 RVA: 0x002E81F6 File Offset: 0x002E63F6
			public ConversionRule selectOrTransformValue_transformValue { get; private set; }

			// Token: 0x0600D940 RID: 55616 RVA: 0x002E8200 File Offset: 0x002E6400
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.output_v = (ConversionRule)grammar.Rule("~convert_output_v");
				this.value_object = (ConversionRule)grammar.Rule("~convert_value_object");
				this.value_array = (ConversionRule)grammar.Rule("~convert_value_array");
				this.array_selectArray = (ConversionRule)grammar.Rule("~convert_array_selectArray");
				this.selectOrTransformValue_selectValue = (ConversionRule)grammar.Rule("~convert_selectOrTransformValue_selectValue");
				this.selectOrTransformValue_transformValue = (ConversionRule)grammar.Rule("~convert_selectOrTransformValue_transformValue");
			}
		}

		// Token: 0x02001A02 RID: 6658
		public class GrammarHoles
		{
			// Token: 0x1700247D RID: 9341
			// (get) Token: 0x0600D941 RID: 55617 RVA: 0x002E8297 File Offset: 0x002E6497
			// (set) Token: 0x0600D942 RID: 55618 RVA: 0x002E829F File Offset: 0x002E649F
			public Hole v { get; private set; }

			// Token: 0x1700247E RID: 9342
			// (get) Token: 0x0600D943 RID: 55619 RVA: 0x002E82A8 File Offset: 0x002E64A8
			// (set) Token: 0x0600D944 RID: 55620 RVA: 0x002E82B0 File Offset: 0x002E64B0
			public Hole path { get; private set; }

			// Token: 0x1700247F RID: 9343
			// (get) Token: 0x0600D945 RID: 55621 RVA: 0x002E82B9 File Offset: 0x002E64B9
			// (set) Token: 0x0600D946 RID: 55622 RVA: 0x002E82C1 File Offset: 0x002E64C1
			public Hole str { get; private set; }

			// Token: 0x17002480 RID: 9344
			// (get) Token: 0x0600D947 RID: 55623 RVA: 0x002E82CA File Offset: 0x002E64CA
			// (set) Token: 0x0600D948 RID: 55624 RVA: 0x002E82D2 File Offset: 0x002E64D2
			public Hole t { get; private set; }

			// Token: 0x17002481 RID: 9345
			// (get) Token: 0x0600D949 RID: 55625 RVA: 0x002E82DB File Offset: 0x002E64DB
			// (set) Token: 0x0600D94A RID: 55626 RVA: 0x002E82E3 File Offset: 0x002E64E3
			public Hole output { get; private set; }

			// Token: 0x17002482 RID: 9346
			// (get) Token: 0x0600D94B RID: 55627 RVA: 0x002E82EC File Offset: 0x002E64EC
			// (set) Token: 0x0600D94C RID: 55628 RVA: 0x002E82F4 File Offset: 0x002E64F4
			public Hole x { get; private set; }

			// Token: 0x17002483 RID: 9347
			// (get) Token: 0x0600D94D RID: 55629 RVA: 0x002E82FD File Offset: 0x002E64FD
			// (set) Token: 0x0600D94E RID: 55630 RVA: 0x002E8305 File Offset: 0x002E6505
			public Hole value { get; private set; }

			// Token: 0x17002484 RID: 9348
			// (get) Token: 0x0600D94F RID: 55631 RVA: 0x002E830E File Offset: 0x002E650E
			// (set) Token: 0x0600D950 RID: 55632 RVA: 0x002E8316 File Offset: 0x002E6516
			public Hole @object { get; private set; }

			// Token: 0x17002485 RID: 9349
			// (get) Token: 0x0600D951 RID: 55633 RVA: 0x002E831F File Offset: 0x002E651F
			// (set) Token: 0x0600D952 RID: 55634 RVA: 0x002E8327 File Offset: 0x002E6527
			public Hole array { get; private set; }

			// Token: 0x17002486 RID: 9350
			// (get) Token: 0x0600D953 RID: 55635 RVA: 0x002E8330 File Offset: 0x002E6530
			// (set) Token: 0x0600D954 RID: 55636 RVA: 0x002E8338 File Offset: 0x002E6538
			public Hole property { get; private set; }

			// Token: 0x17002487 RID: 9351
			// (get) Token: 0x0600D955 RID: 55637 RVA: 0x002E8341 File Offset: 0x002E6541
			// (set) Token: 0x0600D956 RID: 55638 RVA: 0x002E8349 File Offset: 0x002E6549
			public Hole elements { get; private set; }

			// Token: 0x17002488 RID: 9352
			// (get) Token: 0x0600D957 RID: 55639 RVA: 0x002E8352 File Offset: 0x002E6552
			// (set) Token: 0x0600D958 RID: 55640 RVA: 0x002E835A File Offset: 0x002E655A
			public Hole key { get; private set; }

			// Token: 0x17002489 RID: 9353
			// (get) Token: 0x0600D959 RID: 55641 RVA: 0x002E8363 File Offset: 0x002E6563
			// (set) Token: 0x0600D95A RID: 55642 RVA: 0x002E836B File Offset: 0x002E656B
			public Hole selectKey { get; private set; }

			// Token: 0x1700248A RID: 9354
			// (get) Token: 0x0600D95B RID: 55643 RVA: 0x002E8374 File Offset: 0x002E6574
			// (set) Token: 0x0600D95C RID: 55644 RVA: 0x002E837C File Offset: 0x002E657C
			public Hole selectOrTransformValue { get; private set; }

			// Token: 0x1700248B RID: 9355
			// (get) Token: 0x0600D95D RID: 55645 RVA: 0x002E8385 File Offset: 0x002E6585
			// (set) Token: 0x0600D95E RID: 55646 RVA: 0x002E838D File Offset: 0x002E658D
			public Hole selectValue { get; private set; }

			// Token: 0x1700248C RID: 9356
			// (get) Token: 0x0600D95F RID: 55647 RVA: 0x002E8396 File Offset: 0x002E6596
			// (set) Token: 0x0600D960 RID: 55648 RVA: 0x002E839E File Offset: 0x002E659E
			public Hole transformValue { get; private set; }

			// Token: 0x1700248D RID: 9357
			// (get) Token: 0x0600D961 RID: 55649 RVA: 0x002E83A7 File Offset: 0x002E65A7
			// (set) Token: 0x0600D962 RID: 55650 RVA: 0x002E83AF File Offset: 0x002E65AF
			public Hole transformLet { get; private set; }

			// Token: 0x1700248E RID: 9358
			// (get) Token: 0x0600D963 RID: 55651 RVA: 0x002E83B8 File Offset: 0x002E65B8
			// (set) Token: 0x0600D964 RID: 55652 RVA: 0x002E83C0 File Offset: 0x002E65C0
			public Hole row { get; private set; }

			// Token: 0x1700248F RID: 9359
			// (get) Token: 0x0600D965 RID: 55653 RVA: 0x002E83C9 File Offset: 0x002E65C9
			// (set) Token: 0x0600D966 RID: 55654 RVA: 0x002E83D1 File Offset: 0x002E65D1
			public Hole transformString { get; private set; }

			// Token: 0x17002490 RID: 9360
			// (get) Token: 0x0600D967 RID: 55655 RVA: 0x002E83DA File Offset: 0x002E65DA
			// (set) Token: 0x0600D968 RID: 55656 RVA: 0x002E83E2 File Offset: 0x002E65E2
			public Hole selectArray { get; private set; }

			// Token: 0x17002491 RID: 9361
			// (get) Token: 0x0600D969 RID: 55657 RVA: 0x002E83EB File Offset: 0x002E65EB
			// (set) Token: 0x0600D96A RID: 55658 RVA: 0x002E83F3 File Offset: 0x002E65F3
			public Hole _LFun0 { get; private set; }

			// Token: 0x17002492 RID: 9362
			// (get) Token: 0x0600D96B RID: 55659 RVA: 0x002E83FC File Offset: 0x002E65FC
			// (set) Token: 0x0600D96C RID: 55660 RVA: 0x002E8404 File Offset: 0x002E6604
			public Hole _LFun1 { get; private set; }

			// Token: 0x17002493 RID: 9363
			// (get) Token: 0x0600D96D RID: 55661 RVA: 0x002E840D File Offset: 0x002E660D
			// (set) Token: 0x0600D96E RID: 55662 RVA: 0x002E8415 File Offset: 0x002E6615
			public Hole _LetB0 { get; private set; }

			// Token: 0x0600D96F RID: 55663 RVA: 0x002E8420 File Offset: 0x002E6620
			public GrammarHoles(GrammarBuilders builders)
			{
				this.v = new Hole(builders.Symbol.v, null);
				this.path = new Hole(builders.Symbol.path, null);
				this.str = new Hole(builders.Symbol.str, null);
				this.t = new Hole(builders.Symbol.t, null);
				this.output = new Hole(builders.Symbol.output, null);
				this.x = new Hole(builders.Symbol.x, null);
				this.value = new Hole(builders.Symbol.value, null);
				this.@object = new Hole(builders.Symbol.@object, null);
				this.array = new Hole(builders.Symbol.array, null);
				this.property = new Hole(builders.Symbol.property, null);
				this.elements = new Hole(builders.Symbol.elements, null);
				this.key = new Hole(builders.Symbol.key, null);
				this.selectKey = new Hole(builders.Symbol.selectKey, null);
				this.selectOrTransformValue = new Hole(builders.Symbol.selectOrTransformValue, null);
				this.selectValue = new Hole(builders.Symbol.selectValue, null);
				this.transformValue = new Hole(builders.Symbol.transformValue, null);
				this.transformLet = new Hole(builders.Symbol.transformLet, null);
				this.row = new Hole(builders.Symbol.row, null);
				this.transformString = new Hole(builders.Symbol.transformString, null);
				this.selectArray = new Hole(builders.Symbol.selectArray, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
				this._LFun1 = new Hole(builders.Symbol._LFun1, null);
				this._LetB0 = new Hole(builders.Symbol._LetB0, null);
			}
		}

		// Token: 0x02001A03 RID: 6659
		public class Nodes
		{
			// Token: 0x0600D970 RID: 55664 RVA: 0x002E8644 File Offset: 0x002E6844
			public Nodes(GrammarBuilders builders)
			{
				this.Rule = new GrammarBuilders.Nodes.NodeRules(builders);
				this.UnnamedConversion = new GrammarBuilders.Nodes.NodeUnnamedConversionRules(builders);
				this._variable = new Lazy<GrammarBuilders.Nodes.NodeVariables>(() => new GrammarBuilders.Nodes.NodeVariables(builders), LazyThreadSafetyMode.ExecutionAndPublication);
				this._hole = new Lazy<GrammarBuilders.Nodes.NodeHoles>(() => new GrammarBuilders.Nodes.NodeHoles(builders), LazyThreadSafetyMode.ExecutionAndPublication);
				this.Unsafe = new GrammarBuilders.Nodes.NodeUnsafe();
				this.Cast = new GrammarBuilders.Nodes.NodeCast(builders);
				this.CastRule = new GrammarBuilders.Nodes.RuleCast(builders);
				this.Is = new GrammarBuilders.Nodes.NodeIs(builders);
				this.IsRule = new GrammarBuilders.Nodes.RuleIs(builders);
				this.As = new GrammarBuilders.Nodes.NodeAs(builders);
				this.AsRule = new GrammarBuilders.Nodes.RuleAs(builders);
			}

			// Token: 0x17002494 RID: 9364
			// (get) Token: 0x0600D971 RID: 55665 RVA: 0x002E8727 File Offset: 0x002E6927
			// (set) Token: 0x0600D972 RID: 55666 RVA: 0x002E872F File Offset: 0x002E692F
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17002495 RID: 9365
			// (get) Token: 0x0600D973 RID: 55667 RVA: 0x002E8738 File Offset: 0x002E6938
			// (set) Token: 0x0600D974 RID: 55668 RVA: 0x002E8740 File Offset: 0x002E6940
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17002496 RID: 9366
			// (get) Token: 0x0600D975 RID: 55669 RVA: 0x002E8749 File Offset: 0x002E6949
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17002497 RID: 9367
			// (get) Token: 0x0600D976 RID: 55670 RVA: 0x002E8756 File Offset: 0x002E6956
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17002498 RID: 9368
			// (get) Token: 0x0600D977 RID: 55671 RVA: 0x002E8763 File Offset: 0x002E6963
			// (set) Token: 0x0600D978 RID: 55672 RVA: 0x002E876B File Offset: 0x002E696B
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17002499 RID: 9369
			// (get) Token: 0x0600D979 RID: 55673 RVA: 0x002E8774 File Offset: 0x002E6974
			// (set) Token: 0x0600D97A RID: 55674 RVA: 0x002E877C File Offset: 0x002E697C
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x1700249A RID: 9370
			// (get) Token: 0x0600D97B RID: 55675 RVA: 0x002E8785 File Offset: 0x002E6985
			// (set) Token: 0x0600D97C RID: 55676 RVA: 0x002E878D File Offset: 0x002E698D
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x1700249B RID: 9371
			// (get) Token: 0x0600D97D RID: 55677 RVA: 0x002E8796 File Offset: 0x002E6996
			// (set) Token: 0x0600D97E RID: 55678 RVA: 0x002E879E File Offset: 0x002E699E
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x1700249C RID: 9372
			// (get) Token: 0x0600D97F RID: 55679 RVA: 0x002E87A7 File Offset: 0x002E69A7
			// (set) Token: 0x0600D980 RID: 55680 RVA: 0x002E87AF File Offset: 0x002E69AF
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x1700249D RID: 9373
			// (get) Token: 0x0600D981 RID: 55681 RVA: 0x002E87B8 File Offset: 0x002E69B8
			// (set) Token: 0x0600D982 RID: 55682 RVA: 0x002E87C0 File Offset: 0x002E69C0
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x1700249E RID: 9374
			// (get) Token: 0x0600D983 RID: 55683 RVA: 0x002E87C9 File Offset: 0x002E69C9
			// (set) Token: 0x0600D984 RID: 55684 RVA: 0x002E87D1 File Offset: 0x002E69D1
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x040053A6 RID: 21414
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x040053A7 RID: 21415
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001A04 RID: 6660
			public class NodeRules
			{
				// Token: 0x0600D985 RID: 55685 RVA: 0x002E87DA File Offset: 0x002E69DA
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600D986 RID: 55686 RVA: 0x002E87E9 File Offset: 0x002E69E9
				public path path(JPath value)
				{
					return new path(this._builders, value);
				}

				// Token: 0x0600D987 RID: 55687 RVA: 0x002E87F7 File Offset: 0x002E69F7
				public str str(string value)
				{
					return new str(this._builders, value);
				}

				// Token: 0x0600D988 RID: 55688 RVA: 0x002E8805 File Offset: 0x002E6A05
				public t t(JTokenType value)
				{
					return new t(this._builders, value);
				}

				// Token: 0x0600D989 RID: 55689 RVA: 0x002E8813 File Offset: 0x002E6A13
				public value _Value(selectKey value0)
				{
					return new _Value(this._builders, value0);
				}

				// Token: 0x0600D98A RID: 55690 RVA: 0x002E8826 File Offset: 0x002E6A26
				public value ConstValue(str value0)
				{
					return new ConstValue(this._builders, value0);
				}

				// Token: 0x0600D98B RID: 55691 RVA: 0x002E8839 File Offset: 0x002E6A39
				public @object Object(property value0)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object(this._builders, value0);
				}

				// Token: 0x0600D98C RID: 55692 RVA: 0x002E884C File Offset: 0x002E6A4C
				public @object Append(property value0, @object value1)
				{
					return new Append(this._builders, value0, value1);
				}

				// Token: 0x0600D98D RID: 55693 RVA: 0x002E8860 File Offset: 0x002E6A60
				public @object SelectObject(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new SelectObject(this._builders, value0, value1);
				}

				// Token: 0x0600D98E RID: 55694 RVA: 0x002E8874 File Offset: 0x002E6A74
				public @object FlattenObject(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new FlattenObject(this._builders, value0, value1);
				}

				// Token: 0x0600D98F RID: 55695 RVA: 0x002E8888 File Offset: 0x002E6A88
				public array Array(elements value0)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array(this._builders, value0);
				}

				// Token: 0x0600D990 RID: 55696 RVA: 0x002E889B File Offset: 0x002E6A9B
				public property Property(key value0, value value1)
				{
					return new Property(this._builders, value0, value1);
				}

				// Token: 0x0600D991 RID: 55697 RVA: 0x002E88AF File Offset: 0x002E6AAF
				public property SelectProperty(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new SelectProperty(this._builders, value0, value1);
				}

				// Token: 0x0600D992 RID: 55698 RVA: 0x002E88C3 File Offset: 0x002E6AC3
				public key Key(selectValue value0)
				{
					return new Key(this._builders, value0);
				}

				// Token: 0x0600D993 RID: 55699 RVA: 0x002E88D6 File Offset: 0x002E6AD6
				public key ConstKey(str value0)
				{
					return new ConstKey(this._builders, value0);
				}

				// Token: 0x0600D994 RID: 55700 RVA: 0x002E88E9 File Offset: 0x002E6AE9
				public selectKey SelectKey(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new SelectKey(this._builders, value0, value1);
				}

				// Token: 0x0600D995 RID: 55701 RVA: 0x002E88FD File Offset: 0x002E6AFD
				public selectValue SelectValue(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new SelectValue(this._builders, value0, value1);
				}

				// Token: 0x0600D996 RID: 55702 RVA: 0x002E8911 File Offset: 0x002E6B11
				public selectValue ValueToString(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new ValueToString(this._builders, value0, value1);
				}

				// Token: 0x0600D997 RID: 55703 RVA: 0x002E8925 File Offset: 0x002E6B25
				public selectValue ConvertValueTo(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, t value1, path value2)
				{
					return new ConvertValueTo(this._builders, value0, value1, value2);
				}

				// Token: 0x0600D998 RID: 55704 RVA: 0x002E893A File Offset: 0x002E6B3A
				public transformValue TransformValue(transformLet value0)
				{
					return new TransformValue(this._builders, value0);
				}

				// Token: 0x0600D999 RID: 55705 RVA: 0x002E894D File Offset: 0x002E6B4D
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 SelectStringValues(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0)
				{
					return new SelectStringValues(this._builders, value0);
				}

				// Token: 0x0600D99A RID: 55706 RVA: 0x002E8960 File Offset: 0x002E6B60
				public selectArray SelectArray(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new SelectArray(this._builders, value0, value1);
				}

				// Token: 0x0600D99B RID: 55707 RVA: 0x002E8974 File Offset: 0x002E6B74
				public selectArray ToArray(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value0, path value1)
				{
					return new ToArray(this._builders, value0, value1);
				}

				// Token: 0x0600D99C RID: 55708 RVA: 0x002E8988 File Offset: 0x002E6B88
				public elements Transform(value value0, selectArray value1)
				{
					return new Transform(this._builders, value0, value1);
				}

				// Token: 0x0600D99D RID: 55709 RVA: 0x002E899C File Offset: 0x002E6B9C
				public elements TransformFlatten(array value0, selectArray value1)
				{
					return new TransformFlatten(this._builders, value0, value1);
				}

				// Token: 0x0600D99E RID: 55710 RVA: 0x002E89B0 File Offset: 0x002E6BB0
				public value SelectOrTransformValue(selectOrTransformValue value0)
				{
					return new SelectOrTransformValue(this._builders, value0);
				}

				// Token: 0x0600D99F RID: 55711 RVA: 0x002E89C3 File Offset: 0x002E6BC3
				public transformString TransformString(@switch value0)
				{
					return new TransformString(this._builders, value0);
				}

				// Token: 0x0600D9A0 RID: 55712 RVA: 0x002E89D6 File Offset: 0x002E6BD6
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output output(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v value0, value value1)
				{
					return new Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output(this._builders, value0, value1);
				}

				// Token: 0x0600D9A1 RID: 55713 RVA: 0x002E89EA File Offset: 0x002E6BEA
				public transformLet TransformLet(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 value0, transformString value1)
				{
					return new TransformLet(this._builders, value0, value1);
				}

				// Token: 0x040053AF RID: 21423
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A05 RID: 6661
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0600D9A2 RID: 55714 RVA: 0x002E89FE File Offset: 0x002E6BFE
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600D9A3 RID: 55715 RVA: 0x002E8A0D File Offset: 0x002E6C0D
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output output_v(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v value0)
				{
					return new output_v(this._builders, value0);
				}

				// Token: 0x0600D9A4 RID: 55716 RVA: 0x002E8A20 File Offset: 0x002E6C20
				public value value_object(@object value0)
				{
					return new value_object(this._builders, value0);
				}

				// Token: 0x0600D9A5 RID: 55717 RVA: 0x002E8A33 File Offset: 0x002E6C33
				public value value_array(array value0)
				{
					return new value_array(this._builders, value0);
				}

				// Token: 0x0600D9A6 RID: 55718 RVA: 0x002E8A46 File Offset: 0x002E6C46
				public array array_selectArray(selectArray value0)
				{
					return new array_selectArray(this._builders, value0);
				}

				// Token: 0x0600D9A7 RID: 55719 RVA: 0x002E8A59 File Offset: 0x002E6C59
				public selectOrTransformValue selectOrTransformValue_selectValue(selectValue value0)
				{
					return new selectOrTransformValue_selectValue(this._builders, value0);
				}

				// Token: 0x0600D9A8 RID: 55720 RVA: 0x002E8A6C File Offset: 0x002E6C6C
				public selectOrTransformValue selectOrTransformValue_transformValue(transformValue value0)
				{
					return new selectOrTransformValue_transformValue(this._builders, value0);
				}

				// Token: 0x040053B0 RID: 21424
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A06 RID: 6662
			public class NodeVariables
			{
				// Token: 0x1700249F RID: 9375
				// (get) Token: 0x0600D9A9 RID: 55721 RVA: 0x002E8A7F File Offset: 0x002E6C7F
				// (set) Token: 0x0600D9AA RID: 55722 RVA: 0x002E8A87 File Offset: 0x002E6C87
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v v { get; private set; }

				// Token: 0x170024A0 RID: 9376
				// (get) Token: 0x0600D9AB RID: 55723 RVA: 0x002E8A90 File Offset: 0x002E6C90
				// (set) Token: 0x0600D9AC RID: 55724 RVA: 0x002E8A98 File Offset: 0x002E6C98
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x x { get; private set; }

				// Token: 0x170024A1 RID: 9377
				// (get) Token: 0x0600D9AD RID: 55725 RVA: 0x002E8AA1 File Offset: 0x002E6CA1
				// (set) Token: 0x0600D9AE RID: 55726 RVA: 0x002E8AA9 File Offset: 0x002E6CA9
				public row row { get; private set; }

				// Token: 0x0600D9AF RID: 55727 RVA: 0x002E8AB2 File Offset: 0x002E6CB2
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v(builders);
					this.x = new Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x(builders);
					this.row = new row(builders);
				}
			}

			// Token: 0x02001A07 RID: 6663
			public class NodeHoles
			{
				// Token: 0x170024A2 RID: 9378
				// (get) Token: 0x0600D9B0 RID: 55728 RVA: 0x002E8ADE File Offset: 0x002E6CDE
				// (set) Token: 0x0600D9B1 RID: 55729 RVA: 0x002E8AE6 File Offset: 0x002E6CE6
				public path path { get; private set; }

				// Token: 0x170024A3 RID: 9379
				// (get) Token: 0x0600D9B2 RID: 55730 RVA: 0x002E8AEF File Offset: 0x002E6CEF
				// (set) Token: 0x0600D9B3 RID: 55731 RVA: 0x002E8AF7 File Offset: 0x002E6CF7
				public str str { get; private set; }

				// Token: 0x170024A4 RID: 9380
				// (get) Token: 0x0600D9B4 RID: 55732 RVA: 0x002E8B00 File Offset: 0x002E6D00
				// (set) Token: 0x0600D9B5 RID: 55733 RVA: 0x002E8B08 File Offset: 0x002E6D08
				public t t { get; private set; }

				// Token: 0x170024A5 RID: 9381
				// (get) Token: 0x0600D9B6 RID: 55734 RVA: 0x002E8B11 File Offset: 0x002E6D11
				// (set) Token: 0x0600D9B7 RID: 55735 RVA: 0x002E8B19 File Offset: 0x002E6D19
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output output { get; private set; }

				// Token: 0x170024A6 RID: 9382
				// (get) Token: 0x0600D9B8 RID: 55736 RVA: 0x002E8B22 File Offset: 0x002E6D22
				// (set) Token: 0x0600D9B9 RID: 55737 RVA: 0x002E8B2A File Offset: 0x002E6D2A
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x x { get; private set; }

				// Token: 0x170024A7 RID: 9383
				// (get) Token: 0x0600D9BA RID: 55738 RVA: 0x002E8B33 File Offset: 0x002E6D33
				// (set) Token: 0x0600D9BB RID: 55739 RVA: 0x002E8B3B File Offset: 0x002E6D3B
				public value value { get; private set; }

				// Token: 0x170024A8 RID: 9384
				// (get) Token: 0x0600D9BC RID: 55740 RVA: 0x002E8B44 File Offset: 0x002E6D44
				// (set) Token: 0x0600D9BD RID: 55741 RVA: 0x002E8B4C File Offset: 0x002E6D4C
				public @object @object { get; private set; }

				// Token: 0x170024A9 RID: 9385
				// (get) Token: 0x0600D9BE RID: 55742 RVA: 0x002E8B55 File Offset: 0x002E6D55
				// (set) Token: 0x0600D9BF RID: 55743 RVA: 0x002E8B5D File Offset: 0x002E6D5D
				public array array { get; private set; }

				// Token: 0x170024AA RID: 9386
				// (get) Token: 0x0600D9C0 RID: 55744 RVA: 0x002E8B66 File Offset: 0x002E6D66
				// (set) Token: 0x0600D9C1 RID: 55745 RVA: 0x002E8B6E File Offset: 0x002E6D6E
				public property property { get; private set; }

				// Token: 0x170024AB RID: 9387
				// (get) Token: 0x0600D9C2 RID: 55746 RVA: 0x002E8B77 File Offset: 0x002E6D77
				// (set) Token: 0x0600D9C3 RID: 55747 RVA: 0x002E8B7F File Offset: 0x002E6D7F
				public elements elements { get; private set; }

				// Token: 0x170024AC RID: 9388
				// (get) Token: 0x0600D9C4 RID: 55748 RVA: 0x002E8B88 File Offset: 0x002E6D88
				// (set) Token: 0x0600D9C5 RID: 55749 RVA: 0x002E8B90 File Offset: 0x002E6D90
				public key key { get; private set; }

				// Token: 0x170024AD RID: 9389
				// (get) Token: 0x0600D9C6 RID: 55750 RVA: 0x002E8B99 File Offset: 0x002E6D99
				// (set) Token: 0x0600D9C7 RID: 55751 RVA: 0x002E8BA1 File Offset: 0x002E6DA1
				public selectKey selectKey { get; private set; }

				// Token: 0x170024AE RID: 9390
				// (get) Token: 0x0600D9C8 RID: 55752 RVA: 0x002E8BAA File Offset: 0x002E6DAA
				// (set) Token: 0x0600D9C9 RID: 55753 RVA: 0x002E8BB2 File Offset: 0x002E6DB2
				public selectOrTransformValue selectOrTransformValue { get; private set; }

				// Token: 0x170024AF RID: 9391
				// (get) Token: 0x0600D9CA RID: 55754 RVA: 0x002E8BBB File Offset: 0x002E6DBB
				// (set) Token: 0x0600D9CB RID: 55755 RVA: 0x002E8BC3 File Offset: 0x002E6DC3
				public selectValue selectValue { get; private set; }

				// Token: 0x170024B0 RID: 9392
				// (get) Token: 0x0600D9CC RID: 55756 RVA: 0x002E8BCC File Offset: 0x002E6DCC
				// (set) Token: 0x0600D9CD RID: 55757 RVA: 0x002E8BD4 File Offset: 0x002E6DD4
				public transformValue transformValue { get; private set; }

				// Token: 0x170024B1 RID: 9393
				// (get) Token: 0x0600D9CE RID: 55758 RVA: 0x002E8BDD File Offset: 0x002E6DDD
				// (set) Token: 0x0600D9CF RID: 55759 RVA: 0x002E8BE5 File Offset: 0x002E6DE5
				public transformLet transformLet { get; private set; }

				// Token: 0x170024B2 RID: 9394
				// (get) Token: 0x0600D9D0 RID: 55760 RVA: 0x002E8BEE File Offset: 0x002E6DEE
				// (set) Token: 0x0600D9D1 RID: 55761 RVA: 0x002E8BF6 File Offset: 0x002E6DF6
				public row row { get; private set; }

				// Token: 0x170024B3 RID: 9395
				// (get) Token: 0x0600D9D2 RID: 55762 RVA: 0x002E8BFF File Offset: 0x002E6DFF
				// (set) Token: 0x0600D9D3 RID: 55763 RVA: 0x002E8C07 File Offset: 0x002E6E07
				public transformString transformString { get; private set; }

				// Token: 0x170024B4 RID: 9396
				// (get) Token: 0x0600D9D4 RID: 55764 RVA: 0x002E8C10 File Offset: 0x002E6E10
				// (set) Token: 0x0600D9D5 RID: 55765 RVA: 0x002E8C18 File Offset: 0x002E6E18
				public selectArray selectArray { get; private set; }

				// Token: 0x170024B5 RID: 9397
				// (get) Token: 0x0600D9D6 RID: 55766 RVA: 0x002E8C21 File Offset: 0x002E6E21
				// (set) Token: 0x0600D9D7 RID: 55767 RVA: 0x002E8C29 File Offset: 0x002E6E29
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 _LetB0 { get; private set; }

				// Token: 0x0600D9D8 RID: 55768 RVA: 0x002E8C34 File Offset: 0x002E6E34
				public NodeHoles(GrammarBuilders builders)
				{
					this.path = path.CreateHole(builders, null);
					this.str = str.CreateHole(builders, null);
					this.t = t.CreateHole(builders, null);
					this.output = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateHole(builders, null);
					this.x = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateHole(builders, null);
					this.value = value.CreateHole(builders, null);
					this.@object = @object.CreateHole(builders, null);
					this.array = array.CreateHole(builders, null);
					this.property = property.CreateHole(builders, null);
					this.elements = elements.CreateHole(builders, null);
					this.key = key.CreateHole(builders, null);
					this.selectKey = selectKey.CreateHole(builders, null);
					this.selectOrTransformValue = selectOrTransformValue.CreateHole(builders, null);
					this.selectValue = selectValue.CreateHole(builders, null);
					this.transformValue = transformValue.CreateHole(builders, null);
					this.transformLet = transformLet.CreateHole(builders, null);
					this.row = row.CreateHole(builders, null);
					this.transformString = transformString.CreateHole(builders, null);
					this.selectArray = selectArray.CreateHole(builders, null);
					this._LetB0 = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateHole(builders, null);
				}
			}

			// Token: 0x02001A08 RID: 6664
			public class NodeUnsafe
			{
				// Token: 0x0600D9D9 RID: 55769 RVA: 0x002E8D4B File Offset: 0x002E6F4B
				public path path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path.CreateUnsafe(node);
				}

				// Token: 0x0600D9DA RID: 55770 RVA: 0x002E8D53 File Offset: 0x002E6F53
				public str str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str.CreateUnsafe(node);
				}

				// Token: 0x0600D9DB RID: 55771 RVA: 0x002E8D5B File Offset: 0x002E6F5B
				public t t(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t.CreateUnsafe(node);
				}

				// Token: 0x0600D9DC RID: 55772 RVA: 0x002E8D63 File Offset: 0x002E6F63
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateUnsafe(node);
				}

				// Token: 0x0600D9DD RID: 55773 RVA: 0x002E8D6B File Offset: 0x002E6F6B
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateUnsafe(node);
				}

				// Token: 0x0600D9DE RID: 55774 RVA: 0x002E8D73 File Offset: 0x002E6F73
				public value value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value.CreateUnsafe(node);
				}

				// Token: 0x0600D9DF RID: 55775 RVA: 0x002E8D7B File Offset: 0x002E6F7B
				public @object @object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object.CreateUnsafe(node);
				}

				// Token: 0x0600D9E0 RID: 55776 RVA: 0x002E8D83 File Offset: 0x002E6F83
				public array array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array.CreateUnsafe(node);
				}

				// Token: 0x0600D9E1 RID: 55777 RVA: 0x002E8D8B File Offset: 0x002E6F8B
				public property property(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property.CreateUnsafe(node);
				}

				// Token: 0x0600D9E2 RID: 55778 RVA: 0x002E8D93 File Offset: 0x002E6F93
				public elements elements(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements.CreateUnsafe(node);
				}

				// Token: 0x0600D9E3 RID: 55779 RVA: 0x002E8D9B File Offset: 0x002E6F9B
				public key key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key.CreateUnsafe(node);
				}

				// Token: 0x0600D9E4 RID: 55780 RVA: 0x002E8DA3 File Offset: 0x002E6FA3
				public selectKey selectKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey.CreateUnsafe(node);
				}

				// Token: 0x0600D9E5 RID: 55781 RVA: 0x002E8DAB File Offset: 0x002E6FAB
				public selectOrTransformValue selectOrTransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue.CreateUnsafe(node);
				}

				// Token: 0x0600D9E6 RID: 55782 RVA: 0x002E8DB3 File Offset: 0x002E6FB3
				public selectValue selectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue.CreateUnsafe(node);
				}

				// Token: 0x0600D9E7 RID: 55783 RVA: 0x002E8DBB File Offset: 0x002E6FBB
				public transformValue transformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue.CreateUnsafe(node);
				}

				// Token: 0x0600D9E8 RID: 55784 RVA: 0x002E8DC3 File Offset: 0x002E6FC3
				public transformLet transformLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet.CreateUnsafe(node);
				}

				// Token: 0x0600D9E9 RID: 55785 RVA: 0x002E8DCB File Offset: 0x002E6FCB
				public row row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row.CreateUnsafe(node);
				}

				// Token: 0x0600D9EA RID: 55786 RVA: 0x002E8DD3 File Offset: 0x002E6FD3
				public transformString transformString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString.CreateUnsafe(node);
				}

				// Token: 0x0600D9EB RID: 55787 RVA: 0x002E8DDB File Offset: 0x002E6FDB
				public selectArray selectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray.CreateUnsafe(node);
				}

				// Token: 0x0600D9EC RID: 55788 RVA: 0x002E8DE3 File Offset: 0x002E6FE3
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateUnsafe(node);
				}
			}

			// Token: 0x02001A09 RID: 6665
			public class NodeCast
			{
				// Token: 0x0600D9EE RID: 55790 RVA: 0x002E8DEB File Offset: 0x002E6FEB
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600D9EF RID: 55791 RVA: 0x002E8DFC File Offset: 0x002E6FFC
				public path path(ProgramNode node)
				{
					path? path = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						string text = "node";
						string text2 = "expected node for symbol path but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return path.Value;
				}

				// Token: 0x0600D9F0 RID: 55792 RVA: 0x002E8E50 File Offset: 0x002E7050
				public str str(ProgramNode node)
				{
					str? str = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						string text = "node";
						string text2 = "expected node for symbol str but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return str.Value;
				}

				// Token: 0x0600D9F1 RID: 55793 RVA: 0x002E8EA4 File Offset: 0x002E70A4
				public t t(ProgramNode node)
				{
					t? t = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t.CreateSafe(this._builders, node);
					if (t == null)
					{
						string text = "node";
						string text2 = "expected node for symbol t but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return t.Value;
				}

				// Token: 0x0600D9F2 RID: 55794 RVA: 0x002E8EF8 File Offset: 0x002E70F8
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output output(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output? output = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x0600D9F3 RID: 55795 RVA: 0x002E8F4C File Offset: 0x002E714C
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x x(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x? x = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						string text = "node";
						string text2 = "expected node for symbol x but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return x.Value;
				}

				// Token: 0x0600D9F4 RID: 55796 RVA: 0x002E8FA0 File Offset: 0x002E71A0
				public value value(ProgramNode node)
				{
					value? value = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @value but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value.Value;
				}

				// Token: 0x0600D9F5 RID: 55797 RVA: 0x002E8FF4 File Offset: 0x002E71F4
				public @object @object(ProgramNode node)
				{
					@object? @object = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object.CreateSafe(this._builders, node);
					if (@object == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @object but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @object.Value;
				}

				// Token: 0x0600D9F6 RID: 55798 RVA: 0x002E9048 File Offset: 0x002E7248
				public array array(ProgramNode node)
				{
					array? array = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array.CreateSafe(this._builders, node);
					if (array == null)
					{
						string text = "node";
						string text2 = "expected node for symbol array but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return array.Value;
				}

				// Token: 0x0600D9F7 RID: 55799 RVA: 0x002E909C File Offset: 0x002E729C
				public property property(ProgramNode node)
				{
					property? property = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property.CreateSafe(this._builders, node);
					if (property == null)
					{
						string text = "node";
						string text2 = "expected node for symbol property but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return property.Value;
				}

				// Token: 0x0600D9F8 RID: 55800 RVA: 0x002E90F0 File Offset: 0x002E72F0
				public elements elements(ProgramNode node)
				{
					elements? elements = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements.CreateSafe(this._builders, node);
					if (elements == null)
					{
						string text = "node";
						string text2 = "expected node for symbol elements but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return elements.Value;
				}

				// Token: 0x0600D9F9 RID: 55801 RVA: 0x002E9144 File Offset: 0x002E7344
				public key key(ProgramNode node)
				{
					key? key = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key.CreateSafe(this._builders, node);
					if (key == null)
					{
						string text = "node";
						string text2 = "expected node for symbol key but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return key.Value;
				}

				// Token: 0x0600D9FA RID: 55802 RVA: 0x002E9198 File Offset: 0x002E7398
				public selectKey selectKey(ProgramNode node)
				{
					selectKey? selectKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey.CreateSafe(this._builders, node);
					if (selectKey == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectKey but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectKey.Value;
				}

				// Token: 0x0600D9FB RID: 55803 RVA: 0x002E91EC File Offset: 0x002E73EC
				public selectOrTransformValue selectOrTransformValue(ProgramNode node)
				{
					selectOrTransformValue? selectOrTransformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectOrTransformValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectOrTransformValue.Value;
				}

				// Token: 0x0600D9FC RID: 55804 RVA: 0x002E9240 File Offset: 0x002E7440
				public selectValue selectValue(ProgramNode node)
				{
					selectValue? selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue.CreateSafe(this._builders, node);
					if (selectValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectValue.Value;
				}

				// Token: 0x0600D9FD RID: 55805 RVA: 0x002E9294 File Offset: 0x002E7494
				public transformValue transformValue(ProgramNode node)
				{
					transformValue? transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue.CreateSafe(this._builders, node);
					if (transformValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol transformValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformValue.Value;
				}

				// Token: 0x0600D9FE RID: 55806 RVA: 0x002E92E8 File Offset: 0x002E74E8
				public transformLet transformLet(ProgramNode node)
				{
					transformLet? transformLet = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet.CreateSafe(this._builders, node);
					if (transformLet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol transformLet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformLet.Value;
				}

				// Token: 0x0600D9FF RID: 55807 RVA: 0x002E933C File Offset: 0x002E753C
				public row row(ProgramNode node)
				{
					row? row = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row.CreateSafe(this._builders, node);
					if (row == null)
					{
						string text = "node";
						string text2 = "expected node for symbol row but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return row.Value;
				}

				// Token: 0x0600DA00 RID: 55808 RVA: 0x002E9390 File Offset: 0x002E7590
				public transformString transformString(ProgramNode node)
				{
					transformString? transformString = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString.CreateSafe(this._builders, node);
					if (transformString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol transformString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformString.Value;
				}

				// Token: 0x0600DA01 RID: 55809 RVA: 0x002E93E4 File Offset: 0x002E75E4
				public selectArray selectArray(ProgramNode node)
				{
					selectArray? selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray.CreateSafe(this._builders, node);
					if (selectArray == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectArray but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectArray.Value;
				}

				// Token: 0x0600DA02 RID: 55810 RVA: 0x002E9438 File Offset: 0x002E7638
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 _LetB0(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _LetB0 but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return letB.Value;
				}

				// Token: 0x040053C8 RID: 21448
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A0A RID: 6666
			public class RuleCast
			{
				// Token: 0x0600DA03 RID: 55811 RVA: 0x002E9489 File Offset: 0x002E7689
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DA04 RID: 55812 RVA: 0x002E9498 File Offset: 0x002E7698
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output output(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output? output = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output.Value;
				}

				// Token: 0x0600DA05 RID: 55813 RVA: 0x002E94EC File Offset: 0x002E76EC
				public output_v output_v(ProgramNode node)
				{
					output_v? output_v = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.output_v.CreateSafe(this._builders, node);
					if (output_v == null)
					{
						string text = "node";
						string text2 = "expected node for symbol output_v but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return output_v.Value;
				}

				// Token: 0x0600DA06 RID: 55814 RVA: 0x002E9540 File Offset: 0x002E7740
				public value_object value_object(ProgramNode node)
				{
					value_object? value_object = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_object.CreateSafe(this._builders, node);
					if (value_object == null)
					{
						string text = "node";
						string text2 = "expected node for symbol value_object but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value_object.Value;
				}

				// Token: 0x0600DA07 RID: 55815 RVA: 0x002E9594 File Offset: 0x002E7794
				public value_array value_array(ProgramNode node)
				{
					value_array? value_array = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_array.CreateSafe(this._builders, node);
					if (value_array == null)
					{
						string text = "node";
						string text2 = "expected node for symbol value_array but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value_array.Value;
				}

				// Token: 0x0600DA08 RID: 55816 RVA: 0x002E95E8 File Offset: 0x002E77E8
				public SelectOrTransformValue SelectOrTransformValue(ProgramNode node)
				{
					SelectOrTransformValue? selectOrTransformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectOrTransformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectOrTransformValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectOrTransformValue.Value;
				}

				// Token: 0x0600DA09 RID: 55817 RVA: 0x002E963C File Offset: 0x002E783C
				public _Value _Value(ProgramNode node)
				{
					_Value? value = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes._Value.CreateSafe(this._builders, node);
					if (value == null)
					{
						string text = "node";
						string text2 = "expected node for symbol _Value but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value.Value;
				}

				// Token: 0x0600DA0A RID: 55818 RVA: 0x002E9690 File Offset: 0x002E7890
				public ConstValue ConstValue(ProgramNode node)
				{
					ConstValue? constValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstValue.CreateSafe(this._builders, node);
					if (constValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constValue.Value;
				}

				// Token: 0x0600DA0B RID: 55819 RVA: 0x002E96E4 File Offset: 0x002E78E4
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object Object(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object? @object = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateSafe(this._builders, node);
					if (@object == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Object but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return @object.Value;
				}

				// Token: 0x0600DA0C RID: 55820 RVA: 0x002E9738 File Offset: 0x002E7938
				public Append Append(ProgramNode node)
				{
					Append? append = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
					if (append == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Append but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return append.Value;
				}

				// Token: 0x0600DA0D RID: 55821 RVA: 0x002E978C File Offset: 0x002E798C
				public SelectObject SelectObject(ProgramNode node)
				{
					SelectObject? selectObject = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectObject.CreateSafe(this._builders, node);
					if (selectObject == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectObject but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectObject.Value;
				}

				// Token: 0x0600DA0E RID: 55822 RVA: 0x002E97E0 File Offset: 0x002E79E0
				public FlattenObject FlattenObject(ProgramNode node)
				{
					FlattenObject? flattenObject = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.FlattenObject.CreateSafe(this._builders, node);
					if (flattenObject == null)
					{
						string text = "node";
						string text2 = "expected node for symbol FlattenObject but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return flattenObject.Value;
				}

				// Token: 0x0600DA0F RID: 55823 RVA: 0x002E9834 File Offset: 0x002E7A34
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array Array(ProgramNode node)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array? array = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateSafe(this._builders, node);
					if (array == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Array but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return array.Value;
				}

				// Token: 0x0600DA10 RID: 55824 RVA: 0x002E9888 File Offset: 0x002E7A88
				public array_selectArray array_selectArray(ProgramNode node)
				{
					array_selectArray? array_selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.array_selectArray.CreateSafe(this._builders, node);
					if (array_selectArray == null)
					{
						string text = "node";
						string text2 = "expected node for symbol array_selectArray but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return array_selectArray.Value;
				}

				// Token: 0x0600DA11 RID: 55825 RVA: 0x002E98DC File Offset: 0x002E7ADC
				public Property Property(ProgramNode node)
				{
					Property? property = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Property.CreateSafe(this._builders, node);
					if (property == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Property but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return property.Value;
				}

				// Token: 0x0600DA12 RID: 55826 RVA: 0x002E9930 File Offset: 0x002E7B30
				public SelectProperty SelectProperty(ProgramNode node)
				{
					SelectProperty? selectProperty = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectProperty.CreateSafe(this._builders, node);
					if (selectProperty == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectProperty but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectProperty.Value;
				}

				// Token: 0x0600DA13 RID: 55827 RVA: 0x002E9984 File Offset: 0x002E7B84
				public Transform Transform(ProgramNode node)
				{
					Transform? transform = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Transform.CreateSafe(this._builders, node);
					if (transform == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Transform but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transform.Value;
				}

				// Token: 0x0600DA14 RID: 55828 RVA: 0x002E99D8 File Offset: 0x002E7BD8
				public TransformFlatten TransformFlatten(ProgramNode node)
				{
					TransformFlatten? transformFlatten = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformFlatten.CreateSafe(this._builders, node);
					if (transformFlatten == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TransformFlatten but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformFlatten.Value;
				}

				// Token: 0x0600DA15 RID: 55829 RVA: 0x002E9A2C File Offset: 0x002E7C2C
				public Key Key(ProgramNode node)
				{
					Key? key = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Key.CreateSafe(this._builders, node);
					if (key == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Key but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return key.Value;
				}

				// Token: 0x0600DA16 RID: 55830 RVA: 0x002E9A80 File Offset: 0x002E7C80
				public ConstKey ConstKey(ProgramNode node)
				{
					ConstKey? constKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstKey.CreateSafe(this._builders, node);
					if (constKey == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstKey but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constKey.Value;
				}

				// Token: 0x0600DA17 RID: 55831 RVA: 0x002E9AD4 File Offset: 0x002E7CD4
				public SelectKey SelectKey(ProgramNode node)
				{
					SelectKey? selectKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectKey.CreateSafe(this._builders, node);
					if (selectKey == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectKey but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectKey.Value;
				}

				// Token: 0x0600DA18 RID: 55832 RVA: 0x002E9B28 File Offset: 0x002E7D28
				public selectOrTransformValue_selectValue selectOrTransformValue_selectValue(ProgramNode node)
				{
					selectOrTransformValue_selectValue? selectOrTransformValue_selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_selectValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue_selectValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectOrTransformValue_selectValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectOrTransformValue_selectValue.Value;
				}

				// Token: 0x0600DA19 RID: 55833 RVA: 0x002E9B7C File Offset: 0x002E7D7C
				public selectOrTransformValue_transformValue selectOrTransformValue_transformValue(ProgramNode node)
				{
					selectOrTransformValue_transformValue? selectOrTransformValue_transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_transformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue_transformValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectOrTransformValue_transformValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectOrTransformValue_transformValue.Value;
				}

				// Token: 0x0600DA1A RID: 55834 RVA: 0x002E9BD0 File Offset: 0x002E7DD0
				public SelectValue SelectValue(ProgramNode node)
				{
					SelectValue? selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectValue.CreateSafe(this._builders, node);
					if (selectValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectValue.Value;
				}

				// Token: 0x0600DA1B RID: 55835 RVA: 0x002E9C24 File Offset: 0x002E7E24
				public ValueToString ValueToString(ProgramNode node)
				{
					ValueToString? valueToString = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ValueToString.CreateSafe(this._builders, node);
					if (valueToString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ValueToString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return valueToString.Value;
				}

				// Token: 0x0600DA1C RID: 55836 RVA: 0x002E9C78 File Offset: 0x002E7E78
				public ConvertValueTo ConvertValueTo(ProgramNode node)
				{
					ConvertValueTo? convertValueTo = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConvertValueTo.CreateSafe(this._builders, node);
					if (convertValueTo == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConvertValueTo but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return convertValueTo.Value;
				}

				// Token: 0x0600DA1D RID: 55837 RVA: 0x002E9CCC File Offset: 0x002E7ECC
				public TransformValue TransformValue(ProgramNode node)
				{
					TransformValue? transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformValue.CreateSafe(this._builders, node);
					if (transformValue == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TransformValue but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformValue.Value;
				}

				// Token: 0x0600DA1E RID: 55838 RVA: 0x002E9D20 File Offset: 0x002E7F20
				public SelectStringValues SelectStringValues(ProgramNode node)
				{
					SelectStringValues? selectStringValues = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectStringValues.CreateSafe(this._builders, node);
					if (selectStringValues == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectStringValues but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectStringValues.Value;
				}

				// Token: 0x0600DA1F RID: 55839 RVA: 0x002E9D74 File Offset: 0x002E7F74
				public TransformLet TransformLet(ProgramNode node)
				{
					TransformLet? transformLet = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformLet.CreateSafe(this._builders, node);
					if (transformLet == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TransformLet but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformLet.Value;
				}

				// Token: 0x0600DA20 RID: 55840 RVA: 0x002E9DC8 File Offset: 0x002E7FC8
				public TransformString TransformString(ProgramNode node)
				{
					TransformString? transformString = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformString.CreateSafe(this._builders, node);
					if (transformString == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TransformString but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return transformString.Value;
				}

				// Token: 0x0600DA21 RID: 55841 RVA: 0x002E9E1C File Offset: 0x002E801C
				public SelectArray SelectArray(ProgramNode node)
				{
					SelectArray? selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectArray.CreateSafe(this._builders, node);
					if (selectArray == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SelectArray but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectArray.Value;
				}

				// Token: 0x0600DA22 RID: 55842 RVA: 0x002E9E70 File Offset: 0x002E8070
				public ToArray ToArray(ProgramNode node)
				{
					ToArray? toArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ToArray.CreateSafe(this._builders, node);
					if (toArray == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ToArray but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return toArray.Value;
				}

				// Token: 0x040053C9 RID: 21449
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A0B RID: 6667
			public class NodeIs
			{
				// Token: 0x0600DA23 RID: 55843 RVA: 0x002E9EC1 File Offset: 0x002E80C1
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DA24 RID: 55844 RVA: 0x002E9ED0 File Offset: 0x002E80D0
				public bool path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA25 RID: 55845 RVA: 0x002E9EF4 File Offset: 0x002E80F4
				public bool path(ProgramNode node, out path value)
				{
					path? path = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						value = default(path);
						return false;
					}
					value = path.Value;
					return true;
				}

				// Token: 0x0600DA26 RID: 55846 RVA: 0x002E9F30 File Offset: 0x002E8130
				public bool str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA27 RID: 55847 RVA: 0x002E9F54 File Offset: 0x002E8154
				public bool str(ProgramNode node, out str value)
				{
					str? str = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str.CreateSafe(this._builders, node);
					if (str == null)
					{
						value = default(str);
						return false;
					}
					value = str.Value;
					return true;
				}

				// Token: 0x0600DA28 RID: 55848 RVA: 0x002E9F90 File Offset: 0x002E8190
				public bool t(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA29 RID: 55849 RVA: 0x002E9FB4 File Offset: 0x002E81B4
				public bool t(ProgramNode node, out t value)
				{
					t? t = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t.CreateSafe(this._builders, node);
					if (t == null)
					{
						value = default(t);
						return false;
					}
					value = t.Value;
					return true;
				}

				// Token: 0x0600DA2A RID: 55850 RVA: 0x002E9FF0 File Offset: 0x002E81F0
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA2B RID: 55851 RVA: 0x002EA014 File Offset: 0x002E8214
				public bool output(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output? output = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x0600DA2C RID: 55852 RVA: 0x002EA050 File Offset: 0x002E8250
				public bool x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA2D RID: 55853 RVA: 0x002EA074 File Offset: 0x002E8274
				public bool x(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x? x = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x);
						return false;
					}
					value = x.Value;
					return true;
				}

				// Token: 0x0600DA2E RID: 55854 RVA: 0x002EA0B0 File Offset: 0x002E82B0
				public bool value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA2F RID: 55855 RVA: 0x002EA0D4 File Offset: 0x002E82D4
				public bool value(ProgramNode node, out value value)
				{
					value? value2 = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value2 == null)
					{
						value = default(value);
						return false;
					}
					value = value2.Value;
					return true;
				}

				// Token: 0x0600DA30 RID: 55856 RVA: 0x002EA110 File Offset: 0x002E8310
				public bool @object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA31 RID: 55857 RVA: 0x002EA134 File Offset: 0x002E8334
				public bool @object(ProgramNode node, out @object value)
				{
					@object? @object = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object.CreateSafe(this._builders, node);
					if (@object == null)
					{
						value = default(@object);
						return false;
					}
					value = @object.Value;
					return true;
				}

				// Token: 0x0600DA32 RID: 55858 RVA: 0x002EA170 File Offset: 0x002E8370
				public bool array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA33 RID: 55859 RVA: 0x002EA194 File Offset: 0x002E8394
				public bool array(ProgramNode node, out array value)
				{
					array? array = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array.CreateSafe(this._builders, node);
					if (array == null)
					{
						value = default(array);
						return false;
					}
					value = array.Value;
					return true;
				}

				// Token: 0x0600DA34 RID: 55860 RVA: 0x002EA1D0 File Offset: 0x002E83D0
				public bool property(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA35 RID: 55861 RVA: 0x002EA1F4 File Offset: 0x002E83F4
				public bool property(ProgramNode node, out property value)
				{
					property? property = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property.CreateSafe(this._builders, node);
					if (property == null)
					{
						value = default(property);
						return false;
					}
					value = property.Value;
					return true;
				}

				// Token: 0x0600DA36 RID: 55862 RVA: 0x002EA230 File Offset: 0x002E8430
				public bool elements(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA37 RID: 55863 RVA: 0x002EA254 File Offset: 0x002E8454
				public bool elements(ProgramNode node, out elements value)
				{
					elements? elements = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements.CreateSafe(this._builders, node);
					if (elements == null)
					{
						value = default(elements);
						return false;
					}
					value = elements.Value;
					return true;
				}

				// Token: 0x0600DA38 RID: 55864 RVA: 0x002EA290 File Offset: 0x002E8490
				public bool key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA39 RID: 55865 RVA: 0x002EA2B4 File Offset: 0x002E84B4
				public bool key(ProgramNode node, out key value)
				{
					key? key = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key.CreateSafe(this._builders, node);
					if (key == null)
					{
						value = default(key);
						return false;
					}
					value = key.Value;
					return true;
				}

				// Token: 0x0600DA3A RID: 55866 RVA: 0x002EA2F0 File Offset: 0x002E84F0
				public bool selectKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA3B RID: 55867 RVA: 0x002EA314 File Offset: 0x002E8514
				public bool selectKey(ProgramNode node, out selectKey value)
				{
					selectKey? selectKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey.CreateSafe(this._builders, node);
					if (selectKey == null)
					{
						value = default(selectKey);
						return false;
					}
					value = selectKey.Value;
					return true;
				}

				// Token: 0x0600DA3C RID: 55868 RVA: 0x002EA350 File Offset: 0x002E8550
				public bool selectOrTransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA3D RID: 55869 RVA: 0x002EA374 File Offset: 0x002E8574
				public bool selectOrTransformValue(ProgramNode node, out selectOrTransformValue value)
				{
					selectOrTransformValue? selectOrTransformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue == null)
					{
						value = default(selectOrTransformValue);
						return false;
					}
					value = selectOrTransformValue.Value;
					return true;
				}

				// Token: 0x0600DA3E RID: 55870 RVA: 0x002EA3B0 File Offset: 0x002E85B0
				public bool selectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA3F RID: 55871 RVA: 0x002EA3D4 File Offset: 0x002E85D4
				public bool selectValue(ProgramNode node, out selectValue value)
				{
					selectValue? selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue.CreateSafe(this._builders, node);
					if (selectValue == null)
					{
						value = default(selectValue);
						return false;
					}
					value = selectValue.Value;
					return true;
				}

				// Token: 0x0600DA40 RID: 55872 RVA: 0x002EA410 File Offset: 0x002E8610
				public bool transformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA41 RID: 55873 RVA: 0x002EA434 File Offset: 0x002E8634
				public bool transformValue(ProgramNode node, out transformValue value)
				{
					transformValue? transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue.CreateSafe(this._builders, node);
					if (transformValue == null)
					{
						value = default(transformValue);
						return false;
					}
					value = transformValue.Value;
					return true;
				}

				// Token: 0x0600DA42 RID: 55874 RVA: 0x002EA470 File Offset: 0x002E8670
				public bool transformLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA43 RID: 55875 RVA: 0x002EA494 File Offset: 0x002E8694
				public bool transformLet(ProgramNode node, out transformLet value)
				{
					transformLet? transformLet = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet.CreateSafe(this._builders, node);
					if (transformLet == null)
					{
						value = default(transformLet);
						return false;
					}
					value = transformLet.Value;
					return true;
				}

				// Token: 0x0600DA44 RID: 55876 RVA: 0x002EA4D0 File Offset: 0x002E86D0
				public bool row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA45 RID: 55877 RVA: 0x002EA4F4 File Offset: 0x002E86F4
				public bool row(ProgramNode node, out row value)
				{
					row? row = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row.CreateSafe(this._builders, node);
					if (row == null)
					{
						value = default(row);
						return false;
					}
					value = row.Value;
					return true;
				}

				// Token: 0x0600DA46 RID: 55878 RVA: 0x002EA530 File Offset: 0x002E8730
				public bool transformString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA47 RID: 55879 RVA: 0x002EA554 File Offset: 0x002E8754
				public bool transformString(ProgramNode node, out transformString value)
				{
					transformString? transformString = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString.CreateSafe(this._builders, node);
					if (transformString == null)
					{
						value = default(transformString);
						return false;
					}
					value = transformString.Value;
					return true;
				}

				// Token: 0x0600DA48 RID: 55880 RVA: 0x002EA590 File Offset: 0x002E8790
				public bool selectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA49 RID: 55881 RVA: 0x002EA5B4 File Offset: 0x002E87B4
				public bool selectArray(ProgramNode node, out selectArray value)
				{
					selectArray? selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray.CreateSafe(this._builders, node);
					if (selectArray == null)
					{
						value = default(selectArray);
						return false;
					}
					value = selectArray.Value;
					return true;
				}

				// Token: 0x0600DA4A RID: 55882 RVA: 0x002EA5F0 File Offset: 0x002E87F0
				public bool _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA4B RID: 55883 RVA: 0x002EA614 File Offset: 0x002E8814
				public bool _LetB0(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0 value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0? letB = Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
					if (letB == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0);
						return false;
					}
					value = letB.Value;
					return true;
				}

				// Token: 0x040053CA RID: 21450
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A0C RID: 6668
			public class RuleIs
			{
				// Token: 0x0600DA4C RID: 55884 RVA: 0x002EA64E File Offset: 0x002E884E
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DA4D RID: 55885 RVA: 0x002EA660 File Offset: 0x002E8860
				public bool output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA4E RID: 55886 RVA: 0x002EA684 File Offset: 0x002E8884
				public bool output(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output? output = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output.CreateSafe(this._builders, node);
					if (output == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output);
						return false;
					}
					value = output.Value;
					return true;
				}

				// Token: 0x0600DA4F RID: 55887 RVA: 0x002EA6C0 File Offset: 0x002E88C0
				public bool output_v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.output_v.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA50 RID: 55888 RVA: 0x002EA6E4 File Offset: 0x002E88E4
				public bool output_v(ProgramNode node, out output_v value)
				{
					output_v? output_v = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.output_v.CreateSafe(this._builders, node);
					if (output_v == null)
					{
						value = default(output_v);
						return false;
					}
					value = output_v.Value;
					return true;
				}

				// Token: 0x0600DA51 RID: 55889 RVA: 0x002EA720 File Offset: 0x002E8920
				public bool value_object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_object.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA52 RID: 55890 RVA: 0x002EA744 File Offset: 0x002E8944
				public bool value_object(ProgramNode node, out value_object value)
				{
					value_object? value_object = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_object.CreateSafe(this._builders, node);
					if (value_object == null)
					{
						value = default(value_object);
						return false;
					}
					value = value_object.Value;
					return true;
				}

				// Token: 0x0600DA53 RID: 55891 RVA: 0x002EA780 File Offset: 0x002E8980
				public bool value_array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_array.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA54 RID: 55892 RVA: 0x002EA7A4 File Offset: 0x002E89A4
				public bool value_array(ProgramNode node, out value_array value)
				{
					value_array? value_array = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_array.CreateSafe(this._builders, node);
					if (value_array == null)
					{
						value = default(value_array);
						return false;
					}
					value = value_array.Value;
					return true;
				}

				// Token: 0x0600DA55 RID: 55893 RVA: 0x002EA7E0 File Offset: 0x002E89E0
				public bool SelectOrTransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectOrTransformValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA56 RID: 55894 RVA: 0x002EA804 File Offset: 0x002E8A04
				public bool SelectOrTransformValue(ProgramNode node, out SelectOrTransformValue value)
				{
					SelectOrTransformValue? selectOrTransformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectOrTransformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue == null)
					{
						value = default(SelectOrTransformValue);
						return false;
					}
					value = selectOrTransformValue.Value;
					return true;
				}

				// Token: 0x0600DA57 RID: 55895 RVA: 0x002EA840 File Offset: 0x002E8A40
				public bool _Value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes._Value.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA58 RID: 55896 RVA: 0x002EA864 File Offset: 0x002E8A64
				public bool _Value(ProgramNode node, out _Value value)
				{
					_Value? value2 = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes._Value.CreateSafe(this._builders, node);
					if (value2 == null)
					{
						value = default(_Value);
						return false;
					}
					value = value2.Value;
					return true;
				}

				// Token: 0x0600DA59 RID: 55897 RVA: 0x002EA8A0 File Offset: 0x002E8AA0
				public bool ConstValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA5A RID: 55898 RVA: 0x002EA8C4 File Offset: 0x002E8AC4
				public bool ConstValue(ProgramNode node, out ConstValue value)
				{
					ConstValue? constValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstValue.CreateSafe(this._builders, node);
					if (constValue == null)
					{
						value = default(ConstValue);
						return false;
					}
					value = constValue.Value;
					return true;
				}

				// Token: 0x0600DA5B RID: 55899 RVA: 0x002EA900 File Offset: 0x002E8B00
				public bool Object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA5C RID: 55900 RVA: 0x002EA924 File Offset: 0x002E8B24
				public bool Object(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object? @object = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateSafe(this._builders, node);
					if (@object == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object);
						return false;
					}
					value = @object.Value;
					return true;
				}

				// Token: 0x0600DA5D RID: 55901 RVA: 0x002EA960 File Offset: 0x002E8B60
				public bool Append(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA5E RID: 55902 RVA: 0x002EA984 File Offset: 0x002E8B84
				public bool Append(ProgramNode node, out Append value)
				{
					Append? append = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
					if (append == null)
					{
						value = default(Append);
						return false;
					}
					value = append.Value;
					return true;
				}

				// Token: 0x0600DA5F RID: 55903 RVA: 0x002EA9C0 File Offset: 0x002E8BC0
				public bool SelectObject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectObject.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA60 RID: 55904 RVA: 0x002EA9E4 File Offset: 0x002E8BE4
				public bool SelectObject(ProgramNode node, out SelectObject value)
				{
					SelectObject? selectObject = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectObject.CreateSafe(this._builders, node);
					if (selectObject == null)
					{
						value = default(SelectObject);
						return false;
					}
					value = selectObject.Value;
					return true;
				}

				// Token: 0x0600DA61 RID: 55905 RVA: 0x002EAA20 File Offset: 0x002E8C20
				public bool FlattenObject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.FlattenObject.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA62 RID: 55906 RVA: 0x002EAA44 File Offset: 0x002E8C44
				public bool FlattenObject(ProgramNode node, out FlattenObject value)
				{
					FlattenObject? flattenObject = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.FlattenObject.CreateSafe(this._builders, node);
					if (flattenObject == null)
					{
						value = default(FlattenObject);
						return false;
					}
					value = flattenObject.Value;
					return true;
				}

				// Token: 0x0600DA63 RID: 55907 RVA: 0x002EAA80 File Offset: 0x002E8C80
				public bool Array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA64 RID: 55908 RVA: 0x002EAAA4 File Offset: 0x002E8CA4
				public bool Array(ProgramNode node, out Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array value)
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array? array = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateSafe(this._builders, node);
					if (array == null)
					{
						value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array);
						return false;
					}
					value = array.Value;
					return true;
				}

				// Token: 0x0600DA65 RID: 55909 RVA: 0x002EAAE0 File Offset: 0x002E8CE0
				public bool array_selectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.array_selectArray.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA66 RID: 55910 RVA: 0x002EAB04 File Offset: 0x002E8D04
				public bool array_selectArray(ProgramNode node, out array_selectArray value)
				{
					array_selectArray? array_selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.array_selectArray.CreateSafe(this._builders, node);
					if (array_selectArray == null)
					{
						value = default(array_selectArray);
						return false;
					}
					value = array_selectArray.Value;
					return true;
				}

				// Token: 0x0600DA67 RID: 55911 RVA: 0x002EAB40 File Offset: 0x002E8D40
				public bool Property(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Property.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA68 RID: 55912 RVA: 0x002EAB64 File Offset: 0x002E8D64
				public bool Property(ProgramNode node, out Property value)
				{
					Property? property = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Property.CreateSafe(this._builders, node);
					if (property == null)
					{
						value = default(Property);
						return false;
					}
					value = property.Value;
					return true;
				}

				// Token: 0x0600DA69 RID: 55913 RVA: 0x002EABA0 File Offset: 0x002E8DA0
				public bool SelectProperty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectProperty.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA6A RID: 55914 RVA: 0x002EABC4 File Offset: 0x002E8DC4
				public bool SelectProperty(ProgramNode node, out SelectProperty value)
				{
					SelectProperty? selectProperty = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectProperty.CreateSafe(this._builders, node);
					if (selectProperty == null)
					{
						value = default(SelectProperty);
						return false;
					}
					value = selectProperty.Value;
					return true;
				}

				// Token: 0x0600DA6B RID: 55915 RVA: 0x002EAC00 File Offset: 0x002E8E00
				public bool Transform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Transform.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA6C RID: 55916 RVA: 0x002EAC24 File Offset: 0x002E8E24
				public bool Transform(ProgramNode node, out Transform value)
				{
					Transform? transform = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Transform.CreateSafe(this._builders, node);
					if (transform == null)
					{
						value = default(Transform);
						return false;
					}
					value = transform.Value;
					return true;
				}

				// Token: 0x0600DA6D RID: 55917 RVA: 0x002EAC60 File Offset: 0x002E8E60
				public bool TransformFlatten(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformFlatten.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA6E RID: 55918 RVA: 0x002EAC84 File Offset: 0x002E8E84
				public bool TransformFlatten(ProgramNode node, out TransformFlatten value)
				{
					TransformFlatten? transformFlatten = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformFlatten.CreateSafe(this._builders, node);
					if (transformFlatten == null)
					{
						value = default(TransformFlatten);
						return false;
					}
					value = transformFlatten.Value;
					return true;
				}

				// Token: 0x0600DA6F RID: 55919 RVA: 0x002EACC0 File Offset: 0x002E8EC0
				public bool Key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Key.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA70 RID: 55920 RVA: 0x002EACE4 File Offset: 0x002E8EE4
				public bool Key(ProgramNode node, out Key value)
				{
					Key? key = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Key.CreateSafe(this._builders, node);
					if (key == null)
					{
						value = default(Key);
						return false;
					}
					value = key.Value;
					return true;
				}

				// Token: 0x0600DA71 RID: 55921 RVA: 0x002EAD20 File Offset: 0x002E8F20
				public bool ConstKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstKey.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA72 RID: 55922 RVA: 0x002EAD44 File Offset: 0x002E8F44
				public bool ConstKey(ProgramNode node, out ConstKey value)
				{
					ConstKey? constKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstKey.CreateSafe(this._builders, node);
					if (constKey == null)
					{
						value = default(ConstKey);
						return false;
					}
					value = constKey.Value;
					return true;
				}

				// Token: 0x0600DA73 RID: 55923 RVA: 0x002EAD80 File Offset: 0x002E8F80
				public bool SelectKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectKey.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA74 RID: 55924 RVA: 0x002EADA4 File Offset: 0x002E8FA4
				public bool SelectKey(ProgramNode node, out SelectKey value)
				{
					SelectKey? selectKey = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectKey.CreateSafe(this._builders, node);
					if (selectKey == null)
					{
						value = default(SelectKey);
						return false;
					}
					value = selectKey.Value;
					return true;
				}

				// Token: 0x0600DA75 RID: 55925 RVA: 0x002EADE0 File Offset: 0x002E8FE0
				public bool selectOrTransformValue_selectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_selectValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA76 RID: 55926 RVA: 0x002EAE04 File Offset: 0x002E9004
				public bool selectOrTransformValue_selectValue(ProgramNode node, out selectOrTransformValue_selectValue value)
				{
					selectOrTransformValue_selectValue? selectOrTransformValue_selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_selectValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue_selectValue == null)
					{
						value = default(selectOrTransformValue_selectValue);
						return false;
					}
					value = selectOrTransformValue_selectValue.Value;
					return true;
				}

				// Token: 0x0600DA77 RID: 55927 RVA: 0x002EAE40 File Offset: 0x002E9040
				public bool selectOrTransformValue_transformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_transformValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA78 RID: 55928 RVA: 0x002EAE64 File Offset: 0x002E9064
				public bool selectOrTransformValue_transformValue(ProgramNode node, out selectOrTransformValue_transformValue value)
				{
					selectOrTransformValue_transformValue? selectOrTransformValue_transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_transformValue.CreateSafe(this._builders, node);
					if (selectOrTransformValue_transformValue == null)
					{
						value = default(selectOrTransformValue_transformValue);
						return false;
					}
					value = selectOrTransformValue_transformValue.Value;
					return true;
				}

				// Token: 0x0600DA79 RID: 55929 RVA: 0x002EAEA0 File Offset: 0x002E90A0
				public bool SelectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA7A RID: 55930 RVA: 0x002EAEC4 File Offset: 0x002E90C4
				public bool SelectValue(ProgramNode node, out SelectValue value)
				{
					SelectValue? selectValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectValue.CreateSafe(this._builders, node);
					if (selectValue == null)
					{
						value = default(SelectValue);
						return false;
					}
					value = selectValue.Value;
					return true;
				}

				// Token: 0x0600DA7B RID: 55931 RVA: 0x002EAF00 File Offset: 0x002E9100
				public bool ValueToString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ValueToString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA7C RID: 55932 RVA: 0x002EAF24 File Offset: 0x002E9124
				public bool ValueToString(ProgramNode node, out ValueToString value)
				{
					ValueToString? valueToString = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ValueToString.CreateSafe(this._builders, node);
					if (valueToString == null)
					{
						value = default(ValueToString);
						return false;
					}
					value = valueToString.Value;
					return true;
				}

				// Token: 0x0600DA7D RID: 55933 RVA: 0x002EAF60 File Offset: 0x002E9160
				public bool ConvertValueTo(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConvertValueTo.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA7E RID: 55934 RVA: 0x002EAF84 File Offset: 0x002E9184
				public bool ConvertValueTo(ProgramNode node, out ConvertValueTo value)
				{
					ConvertValueTo? convertValueTo = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConvertValueTo.CreateSafe(this._builders, node);
					if (convertValueTo == null)
					{
						value = default(ConvertValueTo);
						return false;
					}
					value = convertValueTo.Value;
					return true;
				}

				// Token: 0x0600DA7F RID: 55935 RVA: 0x002EAFC0 File Offset: 0x002E91C0
				public bool TransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformValue.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA80 RID: 55936 RVA: 0x002EAFE4 File Offset: 0x002E91E4
				public bool TransformValue(ProgramNode node, out TransformValue value)
				{
					TransformValue? transformValue = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformValue.CreateSafe(this._builders, node);
					if (transformValue == null)
					{
						value = default(TransformValue);
						return false;
					}
					value = transformValue.Value;
					return true;
				}

				// Token: 0x0600DA81 RID: 55937 RVA: 0x002EB020 File Offset: 0x002E9220
				public bool SelectStringValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectStringValues.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA82 RID: 55938 RVA: 0x002EB044 File Offset: 0x002E9244
				public bool SelectStringValues(ProgramNode node, out SelectStringValues value)
				{
					SelectStringValues? selectStringValues = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectStringValues.CreateSafe(this._builders, node);
					if (selectStringValues == null)
					{
						value = default(SelectStringValues);
						return false;
					}
					value = selectStringValues.Value;
					return true;
				}

				// Token: 0x0600DA83 RID: 55939 RVA: 0x002EB080 File Offset: 0x002E9280
				public bool TransformLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformLet.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA84 RID: 55940 RVA: 0x002EB0A4 File Offset: 0x002E92A4
				public bool TransformLet(ProgramNode node, out TransformLet value)
				{
					TransformLet? transformLet = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformLet.CreateSafe(this._builders, node);
					if (transformLet == null)
					{
						value = default(TransformLet);
						return false;
					}
					value = transformLet.Value;
					return true;
				}

				// Token: 0x0600DA85 RID: 55941 RVA: 0x002EB0E0 File Offset: 0x002E92E0
				public bool TransformString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformString.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA86 RID: 55942 RVA: 0x002EB104 File Offset: 0x002E9304
				public bool TransformString(ProgramNode node, out TransformString value)
				{
					TransformString? transformString = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformString.CreateSafe(this._builders, node);
					if (transformString == null)
					{
						value = default(TransformString);
						return false;
					}
					value = transformString.Value;
					return true;
				}

				// Token: 0x0600DA87 RID: 55943 RVA: 0x002EB140 File Offset: 0x002E9340
				public bool SelectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectArray.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA88 RID: 55944 RVA: 0x002EB164 File Offset: 0x002E9364
				public bool SelectArray(ProgramNode node, out SelectArray value)
				{
					SelectArray? selectArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectArray.CreateSafe(this._builders, node);
					if (selectArray == null)
					{
						value = default(SelectArray);
						return false;
					}
					value = selectArray.Value;
					return true;
				}

				// Token: 0x0600DA89 RID: 55945 RVA: 0x002EB1A0 File Offset: 0x002E93A0
				public bool ToArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ToArray.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0600DA8A RID: 55946 RVA: 0x002EB1C4 File Offset: 0x002E93C4
				public bool ToArray(ProgramNode node, out ToArray value)
				{
					ToArray? toArray = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ToArray.CreateSafe(this._builders, node);
					if (toArray == null)
					{
						value = default(ToArray);
						return false;
					}
					value = toArray.Value;
					return true;
				}

				// Token: 0x040053CB RID: 21451
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A0D RID: 6669
			public class NodeAs
			{
				// Token: 0x0600DA8B RID: 55947 RVA: 0x002EB1FE File Offset: 0x002E93FE
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DA8C RID: 55948 RVA: 0x002EB20D File Offset: 0x002E940D
				public path? path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA8D RID: 55949 RVA: 0x002EB21B File Offset: 0x002E941B
				public str? str(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA8E RID: 55950 RVA: 0x002EB229 File Offset: 0x002E9429
				public t? t(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA8F RID: 55951 RVA: 0x002EB237 File Offset: 0x002E9437
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA90 RID: 55952 RVA: 0x002EB245 File Offset: 0x002E9445
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x? x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA91 RID: 55953 RVA: 0x002EB253 File Offset: 0x002E9453
				public value? value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA92 RID: 55954 RVA: 0x002EB261 File Offset: 0x002E9461
				public @object? @object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA93 RID: 55955 RVA: 0x002EB26F File Offset: 0x002E946F
				public array? array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA94 RID: 55956 RVA: 0x002EB27D File Offset: 0x002E947D
				public property? property(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA95 RID: 55957 RVA: 0x002EB28B File Offset: 0x002E948B
				public elements? elements(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA96 RID: 55958 RVA: 0x002EB299 File Offset: 0x002E9499
				public key? key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA97 RID: 55959 RVA: 0x002EB2A7 File Offset: 0x002E94A7
				public selectKey? selectKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA98 RID: 55960 RVA: 0x002EB2B5 File Offset: 0x002E94B5
				public selectOrTransformValue? selectOrTransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA99 RID: 55961 RVA: 0x002EB2C3 File Offset: 0x002E94C3
				public selectValue? selectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9A RID: 55962 RVA: 0x002EB2D1 File Offset: 0x002E94D1
				public transformValue? transformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9B RID: 55963 RVA: 0x002EB2DF File Offset: 0x002E94DF
				public transformLet? transformLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9C RID: 55964 RVA: 0x002EB2ED File Offset: 0x002E94ED
				public row? row(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9D RID: 55965 RVA: 0x002EB2FB File Offset: 0x002E94FB
				public transformString? transformString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9E RID: 55966 RVA: 0x002EB309 File Offset: 0x002E9509
				public selectArray? selectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DA9F RID: 55967 RVA: 0x002EB317 File Offset: 0x002E9517
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0? _LetB0(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0.CreateSafe(this._builders, node);
				}

				// Token: 0x040053CC RID: 21452
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A0E RID: 6670
			public class RuleAs
			{
				// Token: 0x0600DAA0 RID: 55968 RVA: 0x002EB325 File Offset: 0x002E9525
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DAA1 RID: 55969 RVA: 0x002EB334 File Offset: 0x002E9534
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output? output(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.output.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA2 RID: 55970 RVA: 0x002EB342 File Offset: 0x002E9542
				public output_v? output_v(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.output_v.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA3 RID: 55971 RVA: 0x002EB350 File Offset: 0x002E9550
				public value_object? value_object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_object.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA4 RID: 55972 RVA: 0x002EB35E File Offset: 0x002E955E
				public value_array? value_array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.value_array.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA5 RID: 55973 RVA: 0x002EB36C File Offset: 0x002E956C
				public SelectOrTransformValue? SelectOrTransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectOrTransformValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA6 RID: 55974 RVA: 0x002EB37A File Offset: 0x002E957A
				public _Value? _Value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes._Value.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA7 RID: 55975 RVA: 0x002EB388 File Offset: 0x002E9588
				public ConstValue? ConstValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA8 RID: 55976 RVA: 0x002EB396 File Offset: 0x002E9596
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object? Object(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAA9 RID: 55977 RVA: 0x002EB3A4 File Offset: 0x002E95A4
				public Append? Append(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Append.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAA RID: 55978 RVA: 0x002EB3B2 File Offset: 0x002E95B2
				public SelectObject? SelectObject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectObject.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAB RID: 55979 RVA: 0x002EB3C0 File Offset: 0x002E95C0
				public FlattenObject? FlattenObject(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.FlattenObject.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAC RID: 55980 RVA: 0x002EB3CE File Offset: 0x002E95CE
				public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array? Array(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAD RID: 55981 RVA: 0x002EB3DC File Offset: 0x002E95DC
				public array_selectArray? array_selectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.array_selectArray.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAE RID: 55982 RVA: 0x002EB3EA File Offset: 0x002E95EA
				public Property? Property(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Property.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAAF RID: 55983 RVA: 0x002EB3F8 File Offset: 0x002E95F8
				public SelectProperty? SelectProperty(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectProperty.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB0 RID: 55984 RVA: 0x002EB406 File Offset: 0x002E9606
				public Transform? Transform(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Transform.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB1 RID: 55985 RVA: 0x002EB414 File Offset: 0x002E9614
				public TransformFlatten? TransformFlatten(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformFlatten.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB2 RID: 55986 RVA: 0x002EB422 File Offset: 0x002E9622
				public Key? Key(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Key.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB3 RID: 55987 RVA: 0x002EB430 File Offset: 0x002E9630
				public ConstKey? ConstKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConstKey.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB4 RID: 55988 RVA: 0x002EB43E File Offset: 0x002E963E
				public SelectKey? SelectKey(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectKey.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB5 RID: 55989 RVA: 0x002EB44C File Offset: 0x002E964C
				public selectOrTransformValue_selectValue? selectOrTransformValue_selectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_selectValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB6 RID: 55990 RVA: 0x002EB45A File Offset: 0x002E965A
				public selectOrTransformValue_transformValue? selectOrTransformValue_transformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes.selectOrTransformValue_transformValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB7 RID: 55991 RVA: 0x002EB468 File Offset: 0x002E9668
				public SelectValue? SelectValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB8 RID: 55992 RVA: 0x002EB476 File Offset: 0x002E9676
				public ValueToString? ValueToString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ValueToString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DAB9 RID: 55993 RVA: 0x002EB484 File Offset: 0x002E9684
				public ConvertValueTo? ConvertValueTo(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ConvertValueTo.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABA RID: 55994 RVA: 0x002EB492 File Offset: 0x002E9692
				public TransformValue? TransformValue(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformValue.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABB RID: 55995 RVA: 0x002EB4A0 File Offset: 0x002E96A0
				public SelectStringValues? SelectStringValues(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectStringValues.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABC RID: 55996 RVA: 0x002EB4AE File Offset: 0x002E96AE
				public TransformLet? TransformLet(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformLet.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABD RID: 55997 RVA: 0x002EB4BC File Offset: 0x002E96BC
				public TransformString? TransformString(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.TransformString.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABE RID: 55998 RVA: 0x002EB4CA File Offset: 0x002E96CA
				public SelectArray? SelectArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.SelectArray.CreateSafe(this._builders, node);
				}

				// Token: 0x0600DABF RID: 55999 RVA: 0x002EB4D8 File Offset: 0x002E96D8
				public ToArray? ToArray(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.ToArray.CreateSafe(this._builders, node);
				}

				// Token: 0x040053CD RID: 21453
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02001A10 RID: 6672
		public class Sets
		{
			// Token: 0x0600DAC3 RID: 56003 RVA: 0x002EB500 File Offset: 0x002E9700
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x170024B6 RID: 9398
			// (get) Token: 0x0600DAC4 RID: 56004 RVA: 0x002EB54F File Offset: 0x002E974F
			// (set) Token: 0x0600DAC5 RID: 56005 RVA: 0x002EB557 File Offset: 0x002E9757
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x170024B7 RID: 9399
			// (get) Token: 0x0600DAC6 RID: 56006 RVA: 0x002EB560 File Offset: 0x002E9760
			// (set) Token: 0x0600DAC7 RID: 56007 RVA: 0x002EB568 File Offset: 0x002E9768
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x170024B8 RID: 9400
			// (get) Token: 0x0600DAC8 RID: 56008 RVA: 0x002EB571 File Offset: 0x002E9771
			// (set) Token: 0x0600DAC9 RID: 56009 RVA: 0x002EB579 File Offset: 0x002E9779
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x170024B9 RID: 9401
			// (get) Token: 0x0600DACA RID: 56010 RVA: 0x002EB582 File Offset: 0x002E9782
			// (set) Token: 0x0600DACB RID: 56011 RVA: 0x002EB58A File Offset: 0x002E978A
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x170024BA RID: 9402
			// (get) Token: 0x0600DACC RID: 56012 RVA: 0x002EB593 File Offset: 0x002E9793
			// (set) Token: 0x0600DACD RID: 56013 RVA: 0x002EB59B File Offset: 0x002E979B
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02001A11 RID: 6673
			public class Joins
			{
				// Token: 0x0600DACE RID: 56014 RVA: 0x002EB5A4 File Offset: 0x002E97A4
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DACF RID: 56015 RVA: 0x002EB5B3 File Offset: 0x002E97B3
				public ProgramSetBuilder<value> _Value(ProgramSetBuilder<selectKey> value0)
				{
					return ProgramSetBuilder<value>.CreateUnsafe(ProgramSet.Join(this._builders.Rule._Value, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAD0 RID: 56016 RVA: 0x002EB5E4 File Offset: 0x002E97E4
				public ProgramSetBuilder<value> ConstValue(ProgramSetBuilder<str> value0)
				{
					return ProgramSetBuilder<value>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAD1 RID: 56017 RVA: 0x002EB615 File Offset: 0x002E9815
				public ProgramSetBuilder<@object> Object(ProgramSetBuilder<property> value0)
				{
					return ProgramSetBuilder<@object>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Object, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAD2 RID: 56018 RVA: 0x002EB646 File Offset: 0x002E9846
				public ProgramSetBuilder<@object> Append(ProgramSetBuilder<property> value0, ProgramSetBuilder<@object> value1)
				{
					return ProgramSetBuilder<@object>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Append, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAD3 RID: 56019 RVA: 0x002EB686 File Offset: 0x002E9886
				public ProgramSetBuilder<@object> SelectObject(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<@object>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectObject, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAD4 RID: 56020 RVA: 0x002EB6C6 File Offset: 0x002E98C6
				public ProgramSetBuilder<@object> FlattenObject(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<@object>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.FlattenObject, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAD5 RID: 56021 RVA: 0x002EB706 File Offset: 0x002E9906
				public ProgramSetBuilder<array> Array(ProgramSetBuilder<elements> value0)
				{
					return ProgramSetBuilder<array>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Array, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAD6 RID: 56022 RVA: 0x002EB737 File Offset: 0x002E9937
				public ProgramSetBuilder<property> Property(ProgramSetBuilder<key> value0, ProgramSetBuilder<value> value1)
				{
					return ProgramSetBuilder<property>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Property, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAD7 RID: 56023 RVA: 0x002EB777 File Offset: 0x002E9977
				public ProgramSetBuilder<property> SelectProperty(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<property>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectProperty, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAD8 RID: 56024 RVA: 0x002EB7B7 File Offset: 0x002E99B7
				public ProgramSetBuilder<key> Key(ProgramSetBuilder<selectValue> value0)
				{
					return ProgramSetBuilder<key>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Key, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAD9 RID: 56025 RVA: 0x002EB7E8 File Offset: 0x002E99E8
				public ProgramSetBuilder<key> ConstKey(ProgramSetBuilder<str> value0)
				{
					return ProgramSetBuilder<key>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstKey, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DADA RID: 56026 RVA: 0x002EB819 File Offset: 0x002E9A19
				public ProgramSetBuilder<selectKey> SelectKey(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectKey>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectKey, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DADB RID: 56027 RVA: 0x002EB859 File Offset: 0x002E9A59
				public ProgramSetBuilder<selectValue> SelectValue(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectValue>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DADC RID: 56028 RVA: 0x002EB899 File Offset: 0x002E9A99
				public ProgramSetBuilder<selectValue> ValueToString(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectValue>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ValueToString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DADD RID: 56029 RVA: 0x002EB8DC File Offset: 0x002E9ADC
				public ProgramSetBuilder<selectValue> ConvertValueTo(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<t> value1, ProgramSetBuilder<path> value2)
				{
					return ProgramSetBuilder<selectValue>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConvertValueTo, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600DADE RID: 56030 RVA: 0x002EB936 File Offset: 0x002E9B36
				public ProgramSetBuilder<transformValue> TransformValue(ProgramSetBuilder<transformLet> value0)
				{
					return ProgramSetBuilder<transformValue>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TransformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DADF RID: 56031 RVA: 0x002EB967 File Offset: 0x002E9B67
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0> SelectStringValues(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectStringValues, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAE0 RID: 56032 RVA: 0x002EB998 File Offset: 0x002E9B98
				public ProgramSetBuilder<selectArray> SelectArray(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectArray>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectArray, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAE1 RID: 56033 RVA: 0x002EB9D8 File Offset: 0x002E9BD8
				public ProgramSetBuilder<selectArray> ToArray(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return ProgramSetBuilder<selectArray>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ToArray, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAE2 RID: 56034 RVA: 0x002EBA18 File Offset: 0x002E9C18
				public ProgramSetBuilder<elements> Transform(ProgramSetBuilder<value> value0, ProgramSetBuilder<selectArray> value1)
				{
					return ProgramSetBuilder<elements>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Transform, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAE3 RID: 56035 RVA: 0x002EBA58 File Offset: 0x002E9C58
				public ProgramSetBuilder<elements> TransformFlatten(ProgramSetBuilder<array> value0, ProgramSetBuilder<selectArray> value1)
				{
					return ProgramSetBuilder<elements>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TransformFlatten, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAE4 RID: 56036 RVA: 0x002EBA98 File Offset: 0x002E9C98
				public ProgramSetBuilder<value> SelectOrTransformValue(ProgramSetBuilder<selectOrTransformValue> value0)
				{
					return ProgramSetBuilder<value>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SelectOrTransformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAE5 RID: 56037 RVA: 0x002EBAC9 File Offset: 0x002E9CC9
				public ProgramSetBuilder<transformString> TransformString(ProgramSetBuilder<@switch> value0)
				{
					return ProgramSetBuilder<transformString>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TransformString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAE6 RID: 56038 RVA: 0x002EBAFA File Offset: 0x002E9CFA
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output> output(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v> value0, ProgramSetBuilder<value> value1)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.output, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAE7 RID: 56039 RVA: 0x002EBB3A File Offset: 0x002E9D3A
				public ProgramSetBuilder<transformLet> TransformLet(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<transformString> value1)
				{
					return ProgramSetBuilder<transformLet>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TransformLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040053D4 RID: 21460
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A12 RID: 6674
			public class ExplicitJoins
			{
				// Token: 0x0600DAE8 RID: 56040 RVA: 0x002EBB7A File Offset: 0x002E9D7A
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DAE9 RID: 56041 RVA: 0x002EBB89 File Offset: 0x002E9D89
				public JoinProgramSetBuilder<value> _Value(ProgramSetBuilder<selectKey> value0)
				{
					return JoinProgramSetBuilder<value>.CreateUnsafe(new JoinProgramSet(this._builders.Rule._Value, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAEA RID: 56042 RVA: 0x002EBBBA File Offset: 0x002E9DBA
				public JoinProgramSetBuilder<value> ConstValue(ProgramSetBuilder<str> value0)
				{
					return JoinProgramSetBuilder<value>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAEB RID: 56043 RVA: 0x002EBBEB File Offset: 0x002E9DEB
				public JoinProgramSetBuilder<@object> Object(ProgramSetBuilder<property> value0)
				{
					return JoinProgramSetBuilder<@object>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Object, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAEC RID: 56044 RVA: 0x002EBC1C File Offset: 0x002E9E1C
				public JoinProgramSetBuilder<@object> Append(ProgramSetBuilder<property> value0, ProgramSetBuilder<@object> value1)
				{
					return JoinProgramSetBuilder<@object>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Append, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAED RID: 56045 RVA: 0x002EBC5C File Offset: 0x002E9E5C
				public JoinProgramSetBuilder<@object> SelectObject(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<@object>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectObject, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAEE RID: 56046 RVA: 0x002EBC9C File Offset: 0x002E9E9C
				public JoinProgramSetBuilder<@object> FlattenObject(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<@object>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.FlattenObject, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAEF RID: 56047 RVA: 0x002EBCDC File Offset: 0x002E9EDC
				public JoinProgramSetBuilder<array> Array(ProgramSetBuilder<elements> value0)
				{
					return JoinProgramSetBuilder<array>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Array, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAF0 RID: 56048 RVA: 0x002EBD0D File Offset: 0x002E9F0D
				public JoinProgramSetBuilder<property> Property(ProgramSetBuilder<key> value0, ProgramSetBuilder<value> value1)
				{
					return JoinProgramSetBuilder<property>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Property, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAF1 RID: 56049 RVA: 0x002EBD4D File Offset: 0x002E9F4D
				public JoinProgramSetBuilder<property> SelectProperty(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<property>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectProperty, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAF2 RID: 56050 RVA: 0x002EBD8D File Offset: 0x002E9F8D
				public JoinProgramSetBuilder<key> Key(ProgramSetBuilder<selectValue> value0)
				{
					return JoinProgramSetBuilder<key>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Key, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAF3 RID: 56051 RVA: 0x002EBDBE File Offset: 0x002E9FBE
				public JoinProgramSetBuilder<key> ConstKey(ProgramSetBuilder<str> value0)
				{
					return JoinProgramSetBuilder<key>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstKey, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAF4 RID: 56052 RVA: 0x002EBDEF File Offset: 0x002E9FEF
				public JoinProgramSetBuilder<selectKey> SelectKey(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectKey>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectKey, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAF5 RID: 56053 RVA: 0x002EBE2F File Offset: 0x002EA02F
				public JoinProgramSetBuilder<selectValue> SelectValue(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectValue>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectValue, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAF6 RID: 56054 RVA: 0x002EBE6F File Offset: 0x002EA06F
				public JoinProgramSetBuilder<selectValue> ValueToString(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectValue>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ValueToString, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAF7 RID: 56055 RVA: 0x002EBEB0 File Offset: 0x002EA0B0
				public JoinProgramSetBuilder<selectValue> ConvertValueTo(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<t> value1, ProgramSetBuilder<path> value2)
				{
					return JoinProgramSetBuilder<selectValue>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConvertValueTo, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0600DAF8 RID: 56056 RVA: 0x002EBF0A File Offset: 0x002EA10A
				public JoinProgramSetBuilder<transformValue> TransformValue(ProgramSetBuilder<transformLet> value0)
				{
					return JoinProgramSetBuilder<transformValue>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TransformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAF9 RID: 56057 RVA: 0x002EBF3B File Offset: 0x002EA13B
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0> SelectStringValues(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectStringValues, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAFA RID: 56058 RVA: 0x002EBF6C File Offset: 0x002EA16C
				public JoinProgramSetBuilder<selectArray> SelectArray(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectArray>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectArray, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAFB RID: 56059 RVA: 0x002EBFAC File Offset: 0x002EA1AC
				public JoinProgramSetBuilder<selectArray> ToArray(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> value0, ProgramSetBuilder<path> value1)
				{
					return JoinProgramSetBuilder<selectArray>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ToArray, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAFC RID: 56060 RVA: 0x002EBFEC File Offset: 0x002EA1EC
				public JoinProgramSetBuilder<elements> Transform(ProgramSetBuilder<value> value0, ProgramSetBuilder<selectArray> value1)
				{
					return JoinProgramSetBuilder<elements>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Transform, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAFD RID: 56061 RVA: 0x002EC02C File Offset: 0x002EA22C
				public JoinProgramSetBuilder<elements> TransformFlatten(ProgramSetBuilder<array> value0, ProgramSetBuilder<selectArray> value1)
				{
					return JoinProgramSetBuilder<elements>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TransformFlatten, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DAFE RID: 56062 RVA: 0x002EC06C File Offset: 0x002EA26C
				public JoinProgramSetBuilder<value> SelectOrTransformValue(ProgramSetBuilder<selectOrTransformValue> value0)
				{
					return JoinProgramSetBuilder<value>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SelectOrTransformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DAFF RID: 56063 RVA: 0x002EC09D File Offset: 0x002EA29D
				public JoinProgramSetBuilder<transformString> TransformString(ProgramSetBuilder<@switch> value0)
				{
					return JoinProgramSetBuilder<transformString>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TransformString, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB00 RID: 56064 RVA: 0x002EC0CE File Offset: 0x002EA2CE
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output> output(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v> value0, ProgramSetBuilder<value> value1)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.output, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0600DB01 RID: 56065 RVA: 0x002EC10E File Offset: 0x002EA30E
				public JoinProgramSetBuilder<transformLet> TransformLet(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0> value0, ProgramSetBuilder<transformString> value1)
				{
					return JoinProgramSetBuilder<transformLet>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TransformLet, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x040053D5 RID: 21461
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A13 RID: 6675
			public class JoinUnnamedConversions
			{
				// Token: 0x0600DB02 RID: 56066 RVA: 0x002EC14E File Offset: 0x002EA34E
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DB03 RID: 56067 RVA: 0x002EC15D File Offset: 0x002EA35D
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output> output_v(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v> value0)
				{
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.output_v, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB04 RID: 56068 RVA: 0x002EC18E File Offset: 0x002EA38E
				public ProgramSetBuilder<value> value_object(ProgramSetBuilder<@object> value0)
				{
					return ProgramSetBuilder<value>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.value_object, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB05 RID: 56069 RVA: 0x002EC1BF File Offset: 0x002EA3BF
				public ProgramSetBuilder<value> value_array(ProgramSetBuilder<array> value0)
				{
					return ProgramSetBuilder<value>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.value_array, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB06 RID: 56070 RVA: 0x002EC1F0 File Offset: 0x002EA3F0
				public ProgramSetBuilder<array> array_selectArray(ProgramSetBuilder<selectArray> value0)
				{
					return ProgramSetBuilder<array>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.array_selectArray, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB07 RID: 56071 RVA: 0x002EC221 File Offset: 0x002EA421
				public ProgramSetBuilder<selectOrTransformValue> selectOrTransformValue_selectValue(ProgramSetBuilder<selectValue> value0)
				{
					return ProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selectOrTransformValue_selectValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB08 RID: 56072 RVA: 0x002EC252 File Offset: 0x002EA452
				public ProgramSetBuilder<selectOrTransformValue> selectOrTransformValue_transformValue(ProgramSetBuilder<transformValue> value0)
				{
					return ProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.selectOrTransformValue_transformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040053D6 RID: 21462
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A14 RID: 6676
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x0600DB09 RID: 56073 RVA: 0x002EC283 File Offset: 0x002EA483
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DB0A RID: 56074 RVA: 0x002EC292 File Offset: 0x002EA492
				public JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output> output_v(ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.v> value0)
				{
					return JoinProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.output_v, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB0B RID: 56075 RVA: 0x002EC2C3 File Offset: 0x002EA4C3
				public JoinProgramSetBuilder<value> value_object(ProgramSetBuilder<@object> value0)
				{
					return JoinProgramSetBuilder<value>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.value_object, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB0C RID: 56076 RVA: 0x002EC2F4 File Offset: 0x002EA4F4
				public JoinProgramSetBuilder<value> value_array(ProgramSetBuilder<array> value0)
				{
					return JoinProgramSetBuilder<value>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.value_array, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB0D RID: 56077 RVA: 0x002EC325 File Offset: 0x002EA525
				public JoinProgramSetBuilder<array> array_selectArray(ProgramSetBuilder<selectArray> value0)
				{
					return JoinProgramSetBuilder<array>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.array_selectArray, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB0E RID: 56078 RVA: 0x002EC356 File Offset: 0x002EA556
				public JoinProgramSetBuilder<selectOrTransformValue> selectOrTransformValue_selectValue(ProgramSetBuilder<selectValue> value0)
				{
					return JoinProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selectOrTransformValue_selectValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0600DB0F RID: 56079 RVA: 0x002EC387 File Offset: 0x002EA587
				public JoinProgramSetBuilder<selectOrTransformValue> selectOrTransformValue_transformValue(ProgramSetBuilder<transformValue> value0)
				{
					return JoinProgramSetBuilder<selectOrTransformValue>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.selectOrTransformValue_transformValue, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x040053D7 RID: 21463
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001A15 RID: 6677
			public class Casts
			{
				// Token: 0x0600DB10 RID: 56080 RVA: 0x002EC3B8 File Offset: 0x002EA5B8
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0600DB11 RID: 56081 RVA: 0x002EC3C8 File Offset: 0x002EA5C8
				public ProgramSetBuilder<path> path(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.path)
					{
						string text = "set";
						string text2 = "expected program set for symbol path but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.path>.CreateUnsafe(set);
				}

				// Token: 0x0600DB12 RID: 56082 RVA: 0x002EC420 File Offset: 0x002EA620
				public ProgramSetBuilder<str> str(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.str)
					{
						string text = "set";
						string text2 = "expected program set for symbol str but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.str>.CreateUnsafe(set);
				}

				// Token: 0x0600DB13 RID: 56083 RVA: 0x002EC478 File Offset: 0x002EA678
				public ProgramSetBuilder<t> t(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.t)
					{
						string text = "set";
						string text2 = "expected program set for symbol t but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.t>.CreateUnsafe(set);
				}

				// Token: 0x0600DB14 RID: 56084 RVA: 0x002EC4D0 File Offset: 0x002EA6D0
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output> output(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.output)
					{
						string text = "set";
						string text2 = "expected program set for symbol output but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.output>.CreateUnsafe(set);
				}

				// Token: 0x0600DB15 RID: 56085 RVA: 0x002EC528 File Offset: 0x002EA728
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x> x(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.x)
					{
						string text = "set";
						string text2 = "expected program set for symbol x but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.x>.CreateUnsafe(set);
				}

				// Token: 0x0600DB16 RID: 56086 RVA: 0x002EC580 File Offset: 0x002EA780
				public ProgramSetBuilder<value> value(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.value)
					{
						string text = "set";
						string text2 = "expected program set for symbol @value but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.value>.CreateUnsafe(set);
				}

				// Token: 0x0600DB17 RID: 56087 RVA: 0x002EC5D8 File Offset: 0x002EA7D8
				public ProgramSetBuilder<@object> @object(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.@object)
					{
						string text = "set";
						string text2 = "expected program set for symbol @object but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.@object>.CreateUnsafe(set);
				}

				// Token: 0x0600DB18 RID: 56088 RVA: 0x002EC630 File Offset: 0x002EA830
				public ProgramSetBuilder<array> array(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.array)
					{
						string text = "set";
						string text2 = "expected program set for symbol array but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.array>.CreateUnsafe(set);
				}

				// Token: 0x0600DB19 RID: 56089 RVA: 0x002EC688 File Offset: 0x002EA888
				public ProgramSetBuilder<property> property(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.property)
					{
						string text = "set";
						string text2 = "expected program set for symbol property but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.property>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1A RID: 56090 RVA: 0x002EC6E0 File Offset: 0x002EA8E0
				public ProgramSetBuilder<elements> elements(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.elements)
					{
						string text = "set";
						string text2 = "expected program set for symbol elements but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.elements>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1B RID: 56091 RVA: 0x002EC738 File Offset: 0x002EA938
				public ProgramSetBuilder<key> key(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.key)
					{
						string text = "set";
						string text2 = "expected program set for symbol key but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.key>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1C RID: 56092 RVA: 0x002EC790 File Offset: 0x002EA990
				public ProgramSetBuilder<selectKey> selectKey(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectKey)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectKey but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectKey>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1D RID: 56093 RVA: 0x002EC7E8 File Offset: 0x002EA9E8
				public ProgramSetBuilder<selectOrTransformValue> selectOrTransformValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectOrTransformValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectOrTransformValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectOrTransformValue>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1E RID: 56094 RVA: 0x002EC840 File Offset: 0x002EAA40
				public ProgramSetBuilder<selectValue> selectValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectValue>.CreateUnsafe(set);
				}

				// Token: 0x0600DB1F RID: 56095 RVA: 0x002EC898 File Offset: 0x002EAA98
				public ProgramSetBuilder<transformValue> transformValue(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.transformValue)
					{
						string text = "set";
						string text2 = "expected program set for symbol transformValue but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformValue>.CreateUnsafe(set);
				}

				// Token: 0x0600DB20 RID: 56096 RVA: 0x002EC8F0 File Offset: 0x002EAAF0
				public ProgramSetBuilder<transformLet> transformLet(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.transformLet)
					{
						string text = "set";
						string text2 = "expected program set for symbol transformLet but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformLet>.CreateUnsafe(set);
				}

				// Token: 0x0600DB21 RID: 56097 RVA: 0x002EC948 File Offset: 0x002EAB48
				public ProgramSetBuilder<row> row(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.row)
					{
						string text = "set";
						string text2 = "expected program set for symbol row but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.row>.CreateUnsafe(set);
				}

				// Token: 0x0600DB22 RID: 56098 RVA: 0x002EC9A0 File Offset: 0x002EABA0
				public ProgramSetBuilder<transformString> transformString(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.transformString)
					{
						string text = "set";
						string text2 = "expected program set for symbol transformString but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.transformString>.CreateUnsafe(set);
				}

				// Token: 0x0600DB23 RID: 56099 RVA: 0x002EC9F8 File Offset: 0x002EABF8
				public ProgramSetBuilder<selectArray> selectArray(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectArray)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectArray but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes.selectArray>.CreateUnsafe(set);
				}

				// Token: 0x0600DB24 RID: 56100 RVA: 0x002ECA50 File Offset: 0x002EAC50
				public ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0> _LetB0(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol._LetB0)
					{
						string text = "set";
						string text2 = "expected program set for symbol _LetB0 but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes._LetB0>.CreateUnsafe(set);
				}

				// Token: 0x040053D8 RID: 21464
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
