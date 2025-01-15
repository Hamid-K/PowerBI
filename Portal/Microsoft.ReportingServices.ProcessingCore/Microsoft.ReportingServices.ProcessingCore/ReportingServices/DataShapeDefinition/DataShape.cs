using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058C RID: 1420
	[DataContract]
	internal sealed class DataShape : DataItem
	{
		// Token: 0x0600519A RID: 20890 RVA: 0x00159CBF File Offset: 0x00157EBF
		internal DataShape(string id)
			: base(id)
		{
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00159CC8 File Offset: 0x00157EC8
		internal DataShape(string id, IEnumerable<Calculation> calculations, IEnumerable<DataShape> dataShapes, DataBinding dataBinding, IEnumerable<DataMember> primaryHierarchy, IEnumerable<DataMember> secondaryHierarchy, IEnumerable<DataRow> dataRows, int? requestedPrimaryLeafCount, IEnumerable<DataShapeMessage> dataShapeMessages, IEnumerable<Limit> limits, IEnumerable<RestartDefinition> restartDefinitions)
			: base(id, calculations, dataShapes)
		{
			this.m_dataBinding = dataBinding;
			this.m_primaryHierarchy = primaryHierarchy.ToReadOnlyCollection<DataMember>();
			this.m_secondaryHierarchy = secondaryHierarchy.ToReadOnlyCollection<DataMember>();
			this.m_dataRows = dataRows.ToReadOnlyCollection<DataRow>();
			this.m_requestedPrimaryLeafCount = requestedPrimaryLeafCount;
			this.m_dataShapeMessages = dataShapeMessages.ToReadOnlyCollection<DataShapeMessage>();
			this.m_limits = limits.ToReadOnlyCollection<Limit>();
			this.m_restartDefinitions = restartDefinitions;
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x0600519C RID: 20892 RVA: 0x00159D37 File Offset: 0x00157F37
		internal DataBinding DataBinding
		{
			get
			{
				return this.m_dataBinding;
			}
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x0600519D RID: 20893 RVA: 0x00159D3F File Offset: 0x00157F3F
		internal IEnumerable<DataMember> PrimaryHierarchy
		{
			get
			{
				return this.m_primaryHierarchy;
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x0600519E RID: 20894 RVA: 0x00159D47 File Offset: 0x00157F47
		internal IEnumerable<DataMember> SecondaryHierarchy
		{
			get
			{
				return this.m_secondaryHierarchy;
			}
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x00159D4F File Offset: 0x00157F4F
		internal IEnumerable<DataRow> DataRows
		{
			get
			{
				return this.m_dataRows;
			}
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x060051A0 RID: 20896 RVA: 0x00159D57 File Offset: 0x00157F57
		internal int? RequestedPrimaryLeafCount
		{
			get
			{
				return this.m_requestedPrimaryLeafCount;
			}
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x060051A1 RID: 20897 RVA: 0x00159D5F File Offset: 0x00157F5F
		internal IEnumerable<DataShapeMessage> DataShapeMessages
		{
			get
			{
				return this.m_dataShapeMessages;
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x060051A2 RID: 20898 RVA: 0x00159D67 File Offset: 0x00157F67
		internal IEnumerable<Limit> Limits
		{
			get
			{
				return this.m_limits;
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x060051A3 RID: 20899 RVA: 0x00159D6F File Offset: 0x00157F6F
		internal IEnumerable<RestartDefinition> RestartDefinitions
		{
			get
			{
				return this.m_restartDefinitions;
			}
		}

		// Token: 0x04002930 RID: 10544
		[DataMember(Name = "DataBinding", Order = 4)]
		private readonly DataBinding m_dataBinding;

		// Token: 0x04002931 RID: 10545
		[DataMember(Name = "PrimaryHierarchy", Order = 5)]
		private readonly IEnumerable<DataMember> m_primaryHierarchy;

		// Token: 0x04002932 RID: 10546
		[DataMember(Name = "SecondaryHierarchy", Order = 6)]
		private readonly IEnumerable<DataMember> m_secondaryHierarchy;

		// Token: 0x04002933 RID: 10547
		[DataMember(Name = "DataRows", Order = 7)]
		private readonly IEnumerable<DataRow> m_dataRows;

		// Token: 0x04002934 RID: 10548
		[DataMember(Name = "RequestedPrimaryLeafCount", Order = 8, EmitDefaultValue = false, IsRequired = false)]
		private readonly int? m_requestedPrimaryLeafCount;

		// Token: 0x04002935 RID: 10549
		[DataMember(Name = "DataShapeMessages", Order = 9, EmitDefaultValue = false, IsRequired = false)]
		private readonly IEnumerable<DataShapeMessage> m_dataShapeMessages;

		// Token: 0x04002936 RID: 10550
		[DataMember(Name = "Limits", Order = 10, EmitDefaultValue = false, IsRequired = false)]
		private readonly IEnumerable<Limit> m_limits;

		// Token: 0x04002937 RID: 10551
		[DataMember(Name = "RestartDefinitions", Order = 11, EmitDefaultValue = false, IsRequired = false)]
		private readonly IEnumerable<RestartDefinition> m_restartDefinitions;
	}
}
