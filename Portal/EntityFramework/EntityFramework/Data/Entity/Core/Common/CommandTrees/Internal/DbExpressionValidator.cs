using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F3 RID: 1779
	internal sealed class DbExpressionValidator : DbExpressionRebinder
	{
		// Token: 0x060052AD RID: 21165 RVA: 0x00128C14 File Offset: 0x00126E14
		internal DbExpressionValidator(MetadataWorkspace metadata, DataSpace expectedDataSpace)
			: base(metadata)
		{
			this.requiredSpace = expectedDataSpace;
			this.allowedFunctionSpaces = new DataSpace[]
			{
				DataSpace.CSpace,
				DataSpace.SSpace
			};
			if (expectedDataSpace == DataSpace.SSpace)
			{
				this.allowedMetadataSpaces = new DataSpace[]
				{
					DataSpace.SSpace,
					DataSpace.CSpace
				};
				return;
			}
			this.allowedMetadataSpaces = new DataSpace[] { DataSpace.CSpace };
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x00128C82 File Offset: 0x00126E82
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this.paramMappings;
			}
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00128C8A File Offset: 0x00126E8A
		internal void ValidateExpression(DbExpression expression, string argumentName)
		{
			this.expressionArgumentName = argumentName;
			this.VisitExpression(expression);
			this.expressionArgumentName = null;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x00128CA2 File Offset: 0x00126EA2
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return this.ValidateMetadata<EntitySetBase>(entitySet, new Func<EntitySetBase, EntitySetBase>(base.VisitEntitySet), (EntitySetBase es) => es.EntityContainer.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x00128CDC File Offset: 0x00126EDC
		protected override EdmFunction VisitFunction(EdmFunction function)
		{
			return this.ValidateMetadata<EdmFunction>(function, new Func<EdmFunction, EdmFunction>(base.VisitFunction), (EdmFunction func) => func.DataSpace, this.allowedFunctionSpaces);
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00128D16 File Offset: 0x00126F16
		protected override EdmType VisitType(EdmType type)
		{
			return this.ValidateMetadata<EdmType>(type, new Func<EdmType, EdmType>(base.VisitType), (EdmType et) => et.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00128D50 File Offset: 0x00126F50
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return this.ValidateMetadata<TypeUsage>(type, new Func<TypeUsage, TypeUsage>(base.VisitTypeUsage), (TypeUsage tu) => tu.EdmType.DataSpace, this.allowedMetadataSpaces);
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x00128D8C File Offset: 0x00126F8C
		protected override void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
			Dictionary<string, TypeUsage> dictionary = scopeVariables.ToDictionary((DbVariableReferenceExpression var) => var.VariableName, (DbVariableReferenceExpression var) => var.ResultType, StringComparer.Ordinal);
			this.variableScopes.Push(dictionary);
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x00128DEF File Offset: 0x00126FEF
		protected override void OnExitScope()
		{
			this.variableScopes.Pop();
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x00128E00 File Offset: 0x00127000
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			DbExpression dbExpression = base.Visit(expression);
			if (dbExpression.ExpressionKind == DbExpressionKind.VariableReference)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)dbExpression;
				TypeUsage typeUsage = null;
				using (Stack<Dictionary<string, TypeUsage>>.Enumerator enumerator = this.variableScopes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TryGetValue(dbVariableReferenceExpression.VariableName, out typeUsage))
						{
							break;
						}
					}
				}
				if (typeUsage == null)
				{
					this.ThrowInvalid(Strings.Cqt_Validator_VarRefInvalid(dbVariableReferenceExpression.VariableName));
				}
				if (!TypeSemantics.IsEqual(dbVariableReferenceExpression.ResultType, typeUsage))
				{
					this.ThrowInvalid(Strings.Cqt_Validator_VarRefTypeMismatch(dbVariableReferenceExpression.VariableName));
				}
			}
			return dbExpression;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x00128EB8 File Offset: 0x001270B8
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			DbExpression dbExpression = base.Visit(expression);
			if (dbExpression.ExpressionKind == DbExpressionKind.ParameterReference)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression = dbExpression as DbParameterReferenceExpression;
				DbParameterReferenceExpression dbParameterReferenceExpression2;
				if (this.paramMappings.TryGetValue(dbParameterReferenceExpression.ParameterName, out dbParameterReferenceExpression2))
				{
					if (!TypeSemantics.IsEqual(dbParameterReferenceExpression.ResultType, dbParameterReferenceExpression2.ResultType))
					{
						this.ThrowInvalid(Strings.Cqt_Validator_InvalidIncompatibleParameterReferences(dbParameterReferenceExpression.ParameterName));
					}
				}
				else
				{
					this.paramMappings.Add(dbParameterReferenceExpression.ParameterName, dbParameterReferenceExpression);
				}
			}
			return dbExpression;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00128F38 File Offset: 0x00127138
		private TMetadata ValidateMetadata<TMetadata>(TMetadata metadata, Func<TMetadata, TMetadata> map, Func<TMetadata, DataSpace> getDataSpace, DataSpace[] allowedSpaces)
		{
			TMetadata tmetadata = map(metadata);
			if (metadata != tmetadata)
			{
				this.ThrowInvalidMetadata<TMetadata>();
			}
			DataSpace resultSpace = getDataSpace(tmetadata);
			if (!allowedSpaces.Any((DataSpace ds) => ds == resultSpace))
			{
				this.ThrowInvalidSpace<TMetadata>();
			}
			return tmetadata;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00128F90 File Offset: 0x00127190
		private void ThrowInvalidMetadata<TMetadata>()
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidOtherWorkspaceMetadata(typeof(TMetadata).Name));
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00128FAC File Offset: 0x001271AC
		private void ThrowInvalidSpace<TMetadata>()
		{
			this.ThrowInvalid(Strings.Cqt_Validator_InvalidIncorrectDataSpaceMetadata(typeof(TMetadata).Name, Enum.GetName(typeof(DataSpace), this.requiredSpace)));
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00128FE2 File Offset: 0x001271E2
		private void ThrowInvalid(string message)
		{
			throw new ArgumentException(message, this.expressionArgumentName);
		}

		// Token: 0x04001DE1 RID: 7649
		private readonly DataSpace requiredSpace;

		// Token: 0x04001DE2 RID: 7650
		private readonly DataSpace[] allowedMetadataSpaces;

		// Token: 0x04001DE3 RID: 7651
		private readonly DataSpace[] allowedFunctionSpaces;

		// Token: 0x04001DE4 RID: 7652
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();

		// Token: 0x04001DE5 RID: 7653
		private readonly Stack<Dictionary<string, TypeUsage>> variableScopes = new Stack<Dictionary<string, TypeUsage>>();

		// Token: 0x04001DE6 RID: 7654
		private string expressionArgumentName;
	}
}
