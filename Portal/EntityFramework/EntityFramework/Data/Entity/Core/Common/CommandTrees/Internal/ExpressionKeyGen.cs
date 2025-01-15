using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006EC RID: 1772
	internal sealed class ExpressionKeyGen : DbExpressionVisitor
	{
		// Token: 0x0600524D RID: 21069 RVA: 0x00126BB4 File Offset: 0x00124DB4
		internal static bool TryGenerateKey(DbExpression tree, out string key)
		{
			ExpressionKeyGen expressionKeyGen = new ExpressionKeyGen();
			bool flag;
			try
			{
				tree.Accept(expressionKeyGen);
				key = expressionKeyGen._key.ToString();
				flag = true;
			}
			catch (NotSupportedException)
			{
				key = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x00126BF8 File Offset: 0x00124DF8
		internal ExpressionKeyGen()
		{
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x00126C0C File Offset: 0x00124E0C
		private static string[] InitializeExprKindNames()
		{
			string[] names = Enum.GetNames(typeof(DbExpressionKind));
			names[10] = "/";
			names[33] = "%";
			names[34] = "*";
			names[44] = "+";
			names[32] = "-";
			names[54] = "-";
			names[13] = "=";
			names[28] = "<";
			names[29] = "<=";
			names[18] = ">";
			names[19] = ">=";
			names[37] = "<>";
			names[46] = ".";
			names[21] = "IJ";
			names[16] = "FOJ";
			names[27] = "LOJ";
			names[6] = "CA";
			names[42] = "OA";
			return names;
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x00126CC9 File Offset: 0x00124EC9
		internal string Key
		{
			get
			{
				return this._key.ToString();
			}
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x00126CD6 File Offset: 0x00124ED6
		private void VisitVariableName(string varName)
		{
			this._key.Append('\'');
			this._key.Append(varName.Replace("'", "''"));
			this._key.Append('\'');
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x00126D10 File Offset: 0x00124F10
		private void VisitBinding(DbExpressionBinding binding)
		{
			this._key.Append("BV");
			this.VisitVariableName(binding.VariableName);
			this._key.Append("=(");
			binding.Expression.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x00126D68 File Offset: 0x00124F68
		private void VisitGroupBinding(DbGroupExpressionBinding groupBinding)
		{
			this._key.Append("GBVV");
			this.VisitVariableName(groupBinding.VariableName);
			this._key.Append(",");
			this.VisitVariableName(groupBinding.GroupVariableName);
			this._key.Append("=(");
			groupBinding.Expression.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00126DDC File Offset: 0x00124FDC
		private void VisitFunction(EdmFunction func, IList<DbExpression> args)
		{
			this._key.Append("FUNC<");
			this._key.Append(func.Identity);
			this._key.Append(">:ARGS(");
			foreach (DbExpression dbExpression in args)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			this._key.Append(')');
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x00126E80 File Offset: 0x00125080
		private void VisitExprKind(DbExpressionKind kind)
		{
			this._key.Append('[');
			this._key.Append(ExpressionKeyGen._exprKindNames[(int)kind]);
			this._key.Append(']');
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x00126EB1 File Offset: 0x001250B1
		private void VisitUnary(DbUnaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x00126EE8 File Offset: 0x001250E8
		private void VisitBinary(DbBinaryExpression expr)
		{
			this.VisitExprKind(expr.ExpressionKind);
			this._key.Append('(');
			expr.Left.Accept(this);
			this._key.Append(',');
			expr.Right.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x00126F44 File Offset: 0x00125144
		private void VisitCastOrTreat(DbUnaryExpression e)
		{
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00126FAD File Offset: 0x001251AD
		public override void Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00126FD0 File Offset: 0x001251D0
		public override void Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			switch (((PrimitiveType)TypeHelpers.GetPrimitiveTypeUsageForScalar(e.ResultType).EdmType).PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				byte[] array = e.Value as byte[];
				if (array == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append("'");
				foreach (byte b in array)
				{
					this._key.AppendFormat("{0:X2}", b);
				}
				this._key.Append("'");
				break;
			}
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
			case PrimitiveTypeKind.Decimal:
			case PrimitiveTypeKind.Double:
			case PrimitiveTypeKind.Guid:
			case PrimitiveTypeKind.Single:
			case PrimitiveTypeKind.SByte:
			case PrimitiveTypeKind.Int16:
			case PrimitiveTypeKind.Int32:
			case PrimitiveTypeKind.Int64:
			case PrimitiveTypeKind.Time:
				this._key.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[] { e.Value });
				break;
			case PrimitiveTypeKind.DateTime:
				this._key.Append(((DateTime)e.Value).ToString("o", CultureInfo.InvariantCulture));
				break;
			case PrimitiveTypeKind.String:
			{
				string text = e.Value as string;
				if (text == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append("'");
				this._key.Append(text.Replace("'", "''"));
				this._key.Append("'");
				break;
			}
			case PrimitiveTypeKind.DateTimeOffset:
				this._key.Append(((DateTimeOffset)e.Value).ToString("o", CultureInfo.InvariantCulture));
				break;
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
			{
				DbGeometry dbGeometry = e.Value as DbGeometry;
				if (dbGeometry == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append(dbGeometry.AsText());
				break;
			}
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
			{
				DbGeography dbGeography = e.Value as DbGeography;
				if (dbGeography == null)
				{
					throw new NotSupportedException();
				}
				this._key.Append(dbGeography.AsText());
				break;
			}
			case PrimitiveTypeKind.HierarchyId:
			{
				HierarchyId hierarchyId = e.Value as HierarchyId;
				if (!(hierarchyId != null))
				{
					throw new NotSupportedException();
				}
				this._key.Append(hierarchyId);
				break;
			}
			default:
				throw new NotSupportedException();
			}
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0012728F File Offset: 0x0012548F
		public override void Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			this._key.Append("NULL:");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x001272C5 File Offset: 0x001254C5
		public override void Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			this._key.Append("Var(");
			this.VisitVariableName(e.VariableName);
			this._key.Append(")");
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x00127304 File Offset: 0x00125504
		public override void Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			this._key.Append("@");
			this._key.Append(e.ParameterName);
			this._key.Append(":");
			this._key.Append(e.ResultType.Identity);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x00127368 File Offset: 0x00125568
		public override void Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			this.VisitFunction(e.Function, e.Arguments);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00127388 File Offset: 0x00125588
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			this._key.Append("Lambda(");
			foreach (DbVariableReferenceExpression dbVariableReferenceExpression in expression.Lambda.Variables)
			{
				this._key.Append("(V");
				this.VisitVariableName(dbVariableReferenceExpression.VariableName);
				this._key.Append(":");
				this._key.Append(dbVariableReferenceExpression.ResultType.Identity);
				this._key.Append(')');
			}
			this._key.Append("=");
			foreach (DbExpression dbExpression in expression.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			this._key.Append(")Body(");
			expression.Lambda.Body.Accept(this);
			this._key.Append(")");
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x001274E0 File Offset: 0x001256E0
		public override void Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			e.Instance.Accept(this);
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append(e.Property.Name);
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0012751D File Offset: 0x0012571D
		public override void Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00127534 File Offset: 0x00125734
		public override void Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(")(");
			e.Pattern.Accept(this);
			this._key.Append(")(");
			if (e.Escape != null)
			{
				e.Escape.Accept(this);
			}
			e.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x001275D0 File Offset: 0x001257D0
		public override void Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			if (e.WithTies)
			{
				this._key.Append("WithTies");
			}
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(")(");
			e.Limit.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x00127653 File Offset: 0x00125853
		public override void Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x00127668 File Offset: 0x00125868
		public override void Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			foreach (DbExpression dbExpression in e.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x001276E8 File Offset: 0x001258E8
		public override void Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x001276FD File Offset: 0x001258FD
		public override void Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00127714 File Offset: 0x00125914
		public override void Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Item.Accept(this);
			this._key.Append(",(");
			bool flag = true;
			foreach (DbExpression dbExpression in e.List)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this._key.Append(',');
				}
				dbExpression.Accept(this);
			}
			this._key.Append("))");
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x001277CC File Offset: 0x001259CC
		public override void Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x001277E1 File Offset: 0x001259E1
		public override void Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x001277F6 File Offset: 0x001259F6
		public override void Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0012780B File Offset: 0x00125A0B
		public override void Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x00127820 File Offset: 0x00125A20
		public override void Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00127835 File Offset: 0x00125A35
		public override void Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0012784A File Offset: 0x00125A4A
		public override void Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			this.VisitBinary(e);
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0012785F File Offset: 0x00125A5F
		public override void Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			this.VisitCastOrTreat(e);
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00127874 File Offset: 0x00125A74
		public override void Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			this.VisitCastOrTreat(e);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0012788C File Offset: 0x00125A8C
		public override void Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x00127908 File Offset: 0x00125B08
		public override void Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.Argument.Accept(this);
			this._key.Append(":");
			this._key.Append(e.OfType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00127984 File Offset: 0x00125B84
		public override void Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			for (int i = 0; i < e.When.Count; i++)
			{
				this._key.Append("WHEN:(");
				e.When[i].Accept(this);
				this._key.Append(")THEN:(");
				e.Then[i].Accept(this);
			}
			this._key.Append("ELSE:(");
			e.Else.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00127A44 File Offset: 0x00125C44
		public override void Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append(':');
			this._key.Append(e.ResultType.EdmType.Identity);
			this._key.Append('(');
			foreach (DbExpression dbExpression in e.Arguments)
			{
				this._key.Append('(');
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			if (e.HasRelatedEntityReferences)
			{
				foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
				{
					this._key.Append("RE(A(");
					this._key.Append(dbRelatedEntityRef.SourceEnd.DeclaringType.Identity);
					this._key.Append(")(");
					this._key.Append(dbRelatedEntityRef.SourceEnd.Name);
					this._key.Append("->");
					this._key.Append(dbRelatedEntityRef.TargetEnd.Name);
					this._key.Append(")(");
					dbRelatedEntityRef.TargetEntityReference.Accept(this);
					this._key.Append("))");
				}
			}
			this._key.Append(')');
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x00127BFC File Offset: 0x00125DFC
		public override void Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append("(ESET(");
			this._key.Append(e.EntitySet.EntityContainer.Name);
			this._key.Append('.');
			this._key.Append(e.EntitySet.Name);
			this._key.Append(")T(");
			this._key.Append(TypeHelpers.GetEdmType<RefType>(e.ResultType).ElementType.FullName);
			this._key.Append(")(");
			e.Argument.Accept(this);
			this._key.Append(')');
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00127CD0 File Offset: 0x00125ED0
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			e.NavigationSource.Accept(this);
			this._key.Append(")A(");
			this._key.Append(e.NavigateFrom.DeclaringType.Identity);
			this._key.Append(")(");
			this._key.Append(e.NavigateFrom.Name);
			this._key.Append("->");
			this._key.Append(e.NavigateTo.Name);
			this._key.Append("))");
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00127D9D File Offset: 0x00125F9D
		public override void Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00127DB2 File Offset: 0x00125FB2
		public override void Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00127DC7 File Offset: 0x00125FC7
		public override void Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			this.VisitUnary(e);
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00127DDC File Offset: 0x00125FDC
		public override void Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this._key.Append(e.Target.EntityContainer.Name);
			this._key.Append('.');
			this._key.Append(e.Target.Name);
			this._key.Append(':');
			this._key.Append(e.ResultType.EdmType.Identity);
			this._key.Append(')');
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00127E88 File Offset: 0x00126088
		public override void Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00127EF4 File Offset: 0x001260F4
		public override void Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Projection.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00127F60 File Offset: 0x00126160
		public override void Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			foreach (DbExpressionBinding dbExpressionBinding in e.Inputs)
			{
				this.VisitBinding(dbExpressionBinding);
			}
			this._key.Append(')');
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00127FE4 File Offset: 0x001261E4
		public override void Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Left);
			this.VisitBinding(e.Right);
			this._key.Append('(');
			e.JoinCondition.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0012805C File Offset: 0x0012625C
		public override void Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitBinding(e.Apply);
			this._key.Append(')');
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x001280B8 File Offset: 0x001262B8
		public override void Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitGroupBinding(e.Input);
			foreach (DbExpression dbExpression in e.Keys)
			{
				this._key.Append("K(");
				dbExpression.Accept(this);
				this._key.Append(')');
			}
			foreach (DbAggregate dbAggregate in e.Aggregates)
			{
				DbGroupAggregate dbGroupAggregate = dbAggregate as DbGroupAggregate;
				if (dbGroupAggregate != null)
				{
					this._key.Append("GA(");
					dbGroupAggregate.Arguments[0].Accept(this);
					this._key.Append(')');
				}
				else
				{
					this._key.Append("A:");
					DbFunctionAggregate dbFunctionAggregate = (DbFunctionAggregate)dbAggregate;
					if (dbFunctionAggregate.Distinct)
					{
						this._key.Append("D:");
					}
					this.VisitFunction(dbFunctionAggregate.Function, dbFunctionAggregate.Arguments);
				}
			}
			this._key.Append(')');
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00128224 File Offset: 0x00126424
		private void VisitSortOrder(IList<DbSortClause> sortOrder)
		{
			this._key.Append("SO(");
			foreach (DbSortClause dbSortClause in sortOrder)
			{
				this._key.Append(dbSortClause.Ascending ? "ASC(" : "DESC(");
				dbSortClause.Expression.Accept(this);
				this._key.Append(')');
				if (!string.IsNullOrEmpty(dbSortClause.Collation))
				{
					this._key.Append(":(");
					this._key.Append(dbSortClause.Collation);
					this._key.Append(')');
				}
			}
			this._key.Append(')');
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00128300 File Offset: 0x00126500
		public override void Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append('(');
			e.Count.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00128378 File Offset: 0x00126578
		public override void Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this.VisitSortOrder(e.SortOrder);
			this._key.Append(')');
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x001283D4 File Offset: 0x001265D4
		public override void Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			this.VisitExprKind(e.ExpressionKind);
			this._key.Append('(');
			this.VisitBinding(e.Input);
			this._key.Append('(');
			e.Predicate.Accept(this);
			this._key.Append("))");
		}

		// Token: 0x04001DD9 RID: 7641
		private readonly StringBuilder _key = new StringBuilder();

		// Token: 0x04001DDA RID: 7642
		private static readonly string[] _exprKindNames = ExpressionKeyGen.InitializeExprKindNames();
	}
}
