using System;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	internal class FunctionRenderFormat : BaseInternalExpression
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x0002FE3F File Offset: 0x0002E03F
		public FunctionRenderFormat()
		{
			this.m_propertyName = null;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0002FE4E File Offset: 0x0002E04E
		public FunctionRenderFormat(string propertyName)
		{
			this.m_propertyName = propertyName;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0002FE5D File Offset: 0x0002E05D
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0002FE60 File Offset: 0x0002E060
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("Globals!RenderFormat");
			if (!string.IsNullOrEmpty(this.m_propertyName))
			{
				stringBuilder.Append(".");
				stringBuilder.Append(this.m_propertyName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040006A8 RID: 1704
		private readonly string m_propertyName;
	}
}
