using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000645 RID: 1605
	internal static class CqlErrorHelper
	{
		// Token: 0x06004D21 RID: 19745 RVA: 0x00110848 File Offset: 0x0010EA48
		internal static void ReportFunctionOverloadError(MethodExpr functionExpr, EdmFunction functionType, List<TypeUsage> argTypes)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionType.Name).Append("(");
			for (int i = 0; i < argTypes.Count; i++)
			{
				stringBuilder.Append(text);
				stringBuilder.Append((argTypes[i] != null) ? argTypes[i].EdmType.FullName : "NULL");
				text = ", ";
			}
			stringBuilder.Append(")");
			Func<object, object, object, string> func;
			if (TypeSemantics.IsAggregateFunction(functionType))
			{
				func = (TypeHelpers.IsCanonicalFunction(functionType) ? new Func<object, object, object, string>(Strings.NoCanonicalAggrFunctionOverloadMatch) : new Func<object, object, object, string>(Strings.NoAggrFunctionOverloadMatch));
			}
			else
			{
				func = (TypeHelpers.IsCanonicalFunction(functionType) ? new Func<object, object, object, string>(Strings.NoCanonicalFunctionOverloadMatch) : new Func<object, object, object, string>(Strings.NoFunctionOverloadMatch));
			}
			throw EntitySqlException.Create(functionExpr.ErrCtx.CommandText, func(functionType.NamespaceName, functionType.Name, stringBuilder.ToString()), functionExpr.ErrCtx.InputPosition, Strings.CtxFunction(functionType.Name), false, null);
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0011095B File Offset: 0x0010EB5B
		internal static void ReportAliasAlreadyUsedError(string aliasName, ErrorContext errCtx, string contextMessage)
		{
			throw EntitySqlException.Create(errCtx, string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Strings.AliasNameAlreadyUsed(aliasName),
				contextMessage
			}), null);
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x00110986 File Offset: 0x0010EB86
		internal static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage leftType, TypeUsage rightType)
		{
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, leftType, rightType, leftType, rightType);
			throw EntitySqlException.Create(errCtx, Strings.ArgumentTypesAreIncompatible(leftType.Identity, rightType.Identity), null);
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x001109AC File Offset: 0x0010EBAC
		private static void ReportIncompatibleCommonType(ErrorContext errCtx, TypeUsage rootLeftType, TypeUsage rootRightType, TypeUsage leftType, TypeUsage rightType)
		{
			TypeUsage typeUsage = null;
			bool flag = rootLeftType == leftType;
			string text = string.Empty;
			if (leftType.EdmType.BuiltInTypeKind != rightType.EdmType.BuiltInTypeKind)
			{
				throw EntitySqlException.Create(errCtx, Strings.TypeKindMismatch(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType)), null);
			}
			BuiltInTypeKind builtInTypeKind = leftType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind != BuiltInTypeKind.ComplexType)
					{
						goto IL_0270;
					}
					ComplexType complexType = (ComplexType)leftType.EdmType;
					ComplexType complexType2 = (ComplexType)rightType.EdmType;
					if (complexType.Members.Count != complexType2.Members.Count)
					{
						if (flag)
						{
							text = Strings.InvalidRootComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(complexType2));
						}
						else
						{
							text = Strings.InvalidComplexType(CqlErrorHelper.GetReadableTypeName(complexType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(complexType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntitySqlException.Create(errCtx, text, null);
					}
					for (int i = 0; i < complexType.Members.Count; i++)
					{
						CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, complexType.Members[i].TypeUsage, complexType2.Members[i].TypeUsage);
					}
					return;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RefType)
				{
					if (builtInTypeKind != BuiltInTypeKind.RowType)
					{
						goto IL_0270;
					}
					RowType rowType = (RowType)leftType.EdmType;
					RowType rowType2 = (RowType)rightType.EdmType;
					if (rowType.Members.Count != rowType2.Members.Count)
					{
						if (flag)
						{
							text = Strings.InvalidRootRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rowType2));
						}
						else
						{
							text = Strings.InvalidRowType(CqlErrorHelper.GetReadableTypeName(rowType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rowType2), CqlErrorHelper.GetReadableTypeName(rootRightType));
						}
						throw EntitySqlException.Create(errCtx, text, null);
					}
					for (int j = 0; j < rowType.Members.Count; j++)
					{
						CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, rowType.Members[j].TypeUsage, rowType2.Members[j].TypeUsage);
					}
					return;
				}
			}
			else
			{
				if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
				{
					if (flag)
					{
						text = Strings.InvalidEntityRootTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rightType));
					}
					else
					{
						text = Strings.InvalidEntityTypeArgument(CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
					}
					throw EntitySqlException.Create(errCtx, text, null);
				}
				return;
			}
			CqlErrorHelper.ReportIncompatibleCommonType(errCtx, rootLeftType, rootRightType, TypeHelpers.GetElementTypeUsage(leftType), TypeHelpers.GetElementTypeUsage(rightType));
			return;
			IL_0270:
			if (!TypeSemantics.TryGetCommonType(leftType, rightType, out typeUsage))
			{
				if (flag)
				{
					text = Strings.InvalidPlaceholderRootTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType));
				}
				else
				{
					text = Strings.InvalidPlaceholderTypeArgument(CqlErrorHelper.GetReadableTypeKind(leftType), CqlErrorHelper.GetReadableTypeName(leftType), CqlErrorHelper.GetReadableTypeName(rootLeftType), CqlErrorHelper.GetReadableTypeKind(rightType), CqlErrorHelper.GetReadableTypeName(rightType), CqlErrorHelper.GetReadableTypeName(rootRightType));
				}
				throw EntitySqlException.Create(errCtx, text, null);
			}
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x00110C8F File Offset: 0x0010EE8F
		private static string GetReadableTypeName(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeName(type.EdmType);
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x00110C9C File Offset: 0x0010EE9C
		private static string GetReadableTypeName(EdmType type)
		{
			if (type.BuiltInTypeKind == BuiltInTypeKind.RowType || type.BuiltInTypeKind == BuiltInTypeKind.CollectionType || type.BuiltInTypeKind == BuiltInTypeKind.RefType)
			{
				return type.Name;
			}
			return type.FullName;
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00110CC8 File Offset: 0x0010EEC8
		private static string GetReadableTypeKind(TypeUsage type)
		{
			return CqlErrorHelper.GetReadableTypeKind(type.EdmType);
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00110CD8 File Offset: 0x0010EED8
		private static string GetReadableTypeKind(EdmType type)
		{
			string text = string.Empty;
			BuiltInTypeKind builtInTypeKind = type.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind == BuiltInTypeKind.CollectionType)
				{
					text = Strings.LocalizedCollection;
					goto IL_0075;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					text = Strings.LocalizedComplex;
					goto IL_0075;
				}
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					text = Strings.LocalizedEntity;
					goto IL_0075;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					text = Strings.LocalizedPrimitive;
					goto IL_0075;
				}
				if (builtInTypeKind == BuiltInTypeKind.RefType)
				{
					text = Strings.LocalizedReference;
					goto IL_0075;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					text = Strings.LocalizedRow;
					goto IL_0075;
				}
			}
			text = type.BuiltInTypeKind.ToString();
			IL_0075:
			return text + " " + Strings.LocalizedType;
		}
	}
}
