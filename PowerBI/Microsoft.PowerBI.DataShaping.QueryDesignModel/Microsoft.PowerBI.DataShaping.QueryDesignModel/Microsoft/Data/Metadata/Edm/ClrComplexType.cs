using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007C RID: 124
	internal sealed class ClrComplexType : ComplexType
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x000157DC File Offset: 0x000139DC
		internal ClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
			: base(EntityUtil.GenericCheckArgumentNull<Type>(clrType, "clrType").Name, clrType.Namespace ?? string.Empty, DataSpace.OSpace)
		{
			this._type = clrType.TypeHandle;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = clrType.IsAbstract;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00015839 File Offset: 0x00013A39
		internal static ClrComplexType CreateReadonlyClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
		{
			ClrComplexType clrComplexType = new ClrComplexType(clrType, cspaceNamespaceName, cspaceTypeName);
			clrComplexType.SetReadOnly();
			return clrComplexType;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00015849 File Offset: 0x00013A49
		internal override Type ClrType
		{
			get
			{
				return Type.GetTypeFromHandle(this._type);
			}
		}

		// Token: 0x04000766 RID: 1894
		private readonly RuntimeTypeHandle _type;

		// Token: 0x04000767 RID: 1895
		private readonly string _cspaceTypeName;
	}
}
