using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039E RID: 926
	internal sealed class RdlNamespaceComparer : IComparer<string>
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x000B46DA File Offset: 0x000B28DA
		private RdlNamespaceComparer()
		{
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000B46E4 File Offset: 0x000B28E4
		private static int GetNamespaceComparisonNumber(string nsString)
		{
			if (string.Equals(nsString, "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition"))
			{
				return 0;
			}
			if (string.Equals(nsString, "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"))
			{
				return 1;
			}
			if (string.Equals(nsString, "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"))
			{
				return 2;
			}
			if (string.Equals(nsString, "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition"))
			{
				return 3;
			}
			Global.Tracer.Assert(false, "Invalid RDL namespace: {0}", new object[] { nsString });
			throw new InvalidOperationException("Invalid RDL namespace: " + nsString);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000B4758 File Offset: 0x000B2958
		public int Compare(string x, string y)
		{
			return RdlNamespaceComparer.GetNamespaceComparisonNumber(x).CompareTo(RdlNamespaceComparer.GetNamespaceComparisonNumber(y));
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000B4779 File Offset: 0x000B2979
		public static RdlNamespaceComparer Instance
		{
			get
			{
				return RdlNamespaceComparer.m_instance;
			}
		}

		// Token: 0x04001601 RID: 5633
		private static readonly RdlNamespaceComparer m_instance = new RdlNamespaceComparer();
	}
}
