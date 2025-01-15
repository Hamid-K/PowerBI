using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C7 RID: 1223
	public sealed class FunctionParameter : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06003C67 RID: 15463 RVA: 0x000C8727 File Offset: 0x000C6927
		internal FunctionParameter()
		{
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x000C873A File Offset: 0x000C693A
		internal FunctionParameter(string name, TypeUsage typeUsage, ParameterMode parameterMode)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._typeUsage = typeUsage;
			base.SetParameterMode(parameterMode);
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000C877A File Offset: 0x000C697A
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.FunctionParameter;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000C877E File Offset: 0x000C697E
		[MetadataProperty(BuiltInTypeKind.ParameterMode, false)]
		public ParameterMode Mode
		{
			get
			{
				return base.GetParameterMode();
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000C8786 File Offset: 0x000C6986
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x000C878E File Offset: 0x000C698E
		internal override string Identity
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000C8796 File Offset: 0x000C6996
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000C879E File Offset: 0x000C699E
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this.SetName(value);
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000C87B4 File Offset: 0x000C69B4
		private void SetName(string name)
		{
			this._name = name;
			if (this.DeclaringFunction == null)
			{
				return;
			}
			((this.Mode == ParameterMode.ReturnValue) ? this.DeclaringFunction.ReturnParameters.Source : this.DeclaringFunction.Parameters.Source).InvalidateCache();
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000C8801 File Offset: 0x000C6A01
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000C8809 File Offset: 0x000C6A09
		public string TypeName
		{
			get
			{
				return this.TypeUsage.EdmType.Name;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x000C881C File Offset: 0x000C6A1C
		public bool IsMaxLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000C8850 File Offset: 0x000C6A50
		public int? MaxLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet))
				{
					return null;
				}
				return facet.Value as int?;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x000C8894 File Offset: 0x000C6A94
		public bool IsMaxLength
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.IsUnbounded;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000C88C4 File Offset: 0x000C6AC4
		public bool IsPrecisionConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Precision", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000C88F8 File Offset: 0x000C6AF8
		public byte? Precision
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Precision", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x000C893C File Offset: 0x000C6B3C
		public bool IsScaleConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Scale", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000C8970 File Offset: 0x000C6B70
		public byte? Scale
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Scale", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x000C89B1 File Offset: 0x000C6BB1
		public EdmFunction DeclaringFunction
		{
			get
			{
				return this._declaringFunction.Value;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000C89BE File Offset: 0x000C6BBE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000C89C6 File Offset: 0x000C6BC6
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x000C89D6 File Offset: 0x000C6BD6
		public static FunctionParameter Create(string name, EdmType edmType, ParameterMode parameterMode)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EdmType>(edmType, "edmType");
			FunctionParameter functionParameter = new FunctionParameter(name, TypeUsage.Create(edmType, FacetValues.NullFacetValues), parameterMode);
			functionParameter.SetReadOnly();
			return functionParameter;
		}

		// Token: 0x040014CE RID: 5326
		internal static Func<FunctionParameter, SafeLink<EdmFunction>> DeclaringFunctionLinker = (FunctionParameter fp) => fp._declaringFunction;

		// Token: 0x040014CF RID: 5327
		private readonly SafeLink<EdmFunction> _declaringFunction = new SafeLink<EdmFunction>();

		// Token: 0x040014D0 RID: 5328
		private readonly TypeUsage _typeUsage;

		// Token: 0x040014D1 RID: 5329
		private string _name;
	}
}
