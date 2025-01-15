using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005EE RID: 1518
	public struct FieldMetadata
	{
		// Token: 0x06004A39 RID: 19001 RVA: 0x001071D2 File Offset: 0x001053D2
		public FieldMetadata(int ordinal, EdmMember fieldType)
		{
			if (ordinal < 0)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			Check.NotNull<EdmMember>(fieldType, "fieldType");
			this._fieldType = fieldType;
			this._ordinal = ordinal;
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x001071FD File Offset: 0x001053FD
		public EdmMember FieldType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x00107205 File Offset: 0x00105405
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x04001A2C RID: 6700
		private readonly EdmMember _fieldType;

		// Token: 0x04001A2D RID: 6701
		private readonly int _ordinal;
	}
}
