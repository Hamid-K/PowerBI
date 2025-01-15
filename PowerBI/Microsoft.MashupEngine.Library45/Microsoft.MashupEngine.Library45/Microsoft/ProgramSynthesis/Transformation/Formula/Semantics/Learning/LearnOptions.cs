using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001616 RID: 5654
	public class LearnOptions : DSLOptions
	{
		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x0600BC78 RID: 48248 RVA: 0x00288440 File Offset: 0x00286640
		// (set) Token: 0x0600BC79 RID: 48249 RVA: 0x00288448 File Offset: 0x00286648
		public int ArithmeticMinExampleCount { get; set; } = 2;

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x0600BC7A RID: 48250 RVA: 0x00288451 File Offset: 0x00286651
		// (set) Token: 0x0600BC7B RID: 48251 RVA: 0x00288459 File Offset: 0x00286659
		public ArithmeticStrategy ArithmeticStrategy { get; set; }

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x0600BC7C RID: 48252 RVA: 0x00288462 File Offset: 0x00286662
		// (set) Token: 0x0600BC7D RID: 48253 RVA: 0x0028846A File Offset: 0x0028666A
		public IReadOnlyList<string> ColumnNamePriority { get; set; }

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x0600BC7E RID: 48254 RVA: 0x00288473 File Offset: 0x00286673
		// (set) Token: 0x0600BC7F RID: 48255 RVA: 0x0028847B File Offset: 0x0028667B
		public int ConditionalBranchMinExampleCount { get; set; } = 1;

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x0600BC80 RID: 48256 RVA: 0x00288484 File Offset: 0x00286684
		// (set) Token: 0x0600BC81 RID: 48257 RVA: 0x0028848C File Offset: 0x0028668C
		public int ConditionalMaxBranches { get; set; } = 5;

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x0600BC82 RID: 48258 RVA: 0x00288495 File Offset: 0x00286695
		// (set) Token: 0x0600BC83 RID: 48259 RVA: 0x0028849D File Offset: 0x0028669D
		public IReadOnlyList<CultureInfo> DataCultures { get; set; } = new CultureInfo[]
		{
			new CultureInfo("en-US"),
			new CultureInfo("en-GB")
		};

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x0600BC84 RID: 48260 RVA: 0x002884A6 File Offset: 0x002866A6
		// (set) Token: 0x0600BC85 RID: 48261 RVA: 0x002884AE File Offset: 0x002866AE
		public DateTimeSourceKind DateTimeSources { get; set; } = DateTimeSourceKind.All;

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x0600BC86 RID: 48262 RVA: 0x002884B7 File Offset: 0x002866B7
		// (set) Token: 0x0600BC87 RID: 48263 RVA: 0x002884BF File Offset: 0x002866BF
		public bool EnableArithmetic { get; set; } = true;

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x0600BC88 RID: 48264 RVA: 0x002884C8 File Offset: 0x002866C8
		// (set) Token: 0x0600BC89 RID: 48265 RVA: 0x002884D0 File Offset: 0x002866D0
		public bool EnableArithmeticConstants { get; set; } = true;

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x0600BC8A RID: 48266 RVA: 0x002884D9 File Offset: 0x002866D9
		// (set) Token: 0x0600BC8B RID: 48267 RVA: 0x002884E1 File Offset: 0x002866E1
		public bool EnableConcat { get; set; } = true;

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x0600BC8C RID: 48268 RVA: 0x002884EA File Offset: 0x002866EA
		// (set) Token: 0x0600BC8D RID: 48269 RVA: 0x002884F2 File Offset: 0x002866F2
		public bool EnableConditional { get; set; } = true;

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x0600BC8E RID: 48270 RVA: 0x002884FB File Offset: 0x002866FB
		// (set) Token: 0x0600BC8F RID: 48271 RVA: 0x00288503 File Offset: 0x00286703
		public bool EnableDateTimePart { get; set; } = true;

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x0600BC90 RID: 48272 RVA: 0x0028850C File Offset: 0x0028670C
		// (set) Token: 0x0600BC91 RID: 48273 RVA: 0x00288514 File Offset: 0x00286714
		public bool EnableDefaultColumnNamePriority { get; set; } = true;

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x0600BC92 RID: 48274 RVA: 0x0028851D File Offset: 0x0028671D
		// (set) Token: 0x0600BC93 RID: 48275 RVA: 0x00288525 File Offset: 0x00286725
		public bool EnableForwardFill { get; set; }

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x0600BC94 RID: 48276 RVA: 0x0028852E File Offset: 0x0028672E
		// (set) Token: 0x0600BC95 RID: 48277 RVA: 0x00288536 File Offset: 0x00286736
		public bool EnableFromDateTimePart { get; set; } = true;

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x0600BC96 RID: 48278 RVA: 0x0028853F File Offset: 0x0028673F
		// (set) Token: 0x0600BC97 RID: 48279 RVA: 0x00288547 File Offset: 0x00286747
		public bool EnableFromNumberCoalesced { get; set; }

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x0600BC98 RID: 48280 RVA: 0x00288550 File Offset: 0x00286750
		// (set) Token: 0x0600BC99 RID: 48281 RVA: 0x00288558 File Offset: 0x00286758
		public bool EnableFromNumberStr { get; set; }

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x0600BC9A RID: 48282 RVA: 0x00288561 File Offset: 0x00286761
		// (set) Token: 0x0600BC9B RID: 48283 RVA: 0x00288569 File Offset: 0x00286769
		public bool EnableLearningShortCircuit { get; set; } = true;

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x0600BC9C RID: 48284 RVA: 0x00288572 File Offset: 0x00286772
		// (set) Token: 0x0600BC9D RID: 48285 RVA: 0x0028857A File Offset: 0x0028677A
		public bool EnableLength { get; set; } = true;

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x0600BC9E RID: 48286 RVA: 0x00288583 File Offset: 0x00286783
		public bool EnableMatch
		{
			get
			{
				return this.EnableMatchNames > MatchName.None;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x0600BC9F RID: 48287 RVA: 0x0028858F File Offset: 0x0028678F
		// (set) Token: 0x0600BCA0 RID: 48288 RVA: 0x00288597 File Offset: 0x00286797
		public MatchName EnableMatchNames { get; set; } = MatchName.All;

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x0600BCA1 RID: 48289 RVA: 0x002885A0 File Offset: 0x002867A0
		// (set) Token: 0x0600BCA2 RID: 48290 RVA: 0x002885A8 File Offset: 0x002867A8
		public bool EnableMatchUnicode { get; set; } = true;

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x0600BCA3 RID: 48291 RVA: 0x002885B1 File Offset: 0x002867B1
		// (set) Token: 0x0600BCA4 RID: 48292 RVA: 0x002885B9 File Offset: 0x002867B9
		public bool EnableNegativePosition { get; set; } = true;

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x0600BCA5 RID: 48293 RVA: 0x002885C2 File Offset: 0x002867C2
		// (set) Token: 0x0600BCA6 RID: 48294 RVA: 0x002885CA File Offset: 0x002867CA
		public bool EnableParseDateTimePartial { get; set; } = true;

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x0600BCA7 RID: 48295 RVA: 0x002885D3 File Offset: 0x002867D3
		// (set) Token: 0x0600BCA8 RID: 48296 RVA: 0x002885DB File Offset: 0x002867DB
		public bool EnableProperCase { get; set; } = true;

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x0600BCA9 RID: 48297 RVA: 0x002885E4 File Offset: 0x002867E4
		// (set) Token: 0x0600BCAA RID: 48298 RVA: 0x002885EC File Offset: 0x002867EC
		public bool EnableReplace { get; set; } = true;

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x0600BCAB RID: 48299 RVA: 0x002885F5 File Offset: 0x002867F5
		// (set) Token: 0x0600BCAC RID: 48300 RVA: 0x002885FD File Offset: 0x002867FD
		public bool EnableRoundDateTime { get; set; } = true;

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x0600BCAD RID: 48301 RVA: 0x00288606 File Offset: 0x00286806
		// (set) Token: 0x0600BCAE RID: 48302 RVA: 0x0028860E File Offset: 0x0028680E
		public bool EnableRoundNumber { get; set; } = true;

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x0600BCAF RID: 48303 RVA: 0x00288617 File Offset: 0x00286817
		// (set) Token: 0x0600BCB0 RID: 48304 RVA: 0x0028861F File Offset: 0x0028681F
		public bool EnableSlice { get; set; }

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x0600BCB1 RID: 48305 RVA: 0x00288628 File Offset: 0x00286828
		// (set) Token: 0x0600BCB2 RID: 48306 RVA: 0x00288630 File Offset: 0x00286830
		public bool EnableSliceBetween { get; set; } = true;

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x0600BCB3 RID: 48307 RVA: 0x00288639 File Offset: 0x00286839
		// (set) Token: 0x0600BCB4 RID: 48308 RVA: 0x00288641 File Offset: 0x00286841
		public bool EnableSplit { get; set; } = true;

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x0600BCB5 RID: 48309 RVA: 0x0028864A File Offset: 0x0028684A
		// (set) Token: 0x0600BCB6 RID: 48310 RVA: 0x00288652 File Offset: 0x00286852
		public bool EnableTimePart { get; set; } = true;

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x0600BCB7 RID: 48311 RVA: 0x0028865B File Offset: 0x0028685B
		// (set) Token: 0x0600BCB8 RID: 48312 RVA: 0x00288663 File Offset: 0x00286863
		public bool EnableTrim { get; set; } = true;

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x0600BCB9 RID: 48313 RVA: 0x0028866C File Offset: 0x0028686C
		// (set) Token: 0x0600BCBA RID: 48314 RVA: 0x00288674 File Offset: 0x00286874
		public bool EnableTrimFull { get; set; }

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x0600BCBB RID: 48315 RVA: 0x0028867D File Offset: 0x0028687D
		// (set) Token: 0x0600BCBC RID: 48316 RVA: 0x00288685 File Offset: 0x00286885
		public int ForwardFillMaxExampleCount { get; set; } = 100;

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x0600BCBD RID: 48317 RVA: 0x0028868E File Offset: 0x0028688E
		// (set) Token: 0x0600BCBE RID: 48318 RVA: 0x00288696 File Offset: 0x00286896
		public int ForwardFillMaxScale { get; set; } = 1;

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x0600BCBF RID: 48319 RVA: 0x0028869F File Offset: 0x0028689F
		// (set) Token: 0x0600BCC0 RID: 48320 RVA: 0x002886A7 File Offset: 0x002868A7
		public int ForwardFillMinExampleCount { get; set; } = 3;

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x0600BCC1 RID: 48321 RVA: 0x002886B0 File Offset: 0x002868B0
		// (set) Token: 0x0600BCC2 RID: 48322 RVA: 0x002886B8 File Offset: 0x002868B8
		public int FromNumbersColumnLimit { get; set; } = 8;

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x0600BCC3 RID: 48323 RVA: 0x002886C1 File Offset: 0x002868C1
		// (set) Token: 0x0600BCC4 RID: 48324 RVA: 0x002886C9 File Offset: 0x002868C9
		public LearnConfidenceBehavior LearnConfidence { get; set; }

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x0600BCC5 RID: 48325 RVA: 0x002886D2 File Offset: 0x002868D2
		// (set) Token: 0x0600BCC6 RID: 48326 RVA: 0x002886DA File Offset: 0x002868DA
		public double MinLearnConfidence { get; set; } = 0.7;

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x0600BCC7 RID: 48327 RVA: 0x002886E3 File Offset: 0x002868E3
		// (set) Token: 0x0600BCC8 RID: 48328 RVA: 0x002886EB File Offset: 0x002868EB
		public int NumberFormatMaxLeadingDigits { get; set; } = 1;

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x0600BCC9 RID: 48329 RVA: 0x002886F4 File Offset: 0x002868F4
		// (set) Token: 0x0600BCCA RID: 48330 RVA: 0x002886FC File Offset: 0x002868FC
		public int NumberRoundMinExampleCount { get; set; } = 1;

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x0600BCCB RID: 48331 RVA: 0x00288705 File Offset: 0x00286905
		// (set) Token: 0x0600BCCC RID: 48332 RVA: 0x0028870D File Offset: 0x0028690D
		public NumberSourceKind NumberSources { get; set; } = NumberSourceKind.All;
	}
}
