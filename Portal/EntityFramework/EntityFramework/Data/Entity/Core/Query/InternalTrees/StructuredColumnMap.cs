using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F0 RID: 1008
	internal abstract class StructuredColumnMap : ColumnMap
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x00095768 File Offset: 0x00093968
		internal StructuredColumnMap(TypeUsage type, string name, ColumnMap[] properties)
			: base(type, name)
		{
			this.m_properties = properties;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x00095779 File Offset: 0x00093979
		internal virtual SimpleColumnMap NullSentinel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x0009577C File Offset: 0x0009397C
		internal ColumnMap[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00095784 File Offset: 0x00093984
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.Append("{");
			foreach (ColumnMap columnMap in this.Properties)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[] { text, columnMap });
				text = ",";
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000FE8 RID: 4072
		private readonly ColumnMap[] m_properties;
	}
}
