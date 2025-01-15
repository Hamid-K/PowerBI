using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002FB RID: 763
	internal sealed class ModelFunction : Function
	{
		// Token: 0x0600244A RID: 9290 RVA: 0x00066B5D File Offset: 0x00064D5D
		public ModelFunction(Schema parentElement)
			: base(parentElement)
		{
			this._isComposable = true;
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00066B79 File Offset: 0x00064D79
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x00066B81 File Offset: 0x00064D81
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

		// Token: 0x0600244D RID: 9293 RVA: 0x00066BBB File Offset: 0x00064DBB
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00066BCA File Offset: 0x00064DCA
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00066BD9 File Offset: 0x00064DD9
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "DefiningExpression"))
			{
				this.HandleDefiningExpressionElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				base.HandleParameterElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00066C15 File Offset: 0x00064E15
		protected override void HandleReturnTypeAttribute(XmlReader reader)
		{
			base.HandleReturnTypeAttribute(reader);
			this._isComposable = true;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00066C25 File Offset: 0x00064E25
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00066C44 File Offset: 0x00064E44
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
			if (base.ReturnTypeList != null)
			{
				base.ReturnTypeList[0].ResolveTopLevelNames();
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00066CF0 File Offset: 0x00064EF0
		private void HandleDefiningExpressionElement(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00066D12 File Offset: 0x00064F12
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
		}

		// Token: 0x04000CEB RID: 3307
		private readonly TypeUsageBuilder _typeUsageBuilder;
	}
}
