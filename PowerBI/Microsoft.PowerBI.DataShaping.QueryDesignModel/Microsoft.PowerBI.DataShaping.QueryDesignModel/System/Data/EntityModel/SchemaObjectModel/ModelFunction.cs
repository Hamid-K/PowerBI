using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000032 RID: 50
	internal class ModelFunction : Function
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public ModelFunction(Schema parentElement)
			: base(parentElement)
		{
			this._isComposable = true;
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000CABC File Offset: 0x0000ACBC
		internal TypeUsage TypeUsage
		{
			get
			{
				if (this._typeUsageBuilder.TypeUsage == null)
				{
					return null;
				}
				if (base.CollectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
				}
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000CAF6 File Offset: 0x0000ACF6
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000CB05 File Offset: 0x0000AD05
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "DefiningExpression"))
			{
				this.HandleDefiningExpressionElment(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				base.HandleParameterElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000CB41 File Offset: 0x0000AD41
		protected override void HandleReturnTypeAttribute(XmlReader reader)
		{
			base.HandleReturnTypeAttribute(reader);
			this._isComposable = true;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000CB51 File Offset: 0x0000AD51
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000CB70 File Offset: 0x0000AD70
		internal override void ResolveTopLevelNames()
		{
			if (base.UnresolvedReturnType != null && base.Schema.ResolveTypeName(this, base.UnresolvedReturnType, out this._type) && this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
			}
			foreach (Parameter parameter in base.Parameters)
			{
				parameter.ResolveTopLevelNames();
			}
			if (base.ReturnType != null)
			{
				base.ReturnType.ResolveTopLevelNames();
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000CC14 File Offset: 0x0000AE14
		private void HandleDefiningExpressionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000CC38 File Offset: 0x0000AE38
		internal override void Validate()
		{
			base.Validate();
			if (this._type != null && !(this._type is ScalarType) && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				base.AddError(ErrorCode.ModelFuncionFacetOnNonScalarType, EdmSchemaErrorSeverity.Error, Strings.FacetsOnNonScalarType(this._type.FQName));
			}
			if (this._type == null && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				base.AddError(ErrorCode.ModelFunctionIncorrectlyPlacedFacet, EdmSchemaErrorSeverity.Error, Strings.FacetDeclarationRequiresTypeAttribute);
			}
			if (this._type == null && this._returnType == null)
			{
				base.AddError(ErrorCode.ModelFunctionReturnTypeNotDeclared, EdmSchemaErrorSeverity.Error, Strings.ReturnTypeMustBeDeclared(this.FQName));
			}
			if (this._type != null && this._isRefType && !(this._type is SchemaEntityType))
			{
				base.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(this._type.FQName));
			}
			foreach (Parameter parameter in this._parameters)
			{
				parameter.ValidateForModelFunction();
			}
			if (this._returnType != null)
			{
				this._returnType.ValidateForModelFunction();
			}
		}

		// Token: 0x04000667 RID: 1639
		protected TypeUsageBuilder _typeUsageBuilder;
	}
}
