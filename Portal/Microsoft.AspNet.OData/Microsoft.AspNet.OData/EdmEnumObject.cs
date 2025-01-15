using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000023 RID: 35
	[NonValidatingParameterBinding]
	public class EdmEnumObject : IEdmEnumObject, IEdmObject
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005F4A File Offset: 0x0000414A
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005F52 File Offset: 0x00004152
		public string Value { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005F5B File Offset: 0x0000415B
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005F63 File Offset: 0x00004163
		public bool IsNullable { get; set; }

		// Token: 0x060000FC RID: 252 RVA: 0x00005F6C File Offset: 0x0000416C
		public EdmEnumObject(IEdmEnumType edmType, string value)
			: this(edmType, value, false)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005F77 File Offset: 0x00004177
		public EdmEnumObject(IEdmEnumTypeReference edmType, string value)
			: this(edmType.EnumDefinition(), value, edmType.IsNullable)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005F8C File Offset: 0x0000418C
		public EdmEnumObject(IEdmEnumType edmType, string value, bool isNullable)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			this._edmType = edmType;
			this.Value = value;
			this.IsNullable = isNullable;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005FB7 File Offset: 0x000041B7
		public IEdmTypeReference GetEdmType()
		{
			return new EdmEnumTypeReference(this._edmType as IEdmEnumType, this.IsNullable);
		}

		// Token: 0x04000037 RID: 55
		private readonly IEdmType _edmType;
	}
}
