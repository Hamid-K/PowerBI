using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200013A RID: 314
	public class PrimitiveTypeConfiguration : IEdmTypeConfiguration
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		public PrimitiveTypeConfiguration(ODataModelBuilder builder, IEdmPrimitiveType edmType, Type clrType)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			this._builder = builder;
			this._clrType = clrType;
			this._edmType = edmType;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002C140 File Offset: 0x0002A340
		public Type ClrType
		{
			get
			{
				return this._clrType;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002C148 File Offset: 0x0002A348
		public string FullName
		{
			get
			{
				return this._edmType.FullName();
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002C155 File Offset: 0x0002A355
		public string Namespace
		{
			get
			{
				return this._edmType.Namespace;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0002C162 File Offset: 0x0002A362
		public string Name
		{
			get
			{
				return this._edmType.Name;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000032B9 File Offset: 0x000014B9
		public EdmTypeKind Kind
		{
			get
			{
				return EdmTypeKind.Primitive;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002C16F File Offset: 0x0002A36F
		public ODataModelBuilder ModelBuilder
		{
			get
			{
				return this._builder;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002C177 File Offset: 0x0002A377
		public IEdmPrimitiveType EdmPrimitiveType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x0400036B RID: 875
		private Type _clrType;

		// Token: 0x0400036C RID: 876
		private IEdmPrimitiveType _edmType;

		// Token: 0x0400036D RID: 877
		private ODataModelBuilder _builder;
	}
}
