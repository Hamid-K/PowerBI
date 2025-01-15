using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E0 RID: 1248
	internal abstract class JoinInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003ED6 RID: 16086 RVA: 0x0010AF93 File Offset: 0x00109193
		public JoinInfo()
		{
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0010AF9B File Offset: 0x0010919B
		public JoinInfo(IdcRelationship relationship)
		{
			this.m_relationships = new List<IdcRelationship>(1);
			this.m_relationships.Add(relationship);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0010AFBB File Offset: 0x001091BB
		public JoinInfo(List<IdcRelationship> relationships)
		{
			this.m_relationships = relationships;
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x0010AFCA File Offset: 0x001091CA
		internal List<IdcRelationship> Relationships
		{
			get
			{
				return this.m_relationships;
			}
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0010AFD4 File Offset: 0x001091D4
		internal void SetJoinConditionExprHost(IList<JoinConditionExprHost> joinConditionExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_relationships != null)
			{
				foreach (IdcRelationship idcRelationship in this.m_relationships)
				{
					idcRelationship.SetExprHost(joinConditionExprHost, reportObjectModel);
				}
			}
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0010B030 File Offset: 0x00109230
		protected Relationship GetActiveRelationship(DataSet ourDataSet, DataSet parentDataSet)
		{
			if (this.m_relationships == null && (ourDataSet == null || ourDataSet.DefaultRelationships == null))
			{
				return null;
			}
			Relationship relationship = JoinInfo.FindActiveRelationship<IdcRelationship>(this.m_relationships, parentDataSet);
			if (relationship == null && ourDataSet != null)
			{
				relationship = ourDataSet.GetDefaultRelationship(parentDataSet);
			}
			return relationship;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0010B070 File Offset: 0x00109270
		internal static T FindActiveRelationship<T>(List<T> relationships, DataSet parentDataSet) where T : Relationship
		{
			if (relationships != null)
			{
				foreach (T t in relationships)
				{
					if (DataSet.AreEqualById(t.RelatedDataSet, parentDataSet))
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0010B0DC File Offset: 0x001092DC
		internal void Initialize(InitializationContext context)
		{
			if (this.m_relationships != null && 0 < this.m_relationships.Count)
			{
				foreach (IdcRelationship idcRelationship in this.m_relationships)
				{
					idcRelationship.Initialize(context);
				}
			}
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x0010B144 File Offset: 0x00109344
		internal JoinInfo PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "IDC does not support automatic subtotals");
			throw new InvalidOperationException();
		}

		// Token: 0x06003EDF RID: 16095
		internal abstract bool ValidateRelationships(ScopeTree scopeTree, ErrorContext errorContext, DataSet ourDataSet, ParentDataSetContainer parentDataSets, IRIFReportDataScope currentScope);

		// Token: 0x06003EE0 RID: 16096
		internal abstract void CheckContainerJoinForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope);

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0010B15C File Offset: 0x0010935C
		protected void CheckContainerRelationshipForNaturalJoin(IRIFDataScope startScope, ErrorContext errorContext, IRIFDataScope scope, Relationship outerRelationship)
		{
			if (outerRelationship != null && !outerRelationship.NaturalJoin)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidRelationshipContainerNotNaturalJoin, Severity.Error, startScope.DataScopeObjectType, startScope.Name, "Relationship", new string[]
				{
					scope.DataScopeObjectType.ToString(),
					scope.Name
				});
			}
		}

		// Token: 0x06003EE2 RID: 16098
		internal abstract void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope scope);

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0010B1BA File Offset: 0x001093BA
		protected void ValidateScopeRulesForIdcNaturalJoin(InitializationContext context, IRIFDataScope startScopeForValidation, Relationship relationship)
		{
			if (relationship != null && relationship.NaturalJoin)
			{
				context.ValidateScopeRulesForIdcNaturalJoin(startScopeForValidation);
			}
		}

		// Token: 0x06003EE4 RID: 16100
		internal abstract void AddMappedFieldIndices(List<int> parentFieldIndices, DataSet parentDataSet, DataSet ourDataSet, List<int> ourFieldIndices);

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0010B1D0 File Offset: 0x001093D0
		protected static void AddMappedFieldIndices(Relationship relationship, List<int> parentFieldIndices, List<int> ourFieldIndices)
		{
			foreach (int num in parentFieldIndices)
			{
				int num2;
				if (relationship.TryMapFieldIndex(num, out num2))
				{
					ourFieldIndices.Add(num2);
				}
			}
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0010B22C File Offset: 0x0010942C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Relationships, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IdcRelationship)
			});
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x0010B264 File Offset: 0x00109464
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(JoinInfo.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Relationships)
				{
					writer.Write<IdcRelationship>(this.m_relationships);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x0010B2B8 File Offset: 0x001094B8
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(JoinInfo.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Relationships)
				{
					this.m_relationships = reader.ReadGenericListOfRIFObjects<IdcRelationship>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x0010B309 File Offset: 0x00109509
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x0010B30B File Offset: 0x0010950B
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo;
		}

		// Token: 0x04001D1C RID: 7452
		protected List<IdcRelationship> m_relationships;

		// Token: 0x04001D1D RID: 7453
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = JoinInfo.GetDeclaration();
	}
}
