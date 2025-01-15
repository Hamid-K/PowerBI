using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200048D RID: 1165
	internal sealed class ClrEnumType : EnumType
	{
		// Token: 0x060039BB RID: 14779 RVA: 0x000BE384 File Offset: 0x000BC584
		internal ClrEnumType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
			: base(clrType)
		{
			this._type = clrType;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000BE3A6 File Offset: 0x000BC5A6
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000BE3AE File Offset: 0x000BC5AE
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x04001346 RID: 4934
		private readonly Type _type;

		// Token: 0x04001347 RID: 4935
		private readonly string _cspaceTypeName;
	}
}
