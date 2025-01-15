using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000095 RID: 149
	public sealed class FunctionParameter : MetadataItem
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00018FC6 File Offset: 0x000171C6
		internal FunctionParameter(string name, TypeUsage typeUsage, ParameterMode parameterMode)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._typeUsage = typeUsage;
			base.SetParameterMode(parameterMode);
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00019005 File Offset: 0x00017205
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.FunctionParameter;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00019009 File Offset: 0x00017209
		[MetadataProperty(BuiltInTypeKind.ParameterMode, false)]
		public ParameterMode Mode
		{
			get
			{
				return base.GetParameterMode();
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00019011 File Offset: 0x00017211
		internal override string Identity
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00019019 File Offset: 0x00017219
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00019021 File Offset: 0x00017221
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00019029 File Offset: 0x00017229
		public EdmFunction DeclaringFunction
		{
			get
			{
				return this._declaringFunction.Value;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00019036 File Offset: 0x00017236
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001903E File Offset: 0x0001723E
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x0400084B RID: 2123
		internal static Func<FunctionParameter, SafeLink<EdmFunction>> DeclaringFunctionLinker = (FunctionParameter fp) => fp._declaringFunction;

		// Token: 0x0400084C RID: 2124
		private readonly TypeUsage _typeUsage;

		// Token: 0x0400084D RID: 2125
		private readonly string _name;

		// Token: 0x0400084E RID: 2126
		private readonly SafeLink<EdmFunction> _declaringFunction = new SafeLink<EdmFunction>();
	}
}
