using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F5 RID: 757
	internal class FunctionImportElement : Function
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x00066355 File Offset: 0x00064555
		internal FunctionImportElement(EntityContainer container)
			: base(container.Schema)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
			this._container = container;
			this._isComposable = false;
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00066394 File Offset: 0x00064594
		public override bool IsFunctionImport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x00066397 File Offset: 0x00064597
		public override string FQName
		{
			get
			{
				return this._container.Name + "." + this.Name;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000663B4 File Offset: 0x000645B4
		public override string Identity
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000663BC File Offset: 0x000645BC
		public EntityContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x000663C4 File Offset: 0x000645C4
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000663CC File Offset: 0x000645CC
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				string text;
				if (Utils.GetString(base.Schema, reader, out text))
				{
					this._unresolvedEntitySet = text;
				}
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySetPath"))
			{
				string text2;
				if (Utils.GetString(base.Schema, reader, out text2))
				{
					this._entitySetPathDefined = true;
				}
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsBindable"))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsSideEffecting"))
			{
				bool flag = true;
				if (base.HandleBoolAttribute(reader, ref flag))
				{
					this._isSideEffecting = new bool?(flag);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00066468 File Offset: 0x00064668
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.ResolveEntitySet(this, this._unresolvedEntitySet, ref this._entitySet);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00066483 File Offset: 0x00064683
		internal void ResolveEntitySet(SchemaElement owner, string unresolvedEntitySet, ref EntityContainerEntitySet entitySet)
		{
			if (entitySet == null && unresolvedEntitySet != null)
			{
				entitySet = this._container.FindEntitySet(unresolvedEntitySet);
				if (entitySet == null)
				{
					owner.AddError(ErrorCode.FunctionImportUnknownEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportUnknownEntitySet(unresolvedEntitySet, this.FQName));
				}
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000664B8 File Offset: 0x000646B8
		internal override void Validate()
		{
			base.Validate();
			this.ValidateFunctionImportReturnType(this, this._type, base.CollectionKind, this._entitySet, this._entitySetPathDefined);
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					this.ValidateFunctionImportReturnType(returnType, returnType.Type, returnType.CollectionKind, returnType.EntitySet, returnType.EntitySetPathDefined);
				}
			}
			if (this._isComposable && this._isSideEffecting != null && this._isSideEffecting.Value)
			{
				base.AddError(ErrorCode.FunctionImportComposableAndSideEffectingNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportComposableAndSideEffectingNotAllowed(this.FQName));
			}
			if (this._parameters != null)
			{
				foreach (Parameter parameter in this._parameters)
				{
					if (parameter.IsRefType || parameter.CollectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.FunctionImportCollectionAndRefParametersNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportCollectionAndRefParametersNotAllowed(this.FQName));
					}
					if (!parameter.TypeUsageBuilder.Nullable)
					{
						base.AddError(ErrorCode.FunctionImportNonNullableParametersNotAllowed, EdmSchemaErrorSeverity.Error, Strings.FunctionImportNonNullableParametersNotAllowed(this.FQName));
					}
				}
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00066614 File Offset: 0x00064814
		private void ValidateFunctionImportReturnType(SchemaElement owner, SchemaType returnType, CollectionKind returnTypeCollectionKind, EntityContainerEntitySet entitySet, bool entitySetPathDefined)
		{
			if (returnType != null && !this.ReturnTypeMeetsFunctionImportBasicRequirements(returnType, returnTypeCollectionKind))
			{
				owner.AddError(ErrorCode.FunctionImportUnsupportedReturnType, EdmSchemaErrorSeverity.Error, owner, this.GetReturnTypeErrorMessage(this.Name));
			}
			this.ValidateFunctionImportReturnType(owner, returnType, entitySet, entitySetPathDefined);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00066648 File Offset: 0x00064848
		private bool ReturnTypeMeetsFunctionImportBasicRequirements(SchemaType type, CollectionKind returnTypeCollectionKind)
		{
			if (type is ScalarType && returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (type is SchemaEntityType && returnTypeCollectionKind == CollectionKind.Bag)
			{
				return true;
			}
			if (base.Schema.SchemaVersion == 1.1)
			{
				if (type is ScalarType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaEntityType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.None)
				{
					return true;
				}
				if (type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.Bag)
				{
					return true;
				}
			}
			return (base.Schema.SchemaVersion >= 2.0 && type is SchemaComplexType && returnTypeCollectionKind == CollectionKind.Bag) || (base.Schema.SchemaVersion >= 3.0 && type is SchemaEnumType && returnTypeCollectionKind == CollectionKind.Bag);
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00066708 File Offset: 0x00064908
		private void ValidateFunctionImportReturnType(SchemaElement owner, SchemaType returnType, EntityContainerEntitySet entitySet, bool entitySetPathDefined)
		{
			SchemaEntityType schemaEntityType = returnType as SchemaEntityType;
			if (entitySet != null && entitySetPathDefined)
			{
				owner.AddError(ErrorCode.FunctionImportEntitySetAndEntitySetPathDeclared, EdmSchemaErrorSeverity.Error, Strings.FunctionImportEntitySetAndEntitySetPathDeclared(this.FQName));
			}
			if (schemaEntityType != null)
			{
				if (entitySet == null)
				{
					owner.AddError(ErrorCode.FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(this.FQName));
					return;
				}
				if (entitySet.EntityType != null && !schemaEntityType.IsOfType(entitySet.EntityType))
				{
					owner.AddError(ErrorCode.FunctionImportEntityTypeDoesNotMatchEntitySet, EdmSchemaErrorSeverity.Error, Strings.FunctionImportEntityTypeDoesNotMatchEntitySet(this.FQName, entitySet.EntityType.FQName, entitySet.Name));
					return;
				}
			}
			else
			{
				SchemaComplexType schemaComplexType = returnType as SchemaComplexType;
				if (schemaComplexType != null)
				{
					if (entitySet != null || entitySetPathDefined)
					{
						owner.AddError(ErrorCode.ComplexTypeAsReturnTypeAndDefinedEntitySet, EdmSchemaErrorSeverity.Error, owner.LineNumber, owner.LinePosition, Strings.ComplexTypeAsReturnTypeAndDefinedEntitySet(this.FQName, schemaComplexType.Name));
						return;
					}
				}
				else if (entitySet != null || entitySetPathDefined)
				{
					owner.AddError(ErrorCode.FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType, EdmSchemaErrorSeverity.Error, Strings.FunctionImportSpecifiesEntitySetButNotEntityType(this.FQName));
				}
			}
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000667FC File Offset: 0x000649FC
		private string GetReturnTypeErrorMessage(string functionName)
		{
			string text;
			if (base.Schema.SchemaVersion == 1.0)
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV1(functionName);
			}
			else if (base.Schema.SchemaVersion == 1.1)
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV1_1(functionName);
			}
			else
			{
				text = Strings.FunctionImportWithUnsupportedReturnTypeV2(functionName);
			}
			return text;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00066850 File Offset: 0x00064A50
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			FunctionImportElement functionImportElement = new FunctionImportElement((EntityContainer)parentElement);
			base.CloneSetFunctionFields(functionImportElement);
			functionImportElement._container = this._container;
			functionImportElement._entitySet = this._entitySet;
			functionImportElement._unresolvedEntitySet = this._unresolvedEntitySet;
			functionImportElement._entitySetPathDefined = this._entitySetPathDefined;
			return functionImportElement;
		}

		// Token: 0x04000CE2 RID: 3298
		private string _unresolvedEntitySet;

		// Token: 0x04000CE3 RID: 3299
		private bool _entitySetPathDefined;

		// Token: 0x04000CE4 RID: 3300
		private EntityContainer _container;

		// Token: 0x04000CE5 RID: 3301
		private EntityContainerEntitySet _entitySet;

		// Token: 0x04000CE6 RID: 3302
		private bool? _isSideEffecting;
	}
}
