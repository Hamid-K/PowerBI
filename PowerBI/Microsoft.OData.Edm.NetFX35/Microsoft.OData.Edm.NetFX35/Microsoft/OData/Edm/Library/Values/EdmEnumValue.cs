using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001C2 RID: 450
	public class EdmEnumValue : EdmValue, IEdmEnumValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0001960D File Offset: 0x0001780D
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMember member)
			: this(type, member.Value)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001961C File Offset: 0x0001781C
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmPrimitiveValue value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001962C File Offset: 0x0001782C
		public IEdmPrimitiveValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00019634 File Offset: 0x00017834
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Enum;
			}
		}

		// Token: 0x040004A7 RID: 1191
		private readonly IEdmPrimitiveValue value;
	}
}
