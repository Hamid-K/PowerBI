using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039B RID: 923
	internal class DiscriminatedEntityIdentity : EntityIdentity
	{
		// Token: 0x06002CEB RID: 11499 RVA: 0x00090290 File Offset: 0x0008E490
		internal DiscriminatedEntityIdentity(SimpleColumnMap entitySetColumn, EntitySet[] entitySetMap, SimpleColumnMap[] keyColumns)
			: base(keyColumns)
		{
			this.m_entitySetColumn = entitySetColumn;
			this.m_entitySetMap = entitySetMap;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000902A7 File Offset: 0x0008E4A7
		internal SimpleColumnMap EntitySetColumnMap
		{
			get
			{
				return this.m_entitySetColumn;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000902AF File Offset: 0x0008E4AF
		internal EntitySet[] EntitySetMap
		{
			get
			{
				return this.m_entitySetMap;
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000902B8 File Offset: 0x0008E4B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[(Keys={", new object[0]);
			foreach (SimpleColumnMap simpleColumnMap in base.Keys)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[] { text, simpleColumnMap });
				text = ",";
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "})]", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x04000F16 RID: 3862
		private readonly SimpleColumnMap m_entitySetColumn;

		// Token: 0x04000F17 RID: 3863
		private readonly EntitySet[] m_entitySetMap;
	}
}
