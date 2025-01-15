using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006C9 RID: 1737
	public sealed class DbLambda
	{
		// Token: 0x060050FA RID: 20730 RVA: 0x001222BD File Offset: 0x001204BD
		internal DbLambda(ReadOnlyCollection<DbVariableReferenceExpression> variables, DbExpression bodyExp)
		{
			this._variables = variables;
			this._body = bodyExp;
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x001222D3 File Offset: 0x001204D3
		public DbExpression Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x001222DB File Offset: 0x001204DB
		public IList<DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x001222E3 File Offset: 0x001204E3
		public static DbLambda Create(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x001222EC File Offset: 0x001204EC
		public static DbLambda Create(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x001222F8 File Offset: 0x001204F8
		public static DbLambda Create(TypeUsage argument1Type, Func<DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<Func<DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0]), array);
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x00122344 File Offset: 0x00120544
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, Func<DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1]), array);
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x001223A4 File Offset: 0x001205A4
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, Func<DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2]), array);
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00122414 File Offset: 0x00120614
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3]), array);
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x0012249C File Offset: 0x0012069C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4]), array);
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x00122538 File Offset: 0x00120738
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5]), array);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x001225E8 File Offset: 0x001207E8
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6]), array);
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x001226B0 File Offset: 0x001208B0
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]), array);
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x0012278C File Offset: 0x0012098C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8]), array);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0012287C File Offset: 0x00120A7C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[] { argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type });
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9]), array);
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x00122984 File Offset: 0x00120B84
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10]), array);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00122AA4 File Offset: 0x00120CA4
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type, argument12Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11]), array);
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x00122BDC File Offset: 0x00120DDC
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type, argument12Type, argument13Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12]), array);
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x00122D28 File Offset: 0x00120F28
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type, argument12Type, argument13Type, argument14Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13]), array);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00122E8C File Offset: 0x0012108C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<TypeUsage>(argument15Type, "argument15Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type, argument12Type, argument13Type, argument14Type, argument15Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14]), array);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00123008 File Offset: 0x00121208
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, TypeUsage argument16Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<TypeUsage>(argument15Type, "argument15Type");
			Check.NotNull<TypeUsage>(argument16Type, "argument16Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type, argument2Type, argument3Type, argument4Type, argument5Type, argument6Type, argument7Type, argument8Type, argument9Type, argument10Type,
				argument11Type, argument12Type, argument13Type, argument14Type, argument15Type, argument16Type
			});
			return DbExpressionBuilder.Lambda(lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15]), array);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x0012319C File Offset: 0x0012139C
		private static DbVariableReferenceExpression[] CreateVariables(MethodInfo lambdaMethod, params TypeUsage[] argumentTypes)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(lambdaMethod);
			DbVariableReferenceExpression[] array2 = new DbVariableReferenceExpression[argumentTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = argumentTypes[i].Variable(array[i]);
			}
			return array2;
		}

		// Token: 0x04001DA5 RID: 7589
		private readonly ReadOnlyCollection<DbVariableReferenceExpression> _variables;

		// Token: 0x04001DA6 RID: 7590
		private readonly DbExpression _body;
	}
}
