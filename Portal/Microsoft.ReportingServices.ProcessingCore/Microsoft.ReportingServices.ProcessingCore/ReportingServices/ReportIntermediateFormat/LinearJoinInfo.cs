using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E1 RID: 1249
	internal sealed class LinearJoinInfo : JoinInfo
	{
		// Token: 0x06003EEC RID: 16108 RVA: 0x0010B31E File Offset: 0x0010951E
		public LinearJoinInfo()
		{
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x0010B326 File Offset: 0x00109526
		public LinearJoinInfo(IdcRelationship relationship)
			: base(relationship)
		{
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x0010B32F File Offset: 0x0010952F
		internal DataSet ParentDataSet
		{
			get
			{
				return this.m_parentDataSet;
			}
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0010B338 File Offset: 0x00109538
		internal override bool ValidateRelationships(ScopeTree scopeTree, ErrorContext errorContext, DataSet ourDataSet, ParentDataSetContainer parentDataSets, IRIFReportDataScope currentScope)
		{
			Global.Tracer.Assert(parentDataSets != null && parentDataSets.Count == 1, "LinearJoinInfo can only be used with exactly one parent data set");
			this.m_parentDataSet = parentDataSets.ParentDataSet;
			if (DataSet.AreEqualById(ourDataSet, this.m_parentDataSet))
			{
				return false;
			}
			bool flag = false;
			if (this.m_relationships != null)
			{
				foreach (IdcRelationship idcRelationship in this.m_relationships)
				{
					flag |= idcRelationship.ValidateLinearRelationship(errorContext, this.m_parentDataSet);
				}
			}
			if (!flag && !ourDataSet.HasDefaultRelationship(this.m_parentDataSet))
			{
				this.RegisterInvalidInnerDataSetNameError(errorContext, ourDataSet, currentScope);
				return false;
			}
			Relationship activeRelationship = this.GetActiveRelationship(ourDataSet);
			if (activeRelationship == null)
			{
				this.RegisterInvalidInnerDataSetNameError(errorContext, ourDataSet, currentScope);
				return false;
			}
			if (activeRelationship.IsCrossJoin && (!activeRelationship.NaturalJoin || LinearJoinInfo.ScopeHasParentGroups(currentScope, scopeTree)))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidNaturalCrossJoin, Severity.Error, currentScope.DataScopeObjectType, currentScope.Name, "JoinConditions", Array.Empty<string>());
				return false;
			}
			return true;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0010B454 File Offset: 0x00109654
		private static bool ScopeHasParentGroups(IRIFReportDataScope currentScope, ScopeTree scopeTree)
		{
			ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = (IRIFDataScope candidate) => candidate == currentScope || (candidate.DataScopeObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShapeMember && candidate.DataScopeObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping);
			return !scopeTree.Traverse(directedScopeTreeVisitor, currentScope);
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0010B48C File Offset: 0x0010968C
		private void RegisterInvalidInnerDataSetNameError(ErrorContext errorContext, DataSet ourDataSet, IRIFReportDataScope currentScope)
		{
			Severity severity = Severity.Error;
			if (currentScope is DataRegion)
			{
				severity = Severity.Warning;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidInnerDataSetName, severity, currentScope.DataScopeObjectType, currentScope.Name, "DataSetName", new string[]
			{
				this.m_parentDataSet.Name,
				ourDataSet.Name
			});
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0010B4E0 File Offset: 0x001096E0
		internal Relationship GetActiveRelationship(DataSet ourDataSet)
		{
			return base.GetActiveRelationship(ourDataSet, this.m_parentDataSet);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0010B4EF File Offset: 0x001096EF
		internal override void CheckContainerJoinForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope)
		{
			base.CheckContainerRelationshipForNaturalJoin(startScope, errorContext, scope, this.GetActiveRelationship(scope.DataScopeInfo.DataSet));
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0010B50C File Offset: 0x0010970C
		internal override void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope scope)
		{
			Relationship activeRelationship = this.GetActiveRelationship(scope.DataScopeInfo.DataSet);
			base.ValidateScopeRulesForIdcNaturalJoin(context, scope, activeRelationship);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0010B534 File Offset: 0x00109734
		internal override void AddMappedFieldIndices(List<int> parentFieldIndices, DataSet parentDataSet, DataSet ourDataSet, List<int> ourFieldIndices)
		{
			Global.Tracer.Assert(DataSet.AreEqualById(this.m_parentDataSet, parentDataSet), "Invalid parent data set");
			JoinInfo.AddMappedFieldIndices(this.GetActiveRelationship(ourDataSet), parentFieldIndices, ourFieldIndices);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0010B560 File Offset: 0x00109760
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearJoinInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ParentDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference)
			});
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x0010B59C File Offset: 0x0010979C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(LinearJoinInfo.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ParentDataSet)
				{
					writer.WriteReference(this.m_parentDataSet);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x0010B5F4 File Offset: 0x001097F4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LinearJoinInfo.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ParentDataSet)
				{
					this.m_parentDataSet = reader.ReadReference<DataSet>(this);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x0010B650 File Offset: 0x00109850
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(LinearJoinInfo.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.ParentDataSet)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
						this.m_parentDataSet = (DataSet)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x0010B714 File Offset: 0x00109914
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearJoinInfo;
		}

		// Token: 0x04001D1E RID: 7454
		[Reference]
		private DataSet m_parentDataSet;

		// Token: 0x04001D1F RID: 7455
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LinearJoinInfo.GetDeclaration();
	}
}
