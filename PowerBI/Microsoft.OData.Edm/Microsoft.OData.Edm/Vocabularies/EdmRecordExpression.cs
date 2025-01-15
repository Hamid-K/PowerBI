using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F4 RID: 244
	public class EdmRecordExpression : EdmElement, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x000120E0 File Offset: 0x000102E0
		public EdmRecordExpression(params IEdmPropertyConstructor[] properties)
			: this(properties)
		{
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000120E9 File Offset: 0x000102E9
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, params IEdmPropertyConstructor[] properties)
			: this(declaredType, properties)
		{
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000120F3 File Offset: 0x000102F3
		public EdmRecordExpression(IEnumerable<IEdmPropertyConstructor> properties)
			: this(null, properties)
		{
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000120FD File Offset: 0x000102FD
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, IEnumerable<IEdmPropertyConstructor> properties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyConstructor>>(properties, "properties");
			this.declaredType = declaredType;
			this.properties = properties;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001211F File Offset: 0x0001031F
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00012127 File Offset: 0x00010327
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001212F File Offset: 0x0001032F
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x04000315 RID: 789
		private readonly IEdmStructuredTypeReference declaredType;

		// Token: 0x04000316 RID: 790
		private readonly IEnumerable<IEdmPropertyConstructor> properties;
	}
}
