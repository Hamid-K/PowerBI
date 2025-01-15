using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200057B RID: 1403
	[Serializable]
	public sealed class ExpressionUsage
	{
		// Token: 0x060050D4 RID: 20692 RVA: 0x00153E51 File Offset: 0x00152051
		public ExpressionUsage()
		{
			this.m_functionCollection = new Dictionary<string, int>();
			this.m_syntaxNodeCollection = new Dictionary<string, HashSet<string>>();
			this.CanUseSafeExpressionsRuntime = true;
			this.IsEnabledForSafeExpressionsRuntime = true;
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x00153E7D File Offset: 0x0015207D
		public IDictionary<string, int> FunctionCollection
		{
			get
			{
				return this.m_functionCollection;
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x00153E85 File Offset: 0x00152085
		public IDictionary<string, HashSet<string>> SyntaxNodeCollection
		{
			get
			{
				return this.m_syntaxNodeCollection;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x00153E8D File Offset: 0x0015208D
		// (set) Token: 0x060050D8 RID: 20696 RVA: 0x00153E95 File Offset: 0x00152095
		public bool HasParameters { get; set; }

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x060050D9 RID: 20697 RVA: 0x00153E9E File Offset: 0x0015209E
		// (set) Token: 0x060050DA RID: 20698 RVA: 0x00153EA6 File Offset: 0x001520A6
		public bool HasFields { get; set; }

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x00153EAF File Offset: 0x001520AF
		// (set) Token: 0x060050DC RID: 20700 RVA: 0x00153EB7 File Offset: 0x001520B7
		public bool HasGlobals { get; set; }

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00153EC0 File Offset: 0x001520C0
		// (set) Token: 0x060050DE RID: 20702 RVA: 0x00153EC8 File Offset: 0x001520C8
		public bool HasUser { get; set; }

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x00153ED1 File Offset: 0x001520D1
		// (set) Token: 0x060050E0 RID: 20704 RVA: 0x00153ED9 File Offset: 0x001520D9
		public bool HasVariables { get; set; }

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x00153EE2 File Offset: 0x001520E2
		// (set) Token: 0x060050E2 RID: 20706 RVA: 0x00153EEA File Offset: 0x001520EA
		public bool HasLiterals { get; set; }

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x060050E3 RID: 20707 RVA: 0x00153EF3 File Offset: 0x001520F3
		// (set) Token: 0x060050E4 RID: 20708 RVA: 0x00153EFB File Offset: 0x001520FB
		public bool HasUnknownExpressionTypes { get; set; }

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x060050E5 RID: 20709 RVA: 0x00153F04 File Offset: 0x00152104
		// (set) Token: 0x060050E6 RID: 20710 RVA: 0x00153F0C File Offset: 0x0015210C
		public bool HasComplexExpressions { get; set; }

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x060050E7 RID: 20711 RVA: 0x00153F15 File Offset: 0x00152115
		// (set) Token: 0x060050E8 RID: 20712 RVA: 0x00153F1D File Offset: 0x0015211D
		public bool CanUseSafeExpressionsRuntime { get; set; }

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x00153F26 File Offset: 0x00152126
		// (set) Token: 0x060050EA RID: 20714 RVA: 0x00153F2E File Offset: 0x0015212E
		public bool IsEnabledForSafeExpressionsRuntime { get; set; }

		// Token: 0x040028C9 RID: 10441
		private readonly IDictionary<string, int> m_functionCollection;

		// Token: 0x040028CA RID: 10442
		private readonly IDictionary<string, HashSet<string>> m_syntaxNodeCollection;
	}
}
