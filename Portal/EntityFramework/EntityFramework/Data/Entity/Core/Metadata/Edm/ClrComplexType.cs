using System;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000492 RID: 1170
	internal sealed class ClrComplexType : ComplexType
	{
		// Token: 0x060039D3 RID: 14803 RVA: 0x000BE6A8 File Offset: 0x000BC8A8
		internal ClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
			: base(Check.NotNull<Type>(clrType, "clrType").Name, clrType.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			this._type = clrType;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = clrType.IsAbstract();
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000BE700 File Offset: 0x000BC900
		internal static ClrComplexType CreateReadonlyClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
		{
			ClrComplexType clrComplexType = new ClrComplexType(clrType, cspaceNamespaceName, cspaceTypeName);
			clrComplexType.SetReadOnly();
			return clrComplexType;
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000BE710 File Offset: 0x000BC910
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000BE718 File Offset: 0x000BC918
		internal Func<object> Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._constructor, value, null);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000BE728 File Offset: 0x000BC928
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000BE730 File Offset: 0x000BC930
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x0400134E RID: 4942
		private readonly Type _type;

		// Token: 0x0400134F RID: 4943
		private Func<object> _constructor;

		// Token: 0x04001350 RID: 4944
		private readonly string _cspaceTypeName;
	}
}
