using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build
{
	// Token: 0x02001E3B RID: 7739
	public class GrammarBuilders
	{
		// Token: 0x0601025E RID: 66142 RVA: 0x003810B3 File Offset: 0x0037F2B3
		public static GrammarBuilders Instance(Grammar grammar)
		{
			return GrammarBuilders._builderCache.GetOrAdd(grammar, (Grammar key) => new GrammarBuilders(key));
		}

		// Token: 0x17002AD7 RID: 10967
		// (get) Token: 0x0601025F RID: 66143 RVA: 0x003810DF File Offset: 0x0037F2DF
		public GrammarBuilders.GrammarSymbols Symbol
		{
			get
			{
				return this._symbol.Value;
			}
		}

		// Token: 0x17002AD8 RID: 10968
		// (get) Token: 0x06010260 RID: 66144 RVA: 0x003810EC File Offset: 0x0037F2EC
		public GrammarBuilders.GrammarRules Rule
		{
			get
			{
				return this._rule.Value;
			}
		}

		// Token: 0x17002AD9 RID: 10969
		// (get) Token: 0x06010261 RID: 66145 RVA: 0x003810F9 File Offset: 0x0037F2F9
		public GrammarBuilders.GrammarUnnamedConversions UnnamedConversion
		{
			get
			{
				return this._unnamedConversion.Value;
			}
		}

		// Token: 0x17002ADA RID: 10970
		// (get) Token: 0x06010262 RID: 66146 RVA: 0x00381106 File Offset: 0x0037F306
		public GrammarBuilders.GrammarHoles Hole
		{
			get
			{
				return this._hole.Value;
			}
		}

		// Token: 0x17002ADB RID: 10971
		// (get) Token: 0x06010263 RID: 66147 RVA: 0x00381113 File Offset: 0x0037F313
		// (set) Token: 0x06010264 RID: 66148 RVA: 0x0038111B File Offset: 0x0037F31B
		public GrammarBuilders.Nodes Node { get; private set; }

		// Token: 0x17002ADC RID: 10972
		// (get) Token: 0x06010265 RID: 66149 RVA: 0x00381124 File Offset: 0x0037F324
		// (set) Token: 0x06010266 RID: 66150 RVA: 0x0038112C File Offset: 0x0037F32C
		public GrammarBuilders.Sets Set { get; private set; }

		// Token: 0x06010267 RID: 66151 RVA: 0x00381138 File Offset: 0x0037F338
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

		// Token: 0x040061AE RID: 25006
		private static readonly ConcurrentDictionary<Grammar, GrammarBuilders> _builderCache = new ConcurrentDictionary<Grammar, GrammarBuilders>();

		// Token: 0x040061AF RID: 25007
		private readonly Lazy<GrammarBuilders.GrammarSymbols> _symbol;

		// Token: 0x040061B0 RID: 25008
		private readonly Lazy<GrammarBuilders.GrammarRules> _rule;

		// Token: 0x040061B1 RID: 25009
		private readonly Lazy<GrammarBuilders.GrammarUnnamedConversions> _unnamedConversion;

		// Token: 0x040061B2 RID: 25010
		private readonly Lazy<GrammarBuilders.GrammarHoles> _hole;

		// Token: 0x02001E3C RID: 7740
		public class GrammarSymbols
		{
			// Token: 0x17002ADD RID: 10973
			// (get) Token: 0x06010269 RID: 66153 RVA: 0x003811E3 File Offset: 0x0037F3E3
			// (set) Token: 0x0601026A RID: 66154 RVA: 0x003811EB File Offset: 0x0037F3EB
			public Symbol v { get; private set; }

			// Token: 0x17002ADE RID: 10974
			// (get) Token: 0x0601026B RID: 66155 RVA: 0x003811F4 File Offset: 0x0037F3F4
			// (set) Token: 0x0601026C RID: 66156 RVA: 0x003811FC File Offset: 0x0037F3FC
			public Symbol guardedRule { get; private set; }

			// Token: 0x17002ADF RID: 10975
			// (get) Token: 0x0601026D RID: 66157 RVA: 0x00381205 File Offset: 0x0037F405
			// (set) Token: 0x0601026E RID: 66158 RVA: 0x0038120D File Offset: 0x0037F40D
			public Symbol match { get; private set; }

			// Token: 0x17002AE0 RID: 10976
			// (get) Token: 0x0601026F RID: 66159 RVA: 0x00381216 File Offset: 0x0037F416
			// (set) Token: 0x06010270 RID: 66160 RVA: 0x0038121E File Offset: 0x0037F41E
			public Symbol pred { get; private set; }

			// Token: 0x17002AE1 RID: 10977
			// (get) Token: 0x06010271 RID: 66161 RVA: 0x00381227 File Offset: 0x0037F427
			// (set) Token: 0x06010272 RID: 66162 RVA: 0x0038122F File Offset: 0x0037F42F
			public Symbol newDsl { get; private set; }

			// Token: 0x17002AE2 RID: 10978
			// (get) Token: 0x06010273 RID: 66163 RVA: 0x00381238 File Offset: 0x0037F438
			// (set) Token: 0x06010274 RID: 66164 RVA: 0x00381240 File Offset: 0x0037F440
			public Symbol construction { get; private set; }

			// Token: 0x17002AE3 RID: 10979
			// (get) Token: 0x06010275 RID: 66165 RVA: 0x00381249 File Offset: 0x0037F449
			// (set) Token: 0x06010276 RID: 66166 RVA: 0x00381251 File Offset: 0x0037F451
			public Symbol select { get; private set; }

			// Token: 0x17002AE4 RID: 10980
			// (get) Token: 0x06010277 RID: 66167 RVA: 0x0038125A File Offset: 0x0037F45A
			// (set) Token: 0x06010278 RID: 66168 RVA: 0x00381262 File Offset: 0x0037F462
			public Symbol tmpFilter { get; private set; }

			// Token: 0x17002AE5 RID: 10981
			// (get) Token: 0x06010279 RID: 66169 RVA: 0x0038126B File Offset: 0x0037F46B
			// (set) Token: 0x0601027A RID: 66170 RVA: 0x00381273 File Offset: 0x0037F473
			public Symbol x { get; private set; }

			// Token: 0x17002AE6 RID: 10982
			// (get) Token: 0x0601027B RID: 66171 RVA: 0x0038127C File Offset: 0x0037F47C
			// (set) Token: 0x0601027C RID: 66172 RVA: 0x00381284 File Offset: 0x0037F484
			public Symbol sequenceChildren { get; private set; }

			// Token: 0x17002AE7 RID: 10983
			// (get) Token: 0x0601027D RID: 66173 RVA: 0x0038128D File Offset: 0x0037F48D
			// (set) Token: 0x0601027E RID: 66174 RVA: 0x00381295 File Offset: 0x0037F495
			public Symbol convertSequence { get; private set; }

			// Token: 0x17002AE8 RID: 10984
			// (get) Token: 0x0601027F RID: 66175 RVA: 0x0038129E File Offset: 0x0037F49E
			// (set) Token: 0x06010280 RID: 66176 RVA: 0x003812A6 File Offset: 0x0037F4A6
			public Symbol parent { get; private set; }

			// Token: 0x17002AE9 RID: 10985
			// (get) Token: 0x06010281 RID: 66177 RVA: 0x003812AF File Offset: 0x0037F4AF
			// (set) Token: 0x06010282 RID: 66178 RVA: 0x003812B7 File Offset: 0x0037F4B7
			public Symbol sequenceMap { get; private set; }

			// Token: 0x17002AEA RID: 10986
			// (get) Token: 0x06010283 RID: 66179 RVA: 0x003812C0 File Offset: 0x0037F4C0
			// (set) Token: 0x06010284 RID: 66180 RVA: 0x003812C8 File Offset: 0x0037F4C8
			public Symbol selectedNode { get; private set; }

			// Token: 0x17002AEB RID: 10987
			// (get) Token: 0x06010285 RID: 66181 RVA: 0x003812D1 File Offset: 0x0037F4D1
			// (set) Token: 0x06010286 RID: 66182 RVA: 0x003812D9 File Offset: 0x0037F4D9
			public Symbol parentChildren { get; private set; }

			// Token: 0x17002AEC RID: 10988
			// (get) Token: 0x06010287 RID: 66183 RVA: 0x003812E2 File Offset: 0x0037F4E2
			// (set) Token: 0x06010288 RID: 66184 RVA: 0x003812EA File Offset: 0x0037F4EA
			public Symbol relChildList { get; private set; }

			// Token: 0x17002AED RID: 10989
			// (get) Token: 0x06010289 RID: 66185 RVA: 0x003812F3 File Offset: 0x0037F4F3
			// (set) Token: 0x0601028A RID: 66186 RVA: 0x003812FB File Offset: 0x0037F4FB
			public Symbol singleRelChildList { get; private set; }

			// Token: 0x17002AEE RID: 10990
			// (get) Token: 0x0601028B RID: 66187 RVA: 0x00381304 File Offset: 0x0037F504
			// (set) Token: 0x0601028C RID: 66188 RVA: 0x0038130C File Offset: 0x0037F50C
			public Symbol relChild { get; private set; }

			// Token: 0x17002AEF RID: 10991
			// (get) Token: 0x0601028D RID: 66189 RVA: 0x00381315 File Offset: 0x0037F515
			// (set) Token: 0x0601028E RID: 66190 RVA: 0x0038131D File Offset: 0x0037F51D
			public Symbol pos { get; private set; }

			// Token: 0x17002AF0 RID: 10992
			// (get) Token: 0x0601028F RID: 66191 RVA: 0x00381326 File Offset: 0x0037F526
			// (set) Token: 0x06010290 RID: 66192 RVA: 0x0038132E File Offset: 0x0037F52E
			public Symbol children { get; private set; }

			// Token: 0x17002AF1 RID: 10993
			// (get) Token: 0x06010291 RID: 66193 RVA: 0x00381337 File Offset: 0x0037F537
			// (set) Token: 0x06010292 RID: 66194 RVA: 0x0038133F File Offset: 0x0037F53F
			public Symbol interval { get; private set; }

			// Token: 0x17002AF2 RID: 10994
			// (get) Token: 0x06010293 RID: 66195 RVA: 0x00381348 File Offset: 0x0037F548
			// (set) Token: 0x06010294 RID: 66196 RVA: 0x00381350 File Offset: 0x0037F550
			public Symbol inorderAllNodes { get; private set; }

			// Token: 0x17002AF3 RID: 10995
			// (get) Token: 0x06010295 RID: 66197 RVA: 0x00381359 File Offset: 0x0037F559
			// (set) Token: 0x06010296 RID: 66198 RVA: 0x00381361 File Offset: 0x0037F561
			public Symbol label { get; private set; }

			// Token: 0x17002AF4 RID: 10996
			// (get) Token: 0x06010297 RID: 66199 RVA: 0x0038136A File Offset: 0x0037F56A
			// (set) Token: 0x06010298 RID: 66200 RVA: 0x00381372 File Offset: 0x0037F572
			public Symbol attributes { get; private set; }

			// Token: 0x17002AF5 RID: 10997
			// (get) Token: 0x06010299 RID: 66201 RVA: 0x0038137B File Offset: 0x0037F57B
			// (set) Token: 0x0601029A RID: 66202 RVA: 0x00381383 File Offset: 0x0037F583
			public Symbol kind { get; private set; }

			// Token: 0x17002AF6 RID: 10998
			// (get) Token: 0x0601029B RID: 66203 RVA: 0x0038138C File Offset: 0x0037F58C
			// (set) Token: 0x0601029C RID: 66204 RVA: 0x00381394 File Offset: 0x0037F594
			public Symbol name { get; private set; }

			// Token: 0x17002AF7 RID: 10999
			// (get) Token: 0x0601029D RID: 66205 RVA: 0x0038139D File Offset: 0x0037F59D
			// (set) Token: 0x0601029E RID: 66206 RVA: 0x003813A5 File Offset: 0x0037F5A5
			public Symbol value { get; private set; }

			// Token: 0x17002AF8 RID: 11000
			// (get) Token: 0x0601029F RID: 66207 RVA: 0x003813AE File Offset: 0x0037F5AE
			// (set) Token: 0x060102A0 RID: 66208 RVA: 0x003813B6 File Offset: 0x0037F5B6
			public Symbol k { get; private set; }

			// Token: 0x17002AF9 RID: 11001
			// (get) Token: 0x060102A1 RID: 66209 RVA: 0x003813BF File Offset: 0x0037F5BF
			// (set) Token: 0x060102A2 RID: 66210 RVA: 0x003813C7 File Offset: 0x0037F5C7
			public Symbol p { get; private set; }

			// Token: 0x17002AFA RID: 11002
			// (get) Token: 0x060102A3 RID: 66211 RVA: 0x003813D0 File Offset: 0x0037F5D0
			// (set) Token: 0x060102A4 RID: 66212 RVA: 0x003813D8 File Offset: 0x0037F5D8
			public Symbol path { get; private set; }

			// Token: 0x17002AFB RID: 11003
			// (get) Token: 0x060102A5 RID: 66213 RVA: 0x003813E1 File Offset: 0x0037F5E1
			// (set) Token: 0x060102A6 RID: 66214 RVA: 0x003813E9 File Offset: 0x0037F5E9
			public Symbol _LFun0 { get; private set; }

			// Token: 0x17002AFC RID: 11004
			// (get) Token: 0x060102A7 RID: 66215 RVA: 0x003813F2 File Offset: 0x0037F5F2
			// (set) Token: 0x060102A8 RID: 66216 RVA: 0x003813FA File Offset: 0x0037F5FA
			public Symbol _LFun1 { get; private set; }

			// Token: 0x060102A9 RID: 66217 RVA: 0x00381404 File Offset: 0x0037F604
			public GrammarSymbols(Grammar grammar)
			{
				this.v = grammar.Symbol("v");
				this.guardedRule = grammar.Symbol("guardedRule");
				this.match = grammar.Symbol("match");
				this.pred = grammar.Symbol("pred");
				this.newDsl = grammar.Symbol("newDsl");
				this.construction = grammar.Symbol("construction");
				this.select = grammar.Symbol("select");
				this.tmpFilter = grammar.Symbol("tmpFilter");
				this.x = grammar.Symbol("x");
				this.sequenceChildren = grammar.Symbol("sequenceChildren");
				this.convertSequence = grammar.Symbol("convertSequence");
				this.parent = grammar.Symbol("parent");
				this.sequenceMap = grammar.Symbol("sequenceMap");
				this.selectedNode = grammar.Symbol("selectedNode");
				this.parentChildren = grammar.Symbol("parentChildren");
				this.relChildList = grammar.Symbol("relChildList");
				this.singleRelChildList = grammar.Symbol("singleRelChildList");
				this.relChild = grammar.Symbol("relChild");
				this.pos = grammar.Symbol("pos");
				this.children = grammar.Symbol("children");
				this.interval = grammar.Symbol("interval");
				this.inorderAllNodes = grammar.Symbol("inorderAllNodes");
				this.label = grammar.Symbol("label");
				this.attributes = grammar.Symbol("attributes");
				this.kind = grammar.Symbol("kind");
				this.name = grammar.Symbol("name");
				this.value = grammar.Symbol("value");
				this.k = grammar.Symbol("k");
				this.p = grammar.Symbol("p");
				this.path = grammar.Symbol("path");
				this._LFun0 = grammar.Symbol("_LFun0");
				this._LFun1 = grammar.Symbol("_LFun1");
			}
		}

		// Token: 0x02001E3D RID: 7741
		public class GrammarRules
		{
			// Token: 0x17002AFD RID: 11005
			// (get) Token: 0x060102AA RID: 66218 RVA: 0x00381637 File Offset: 0x0037F837
			// (set) Token: 0x060102AB RID: 66219 RVA: 0x0038163F File Offset: 0x0037F83F
			public BlackBoxRule GuardedRule { get; private set; }

			// Token: 0x17002AFE RID: 11006
			// (get) Token: 0x060102AC RID: 66220 RVA: 0x00381648 File Offset: 0x0037F848
			// (set) Token: 0x060102AD RID: 66221 RVA: 0x00381650 File Offset: 0x0037F850
			public BlackBoxRule Conj { get; private set; }

			// Token: 0x17002AFF RID: 11007
			// (get) Token: 0x060102AE RID: 66222 RVA: 0x00381659 File Offset: 0x0037F859
			// (set) Token: 0x060102AF RID: 66223 RVA: 0x00381661 File Offset: 0x0037F861
			public BlackBoxRule IsKind { get; private set; }

			// Token: 0x17002B00 RID: 11008
			// (get) Token: 0x060102B0 RID: 66224 RVA: 0x0038166A File Offset: 0x0037F86A
			// (set) Token: 0x060102B1 RID: 66225 RVA: 0x00381672 File Offset: 0x0037F872
			public BlackBoxRule IsAttributePresent { get; private set; }

			// Token: 0x17002B01 RID: 11009
			// (get) Token: 0x060102B2 RID: 66226 RVA: 0x0038167B File Offset: 0x0037F87B
			// (set) Token: 0x060102B3 RID: 66227 RVA: 0x00381683 File Offset: 0x0037F883
			public BlackBoxRule IsNthChild { get; private set; }

			// Token: 0x17002B02 RID: 11010
			// (get) Token: 0x060102B4 RID: 66228 RVA: 0x0038168C File Offset: 0x0037F88C
			// (set) Token: 0x060102B5 RID: 66229 RVA: 0x00381694 File Offset: 0x0037F894
			public BlackBoxRule HasNChildren { get; private set; }

			// Token: 0x17002B03 RID: 11011
			// (get) Token: 0x060102B6 RID: 66230 RVA: 0x0038169D File Offset: 0x0037F89D
			// (set) Token: 0x060102B7 RID: 66231 RVA: 0x003816A5 File Offset: 0x0037F8A5
			public BlackBoxRule Not { get; private set; }

			// Token: 0x17002B04 RID: 11012
			// (get) Token: 0x060102B8 RID: 66232 RVA: 0x003816AE File Offset: 0x0037F8AE
			// (set) Token: 0x060102B9 RID: 66233 RVA: 0x003816B6 File Offset: 0x0037F8B6
			public BlackBoxRule LeafConstLabelNode { get; private set; }

			// Token: 0x17002B05 RID: 11013
			// (get) Token: 0x060102BA RID: 66234 RVA: 0x003816BF File Offset: 0x0037F8BF
			// (set) Token: 0x060102BB RID: 66235 RVA: 0x003816C7 File Offset: 0x0037F8C7
			public BlackBoxRule ConstLabelNode { get; private set; }

			// Token: 0x17002B06 RID: 11014
			// (get) Token: 0x060102BC RID: 66236 RVA: 0x003816D0 File Offset: 0x0037F8D0
			// (set) Token: 0x060102BD RID: 66237 RVA: 0x003816D8 File Offset: 0x0037F8D8
			public BlackBoxRule ConstSequenceLabelNode { get; private set; }

			// Token: 0x17002B07 RID: 11015
			// (get) Token: 0x060102BE RID: 66238 RVA: 0x003816E1 File Offset: 0x0037F8E1
			// (set) Token: 0x060102BF RID: 66239 RVA: 0x003816E9 File Offset: 0x0037F8E9
			public BlackBoxRule LeafConstSequenceLabelNode { get; private set; }

			// Token: 0x17002B08 RID: 11016
			// (get) Token: 0x060102C0 RID: 66240 RVA: 0x003816F2 File Offset: 0x0037F8F2
			// (set) Token: 0x060102C1 RID: 66241 RVA: 0x003816FA File Offset: 0x0037F8FA
			public BlackBoxRule InsertAtAbs { get; private set; }

			// Token: 0x17002B09 RID: 11017
			// (get) Token: 0x060102C2 RID: 66242 RVA: 0x00381703 File Offset: 0x0037F903
			// (set) Token: 0x060102C3 RID: 66243 RVA: 0x0038170B File Offset: 0x0037F90B
			public BlackBoxRule InsertAtRel { get; private set; }

			// Token: 0x17002B0A RID: 11018
			// (get) Token: 0x060102C4 RID: 66244 RVA: 0x00381714 File Offset: 0x0037F914
			// (set) Token: 0x060102C5 RID: 66245 RVA: 0x0038171C File Offset: 0x0037F91C
			public BlackBoxRule DeleteChild { get; private set; }

			// Token: 0x17002B0B RID: 11019
			// (get) Token: 0x060102C6 RID: 66246 RVA: 0x00381725 File Offset: 0x0037F925
			// (set) Token: 0x060102C7 RID: 66247 RVA: 0x0038172D File Offset: 0x0037F92D
			public BlackBoxRule ReplaceChildren { get; private set; }

			// Token: 0x17002B0C RID: 11020
			// (get) Token: 0x060102C8 RID: 66248 RVA: 0x00381736 File Offset: 0x0037F936
			// (set) Token: 0x060102C9 RID: 66249 RVA: 0x0038173E File Offset: 0x0037F93E
			public BlackBoxRule Children { get; private set; }

			// Token: 0x17002B0D RID: 11021
			// (get) Token: 0x060102CA RID: 66250 RVA: 0x00381747 File Offset: 0x0037F947
			// (set) Token: 0x060102CB RID: 66251 RVA: 0x0038174F File Offset: 0x0037F94F
			public BlackBoxRule ConcatChild { get; private set; }

			// Token: 0x17002B0E RID: 11022
			// (get) Token: 0x060102CC RID: 66252 RVA: 0x00381758 File Offset: 0x0037F958
			// (set) Token: 0x060102CD RID: 66253 RVA: 0x00381760 File Offset: 0x0037F960
			public BlackBoxRule SinglePosList { get; private set; }

			// Token: 0x17002B0F RID: 11023
			// (get) Token: 0x060102CE RID: 66254 RVA: 0x00381769 File Offset: 0x0037F969
			// (set) Token: 0x060102CF RID: 66255 RVA: 0x00381771 File Offset: 0x0037F971
			public BlackBoxRule RelChild { get; private set; }

			// Token: 0x17002B10 RID: 11024
			// (get) Token: 0x060102D0 RID: 66256 RVA: 0x0038177A File Offset: 0x0037F97A
			// (set) Token: 0x060102D1 RID: 66257 RVA: 0x00381782 File Offset: 0x0037F982
			public BlackBoxRule AbsPos { get; private set; }

			// Token: 0x17002B11 RID: 11025
			// (get) Token: 0x060102D2 RID: 66258 RVA: 0x0038178B File Offset: 0x0037F98B
			// (set) Token: 0x060102D3 RID: 66259 RVA: 0x00381793 File Offset: 0x0037F993
			public BlackBoxRule Prepend { get; private set; }

			// Token: 0x17002B12 RID: 11026
			// (get) Token: 0x060102D4 RID: 66260 RVA: 0x0038179C File Offset: 0x0037F99C
			// (set) Token: 0x060102D5 RID: 66261 RVA: 0x003817A4 File Offset: 0x0037F9A4
			public BlackBoxRule SingleList { get; private set; }

			// Token: 0x17002B13 RID: 11027
			// (get) Token: 0x060102D6 RID: 66262 RVA: 0x003817AD File Offset: 0x0037F9AD
			// (set) Token: 0x060102D7 RID: 66263 RVA: 0x003817B5 File Offset: 0x0037F9B5
			public BlackBoxRule InOrderAllNodes { get; private set; }

			// Token: 0x17002B14 RID: 11028
			// (get) Token: 0x060102D8 RID: 66264 RVA: 0x003817BE File Offset: 0x0037F9BE
			// (set) Token: 0x060102D9 RID: 66265 RVA: 0x003817C6 File Offset: 0x0037F9C6
			public ConceptRule Select { get; private set; }

			// Token: 0x17002B15 RID: 11029
			// (get) Token: 0x060102DA RID: 66266 RVA: 0x003817CF File Offset: 0x0037F9CF
			// (set) Token: 0x060102DB RID: 66267 RVA: 0x003817D7 File Offset: 0x0037F9D7
			public ConceptRule TmpFilter { get; private set; }

			// Token: 0x17002B16 RID: 11030
			// (get) Token: 0x060102DC RID: 66268 RVA: 0x003817E0 File Offset: 0x0037F9E0
			// (set) Token: 0x060102DD RID: 66269 RVA: 0x003817E8 File Offset: 0x0037F9E8
			public ConceptRule SequenceMap { get; private set; }

			// Token: 0x17002B17 RID: 11031
			// (get) Token: 0x060102DE RID: 66270 RVA: 0x003817F1 File Offset: 0x0037F9F1
			// (set) Token: 0x060102DF RID: 66271 RVA: 0x003817F9 File Offset: 0x0037F9F9
			public LetRule ConvertSequence { get; private set; }

			// Token: 0x060102E0 RID: 66272 RVA: 0x00381804 File Offset: 0x0037FA04
			public GrammarRules(Grammar grammar)
			{
				this.GuardedRule = (BlackBoxRule)grammar.Rule("GuardedRule");
				this.Conj = (BlackBoxRule)grammar.Rule("Conj");
				this.IsKind = (BlackBoxRule)grammar.Rule("IsKind");
				this.IsAttributePresent = (BlackBoxRule)grammar.Rule("IsAttributePresent");
				this.IsNthChild = (BlackBoxRule)grammar.Rule("IsNthChild");
				this.HasNChildren = (BlackBoxRule)grammar.Rule("HasNChildren");
				this.Not = (BlackBoxRule)grammar.Rule("Not");
				this.LeafConstLabelNode = (BlackBoxRule)grammar.Rule("LeafConstLabelNode");
				this.ConstLabelNode = (BlackBoxRule)grammar.Rule("ConstLabelNode");
				this.ConstSequenceLabelNode = (BlackBoxRule)grammar.Rule("ConstSequenceLabelNode");
				this.LeafConstSequenceLabelNode = (BlackBoxRule)grammar.Rule("LeafConstSequenceLabelNode");
				this.InsertAtAbs = (BlackBoxRule)grammar.Rule("InsertAtAbs");
				this.InsertAtRel = (BlackBoxRule)grammar.Rule("InsertAtRel");
				this.DeleteChild = (BlackBoxRule)grammar.Rule("DeleteChild");
				this.ReplaceChildren = (BlackBoxRule)grammar.Rule("ReplaceChildren");
				this.Children = (BlackBoxRule)grammar.Rule("Children");
				this.ConcatChild = (BlackBoxRule)grammar.Rule("ConcatChild");
				this.SinglePosList = (BlackBoxRule)grammar.Rule("SinglePosList");
				this.RelChild = (BlackBoxRule)grammar.Rule("RelChild");
				this.AbsPos = (BlackBoxRule)grammar.Rule("AbsPos");
				this.Prepend = (BlackBoxRule)grammar.Rule("Prepend");
				this.SingleList = (BlackBoxRule)grammar.Rule("SingleList");
				this.InOrderAllNodes = (BlackBoxRule)grammar.Rule("InOrderAllNodes");
				this.Select = (ConceptRule)grammar.Rule("Select");
				this.TmpFilter = (ConceptRule)grammar.Rule("TmpFilter");
				this.SequenceMap = (ConceptRule)grammar.Rule("SequenceMap");
				this.ConvertSequence = (LetRule)grammar.Rule("ConvertSequence");
			}
		}

		// Token: 0x02001E3E RID: 7742
		public class GrammarUnnamedConversions
		{
			// Token: 0x17002B18 RID: 11032
			// (get) Token: 0x060102E1 RID: 66273 RVA: 0x00381A69 File Offset: 0x0037FC69
			// (set) Token: 0x060102E2 RID: 66274 RVA: 0x00381A71 File Offset: 0x0037FC71
			public ConversionRule match_pred { get; private set; }

			// Token: 0x17002B19 RID: 11033
			// (get) Token: 0x060102E3 RID: 66275 RVA: 0x00381A7A File Offset: 0x0037FC7A
			// (set) Token: 0x060102E4 RID: 66276 RVA: 0x00381A82 File Offset: 0x0037FC82
			public ConversionRule newDsl_select { get; private set; }

			// Token: 0x17002B1A RID: 11034
			// (get) Token: 0x060102E5 RID: 66277 RVA: 0x00381A8B File Offset: 0x0037FC8B
			// (set) Token: 0x060102E6 RID: 66278 RVA: 0x00381A93 File Offset: 0x0037FC93
			public ConversionRule newDsl_construction { get; private set; }

			// Token: 0x17002B1B RID: 11035
			// (get) Token: 0x060102E7 RID: 66279 RVA: 0x00381A9C File Offset: 0x0037FC9C
			// (set) Token: 0x060102E8 RID: 66280 RVA: 0x00381AA4 File Offset: 0x0037FCA4
			public ConversionRule sequenceChildren_children { get; private set; }

			// Token: 0x17002B1C RID: 11036
			// (get) Token: 0x060102E9 RID: 66281 RVA: 0x00381AAD File Offset: 0x0037FCAD
			// (set) Token: 0x060102EA RID: 66282 RVA: 0x00381AB5 File Offset: 0x0037FCB5
			public ConversionRule sequenceChildren_convertSequence { get; private set; }

			// Token: 0x17002B1D RID: 11037
			// (get) Token: 0x060102EB RID: 66283 RVA: 0x00381ABE File Offset: 0x0037FCBE
			// (set) Token: 0x060102EC RID: 66284 RVA: 0x00381AC6 File Offset: 0x0037FCC6
			public ConversionRule relChildList_singleRelChildList { get; private set; }

			// Token: 0x17002B1E RID: 11038
			// (get) Token: 0x060102ED RID: 66285 RVA: 0x00381ACF File Offset: 0x0037FCCF
			// (set) Token: 0x060102EE RID: 66286 RVA: 0x00381AD7 File Offset: 0x0037FCD7
			public ConversionRule children_interval { get; private set; }

			// Token: 0x060102EF RID: 66287 RVA: 0x00381AE0 File Offset: 0x0037FCE0
			public GrammarUnnamedConversions(Grammar grammar)
			{
				this.match_pred = (ConversionRule)grammar.Rule("~convert_match_pred");
				this.newDsl_select = (ConversionRule)grammar.Rule("~convert_newDsl_select");
				this.newDsl_construction = (ConversionRule)grammar.Rule("~convert_newDsl_construction");
				this.sequenceChildren_children = (ConversionRule)grammar.Rule("~convert_sequenceChildren_children");
				this.sequenceChildren_convertSequence = (ConversionRule)grammar.Rule("~convert_sequenceChildren_convertSequence");
				this.relChildList_singleRelChildList = (ConversionRule)grammar.Rule("~convert_relChildList_singleRelChildList");
				this.children_interval = (ConversionRule)grammar.Rule("~convert_children_interval");
			}
		}

		// Token: 0x02001E3F RID: 7743
		public class GrammarHoles
		{
			// Token: 0x17002B1F RID: 11039
			// (get) Token: 0x060102F0 RID: 66288 RVA: 0x00381B8D File Offset: 0x0037FD8D
			// (set) Token: 0x060102F1 RID: 66289 RVA: 0x00381B95 File Offset: 0x0037FD95
			public Hole v { get; private set; }

			// Token: 0x17002B20 RID: 11040
			// (get) Token: 0x060102F2 RID: 66290 RVA: 0x00381B9E File Offset: 0x0037FD9E
			// (set) Token: 0x060102F3 RID: 66291 RVA: 0x00381BA6 File Offset: 0x0037FDA6
			public Hole guardedRule { get; private set; }

			// Token: 0x17002B21 RID: 11041
			// (get) Token: 0x060102F4 RID: 66292 RVA: 0x00381BAF File Offset: 0x0037FDAF
			// (set) Token: 0x060102F5 RID: 66293 RVA: 0x00381BB7 File Offset: 0x0037FDB7
			public Hole match { get; private set; }

			// Token: 0x17002B22 RID: 11042
			// (get) Token: 0x060102F6 RID: 66294 RVA: 0x00381BC0 File Offset: 0x0037FDC0
			// (set) Token: 0x060102F7 RID: 66295 RVA: 0x00381BC8 File Offset: 0x0037FDC8
			public Hole pred { get; private set; }

			// Token: 0x17002B23 RID: 11043
			// (get) Token: 0x060102F8 RID: 66296 RVA: 0x00381BD1 File Offset: 0x0037FDD1
			// (set) Token: 0x060102F9 RID: 66297 RVA: 0x00381BD9 File Offset: 0x0037FDD9
			public Hole newDsl { get; private set; }

			// Token: 0x17002B24 RID: 11044
			// (get) Token: 0x060102FA RID: 66298 RVA: 0x00381BE2 File Offset: 0x0037FDE2
			// (set) Token: 0x060102FB RID: 66299 RVA: 0x00381BEA File Offset: 0x0037FDEA
			public Hole construction { get; private set; }

			// Token: 0x17002B25 RID: 11045
			// (get) Token: 0x060102FC RID: 66300 RVA: 0x00381BF3 File Offset: 0x0037FDF3
			// (set) Token: 0x060102FD RID: 66301 RVA: 0x00381BFB File Offset: 0x0037FDFB
			public Hole select { get; private set; }

			// Token: 0x17002B26 RID: 11046
			// (get) Token: 0x060102FE RID: 66302 RVA: 0x00381C04 File Offset: 0x0037FE04
			// (set) Token: 0x060102FF RID: 66303 RVA: 0x00381C0C File Offset: 0x0037FE0C
			public Hole tmpFilter { get; private set; }

			// Token: 0x17002B27 RID: 11047
			// (get) Token: 0x06010300 RID: 66304 RVA: 0x00381C15 File Offset: 0x0037FE15
			// (set) Token: 0x06010301 RID: 66305 RVA: 0x00381C1D File Offset: 0x0037FE1D
			public Hole x { get; private set; }

			// Token: 0x17002B28 RID: 11048
			// (get) Token: 0x06010302 RID: 66306 RVA: 0x00381C26 File Offset: 0x0037FE26
			// (set) Token: 0x06010303 RID: 66307 RVA: 0x00381C2E File Offset: 0x0037FE2E
			public Hole sequenceChildren { get; private set; }

			// Token: 0x17002B29 RID: 11049
			// (get) Token: 0x06010304 RID: 66308 RVA: 0x00381C37 File Offset: 0x0037FE37
			// (set) Token: 0x06010305 RID: 66309 RVA: 0x00381C3F File Offset: 0x0037FE3F
			public Hole convertSequence { get; private set; }

			// Token: 0x17002B2A RID: 11050
			// (get) Token: 0x06010306 RID: 66310 RVA: 0x00381C48 File Offset: 0x0037FE48
			// (set) Token: 0x06010307 RID: 66311 RVA: 0x00381C50 File Offset: 0x0037FE50
			public Hole parent { get; private set; }

			// Token: 0x17002B2B RID: 11051
			// (get) Token: 0x06010308 RID: 66312 RVA: 0x00381C59 File Offset: 0x0037FE59
			// (set) Token: 0x06010309 RID: 66313 RVA: 0x00381C61 File Offset: 0x0037FE61
			public Hole sequenceMap { get; private set; }

			// Token: 0x17002B2C RID: 11052
			// (get) Token: 0x0601030A RID: 66314 RVA: 0x00381C6A File Offset: 0x0037FE6A
			// (set) Token: 0x0601030B RID: 66315 RVA: 0x00381C72 File Offset: 0x0037FE72
			public Hole selectedNode { get; private set; }

			// Token: 0x17002B2D RID: 11053
			// (get) Token: 0x0601030C RID: 66316 RVA: 0x00381C7B File Offset: 0x0037FE7B
			// (set) Token: 0x0601030D RID: 66317 RVA: 0x00381C83 File Offset: 0x0037FE83
			public Hole parentChildren { get; private set; }

			// Token: 0x17002B2E RID: 11054
			// (get) Token: 0x0601030E RID: 66318 RVA: 0x00381C8C File Offset: 0x0037FE8C
			// (set) Token: 0x0601030F RID: 66319 RVA: 0x00381C94 File Offset: 0x0037FE94
			public Hole relChildList { get; private set; }

			// Token: 0x17002B2F RID: 11055
			// (get) Token: 0x06010310 RID: 66320 RVA: 0x00381C9D File Offset: 0x0037FE9D
			// (set) Token: 0x06010311 RID: 66321 RVA: 0x00381CA5 File Offset: 0x0037FEA5
			public Hole singleRelChildList { get; private set; }

			// Token: 0x17002B30 RID: 11056
			// (get) Token: 0x06010312 RID: 66322 RVA: 0x00381CAE File Offset: 0x0037FEAE
			// (set) Token: 0x06010313 RID: 66323 RVA: 0x00381CB6 File Offset: 0x0037FEB6
			public Hole relChild { get; private set; }

			// Token: 0x17002B31 RID: 11057
			// (get) Token: 0x06010314 RID: 66324 RVA: 0x00381CBF File Offset: 0x0037FEBF
			// (set) Token: 0x06010315 RID: 66325 RVA: 0x00381CC7 File Offset: 0x0037FEC7
			public Hole pos { get; private set; }

			// Token: 0x17002B32 RID: 11058
			// (get) Token: 0x06010316 RID: 66326 RVA: 0x00381CD0 File Offset: 0x0037FED0
			// (set) Token: 0x06010317 RID: 66327 RVA: 0x00381CD8 File Offset: 0x0037FED8
			public Hole children { get; private set; }

			// Token: 0x17002B33 RID: 11059
			// (get) Token: 0x06010318 RID: 66328 RVA: 0x00381CE1 File Offset: 0x0037FEE1
			// (set) Token: 0x06010319 RID: 66329 RVA: 0x00381CE9 File Offset: 0x0037FEE9
			public Hole interval { get; private set; }

			// Token: 0x17002B34 RID: 11060
			// (get) Token: 0x0601031A RID: 66330 RVA: 0x00381CF2 File Offset: 0x0037FEF2
			// (set) Token: 0x0601031B RID: 66331 RVA: 0x00381CFA File Offset: 0x0037FEFA
			public Hole inorderAllNodes { get; private set; }

			// Token: 0x17002B35 RID: 11061
			// (get) Token: 0x0601031C RID: 66332 RVA: 0x00381D03 File Offset: 0x0037FF03
			// (set) Token: 0x0601031D RID: 66333 RVA: 0x00381D0B File Offset: 0x0037FF0B
			public Hole label { get; private set; }

			// Token: 0x17002B36 RID: 11062
			// (get) Token: 0x0601031E RID: 66334 RVA: 0x00381D14 File Offset: 0x0037FF14
			// (set) Token: 0x0601031F RID: 66335 RVA: 0x00381D1C File Offset: 0x0037FF1C
			public Hole attributes { get; private set; }

			// Token: 0x17002B37 RID: 11063
			// (get) Token: 0x06010320 RID: 66336 RVA: 0x00381D25 File Offset: 0x0037FF25
			// (set) Token: 0x06010321 RID: 66337 RVA: 0x00381D2D File Offset: 0x0037FF2D
			public Hole kind { get; private set; }

			// Token: 0x17002B38 RID: 11064
			// (get) Token: 0x06010322 RID: 66338 RVA: 0x00381D36 File Offset: 0x0037FF36
			// (set) Token: 0x06010323 RID: 66339 RVA: 0x00381D3E File Offset: 0x0037FF3E
			public Hole name { get; private set; }

			// Token: 0x17002B39 RID: 11065
			// (get) Token: 0x06010324 RID: 66340 RVA: 0x00381D47 File Offset: 0x0037FF47
			// (set) Token: 0x06010325 RID: 66341 RVA: 0x00381D4F File Offset: 0x0037FF4F
			public Hole value { get; private set; }

			// Token: 0x17002B3A RID: 11066
			// (get) Token: 0x06010326 RID: 66342 RVA: 0x00381D58 File Offset: 0x0037FF58
			// (set) Token: 0x06010327 RID: 66343 RVA: 0x00381D60 File Offset: 0x0037FF60
			public Hole k { get; private set; }

			// Token: 0x17002B3B RID: 11067
			// (get) Token: 0x06010328 RID: 66344 RVA: 0x00381D69 File Offset: 0x0037FF69
			// (set) Token: 0x06010329 RID: 66345 RVA: 0x00381D71 File Offset: 0x0037FF71
			public Hole p { get; private set; }

			// Token: 0x17002B3C RID: 11068
			// (get) Token: 0x0601032A RID: 66346 RVA: 0x00381D7A File Offset: 0x0037FF7A
			// (set) Token: 0x0601032B RID: 66347 RVA: 0x00381D82 File Offset: 0x0037FF82
			public Hole path { get; private set; }

			// Token: 0x17002B3D RID: 11069
			// (get) Token: 0x0601032C RID: 66348 RVA: 0x00381D8B File Offset: 0x0037FF8B
			// (set) Token: 0x0601032D RID: 66349 RVA: 0x00381D93 File Offset: 0x0037FF93
			public Hole _LFun0 { get; private set; }

			// Token: 0x17002B3E RID: 11070
			// (get) Token: 0x0601032E RID: 66350 RVA: 0x00381D9C File Offset: 0x0037FF9C
			// (set) Token: 0x0601032F RID: 66351 RVA: 0x00381DA4 File Offset: 0x0037FFA4
			public Hole _LFun1 { get; private set; }

			// Token: 0x06010330 RID: 66352 RVA: 0x00381DB0 File Offset: 0x0037FFB0
			public GrammarHoles(GrammarBuilders builders)
			{
				this.v = new Hole(builders.Symbol.v, null);
				this.guardedRule = new Hole(builders.Symbol.guardedRule, null);
				this.match = new Hole(builders.Symbol.match, null);
				this.pred = new Hole(builders.Symbol.pred, null);
				this.newDsl = new Hole(builders.Symbol.newDsl, null);
				this.construction = new Hole(builders.Symbol.construction, null);
				this.select = new Hole(builders.Symbol.select, null);
				this.tmpFilter = new Hole(builders.Symbol.tmpFilter, null);
				this.x = new Hole(builders.Symbol.x, null);
				this.sequenceChildren = new Hole(builders.Symbol.sequenceChildren, null);
				this.convertSequence = new Hole(builders.Symbol.convertSequence, null);
				this.parent = new Hole(builders.Symbol.parent, null);
				this.sequenceMap = new Hole(builders.Symbol.sequenceMap, null);
				this.selectedNode = new Hole(builders.Symbol.selectedNode, null);
				this.parentChildren = new Hole(builders.Symbol.parentChildren, null);
				this.relChildList = new Hole(builders.Symbol.relChildList, null);
				this.singleRelChildList = new Hole(builders.Symbol.singleRelChildList, null);
				this.relChild = new Hole(builders.Symbol.relChild, null);
				this.pos = new Hole(builders.Symbol.pos, null);
				this.children = new Hole(builders.Symbol.children, null);
				this.interval = new Hole(builders.Symbol.interval, null);
				this.inorderAllNodes = new Hole(builders.Symbol.inorderAllNodes, null);
				this.label = new Hole(builders.Symbol.label, null);
				this.attributes = new Hole(builders.Symbol.attributes, null);
				this.kind = new Hole(builders.Symbol.kind, null);
				this.name = new Hole(builders.Symbol.name, null);
				this.value = new Hole(builders.Symbol.value, null);
				this.k = new Hole(builders.Symbol.k, null);
				this.p = new Hole(builders.Symbol.p, null);
				this.path = new Hole(builders.Symbol.path, null);
				this._LFun0 = new Hole(builders.Symbol._LFun0, null);
				this._LFun1 = new Hole(builders.Symbol._LFun1, null);
			}
		}

		// Token: 0x02001E40 RID: 7744
		public class Nodes
		{
			// Token: 0x06010331 RID: 66353 RVA: 0x003820A4 File Offset: 0x003802A4
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

			// Token: 0x17002B3F RID: 11071
			// (get) Token: 0x06010332 RID: 66354 RVA: 0x00382187 File Offset: 0x00380387
			// (set) Token: 0x06010333 RID: 66355 RVA: 0x0038218F File Offset: 0x0038038F
			public GrammarBuilders.Nodes.NodeRules Rule { get; private set; }

			// Token: 0x17002B40 RID: 11072
			// (get) Token: 0x06010334 RID: 66356 RVA: 0x00382198 File Offset: 0x00380398
			// (set) Token: 0x06010335 RID: 66357 RVA: 0x003821A0 File Offset: 0x003803A0
			public GrammarBuilders.Nodes.NodeUnnamedConversionRules UnnamedConversion { get; private set; }

			// Token: 0x17002B41 RID: 11073
			// (get) Token: 0x06010336 RID: 66358 RVA: 0x003821A9 File Offset: 0x003803A9
			public GrammarBuilders.Nodes.NodeVariables Variable
			{
				get
				{
					return this._variable.Value;
				}
			}

			// Token: 0x17002B42 RID: 11074
			// (get) Token: 0x06010337 RID: 66359 RVA: 0x003821B6 File Offset: 0x003803B6
			public GrammarBuilders.Nodes.NodeHoles Hole
			{
				get
				{
					return this._hole.Value;
				}
			}

			// Token: 0x17002B43 RID: 11075
			// (get) Token: 0x06010338 RID: 66360 RVA: 0x003821C3 File Offset: 0x003803C3
			// (set) Token: 0x06010339 RID: 66361 RVA: 0x003821CB File Offset: 0x003803CB
			public GrammarBuilders.Nodes.NodeUnsafe Unsafe { get; private set; }

			// Token: 0x17002B44 RID: 11076
			// (get) Token: 0x0601033A RID: 66362 RVA: 0x003821D4 File Offset: 0x003803D4
			// (set) Token: 0x0601033B RID: 66363 RVA: 0x003821DC File Offset: 0x003803DC
			public GrammarBuilders.Nodes.NodeCast Cast { get; private set; }

			// Token: 0x17002B45 RID: 11077
			// (get) Token: 0x0601033C RID: 66364 RVA: 0x003821E5 File Offset: 0x003803E5
			// (set) Token: 0x0601033D RID: 66365 RVA: 0x003821ED File Offset: 0x003803ED
			public GrammarBuilders.Nodes.RuleCast CastRule { get; private set; }

			// Token: 0x17002B46 RID: 11078
			// (get) Token: 0x0601033E RID: 66366 RVA: 0x003821F6 File Offset: 0x003803F6
			// (set) Token: 0x0601033F RID: 66367 RVA: 0x003821FE File Offset: 0x003803FE
			public GrammarBuilders.Nodes.NodeIs Is { get; private set; }

			// Token: 0x17002B47 RID: 11079
			// (get) Token: 0x06010340 RID: 66368 RVA: 0x00382207 File Offset: 0x00380407
			// (set) Token: 0x06010341 RID: 66369 RVA: 0x0038220F File Offset: 0x0038040F
			public GrammarBuilders.Nodes.RuleIs IsRule { get; private set; }

			// Token: 0x17002B48 RID: 11080
			// (get) Token: 0x06010342 RID: 66370 RVA: 0x00382218 File Offset: 0x00380418
			// (set) Token: 0x06010343 RID: 66371 RVA: 0x00382220 File Offset: 0x00380420
			public GrammarBuilders.Nodes.NodeAs As { get; private set; }

			// Token: 0x17002B49 RID: 11081
			// (get) Token: 0x06010344 RID: 66372 RVA: 0x00382229 File Offset: 0x00380429
			// (set) Token: 0x06010345 RID: 66373 RVA: 0x00382231 File Offset: 0x00380431
			public GrammarBuilders.Nodes.RuleAs AsRule { get; private set; }

			// Token: 0x04006219 RID: 25113
			private readonly Lazy<GrammarBuilders.Nodes.NodeVariables> _variable;

			// Token: 0x0400621A RID: 25114
			private readonly Lazy<GrammarBuilders.Nodes.NodeHoles> _hole;

			// Token: 0x02001E41 RID: 7745
			public class NodeRules
			{
				// Token: 0x06010346 RID: 66374 RVA: 0x0038223A File Offset: 0x0038043A
				public NodeRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010347 RID: 66375 RVA: 0x00382249 File Offset: 0x00380449
				public label label(string value)
				{
					return new label(this._builders, value);
				}

				// Token: 0x06010348 RID: 66376 RVA: 0x00382257 File Offset: 0x00380457
				public attributes attributes(Dictionary<string, string> value)
				{
					return new attributes(this._builders, value);
				}

				// Token: 0x06010349 RID: 66377 RVA: 0x00382265 File Offset: 0x00380465
				public kind kind(string value)
				{
					return new kind(this._builders, value);
				}

				// Token: 0x0601034A RID: 66378 RVA: 0x00382273 File Offset: 0x00380473
				public name name(string value)
				{
					return new name(this._builders, value);
				}

				// Token: 0x0601034B RID: 66379 RVA: 0x00382281 File Offset: 0x00380481
				public value value(string value)
				{
					return new value(this._builders, value);
				}

				// Token: 0x0601034C RID: 66380 RVA: 0x0038228F File Offset: 0x0038048F
				public k k(int value)
				{
					return new k(this._builders, value);
				}

				// Token: 0x0601034D RID: 66381 RVA: 0x0038229D File Offset: 0x0038049D
				public p p(int value)
				{
					return new p(this._builders, value);
				}

				// Token: 0x0601034E RID: 66382 RVA: 0x003822AB File Offset: 0x003804AB
				public path path(TreePath value)
				{
					return new path(this._builders, value);
				}

				// Token: 0x0601034F RID: 66383 RVA: 0x003822B9 File Offset: 0x003804B9
				public guardedRule GuardedRule(match value0, newDsl value1)
				{
					return new GuardedRule(this._builders, value0, value1);
				}

				// Token: 0x06010350 RID: 66384 RVA: 0x003822CD File Offset: 0x003804CD
				public match Conj(pred value0, match value1)
				{
					return new Conj(this._builders, value0, value1);
				}

				// Token: 0x06010351 RID: 66385 RVA: 0x003822E1 File Offset: 0x003804E1
				public pred IsKind(x value0, path value1, kind value2)
				{
					return new IsKind(this._builders, value0, value1, value2);
				}

				// Token: 0x06010352 RID: 66386 RVA: 0x003822F6 File Offset: 0x003804F6
				public pred IsAttributePresent(x value0, path value1, name value2, value value3)
				{
					return new IsAttributePresent(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06010353 RID: 66387 RVA: 0x0038230D File Offset: 0x0038050D
				public pred IsNthChild(x value0, k value1)
				{
					return new IsNthChild(this._builders, value0, value1);
				}

				// Token: 0x06010354 RID: 66388 RVA: 0x00382321 File Offset: 0x00380521
				public pred HasNChildren(x value0, path value1, k value2)
				{
					return new HasNChildren(this._builders, value0, value1, value2);
				}

				// Token: 0x06010355 RID: 66389 RVA: 0x00382336 File Offset: 0x00380536
				public pred Not(pred value0)
				{
					return new Not(this._builders, value0);
				}

				// Token: 0x06010356 RID: 66390 RVA: 0x00382349 File Offset: 0x00380549
				public construction LeafConstLabelNode(label value0, attributes value1)
				{
					return new LeafConstLabelNode(this._builders, value0, value1);
				}

				// Token: 0x06010357 RID: 66391 RVA: 0x0038235D File Offset: 0x0038055D
				public construction ConstLabelNode(label value0, attributes value1, children value2)
				{
					return new ConstLabelNode(this._builders, value0, value1, value2);
				}

				// Token: 0x06010358 RID: 66392 RVA: 0x00382372 File Offset: 0x00380572
				public construction ConstSequenceLabelNode(label value0, attributes value1, construction value2, sequenceChildren value3)
				{
					return new ConstSequenceLabelNode(this._builders, value0, value1, value2, value3);
				}

				// Token: 0x06010359 RID: 66393 RVA: 0x00382389 File Offset: 0x00380589
				public construction LeafConstSequenceLabelNode(label value0, attributes value1, construction value2)
				{
					return new LeafConstSequenceLabelNode(this._builders, value0, value1, value2);
				}

				// Token: 0x0601035A RID: 66394 RVA: 0x0038239E File Offset: 0x0038059E
				public sequenceChildren InsertAtAbs(select value0, pos value1, newDsl value2)
				{
					return new InsertAtAbs(this._builders, value0, value1, value2);
				}

				// Token: 0x0601035B RID: 66395 RVA: 0x003823B3 File Offset: 0x003805B3
				public sequenceChildren InsertAtRel(select value0, relChild value1, newDsl value2)
				{
					return new InsertAtRel(this._builders, value0, value1, value2);
				}

				// Token: 0x0601035C RID: 66396 RVA: 0x003823C8 File Offset: 0x003805C8
				public sequenceChildren DeleteChild(select value0, relChild value1)
				{
					return new DeleteChild(this._builders, value0, value1);
				}

				// Token: 0x0601035D RID: 66397 RVA: 0x003823DC File Offset: 0x003805DC
				public sequenceChildren ReplaceChildren(select value0, relChildList value1, children value2)
				{
					return new ReplaceChildren(this._builders, value0, value1, value2);
				}

				// Token: 0x0601035E RID: 66398 RVA: 0x003823F1 File Offset: 0x003805F1
				public parentChildren Children(parent value0)
				{
					return new Children(this._builders, value0);
				}

				// Token: 0x0601035F RID: 66399 RVA: 0x00382404 File Offset: 0x00380604
				public relChildList ConcatChild(singleRelChildList value0, relChildList value1)
				{
					return new ConcatChild(this._builders, value0, value1);
				}

				// Token: 0x06010360 RID: 66400 RVA: 0x00382418 File Offset: 0x00380618
				public singleRelChildList SinglePosList(relChild value0)
				{
					return new SinglePosList(this._builders, value0);
				}

				// Token: 0x06010361 RID: 66401 RVA: 0x0038242B File Offset: 0x0038062B
				public relChild RelChild(select value0)
				{
					return new RelChild(this._builders, value0);
				}

				// Token: 0x06010362 RID: 66402 RVA: 0x0038243E File Offset: 0x0038063E
				public pos AbsPos(p value0)
				{
					return new AbsPos(this._builders, value0);
				}

				// Token: 0x06010363 RID: 66403 RVA: 0x00382451 File Offset: 0x00380651
				public children Prepend(interval value0, children value1)
				{
					return new Prepend(this._builders, value0, value1);
				}

				// Token: 0x06010364 RID: 66404 RVA: 0x00382465 File Offset: 0x00380665
				public interval SingleList(newDsl value0)
				{
					return new SingleList(this._builders, value0);
				}

				// Token: 0x06010365 RID: 66405 RVA: 0x00382478 File Offset: 0x00380678
				public inorderAllNodes InOrderAllNodes(selectedNode value0)
				{
					return new InOrderAllNodes(this._builders, value0);
				}

				// Token: 0x06010366 RID: 66406 RVA: 0x0038248B File Offset: 0x0038068B
				public select Select(tmpFilter value0, k value1)
				{
					return new Select(this._builders, value0, value1);
				}

				// Token: 0x06010367 RID: 66407 RVA: 0x0038249F File Offset: 0x0038069F
				public tmpFilter TmpFilter(match value0, inorderAllNodes value1)
				{
					return new TmpFilter(this._builders, value0, value1);
				}

				// Token: 0x06010368 RID: 66408 RVA: 0x003824B3 File Offset: 0x003806B3
				public sequenceMap SequenceMap(newDsl value0, parentChildren value1)
				{
					return new SequenceMap(this._builders, value0, value1);
				}

				// Token: 0x06010369 RID: 66409 RVA: 0x003824C7 File Offset: 0x003806C7
				public convertSequence ConvertSequence(select value0, sequenceMap value1)
				{
					return new ConvertSequence(this._builders, value0, value1);
				}

				// Token: 0x04006222 RID: 25122
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E42 RID: 7746
			public class NodeUnnamedConversionRules
			{
				// Token: 0x0601036A RID: 66410 RVA: 0x003824DB File Offset: 0x003806DB
				public NodeUnnamedConversionRules(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0601036B RID: 66411 RVA: 0x003824EA File Offset: 0x003806EA
				public match match_pred(pred value0)
				{
					return new match_pred(this._builders, value0);
				}

				// Token: 0x0601036C RID: 66412 RVA: 0x003824FD File Offset: 0x003806FD
				public newDsl newDsl_select(select value0)
				{
					return new newDsl_select(this._builders, value0);
				}

				// Token: 0x0601036D RID: 66413 RVA: 0x00382510 File Offset: 0x00380710
				public newDsl newDsl_construction(construction value0)
				{
					return new newDsl_construction(this._builders, value0);
				}

				// Token: 0x0601036E RID: 66414 RVA: 0x00382523 File Offset: 0x00380723
				public sequenceChildren sequenceChildren_children(children value0)
				{
					return new sequenceChildren_children(this._builders, value0);
				}

				// Token: 0x0601036F RID: 66415 RVA: 0x00382536 File Offset: 0x00380736
				public sequenceChildren sequenceChildren_convertSequence(convertSequence value0)
				{
					return new sequenceChildren_convertSequence(this._builders, value0);
				}

				// Token: 0x06010370 RID: 66416 RVA: 0x00382549 File Offset: 0x00380749
				public relChildList relChildList_singleRelChildList(singleRelChildList value0)
				{
					return new relChildList_singleRelChildList(this._builders, value0);
				}

				// Token: 0x06010371 RID: 66417 RVA: 0x0038255C File Offset: 0x0038075C
				public children children_interval(interval value0)
				{
					return new children_interval(this._builders, value0);
				}

				// Token: 0x04006223 RID: 25123
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E43 RID: 7747
			public class NodeVariables
			{
				// Token: 0x17002B4A RID: 11082
				// (get) Token: 0x06010372 RID: 66418 RVA: 0x0038256F File Offset: 0x0038076F
				// (set) Token: 0x06010373 RID: 66419 RVA: 0x00382577 File Offset: 0x00380777
				public v v { get; private set; }

				// Token: 0x17002B4B RID: 11083
				// (get) Token: 0x06010374 RID: 66420 RVA: 0x00382580 File Offset: 0x00380780
				// (set) Token: 0x06010375 RID: 66421 RVA: 0x00382588 File Offset: 0x00380788
				public x x { get; private set; }

				// Token: 0x17002B4C RID: 11084
				// (get) Token: 0x06010376 RID: 66422 RVA: 0x00382591 File Offset: 0x00380791
				// (set) Token: 0x06010377 RID: 66423 RVA: 0x00382599 File Offset: 0x00380799
				public parent parent { get; private set; }

				// Token: 0x17002B4D RID: 11085
				// (get) Token: 0x06010378 RID: 66424 RVA: 0x003825A2 File Offset: 0x003807A2
				// (set) Token: 0x06010379 RID: 66425 RVA: 0x003825AA File Offset: 0x003807AA
				public selectedNode selectedNode { get; private set; }

				// Token: 0x0601037A RID: 66426 RVA: 0x003825B3 File Offset: 0x003807B3
				public NodeVariables(GrammarBuilders builders)
				{
					this.v = new v(builders);
					this.x = new x(builders);
					this.parent = new parent(builders);
					this.selectedNode = new selectedNode(builders);
				}
			}

			// Token: 0x02001E44 RID: 7748
			public class NodeHoles
			{
				// Token: 0x17002B4E RID: 11086
				// (get) Token: 0x0601037B RID: 66427 RVA: 0x003825EB File Offset: 0x003807EB
				// (set) Token: 0x0601037C RID: 66428 RVA: 0x003825F3 File Offset: 0x003807F3
				public guardedRule guardedRule { get; private set; }

				// Token: 0x17002B4F RID: 11087
				// (get) Token: 0x0601037D RID: 66429 RVA: 0x003825FC File Offset: 0x003807FC
				// (set) Token: 0x0601037E RID: 66430 RVA: 0x00382604 File Offset: 0x00380804
				public match match { get; private set; }

				// Token: 0x17002B50 RID: 11088
				// (get) Token: 0x0601037F RID: 66431 RVA: 0x0038260D File Offset: 0x0038080D
				// (set) Token: 0x06010380 RID: 66432 RVA: 0x00382615 File Offset: 0x00380815
				public pred pred { get; private set; }

				// Token: 0x17002B51 RID: 11089
				// (get) Token: 0x06010381 RID: 66433 RVA: 0x0038261E File Offset: 0x0038081E
				// (set) Token: 0x06010382 RID: 66434 RVA: 0x00382626 File Offset: 0x00380826
				public newDsl newDsl { get; private set; }

				// Token: 0x17002B52 RID: 11090
				// (get) Token: 0x06010383 RID: 66435 RVA: 0x0038262F File Offset: 0x0038082F
				// (set) Token: 0x06010384 RID: 66436 RVA: 0x00382637 File Offset: 0x00380837
				public construction construction { get; private set; }

				// Token: 0x17002B53 RID: 11091
				// (get) Token: 0x06010385 RID: 66437 RVA: 0x00382640 File Offset: 0x00380840
				// (set) Token: 0x06010386 RID: 66438 RVA: 0x00382648 File Offset: 0x00380848
				public select select { get; private set; }

				// Token: 0x17002B54 RID: 11092
				// (get) Token: 0x06010387 RID: 66439 RVA: 0x00382651 File Offset: 0x00380851
				// (set) Token: 0x06010388 RID: 66440 RVA: 0x00382659 File Offset: 0x00380859
				public tmpFilter tmpFilter { get; private set; }

				// Token: 0x17002B55 RID: 11093
				// (get) Token: 0x06010389 RID: 66441 RVA: 0x00382662 File Offset: 0x00380862
				// (set) Token: 0x0601038A RID: 66442 RVA: 0x0038266A File Offset: 0x0038086A
				public x x { get; private set; }

				// Token: 0x17002B56 RID: 11094
				// (get) Token: 0x0601038B RID: 66443 RVA: 0x00382673 File Offset: 0x00380873
				// (set) Token: 0x0601038C RID: 66444 RVA: 0x0038267B File Offset: 0x0038087B
				public sequenceChildren sequenceChildren { get; private set; }

				// Token: 0x17002B57 RID: 11095
				// (get) Token: 0x0601038D RID: 66445 RVA: 0x00382684 File Offset: 0x00380884
				// (set) Token: 0x0601038E RID: 66446 RVA: 0x0038268C File Offset: 0x0038088C
				public convertSequence convertSequence { get; private set; }

				// Token: 0x17002B58 RID: 11096
				// (get) Token: 0x0601038F RID: 66447 RVA: 0x00382695 File Offset: 0x00380895
				// (set) Token: 0x06010390 RID: 66448 RVA: 0x0038269D File Offset: 0x0038089D
				public parent parent { get; private set; }

				// Token: 0x17002B59 RID: 11097
				// (get) Token: 0x06010391 RID: 66449 RVA: 0x003826A6 File Offset: 0x003808A6
				// (set) Token: 0x06010392 RID: 66450 RVA: 0x003826AE File Offset: 0x003808AE
				public sequenceMap sequenceMap { get; private set; }

				// Token: 0x17002B5A RID: 11098
				// (get) Token: 0x06010393 RID: 66451 RVA: 0x003826B7 File Offset: 0x003808B7
				// (set) Token: 0x06010394 RID: 66452 RVA: 0x003826BF File Offset: 0x003808BF
				public selectedNode selectedNode { get; private set; }

				// Token: 0x17002B5B RID: 11099
				// (get) Token: 0x06010395 RID: 66453 RVA: 0x003826C8 File Offset: 0x003808C8
				// (set) Token: 0x06010396 RID: 66454 RVA: 0x003826D0 File Offset: 0x003808D0
				public parentChildren parentChildren { get; private set; }

				// Token: 0x17002B5C RID: 11100
				// (get) Token: 0x06010397 RID: 66455 RVA: 0x003826D9 File Offset: 0x003808D9
				// (set) Token: 0x06010398 RID: 66456 RVA: 0x003826E1 File Offset: 0x003808E1
				public relChildList relChildList { get; private set; }

				// Token: 0x17002B5D RID: 11101
				// (get) Token: 0x06010399 RID: 66457 RVA: 0x003826EA File Offset: 0x003808EA
				// (set) Token: 0x0601039A RID: 66458 RVA: 0x003826F2 File Offset: 0x003808F2
				public singleRelChildList singleRelChildList { get; private set; }

				// Token: 0x17002B5E RID: 11102
				// (get) Token: 0x0601039B RID: 66459 RVA: 0x003826FB File Offset: 0x003808FB
				// (set) Token: 0x0601039C RID: 66460 RVA: 0x00382703 File Offset: 0x00380903
				public relChild relChild { get; private set; }

				// Token: 0x17002B5F RID: 11103
				// (get) Token: 0x0601039D RID: 66461 RVA: 0x0038270C File Offset: 0x0038090C
				// (set) Token: 0x0601039E RID: 66462 RVA: 0x00382714 File Offset: 0x00380914
				public pos pos { get; private set; }

				// Token: 0x17002B60 RID: 11104
				// (get) Token: 0x0601039F RID: 66463 RVA: 0x0038271D File Offset: 0x0038091D
				// (set) Token: 0x060103A0 RID: 66464 RVA: 0x00382725 File Offset: 0x00380925
				public children children { get; private set; }

				// Token: 0x17002B61 RID: 11105
				// (get) Token: 0x060103A1 RID: 66465 RVA: 0x0038272E File Offset: 0x0038092E
				// (set) Token: 0x060103A2 RID: 66466 RVA: 0x00382736 File Offset: 0x00380936
				public interval interval { get; private set; }

				// Token: 0x17002B62 RID: 11106
				// (get) Token: 0x060103A3 RID: 66467 RVA: 0x0038273F File Offset: 0x0038093F
				// (set) Token: 0x060103A4 RID: 66468 RVA: 0x00382747 File Offset: 0x00380947
				public inorderAllNodes inorderAllNodes { get; private set; }

				// Token: 0x17002B63 RID: 11107
				// (get) Token: 0x060103A5 RID: 66469 RVA: 0x00382750 File Offset: 0x00380950
				// (set) Token: 0x060103A6 RID: 66470 RVA: 0x00382758 File Offset: 0x00380958
				public label label { get; private set; }

				// Token: 0x17002B64 RID: 11108
				// (get) Token: 0x060103A7 RID: 66471 RVA: 0x00382761 File Offset: 0x00380961
				// (set) Token: 0x060103A8 RID: 66472 RVA: 0x00382769 File Offset: 0x00380969
				public attributes attributes { get; private set; }

				// Token: 0x17002B65 RID: 11109
				// (get) Token: 0x060103A9 RID: 66473 RVA: 0x00382772 File Offset: 0x00380972
				// (set) Token: 0x060103AA RID: 66474 RVA: 0x0038277A File Offset: 0x0038097A
				public kind kind { get; private set; }

				// Token: 0x17002B66 RID: 11110
				// (get) Token: 0x060103AB RID: 66475 RVA: 0x00382783 File Offset: 0x00380983
				// (set) Token: 0x060103AC RID: 66476 RVA: 0x0038278B File Offset: 0x0038098B
				public name name { get; private set; }

				// Token: 0x17002B67 RID: 11111
				// (get) Token: 0x060103AD RID: 66477 RVA: 0x00382794 File Offset: 0x00380994
				// (set) Token: 0x060103AE RID: 66478 RVA: 0x0038279C File Offset: 0x0038099C
				public value value { get; private set; }

				// Token: 0x17002B68 RID: 11112
				// (get) Token: 0x060103AF RID: 66479 RVA: 0x003827A5 File Offset: 0x003809A5
				// (set) Token: 0x060103B0 RID: 66480 RVA: 0x003827AD File Offset: 0x003809AD
				public k k { get; private set; }

				// Token: 0x17002B69 RID: 11113
				// (get) Token: 0x060103B1 RID: 66481 RVA: 0x003827B6 File Offset: 0x003809B6
				// (set) Token: 0x060103B2 RID: 66482 RVA: 0x003827BE File Offset: 0x003809BE
				public p p { get; private set; }

				// Token: 0x17002B6A RID: 11114
				// (get) Token: 0x060103B3 RID: 66483 RVA: 0x003827C7 File Offset: 0x003809C7
				// (set) Token: 0x060103B4 RID: 66484 RVA: 0x003827CF File Offset: 0x003809CF
				public path path { get; private set; }

				// Token: 0x060103B5 RID: 66485 RVA: 0x003827D8 File Offset: 0x003809D8
				public NodeHoles(GrammarBuilders builders)
				{
					this.guardedRule = guardedRule.CreateHole(builders, null);
					this.match = match.CreateHole(builders, null);
					this.pred = pred.CreateHole(builders, null);
					this.newDsl = newDsl.CreateHole(builders, null);
					this.construction = construction.CreateHole(builders, null);
					this.select = select.CreateHole(builders, null);
					this.tmpFilter = tmpFilter.CreateHole(builders, null);
					this.x = x.CreateHole(builders, null);
					this.sequenceChildren = sequenceChildren.CreateHole(builders, null);
					this.convertSequence = convertSequence.CreateHole(builders, null);
					this.parent = parent.CreateHole(builders, null);
					this.sequenceMap = sequenceMap.CreateHole(builders, null);
					this.selectedNode = selectedNode.CreateHole(builders, null);
					this.parentChildren = parentChildren.CreateHole(builders, null);
					this.relChildList = relChildList.CreateHole(builders, null);
					this.singleRelChildList = singleRelChildList.CreateHole(builders, null);
					this.relChild = relChild.CreateHole(builders, null);
					this.pos = pos.CreateHole(builders, null);
					this.children = children.CreateHole(builders, null);
					this.interval = interval.CreateHole(builders, null);
					this.inorderAllNodes = inorderAllNodes.CreateHole(builders, null);
					this.label = label.CreateHole(builders, null);
					this.attributes = attributes.CreateHole(builders, null);
					this.kind = kind.CreateHole(builders, null);
					this.name = name.CreateHole(builders, null);
					this.value = value.CreateHole(builders, null);
					this.k = k.CreateHole(builders, null);
					this.p = p.CreateHole(builders, null);
					this.path = path.CreateHole(builders, null);
				}
			}

			// Token: 0x02001E45 RID: 7749
			public class NodeUnsafe
			{
				// Token: 0x060103B6 RID: 66486 RVA: 0x00382964 File Offset: 0x00380B64
				public guardedRule guardedRule(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule.CreateUnsafe(node);
				}

				// Token: 0x060103B7 RID: 66487 RVA: 0x0038296C File Offset: 0x00380B6C
				public match match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match.CreateUnsafe(node);
				}

				// Token: 0x060103B8 RID: 66488 RVA: 0x00382974 File Offset: 0x00380B74
				public pred pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred.CreateUnsafe(node);
				}

				// Token: 0x060103B9 RID: 66489 RVA: 0x0038297C File Offset: 0x00380B7C
				public newDsl newDsl(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl.CreateUnsafe(node);
				}

				// Token: 0x060103BA RID: 66490 RVA: 0x00382984 File Offset: 0x00380B84
				public construction construction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction.CreateUnsafe(node);
				}

				// Token: 0x060103BB RID: 66491 RVA: 0x0038298C File Offset: 0x00380B8C
				public select select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select.CreateUnsafe(node);
				}

				// Token: 0x060103BC RID: 66492 RVA: 0x00382994 File Offset: 0x00380B94
				public tmpFilter tmpFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter.CreateUnsafe(node);
				}

				// Token: 0x060103BD RID: 66493 RVA: 0x0038299C File Offset: 0x00380B9C
				public x x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x.CreateUnsafe(node);
				}

				// Token: 0x060103BE RID: 66494 RVA: 0x003829A4 File Offset: 0x00380BA4
				public sequenceChildren sequenceChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren.CreateUnsafe(node);
				}

				// Token: 0x060103BF RID: 66495 RVA: 0x003829AC File Offset: 0x00380BAC
				public convertSequence convertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence.CreateUnsafe(node);
				}

				// Token: 0x060103C0 RID: 66496 RVA: 0x003829B4 File Offset: 0x00380BB4
				public parent parent(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent.CreateUnsafe(node);
				}

				// Token: 0x060103C1 RID: 66497 RVA: 0x003829BC File Offset: 0x00380BBC
				public sequenceMap sequenceMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap.CreateUnsafe(node);
				}

				// Token: 0x060103C2 RID: 66498 RVA: 0x003829C4 File Offset: 0x00380BC4
				public selectedNode selectedNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode.CreateUnsafe(node);
				}

				// Token: 0x060103C3 RID: 66499 RVA: 0x003829CC File Offset: 0x00380BCC
				public parentChildren parentChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren.CreateUnsafe(node);
				}

				// Token: 0x060103C4 RID: 66500 RVA: 0x003829D4 File Offset: 0x00380BD4
				public relChildList relChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList.CreateUnsafe(node);
				}

				// Token: 0x060103C5 RID: 66501 RVA: 0x003829DC File Offset: 0x00380BDC
				public singleRelChildList singleRelChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList.CreateUnsafe(node);
				}

				// Token: 0x060103C6 RID: 66502 RVA: 0x003829E4 File Offset: 0x00380BE4
				public relChild relChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild.CreateUnsafe(node);
				}

				// Token: 0x060103C7 RID: 66503 RVA: 0x003829EC File Offset: 0x00380BEC
				public pos pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos.CreateUnsafe(node);
				}

				// Token: 0x060103C8 RID: 66504 RVA: 0x003829F4 File Offset: 0x00380BF4
				public children children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children.CreateUnsafe(node);
				}

				// Token: 0x060103C9 RID: 66505 RVA: 0x003829FC File Offset: 0x00380BFC
				public interval interval(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval.CreateUnsafe(node);
				}

				// Token: 0x060103CA RID: 66506 RVA: 0x00382A04 File Offset: 0x00380C04
				public inorderAllNodes inorderAllNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes.CreateUnsafe(node);
				}

				// Token: 0x060103CB RID: 66507 RVA: 0x00382A0C File Offset: 0x00380C0C
				public label label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label.CreateUnsafe(node);
				}

				// Token: 0x060103CC RID: 66508 RVA: 0x00382A14 File Offset: 0x00380C14
				public attributes attributes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes.CreateUnsafe(node);
				}

				// Token: 0x060103CD RID: 66509 RVA: 0x00382A1C File Offset: 0x00380C1C
				public kind kind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind.CreateUnsafe(node);
				}

				// Token: 0x060103CE RID: 66510 RVA: 0x00382A24 File Offset: 0x00380C24
				public name name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name.CreateUnsafe(node);
				}

				// Token: 0x060103CF RID: 66511 RVA: 0x00382A2C File Offset: 0x00380C2C
				public value value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value.CreateUnsafe(node);
				}

				// Token: 0x060103D0 RID: 66512 RVA: 0x00382A34 File Offset: 0x00380C34
				public k k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k.CreateUnsafe(node);
				}

				// Token: 0x060103D1 RID: 66513 RVA: 0x00382A3C File Offset: 0x00380C3C
				public p p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p.CreateUnsafe(node);
				}

				// Token: 0x060103D2 RID: 66514 RVA: 0x00382A44 File Offset: 0x00380C44
				public path path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path.CreateUnsafe(node);
				}
			}

			// Token: 0x02001E46 RID: 7750
			public class NodeCast
			{
				// Token: 0x060103D4 RID: 66516 RVA: 0x00382A4C File Offset: 0x00380C4C
				public NodeCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060103D5 RID: 66517 RVA: 0x00382A5C File Offset: 0x00380C5C
				public guardedRule guardedRule(ProgramNode node)
				{
					guardedRule? guardedRule = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule.CreateSafe(this._builders, node);
					if (guardedRule == null)
					{
						string text = "node";
						string text2 = "expected node for symbol guardedRule but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return guardedRule.Value;
				}

				// Token: 0x060103D6 RID: 66518 RVA: 0x00382AB0 File Offset: 0x00380CB0
				public match match(ProgramNode node)
				{
					match? match = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						string text = "node";
						string text2 = "expected node for symbol match but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return match.Value;
				}

				// Token: 0x060103D7 RID: 66519 RVA: 0x00382B04 File Offset: 0x00380D04
				public pred pred(ProgramNode node)
				{
					pred? pred = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pred.Value;
				}

				// Token: 0x060103D8 RID: 66520 RVA: 0x00382B58 File Offset: 0x00380D58
				public newDsl newDsl(ProgramNode node)
				{
					newDsl? newDsl = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl.CreateSafe(this._builders, node);
					if (newDsl == null)
					{
						string text = "node";
						string text2 = "expected node for symbol newDsl but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return newDsl.Value;
				}

				// Token: 0x060103D9 RID: 66521 RVA: 0x00382BAC File Offset: 0x00380DAC
				public construction construction(ProgramNode node)
				{
					construction? construction = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction.CreateSafe(this._builders, node);
					if (construction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol construction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return construction.Value;
				}

				// Token: 0x060103DA RID: 66522 RVA: 0x00382C00 File Offset: 0x00380E00
				public select select(ProgramNode node)
				{
					select? select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select.CreateSafe(this._builders, node);
					if (select == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @select but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return select.Value;
				}

				// Token: 0x060103DB RID: 66523 RVA: 0x00382C54 File Offset: 0x00380E54
				public tmpFilter tmpFilter(ProgramNode node)
				{
					tmpFilter? tmpFilter = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter.CreateSafe(this._builders, node);
					if (tmpFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol tmpFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tmpFilter.Value;
				}

				// Token: 0x060103DC RID: 66524 RVA: 0x00382CA8 File Offset: 0x00380EA8
				public x x(ProgramNode node)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						string text = "node";
						string text2 = "expected node for symbol x but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return x.Value;
				}

				// Token: 0x060103DD RID: 66525 RVA: 0x00382CFC File Offset: 0x00380EFC
				public sequenceChildren sequenceChildren(ProgramNode node)
				{
					sequenceChildren? sequenceChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren.CreateSafe(this._builders, node);
					if (sequenceChildren == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequenceChildren but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceChildren.Value;
				}

				// Token: 0x060103DE RID: 66526 RVA: 0x00382D50 File Offset: 0x00380F50
				public convertSequence convertSequence(ProgramNode node)
				{
					convertSequence? convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence.CreateSafe(this._builders, node);
					if (convertSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol convertSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return convertSequence.Value;
				}

				// Token: 0x060103DF RID: 66527 RVA: 0x00382DA4 File Offset: 0x00380FA4
				public parent parent(ProgramNode node)
				{
					parent? parent = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent.CreateSafe(this._builders, node);
					if (parent == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parent but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parent.Value;
				}

				// Token: 0x060103E0 RID: 66528 RVA: 0x00382DF8 File Offset: 0x00380FF8
				public sequenceMap sequenceMap(ProgramNode node)
				{
					sequenceMap? sequenceMap = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap.CreateSafe(this._builders, node);
					if (sequenceMap == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequenceMap but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceMap.Value;
				}

				// Token: 0x060103E1 RID: 66529 RVA: 0x00382E4C File Offset: 0x0038104C
				public selectedNode selectedNode(ProgramNode node)
				{
					selectedNode? selectedNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode.CreateSafe(this._builders, node);
					if (selectedNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol selectedNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return selectedNode.Value;
				}

				// Token: 0x060103E2 RID: 66530 RVA: 0x00382EA0 File Offset: 0x003810A0
				public parentChildren parentChildren(ProgramNode node)
				{
					parentChildren? parentChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren.CreateSafe(this._builders, node);
					if (parentChildren == null)
					{
						string text = "node";
						string text2 = "expected node for symbol parentChildren but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return parentChildren.Value;
				}

				// Token: 0x060103E3 RID: 66531 RVA: 0x00382EF4 File Offset: 0x003810F4
				public relChildList relChildList(ProgramNode node)
				{
					relChildList? relChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList.CreateSafe(this._builders, node);
					if (relChildList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol relChildList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return relChildList.Value;
				}

				// Token: 0x060103E4 RID: 66532 RVA: 0x00382F48 File Offset: 0x00381148
				public singleRelChildList singleRelChildList(ProgramNode node)
				{
					singleRelChildList? singleRelChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList.CreateSafe(this._builders, node);
					if (singleRelChildList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol singleRelChildList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleRelChildList.Value;
				}

				// Token: 0x060103E5 RID: 66533 RVA: 0x00382F9C File Offset: 0x0038119C
				public relChild relChild(ProgramNode node)
				{
					relChild? relChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild.CreateSafe(this._builders, node);
					if (relChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol relChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return relChild.Value;
				}

				// Token: 0x060103E6 RID: 66534 RVA: 0x00382FF0 File Offset: 0x003811F0
				public pos pos(ProgramNode node)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol pos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return pos.Value;
				}

				// Token: 0x060103E7 RID: 66535 RVA: 0x00383044 File Offset: 0x00381244
				public children children(ProgramNode node)
				{
					children? children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children.CreateSafe(this._builders, node);
					if (children == null)
					{
						string text = "node";
						string text2 = "expected node for symbol children but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return children.Value;
				}

				// Token: 0x060103E8 RID: 66536 RVA: 0x00383098 File Offset: 0x00381298
				public interval interval(ProgramNode node)
				{
					interval? interval = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval.CreateSafe(this._builders, node);
					if (interval == null)
					{
						string text = "node";
						string text2 = "expected node for symbol interval but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return interval.Value;
				}

				// Token: 0x060103E9 RID: 66537 RVA: 0x003830EC File Offset: 0x003812EC
				public inorderAllNodes inorderAllNodes(ProgramNode node)
				{
					inorderAllNodes? inorderAllNodes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes.CreateSafe(this._builders, node);
					if (inorderAllNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol inorderAllNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inorderAllNodes.Value;
				}

				// Token: 0x060103EA RID: 66538 RVA: 0x00383140 File Offset: 0x00381340
				public label label(ProgramNode node)
				{
					label? label = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label.CreateSafe(this._builders, node);
					if (label == null)
					{
						string text = "node";
						string text2 = "expected node for symbol label but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return label.Value;
				}

				// Token: 0x060103EB RID: 66539 RVA: 0x00383194 File Offset: 0x00381394
				public attributes attributes(ProgramNode node)
				{
					attributes? attributes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes.CreateSafe(this._builders, node);
					if (attributes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol attributes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return attributes.Value;
				}

				// Token: 0x060103EC RID: 66540 RVA: 0x003831E8 File Offset: 0x003813E8
				public kind kind(ProgramNode node)
				{
					kind? kind = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind.CreateSafe(this._builders, node);
					if (kind == null)
					{
						string text = "node";
						string text2 = "expected node for symbol kind but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return kind.Value;
				}

				// Token: 0x060103ED RID: 66541 RVA: 0x0038323C File Offset: 0x0038143C
				public name name(ProgramNode node)
				{
					name? name = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						string text = "node";
						string text2 = "expected node for symbol name but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return name.Value;
				}

				// Token: 0x060103EE RID: 66542 RVA: 0x00383290 File Offset: 0x00381490
				public value value(ProgramNode node)
				{
					value? value = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value == null)
					{
						string text = "node";
						string text2 = "expected node for symbol @value but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return value.Value;
				}

				// Token: 0x060103EF RID: 66543 RVA: 0x003832E4 File Offset: 0x003814E4
				public k k(ProgramNode node)
				{
					k? k = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						string text = "node";
						string text2 = "expected node for symbol k but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return k.Value;
				}

				// Token: 0x060103F0 RID: 66544 RVA: 0x00383338 File Offset: 0x00381538
				public p p(ProgramNode node)
				{
					p? p = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p.CreateSafe(this._builders, node);
					if (p == null)
					{
						string text = "node";
						string text2 = "expected node for symbol p but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return p.Value;
				}

				// Token: 0x060103F1 RID: 66545 RVA: 0x0038338C File Offset: 0x0038158C
				public path path(ProgramNode node)
				{
					path? path = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						string text = "node";
						string text2 = "expected node for symbol path but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return path.Value;
				}

				// Token: 0x04006245 RID: 25157
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E47 RID: 7751
			public class RuleCast
			{
				// Token: 0x060103F2 RID: 66546 RVA: 0x003833DD File Offset: 0x003815DD
				public RuleCast(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060103F3 RID: 66547 RVA: 0x003833EC File Offset: 0x003815EC
				public GuardedRule GuardedRule(ProgramNode node)
				{
					GuardedRule? guardedRule = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.GuardedRule.CreateSafe(this._builders, node);
					if (guardedRule == null)
					{
						string text = "node";
						string text2 = "expected node for symbol GuardedRule but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return guardedRule.Value;
				}

				// Token: 0x060103F4 RID: 66548 RVA: 0x00383440 File Offset: 0x00381640
				public match_pred match_pred(ProgramNode node)
				{
					match_pred? match_pred = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.match_pred.CreateSafe(this._builders, node);
					if (match_pred == null)
					{
						string text = "node";
						string text2 = "expected node for symbol match_pred but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return match_pred.Value;
				}

				// Token: 0x060103F5 RID: 66549 RVA: 0x00383494 File Offset: 0x00381694
				public Conj Conj(ProgramNode node)
				{
					Conj? conj = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Conj.CreateSafe(this._builders, node);
					if (conj == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Conj but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return conj.Value;
				}

				// Token: 0x060103F6 RID: 66550 RVA: 0x003834E8 File Offset: 0x003816E8
				public IsKind IsKind(ProgramNode node)
				{
					IsKind? isKind = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsKind.CreateSafe(this._builders, node);
					if (isKind == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsKind but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isKind.Value;
				}

				// Token: 0x060103F7 RID: 66551 RVA: 0x0038353C File Offset: 0x0038173C
				public IsAttributePresent IsAttributePresent(ProgramNode node)
				{
					IsAttributePresent? isAttributePresent = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsAttributePresent.CreateSafe(this._builders, node);
					if (isAttributePresent == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsAttributePresent but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isAttributePresent.Value;
				}

				// Token: 0x060103F8 RID: 66552 RVA: 0x00383590 File Offset: 0x00381790
				public IsNthChild IsNthChild(ProgramNode node)
				{
					IsNthChild? isNthChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsNthChild.CreateSafe(this._builders, node);
					if (isNthChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol IsNthChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return isNthChild.Value;
				}

				// Token: 0x060103F9 RID: 66553 RVA: 0x003835E4 File Offset: 0x003817E4
				public HasNChildren HasNChildren(ProgramNode node)
				{
					HasNChildren? hasNChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.HasNChildren.CreateSafe(this._builders, node);
					if (hasNChildren == null)
					{
						string text = "node";
						string text2 = "expected node for symbol HasNChildren but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return hasNChildren.Value;
				}

				// Token: 0x060103FA RID: 66554 RVA: 0x00383638 File Offset: 0x00381838
				public Not Not(ProgramNode node)
				{
					Not? not = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
					if (not == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Not but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return not.Value;
				}

				// Token: 0x060103FB RID: 66555 RVA: 0x0038368C File Offset: 0x0038188C
				public newDsl_select newDsl_select(ProgramNode node)
				{
					newDsl_select? newDsl_select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_select.CreateSafe(this._builders, node);
					if (newDsl_select == null)
					{
						string text = "node";
						string text2 = "expected node for symbol newDsl_select but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return newDsl_select.Value;
				}

				// Token: 0x060103FC RID: 66556 RVA: 0x003836E0 File Offset: 0x003818E0
				public newDsl_construction newDsl_construction(ProgramNode node)
				{
					newDsl_construction? newDsl_construction = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_construction.CreateSafe(this._builders, node);
					if (newDsl_construction == null)
					{
						string text = "node";
						string text2 = "expected node for symbol newDsl_construction but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return newDsl_construction.Value;
				}

				// Token: 0x060103FD RID: 66557 RVA: 0x00383734 File Offset: 0x00381934
				public LeafConstLabelNode LeafConstLabelNode(ProgramNode node)
				{
					LeafConstLabelNode? leafConstLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstLabelNode.CreateSafe(this._builders, node);
					if (leafConstLabelNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafConstLabelNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafConstLabelNode.Value;
				}

				// Token: 0x060103FE RID: 66558 RVA: 0x00383788 File Offset: 0x00381988
				public ConstLabelNode ConstLabelNode(ProgramNode node)
				{
					ConstLabelNode? constLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstLabelNode.CreateSafe(this._builders, node);
					if (constLabelNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstLabelNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constLabelNode.Value;
				}

				// Token: 0x060103FF RID: 66559 RVA: 0x003837DC File Offset: 0x003819DC
				public ConstSequenceLabelNode ConstSequenceLabelNode(ProgramNode node)
				{
					ConstSequenceLabelNode? constSequenceLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstSequenceLabelNode.CreateSafe(this._builders, node);
					if (constSequenceLabelNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConstSequenceLabelNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return constSequenceLabelNode.Value;
				}

				// Token: 0x06010400 RID: 66560 RVA: 0x00383830 File Offset: 0x00381A30
				public LeafConstSequenceLabelNode LeafConstSequenceLabelNode(ProgramNode node)
				{
					LeafConstSequenceLabelNode? leafConstSequenceLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstSequenceLabelNode.CreateSafe(this._builders, node);
					if (leafConstSequenceLabelNode == null)
					{
						string text = "node";
						string text2 = "expected node for symbol LeafConstSequenceLabelNode but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return leafConstSequenceLabelNode.Value;
				}

				// Token: 0x06010401 RID: 66561 RVA: 0x00383884 File Offset: 0x00381A84
				public Select Select(ProgramNode node)
				{
					Select? select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
					if (select == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Select but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return select.Value;
				}

				// Token: 0x06010402 RID: 66562 RVA: 0x003838D8 File Offset: 0x00381AD8
				public TmpFilter TmpFilter(ProgramNode node)
				{
					TmpFilter? tmpFilter = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.TmpFilter.CreateSafe(this._builders, node);
					if (tmpFilter == null)
					{
						string text = "node";
						string text2 = "expected node for symbol TmpFilter but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return tmpFilter.Value;
				}

				// Token: 0x06010403 RID: 66563 RVA: 0x0038392C File Offset: 0x00381B2C
				public sequenceChildren_children sequenceChildren_children(ProgramNode node)
				{
					sequenceChildren_children? sequenceChildren_children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_children.CreateSafe(this._builders, node);
					if (sequenceChildren_children == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequenceChildren_children but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceChildren_children.Value;
				}

				// Token: 0x06010404 RID: 66564 RVA: 0x00383980 File Offset: 0x00381B80
				public InsertAtAbs InsertAtAbs(ProgramNode node)
				{
					InsertAtAbs? insertAtAbs = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtAbs.CreateSafe(this._builders, node);
					if (insertAtAbs == null)
					{
						string text = "node";
						string text2 = "expected node for symbol InsertAtAbs but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return insertAtAbs.Value;
				}

				// Token: 0x06010405 RID: 66565 RVA: 0x003839D4 File Offset: 0x00381BD4
				public InsertAtRel InsertAtRel(ProgramNode node)
				{
					InsertAtRel? insertAtRel = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtRel.CreateSafe(this._builders, node);
					if (insertAtRel == null)
					{
						string text = "node";
						string text2 = "expected node for symbol InsertAtRel but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return insertAtRel.Value;
				}

				// Token: 0x06010406 RID: 66566 RVA: 0x00383A28 File Offset: 0x00381C28
				public DeleteChild DeleteChild(ProgramNode node)
				{
					DeleteChild? deleteChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.DeleteChild.CreateSafe(this._builders, node);
					if (deleteChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol DeleteChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return deleteChild.Value;
				}

				// Token: 0x06010407 RID: 66567 RVA: 0x00383A7C File Offset: 0x00381C7C
				public ReplaceChildren ReplaceChildren(ProgramNode node)
				{
					ReplaceChildren? replaceChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ReplaceChildren.CreateSafe(this._builders, node);
					if (replaceChildren == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ReplaceChildren but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return replaceChildren.Value;
				}

				// Token: 0x06010408 RID: 66568 RVA: 0x00383AD0 File Offset: 0x00381CD0
				public sequenceChildren_convertSequence sequenceChildren_convertSequence(ProgramNode node)
				{
					sequenceChildren_convertSequence? sequenceChildren_convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_convertSequence.CreateSafe(this._builders, node);
					if (sequenceChildren_convertSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol sequenceChildren_convertSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceChildren_convertSequence.Value;
				}

				// Token: 0x06010409 RID: 66569 RVA: 0x00383B24 File Offset: 0x00381D24
				public ConvertSequence ConvertSequence(ProgramNode node)
				{
					ConvertSequence? convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConvertSequence.CreateSafe(this._builders, node);
					if (convertSequence == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConvertSequence but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return convertSequence.Value;
				}

				// Token: 0x0601040A RID: 66570 RVA: 0x00383B78 File Offset: 0x00381D78
				public SequenceMap SequenceMap(ProgramNode node)
				{
					SequenceMap? sequenceMap = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SequenceMap.CreateSafe(this._builders, node);
					if (sequenceMap == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SequenceMap but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return sequenceMap.Value;
				}

				// Token: 0x0601040B RID: 66571 RVA: 0x00383BCC File Offset: 0x00381DCC
				public Children Children(ProgramNode node)
				{
					Children? children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Children.CreateSafe(this._builders, node);
					if (children == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Children but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return children.Value;
				}

				// Token: 0x0601040C RID: 66572 RVA: 0x00383C20 File Offset: 0x00381E20
				public relChildList_singleRelChildList relChildList_singleRelChildList(ProgramNode node)
				{
					relChildList_singleRelChildList? relChildList_singleRelChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.relChildList_singleRelChildList.CreateSafe(this._builders, node);
					if (relChildList_singleRelChildList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol relChildList_singleRelChildList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return relChildList_singleRelChildList.Value;
				}

				// Token: 0x0601040D RID: 66573 RVA: 0x00383C74 File Offset: 0x00381E74
				public ConcatChild ConcatChild(ProgramNode node)
				{
					ConcatChild? concatChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConcatChild.CreateSafe(this._builders, node);
					if (concatChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol ConcatChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return concatChild.Value;
				}

				// Token: 0x0601040E RID: 66574 RVA: 0x00383CC8 File Offset: 0x00381EC8
				public SinglePosList SinglePosList(ProgramNode node)
				{
					SinglePosList? singlePosList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SinglePosList.CreateSafe(this._builders, node);
					if (singlePosList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SinglePosList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singlePosList.Value;
				}

				// Token: 0x0601040F RID: 66575 RVA: 0x00383D1C File Offset: 0x00381F1C
				public RelChild RelChild(ProgramNode node)
				{
					RelChild? relChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.RelChild.CreateSafe(this._builders, node);
					if (relChild == null)
					{
						string text = "node";
						string text2 = "expected node for symbol RelChild but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return relChild.Value;
				}

				// Token: 0x06010410 RID: 66576 RVA: 0x00383D70 File Offset: 0x00381F70
				public AbsPos AbsPos(ProgramNode node)
				{
					AbsPos? absPos = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.AbsPos.CreateSafe(this._builders, node);
					if (absPos == null)
					{
						string text = "node";
						string text2 = "expected node for symbol AbsPos but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return absPos.Value;
				}

				// Token: 0x06010411 RID: 66577 RVA: 0x00383DC4 File Offset: 0x00381FC4
				public children_interval children_interval(ProgramNode node)
				{
					children_interval? children_interval = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.children_interval.CreateSafe(this._builders, node);
					if (children_interval == null)
					{
						string text = "node";
						string text2 = "expected node for symbol children_interval but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return children_interval.Value;
				}

				// Token: 0x06010412 RID: 66578 RVA: 0x00383E18 File Offset: 0x00382018
				public Prepend Prepend(ProgramNode node)
				{
					Prepend? prepend = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
					if (prepend == null)
					{
						string text = "node";
						string text2 = "expected node for symbol Prepend but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return prepend.Value;
				}

				// Token: 0x06010413 RID: 66579 RVA: 0x00383E6C File Offset: 0x0038206C
				public SingleList SingleList(ProgramNode node)
				{
					SingleList? singleList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SingleList.CreateSafe(this._builders, node);
					if (singleList == null)
					{
						string text = "node";
						string text2 = "expected node for symbol SingleList but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return singleList.Value;
				}

				// Token: 0x06010414 RID: 66580 RVA: 0x00383EC0 File Offset: 0x003820C0
				public InOrderAllNodes InOrderAllNodes(ProgramNode node)
				{
					InOrderAllNodes? inOrderAllNodes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InOrderAllNodes.CreateSafe(this._builders, node);
					if (inOrderAllNodes == null)
					{
						string text = "node";
						string text2 = "expected node for symbol InOrderAllNodes but received ";
						Symbol symbol = node.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return inOrderAllNodes.Value;
				}

				// Token: 0x04006246 RID: 25158
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E48 RID: 7752
			public class NodeIs
			{
				// Token: 0x06010415 RID: 66581 RVA: 0x00383F11 File Offset: 0x00382111
				public NodeIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010416 RID: 66582 RVA: 0x00383F20 File Offset: 0x00382120
				public bool guardedRule(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010417 RID: 66583 RVA: 0x00383F44 File Offset: 0x00382144
				public bool guardedRule(ProgramNode node, out guardedRule value)
				{
					guardedRule? guardedRule = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule.CreateSafe(this._builders, node);
					if (guardedRule == null)
					{
						value = default(guardedRule);
						return false;
					}
					value = guardedRule.Value;
					return true;
				}

				// Token: 0x06010418 RID: 66584 RVA: 0x00383F80 File Offset: 0x00382180
				public bool match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010419 RID: 66585 RVA: 0x00383FA4 File Offset: 0x003821A4
				public bool match(ProgramNode node, out match value)
				{
					match? match = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match.CreateSafe(this._builders, node);
					if (match == null)
					{
						value = default(match);
						return false;
					}
					value = match.Value;
					return true;
				}

				// Token: 0x0601041A RID: 66586 RVA: 0x00383FE0 File Offset: 0x003821E0
				public bool pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601041B RID: 66587 RVA: 0x00384004 File Offset: 0x00382204
				public bool pred(ProgramNode node, out pred value)
				{
					pred? pred = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred.CreateSafe(this._builders, node);
					if (pred == null)
					{
						value = default(pred);
						return false;
					}
					value = pred.Value;
					return true;
				}

				// Token: 0x0601041C RID: 66588 RVA: 0x00384040 File Offset: 0x00382240
				public bool newDsl(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601041D RID: 66589 RVA: 0x00384064 File Offset: 0x00382264
				public bool newDsl(ProgramNode node, out newDsl value)
				{
					newDsl? newDsl = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl.CreateSafe(this._builders, node);
					if (newDsl == null)
					{
						value = default(newDsl);
						return false;
					}
					value = newDsl.Value;
					return true;
				}

				// Token: 0x0601041E RID: 66590 RVA: 0x003840A0 File Offset: 0x003822A0
				public bool construction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601041F RID: 66591 RVA: 0x003840C4 File Offset: 0x003822C4
				public bool construction(ProgramNode node, out construction value)
				{
					construction? construction = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction.CreateSafe(this._builders, node);
					if (construction == null)
					{
						value = default(construction);
						return false;
					}
					value = construction.Value;
					return true;
				}

				// Token: 0x06010420 RID: 66592 RVA: 0x00384100 File Offset: 0x00382300
				public bool select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010421 RID: 66593 RVA: 0x00384124 File Offset: 0x00382324
				public bool select(ProgramNode node, out select value)
				{
					select? select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select.CreateSafe(this._builders, node);
					if (select == null)
					{
						value = default(select);
						return false;
					}
					value = select.Value;
					return true;
				}

				// Token: 0x06010422 RID: 66594 RVA: 0x00384160 File Offset: 0x00382360
				public bool tmpFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010423 RID: 66595 RVA: 0x00384184 File Offset: 0x00382384
				public bool tmpFilter(ProgramNode node, out tmpFilter value)
				{
					tmpFilter? tmpFilter = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter.CreateSafe(this._builders, node);
					if (tmpFilter == null)
					{
						value = default(tmpFilter);
						return false;
					}
					value = tmpFilter.Value;
					return true;
				}

				// Token: 0x06010424 RID: 66596 RVA: 0x003841C0 File Offset: 0x003823C0
				public bool x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010425 RID: 66597 RVA: 0x003841E4 File Offset: 0x003823E4
				public bool x(ProgramNode node, out x value)
				{
					x? x = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x.CreateSafe(this._builders, node);
					if (x == null)
					{
						value = default(x);
						return false;
					}
					value = x.Value;
					return true;
				}

				// Token: 0x06010426 RID: 66598 RVA: 0x00384220 File Offset: 0x00382420
				public bool sequenceChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010427 RID: 66599 RVA: 0x00384244 File Offset: 0x00382444
				public bool sequenceChildren(ProgramNode node, out sequenceChildren value)
				{
					sequenceChildren? sequenceChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren.CreateSafe(this._builders, node);
					if (sequenceChildren == null)
					{
						value = default(sequenceChildren);
						return false;
					}
					value = sequenceChildren.Value;
					return true;
				}

				// Token: 0x06010428 RID: 66600 RVA: 0x00384280 File Offset: 0x00382480
				public bool convertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010429 RID: 66601 RVA: 0x003842A4 File Offset: 0x003824A4
				public bool convertSequence(ProgramNode node, out convertSequence value)
				{
					convertSequence? convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence.CreateSafe(this._builders, node);
					if (convertSequence == null)
					{
						value = default(convertSequence);
						return false;
					}
					value = convertSequence.Value;
					return true;
				}

				// Token: 0x0601042A RID: 66602 RVA: 0x003842E0 File Offset: 0x003824E0
				public bool parent(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601042B RID: 66603 RVA: 0x00384304 File Offset: 0x00382504
				public bool parent(ProgramNode node, out parent value)
				{
					parent? parent = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent.CreateSafe(this._builders, node);
					if (parent == null)
					{
						value = default(parent);
						return false;
					}
					value = parent.Value;
					return true;
				}

				// Token: 0x0601042C RID: 66604 RVA: 0x00384340 File Offset: 0x00382540
				public bool sequenceMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601042D RID: 66605 RVA: 0x00384364 File Offset: 0x00382564
				public bool sequenceMap(ProgramNode node, out sequenceMap value)
				{
					sequenceMap? sequenceMap = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap.CreateSafe(this._builders, node);
					if (sequenceMap == null)
					{
						value = default(sequenceMap);
						return false;
					}
					value = sequenceMap.Value;
					return true;
				}

				// Token: 0x0601042E RID: 66606 RVA: 0x003843A0 File Offset: 0x003825A0
				public bool selectedNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601042F RID: 66607 RVA: 0x003843C4 File Offset: 0x003825C4
				public bool selectedNode(ProgramNode node, out selectedNode value)
				{
					selectedNode? selectedNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode.CreateSafe(this._builders, node);
					if (selectedNode == null)
					{
						value = default(selectedNode);
						return false;
					}
					value = selectedNode.Value;
					return true;
				}

				// Token: 0x06010430 RID: 66608 RVA: 0x00384400 File Offset: 0x00382600
				public bool parentChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010431 RID: 66609 RVA: 0x00384424 File Offset: 0x00382624
				public bool parentChildren(ProgramNode node, out parentChildren value)
				{
					parentChildren? parentChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren.CreateSafe(this._builders, node);
					if (parentChildren == null)
					{
						value = default(parentChildren);
						return false;
					}
					value = parentChildren.Value;
					return true;
				}

				// Token: 0x06010432 RID: 66610 RVA: 0x00384460 File Offset: 0x00382660
				public bool relChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010433 RID: 66611 RVA: 0x00384484 File Offset: 0x00382684
				public bool relChildList(ProgramNode node, out relChildList value)
				{
					relChildList? relChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList.CreateSafe(this._builders, node);
					if (relChildList == null)
					{
						value = default(relChildList);
						return false;
					}
					value = relChildList.Value;
					return true;
				}

				// Token: 0x06010434 RID: 66612 RVA: 0x003844C0 File Offset: 0x003826C0
				public bool singleRelChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010435 RID: 66613 RVA: 0x003844E4 File Offset: 0x003826E4
				public bool singleRelChildList(ProgramNode node, out singleRelChildList value)
				{
					singleRelChildList? singleRelChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList.CreateSafe(this._builders, node);
					if (singleRelChildList == null)
					{
						value = default(singleRelChildList);
						return false;
					}
					value = singleRelChildList.Value;
					return true;
				}

				// Token: 0x06010436 RID: 66614 RVA: 0x00384520 File Offset: 0x00382720
				public bool relChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010437 RID: 66615 RVA: 0x00384544 File Offset: 0x00382744
				public bool relChild(ProgramNode node, out relChild value)
				{
					relChild? relChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild.CreateSafe(this._builders, node);
					if (relChild == null)
					{
						value = default(relChild);
						return false;
					}
					value = relChild.Value;
					return true;
				}

				// Token: 0x06010438 RID: 66616 RVA: 0x00384580 File Offset: 0x00382780
				public bool pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010439 RID: 66617 RVA: 0x003845A4 File Offset: 0x003827A4
				public bool pos(ProgramNode node, out pos value)
				{
					pos? pos = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos.CreateSafe(this._builders, node);
					if (pos == null)
					{
						value = default(pos);
						return false;
					}
					value = pos.Value;
					return true;
				}

				// Token: 0x0601043A RID: 66618 RVA: 0x003845E0 File Offset: 0x003827E0
				public bool children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601043B RID: 66619 RVA: 0x00384604 File Offset: 0x00382804
				public bool children(ProgramNode node, out children value)
				{
					children? children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children.CreateSafe(this._builders, node);
					if (children == null)
					{
						value = default(children);
						return false;
					}
					value = children.Value;
					return true;
				}

				// Token: 0x0601043C RID: 66620 RVA: 0x00384640 File Offset: 0x00382840
				public bool interval(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601043D RID: 66621 RVA: 0x00384664 File Offset: 0x00382864
				public bool interval(ProgramNode node, out interval value)
				{
					interval? interval = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval.CreateSafe(this._builders, node);
					if (interval == null)
					{
						value = default(interval);
						return false;
					}
					value = interval.Value;
					return true;
				}

				// Token: 0x0601043E RID: 66622 RVA: 0x003846A0 File Offset: 0x003828A0
				public bool inorderAllNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601043F RID: 66623 RVA: 0x003846C4 File Offset: 0x003828C4
				public bool inorderAllNodes(ProgramNode node, out inorderAllNodes value)
				{
					inorderAllNodes? inorderAllNodes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes.CreateSafe(this._builders, node);
					if (inorderAllNodes == null)
					{
						value = default(inorderAllNodes);
						return false;
					}
					value = inorderAllNodes.Value;
					return true;
				}

				// Token: 0x06010440 RID: 66624 RVA: 0x00384700 File Offset: 0x00382900
				public bool label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010441 RID: 66625 RVA: 0x00384724 File Offset: 0x00382924
				public bool label(ProgramNode node, out label value)
				{
					label? label = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label.CreateSafe(this._builders, node);
					if (label == null)
					{
						value = default(label);
						return false;
					}
					value = label.Value;
					return true;
				}

				// Token: 0x06010442 RID: 66626 RVA: 0x00384760 File Offset: 0x00382960
				public bool attributes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010443 RID: 66627 RVA: 0x00384784 File Offset: 0x00382984
				public bool attributes(ProgramNode node, out attributes value)
				{
					attributes? attributes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes.CreateSafe(this._builders, node);
					if (attributes == null)
					{
						value = default(attributes);
						return false;
					}
					value = attributes.Value;
					return true;
				}

				// Token: 0x06010444 RID: 66628 RVA: 0x003847C0 File Offset: 0x003829C0
				public bool kind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010445 RID: 66629 RVA: 0x003847E4 File Offset: 0x003829E4
				public bool kind(ProgramNode node, out kind value)
				{
					kind? kind = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind.CreateSafe(this._builders, node);
					if (kind == null)
					{
						value = default(kind);
						return false;
					}
					value = kind.Value;
					return true;
				}

				// Token: 0x06010446 RID: 66630 RVA: 0x00384820 File Offset: 0x00382A20
				public bool name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010447 RID: 66631 RVA: 0x00384844 File Offset: 0x00382A44
				public bool name(ProgramNode node, out name value)
				{
					name? name = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name.CreateSafe(this._builders, node);
					if (name == null)
					{
						value = default(name);
						return false;
					}
					value = name.Value;
					return true;
				}

				// Token: 0x06010448 RID: 66632 RVA: 0x00384880 File Offset: 0x00382A80
				public bool value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010449 RID: 66633 RVA: 0x003848A4 File Offset: 0x00382AA4
				public bool value(ProgramNode node, out value value)
				{
					value? value2 = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value.CreateSafe(this._builders, node);
					if (value2 == null)
					{
						value = default(value);
						return false;
					}
					value = value2.Value;
					return true;
				}

				// Token: 0x0601044A RID: 66634 RVA: 0x003848E0 File Offset: 0x00382AE0
				public bool k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601044B RID: 66635 RVA: 0x00384904 File Offset: 0x00382B04
				public bool k(ProgramNode node, out k value)
				{
					k? k = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k.CreateSafe(this._builders, node);
					if (k == null)
					{
						value = default(k);
						return false;
					}
					value = k.Value;
					return true;
				}

				// Token: 0x0601044C RID: 66636 RVA: 0x00384940 File Offset: 0x00382B40
				public bool p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601044D RID: 66637 RVA: 0x00384964 File Offset: 0x00382B64
				public bool p(ProgramNode node, out p value)
				{
					p? p = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p.CreateSafe(this._builders, node);
					if (p == null)
					{
						value = default(p);
						return false;
					}
					value = p.Value;
					return true;
				}

				// Token: 0x0601044E RID: 66638 RVA: 0x003849A0 File Offset: 0x00382BA0
				public bool path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601044F RID: 66639 RVA: 0x003849C4 File Offset: 0x00382BC4
				public bool path(ProgramNode node, out path value)
				{
					path? path = Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path.CreateSafe(this._builders, node);
					if (path == null)
					{
						value = default(path);
						return false;
					}
					value = path.Value;
					return true;
				}

				// Token: 0x04006247 RID: 25159
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E49 RID: 7753
			public class RuleIs
			{
				// Token: 0x06010450 RID: 66640 RVA: 0x003849FE File Offset: 0x00382BFE
				public RuleIs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010451 RID: 66641 RVA: 0x00384A10 File Offset: 0x00382C10
				public bool GuardedRule(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.GuardedRule.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010452 RID: 66642 RVA: 0x00384A34 File Offset: 0x00382C34
				public bool GuardedRule(ProgramNode node, out GuardedRule value)
				{
					GuardedRule? guardedRule = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.GuardedRule.CreateSafe(this._builders, node);
					if (guardedRule == null)
					{
						value = default(GuardedRule);
						return false;
					}
					value = guardedRule.Value;
					return true;
				}

				// Token: 0x06010453 RID: 66643 RVA: 0x00384A70 File Offset: 0x00382C70
				public bool match_pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.match_pred.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010454 RID: 66644 RVA: 0x00384A94 File Offset: 0x00382C94
				public bool match_pred(ProgramNode node, out match_pred value)
				{
					match_pred? match_pred = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.match_pred.CreateSafe(this._builders, node);
					if (match_pred == null)
					{
						value = default(match_pred);
						return false;
					}
					value = match_pred.Value;
					return true;
				}

				// Token: 0x06010455 RID: 66645 RVA: 0x00384AD0 File Offset: 0x00382CD0
				public bool Conj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Conj.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010456 RID: 66646 RVA: 0x00384AF4 File Offset: 0x00382CF4
				public bool Conj(ProgramNode node, out Conj value)
				{
					Conj? conj = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Conj.CreateSafe(this._builders, node);
					if (conj == null)
					{
						value = default(Conj);
						return false;
					}
					value = conj.Value;
					return true;
				}

				// Token: 0x06010457 RID: 66647 RVA: 0x00384B30 File Offset: 0x00382D30
				public bool IsKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsKind.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010458 RID: 66648 RVA: 0x00384B54 File Offset: 0x00382D54
				public bool IsKind(ProgramNode node, out IsKind value)
				{
					IsKind? isKind = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsKind.CreateSafe(this._builders, node);
					if (isKind == null)
					{
						value = default(IsKind);
						return false;
					}
					value = isKind.Value;
					return true;
				}

				// Token: 0x06010459 RID: 66649 RVA: 0x00384B90 File Offset: 0x00382D90
				public bool IsAttributePresent(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsAttributePresent.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601045A RID: 66650 RVA: 0x00384BB4 File Offset: 0x00382DB4
				public bool IsAttributePresent(ProgramNode node, out IsAttributePresent value)
				{
					IsAttributePresent? isAttributePresent = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsAttributePresent.CreateSafe(this._builders, node);
					if (isAttributePresent == null)
					{
						value = default(IsAttributePresent);
						return false;
					}
					value = isAttributePresent.Value;
					return true;
				}

				// Token: 0x0601045B RID: 66651 RVA: 0x00384BF0 File Offset: 0x00382DF0
				public bool IsNthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsNthChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601045C RID: 66652 RVA: 0x00384C14 File Offset: 0x00382E14
				public bool IsNthChild(ProgramNode node, out IsNthChild value)
				{
					IsNthChild? isNthChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsNthChild.CreateSafe(this._builders, node);
					if (isNthChild == null)
					{
						value = default(IsNthChild);
						return false;
					}
					value = isNthChild.Value;
					return true;
				}

				// Token: 0x0601045D RID: 66653 RVA: 0x00384C50 File Offset: 0x00382E50
				public bool HasNChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.HasNChildren.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601045E RID: 66654 RVA: 0x00384C74 File Offset: 0x00382E74
				public bool HasNChildren(ProgramNode node, out HasNChildren value)
				{
					HasNChildren? hasNChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.HasNChildren.CreateSafe(this._builders, node);
					if (hasNChildren == null)
					{
						value = default(HasNChildren);
						return false;
					}
					value = hasNChildren.Value;
					return true;
				}

				// Token: 0x0601045F RID: 66655 RVA: 0x00384CB0 File Offset: 0x00382EB0
				public bool Not(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010460 RID: 66656 RVA: 0x00384CD4 File Offset: 0x00382ED4
				public bool Not(ProgramNode node, out Not value)
				{
					Not? not = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
					if (not == null)
					{
						value = default(Not);
						return false;
					}
					value = not.Value;
					return true;
				}

				// Token: 0x06010461 RID: 66657 RVA: 0x00384D10 File Offset: 0x00382F10
				public bool newDsl_select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_select.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010462 RID: 66658 RVA: 0x00384D34 File Offset: 0x00382F34
				public bool newDsl_select(ProgramNode node, out newDsl_select value)
				{
					newDsl_select? newDsl_select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_select.CreateSafe(this._builders, node);
					if (newDsl_select == null)
					{
						value = default(newDsl_select);
						return false;
					}
					value = newDsl_select.Value;
					return true;
				}

				// Token: 0x06010463 RID: 66659 RVA: 0x00384D70 File Offset: 0x00382F70
				public bool newDsl_construction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_construction.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010464 RID: 66660 RVA: 0x00384D94 File Offset: 0x00382F94
				public bool newDsl_construction(ProgramNode node, out newDsl_construction value)
				{
					newDsl_construction? newDsl_construction = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_construction.CreateSafe(this._builders, node);
					if (newDsl_construction == null)
					{
						value = default(newDsl_construction);
						return false;
					}
					value = newDsl_construction.Value;
					return true;
				}

				// Token: 0x06010465 RID: 66661 RVA: 0x00384DD0 File Offset: 0x00382FD0
				public bool LeafConstLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstLabelNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010466 RID: 66662 RVA: 0x00384DF4 File Offset: 0x00382FF4
				public bool LeafConstLabelNode(ProgramNode node, out LeafConstLabelNode value)
				{
					LeafConstLabelNode? leafConstLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstLabelNode.CreateSafe(this._builders, node);
					if (leafConstLabelNode == null)
					{
						value = default(LeafConstLabelNode);
						return false;
					}
					value = leafConstLabelNode.Value;
					return true;
				}

				// Token: 0x06010467 RID: 66663 RVA: 0x00384E30 File Offset: 0x00383030
				public bool ConstLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstLabelNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010468 RID: 66664 RVA: 0x00384E54 File Offset: 0x00383054
				public bool ConstLabelNode(ProgramNode node, out ConstLabelNode value)
				{
					ConstLabelNode? constLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstLabelNode.CreateSafe(this._builders, node);
					if (constLabelNode == null)
					{
						value = default(ConstLabelNode);
						return false;
					}
					value = constLabelNode.Value;
					return true;
				}

				// Token: 0x06010469 RID: 66665 RVA: 0x00384E90 File Offset: 0x00383090
				public bool ConstSequenceLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstSequenceLabelNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601046A RID: 66666 RVA: 0x00384EB4 File Offset: 0x003830B4
				public bool ConstSequenceLabelNode(ProgramNode node, out ConstSequenceLabelNode value)
				{
					ConstSequenceLabelNode? constSequenceLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstSequenceLabelNode.CreateSafe(this._builders, node);
					if (constSequenceLabelNode == null)
					{
						value = default(ConstSequenceLabelNode);
						return false;
					}
					value = constSequenceLabelNode.Value;
					return true;
				}

				// Token: 0x0601046B RID: 66667 RVA: 0x00384EF0 File Offset: 0x003830F0
				public bool LeafConstSequenceLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstSequenceLabelNode.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601046C RID: 66668 RVA: 0x00384F14 File Offset: 0x00383114
				public bool LeafConstSequenceLabelNode(ProgramNode node, out LeafConstSequenceLabelNode value)
				{
					LeafConstSequenceLabelNode? leafConstSequenceLabelNode = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstSequenceLabelNode.CreateSafe(this._builders, node);
					if (leafConstSequenceLabelNode == null)
					{
						value = default(LeafConstSequenceLabelNode);
						return false;
					}
					value = leafConstSequenceLabelNode.Value;
					return true;
				}

				// Token: 0x0601046D RID: 66669 RVA: 0x00384F50 File Offset: 0x00383150
				public bool Select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601046E RID: 66670 RVA: 0x00384F74 File Offset: 0x00383174
				public bool Select(ProgramNode node, out Select value)
				{
					Select? select = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
					if (select == null)
					{
						value = default(Select);
						return false;
					}
					value = select.Value;
					return true;
				}

				// Token: 0x0601046F RID: 66671 RVA: 0x00384FB0 File Offset: 0x003831B0
				public bool TmpFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.TmpFilter.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010470 RID: 66672 RVA: 0x00384FD4 File Offset: 0x003831D4
				public bool TmpFilter(ProgramNode node, out TmpFilter value)
				{
					TmpFilter? tmpFilter = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.TmpFilter.CreateSafe(this._builders, node);
					if (tmpFilter == null)
					{
						value = default(TmpFilter);
						return false;
					}
					value = tmpFilter.Value;
					return true;
				}

				// Token: 0x06010471 RID: 66673 RVA: 0x00385010 File Offset: 0x00383210
				public bool sequenceChildren_children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_children.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010472 RID: 66674 RVA: 0x00385034 File Offset: 0x00383234
				public bool sequenceChildren_children(ProgramNode node, out sequenceChildren_children value)
				{
					sequenceChildren_children? sequenceChildren_children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_children.CreateSafe(this._builders, node);
					if (sequenceChildren_children == null)
					{
						value = default(sequenceChildren_children);
						return false;
					}
					value = sequenceChildren_children.Value;
					return true;
				}

				// Token: 0x06010473 RID: 66675 RVA: 0x00385070 File Offset: 0x00383270
				public bool InsertAtAbs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtAbs.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010474 RID: 66676 RVA: 0x00385094 File Offset: 0x00383294
				public bool InsertAtAbs(ProgramNode node, out InsertAtAbs value)
				{
					InsertAtAbs? insertAtAbs = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtAbs.CreateSafe(this._builders, node);
					if (insertAtAbs == null)
					{
						value = default(InsertAtAbs);
						return false;
					}
					value = insertAtAbs.Value;
					return true;
				}

				// Token: 0x06010475 RID: 66677 RVA: 0x003850D0 File Offset: 0x003832D0
				public bool InsertAtRel(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtRel.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010476 RID: 66678 RVA: 0x003850F4 File Offset: 0x003832F4
				public bool InsertAtRel(ProgramNode node, out InsertAtRel value)
				{
					InsertAtRel? insertAtRel = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtRel.CreateSafe(this._builders, node);
					if (insertAtRel == null)
					{
						value = default(InsertAtRel);
						return false;
					}
					value = insertAtRel.Value;
					return true;
				}

				// Token: 0x06010477 RID: 66679 RVA: 0x00385130 File Offset: 0x00383330
				public bool DeleteChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.DeleteChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010478 RID: 66680 RVA: 0x00385154 File Offset: 0x00383354
				public bool DeleteChild(ProgramNode node, out DeleteChild value)
				{
					DeleteChild? deleteChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.DeleteChild.CreateSafe(this._builders, node);
					if (deleteChild == null)
					{
						value = default(DeleteChild);
						return false;
					}
					value = deleteChild.Value;
					return true;
				}

				// Token: 0x06010479 RID: 66681 RVA: 0x00385190 File Offset: 0x00383390
				public bool ReplaceChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ReplaceChildren.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601047A RID: 66682 RVA: 0x003851B4 File Offset: 0x003833B4
				public bool ReplaceChildren(ProgramNode node, out ReplaceChildren value)
				{
					ReplaceChildren? replaceChildren = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ReplaceChildren.CreateSafe(this._builders, node);
					if (replaceChildren == null)
					{
						value = default(ReplaceChildren);
						return false;
					}
					value = replaceChildren.Value;
					return true;
				}

				// Token: 0x0601047B RID: 66683 RVA: 0x003851F0 File Offset: 0x003833F0
				public bool sequenceChildren_convertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_convertSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601047C RID: 66684 RVA: 0x00385214 File Offset: 0x00383414
				public bool sequenceChildren_convertSequence(ProgramNode node, out sequenceChildren_convertSequence value)
				{
					sequenceChildren_convertSequence? sequenceChildren_convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_convertSequence.CreateSafe(this._builders, node);
					if (sequenceChildren_convertSequence == null)
					{
						value = default(sequenceChildren_convertSequence);
						return false;
					}
					value = sequenceChildren_convertSequence.Value;
					return true;
				}

				// Token: 0x0601047D RID: 66685 RVA: 0x00385250 File Offset: 0x00383450
				public bool ConvertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConvertSequence.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601047E RID: 66686 RVA: 0x00385274 File Offset: 0x00383474
				public bool ConvertSequence(ProgramNode node, out ConvertSequence value)
				{
					ConvertSequence? convertSequence = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConvertSequence.CreateSafe(this._builders, node);
					if (convertSequence == null)
					{
						value = default(ConvertSequence);
						return false;
					}
					value = convertSequence.Value;
					return true;
				}

				// Token: 0x0601047F RID: 66687 RVA: 0x003852B0 File Offset: 0x003834B0
				public bool SequenceMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SequenceMap.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010480 RID: 66688 RVA: 0x003852D4 File Offset: 0x003834D4
				public bool SequenceMap(ProgramNode node, out SequenceMap value)
				{
					SequenceMap? sequenceMap = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SequenceMap.CreateSafe(this._builders, node);
					if (sequenceMap == null)
					{
						value = default(SequenceMap);
						return false;
					}
					value = sequenceMap.Value;
					return true;
				}

				// Token: 0x06010481 RID: 66689 RVA: 0x00385310 File Offset: 0x00383510
				public bool Children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Children.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010482 RID: 66690 RVA: 0x00385334 File Offset: 0x00383534
				public bool Children(ProgramNode node, out Children value)
				{
					Children? children = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Children.CreateSafe(this._builders, node);
					if (children == null)
					{
						value = default(Children);
						return false;
					}
					value = children.Value;
					return true;
				}

				// Token: 0x06010483 RID: 66691 RVA: 0x00385370 File Offset: 0x00383570
				public bool relChildList_singleRelChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.relChildList_singleRelChildList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010484 RID: 66692 RVA: 0x00385394 File Offset: 0x00383594
				public bool relChildList_singleRelChildList(ProgramNode node, out relChildList_singleRelChildList value)
				{
					relChildList_singleRelChildList? relChildList_singleRelChildList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.relChildList_singleRelChildList.CreateSafe(this._builders, node);
					if (relChildList_singleRelChildList == null)
					{
						value = default(relChildList_singleRelChildList);
						return false;
					}
					value = relChildList_singleRelChildList.Value;
					return true;
				}

				// Token: 0x06010485 RID: 66693 RVA: 0x003853D0 File Offset: 0x003835D0
				public bool ConcatChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConcatChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010486 RID: 66694 RVA: 0x003853F4 File Offset: 0x003835F4
				public bool ConcatChild(ProgramNode node, out ConcatChild value)
				{
					ConcatChild? concatChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConcatChild.CreateSafe(this._builders, node);
					if (concatChild == null)
					{
						value = default(ConcatChild);
						return false;
					}
					value = concatChild.Value;
					return true;
				}

				// Token: 0x06010487 RID: 66695 RVA: 0x00385430 File Offset: 0x00383630
				public bool SinglePosList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SinglePosList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010488 RID: 66696 RVA: 0x00385454 File Offset: 0x00383654
				public bool SinglePosList(ProgramNode node, out SinglePosList value)
				{
					SinglePosList? singlePosList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SinglePosList.CreateSafe(this._builders, node);
					if (singlePosList == null)
					{
						value = default(SinglePosList);
						return false;
					}
					value = singlePosList.Value;
					return true;
				}

				// Token: 0x06010489 RID: 66697 RVA: 0x00385490 File Offset: 0x00383690
				public bool RelChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.RelChild.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601048A RID: 66698 RVA: 0x003854B4 File Offset: 0x003836B4
				public bool RelChild(ProgramNode node, out RelChild value)
				{
					RelChild? relChild = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.RelChild.CreateSafe(this._builders, node);
					if (relChild == null)
					{
						value = default(RelChild);
						return false;
					}
					value = relChild.Value;
					return true;
				}

				// Token: 0x0601048B RID: 66699 RVA: 0x003854F0 File Offset: 0x003836F0
				public bool AbsPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.AbsPos.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601048C RID: 66700 RVA: 0x00385514 File Offset: 0x00383714
				public bool AbsPos(ProgramNode node, out AbsPos value)
				{
					AbsPos? absPos = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.AbsPos.CreateSafe(this._builders, node);
					if (absPos == null)
					{
						value = default(AbsPos);
						return false;
					}
					value = absPos.Value;
					return true;
				}

				// Token: 0x0601048D RID: 66701 RVA: 0x00385550 File Offset: 0x00383750
				public bool children_interval(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.children_interval.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x0601048E RID: 66702 RVA: 0x00385574 File Offset: 0x00383774
				public bool children_interval(ProgramNode node, out children_interval value)
				{
					children_interval? children_interval = Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.children_interval.CreateSafe(this._builders, node);
					if (children_interval == null)
					{
						value = default(children_interval);
						return false;
					}
					value = children_interval.Value;
					return true;
				}

				// Token: 0x0601048F RID: 66703 RVA: 0x003855B0 File Offset: 0x003837B0
				public bool Prepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010490 RID: 66704 RVA: 0x003855D4 File Offset: 0x003837D4
				public bool Prepend(ProgramNode node, out Prepend value)
				{
					Prepend? prepend = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
					if (prepend == null)
					{
						value = default(Prepend);
						return false;
					}
					value = prepend.Value;
					return true;
				}

				// Token: 0x06010491 RID: 66705 RVA: 0x00385610 File Offset: 0x00383810
				public bool SingleList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SingleList.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010492 RID: 66706 RVA: 0x00385634 File Offset: 0x00383834
				public bool SingleList(ProgramNode node, out SingleList value)
				{
					SingleList? singleList = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SingleList.CreateSafe(this._builders, node);
					if (singleList == null)
					{
						value = default(SingleList);
						return false;
					}
					value = singleList.Value;
					return true;
				}

				// Token: 0x06010493 RID: 66707 RVA: 0x00385670 File Offset: 0x00383870
				public bool InOrderAllNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InOrderAllNodes.CreateSafe(this._builders, node) != null;
				}

				// Token: 0x06010494 RID: 66708 RVA: 0x00385694 File Offset: 0x00383894
				public bool InOrderAllNodes(ProgramNode node, out InOrderAllNodes value)
				{
					InOrderAllNodes? inOrderAllNodes = Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InOrderAllNodes.CreateSafe(this._builders, node);
					if (inOrderAllNodes == null)
					{
						value = default(InOrderAllNodes);
						return false;
					}
					value = inOrderAllNodes.Value;
					return true;
				}

				// Token: 0x04006248 RID: 25160
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E4A RID: 7754
			public class NodeAs
			{
				// Token: 0x06010495 RID: 66709 RVA: 0x003856CE File Offset: 0x003838CE
				public NodeAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010496 RID: 66710 RVA: 0x003856DD File Offset: 0x003838DD
				public guardedRule? guardedRule(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule.CreateSafe(this._builders, node);
				}

				// Token: 0x06010497 RID: 66711 RVA: 0x003856EB File Offset: 0x003838EB
				public match? match(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match.CreateSafe(this._builders, node);
				}

				// Token: 0x06010498 RID: 66712 RVA: 0x003856F9 File Offset: 0x003838F9
				public pred? pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred.CreateSafe(this._builders, node);
				}

				// Token: 0x06010499 RID: 66713 RVA: 0x00385707 File Offset: 0x00383907
				public newDsl? newDsl(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049A RID: 66714 RVA: 0x00385715 File Offset: 0x00383915
				public construction? construction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049B RID: 66715 RVA: 0x00385723 File Offset: 0x00383923
				public select? select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049C RID: 66716 RVA: 0x00385731 File Offset: 0x00383931
				public tmpFilter? tmpFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049D RID: 66717 RVA: 0x0038573F File Offset: 0x0038393F
				public x? x(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049E RID: 66718 RVA: 0x0038574D File Offset: 0x0038394D
				public sequenceChildren? sequenceChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren.CreateSafe(this._builders, node);
				}

				// Token: 0x0601049F RID: 66719 RVA: 0x0038575B File Offset: 0x0038395B
				public convertSequence? convertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A0 RID: 66720 RVA: 0x00385769 File Offset: 0x00383969
				public parent? parent(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A1 RID: 66721 RVA: 0x00385777 File Offset: 0x00383977
				public sequenceMap? sequenceMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A2 RID: 66722 RVA: 0x00385785 File Offset: 0x00383985
				public selectedNode? selectedNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A3 RID: 66723 RVA: 0x00385793 File Offset: 0x00383993
				public parentChildren? parentChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A4 RID: 66724 RVA: 0x003857A1 File Offset: 0x003839A1
				public relChildList? relChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A5 RID: 66725 RVA: 0x003857AF File Offset: 0x003839AF
				public singleRelChildList? singleRelChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A6 RID: 66726 RVA: 0x003857BD File Offset: 0x003839BD
				public relChild? relChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A7 RID: 66727 RVA: 0x003857CB File Offset: 0x003839CB
				public pos? pos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A8 RID: 66728 RVA: 0x003857D9 File Offset: 0x003839D9
				public children? children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children.CreateSafe(this._builders, node);
				}

				// Token: 0x060104A9 RID: 66729 RVA: 0x003857E7 File Offset: 0x003839E7
				public interval? interval(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AA RID: 66730 RVA: 0x003857F5 File Offset: 0x003839F5
				public inorderAllNodes? inorderAllNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AB RID: 66731 RVA: 0x00385803 File Offset: 0x00383A03
				public label? label(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AC RID: 66732 RVA: 0x00385811 File Offset: 0x00383A11
				public attributes? attributes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AD RID: 66733 RVA: 0x0038581F File Offset: 0x00383A1F
				public kind? kind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AE RID: 66734 RVA: 0x0038582D File Offset: 0x00383A2D
				public name? name(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name.CreateSafe(this._builders, node);
				}

				// Token: 0x060104AF RID: 66735 RVA: 0x0038583B File Offset: 0x00383A3B
				public value? value(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B0 RID: 66736 RVA: 0x00385849 File Offset: 0x00383A49
				public k? k(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B1 RID: 66737 RVA: 0x00385857 File Offset: 0x00383A57
				public p? p(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B2 RID: 66738 RVA: 0x00385865 File Offset: 0x00383A65
				public path? path(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path.CreateSafe(this._builders, node);
				}

				// Token: 0x04006249 RID: 25161
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E4B RID: 7755
			public class RuleAs
			{
				// Token: 0x060104B3 RID: 66739 RVA: 0x00385873 File Offset: 0x00383A73
				public RuleAs(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060104B4 RID: 66740 RVA: 0x00385882 File Offset: 0x00383A82
				public GuardedRule? GuardedRule(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.GuardedRule.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B5 RID: 66741 RVA: 0x00385890 File Offset: 0x00383A90
				public match_pred? match_pred(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.match_pred.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B6 RID: 66742 RVA: 0x0038589E File Offset: 0x00383A9E
				public Conj? Conj(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Conj.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B7 RID: 66743 RVA: 0x003858AC File Offset: 0x00383AAC
				public IsKind? IsKind(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsKind.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B8 RID: 66744 RVA: 0x003858BA File Offset: 0x00383ABA
				public IsAttributePresent? IsAttributePresent(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsAttributePresent.CreateSafe(this._builders, node);
				}

				// Token: 0x060104B9 RID: 66745 RVA: 0x003858C8 File Offset: 0x00383AC8
				public IsNthChild? IsNthChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.IsNthChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BA RID: 66746 RVA: 0x003858D6 File Offset: 0x00383AD6
				public HasNChildren? HasNChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.HasNChildren.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BB RID: 66747 RVA: 0x003858E4 File Offset: 0x00383AE4
				public Not? Not(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Not.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BC RID: 66748 RVA: 0x003858F2 File Offset: 0x00383AF2
				public newDsl_select? newDsl_select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_select.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BD RID: 66749 RVA: 0x00385900 File Offset: 0x00383B00
				public newDsl_construction? newDsl_construction(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.newDsl_construction.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BE RID: 66750 RVA: 0x0038590E File Offset: 0x00383B0E
				public LeafConstLabelNode? LeafConstLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstLabelNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060104BF RID: 66751 RVA: 0x0038591C File Offset: 0x00383B1C
				public ConstLabelNode? ConstLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstLabelNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C0 RID: 66752 RVA: 0x0038592A File Offset: 0x00383B2A
				public ConstSequenceLabelNode? ConstSequenceLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConstSequenceLabelNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C1 RID: 66753 RVA: 0x00385938 File Offset: 0x00383B38
				public LeafConstSequenceLabelNode? LeafConstSequenceLabelNode(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.LeafConstSequenceLabelNode.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C2 RID: 66754 RVA: 0x00385946 File Offset: 0x00383B46
				public Select? Select(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Select.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C3 RID: 66755 RVA: 0x00385954 File Offset: 0x00383B54
				public TmpFilter? TmpFilter(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.TmpFilter.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C4 RID: 66756 RVA: 0x00385962 File Offset: 0x00383B62
				public sequenceChildren_children? sequenceChildren_children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_children.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C5 RID: 66757 RVA: 0x00385970 File Offset: 0x00383B70
				public InsertAtAbs? InsertAtAbs(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtAbs.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C6 RID: 66758 RVA: 0x0038597E File Offset: 0x00383B7E
				public InsertAtRel? InsertAtRel(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InsertAtRel.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C7 RID: 66759 RVA: 0x0038598C File Offset: 0x00383B8C
				public DeleteChild? DeleteChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.DeleteChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C8 RID: 66760 RVA: 0x0038599A File Offset: 0x00383B9A
				public ReplaceChildren? ReplaceChildren(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ReplaceChildren.CreateSafe(this._builders, node);
				}

				// Token: 0x060104C9 RID: 66761 RVA: 0x003859A8 File Offset: 0x00383BA8
				public sequenceChildren_convertSequence? sequenceChildren_convertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.sequenceChildren_convertSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CA RID: 66762 RVA: 0x003859B6 File Offset: 0x00383BB6
				public ConvertSequence? ConvertSequence(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConvertSequence.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CB RID: 66763 RVA: 0x003859C4 File Offset: 0x00383BC4
				public SequenceMap? SequenceMap(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SequenceMap.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CC RID: 66764 RVA: 0x003859D2 File Offset: 0x00383BD2
				public Children? Children(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Children.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CD RID: 66765 RVA: 0x003859E0 File Offset: 0x00383BE0
				public relChildList_singleRelChildList? relChildList_singleRelChildList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.relChildList_singleRelChildList.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CE RID: 66766 RVA: 0x003859EE File Offset: 0x00383BEE
				public ConcatChild? ConcatChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.ConcatChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060104CF RID: 66767 RVA: 0x003859FC File Offset: 0x00383BFC
				public SinglePosList? SinglePosList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SinglePosList.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D0 RID: 66768 RVA: 0x00385A0A File Offset: 0x00383C0A
				public RelChild? RelChild(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.RelChild.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D1 RID: 66769 RVA: 0x00385A18 File Offset: 0x00383C18
				public AbsPos? AbsPos(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.AbsPos.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D2 RID: 66770 RVA: 0x00385A26 File Offset: 0x00383C26
				public children_interval? children_interval(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes.children_interval.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D3 RID: 66771 RVA: 0x00385A34 File Offset: 0x00383C34
				public Prepend? Prepend(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.Prepend.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D4 RID: 66772 RVA: 0x00385A42 File Offset: 0x00383C42
				public SingleList? SingleList(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.SingleList.CreateSafe(this._builders, node);
				}

				// Token: 0x060104D5 RID: 66773 RVA: 0x00385A50 File Offset: 0x00383C50
				public InOrderAllNodes? InOrderAllNodes(ProgramNode node)
				{
					return Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes.InOrderAllNodes.CreateSafe(this._builders, node);
				}

				// Token: 0x0400624A RID: 25162
				private readonly GrammarBuilders _builders;
			}
		}

		// Token: 0x02001E4D RID: 7757
		public class Sets
		{
			// Token: 0x060104D9 RID: 66777 RVA: 0x00385A78 File Offset: 0x00383C78
			public Sets(GrammarBuilders builders)
			{
				this.Join = new GrammarBuilders.Sets.Joins(builders);
				this.ExplicitJoin = new GrammarBuilders.Sets.ExplicitJoins(builders);
				this.UnnamedConversion = new GrammarBuilders.Sets.JoinUnnamedConversions(builders);
				this.ExplicitUnnamedConversion = new GrammarBuilders.Sets.ExplicitJoinUnnamedConversions(builders);
				this.Cast = new GrammarBuilders.Sets.Casts(builders);
			}

			// Token: 0x17002B6B RID: 11115
			// (get) Token: 0x060104DA RID: 66778 RVA: 0x00385AC7 File Offset: 0x00383CC7
			// (set) Token: 0x060104DB RID: 66779 RVA: 0x00385ACF File Offset: 0x00383CCF
			public GrammarBuilders.Sets.Joins Join { get; private set; }

			// Token: 0x17002B6C RID: 11116
			// (get) Token: 0x060104DC RID: 66780 RVA: 0x00385AD8 File Offset: 0x00383CD8
			// (set) Token: 0x060104DD RID: 66781 RVA: 0x00385AE0 File Offset: 0x00383CE0
			public GrammarBuilders.Sets.ExplicitJoins ExplicitJoin { get; private set; }

			// Token: 0x17002B6D RID: 11117
			// (get) Token: 0x060104DE RID: 66782 RVA: 0x00385AE9 File Offset: 0x00383CE9
			// (set) Token: 0x060104DF RID: 66783 RVA: 0x00385AF1 File Offset: 0x00383CF1
			public GrammarBuilders.Sets.JoinUnnamedConversions UnnamedConversion { get; private set; }

			// Token: 0x17002B6E RID: 11118
			// (get) Token: 0x060104E0 RID: 66784 RVA: 0x00385AFA File Offset: 0x00383CFA
			// (set) Token: 0x060104E1 RID: 66785 RVA: 0x00385B02 File Offset: 0x00383D02
			public GrammarBuilders.Sets.ExplicitJoinUnnamedConversions ExplicitUnnamedConversion { get; private set; }

			// Token: 0x17002B6F RID: 11119
			// (get) Token: 0x060104E2 RID: 66786 RVA: 0x00385B0B File Offset: 0x00383D0B
			// (set) Token: 0x060104E3 RID: 66787 RVA: 0x00385B13 File Offset: 0x00383D13
			public GrammarBuilders.Sets.Casts Cast { get; private set; }

			// Token: 0x02001E4E RID: 7758
			public class Joins
			{
				// Token: 0x060104E4 RID: 66788 RVA: 0x00385B1C File Offset: 0x00383D1C
				public Joins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x060104E5 RID: 66789 RVA: 0x00385B2B File Offset: 0x00383D2B
				public ProgramSetBuilder<guardedRule> GuardedRule(ProgramSetBuilder<match> value0, ProgramSetBuilder<newDsl> value1)
				{
					return ProgramSetBuilder<guardedRule>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.GuardedRule, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104E6 RID: 66790 RVA: 0x00385B6B File Offset: 0x00383D6B
				public ProgramSetBuilder<match> Conj(ProgramSetBuilder<pred> value0, ProgramSetBuilder<match> value1)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Conj, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104E7 RID: 66791 RVA: 0x00385BAC File Offset: 0x00383DAC
				public ProgramSetBuilder<pred> IsKind(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<kind> value2)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsKind, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104E8 RID: 66792 RVA: 0x00385C08 File Offset: 0x00383E08
				public ProgramSetBuilder<pred> IsAttributePresent(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<name> value2, ProgramSetBuilder<value> value3)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsAttributePresent, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x060104E9 RID: 66793 RVA: 0x00385C73 File Offset: 0x00383E73
				public ProgramSetBuilder<pred> IsNthChild(ProgramSetBuilder<x> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.IsNthChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104EA RID: 66794 RVA: 0x00385CB4 File Offset: 0x00383EB4
				public ProgramSetBuilder<pred> HasNChildren(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<k> value2)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.HasNChildren, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104EB RID: 66795 RVA: 0x00385D0E File Offset: 0x00383F0E
				public ProgramSetBuilder<pred> Not(ProgramSetBuilder<pred> value0)
				{
					return ProgramSetBuilder<pred>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Not, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104EC RID: 66796 RVA: 0x00385D3F File Offset: 0x00383F3F
				public ProgramSetBuilder<construction> LeafConstLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1)
				{
					return ProgramSetBuilder<construction>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafConstLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104ED RID: 66797 RVA: 0x00385D80 File Offset: 0x00383F80
				public ProgramSetBuilder<construction> ConstLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<children> value2)
				{
					return ProgramSetBuilder<construction>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104EE RID: 66798 RVA: 0x00385DDC File Offset: 0x00383FDC
				public ProgramSetBuilder<construction> ConstSequenceLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<construction> value2, ProgramSetBuilder<sequenceChildren> value3)
				{
					return ProgramSetBuilder<construction>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConstSequenceLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x060104EF RID: 66799 RVA: 0x00385E48 File Offset: 0x00384048
				public ProgramSetBuilder<construction> LeafConstSequenceLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<construction> value2)
				{
					return ProgramSetBuilder<construction>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.LeafConstSequenceLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104F0 RID: 66800 RVA: 0x00385EA4 File Offset: 0x003840A4
				public ProgramSetBuilder<sequenceChildren> InsertAtAbs(ProgramSetBuilder<select> value0, ProgramSetBuilder<pos> value1, ProgramSetBuilder<newDsl> value2)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.InsertAtAbs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104F1 RID: 66801 RVA: 0x00385F00 File Offset: 0x00384100
				public ProgramSetBuilder<sequenceChildren> InsertAtRel(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChild> value1, ProgramSetBuilder<newDsl> value2)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.InsertAtRel, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104F2 RID: 66802 RVA: 0x00385F5A File Offset: 0x0038415A
				public ProgramSetBuilder<sequenceChildren> DeleteChild(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChild> value1)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.DeleteChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104F3 RID: 66803 RVA: 0x00385F9C File Offset: 0x0038419C
				public ProgramSetBuilder<sequenceChildren> ReplaceChildren(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChildList> value1, ProgramSetBuilder<children> value2)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ReplaceChildren, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x060104F4 RID: 66804 RVA: 0x00385FF6 File Offset: 0x003841F6
				public ProgramSetBuilder<parentChildren> Children(ProgramSetBuilder<parent> value0)
				{
					return ProgramSetBuilder<parentChildren>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Children, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104F5 RID: 66805 RVA: 0x00386027 File Offset: 0x00384227
				public ProgramSetBuilder<relChildList> ConcatChild(ProgramSetBuilder<singleRelChildList> value0, ProgramSetBuilder<relChildList> value1)
				{
					return ProgramSetBuilder<relChildList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConcatChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104F6 RID: 66806 RVA: 0x00386067 File Offset: 0x00384267
				public ProgramSetBuilder<singleRelChildList> SinglePosList(ProgramSetBuilder<relChild> value0)
				{
					return ProgramSetBuilder<singleRelChildList>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SinglePosList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104F7 RID: 66807 RVA: 0x00386098 File Offset: 0x00384298
				public ProgramSetBuilder<relChild> RelChild(ProgramSetBuilder<select> value0)
				{
					return ProgramSetBuilder<relChild>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.RelChild, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104F8 RID: 66808 RVA: 0x003860C9 File Offset: 0x003842C9
				public ProgramSetBuilder<pos> AbsPos(ProgramSetBuilder<p> value0)
				{
					return ProgramSetBuilder<pos>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.AbsPos, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104F9 RID: 66809 RVA: 0x003860FA File Offset: 0x003842FA
				public ProgramSetBuilder<children> Prepend(ProgramSetBuilder<interval> value0, ProgramSetBuilder<children> value1)
				{
					return ProgramSetBuilder<children>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Prepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104FA RID: 66810 RVA: 0x0038613A File Offset: 0x0038433A
				public ProgramSetBuilder<interval> SingleList(ProgramSetBuilder<newDsl> value0)
				{
					return ProgramSetBuilder<interval>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SingleList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104FB RID: 66811 RVA: 0x0038616B File Offset: 0x0038436B
				public ProgramSetBuilder<inorderAllNodes> InOrderAllNodes(ProgramSetBuilder<selectedNode> value0)
				{
					return ProgramSetBuilder<inorderAllNodes>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.InOrderAllNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x060104FC RID: 66812 RVA: 0x0038619C File Offset: 0x0038439C
				public ProgramSetBuilder<select> Select(ProgramSetBuilder<tmpFilter> value0, ProgramSetBuilder<k> value1)
				{
					return ProgramSetBuilder<select>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.Select, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104FD RID: 66813 RVA: 0x003861DC File Offset: 0x003843DC
				public ProgramSetBuilder<tmpFilter> TmpFilter(ProgramSetBuilder<match> value0, ProgramSetBuilder<inorderAllNodes> value1)
				{
					return ProgramSetBuilder<tmpFilter>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.TmpFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104FE RID: 66814 RVA: 0x0038621C File Offset: 0x0038441C
				public ProgramSetBuilder<sequenceMap> SequenceMap(ProgramSetBuilder<newDsl> value0, ProgramSetBuilder<parentChildren> value1)
				{
					return ProgramSetBuilder<sequenceMap>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.SequenceMap, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x060104FF RID: 66815 RVA: 0x0038625C File Offset: 0x0038445C
				public ProgramSetBuilder<convertSequence> ConvertSequence(ProgramSetBuilder<select> value0, ProgramSetBuilder<sequenceMap> value1)
				{
					return ProgramSetBuilder<convertSequence>.CreateUnsafe(ProgramSet.Join(this._builders.Rule.ConvertSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04006251 RID: 25169
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E4F RID: 7759
			public class ExplicitJoins
			{
				// Token: 0x06010500 RID: 66816 RVA: 0x0038629C File Offset: 0x0038449C
				public ExplicitJoins(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010501 RID: 66817 RVA: 0x003862AB File Offset: 0x003844AB
				public JoinProgramSetBuilder<guardedRule> GuardedRule(ProgramSetBuilder<match> value0, ProgramSetBuilder<newDsl> value1)
				{
					return JoinProgramSetBuilder<guardedRule>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.GuardedRule, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010502 RID: 66818 RVA: 0x003862EB File Offset: 0x003844EB
				public JoinProgramSetBuilder<match> Conj(ProgramSetBuilder<pred> value0, ProgramSetBuilder<match> value1)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Conj, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010503 RID: 66819 RVA: 0x0038632C File Offset: 0x0038452C
				public JoinProgramSetBuilder<pred> IsKind(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<kind> value2)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsKind, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06010504 RID: 66820 RVA: 0x00386388 File Offset: 0x00384588
				public JoinProgramSetBuilder<pred> IsAttributePresent(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<name> value2, ProgramSetBuilder<value> value3)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsAttributePresent, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x06010505 RID: 66821 RVA: 0x003863F3 File Offset: 0x003845F3
				public JoinProgramSetBuilder<pred> IsNthChild(ProgramSetBuilder<x> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.IsNthChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010506 RID: 66822 RVA: 0x00386434 File Offset: 0x00384634
				public JoinProgramSetBuilder<pred> HasNChildren(ProgramSetBuilder<x> value0, ProgramSetBuilder<path> value1, ProgramSetBuilder<k> value2)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.HasNChildren, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06010507 RID: 66823 RVA: 0x0038648E File Offset: 0x0038468E
				public JoinProgramSetBuilder<pred> Not(ProgramSetBuilder<pred> value0)
				{
					return JoinProgramSetBuilder<pred>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Not, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010508 RID: 66824 RVA: 0x003864BF File Offset: 0x003846BF
				public JoinProgramSetBuilder<construction> LeafConstLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1)
				{
					return JoinProgramSetBuilder<construction>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafConstLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010509 RID: 66825 RVA: 0x00386500 File Offset: 0x00384700
				public JoinProgramSetBuilder<construction> ConstLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<children> value2)
				{
					return JoinProgramSetBuilder<construction>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0601050A RID: 66826 RVA: 0x0038655C File Offset: 0x0038475C
				public JoinProgramSetBuilder<construction> ConstSequenceLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<construction> value2, ProgramSetBuilder<sequenceChildren> value3)
				{
					return JoinProgramSetBuilder<construction>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConstSequenceLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null,
						(value3 != null) ? value3.Set : null
					}));
				}

				// Token: 0x0601050B RID: 66827 RVA: 0x003865C8 File Offset: 0x003847C8
				public JoinProgramSetBuilder<construction> LeafConstSequenceLabelNode(ProgramSetBuilder<label> value0, ProgramSetBuilder<attributes> value1, ProgramSetBuilder<construction> value2)
				{
					return JoinProgramSetBuilder<construction>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.LeafConstSequenceLabelNode, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0601050C RID: 66828 RVA: 0x00386624 File Offset: 0x00384824
				public JoinProgramSetBuilder<sequenceChildren> InsertAtAbs(ProgramSetBuilder<select> value0, ProgramSetBuilder<pos> value1, ProgramSetBuilder<newDsl> value2)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.InsertAtAbs, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0601050D RID: 66829 RVA: 0x00386680 File Offset: 0x00384880
				public JoinProgramSetBuilder<sequenceChildren> InsertAtRel(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChild> value1, ProgramSetBuilder<newDsl> value2)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.InsertAtRel, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x0601050E RID: 66830 RVA: 0x003866DA File Offset: 0x003848DA
				public JoinProgramSetBuilder<sequenceChildren> DeleteChild(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChild> value1)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.DeleteChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0601050F RID: 66831 RVA: 0x0038671C File Offset: 0x0038491C
				public JoinProgramSetBuilder<sequenceChildren> ReplaceChildren(ProgramSetBuilder<select> value0, ProgramSetBuilder<relChildList> value1, ProgramSetBuilder<children> value2)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ReplaceChildren, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null,
						(value2 != null) ? value2.Set : null
					}));
				}

				// Token: 0x06010510 RID: 66832 RVA: 0x00386776 File Offset: 0x00384976
				public JoinProgramSetBuilder<parentChildren> Children(ProgramSetBuilder<parent> value0)
				{
					return JoinProgramSetBuilder<parentChildren>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Children, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010511 RID: 66833 RVA: 0x003867A7 File Offset: 0x003849A7
				public JoinProgramSetBuilder<relChildList> ConcatChild(ProgramSetBuilder<singleRelChildList> value0, ProgramSetBuilder<relChildList> value1)
				{
					return JoinProgramSetBuilder<relChildList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConcatChild, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010512 RID: 66834 RVA: 0x003867E7 File Offset: 0x003849E7
				public JoinProgramSetBuilder<singleRelChildList> SinglePosList(ProgramSetBuilder<relChild> value0)
				{
					return JoinProgramSetBuilder<singleRelChildList>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SinglePosList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010513 RID: 66835 RVA: 0x00386818 File Offset: 0x00384A18
				public JoinProgramSetBuilder<relChild> RelChild(ProgramSetBuilder<select> value0)
				{
					return JoinProgramSetBuilder<relChild>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.RelChild, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010514 RID: 66836 RVA: 0x00386849 File Offset: 0x00384A49
				public JoinProgramSetBuilder<pos> AbsPos(ProgramSetBuilder<p> value0)
				{
					return JoinProgramSetBuilder<pos>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.AbsPos, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010515 RID: 66837 RVA: 0x0038687A File Offset: 0x00384A7A
				public JoinProgramSetBuilder<children> Prepend(ProgramSetBuilder<interval> value0, ProgramSetBuilder<children> value1)
				{
					return JoinProgramSetBuilder<children>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Prepend, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010516 RID: 66838 RVA: 0x003868BA File Offset: 0x00384ABA
				public JoinProgramSetBuilder<interval> SingleList(ProgramSetBuilder<newDsl> value0)
				{
					return JoinProgramSetBuilder<interval>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SingleList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010517 RID: 66839 RVA: 0x003868EB File Offset: 0x00384AEB
				public JoinProgramSetBuilder<inorderAllNodes> InOrderAllNodes(ProgramSetBuilder<selectedNode> value0)
				{
					return JoinProgramSetBuilder<inorderAllNodes>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.InOrderAllNodes, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010518 RID: 66840 RVA: 0x0038691C File Offset: 0x00384B1C
				public JoinProgramSetBuilder<select> Select(ProgramSetBuilder<tmpFilter> value0, ProgramSetBuilder<k> value1)
				{
					return JoinProgramSetBuilder<select>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.Select, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x06010519 RID: 66841 RVA: 0x0038695C File Offset: 0x00384B5C
				public JoinProgramSetBuilder<tmpFilter> TmpFilter(ProgramSetBuilder<match> value0, ProgramSetBuilder<inorderAllNodes> value1)
				{
					return JoinProgramSetBuilder<tmpFilter>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.TmpFilter, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0601051A RID: 66842 RVA: 0x0038699C File Offset: 0x00384B9C
				public JoinProgramSetBuilder<sequenceMap> SequenceMap(ProgramSetBuilder<newDsl> value0, ProgramSetBuilder<parentChildren> value1)
				{
					return JoinProgramSetBuilder<sequenceMap>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.SequenceMap, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x0601051B RID: 66843 RVA: 0x003869DC File Offset: 0x00384BDC
				public JoinProgramSetBuilder<convertSequence> ConvertSequence(ProgramSetBuilder<select> value0, ProgramSetBuilder<sequenceMap> value1)
				{
					return JoinProgramSetBuilder<convertSequence>.CreateUnsafe(new JoinProgramSet(this._builders.Rule.ConvertSequence, new ProgramSet[]
					{
						(value0 != null) ? value0.Set : null,
						(value1 != null) ? value1.Set : null
					}));
				}

				// Token: 0x04006252 RID: 25170
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E50 RID: 7760
			public class JoinUnnamedConversions
			{
				// Token: 0x0601051C RID: 66844 RVA: 0x00386A1C File Offset: 0x00384C1C
				public JoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0601051D RID: 66845 RVA: 0x00386A2B File Offset: 0x00384C2B
				public ProgramSetBuilder<match> match_pred(ProgramSetBuilder<pred> value0)
				{
					return ProgramSetBuilder<match>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.match_pred, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0601051E RID: 66846 RVA: 0x00386A5C File Offset: 0x00384C5C
				public ProgramSetBuilder<newDsl> newDsl_select(ProgramSetBuilder<select> value0)
				{
					return ProgramSetBuilder<newDsl>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.newDsl_select, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0601051F RID: 66847 RVA: 0x00386A8D File Offset: 0x00384C8D
				public ProgramSetBuilder<newDsl> newDsl_construction(ProgramSetBuilder<construction> value0)
				{
					return ProgramSetBuilder<newDsl>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.newDsl_construction, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010520 RID: 66848 RVA: 0x00386ABE File Offset: 0x00384CBE
				public ProgramSetBuilder<sequenceChildren> sequenceChildren_children(ProgramSetBuilder<children> value0)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.sequenceChildren_children, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010521 RID: 66849 RVA: 0x00386AEF File Offset: 0x00384CEF
				public ProgramSetBuilder<sequenceChildren> sequenceChildren_convertSequence(ProgramSetBuilder<convertSequence> value0)
				{
					return ProgramSetBuilder<sequenceChildren>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.sequenceChildren_convertSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010522 RID: 66850 RVA: 0x00386B20 File Offset: 0x00384D20
				public ProgramSetBuilder<relChildList> relChildList_singleRelChildList(ProgramSetBuilder<singleRelChildList> value0)
				{
					return ProgramSetBuilder<relChildList>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.relChildList_singleRelChildList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010523 RID: 66851 RVA: 0x00386B51 File Offset: 0x00384D51
				public ProgramSetBuilder<children> children_interval(ProgramSetBuilder<interval> value0)
				{
					return ProgramSetBuilder<children>.CreateUnsafe(ProgramSet.Join(this._builders.UnnamedConversion.children_interval, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04006253 RID: 25171
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E51 RID: 7761
			public class ExplicitJoinUnnamedConversions
			{
				// Token: 0x06010524 RID: 66852 RVA: 0x00386B82 File Offset: 0x00384D82
				public ExplicitJoinUnnamedConversions(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x06010525 RID: 66853 RVA: 0x00386B91 File Offset: 0x00384D91
				public JoinProgramSetBuilder<match> match_pred(ProgramSetBuilder<pred> value0)
				{
					return JoinProgramSetBuilder<match>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.match_pred, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010526 RID: 66854 RVA: 0x00386BC2 File Offset: 0x00384DC2
				public JoinProgramSetBuilder<newDsl> newDsl_select(ProgramSetBuilder<select> value0)
				{
					return JoinProgramSetBuilder<newDsl>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.newDsl_select, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010527 RID: 66855 RVA: 0x00386BF3 File Offset: 0x00384DF3
				public JoinProgramSetBuilder<newDsl> newDsl_construction(ProgramSetBuilder<construction> value0)
				{
					return JoinProgramSetBuilder<newDsl>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.newDsl_construction, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010528 RID: 66856 RVA: 0x00386C24 File Offset: 0x00384E24
				public JoinProgramSetBuilder<sequenceChildren> sequenceChildren_children(ProgramSetBuilder<children> value0)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.sequenceChildren_children, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x06010529 RID: 66857 RVA: 0x00386C55 File Offset: 0x00384E55
				public JoinProgramSetBuilder<sequenceChildren> sequenceChildren_convertSequence(ProgramSetBuilder<convertSequence> value0)
				{
					return JoinProgramSetBuilder<sequenceChildren>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.sequenceChildren_convertSequence, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0601052A RID: 66858 RVA: 0x00386C86 File Offset: 0x00384E86
				public JoinProgramSetBuilder<relChildList> relChildList_singleRelChildList(ProgramSetBuilder<singleRelChildList> value0)
				{
					return JoinProgramSetBuilder<relChildList>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.relChildList_singleRelChildList, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x0601052B RID: 66859 RVA: 0x00386CB7 File Offset: 0x00384EB7
				public JoinProgramSetBuilder<children> children_interval(ProgramSetBuilder<interval> value0)
				{
					return JoinProgramSetBuilder<children>.CreateUnsafe(new JoinProgramSet(this._builders.UnnamedConversion.children_interval, new ProgramSet[] { (value0 != null) ? value0.Set : null }));
				}

				// Token: 0x04006254 RID: 25172
				private readonly GrammarBuilders _builders;
			}

			// Token: 0x02001E52 RID: 7762
			public class Casts
			{
				// Token: 0x0601052C RID: 66860 RVA: 0x00386CE8 File Offset: 0x00384EE8
				public Casts(GrammarBuilders builders)
				{
					this._builders = builders;
				}

				// Token: 0x0601052D RID: 66861 RVA: 0x00386CF8 File Offset: 0x00384EF8
				public ProgramSetBuilder<guardedRule> guardedRule(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.guardedRule)
					{
						string text = "set";
						string text2 = "expected program set for symbol guardedRule but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.guardedRule>.CreateUnsafe(set);
				}

				// Token: 0x0601052E RID: 66862 RVA: 0x00386D50 File Offset: 0x00384F50
				public ProgramSetBuilder<match> match(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.match)
					{
						string text = "set";
						string text2 = "expected program set for symbol match but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.match>.CreateUnsafe(set);
				}

				// Token: 0x0601052F RID: 66863 RVA: 0x00386DA8 File Offset: 0x00384FA8
				public ProgramSetBuilder<pred> pred(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pred)
					{
						string text = "set";
						string text2 = "expected program set for symbol pred but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pred>.CreateUnsafe(set);
				}

				// Token: 0x06010530 RID: 66864 RVA: 0x00386E00 File Offset: 0x00385000
				public ProgramSetBuilder<newDsl> newDsl(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.newDsl)
					{
						string text = "set";
						string text2 = "expected program set for symbol newDsl but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.newDsl>.CreateUnsafe(set);
				}

				// Token: 0x06010531 RID: 66865 RVA: 0x00386E58 File Offset: 0x00385058
				public ProgramSetBuilder<construction> construction(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.construction)
					{
						string text = "set";
						string text2 = "expected program set for symbol construction but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.construction>.CreateUnsafe(set);
				}

				// Token: 0x06010532 RID: 66866 RVA: 0x00386EB0 File Offset: 0x003850B0
				public ProgramSetBuilder<select> select(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.select)
					{
						string text = "set";
						string text2 = "expected program set for symbol @select but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.select>.CreateUnsafe(set);
				}

				// Token: 0x06010533 RID: 66867 RVA: 0x00386F08 File Offset: 0x00385108
				public ProgramSetBuilder<tmpFilter> tmpFilter(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.tmpFilter)
					{
						string text = "set";
						string text2 = "expected program set for symbol tmpFilter but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.tmpFilter>.CreateUnsafe(set);
				}

				// Token: 0x06010534 RID: 66868 RVA: 0x00386F60 File Offset: 0x00385160
				public ProgramSetBuilder<x> x(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.x)
					{
						string text = "set";
						string text2 = "expected program set for symbol x but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.x>.CreateUnsafe(set);
				}

				// Token: 0x06010535 RID: 66869 RVA: 0x00386FB8 File Offset: 0x003851B8
				public ProgramSetBuilder<sequenceChildren> sequenceChildren(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sequenceChildren)
					{
						string text = "set";
						string text2 = "expected program set for symbol sequenceChildren but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceChildren>.CreateUnsafe(set);
				}

				// Token: 0x06010536 RID: 66870 RVA: 0x00387010 File Offset: 0x00385210
				public ProgramSetBuilder<convertSequence> convertSequence(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.convertSequence)
					{
						string text = "set";
						string text2 = "expected program set for symbol convertSequence but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.convertSequence>.CreateUnsafe(set);
				}

				// Token: 0x06010537 RID: 66871 RVA: 0x00387068 File Offset: 0x00385268
				public ProgramSetBuilder<parent> parent(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.parent)
					{
						string text = "set";
						string text2 = "expected program set for symbol parent but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parent>.CreateUnsafe(set);
				}

				// Token: 0x06010538 RID: 66872 RVA: 0x003870C0 File Offset: 0x003852C0
				public ProgramSetBuilder<sequenceMap> sequenceMap(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.sequenceMap)
					{
						string text = "set";
						string text2 = "expected program set for symbol sequenceMap but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.sequenceMap>.CreateUnsafe(set);
				}

				// Token: 0x06010539 RID: 66873 RVA: 0x00387118 File Offset: 0x00385318
				public ProgramSetBuilder<selectedNode> selectedNode(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.selectedNode)
					{
						string text = "set";
						string text2 = "expected program set for symbol selectedNode but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.selectedNode>.CreateUnsafe(set);
				}

				// Token: 0x0601053A RID: 66874 RVA: 0x00387170 File Offset: 0x00385370
				public ProgramSetBuilder<parentChildren> parentChildren(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.parentChildren)
					{
						string text = "set";
						string text2 = "expected program set for symbol parentChildren but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.parentChildren>.CreateUnsafe(set);
				}

				// Token: 0x0601053B RID: 66875 RVA: 0x003871C8 File Offset: 0x003853C8
				public ProgramSetBuilder<relChildList> relChildList(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.relChildList)
					{
						string text = "set";
						string text2 = "expected program set for symbol relChildList but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChildList>.CreateUnsafe(set);
				}

				// Token: 0x0601053C RID: 66876 RVA: 0x00387220 File Offset: 0x00385420
				public ProgramSetBuilder<singleRelChildList> singleRelChildList(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.singleRelChildList)
					{
						string text = "set";
						string text2 = "expected program set for symbol singleRelChildList but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.singleRelChildList>.CreateUnsafe(set);
				}

				// Token: 0x0601053D RID: 66877 RVA: 0x00387278 File Offset: 0x00385478
				public ProgramSetBuilder<relChild> relChild(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.relChild)
					{
						string text = "set";
						string text2 = "expected program set for symbol relChild but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.relChild>.CreateUnsafe(set);
				}

				// Token: 0x0601053E RID: 66878 RVA: 0x003872D0 File Offset: 0x003854D0
				public ProgramSetBuilder<pos> pos(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.pos)
					{
						string text = "set";
						string text2 = "expected program set for symbol pos but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.pos>.CreateUnsafe(set);
				}

				// Token: 0x0601053F RID: 66879 RVA: 0x00387328 File Offset: 0x00385528
				public ProgramSetBuilder<children> children(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.children)
					{
						string text = "set";
						string text2 = "expected program set for symbol children but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.children>.CreateUnsafe(set);
				}

				// Token: 0x06010540 RID: 66880 RVA: 0x00387380 File Offset: 0x00385580
				public ProgramSetBuilder<interval> interval(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.interval)
					{
						string text = "set";
						string text2 = "expected program set for symbol interval but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.interval>.CreateUnsafe(set);
				}

				// Token: 0x06010541 RID: 66881 RVA: 0x003873D8 File Offset: 0x003855D8
				public ProgramSetBuilder<inorderAllNodes> inorderAllNodes(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.inorderAllNodes)
					{
						string text = "set";
						string text2 = "expected program set for symbol inorderAllNodes but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.inorderAllNodes>.CreateUnsafe(set);
				}

				// Token: 0x06010542 RID: 66882 RVA: 0x00387430 File Offset: 0x00385630
				public ProgramSetBuilder<label> label(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.label)
					{
						string text = "set";
						string text2 = "expected program set for symbol label but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.label>.CreateUnsafe(set);
				}

				// Token: 0x06010543 RID: 66883 RVA: 0x00387488 File Offset: 0x00385688
				public ProgramSetBuilder<attributes> attributes(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.attributes)
					{
						string text = "set";
						string text2 = "expected program set for symbol attributes but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.attributes>.CreateUnsafe(set);
				}

				// Token: 0x06010544 RID: 66884 RVA: 0x003874E0 File Offset: 0x003856E0
				public ProgramSetBuilder<kind> kind(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.kind)
					{
						string text = "set";
						string text2 = "expected program set for symbol kind but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.kind>.CreateUnsafe(set);
				}

				// Token: 0x06010545 RID: 66885 RVA: 0x00387538 File Offset: 0x00385738
				public ProgramSetBuilder<name> name(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.name)
					{
						string text = "set";
						string text2 = "expected program set for symbol name but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.name>.CreateUnsafe(set);
				}

				// Token: 0x06010546 RID: 66886 RVA: 0x00387590 File Offset: 0x00385790
				public ProgramSetBuilder<value> value(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.value)
					{
						string text = "set";
						string text2 = "expected program set for symbol @value but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.value>.CreateUnsafe(set);
				}

				// Token: 0x06010547 RID: 66887 RVA: 0x003875E8 File Offset: 0x003857E8
				public ProgramSetBuilder<k> k(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.k)
					{
						string text = "set";
						string text2 = "expected program set for symbol k but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.k>.CreateUnsafe(set);
				}

				// Token: 0x06010548 RID: 66888 RVA: 0x00387640 File Offset: 0x00385840
				public ProgramSetBuilder<p> p(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.p)
					{
						string text = "set";
						string text2 = "expected program set for symbol p but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.p>.CreateUnsafe(set);
				}

				// Token: 0x06010549 RID: 66889 RVA: 0x00387698 File Offset: 0x00385898
				public ProgramSetBuilder<path> path(ProgramSet set)
				{
					if (set.Symbol != this._builders.Symbol.path)
					{
						string text = "set";
						string text2 = "expected program set for symbol path but received ";
						Symbol symbol = set.Symbol;
						throw new ArgumentException(text, text2 + ((symbol != null) ? symbol.ToString() : null));
					}
					return ProgramSetBuilder<Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes.path>.CreateUnsafe(set);
				}

				// Token: 0x04006255 RID: 25173
				private readonly GrammarBuilders _builders;
			}
		}
	}
}
