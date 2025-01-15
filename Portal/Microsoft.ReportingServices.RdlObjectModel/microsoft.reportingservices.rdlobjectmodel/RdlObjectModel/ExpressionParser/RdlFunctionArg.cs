using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200022D RID: 557
	internal class RdlFunctionArg
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x0002A3C2 File Offset: 0x000285C2
		internal RdlFunctionArg(bool isRequired, RdlArgTypes argType)
			: this(isRequired, argType, false)
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0002A3CD File Offset: 0x000285CD
		internal RdlFunctionArg(bool isRequired, RdlArgTypes argType, bool isVarArg)
		{
			this.m_isRequired = isRequired;
			this.m_argType = argType;
			this.m_isVarArg = isVarArg;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0002A3EA File Offset: 0x000285EA
		internal bool IsRequired
		{
			get
			{
				return this.m_isRequired;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0002A3F2 File Offset: 0x000285F2
		internal RdlArgTypes ArgType
		{
			get
			{
				return this.m_argType;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0002A3FA File Offset: 0x000285FA
		internal bool IsVarArg
		{
			get
			{
				return this.m_isVarArg;
			}
		}

		// Token: 0x040005EB RID: 1515
		private readonly bool m_isRequired;

		// Token: 0x040005EC RID: 1516
		private readonly RdlArgTypes m_argType;

		// Token: 0x040005ED RID: 1517
		private readonly bool m_isVarArg;
	}
}
