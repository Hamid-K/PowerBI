using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000625 RID: 1573
	internal sealed class OdbcQueryColumnInfo
	{
		// Token: 0x060031A4 RID: 12708 RVA: 0x000983C8 File Offset: 0x000965C8
		public OdbcQueryColumnInfo(string name, TypeValue typeValue, OdbcDerivedColumnTypeInfo typeInfo)
		{
			this.name = name;
			this.typeValue = typeValue;
			this.typeInfo = typeInfo;
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000983E5 File Offset: 0x000965E5
		public string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000983ED File Offset: 0x000965ED
		public TypeValue AscribedTypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000983F5 File Offset: 0x000965F5
		public OdbcDerivedColumnTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
		}

		// Token: 0x04001616 RID: 5654
		private readonly string name;

		// Token: 0x04001617 RID: 5655
		private readonly TypeValue typeValue;

		// Token: 0x04001618 RID: 5656
		private readonly OdbcDerivedColumnTypeInfo typeInfo;
	}
}
