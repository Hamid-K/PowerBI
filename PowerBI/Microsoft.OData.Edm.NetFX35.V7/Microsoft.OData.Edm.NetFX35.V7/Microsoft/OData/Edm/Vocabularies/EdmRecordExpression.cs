using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000FB RID: 251
	public class EdmRecordExpression : EdmElement, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00013BFC File Offset: 0x00011DFC
		public EdmRecordExpression(params IEdmPropertyConstructor[] properties)
			: this(properties)
		{
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00013C05 File Offset: 0x00011E05
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, params IEdmPropertyConstructor[] properties)
			: this(declaredType, properties)
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00013C0F File Offset: 0x00011E0F
		public EdmRecordExpression(IEnumerable<IEdmPropertyConstructor> properties)
			: this(null, properties)
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00013C19 File Offset: 0x00011E19
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, IEnumerable<IEdmPropertyConstructor> properties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyConstructor>>(properties, "properties");
			this.declaredType = declaredType;
			this.properties = properties;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00013C3B File Offset: 0x00011E3B
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00013C43 File Offset: 0x00011E43
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00013C4B File Offset: 0x00011E4B
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x04000421 RID: 1057
		private readonly IEdmStructuredTypeReference declaredType;

		// Token: 0x04000422 RID: 1058
		private readonly IEnumerable<IEdmPropertyConstructor> properties;
	}
}
