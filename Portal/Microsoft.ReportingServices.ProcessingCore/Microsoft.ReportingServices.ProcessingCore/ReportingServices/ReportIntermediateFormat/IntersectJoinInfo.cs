using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E2 RID: 1250
	internal sealed class IntersectJoinInfo : JoinInfo
	{
		// Token: 0x06003EFC RID: 16124 RVA: 0x0010B727 File Offset: 0x00109927
		public IntersectJoinInfo()
		{
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0010B72F File Offset: 0x0010992F
		public IntersectJoinInfo(List<IdcRelationship> relationships)
			: base(relationships)
		{
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x0010B738 File Offset: 0x00109938
		internal DataSet RowParentDataSet
		{
			get
			{
				return this.m_rowParentDataSet;
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06003EFF RID: 16127 RVA: 0x0010B740 File Offset: 0x00109940
		internal DataSet ColumnParentDataSet
		{
			get
			{
				return this.m_columnParentDataSet;
			}
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0010B748 File Offset: 0x00109948
		internal Relationship GetActiveRowRelationship(DataSet ourDataSet)
		{
			return base.GetActiveRelationship(ourDataSet, this.m_rowParentDataSet);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0010B757 File Offset: 0x00109957
		internal Relationship GetActiveColumnRelationship(DataSet ourDataSet)
		{
			return base.GetActiveRelationship(ourDataSet, this.m_columnParentDataSet);
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0010B768 File Offset: 0x00109968
		internal override bool ValidateRelationships(ScopeTree scopeTree, ErrorContext errorContext, DataSet ourDataSet, ParentDataSetContainer parentDataSets, IRIFReportDataScope currentScope)
		{
			Global.Tracer.Assert(parentDataSets != null, "IntersectJoinInfo can only be used with one or two parent data sets");
			if (parentDataSets.Count == 1)
			{
				DataRegion parentDataRegion = scopeTree.GetParentDataRegion(currentScope);
				errorContext.Register(ProcessingErrorCode.rsUnexpectedCellDataSetName, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion.Name, "DataSetName", new string[] { parentDataRegion.ObjectType.ToString() });
				return false;
			}
			if (parentDataSets.AreAllSameDataSet() && DataSet.AreEqualById(parentDataSets.RowParentDataSet, ourDataSet))
			{
				return false;
			}
			this.m_rowParentDataSet = parentDataSets.RowParentDataSet;
			this.m_columnParentDataSet = parentDataSets.ColumnParentDataSet;
			if (this.m_rowParentDataSet == null || this.m_columnParentDataSet == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			if (this.m_relationships != null)
			{
				foreach (IdcRelationship idcRelationship in this.m_relationships)
				{
					if (!idcRelationship.ValidateIntersectRelationship(errorContext, currentScope, scopeTree))
					{
						return false;
					}
					this.CheckRelationshipDataSetBinding(scopeTree, errorContext, currentScope, idcRelationship, this.m_rowParentDataSet, ref flag);
					this.CheckRelationshipDataSetBinding(scopeTree, errorContext, currentScope, idcRelationship, this.m_columnParentDataSet, ref flag2);
				}
			}
			flag = this.HasRelationshipOrDefaultForDataSet(scopeTree, errorContext, currentScope, ourDataSet, this.m_rowParentDataSet, flag);
			flag2 = this.HasRelationshipOrDefaultForDataSet(scopeTree, errorContext, currentScope, ourDataSet, this.m_columnParentDataSet, flag2);
			if (!flag || !flag2)
			{
				return false;
			}
			DataRegion parentDataRegion2 = scopeTree.GetParentDataRegion(currentScope);
			if (!this.ValidateCellBoundTotheSameDataSetAsParentScpoe(this.m_columnParentDataSet, this.m_rowParentDataSet, ourDataSet, parentDataRegion2.IsColumnGroupingSwitched))
			{
				return true;
			}
			IRIFDataScope parentDataRegion3 = scopeTree.GetParentDataRegion(currentScope);
			IRIFDataScope parentRowScopeForIntersection = scopeTree.GetParentRowScopeForIntersection(currentScope);
			IRIFDataScope parentColumnScopeForIntersection = scopeTree.GetParentColumnScopeForIntersection(currentScope);
			if (parentDataRegion2.IsColumnGroupingSwitched)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidIntersectionNaturalJoin, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion3.Name, "ParentScope", new string[]
				{
					parentDataRegion3.DataScopeObjectType.ToString(),
					parentColumnScopeForIntersection.Name,
					parentRowScopeForIntersection.Name
				});
				return false;
			}
			errorContext.Register(ProcessingErrorCode.rsInvalidIntersectionNaturalJoin, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion3.Name, "ParentScope", new string[]
			{
				parentDataRegion3.DataScopeObjectType.ToString(),
				parentRowScopeForIntersection.Name,
				parentColumnScopeForIntersection.Name
			});
			return false;
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0010B9D8 File Offset: 0x00109BD8
		private bool ValidateCellBoundTotheSameDataSetAsParentScpoe(DataSet columnParentDataSet, DataSet rowParentDataSet, DataSet ourDataSet, bool isColumnGroupingSwitched)
		{
			if (isColumnGroupingSwitched)
			{
				return DataSet.AreEqualById(this.m_rowParentDataSet, ourDataSet) && this.GetActiveColumnRelationship(ourDataSet).NaturalJoin;
			}
			return DataSet.AreEqualById(this.m_columnParentDataSet, ourDataSet) && this.GetActiveRowRelationship(ourDataSet).NaturalJoin;
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0010BA18 File Offset: 0x00109C18
		private bool HasRelationshipOrDefaultForDataSet(ScopeTree scopeTree, ErrorContext errorContext, IRIFReportDataScope currentScope, DataSet ourDataSet, DataSet parentDataSet, bool hasValidRelationship)
		{
			if (DataSet.AreEqualById(parentDataSet, ourDataSet))
			{
				return true;
			}
			if (!hasValidRelationship && !ourDataSet.HasDefaultRelationship(parentDataSet))
			{
				IntersectJoinInfo.RegisterInvalidCellDataSetNameError(scopeTree, errorContext, currentScope, ourDataSet, parentDataSet);
				return false;
			}
			Relationship activeRelationship = base.GetActiveRelationship(ourDataSet, parentDataSet);
			if (activeRelationship == null)
			{
				IntersectJoinInfo.RegisterInvalidCellDataSetNameError(scopeTree, errorContext, currentScope, ourDataSet, parentDataSet);
				return false;
			}
			if (activeRelationship.IsCrossJoin)
			{
				DataRegion parentDataRegion = scopeTree.GetParentDataRegion(currentScope);
				errorContext.Register(ProcessingErrorCode.rsInvalidIntersectionNaturalCrossJoin, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion.Name, "JoinConditions", new string[] { parentDataRegion.ObjectType.ToString() });
				return false;
			}
			return true;
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0010BABC File Offset: 0x00109CBC
		private static void RegisterInvalidCellDataSetNameError(ScopeTree scopeTree, ErrorContext errorContext, IRIFReportDataScope currentScope, DataSet ourDataSet, DataSet parentDataSet)
		{
			DataRegion parentDataRegion = scopeTree.GetParentDataRegion(currentScope);
			errorContext.Register(ProcessingErrorCode.rsInvalidCellDataSetName, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion.Name, "DataSetName", new string[]
			{
				parentDataSet.Name,
				ourDataSet.Name,
				parentDataRegion.ObjectType.ToString()
			});
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0010BB20 File Offset: 0x00109D20
		private void CheckRelationshipDataSetBinding(ScopeTree scopeTree, ErrorContext errorContext, IRIFReportDataScope currentScope, IdcRelationship relationship, DataSet parentDataSetCandidate, ref bool dataSetAlreadyHasRelationship)
		{
			if (DataSet.AreEqualById(relationship.RelatedDataSet, parentDataSetCandidate))
			{
				if (dataSetAlreadyHasRelationship)
				{
					IRIFDataScope parentDataRegion = scopeTree.GetParentDataRegion(currentScope);
					IRIFDataScope parentRowScopeForIntersection = scopeTree.GetParentRowScopeForIntersection(currentScope);
					IRIFDataScope parentColumnScopeForIntersection = scopeTree.GetParentColumnScopeForIntersection(currentScope);
					errorContext.Register(ProcessingErrorCode.rsInvalidRelationshipDuplicateParentScope, Severity.Error, currentScope.DataScopeObjectType, parentDataRegion.Name, "ParentScope", new string[]
					{
						parentDataRegion.DataScopeObjectType.ToString(),
						parentRowScopeForIntersection.Name,
						parentColumnScopeForIntersection.Name
					});
				}
				dataSetAlreadyHasRelationship = true;
			}
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0010BBAC File Offset: 0x00109DAC
		internal override void CheckContainerJoinForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope)
		{
			DataSet dataSet = scope.DataScopeInfo.DataSet;
			base.CheckContainerRelationshipForNaturalJoin(startScope, errorContext, scope, this.GetActiveRowRelationship(dataSet));
			base.CheckContainerRelationshipForNaturalJoin(startScope, errorContext, scope, this.GetActiveColumnRelationship(dataSet));
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0010BBE8 File Offset: 0x00109DE8
		internal override void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope scope)
		{
			DataSet dataSet = scope.DataScopeInfo.DataSet;
			base.ValidateScopeRulesForIdcNaturalJoin(context, context.ScopeTree.GetParentRowScopeForIntersection(scope), this.GetActiveRowRelationship(dataSet));
			base.ValidateScopeRulesForIdcNaturalJoin(context, context.ScopeTree.GetParentColumnScopeForIntersection(scope), this.GetActiveColumnRelationship(dataSet));
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0010BC38 File Offset: 0x00109E38
		internal override void AddMappedFieldIndices(List<int> parentFieldIndices, DataSet parentDataSet, DataSet ourDataSet, List<int> ourFieldIndices)
		{
			Relationship relationship;
			if (DataSet.AreEqualById(this.m_rowParentDataSet, parentDataSet))
			{
				relationship = this.GetActiveRowRelationship(ourDataSet);
			}
			else if (DataSet.AreEqualById(this.m_columnParentDataSet, parentDataSet))
			{
				relationship = this.GetActiveColumnRelationship(ourDataSet);
			}
			else
			{
				Global.Tracer.Assert(false, "Invalid parent data set");
				relationship = null;
			}
			JoinInfo.AddMappedFieldIndices(relationship, parentFieldIndices, ourFieldIndices);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0010BC90 File Offset: 0x00109E90
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IntersectJoinInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RowParentDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference),
				new MemberInfo(MemberName.ColumnParentDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference)
			});
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0010BCE0 File Offset: 0x00109EE0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(IntersectJoinInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.RowParentDataSet)
				{
					if (memberName != MemberName.ColumnParentDataSet)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteReference(this.m_columnParentDataSet);
					}
				}
				else
				{
					writer.WriteReference(this.m_rowParentDataSet);
				}
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0010BD54 File Offset: 0x00109F54
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(IntersectJoinInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.RowParentDataSet)
				{
					if (memberName != MemberName.ColumnParentDataSet)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_columnParentDataSet = reader.ReadReference<DataSet>(this);
					}
				}
				else
				{
					this.m_rowParentDataSet = reader.ReadReference<DataSet>(this);
				}
			}
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0010BDCC File Offset: 0x00109FCC
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(IntersectJoinInfo.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.RowParentDataSet)
					{
						if (memberName != MemberName.ColumnParentDataSet)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
							this.m_columnParentDataSet = (DataSet)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
						this.m_rowParentDataSet = (DataSet)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0010BEF4 File Offset: 0x0010A0F4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IntersectJoinInfo;
		}

		// Token: 0x04001D20 RID: 7456
		[Reference]
		private DataSet m_rowParentDataSet;

		// Token: 0x04001D21 RID: 7457
		[Reference]
		private DataSet m_columnParentDataSet;

		// Token: 0x04001D22 RID: 7458
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = IntersectJoinInfo.GetDeclaration();
	}
}
