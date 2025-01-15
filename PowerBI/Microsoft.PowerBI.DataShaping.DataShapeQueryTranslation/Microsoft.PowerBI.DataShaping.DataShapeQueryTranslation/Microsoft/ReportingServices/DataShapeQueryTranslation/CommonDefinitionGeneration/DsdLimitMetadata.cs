using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x02000125 RID: 293
	internal sealed class DsdLimitMetadata
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x0002B766 File Offset: 0x00029966
		internal DsdLimitMetadata(ExpressionNode limitCount, Dictionary<string, ExpressionNode> limitProperties)
		{
			this.m_count = limitCount;
			this.m_properties = limitProperties;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002B77C File Offset: 0x0002997C
		public ExpressionNode LimitCount
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002B784 File Offset: 0x00029984
		public Dictionary<string, ExpressionNode> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly ExpressionNode m_count;

		// Token: 0x0400059D RID: 1437
		private readonly Dictionary<string, ExpressionNode> m_properties;
	}
}
