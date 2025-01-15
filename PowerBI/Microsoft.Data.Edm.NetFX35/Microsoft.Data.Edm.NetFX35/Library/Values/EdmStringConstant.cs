using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001DC RID: 476
	public class EdmStringConstant : EdmValue, IEdmStringConstantExpression, IEdmExpression, IEdmStringValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00020DF5 File Offset: 0x0001EFF5
		public EdmStringConstant(string value)
			: this(null, value)
		{
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00020DFF File Offset: 0x0001EFFF
		public EdmStringConstant(IEdmStringTypeReference type, string value)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<string>(value, "value");
			this.value = value;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00020E1B File Offset: 0x0001F01B
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00020E23 File Offset: 0x0001F023
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00020E27 File Offset: 0x0001F027
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.String;
			}
		}

		// Token: 0x0400054C RID: 1356
		private readonly string value;
	}
}
