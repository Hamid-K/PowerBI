using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C7 RID: 967
	internal class OpCopier : BasicOpVisitorOfNode
	{
		// Token: 0x06002E36 RID: 11830 RVA: 0x00093C80 File Offset: 0x00091E80
		internal static Node Copy(Command cmd, Node n)
		{
			VarMap varMap;
			return OpCopier.Copy(cmd, n, out varMap);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x00093C98 File Offset: 0x00091E98
		internal static Node Copy(Command cmd, Node node, VarList varList, out VarList newVarList)
		{
			VarMap varMap;
			Node node2 = OpCopier.Copy(cmd, node, out varMap);
			newVarList = Command.CreateVarList();
			foreach (Var var in varList)
			{
				Var var2 = varMap[var];
				newVarList.Add(var2);
			}
			return node2;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00093D04 File Offset: 0x00091F04
		internal static Node Copy(Command cmd, Node n, out VarMap varMap)
		{
			OpCopier opCopier = new OpCopier(cmd);
			Node node = opCopier.CopyNode(n);
			varMap = opCopier.m_varMap;
			return node;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x00093D27 File Offset: 0x00091F27
		internal static List<SortKey> Copy(Command cmd, List<SortKey> sortKeys)
		{
			return new OpCopier(cmd).Copy(sortKeys);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x00093D35 File Offset: 0x00091F35
		protected OpCopier(Command cmd)
			: this(cmd, cmd)
		{
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x00093D3F File Offset: 0x00091F3F
		private OpCopier(Command destCommand, Command sourceCommand)
		{
			this.m_srcCmd = sourceCommand;
			this.m_destCmd = destCommand;
			this.m_varMap = new VarMap();
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x00093D60 File Offset: 0x00091F60
		private Var GetMappedVar(Var v)
		{
			Var var;
			if (this.m_varMap.TryGetValue(v, out var))
			{
				return var;
			}
			if (this.m_destCmd != this.m_srcCmd)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownVar, 6, null);
			}
			return v;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00093D9B File Offset: 0x00091F9B
		private void SetMappedVar(Var v, Var mappedVar)
		{
			this.m_varMap.Add(v, mappedVar);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x00093DAC File Offset: 0x00091FAC
		private void MapTable(Table newTable, Table oldTable)
		{
			for (int i = 0; i < oldTable.Columns.Count; i++)
			{
				this.SetMappedVar(oldTable.Columns[i], newTable.Columns[i]);
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x00093DED File Offset: 0x00091FED
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var var in vars)
			{
				Var mappedVar = this.GetMappedVar(var);
				yield return mappedVar;
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00093E04 File Offset: 0x00092004
		private VarVec Copy(VarVec vars)
		{
			return this.m_destCmd.CreateVarVec(this.MapVars(vars));
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x00093E18 File Offset: 0x00092018
		private VarList Copy(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x00093E26 File Offset: 0x00092026
		private SortKey Copy(SortKey sortKey)
		{
			return Command.CreateSortKey(this.GetMappedVar(sortKey.Var), sortKey.AscendingSort, sortKey.Collation);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x00093E48 File Offset: 0x00092048
		private List<SortKey> Copy(List<SortKey> sortKeys)
		{
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in sortKeys)
			{
				list.Add(this.Copy(sortKey));
			}
			return list;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x00093EA4 File Offset: 0x000920A4
		protected Node CopyNode(Node n)
		{
			return n.Op.Accept<Node>(this, n);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x00093EB4 File Offset: 0x000920B4
		private List<Node> ProcessChildren(Node n)
		{
			List<Node> list = new List<Node>();
			foreach (Node node in n.Children)
			{
				list.Add(this.CopyNode(node));
			}
			return list;
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x00093F14 File Offset: 0x00092114
		private Node CopyDefault(Op op, Node original)
		{
			return this.m_destCmd.CreateNode(op, this.ProcessChildren(original));
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x00093F29 File Offset: 0x00092129
		public override Node Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x00093F40 File Offset: 0x00092140
		public override Node Visit(ConstantOp op, Node n)
		{
			ConstantBaseOp constantBaseOp = this.m_destCmd.CreateConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(constantBaseOp);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00093F71 File Offset: 0x00092171
		public override Node Visit(NullOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateNullOp(op.Type));
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x00093F8F File Offset: 0x0009218F
		public override Node Visit(ConstantPredicateOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateConstantPredicateOp(op.Value));
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x00093FB0 File Offset: 0x000921B0
		public override Node Visit(InternalConstantOp op, Node n)
		{
			InternalConstantOp internalConstantOp = this.m_destCmd.CreateInternalConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(internalConstantOp);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x00093FE4 File Offset: 0x000921E4
		public override Node Visit(NullSentinelOp op, Node n)
		{
			NullSentinelOp nullSentinelOp = this.m_destCmd.CreateNullSentinelOp();
			return this.m_destCmd.CreateNode(nullSentinelOp);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x00094009 File Offset: 0x00092209
		public override Node Visit(FunctionOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFunctionOp(op.Function), n);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00094023 File Offset: 0x00092223
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreatePropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x0009403D File Offset: 0x0009223D
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRelPropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x00094057 File Offset: 0x00092257
		public override Node Visit(CaseOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCaseOp(op.Type), n);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x00094071 File Offset: 0x00092271
		public override Node Visit(ComparisonOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateComparisonOp(op.OpType, op.UseDatabaseNullSemantics), n);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x00094091 File Offset: 0x00092291
		public override Node Visit(LikeOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLikeOp(), n);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000940A5 File Offset: 0x000922A5
		public override Node Visit(AggregateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateAggregateOp(op.AggFunc, op.IsDistinctAggregate), n);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000940C5 File Offset: 0x000922C5
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewInstanceOp(op.Type), n);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000940E0 File Offset: 0x000922E0
		public override Node Visit(NewEntityOp op, Node n)
		{
			NewEntityOp newEntityOp;
			if (op.Scoped)
			{
				newEntityOp = this.m_destCmd.CreateScopedNewEntityOp(op.Type, op.RelationshipProperties, op.EntitySet);
			}
			else
			{
				newEntityOp = this.m_destCmd.CreateNewEntityOp(op.Type, op.RelationshipProperties);
			}
			return this.CopyDefault(newEntityOp, n);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x00094135 File Offset: 0x00092335
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDiscriminatedNewEntityOp(op.Type, op.DiscriminatorMap, op.EntitySet, op.RelationshipProperties), n);
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x00094161 File Offset: 0x00092361
		public override Node Visit(NewMultisetOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewMultisetOp(op.Type), n);
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x0009417B File Offset: 0x0009237B
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewRecordOp(op.Type), n);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x00094195 File Offset: 0x00092395
		public override Node Visit(RefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRefOp(op.EntitySet, op.Type), n);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000941B8 File Offset: 0x000923B8
		public override Node Visit(VarRefOp op, Node n)
		{
			Var var;
			if (!this.m_varMap.TryGetValue(op.Var, out var))
			{
				var = op.Var;
			}
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarRefOp(var));
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000941F8 File Offset: 0x000923F8
		public override Node Visit(ConditionalOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateConditionalOp(op.OpType), n);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x00094212 File Offset: 0x00092412
		public override Node Visit(ArithmeticOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateArithmeticOp(op.OpType, op.Type), n);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x00094234 File Offset: 0x00092434
		public override Node Visit(TreatOp op, Node n)
		{
			TreatOp treatOp = (op.IsFakeTreat ? this.m_destCmd.CreateFakeTreatOp(op.Type) : this.m_destCmd.CreateTreatOp(op.Type));
			return this.CopyDefault(treatOp, n);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x00094276 File Offset: 0x00092476
		public override Node Visit(CastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCastOp(op.Type), n);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00094290 File Offset: 0x00092490
		public override Node Visit(SoftCastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSoftCastOp(op.Type), n);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000942AA File Offset: 0x000924AA
		public override Node Visit(DerefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDerefOp(op.Type), n);
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000942C4 File Offset: 0x000924C4
		public override Node Visit(NavigateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNavigateOp(op.Type, op.RelProperty), n);
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000942E4 File Offset: 0x000924E4
		public override Node Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.CopyDefault(this.m_destCmd.CreateIsOfOnlyOp(op.IsOfType), n);
			}
			return this.CopyDefault(this.m_destCmd.CreateIsOfOp(op.IsOfType), n);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x0009431F File Offset: 0x0009251F
		public override Node Visit(ExistsOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateExistsOp(), n);
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x00094333 File Offset: 0x00092533
		public override Node Visit(ElementOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateElementOp(op.Type), n);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0009434D File Offset: 0x0009254D
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetRefKeyOp(op.Type), n);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x00094367 File Offset: 0x00092567
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetEntityRefOp(op.Type), n);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x00094381 File Offset: 0x00092581
		public override Node Visit(CollectOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCollectOp(op.Type), n);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x0009439C File Offset: 0x0009259C
		public override Node Visit(ScanTableOp op, Node n)
		{
			ScanTableOp scanTableOp = this.m_destCmd.CreateScanTableOp(op.Table.TableMetadata);
			this.MapTable(scanTableOp.Table, op.Table);
			return this.m_destCmd.CreateNode(scanTableOp);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000943E0 File Offset: 0x000925E0
		public override Node Visit(ScanViewOp op, Node n)
		{
			ScanViewOp scanViewOp = this.m_destCmd.CreateScanViewOp(op.Table.TableMetadata);
			this.MapTable(scanViewOp.Table, op.Table);
			List<Node> list = this.ProcessChildren(n);
			return this.m_destCmd.CreateNode(scanViewOp, list);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0009442C File Offset: 0x0009262C
		public override Node Visit(UnnestOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			Var mappedVar = this.GetMappedVar(op.Var);
			Table table = this.m_destCmd.CreateTableInstance(op.Table.TableMetadata);
			UnnestOp unnestOp = this.m_destCmd.CreateUnnestOp(mappedVar, table);
			this.MapTable(unnestOp.Table, op.Table);
			return this.m_destCmd.CreateNode(unnestOp, list);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x00094494 File Offset: 0x00092694
		public override Node Visit(ProjectOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			VarVec varVec = this.Copy(op.Outputs);
			ProjectOp projectOp = this.m_destCmd.CreateProjectOp(varVec);
			return this.m_destCmd.CreateNode(projectOp, list);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000944D0 File Offset: 0x000926D0
		public override Node Visit(FilterOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFilterOp(), n);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000944E4 File Offset: 0x000926E4
		public override Node Visit(SortOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			List<SortKey> list2 = this.Copy(op.Keys);
			SortOp sortOp = this.m_destCmd.CreateSortOp(list2);
			return this.m_destCmd.CreateNode(sortOp, list);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x00094520 File Offset: 0x00092720
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			List<SortKey> list2 = this.Copy(op.Keys);
			ConstrainedSortOp constrainedSortOp = this.m_destCmd.CreateConstrainedSortOp(list2, op.WithTies);
			return this.m_destCmd.CreateNode(constrainedSortOp, list);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x00094564 File Offset: 0x00092764
		public override Node Visit(GroupByOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			GroupByOp groupByOp = this.m_destCmd.CreateGroupByOp(this.Copy(op.Keys), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(groupByOp, list);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000945AC File Offset: 0x000927AC
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			GroupByIntoOp groupByIntoOp = this.m_destCmd.CreateGroupByIntoOp(this.Copy(op.Keys), this.Copy(op.Inputs), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(groupByIntoOp, list);
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000945FE File Offset: 0x000927FE
		public override Node Visit(CrossJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossJoinOp(), n);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00094612 File Offset: 0x00092812
		public override Node Visit(InnerJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateInnerJoinOp(), n);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00094626 File Offset: 0x00092826
		public override Node Visit(LeftOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLeftOuterJoinOp(), n);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0009463A File Offset: 0x0009283A
		public override Node Visit(FullOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFullOuterJoinOp(), n);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0009464E File Offset: 0x0009284E
		public override Node Visit(CrossApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossApplyOp(), n);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x00094662 File Offset: 0x00092862
		public override Node Visit(OuterApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateOuterApplyOp(), n);
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x00094678 File Offset: 0x00092878
		private Node CopySetOp(SetOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			VarMap varMap = new VarMap();
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in op.VarMap[0])
			{
				Var var = this.m_destCmd.CreateSetOpVar(keyValuePair.Key.Type);
				this.SetMappedVar(keyValuePair.Key, var);
				varMap.Add(var, this.GetMappedVar(keyValuePair.Value));
				varMap2.Add(var, this.GetMappedVar(op.VarMap[1][keyValuePair.Key]));
			}
			SetOp setOp = null;
			switch (op.OpType)
			{
			case OpType.UnionAll:
			{
				Var var2 = ((UnionAllOp)op).BranchDiscriminator;
				if (var2 != null)
				{
					var2 = this.GetMappedVar(var2);
				}
				setOp = this.m_destCmd.CreateUnionAllOp(varMap, varMap2, var2);
				break;
			}
			case OpType.Intersect:
				setOp = this.m_destCmd.CreateIntersectOp(varMap, varMap2);
				break;
			case OpType.Except:
				setOp = this.m_destCmd.CreateExceptOp(varMap, varMap2);
				break;
			}
			return this.m_destCmd.CreateNode(setOp, list);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000947B8 File Offset: 0x000929B8
		public override Node Visit(UnionAllOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000947C2 File Offset: 0x000929C2
		public override Node Visit(IntersectOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000947CC File Offset: 0x000929CC
		public override Node Visit(ExceptOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000947D8 File Offset: 0x000929D8
		public override Node Visit(DistinctOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			VarVec varVec = this.Copy(op.Keys);
			DistinctOp distinctOp = this.m_destCmd.CreateDistinctOp(varVec);
			return this.m_destCmd.CreateNode(distinctOp, list);
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x00094814 File Offset: 0x00092A14
		public override Node Visit(SingleRowOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowOp(), n);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x00094828 File Offset: 0x00092A28
		public override Node Visit(SingleRowTableOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowTableOp(), n);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0009483C File Offset: 0x00092A3C
		public override Node Visit(VarDefOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			Var var = this.m_destCmd.CreateComputedVar(op.Var.Type);
			this.SetMappedVar(op.Var, var);
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarDefOp(var), list);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0009488D File Offset: 0x00092A8D
		public override Node Visit(VarDefListOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateVarDefListOp(), n);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000948A1 File Offset: 0x00092AA1
		private ColumnMap Copy(ColumnMap columnMap)
		{
			return ColumnMapCopier.Copy(columnMap, this.m_varMap);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000948B0 File Offset: 0x00092AB0
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			List<Node> list = this.ProcessChildren(n);
			VarList varList = this.Copy(op.Outputs);
			SimpleCollectionColumnMap simpleCollectionColumnMap = this.Copy(op.ColumnMap) as SimpleCollectionColumnMap;
			PhysicalProjectOp physicalProjectOp = this.m_destCmd.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
			return this.m_destCmd.CreateNode(physicalProjectOp, list);
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00094900 File Offset: 0x00092B00
		private Node VisitNestOp(Node n)
		{
			NestBaseOp nestBaseOp = n.Op as NestBaseOp;
			SingleStreamNestOp singleStreamNestOp = nestBaseOp as SingleStreamNestOp;
			List<Node> list = this.ProcessChildren(n);
			Var var = null;
			if (singleStreamNestOp != null)
			{
				var = this.GetMappedVar(singleStreamNestOp.Discriminator);
			}
			List<CollectionInfo> list2 = new List<CollectionInfo>();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				ColumnMap columnMap = this.Copy(collectionInfo.ColumnMap);
				Var var2 = this.m_destCmd.CreateComputedVar(collectionInfo.CollectionVar.Type);
				this.SetMappedVar(collectionInfo.CollectionVar, var2);
				VarList varList = this.Copy(collectionInfo.FlattenedElementVars);
				VarVec varVec = this.Copy(collectionInfo.Keys);
				List<SortKey> list3 = this.Copy(collectionInfo.SortKeys);
				CollectionInfo collectionInfo2 = Command.CreateCollectionInfo(var2, columnMap, varList, varVec, list3, collectionInfo.DiscriminatorValue);
				list2.Add(collectionInfo2);
			}
			VarVec varVec2 = this.Copy(nestBaseOp.Outputs);
			List<SortKey> list4 = this.Copy(nestBaseOp.PrefixSortKeys);
			NestBaseOp nestBaseOp2;
			if (singleStreamNestOp != null)
			{
				VarVec varVec3 = this.Copy(singleStreamNestOp.Keys);
				List<SortKey> list5 = this.Copy(singleStreamNestOp.PostfixSortKeys);
				nestBaseOp2 = this.m_destCmd.CreateSingleStreamNestOp(varVec3, list4, list5, varVec2, list2, var);
			}
			else
			{
				nestBaseOp2 = this.m_destCmd.CreateMultiStreamNestOp(list4, varVec2, list2);
			}
			return this.m_destCmd.CreateNode(nestBaseOp2, list);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x00094A88 File Offset: 0x00092C88
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x00094A91 File Offset: 0x00092C91
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x04000F63 RID: 3939
		private readonly Command m_srcCmd;

		// Token: 0x04000F64 RID: 3940
		protected Command m_destCmd;

		// Token: 0x04000F65 RID: 3941
		protected VarMap m_varMap;
	}
}
