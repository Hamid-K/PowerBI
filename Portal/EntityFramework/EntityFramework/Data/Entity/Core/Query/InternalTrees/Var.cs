using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F8 RID: 1016
	internal abstract class Var
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x00095C4F File Offset: 0x00093E4F
		internal Var(int id, VarType varType, TypeUsage type)
		{
			this._id = id;
			this._varType = varType;
			this._type = type;
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x00095C6C File Offset: 0x00093E6C
		internal int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x00095C74 File Offset: 0x00093E74
		internal VarType VarType
		{
			get
			{
				return this._varType;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x00095C7C File Offset: 0x00093E7C
		internal TypeUsage Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x00095C84 File Offset: 0x00093E84
		internal virtual bool TryGetName(out string name)
		{
			name = null;
			return false;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x00095C8A File Offset: 0x00093E8A
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { this.Id });
		}

		// Token: 0x04000FFE RID: 4094
		private readonly int _id;

		// Token: 0x04000FFF RID: 4095
		private readonly VarType _varType;

		// Token: 0x04001000 RID: 4096
		private readonly TypeUsage _type;
	}
}
