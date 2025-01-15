using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059D RID: 1437
	[DataContract]
	internal sealed class JoinCondition
	{
		// Token: 0x060051F4 RID: 20980 RVA: 0x0015A3F4 File Offset: 0x001585F4
		internal JoinCondition(Expression primaryKey, Expression foreignKey)
		{
			this.m_primaryKey = primaryKey;
			this.m_foreignKey = foreignKey;
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0015A40A File Offset: 0x0015860A
		internal JoinCondition(Expression primaryKey, Expression foreignKey, SortDirection sortDirection)
			: this(primaryKey, foreignKey)
		{
			this.m_sortDirection = sortDirection;
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x060051F6 RID: 20982 RVA: 0x0015A41B File Offset: 0x0015861B
		internal Expression PrimaryKey
		{
			get
			{
				return this.m_primaryKey;
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x060051F7 RID: 20983 RVA: 0x0015A423 File Offset: 0x00158623
		internal Expression ForeignKey
		{
			get
			{
				return this.m_foreignKey;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x0015A42B File Offset: 0x0015862B
		internal SortDirection SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
		}

		// Token: 0x04002965 RID: 10597
		[DataMember(Name = "PrimaryKey", Order = 1)]
		private readonly Expression m_primaryKey;

		// Token: 0x04002966 RID: 10598
		[DataMember(Name = "ForeignKey", Order = 2)]
		private readonly Expression m_foreignKey;

		// Token: 0x04002967 RID: 10599
		[DataMember(Name = "SortDirection", Order = 3)]
		private readonly SortDirection m_sortDirection;
	}
}
