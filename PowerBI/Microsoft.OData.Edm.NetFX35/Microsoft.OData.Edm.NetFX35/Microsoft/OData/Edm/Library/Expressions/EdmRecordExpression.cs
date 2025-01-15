using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x020001C9 RID: 457
	public class EdmRecordExpression : EdmElement, IEdmRecordExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x00019750 File Offset: 0x00017950
		public EdmRecordExpression(params IEdmPropertyConstructor[] properties)
			: this((IEnumerable<IEdmPropertyConstructor>)properties)
		{
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001975E File Offset: 0x0001795E
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, params IEdmPropertyConstructor[] properties)
			: this(declaredType, (IEnumerable<IEdmPropertyConstructor>)properties)
		{
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001976D File Offset: 0x0001796D
		public EdmRecordExpression(IEnumerable<IEdmPropertyConstructor> properties)
			: this(null, properties)
		{
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00019777 File Offset: 0x00017977
		public EdmRecordExpression(IEdmStructuredTypeReference declaredType, IEnumerable<IEdmPropertyConstructor> properties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyConstructor>>(properties, "properties");
			this.declaredType = declaredType;
			this.properties = properties;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00019799 File Offset: 0x00017999
		public IEdmStructuredTypeReference DeclaredType
		{
			get
			{
				return this.declaredType;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000197A1 File Offset: 0x000179A1
		public IEnumerable<IEdmPropertyConstructor> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000197A9 File Offset: 0x000179A9
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x040004AF RID: 1199
		private readonly IEdmStructuredTypeReference declaredType;

		// Token: 0x040004B0 RID: 1200
		private readonly IEnumerable<IEdmPropertyConstructor> properties;
	}
}
