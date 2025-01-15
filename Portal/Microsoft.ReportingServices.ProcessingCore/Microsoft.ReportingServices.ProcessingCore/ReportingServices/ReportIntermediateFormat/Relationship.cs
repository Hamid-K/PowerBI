using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000401 RID: 1025
	[Serializable]
	internal abstract class Relationship : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000CB4AC File Offset: 0x000C96AC
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x000CB4B4 File Offset: 0x000C96B4
		internal bool NaturalJoin
		{
			get
			{
				return this.m_naturalJoin;
			}
			set
			{
				this.m_naturalJoin = value;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000CB4BD File Offset: 0x000C96BD
		internal DataSet RelatedDataSet
		{
			get
			{
				return this.m_relatedDataSet;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000CB4C5 File Offset: 0x000C96C5
		internal bool IsCrossJoin
		{
			get
			{
				return this.JoinConditionCount == 0;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000CB4D0 File Offset: 0x000C96D0
		internal int JoinConditionCount
		{
			get
			{
				if (this.m_joinConditions != null)
				{
					return this.m_joinConditions.Count;
				}
				return 0;
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000CB4E7 File Offset: 0x000C96E7
		internal void AddJoinCondition(ExpressionInfo foreignKey, ExpressionInfo primaryKey, SortDirection direction)
		{
			this.AddJoinCondition(new Relationship.JoinCondition(foreignKey, primaryKey, direction));
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000CB4F7 File Offset: 0x000C96F7
		internal void AddJoinCondition(Relationship.JoinCondition joinCondition)
		{
			if (this.m_joinConditions == null)
			{
				this.m_joinConditions = new List<Relationship.JoinCondition>();
			}
			this.m_joinConditions.Add(joinCondition);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000CB518 File Offset: 0x000C9718
		internal void JoinConditionInitialize(DataSet relatedDataSet, InitializationContext context)
		{
			for (int i = 0; i < this.JoinConditionCount; i++)
			{
				this.m_joinConditions[i].Initialize(relatedDataSet, this.m_naturalJoin, context);
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000CB550 File Offset: 0x000C9750
		internal void SetExprHost(IList<JoinConditionExprHost> joinConditionExprHost, ObjectModelImpl reportObjectModel)
		{
			for (int i = 0; i < this.JoinConditionCount; i++)
			{
				this.m_joinConditions[i].SetExprHost(joinConditionExprHost, reportObjectModel);
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000CB584 File Offset: 0x000C9784
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult[] EvaluateJoinConditionKeys(bool evaluatePrimaryKeys, Microsoft.ReportingServices.RdlExpressions.ReportRuntime reportRuntime)
		{
			int joinConditionCount = this.JoinConditionCount;
			if (joinConditionCount == 0)
			{
				return null;
			}
			Microsoft.ReportingServices.RdlExpressions.VariantResult[] array = new Microsoft.ReportingServices.RdlExpressions.VariantResult[joinConditionCount];
			for (int i = 0; i < joinConditionCount; i++)
			{
				if (evaluatePrimaryKeys)
				{
					array[i] = this.m_joinConditions[i].EvaluatePrimaryKeyExpr(reportRuntime);
				}
				else
				{
					array[i] = this.m_joinConditions[i].EvaluateForeignKeyExpr(reportRuntime);
				}
			}
			return array;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000CB5E8 File Offset: 0x000C97E8
		internal ExpressionInfo[] GetForeignKeyExpressions()
		{
			int joinConditionCount = this.JoinConditionCount;
			if (joinConditionCount == 0)
			{
				return null;
			}
			ExpressionInfo[] array = new ExpressionInfo[joinConditionCount];
			for (int i = 0; i < joinConditionCount; i++)
			{
				array[i] = this.m_joinConditions[i].ForeignKeyExpression;
			}
			return array;
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000CB62C File Offset: 0x000C982C
		internal SortDirection[] GetSortDirections()
		{
			if (this.JoinConditionCount == 0)
			{
				return null;
			}
			SortDirection[] array = new SortDirection[this.JoinConditionCount];
			for (int i = 0; i < this.JoinConditionCount; i++)
			{
				array[i] = this.m_joinConditions[i].SortDirection;
			}
			return array;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000CB678 File Offset: 0x000C9878
		internal bool TryMapFieldIndex(int primaryKeyFieldIndex, out int foreignKeyFieldIndex)
		{
			if (this.JoinConditionCount > 0)
			{
				foreach (Relationship.JoinCondition joinCondition in this.m_joinConditions)
				{
					if (joinCondition.PrimaryKeyExpression.Type == ExpressionInfo.Types.Field && joinCondition.ForeignKeyExpression.Type == ExpressionInfo.Types.Field && joinCondition.PrimaryKeyExpression.FieldIndex == primaryKeyFieldIndex)
					{
						foreignKeyFieldIndex = joinCondition.ForeignKeyExpression.FieldIndex;
						return true;
					}
				}
			}
			foreignKeyFieldIndex = -1;
			return false;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000CB710 File Offset: 0x000C9910
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Relationship, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.JoinConditions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinCondition),
				new MemberInfo(MemberName.NaturalJoin, Token.Boolean),
				new MemberInfo(MemberName.RelatedDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference)
			});
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000CB770 File Offset: 0x000C9970
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Relationship.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.RelatedDataSet)
				{
					if (memberName != MemberName.JoinConditions)
					{
						if (memberName != MemberName.NaturalJoin)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_naturalJoin);
						}
					}
					else
					{
						writer.Write<Relationship.JoinCondition>(this.m_joinConditions);
					}
				}
				else
				{
					writer.WriteReference(this.m_relatedDataSet);
				}
			}
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000CB7F4 File Offset: 0x000C99F4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Relationship.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.RelatedDataSet)
				{
					if (memberName != MemberName.JoinConditions)
					{
						if (memberName != MemberName.NaturalJoin)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_naturalJoin = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_joinConditions = reader.ReadGenericListOfRIFObjects<Relationship.JoinCondition>();
					}
				}
				else
				{
					this.m_relatedDataSet = reader.ReadReference<DataSet>(this);
				}
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000CB878 File Offset: 0x000C9A78
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Relationship.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.RelatedDataSet)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
						Global.Tracer.Assert(this.m_relatedDataSet != (DataSet)referenceableItems[memberReference.RefID]);
						this.m_relatedDataSet = (DataSet)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000CB968 File Offset: 0x000C9B68
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Relationship;
		}

		// Token: 0x040017D1 RID: 6097
		protected List<Relationship.JoinCondition> m_joinConditions;

		// Token: 0x040017D2 RID: 6098
		protected bool m_naturalJoin;

		// Token: 0x040017D3 RID: 6099
		protected DataSet m_relatedDataSet;

		// Token: 0x040017D4 RID: 6100
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Relationship.GetDeclaration();

		// Token: 0x02000967 RID: 2407
		internal sealed class JoinCondition : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x0600802D RID: 32813 RVA: 0x002109B9 File Offset: 0x0020EBB9
			internal JoinCondition()
			{
			}

			// Token: 0x0600802E RID: 32814 RVA: 0x002109C1 File Offset: 0x0020EBC1
			internal JoinCondition(ExpressionInfo foreignKey, ExpressionInfo primaryKey, SortDirection direction)
			{
				this.m_foreignKeyExpression = foreignKey;
				this.m_primaryKeyExpression = primaryKey;
				this.m_sortDirection = direction;
			}

			// Token: 0x17002981 RID: 10625
			// (get) Token: 0x0600802F RID: 32815 RVA: 0x002109DE File Offset: 0x0020EBDE
			internal ExpressionInfo ForeignKeyExpression
			{
				get
				{
					return this.m_foreignKeyExpression;
				}
			}

			// Token: 0x17002982 RID: 10626
			// (get) Token: 0x06008030 RID: 32816 RVA: 0x002109E6 File Offset: 0x0020EBE6
			internal ExpressionInfo PrimaryKeyExpression
			{
				get
				{
					return this.m_primaryKeyExpression;
				}
			}

			// Token: 0x17002983 RID: 10627
			// (get) Token: 0x06008031 RID: 32817 RVA: 0x002109EE File Offset: 0x0020EBEE
			internal SortDirection SortDirection
			{
				get
				{
					return this.m_sortDirection;
				}
			}

			// Token: 0x17002984 RID: 10628
			// (get) Token: 0x06008032 RID: 32818 RVA: 0x002109F6 File Offset: 0x0020EBF6
			internal JoinConditionExprHost ExprHost
			{
				get
				{
					return this.m_exprHost;
				}
			}

			// Token: 0x06008033 RID: 32819 RVA: 0x00210A00 File Offset: 0x0020EC00
			internal void Initialize(DataSet relatedDataSet, bool naturalJoin, InitializationContext context)
			{
				context.ExprHostBuilder.JoinConditionStart();
				if (this.m_foreignKeyExpression != null)
				{
					this.m_foreignKeyExpression.Initialize("ForeignKey", context);
					context.ExprHostBuilder.JoinConditionForeignKeyExpr(this.m_foreignKeyExpression);
				}
				if (this.m_primaryKeyExpression != null)
				{
					context.RegisterDataSet(relatedDataSet);
					this.m_primaryKeyExpression.Initialize("PrimaryKey", context);
					context.ExprHostBuilder.JoinConditionPrimaryKeyExpr(this.m_primaryKeyExpression);
					context.UnRegisterDataSet(relatedDataSet);
				}
				this.m_exprHostID = context.ExprHostBuilder.JoinConditionEnd();
			}

			// Token: 0x06008034 RID: 32820 RVA: 0x00210A94 File Offset: 0x0020EC94
			internal void SetExprHost(IList<JoinConditionExprHost> joinConditionExprHost, ObjectModelImpl reportObjectModel)
			{
				if (this.m_exprHostID >= 0)
				{
					Global.Tracer.Assert(joinConditionExprHost != null && reportObjectModel != null, "(joinConditionExprHost != null && reportObjectModel != null)");
					this.m_exprHost = joinConditionExprHost[this.m_exprHostID];
					this.m_exprHost.SetReportObjectModel(reportObjectModel);
				}
			}

			// Token: 0x06008035 RID: 32821 RVA: 0x00210AE1 File Offset: 0x0020ECE1
			internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateForeignKeyExpr(Microsoft.ReportingServices.RdlExpressions.ReportRuntime runtime)
			{
				return runtime.EvaluateJoinConditionForeignKeyExpression(this);
			}

			// Token: 0x06008036 RID: 32822 RVA: 0x00210AEA File Offset: 0x0020ECEA
			internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluatePrimaryKeyExpr(Microsoft.ReportingServices.RdlExpressions.ReportRuntime runtime)
			{
				return runtime.EvaluateJoinConditionPrimaryKeyExpression(this);
			}

			// Token: 0x06008037 RID: 32823 RVA: 0x00210AF4 File Offset: 0x0020ECF4
			internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinCondition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ForeignKeyExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
					new MemberInfo(MemberName.PrimaryKeyExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
					new MemberInfo(MemberName.ExprHostID, Token.Int32),
					new MemberInfo(MemberName.SortDirection, Token.Enum)
				});
			}

			// Token: 0x06008038 RID: 32824 RVA: 0x00210B64 File Offset: 0x0020ED64
			public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(Relationship.JoinCondition.m_Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName <= MemberName.SortDirection)
					{
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.SortDirection)
						{
							writer.WriteEnum((int)this.m_sortDirection);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ForeignKeyExpression)
						{
							writer.Write(this.m_foreignKeyExpression);
							continue;
						}
						if (memberName == MemberName.PrimaryKeyExpression)
						{
							writer.Write(this.m_primaryKeyExpression);
							continue;
						}
					}
					Global.Tracer.Assert(false);
				}
			}

			// Token: 0x06008039 RID: 32825 RVA: 0x00210C10 File Offset: 0x0020EE10
			public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(Relationship.JoinCondition.m_Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName <= MemberName.SortDirection)
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.SortDirection)
						{
							this.m_sortDirection = (SortDirection)reader.ReadEnum();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ForeignKeyExpression)
						{
							this.m_foreignKeyExpression = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.PrimaryKeyExpression)
						{
							this.m_primaryKeyExpression = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					Global.Tracer.Assert(false);
				}
			}

			// Token: 0x0600803A RID: 32826 RVA: 0x00210CC4 File Offset: 0x0020EEC4
			public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
			}

			// Token: 0x0600803B RID: 32827 RVA: 0x00210CC6 File Offset: 0x0020EEC6
			public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinCondition;
			}

			// Token: 0x0400409A RID: 16538
			private ExpressionInfo m_foreignKeyExpression;

			// Token: 0x0400409B RID: 16539
			private ExpressionInfo m_primaryKeyExpression;

			// Token: 0x0400409C RID: 16540
			private int m_exprHostID;

			// Token: 0x0400409D RID: 16541
			private SortDirection m_sortDirection;

			// Token: 0x0400409E RID: 16542
			[NonSerialized]
			private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Relationship.JoinCondition.GetDeclaration();

			// Token: 0x0400409F RID: 16543
			[NonSerialized]
			private JoinConditionExprHost m_exprHost;
		}
	}
}
