using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005CC RID: 1484
	internal class Propagator : UpdateExpressionVisitor<ChangeNode>
	{
		// Token: 0x06004797 RID: 18327 RVA: 0x000FDE47 File Offset: 0x000FC047
		private Propagator(UpdateTranslator parent, EntitySet table)
		{
			this.m_updateTranslator = parent;
			this.m_table = table;
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x000FDE5D File Offset: 0x000FC05D
		internal UpdateTranslator UpdateTranslator
		{
			get
			{
				return this.m_updateTranslator;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x000FDE65 File Offset: 0x000FC065
		protected override string VisitorName
		{
			get
			{
				return Propagator._visitorName;
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x000FDE6C File Offset: 0x000FC06C
		internal static ChangeNode Propagate(UpdateTranslator parent, EntitySet table, DbQueryCommandTree umView)
		{
			DbExpressionVisitor<ChangeNode> dbExpressionVisitor = new Propagator(parent, table);
			return umView.Query.Accept<ChangeNode>(dbExpressionVisitor);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000FDE8D File Offset: 0x000FC08D
		private static ChangeNode BuildChangeNode(DbExpression node)
		{
			return new ChangeNode(MetadataHelper.GetElementType(node.ResultType));
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000FDE9F File Offset: 0x000FC09F
		public override ChangeNode Visit(DbCrossJoinExpression node)
		{
			Check.NotNull<DbCrossJoinExpression>(node, "node");
			throw new NotSupportedException(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000FDEC4 File Offset: 0x000FC0C4
		public override ChangeNode Visit(DbJoinExpression node)
		{
			Check.NotNull<DbJoinExpression>(node, "node");
			if (DbExpressionKind.InnerJoin != node.ExpressionKind && DbExpressionKind.LeftOuterJoin != node.ExpressionKind)
			{
				throw new NotSupportedException(Strings.Update_UnsupportedJoinType(node.ExpressionKind));
			}
			DbExpression expression = node.Left.Expression;
			DbExpression expression2 = node.Right.Expression;
			ChangeNode changeNode = this.Visit(expression);
			ChangeNode changeNode2 = this.Visit(expression2);
			return new Propagator.JoinPropagator(changeNode, changeNode2, node, this).Propagate();
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x000FDF3C File Offset: 0x000FC13C
		public override ChangeNode Visit(DbUnionAllExpression node)
		{
			Check.NotNull<DbUnionAllExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Left);
			ChangeNode changeNode3 = this.Visit(node.Right);
			changeNode.Inserted.AddRange(changeNode2.Inserted);
			changeNode.Inserted.AddRange(changeNode3.Inserted);
			changeNode.Deleted.AddRange(changeNode2.Deleted);
			changeNode.Deleted.AddRange(changeNode3.Deleted);
			changeNode.Placeholder = changeNode2.Placeholder;
			return changeNode;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000FDFC8 File Offset: 0x000FC1C8
		public override ChangeNode Visit(DbProjectExpression node)
		{
			Check.NotNull<DbProjectExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			foreach (PropagatorResult propagatorResult in changeNode2.Inserted)
			{
				changeNode.Inserted.Add(Propagator.Project(node, propagatorResult, changeNode.ElementType));
			}
			foreach (PropagatorResult propagatorResult2 in changeNode2.Deleted)
			{
				changeNode.Deleted.Add(Propagator.Project(node, propagatorResult2, changeNode.ElementType));
			}
			changeNode.Placeholder = Propagator.Project(node, changeNode2.Placeholder, changeNode.ElementType);
			return changeNode;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x000FE0C0 File Offset: 0x000FC2C0
		private static PropagatorResult Project(DbProjectExpression node, PropagatorResult row, TypeUsage resultType)
		{
			DbNewInstanceExpression dbNewInstanceExpression = node.Projection as DbNewInstanceExpression;
			if (dbNewInstanceExpression == null)
			{
				throw new NotSupportedException(Strings.Update_UnsupportedProjection(node.Projection.ExpressionKind));
			}
			PropagatorResult[] array = new PropagatorResult[dbNewInstanceExpression.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Propagator.Evaluator.Evaluate(dbNewInstanceExpression.Arguments[i], row);
			}
			return PropagatorResult.CreateStructuralValue(array, (StructuralType)resultType.EdmType, false);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000FE140 File Offset: 0x000FC340
		public override ChangeNode Visit(DbFilterExpression node)
		{
			Check.NotNull<DbFilterExpression>(node, "node");
			ChangeNode changeNode = Propagator.BuildChangeNode(node);
			ChangeNode changeNode2 = this.Visit(node.Input.Expression);
			changeNode.Inserted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Inserted));
			changeNode.Deleted.AddRange(Propagator.Evaluator.Filter(node.Predicate, changeNode2.Deleted));
			changeNode.Placeholder = changeNode2.Placeholder;
			return changeNode;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x000FE1B8 File Offset: 0x000FC3B8
		public override ChangeNode Visit(DbScanExpression node)
		{
			Check.NotNull<DbScanExpression>(node, "node");
			EntitySetBase target = node.Target;
			ChangeNode extentModifications = this.UpdateTranslator.GetExtentModifications(target);
			if (extentModifications.Placeholder == null)
			{
				extentModifications.Placeholder = Propagator.ExtentPlaceholderCreator.CreatePlaceholder(target);
			}
			return extentModifications;
		}

		// Token: 0x04001978 RID: 6520
		private readonly UpdateTranslator m_updateTranslator;

		// Token: 0x04001979 RID: 6521
		private readonly EntitySet m_table;

		// Token: 0x0400197A RID: 6522
		private static readonly string _visitorName = typeof(Propagator).FullName;

		// Token: 0x02000C03 RID: 3075
		private class Evaluator : UpdateExpressionVisitor<PropagatorResult>
		{
			// Token: 0x060068E8 RID: 26856 RVA: 0x0016665B File Offset: 0x0016485B
			private Evaluator(PropagatorResult row)
			{
				this.m_row = row;
			}

			// Token: 0x1700113A RID: 4410
			// (get) Token: 0x060068E9 RID: 26857 RVA: 0x0016666A File Offset: 0x0016486A
			protected override string VisitorName
			{
				get
				{
					return Propagator.Evaluator._visitorName;
				}
			}

			// Token: 0x060068EA RID: 26858 RVA: 0x00166671 File Offset: 0x00164871
			internal static IEnumerable<PropagatorResult> Filter(DbExpression predicate, IEnumerable<PropagatorResult> rows)
			{
				foreach (PropagatorResult propagatorResult in rows)
				{
					if (Propagator.Evaluator.EvaluatePredicate(predicate, propagatorResult))
					{
						yield return propagatorResult;
					}
				}
				IEnumerator<PropagatorResult> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060068EB RID: 26859 RVA: 0x00166688 File Offset: 0x00164888
			internal static bool EvaluatePredicate(DbExpression predicate, PropagatorResult row)
			{
				Propagator.Evaluator evaluator = new Propagator.Evaluator(row);
				return Propagator.Evaluator.ConvertResultToBool(predicate.Accept<PropagatorResult>(evaluator)).GetValueOrDefault();
			}

			// Token: 0x060068EC RID: 26860 RVA: 0x001666B0 File Offset: 0x001648B0
			internal static PropagatorResult Evaluate(DbExpression node, PropagatorResult row)
			{
				DbExpressionVisitor<PropagatorResult> dbExpressionVisitor = new Propagator.Evaluator(row);
				return node.Accept<PropagatorResult>(dbExpressionVisitor);
			}

			// Token: 0x060068ED RID: 26861 RVA: 0x001666CC File Offset: 0x001648CC
			private static bool? ConvertResultToBool(PropagatorResult result)
			{
				if (result.IsNull)
				{
					return null;
				}
				return new bool?((bool)result.GetSimpleValue());
			}

			// Token: 0x060068EE RID: 26862 RVA: 0x001666FC File Offset: 0x001648FC
			private static PropagatorResult ConvertBoolToResult(bool? booleanValue, params PropagatorResult[] inputs)
			{
				object obj;
				if (booleanValue != null)
				{
					obj = booleanValue.Value;
				}
				else
				{
					obj = null;
				}
				return PropagatorResult.CreateSimpleValue(Propagator.Evaluator.PropagateUnknownAndPreserveFlags(null, inputs), obj);
			}

			// Token: 0x060068EF RID: 26863 RVA: 0x00166730 File Offset: 0x00164930
			public override PropagatorResult Visit(DbIsOfExpression predicate)
			{
				Check.NotNull<DbIsOfExpression>(predicate, "predicate");
				if (DbExpressionKind.IsOfOnly != predicate.ExpressionKind)
				{
					throw base.ConstructNotSupportedException(predicate);
				}
				PropagatorResult propagatorResult = this.Visit(predicate.Argument);
				bool flag = !propagatorResult.IsNull && propagatorResult.StructuralType.EdmEquals(predicate.OfType.EdmType);
				return Propagator.Evaluator.ConvertBoolToResult(new bool?(flag), new PropagatorResult[] { propagatorResult });
			}

			// Token: 0x060068F0 RID: 26864 RVA: 0x001667A0 File Offset: 0x001649A0
			public override PropagatorResult Visit(DbComparisonExpression predicate)
			{
				Check.NotNull<DbComparisonExpression>(predicate, "predicate");
				if (DbExpressionKind.Equals == predicate.ExpressionKind)
				{
					PropagatorResult propagatorResult = this.Visit(predicate.Left);
					PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
					bool? flag;
					if (propagatorResult.IsNull || propagatorResult2.IsNull)
					{
						flag = null;
					}
					else
					{
						object simpleValue = propagatorResult.GetSimpleValue();
						object simpleValue2 = propagatorResult2.GetSimpleValue();
						flag = new bool?(ByValueEqualityComparer.Default.Equals(simpleValue, simpleValue2));
					}
					return Propagator.Evaluator.ConvertBoolToResult(flag, new PropagatorResult[] { propagatorResult, propagatorResult2 });
				}
				throw base.ConstructNotSupportedException(predicate);
			}

			// Token: 0x060068F1 RID: 26865 RVA: 0x00166838 File Offset: 0x00164A38
			public override PropagatorResult Visit(DbAndExpression predicate)
			{
				Check.NotNull<DbAndExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? flag = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? flag2 = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((flag != null && !flag.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (flag2 != null && !flag2.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(false);
				}
				return Propagator.Evaluator.ConvertBoolToResult(flag.And(flag2), new PropagatorResult[] { propagatorResult, propagatorResult2 });
			}

			// Token: 0x060068F2 RID: 26866 RVA: 0x001668D4 File Offset: 0x00164AD4
			public override PropagatorResult Visit(DbOrExpression predicate)
			{
				Check.NotNull<DbOrExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Left);
				PropagatorResult propagatorResult2 = this.Visit(predicate.Right);
				bool? flag = Propagator.Evaluator.ConvertResultToBool(propagatorResult);
				bool? flag2 = Propagator.Evaluator.ConvertResultToBool(propagatorResult2);
				if ((flag != null && flag.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult)) || (flag2 != null && flag2.Value && Propagator.Evaluator.PreservedAndKnown(propagatorResult2)))
				{
					return Propagator.Evaluator.CreatePerservedAndKnownResult(true);
				}
				return Propagator.Evaluator.ConvertBoolToResult(flag.Or(flag2), new PropagatorResult[] { propagatorResult, propagatorResult2 });
			}

			// Token: 0x060068F3 RID: 26867 RVA: 0x0016696F File Offset: 0x00164B6F
			private static PropagatorResult CreatePerservedAndKnownResult(object value)
			{
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, value);
			}

			// Token: 0x060068F4 RID: 26868 RVA: 0x00166978 File Offset: 0x00164B78
			private static bool PreservedAndKnown(PropagatorResult result)
			{
				return PropagatorFlags.Preserve == (result.PropagatorFlags & (PropagatorFlags.Preserve | PropagatorFlags.Unknown));
			}

			// Token: 0x060068F5 RID: 26869 RVA: 0x00166988 File Offset: 0x00164B88
			public override PropagatorResult Visit(DbNotExpression predicate)
			{
				Check.NotNull<DbNotExpression>(predicate, "predicate");
				PropagatorResult propagatorResult = this.Visit(predicate.Argument);
				return Propagator.Evaluator.ConvertBoolToResult(Propagator.Evaluator.ConvertResultToBool(propagatorResult).Not(), new PropagatorResult[] { propagatorResult });
			}

			// Token: 0x060068F6 RID: 26870 RVA: 0x001669C8 File Offset: 0x00164BC8
			public override PropagatorResult Visit(DbCaseExpression node)
			{
				Check.NotNull<DbCaseExpression>(node, "node");
				int num = -1;
				int num2 = 0;
				List<PropagatorResult> list = new List<PropagatorResult>();
				foreach (DbExpression dbExpression in node.When)
				{
					PropagatorResult propagatorResult = this.Visit(dbExpression);
					list.Add(propagatorResult);
					if (Propagator.Evaluator.ConvertResultToBool(propagatorResult).GetValueOrDefault())
					{
						num = num2;
						break;
					}
					num2++;
				}
				PropagatorResult propagatorResult2;
				if (-1 == num)
				{
					propagatorResult2 = this.Visit(node.Else);
				}
				else
				{
					propagatorResult2 = this.Visit(node.Then[num]);
				}
				list.Add(propagatorResult2);
				PropagatorFlags propagatorFlags = Propagator.Evaluator.PropagateUnknownAndPreserveFlags(propagatorResult2, list);
				return propagatorResult2.ReplicateResultWithNewFlags(propagatorFlags);
			}

			// Token: 0x060068F7 RID: 26871 RVA: 0x00166A94 File Offset: 0x00164C94
			public override PropagatorResult Visit(DbVariableReferenceExpression node)
			{
				Check.NotNull<DbVariableReferenceExpression>(node, "node");
				return this.m_row;
			}

			// Token: 0x060068F8 RID: 26872 RVA: 0x00166AA8 File Offset: 0x00164CA8
			public override PropagatorResult Visit(DbPropertyExpression node)
			{
				Check.NotNull<DbPropertyExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Instance);
				PropagatorResult propagatorResult2;
				if (propagatorResult.IsNull)
				{
					propagatorResult2 = PropagatorResult.CreateSimpleValue(propagatorResult.PropagatorFlags, null);
				}
				else
				{
					propagatorResult2 = propagatorResult.GetMemberValue(node.Property);
				}
				return propagatorResult2;
			}

			// Token: 0x060068F9 RID: 26873 RVA: 0x00166AF3 File Offset: 0x00164CF3
			public override PropagatorResult Visit(DbConstantExpression node)
			{
				Check.NotNull<DbConstantExpression>(node, "node");
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, node.Value);
			}

			// Token: 0x060068FA RID: 26874 RVA: 0x00166B0D File Offset: 0x00164D0D
			public override PropagatorResult Visit(DbRefKeyExpression node)
			{
				Check.NotNull<DbRefKeyExpression>(node, "node");
				return this.Visit(node.Argument);
			}

			// Token: 0x060068FB RID: 26875 RVA: 0x00166B27 File Offset: 0x00164D27
			public override PropagatorResult Visit(DbNullExpression node)
			{
				Check.NotNull<DbNullExpression>(node, "node");
				return PropagatorResult.CreateSimpleValue(PropagatorFlags.Preserve, null);
			}

			// Token: 0x060068FC RID: 26876 RVA: 0x00166B3C File Offset: 0x00164D3C
			public override PropagatorResult Visit(DbTreatExpression node)
			{
				Check.NotNull<DbTreatExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				if (MetadataHelper.IsSuperTypeOf(node.ResultType.EdmType, propagatorResult.StructuralType))
				{
					return propagatorResult;
				}
				return PropagatorResult.CreateSimpleValue(propagatorResult.PropagatorFlags, null);
			}

			// Token: 0x060068FD RID: 26877 RVA: 0x00166B88 File Offset: 0x00164D88
			public override PropagatorResult Visit(DbCastExpression node)
			{
				Check.NotNull<DbCastExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				TypeUsage resultType = node.ResultType;
				if (!propagatorResult.IsSimple || BuiltInTypeKind.PrimitiveType != resultType.EdmType.BuiltInTypeKind)
				{
					throw new NotSupportedException(Strings.Update_UnsupportedCastArgument(resultType.EdmType.Name));
				}
				object obj;
				if (propagatorResult.IsNull)
				{
					obj = null;
				}
				else
				{
					try
					{
						obj = Propagator.Evaluator.Cast(propagatorResult.GetSimpleValue(), ((PrimitiveType)resultType.EdmType).ClrEquivalentType);
					}
					catch
					{
						throw;
					}
				}
				return propagatorResult.ReplicateResultWithNewValue(obj);
			}

			// Token: 0x060068FE RID: 26878 RVA: 0x00166C28 File Offset: 0x00164E28
			private static object Cast(object value, Type clrPrimitiveType)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				if (value == null || value == DBNull.Value || value.GetType() == clrPrimitiveType)
				{
					return value;
				}
				if (value is DateTime && clrPrimitiveType == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(((DateTime)value).Ticks, TimeSpan.Zero);
				}
				return Convert.ChangeType(value, clrPrimitiveType, invariantCulture);
			}

			// Token: 0x060068FF RID: 26879 RVA: 0x00166C98 File Offset: 0x00164E98
			public override PropagatorResult Visit(DbIsNullExpression node)
			{
				Check.NotNull<DbIsNullExpression>(node, "node");
				PropagatorResult propagatorResult = this.Visit(node.Argument);
				return Propagator.Evaluator.ConvertBoolToResult(new bool?(propagatorResult.IsNull), new PropagatorResult[] { propagatorResult });
			}

			// Token: 0x06006900 RID: 26880 RVA: 0x00166CD8 File Offset: 0x00164ED8
			private static PropagatorFlags PropagateUnknownAndPreserveFlags(PropagatorResult result, IEnumerable<PropagatorResult> inputs)
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = true;
				foreach (PropagatorResult propagatorResult in inputs)
				{
					flag3 = false;
					PropagatorFlags propagatorFlags = propagatorResult.PropagatorFlags;
					if ((PropagatorFlags.Unknown & propagatorFlags) != PropagatorFlags.NoFlags)
					{
						flag = true;
					}
					if ((PropagatorFlags.Preserve & propagatorFlags) == PropagatorFlags.NoFlags)
					{
						flag2 = false;
					}
				}
				if (flag3)
				{
					flag2 = false;
				}
				if (result != null)
				{
					PropagatorFlags propagatorFlags2 = result.PropagatorFlags;
					if (flag)
					{
						propagatorFlags2 |= PropagatorFlags.Unknown;
					}
					if (!flag2)
					{
						propagatorFlags2 &= ~PropagatorFlags.Preserve;
					}
					return propagatorFlags2;
				}
				PropagatorFlags propagatorFlags3 = PropagatorFlags.NoFlags;
				if (flag)
				{
					propagatorFlags3 |= PropagatorFlags.Unknown;
				}
				if (flag2)
				{
					propagatorFlags3 |= PropagatorFlags.Preserve;
				}
				return propagatorFlags3;
			}

			// Token: 0x04002F90 RID: 12176
			private readonly PropagatorResult m_row;

			// Token: 0x04002F91 RID: 12177
			private static readonly string _visitorName = typeof(Propagator.Evaluator).FullName;
		}

		// Token: 0x02000C04 RID: 3076
		internal class ExtentPlaceholderCreator
		{
			// Token: 0x06006902 RID: 26882 RVA: 0x00166D90 File Offset: 0x00164F90
			private static Dictionary<PrimitiveTypeKind, object> InitializeTypeDefaultMap()
			{
				Dictionary<PrimitiveTypeKind, object> dictionary = new Dictionary<PrimitiveTypeKind, object>(EqualityComparer<PrimitiveTypeKind>.Default);
				dictionary[PrimitiveTypeKind.Binary] = new byte[0];
				dictionary[PrimitiveTypeKind.Boolean] = false;
				dictionary[PrimitiveTypeKind.Byte] = 0;
				dictionary[PrimitiveTypeKind.DateTime] = default(DateTime);
				dictionary[PrimitiveTypeKind.Time] = default(TimeSpan);
				dictionary[PrimitiveTypeKind.DateTimeOffset] = default(DateTimeOffset);
				dictionary[PrimitiveTypeKind.Decimal] = 0m;
				dictionary[PrimitiveTypeKind.Double] = 0.0;
				dictionary[PrimitiveTypeKind.Guid] = default(Guid);
				dictionary[PrimitiveTypeKind.Int16] = 0;
				dictionary[PrimitiveTypeKind.Int32] = 0;
				dictionary[PrimitiveTypeKind.Int64] = 0L;
				dictionary[PrimitiveTypeKind.Single] = 0f;
				dictionary[PrimitiveTypeKind.SByte] = 0;
				dictionary[PrimitiveTypeKind.String] = string.Empty;
				dictionary[PrimitiveTypeKind.HierarchyId] = HierarchyId.GetRoot();
				return dictionary;
			}

			// Token: 0x06006903 RID: 26883 RVA: 0x00166EB0 File Offset: 0x001650B0
			private static Dictionary<PrimitiveTypeKind, object> InitializeSpatialTypeDefaultMap()
			{
				Dictionary<PrimitiveTypeKind, object> dictionary = new Dictionary<PrimitiveTypeKind, object>(EqualityComparer<PrimitiveTypeKind>.Default);
				dictionary[PrimitiveTypeKind.Geometry] = DbGeometry.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryPoint] = DbGeometry.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryLineString] = DbGeometry.FromText("LINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeometryPolygon] = DbGeometry.FromText("POLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiPoint] = DbGeometry.FromText("MULTIPOINT EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiLineString] = DbGeometry.FromText("MULTILINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeometryMultiPolygon] = DbGeometry.FromText("MULTIPOLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeometryCollection] = DbGeometry.FromText("GEOMETRYCOLLECTION EMPTY");
				dictionary[PrimitiveTypeKind.Geography] = DbGeography.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyPoint] = DbGeography.FromText("POINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyLineString] = DbGeography.FromText("LINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeographyPolygon] = DbGeography.FromText("POLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiPoint] = DbGeography.FromText("MULTIPOINT EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiLineString] = DbGeography.FromText("MULTILINESTRING EMPTY");
				dictionary[PrimitiveTypeKind.GeographyMultiPolygon] = DbGeography.FromText("MULTIPOLYGON EMPTY");
				dictionary[PrimitiveTypeKind.GeographyCollection] = DbGeography.FromText("GEOMETRYCOLLECTION EMPTY");
				return dictionary;
			}

			// Token: 0x06006904 RID: 26884 RVA: 0x00166FE8 File Offset: 0x001651E8
			private static bool TryGetDefaultValue(PrimitiveType primitiveType, out object defaultValue)
			{
				PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
				if (!Helper.IsSpatialType(primitiveType))
				{
					return Propagator.ExtentPlaceholderCreator._typeDefaultMap.TryGetValue(primitiveTypeKind, out defaultValue);
				}
				return Propagator.ExtentPlaceholderCreator._spatialTypeDefaultMap.Value.TryGetValue(primitiveTypeKind, out defaultValue);
			}

			// Token: 0x06006905 RID: 26885 RVA: 0x00167024 File Offset: 0x00165224
			internal static PropagatorResult CreatePlaceholder(EntitySetBase extent)
			{
				Propagator.ExtentPlaceholderCreator extentPlaceholderCreator = new Propagator.ExtentPlaceholderCreator();
				AssociationSet associationSet = extent as AssociationSet;
				if (associationSet != null)
				{
					return extentPlaceholderCreator.CreateAssociationSetPlaceholder(associationSet);
				}
				EntitySet entitySet = extent as EntitySet;
				if (entitySet != null)
				{
					return extentPlaceholderCreator.CreateEntitySetPlaceholder(entitySet);
				}
				throw new NotSupportedException(Strings.Update_UnsupportedExtentType(extent.Name, extent.GetType().Name));
			}

			// Token: 0x06006906 RID: 26886 RVA: 0x00167078 File Offset: 0x00165278
			private PropagatorResult CreateEntitySetPlaceholder(EntitySet entitySet)
			{
				ReadOnlyMetadataCollection<EdmProperty> properties = entitySet.ElementType.Properties;
				PropagatorResult[] array = new PropagatorResult[properties.Count];
				for (int i = 0; i < properties.Count; i++)
				{
					PropagatorResult propagatorResult = this.CreateMemberPlaceholder(properties[i]);
					array[i] = propagatorResult;
				}
				return PropagatorResult.CreateStructuralValue(array, entitySet.ElementType, false);
			}

			// Token: 0x06006907 RID: 26887 RVA: 0x001670D0 File Offset: 0x001652D0
			private PropagatorResult CreateAssociationSetPlaceholder(AssociationSet associationSet)
			{
				ReadOnlyMetadataCollection<AssociationEndMember> associationEndMembers = associationSet.ElementType.AssociationEndMembers;
				PropagatorResult[] array = new PropagatorResult[associationEndMembers.Count];
				for (int i = 0; i < associationEndMembers.Count; i++)
				{
					EntityType entityType = (EntityType)((RefType)associationEndMembers[i].TypeUsage.EdmType).ElementType;
					PropagatorResult[] array2 = new PropagatorResult[entityType.KeyMembers.Count];
					for (int j = 0; j < entityType.KeyMembers.Count; j++)
					{
						EdmMember edmMember = entityType.KeyMembers[j];
						PropagatorResult propagatorResult = this.CreateMemberPlaceholder(edmMember);
						array2[j] = propagatorResult;
					}
					RowType keyRowType = entityType.GetKeyRowType();
					PropagatorResult propagatorResult2 = PropagatorResult.CreateStructuralValue(array2, keyRowType, false);
					array[i] = propagatorResult2;
				}
				return PropagatorResult.CreateStructuralValue(array, associationSet.ElementType, false);
			}

			// Token: 0x06006908 RID: 26888 RVA: 0x0016719F File Offset: 0x0016539F
			private PropagatorResult CreateMemberPlaceholder(EdmMember member)
			{
				return this.Visit(member);
			}

			// Token: 0x06006909 RID: 26889 RVA: 0x001671A8 File Offset: 0x001653A8
			internal PropagatorResult Visit(EdmMember node)
			{
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(node);
				PropagatorResult propagatorResult;
				if (Helper.IsScalarType(modelTypeUsage.EdmType))
				{
					Propagator.ExtentPlaceholderCreator.GetPropagatorResultForPrimitiveType(Helper.AsPrimitive(modelTypeUsage.EdmType), out propagatorResult);
				}
				else
				{
					StructuralType structuralType = (StructuralType)modelTypeUsage.EdmType;
					IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
					PropagatorResult[] array = new PropagatorResult[allStructuralMembers.Count];
					for (int i = 0; i < allStructuralMembers.Count; i++)
					{
						array[i] = this.Visit(allStructuralMembers[i]);
					}
					propagatorResult = PropagatorResult.CreateStructuralValue(array, structuralType, false);
				}
				return propagatorResult;
			}

			// Token: 0x0600690A RID: 26890 RVA: 0x00167230 File Offset: 0x00165430
			internal static void GetPropagatorResultForPrimitiveType(PrimitiveType primitiveType, out PropagatorResult result)
			{
				object obj;
				if (!Propagator.ExtentPlaceholderCreator.TryGetDefaultValue(primitiveType, out obj))
				{
					obj = 0;
				}
				result = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, obj);
			}

			// Token: 0x04002F92 RID: 12178
			private static readonly Dictionary<PrimitiveTypeKind, object> _typeDefaultMap = Propagator.ExtentPlaceholderCreator.InitializeTypeDefaultMap();

			// Token: 0x04002F93 RID: 12179
			private static readonly Lazy<Dictionary<PrimitiveTypeKind, object>> _spatialTypeDefaultMap = new Lazy<Dictionary<PrimitiveTypeKind, object>>(new Func<Dictionary<PrimitiveTypeKind, object>>(Propagator.ExtentPlaceholderCreator.InitializeSpatialTypeDefaultMap));
		}

		// Token: 0x02000C05 RID: 3077
		private class JoinPropagator
		{
			// Token: 0x0600690D RID: 26893 RVA: 0x00167284 File Offset: 0x00165484
			internal JoinPropagator(ChangeNode left, ChangeNode right, DbJoinExpression node, Propagator parent)
			{
				this.m_left = left;
				this.m_right = right;
				this.m_joinExpression = node;
				this.m_parent = parent;
				if (DbExpressionKind.InnerJoin == this.m_joinExpression.ExpressionKind)
				{
					this.m_insertRules = Propagator.JoinPropagator._innerJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator._innerJoinDeleteRules;
				}
				else
				{
					this.m_insertRules = Propagator.JoinPropagator._leftOuterJoinInsertRules;
					this.m_deleteRules = Propagator.JoinPropagator._leftOuterJoinDeleteRules;
				}
				Propagator.JoinPropagator.JoinConditionVisitor.GetKeySelectors(node.JoinCondition, out this.m_leftKeySelectors, out this.m_rightKeySelectors);
				this.m_leftPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_left.Placeholder, this.m_leftKeySelectors);
				this.m_rightPlaceholderKey = Propagator.JoinPropagator.ExtractKey(this.m_right.Placeholder, this.m_rightKeySelectors);
			}

			// Token: 0x0600690E RID: 26894 RVA: 0x00167340 File Offset: 0x00165540
			static JoinPropagator()
			{
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.Nothing);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftUpdate, Propagator.JoinPropagator.Ops.LeftInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftDeleteUnknownExtended, Propagator.JoinPropagator.Ops.LeftInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightUpdate, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertNullModifiedExtended, Propagator.JoinPropagator.Ops.LeftDeleteJoinRightDelete);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertJoinRightInsert, Propagator.JoinPropagator.Ops.LeftDeleteNullModifiedExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Nothing, Propagator.JoinPropagator.Ops.LeftDeleteNullPreserveExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftInsertNullModifiedExtended, Propagator.JoinPropagator.Ops.Nothing);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.LeftUnknownNullModifiedExtended, Propagator.JoinPropagator.Ops.RightDeleteUnknownExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.RightInsertUnknownExtended, Propagator.JoinPropagator.Ops.LeftUnknownNullModifiedExtended);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftDelete | Propagator.JoinPropagator.Ops.RightInsert, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.RightInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
				Propagator.JoinPropagator.InitializeRule(Propagator.JoinPropagator.Ops.LeftInsert | Propagator.JoinPropagator.Ops.RightDelete, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported, Propagator.JoinPropagator.Ops.Unsupported);
			}

			// Token: 0x0600690F RID: 26895 RVA: 0x001674C9 File Offset: 0x001656C9
			private static void InitializeRule(Propagator.JoinPropagator.Ops input, Propagator.JoinPropagator.Ops joinInsert, Propagator.JoinPropagator.Ops joinDelete, Propagator.JoinPropagator.Ops lojInsert, Propagator.JoinPropagator.Ops lojDelete)
			{
				Propagator.JoinPropagator._innerJoinInsertRules.Add(input, joinInsert);
				Propagator.JoinPropagator._innerJoinDeleteRules.Add(input, joinDelete);
				Propagator.JoinPropagator._leftOuterJoinInsertRules.Add(input, lojInsert);
				Propagator.JoinPropagator._leftOuterJoinDeleteRules.Add(input, lojDelete);
			}

			// Token: 0x06006910 RID: 26896 RVA: 0x001674FC File Offset: 0x001656FC
			internal ChangeNode Propagate()
			{
				ChangeNode changeNode = Propagator.BuildChangeNode(this.m_joinExpression);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary = this.ProcessKeys(this.m_left.Deleted, this.m_leftKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary2 = this.ProcessKeys(this.m_left.Inserted, this.m_leftKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary3 = this.ProcessKeys(this.m_right.Deleted, this.m_rightKeySelectors);
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary4 = this.ProcessKeys(this.m_right.Inserted, this.m_rightKeySelectors);
				foreach (CompositeKey compositeKey in dictionary.Keys.Concat(dictionary2.Keys).Concat(dictionary3.Keys).Concat(dictionary4.Keys)
					.Distinct(this.m_parent.UpdateTranslator.KeyComparer))
				{
					this.Propagate(compositeKey, changeNode, dictionary, dictionary2, dictionary3, dictionary4);
				}
				changeNode.Placeholder = this.CreateResultTuple(Tuple.Create<CompositeKey, PropagatorResult>(null, this.m_left.Placeholder), Tuple.Create<CompositeKey, PropagatorResult>(null, this.m_right.Placeholder), changeNode);
				return changeNode;
			}

			// Token: 0x06006911 RID: 26897 RVA: 0x0016762C File Offset: 0x0016582C
			private void Propagate(CompositeKey key, ChangeNode result, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> leftDeletes, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> leftInserts, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> rightDeletes, Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> rightInserts)
			{
				Tuple<CompositeKey, PropagatorResult> tuple = null;
				Tuple<CompositeKey, PropagatorResult> tuple2 = null;
				Tuple<CompositeKey, PropagatorResult> tuple3 = null;
				Tuple<CompositeKey, PropagatorResult> tuple4 = null;
				Propagator.JoinPropagator.Ops ops = Propagator.JoinPropagator.Ops.Nothing;
				if (leftInserts.TryGetValue(key, out tuple))
				{
					ops |= Propagator.JoinPropagator.Ops.LeftInsert;
				}
				if (leftDeletes.TryGetValue(key, out tuple2))
				{
					ops |= Propagator.JoinPropagator.Ops.LeftDelete;
				}
				if (rightInserts.TryGetValue(key, out tuple3))
				{
					ops |= Propagator.JoinPropagator.Ops.RightInsert;
				}
				if (rightDeletes.TryGetValue(key, out tuple4))
				{
					ops |= Propagator.JoinPropagator.Ops.RightDelete;
				}
				Propagator.JoinPropagator.Ops ops2 = this.m_insertRules[ops];
				Propagator.JoinPropagator.Ops ops3 = this.m_deleteRules[ops];
				if (Propagator.JoinPropagator.Ops.Unsupported == ops2 || Propagator.JoinPropagator.Ops.Unsupported == ops3)
				{
					List<IEntityStateEntry> stateEntries = new List<IEntityStateEntry>();
					Action<Tuple<CompositeKey, PropagatorResult>> action = delegate(Tuple<CompositeKey, PropagatorResult> r)
					{
						if (r != null)
						{
							stateEntries.AddRange(SourceInterpreter.GetAllStateEntries(r.Item2, this.m_parent.m_updateTranslator, this.m_parent.m_table));
						}
					};
					action(tuple);
					action(tuple2);
					action(tuple3);
					action(tuple4);
					throw new UpdateException(Strings.Update_InvalidChanges, null, stateEntries.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				if ((Propagator.JoinPropagator.Ops.LeftUnknown & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple = Tuple.Create<CompositeKey, PropagatorResult>(key, this.LeftPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.LeftUnknown & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple2 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.LeftPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.RightNullModified & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullModified));
				}
				else if ((Propagator.JoinPropagator.Ops.RightNullPreserve & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullPreserve));
				}
				else if ((Propagator.JoinPropagator.Ops.RightUnknown & ops2) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple3 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if ((Propagator.JoinPropagator.Ops.RightNullModified & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullModified));
				}
				else if ((Propagator.JoinPropagator.Ops.RightNullPreserve & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.NullPreserve));
				}
				else if ((Propagator.JoinPropagator.Ops.RightUnknown & ops3) != Propagator.JoinPropagator.Ops.Nothing)
				{
					tuple4 = Tuple.Create<CompositeKey, PropagatorResult>(key, this.RightPlaceholder(key, Propagator.JoinPropagator.PopulateMode.Unknown));
				}
				if (tuple != null && tuple3 != null)
				{
					result.Inserted.Add(this.CreateResultTuple(tuple, tuple3, result));
				}
				if (tuple2 != null && tuple4 != null)
				{
					result.Deleted.Add(this.CreateResultTuple(tuple2, tuple4, result));
				}
			}

			// Token: 0x06006912 RID: 26898 RVA: 0x0016781C File Offset: 0x00165A1C
			private PropagatorResult CreateResultTuple(Tuple<CompositeKey, PropagatorResult> left, Tuple<CompositeKey, PropagatorResult> right, ChangeNode result)
			{
				CompositeKey item = left.Item1;
				CompositeKey item2 = right.Item1;
				Dictionary<PropagatorResult, PropagatorResult> map = null;
				if (item != null && item2 != null && item != item2)
				{
					CompositeKey compositeKey = item.Merge(this.m_parent.m_updateTranslator.KeyManager, item2);
					map = new Dictionary<PropagatorResult, PropagatorResult>();
					for (int i = 0; i < item.KeyComponents.Length; i++)
					{
						map[item.KeyComponents[i]] = compositeKey.KeyComponents[i];
						map[item2.KeyComponents[i]] = compositeKey.KeyComponents[i];
					}
				}
				PropagatorResult propagatorResult = PropagatorResult.CreateStructuralValue(new PropagatorResult[] { left.Item2, right.Item2 }, (StructuralType)result.ElementType.EdmType, false);
				if (map != null)
				{
					PropagatorResult replacement;
					propagatorResult = propagatorResult.Replace(delegate(PropagatorResult original)
					{
						if (!map.TryGetValue(original, out replacement))
						{
							return original;
						}
						return replacement;
					});
				}
				return propagatorResult;
			}

			// Token: 0x06006913 RID: 26899 RVA: 0x00167910 File Offset: 0x00165B10
			private PropagatorResult LeftPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_left.Placeholder, key, this.m_leftPlaceholderKey, mode);
			}

			// Token: 0x06006914 RID: 26900 RVA: 0x0016792A File Offset: 0x00165B2A
			private PropagatorResult RightPlaceholder(CompositeKey key, Propagator.JoinPropagator.PopulateMode mode)
			{
				return Propagator.JoinPropagator.PlaceholderPopulator.Populate(this.m_right.Placeholder, key, this.m_rightPlaceholderKey, mode);
			}

			// Token: 0x06006915 RID: 26901 RVA: 0x00167944 File Offset: 0x00165B44
			private Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> ProcessKeys(IEnumerable<PropagatorResult> instances, ReadOnlyCollection<DbExpression> keySelectors)
			{
				Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>> dictionary = new Dictionary<CompositeKey, Tuple<CompositeKey, PropagatorResult>>(this.m_parent.UpdateTranslator.KeyComparer);
				foreach (PropagatorResult propagatorResult in instances)
				{
					CompositeKey compositeKey = Propagator.JoinPropagator.ExtractKey(propagatorResult, keySelectors);
					dictionary[compositeKey] = Tuple.Create<CompositeKey, PropagatorResult>(compositeKey, propagatorResult);
				}
				return dictionary;
			}

			// Token: 0x06006916 RID: 26902 RVA: 0x001679B4 File Offset: 0x00165BB4
			private static CompositeKey ExtractKey(PropagatorResult change, ReadOnlyCollection<DbExpression> keySelectors)
			{
				PropagatorResult[] array = new PropagatorResult[keySelectors.Count];
				for (int i = 0; i < keySelectors.Count; i++)
				{
					PropagatorResult propagatorResult = Propagator.Evaluator.Evaluate(keySelectors[i], change);
					array[i] = propagatorResult;
				}
				return new CompositeKey(array);
			}

			// Token: 0x04002F94 RID: 12180
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _innerJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04002F95 RID: 12181
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _innerJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04002F96 RID: 12182
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _leftOuterJoinInsertRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04002F97 RID: 12183
			private static readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> _leftOuterJoinDeleteRules = new Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops>(EqualityComparer<Propagator.JoinPropagator.Ops>.Default);

			// Token: 0x04002F98 RID: 12184
			private readonly DbJoinExpression m_joinExpression;

			// Token: 0x04002F99 RID: 12185
			private readonly Propagator m_parent;

			// Token: 0x04002F9A RID: 12186
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_insertRules;

			// Token: 0x04002F9B RID: 12187
			private readonly Dictionary<Propagator.JoinPropagator.Ops, Propagator.JoinPropagator.Ops> m_deleteRules;

			// Token: 0x04002F9C RID: 12188
			private readonly ReadOnlyCollection<DbExpression> m_leftKeySelectors;

			// Token: 0x04002F9D RID: 12189
			private readonly ReadOnlyCollection<DbExpression> m_rightKeySelectors;

			// Token: 0x04002F9E RID: 12190
			private readonly ChangeNode m_left;

			// Token: 0x04002F9F RID: 12191
			private readonly ChangeNode m_right;

			// Token: 0x04002FA0 RID: 12192
			private readonly CompositeKey m_leftPlaceholderKey;

			// Token: 0x04002FA1 RID: 12193
			private readonly CompositeKey m_rightPlaceholderKey;

			// Token: 0x02000D8A RID: 3466
			[Flags]
			private enum Ops : uint
			{
				// Token: 0x04003370 RID: 13168
				Nothing = 0U,
				// Token: 0x04003371 RID: 13169
				LeftInsert = 1U,
				// Token: 0x04003372 RID: 13170
				LeftDelete = 2U,
				// Token: 0x04003373 RID: 13171
				RightInsert = 4U,
				// Token: 0x04003374 RID: 13172
				RightDelete = 8U,
				// Token: 0x04003375 RID: 13173
				LeftUnknown = 32U,
				// Token: 0x04003376 RID: 13174
				RightNullModified = 128U,
				// Token: 0x04003377 RID: 13175
				RightNullPreserve = 256U,
				// Token: 0x04003378 RID: 13176
				RightUnknown = 512U,
				// Token: 0x04003379 RID: 13177
				LeftUpdate = 3U,
				// Token: 0x0400337A RID: 13178
				RightUpdate = 12U,
				// Token: 0x0400337B RID: 13179
				Unsupported = 4096U,
				// Token: 0x0400337C RID: 13180
				LeftInsertJoinRightInsert = 5U,
				// Token: 0x0400337D RID: 13181
				LeftDeleteJoinRightDelete = 10U,
				// Token: 0x0400337E RID: 13182
				LeftInsertNullModifiedExtended = 129U,
				// Token: 0x0400337F RID: 13183
				LeftInsertNullPreserveExtended = 257U,
				// Token: 0x04003380 RID: 13184
				LeftInsertUnknownExtended = 513U,
				// Token: 0x04003381 RID: 13185
				LeftDeleteNullModifiedExtended = 130U,
				// Token: 0x04003382 RID: 13186
				LeftDeleteNullPreserveExtended = 258U,
				// Token: 0x04003383 RID: 13187
				LeftDeleteUnknownExtended = 514U,
				// Token: 0x04003384 RID: 13188
				LeftUnknownNullModifiedExtended = 160U,
				// Token: 0x04003385 RID: 13189
				LeftUnknownNullPreserveExtended = 288U,
				// Token: 0x04003386 RID: 13190
				RightInsertUnknownExtended = 36U,
				// Token: 0x04003387 RID: 13191
				RightDeleteUnknownExtended = 40U
			}

			// Token: 0x02000D8B RID: 3467
			private class JoinConditionVisitor : UpdateExpressionVisitor<object>
			{
				// Token: 0x06006F76 RID: 28534 RVA: 0x0017D927 File Offset: 0x0017BB27
				private JoinConditionVisitor()
				{
					this.m_leftKeySelectors = new List<DbExpression>();
					this.m_rightKeySelectors = new List<DbExpression>();
				}

				// Token: 0x170011C5 RID: 4549
				// (get) Token: 0x06006F77 RID: 28535 RVA: 0x0017D945 File Offset: 0x0017BB45
				protected override string VisitorName
				{
					get
					{
						return Propagator.JoinPropagator.JoinConditionVisitor._visitorName;
					}
				}

				// Token: 0x06006F78 RID: 28536 RVA: 0x0017D94C File Offset: 0x0017BB4C
				internal static void GetKeySelectors(DbExpression joinCondition, out ReadOnlyCollection<DbExpression> leftKeySelectors, out ReadOnlyCollection<DbExpression> rightKeySelectors)
				{
					Propagator.JoinPropagator.JoinConditionVisitor joinConditionVisitor = new Propagator.JoinPropagator.JoinConditionVisitor();
					joinCondition.Accept<object>(joinConditionVisitor);
					leftKeySelectors = new ReadOnlyCollection<DbExpression>(joinConditionVisitor.m_leftKeySelectors);
					rightKeySelectors = new ReadOnlyCollection<DbExpression>(joinConditionVisitor.m_rightKeySelectors);
				}

				// Token: 0x06006F79 RID: 28537 RVA: 0x0017D981 File Offset: 0x0017BB81
				public override object Visit(DbAndExpression node)
				{
					Check.NotNull<DbAndExpression>(node, "node");
					this.Visit(node.Left);
					this.Visit(node.Right);
					return null;
				}

				// Token: 0x06006F7A RID: 28538 RVA: 0x0017D9AC File Offset: 0x0017BBAC
				public override object Visit(DbComparisonExpression node)
				{
					Check.NotNull<DbComparisonExpression>(node, "node");
					if (DbExpressionKind.Equals == node.ExpressionKind)
					{
						this.m_leftKeySelectors.Add(node.Left);
						this.m_rightKeySelectors.Add(node.Right);
						return null;
					}
					throw base.ConstructNotSupportedException(node);
				}

				// Token: 0x04003388 RID: 13192
				private readonly List<DbExpression> m_leftKeySelectors;

				// Token: 0x04003389 RID: 13193
				private readonly List<DbExpression> m_rightKeySelectors;

				// Token: 0x0400338A RID: 13194
				private static readonly string _visitorName = typeof(Propagator.JoinPropagator.JoinConditionVisitor).FullName;
			}

			// Token: 0x02000D8C RID: 3468
			private enum PopulateMode
			{
				// Token: 0x0400338C RID: 13196
				NullModified,
				// Token: 0x0400338D RID: 13197
				NullPreserve,
				// Token: 0x0400338E RID: 13198
				Unknown
			}

			// Token: 0x02000D8D RID: 3469
			private static class PlaceholderPopulator
			{
				// Token: 0x06006F7C RID: 28540 RVA: 0x0017DA10 File Offset: 0x0017BC10
				internal static PropagatorResult Populate(PropagatorResult placeholder, CompositeKey key, CompositeKey placeholderKey, Propagator.JoinPropagator.PopulateMode mode)
				{
					bool isNull = mode == Propagator.JoinPropagator.PopulateMode.NullModified || mode == Propagator.JoinPropagator.PopulateMode.NullPreserve;
					bool flag = mode == Propagator.JoinPropagator.PopulateMode.NullPreserve || mode == Propagator.JoinPropagator.PopulateMode.Unknown;
					PropagatorFlags flags = PropagatorFlags.NoFlags;
					if (!isNull)
					{
						flags |= PropagatorFlags.Unknown;
					}
					if (flag)
					{
						flags |= PropagatorFlags.Preserve;
					}
					return placeholder.Replace(delegate(PropagatorResult node)
					{
						int num = -1;
						for (int i = 0; i < placeholderKey.KeyComponents.Length; i++)
						{
							if (placeholderKey.KeyComponents[i] == node)
							{
								num = i;
								break;
							}
						}
						if (num != -1)
						{
							return key.KeyComponents[num];
						}
						object obj = (isNull ? null : node.GetSimpleValue());
						return PropagatorResult.CreateSimpleValue(flags, obj);
					});
				}
			}
		}
	}
}
