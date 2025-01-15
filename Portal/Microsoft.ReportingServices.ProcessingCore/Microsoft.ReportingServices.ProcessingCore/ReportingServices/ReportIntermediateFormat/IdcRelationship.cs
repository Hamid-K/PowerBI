using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000402 RID: 1026
	[Serializable]
	internal sealed class IdcRelationship : Relationship
	{
		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000CB983 File Offset: 0x000C9B83
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000CB98B File Offset: 0x000C9B8B
		internal string ParentScope
		{
			get
			{
				return this.m_parentScope;
			}
			set
			{
				this.m_parentScope = value;
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000CB994 File Offset: 0x000C9B94
		internal void Initialize(InitializationContext context)
		{
			if (this.m_joinConditions == null)
			{
				return;
			}
			base.JoinConditionInitialize(this.m_relatedDataSet, context);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000CB9AC File Offset: 0x000C9BAC
		internal bool ValidateLinearRelationship(ErrorContext errorContext, DataSet parentDataSet)
		{
			this.m_relatedDataSet = parentDataSet;
			this.m_parentScope = null;
			return true;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000CB9C0 File Offset: 0x000C9BC0
		internal bool ValidateIntersectRelationship(ErrorContext errorContext, IRIFDataScope intersectScope, ScopeTree scopeTree)
		{
			IRIFDataScope parentRowScopeForIntersection = scopeTree.GetParentRowScopeForIntersection(intersectScope);
			IRIFDataScope parentColumnScopeForIntersection = scopeTree.GetParentColumnScopeForIntersection(intersectScope);
			if (ScopeTree.SameScope(this.m_parentScope, parentRowScopeForIntersection.Name))
			{
				this.m_relatedDataSet = parentRowScopeForIntersection.DataScopeInfo.DataSet;
				return true;
			}
			if (ScopeTree.SameScope(this.m_parentScope, parentColumnScopeForIntersection.Name))
			{
				this.m_relatedDataSet = parentColumnScopeForIntersection.DataScopeInfo.DataSet;
				return true;
			}
			IRIFDataScope parentDataRegion = scopeTree.GetParentDataRegion(intersectScope);
			errorContext.Register(ProcessingErrorCode.rsMissingIntersectionRelationshipParentScope, Severity.Error, intersectScope.DataScopeObjectType, parentDataRegion.Name, "ParentScope", new string[]
			{
				parentDataRegion.DataScopeObjectType.ToString(),
				parentRowScopeForIntersection.Name,
				parentColumnScopeForIntersection.Name
			});
			return false;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000CBA80 File Offset: 0x000C9C80
		internal void InsertAggregateIndicatorJoinCondition(Field field, int fieldIndex, Field aggregateIndicatorField, int aggregateIndicatorFieldIndex, InitializationContext context)
		{
			int num = -1;
			if (this.m_joinConditions != null)
			{
				for (int i = 0; i < this.m_joinConditions.Count; i++)
				{
					Relationship.JoinCondition joinCondition = this.m_joinConditions[i];
					if (joinCondition.ForeignKeyExpression.Type == ExpressionInfo.Types.Field)
					{
						if (joinCondition.ForeignKeyExpression.FieldIndex == fieldIndex)
						{
							num = i;
						}
						else if (joinCondition.ForeignKeyExpression.FieldIndex == aggregateIndicatorFieldIndex)
						{
							return;
						}
					}
				}
			}
			bool flag = num == -1;
			string text = this.FindRelatedAggregateIndicatorFieldName(field);
			if (text == null)
			{
				return;
			}
			ExpressionInfo expressionInfo = ExpressionInfo.CreateConstExpression(flag);
			ExpressionInfo expressionInfo2 = new ExpressionInfo();
			expressionInfo2.SetAsSimpleFieldReference(text);
			Relationship.JoinCondition joinCondition2 = new Relationship.JoinCondition(expressionInfo2, expressionInfo, SortDirection.Ascending);
			joinCondition2.Initialize(this.m_relatedDataSet, this.m_naturalJoin, context);
			if (flag)
			{
				base.AddJoinCondition(joinCondition2);
				return;
			}
			this.m_joinConditions.Insert(num, joinCondition2);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000CBB50 File Offset: 0x000C9D50
		private string FindRelatedAggregateIndicatorFieldName(Field field)
		{
			Field field2 = this.m_relatedDataSet.Fields.Where((Field f) => f.Name == field.Name).FirstOrDefault<Field>();
			if (field2 == null)
			{
				return null;
			}
			if (!field2.HasAggregateIndicatorField)
			{
				return null;
			}
			return this.m_relatedDataSet.Fields[field2.AggregateIndicatorFieldIndex].Name;
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000CBBB8 File Offset: 0x000C9DB8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IdcRelationship, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Relationship, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ParentScope, Token.String)
			});
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000CBBF0 File Offset: 0x000C9DF0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(IdcRelationship.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ParentScope)
				{
					writer.Write(this.m_parentScope);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000CBC48 File Offset: 0x000C9E48
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(IdcRelationship.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ParentScope)
				{
					this.m_parentScope = reader.ReadString();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000CBCA0 File Offset: 0x000C9EA0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IdcRelationship;
		}

		// Token: 0x040017D5 RID: 6101
		private string m_parentScope;

		// Token: 0x040017D6 RID: 6102
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = IdcRelationship.GetDeclaration();
	}
}
