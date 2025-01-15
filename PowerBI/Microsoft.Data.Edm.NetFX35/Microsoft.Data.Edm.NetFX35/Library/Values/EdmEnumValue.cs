using System;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x0200018F RID: 399
	public class EdmEnumValue : EdmValue, IEdmEnumValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00018386 File Offset: 0x00016586
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMember member)
			: this(type, member.Value)
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00018395 File Offset: 0x00016595
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmPrimitiveValue value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000183A5 File Offset: 0x000165A5
		public IEdmPrimitiveValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000183AD File Offset: 0x000165AD
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Enum;
			}
		}

		// Token: 0x04000455 RID: 1109
		private readonly IEdmPrimitiveValue value;
	}
}
