using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E7 RID: 999
	internal class SimplePolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x000954B7 File Offset: 0x000936B7
		internal SimplePolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap typeDiscriminator, Dictionary<object, TypedColumnMap> typeChoices)
			: base(type, name, baseTypeColumns)
		{
			this.m_typedColumnMap = typeChoices;
			this.m_typeDiscriminator = typeDiscriminator;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000954D2 File Offset: 0x000936D2
		internal SimpleColumnMap TypeDiscriminator
		{
			get
			{
				return this.m_typeDiscriminator;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000954DA File Offset: 0x000936DA
		internal Dictionary<object, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typedColumnMap;
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000954E2 File Offset: 0x000936E2
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000954EC File Offset: 0x000936EC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000954F8 File Offset: 0x000936F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "P{{TypeId={0}, ", new object[] { this.TypeDiscriminator });
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in this.TypeChoices)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}({1},{2})", new object[] { text, keyValuePair.Key, keyValuePair.Value });
				text = ",";
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000FD9 RID: 4057
		private readonly SimpleColumnMap m_typeDiscriminator;

		// Token: 0x04000FDA RID: 4058
		private readonly Dictionary<object, TypedColumnMap> m_typedColumnMap;
	}
}
