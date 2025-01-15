using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000084 RID: 132
	public sealed class EdmProperty : EdmMember
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001786E File Offset: 0x00015A6E
		internal EdmProperty(string name, TypeUsage typeUsage)
			: base(name, typeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001788F File Offset: 0x00015A8F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmProperty;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00017893 File Offset: 0x00015A93
		public bool Nullable
		{
			get
			{
				return (bool)base.TypeUsage.Facets["Nullable"].Value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000178B4 File Offset: 0x00015AB4
		public object DefaultValue
		{
			get
			{
				return base.TypeUsage.Facets["DefaultValue"].Value;
			}
		}
	}
}
