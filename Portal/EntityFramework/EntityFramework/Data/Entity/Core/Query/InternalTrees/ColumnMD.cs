using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038C RID: 908
	internal class ColumnMD
	{
		// Token: 0x06002C13 RID: 11283 RVA: 0x0008EF5F File Offset: 0x0008D15F
		internal ColumnMD(string name, TypeUsage type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0008EF75 File Offset: 0x0008D175
		internal ColumnMD(EdmMember property)
			: this(property.Name, property.TypeUsage)
		{
			this.m_property = property;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x0008EF90 File Offset: 0x0008D190
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x0008EF98 File Offset: 0x0008D198
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x0008EFA0 File Offset: 0x0008D1A0
		internal bool IsNullable
		{
			get
			{
				return this.m_property == null || TypeSemantics.IsNullable(this.m_property);
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0008EFB7 File Offset: 0x0008D1B7
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x04000EEB RID: 3819
		private readonly string m_name;

		// Token: 0x04000EEC RID: 3820
		private readonly TypeUsage m_type;

		// Token: 0x04000EED RID: 3821
		private readonly EdmMember m_property;
	}
}
