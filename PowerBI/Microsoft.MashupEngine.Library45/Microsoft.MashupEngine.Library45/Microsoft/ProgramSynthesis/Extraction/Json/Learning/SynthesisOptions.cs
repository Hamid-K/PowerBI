using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B82 RID: 2946
	public class SynthesisOptions : DSLOptions
	{
		// Token: 0x06004AC1 RID: 19137 RVA: 0x000EB290 File Offset: 0x000E9490
		public SynthesisOptions()
		{
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x000EB2E4 File Offset: 0x000E94E4
		public SynthesisOptions(SynthesisOptions other)
		{
			this.Auto = other.Auto;
			this.JoinAllArrays = other.JoinAllArrays;
			this.NamePrefix = other.NamePrefix;
			this.SplitTopArrays = other.SplitTopArrays;
			this.HandleInvalidJson = other.HandleInvalidJson;
			this.JoinSingleTopArray = other.JoinSingleTopArray;
			this.DeletePaths = other.DeletePaths;
			this.SplitArrayPaths = other.SplitArrayPaths;
			this.JoinArrayPaths = other.JoinArrayPaths;
			this.LocalizedPowerQueryMStrings = other.LocalizedPowerQueryMStrings;
			this.EscapePowerQueryM = other.EscapePowerQueryM;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x000EB3B9 File Offset: 0x000E95B9
		// (set) Token: 0x06004AC4 RID: 19140 RVA: 0x000EB3C1 File Offset: 0x000E95C1
		public bool Auto { get; set; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x000EB3CA File Offset: 0x000E95CA
		// (set) Token: 0x06004AC6 RID: 19142 RVA: 0x000EB3D2 File Offset: 0x000E95D2
		public bool JoinAllArrays { get; set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x000EB3DB File Offset: 0x000E95DB
		// (set) Token: 0x06004AC8 RID: 19144 RVA: 0x000EB3E3 File Offset: 0x000E95E3
		public string NamePrefix { get; set; }

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x000EB3EC File Offset: 0x000E95EC
		// (set) Token: 0x06004ACA RID: 19146 RVA: 0x000EB3F4 File Offset: 0x000E95F4
		public bool SplitTopArrays { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x000EB3FD File Offset: 0x000E95FD
		// (set) Token: 0x06004ACC RID: 19148 RVA: 0x000EB405 File Offset: 0x000E9605
		public bool HandleInvalidJson { get; set; } = true;

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004ACD RID: 19149 RVA: 0x000EB40E File Offset: 0x000E960E
		// (set) Token: 0x06004ACE RID: 19150 RVA: 0x000EB416 File Offset: 0x000E9616
		public bool JoinSingleTopArray { get; set; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x000EB41F File Offset: 0x000E961F
		// (set) Token: 0x06004AD0 RID: 19152 RVA: 0x000EB427 File Offset: 0x000E9627
		public IReadOnlyList<string[]> DeletePaths { get; set; } = new List<string[]>();

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x000EB430 File Offset: 0x000E9630
		// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x000EB438 File Offset: 0x000E9638
		public IReadOnlyList<string[]> SplitArrayPaths { get; set; } = new List<string[]>();

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x000EB441 File Offset: 0x000E9641
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x000EB449 File Offset: 0x000E9649
		public IReadOnlyList<string[]> JoinArrayPaths { get; set; } = new List<string[]>();

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x000EB452 File Offset: 0x000E9652
		// (set) Token: 0x06004AD6 RID: 19158 RVA: 0x000EB45A File Offset: 0x000E965A
		public HashSet<TargetLanguage> TranslationTargets { get; set; } = new HashSet<TargetLanguage>();

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x000EB463 File Offset: 0x000E9663
		// (set) Token: 0x06004AD8 RID: 19160 RVA: 0x000EB46B File Offset: 0x000E966B
		public HashSet<PythonTarget> PythonTargets { get; set; } = new HashSet<PythonTarget>();

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x000EB474 File Offset: 0x000E9674
		// (set) Token: 0x06004ADA RID: 19162 RVA: 0x000EB47C File Offset: 0x000E967C
		public ILocalizedPowerQueryMJsonStrings LocalizedPowerQueryMStrings { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004ADB RID: 19163 RVA: 0x000EB485 File Offset: 0x000E9685
		// (set) Token: 0x06004ADC RID: 19164 RVA: 0x000EB48D File Offset: 0x000E968D
		public IEscapePowerQueryM EscapePowerQueryM { get; set; }

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x000EB496 File Offset: 0x000E9696
		// (set) Token: 0x06004ADE RID: 19166 RVA: 0x000EB49E File Offset: 0x000E969E
		public HashSet<string> ForbiddenMStepNames { get; set; }

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004ADF RID: 19167 RVA: 0x000EB4A7 File Offset: 0x000E96A7
		// (set) Token: 0x06004AE0 RID: 19168 RVA: 0x000EB4AF File Offset: 0x000E96AF
		public string SourceMStepName { get; set; }
	}
}
