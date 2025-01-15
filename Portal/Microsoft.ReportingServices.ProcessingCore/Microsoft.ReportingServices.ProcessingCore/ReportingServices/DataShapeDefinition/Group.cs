using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059C RID: 1436
	[DataContract]
	internal sealed class Group
	{
		// Token: 0x060051ED RID: 20973 RVA: 0x0015A390 File Offset: 0x00158590
		internal Group(ScopeIdDefinition scopeIdDefinition, bool naturalGroup)
		{
			this.m_scopeIdDefinition = scopeIdDefinition;
			this.m_naturalGroup = naturalGroup;
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0015A3A6 File Offset: 0x001585A6
		internal Group(ScopeIdDefinition scopeIdDefinition, bool naturalGroup, DataBinding dataBinding, IEnumerable<Expression> groupExpressions, IList<object> startPositions)
			: this(scopeIdDefinition, naturalGroup)
		{
			this.m_dataBinding = dataBinding;
			this.m_groupExpressions = groupExpressions.ToReadOnlyCollection<Expression>();
			this.m_startPositions = startPositions;
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x0015A3CC File Offset: 0x001585CC
		internal DataBinding DataBinding
		{
			get
			{
				return this.m_dataBinding;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x0015A3D4 File Offset: 0x001585D4
		internal ScopeIdDefinition ScopeIdDefinition
		{
			get
			{
				return this.m_scopeIdDefinition;
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0015A3DC File Offset: 0x001585DC
		internal bool NaturalGroup
		{
			get
			{
				return this.m_naturalGroup;
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x060051F2 RID: 20978 RVA: 0x0015A3E4 File Offset: 0x001585E4
		internal IList<object> StartPositions
		{
			get
			{
				return this.m_startPositions;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x060051F3 RID: 20979 RVA: 0x0015A3EC File Offset: 0x001585EC
		internal IEnumerable<Expression> GroupExpressions
		{
			get
			{
				return this.m_groupExpressions;
			}
		}

		// Token: 0x04002960 RID: 10592
		[DataMember(Name = "DataBinding", Order = 1)]
		private readonly DataBinding m_dataBinding;

		// Token: 0x04002961 RID: 10593
		[DataMember(Name = "ScopeIdDefinition", Order = 2)]
		private readonly ScopeIdDefinition m_scopeIdDefinition;

		// Token: 0x04002962 RID: 10594
		[DataMember(Name = "NaturalGroup", Order = 3)]
		private readonly bool m_naturalGroup;

		// Token: 0x04002963 RID: 10595
		[DataMember(Name = "StartPositions", Order = 4, EmitDefaultValue = false, IsRequired = false)]
		private readonly IList<object> m_startPositions;

		// Token: 0x04002964 RID: 10596
		[DataMember(Name = "GroupExpressions", Order = 5)]
		private readonly IEnumerable<Expression> m_groupExpressions;
	}
}
