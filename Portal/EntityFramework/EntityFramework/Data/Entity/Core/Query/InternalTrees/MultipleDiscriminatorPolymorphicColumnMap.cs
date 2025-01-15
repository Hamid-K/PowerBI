using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B7 RID: 951
	internal class MultipleDiscriminatorPolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06002DB3 RID: 11699 RVA: 0x000921F0 File Offset: 0x000903F0
		internal MultipleDiscriminatorPolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap[] typeDiscriminators, Dictionary<EntityType, TypedColumnMap> typeChoices, Func<object[], EntityType> discriminate)
			: base(type, name, baseTypeColumns)
		{
			this.m_typeDiscriminators = typeDiscriminators;
			this.m_typeChoices = typeChoices;
			this.m_discriminate = discriminate;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00092213 File Offset: 0x00090413
		internal SimpleColumnMap[] TypeDiscriminators
		{
			get
			{
				return this.m_typeDiscriminators;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x0009221B File Offset: 0x0009041B
		internal Dictionary<EntityType, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typeChoices;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x00092223 File Offset: 0x00090423
		internal Func<object[], EntityType> Discriminate
		{
			get
			{
				return this.m_discriminate;
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0009222B File Offset: 0x0009042B
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00092235 File Offset: 0x00090435
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00092240 File Offset: 0x00090440
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "P{{TypeId=<{0}>, ", new object[] { StringUtil.ToCommaSeparatedString(this.TypeDiscriminators) });
			foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in this.TypeChoices)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}(<{1}>,{2})", new object[] { text, keyValuePair.Key, keyValuePair.Value });
				text = ",";
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04000F48 RID: 3912
		private readonly SimpleColumnMap[] m_typeDiscriminators;

		// Token: 0x04000F49 RID: 3913
		private readonly Dictionary<EntityType, TypedColumnMap> m_typeChoices;

		// Token: 0x04000F4A RID: 3914
		private readonly Func<object[], EntityType> m_discriminate;
	}
}
