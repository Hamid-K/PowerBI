using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028D RID: 653
	[Serializable]
	internal sealed class FunctionUserLanguage : BaseInternalExpression
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x000302A8 File Offset: 0x0002E4A8
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000302AC File Offset: 0x0002E4AC
		public override string WriteSource(NameChanges nameChanges)
		{
			return "User!Language";
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000302B3 File Offset: 0x0002E4B3
		public override object Evaluate()
		{
			return CultureInfo.CurrentCulture.IetfLanguageTag;
		}
	}
}
