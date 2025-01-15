using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200202A RID: 8234
	public struct SqlExpressionTranslationResult
	{
		// Token: 0x0600C86F RID: 51311 RVA: 0x0027E07C File Offset: 0x0027C27C
		public static SqlExpressionTranslationResult NewSupported(string expression, IEnumerable<string> resourceNames)
		{
			return new SqlExpressionTranslationResult(expression, resourceNames);
		}

		// Token: 0x0600C870 RID: 51312 RVA: 0x0027E085 File Offset: 0x0027C285
		public static SqlExpressionTranslationResult NewUnrecognized()
		{
			return new SqlExpressionTranslationResult(null, null);
		}

		// Token: 0x0600C871 RID: 51313 RVA: 0x0027E08E File Offset: 0x0027C28E
		private SqlExpressionTranslationResult(string expression, IEnumerable<string> resourceNames)
		{
			this.expression = expression;
			this.resourceNames = resourceNames;
		}

		// Token: 0x17003087 RID: 12423
		// (get) Token: 0x0600C872 RID: 51314 RVA: 0x0027E09E File Offset: 0x0027C29E
		public bool IsSupported
		{
			get
			{
				return this.expression != null;
			}
		}

		// Token: 0x17003088 RID: 12424
		// (get) Token: 0x0600C873 RID: 51315 RVA: 0x0027E0A9 File Offset: 0x0027C2A9
		public string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17003089 RID: 12425
		// (get) Token: 0x0600C874 RID: 51316 RVA: 0x0027E0B1 File Offset: 0x0027C2B1
		public IEnumerable<string> ResourceNames
		{
			get
			{
				return this.resourceNames;
			}
		}

		// Token: 0x04006647 RID: 26183
		private readonly string expression;

		// Token: 0x04006648 RID: 26184
		private readonly IEnumerable<string> resourceNames;
	}
}
