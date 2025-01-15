using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000214 RID: 532
	public class EdmDateConstant : EdmValue, IEdmDateConstantExpression, IEdmExpression, IEdmDateValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x00022F28 File Offset: 0x00021128
		public EdmDateConstant(Date value)
			: this(null, value)
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00022F32 File Offset: 0x00021132
		public EdmDateConstant(IEdmPrimitiveTypeReference type, Date value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00022F42 File Offset: 0x00021142
		public Date Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00022F4A File Offset: 0x0002114A
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00022F4E File Offset: 0x0002114E
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Date;
			}
		}

		// Token: 0x040005C3 RID: 1475
		private readonly Date value;
	}
}
