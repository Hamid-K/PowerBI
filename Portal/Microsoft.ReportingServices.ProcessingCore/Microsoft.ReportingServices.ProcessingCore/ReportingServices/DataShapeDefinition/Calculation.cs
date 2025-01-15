using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000584 RID: 1412
	[DataContract]
	internal sealed class Calculation
	{
		// Token: 0x06005170 RID: 20848 RVA: 0x00159A6B File Offset: 0x00157C6B
		internal Calculation(string id, Expression expression)
		{
			this.m_id = id;
			this.m_expression = expression;
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x00159A81 File Offset: 0x00157C81
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06005172 RID: 20850 RVA: 0x00159A89 File Offset: 0x00157C89
		internal Expression Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x04002916 RID: 10518
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x04002917 RID: 10519
		[DataMember(Name = "Expression", Order = 2)]
		private readonly Expression m_expression;
	}
}
