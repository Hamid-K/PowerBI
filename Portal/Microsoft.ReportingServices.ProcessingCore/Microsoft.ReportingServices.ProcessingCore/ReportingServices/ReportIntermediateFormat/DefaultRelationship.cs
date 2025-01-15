using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000403 RID: 1027
	[Serializable]
	internal sealed class DefaultRelationship : Relationship
	{
		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000CBCBB File Offset: 0x000C9EBB
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000CBCC3 File Offset: 0x000C9EC3
		internal string RelatedDataSetName
		{
			get
			{
				return this.m_relatedDataSetName;
			}
			set
			{
				this.m_relatedDataSetName = value;
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000CBCCC File Offset: 0x000C9ECC
		internal void Initialize(DataSet thisDataSet, InitializationContext context)
		{
			if (this.m_relatedDataSet == null)
			{
				Global.Tracer.Assert(context.ErrorContext.HasError, "Expected error not found. BindAndValidate should have been called before Initialize");
				return;
			}
			base.JoinConditionInitialize(this.m_relatedDataSet, context);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000CBD00 File Offset: 0x000C9F00
		internal void BindAndValidate(DataSet thisDataSet, ErrorContext errorContext, Report report)
		{
			if (this.m_joinConditions == null && !this.m_naturalJoin)
			{
				errorContext.Register(ProcessingErrorCode.rsMissingDefaultRelationshipJoinCondition, Severity.Error, thisDataSet.ObjectType, thisDataSet.Name, "DefaultRelationship", new string[] { this.m_relatedDataSetName });
				return;
			}
			if (!report.MappingNameToDataSet.TryGetValue(this.m_relatedDataSetName, out this.m_relatedDataSet))
			{
				errorContext.Register(ProcessingErrorCode.rsNonExistingRelationshipRelatedScope, Severity.Error, thisDataSet.ObjectType, thisDataSet.Name, "DefaultRelationship", new string[] { "RelatedDataSet", this.m_relatedDataSetName });
				return;
			}
			if (thisDataSet.ID == this.m_relatedDataSet.ID)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidSelfJoinRelationship, Severity.Error, thisDataSet.ObjectType, thisDataSet.Name, "DefaultRelationship", new string[] { "RelatedDataSet", this.m_relatedDataSetName });
			}
			if (!this.m_naturalJoin)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDefaultRelationshipNotNaturalJoin, Severity.Error, thisDataSet.ObjectType, thisDataSet.Name, "DefaultRelationship", new string[] { "NaturalJoin" });
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000CBE18 File Offset: 0x000CA018
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DefaultRelationship, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Relationship, list);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000CBE3B File Offset: 0x000CA03B
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DefaultRelationship.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000CBE73 File Offset: 0x000CA073
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DefaultRelationship.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000CBEAB File Offset: 0x000CA0AB
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DefaultRelationship;
		}

		// Token: 0x040017D7 RID: 6103
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DefaultRelationship.GetDeclaration();

		// Token: 0x040017D8 RID: 6104
		[NonSerialized]
		private string m_relatedDataSetName;
	}
}
