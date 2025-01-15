using System;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000143 RID: 323
	public class ComplexTypeConfiguration<TComplexType> : StructuralTypeConfiguration<TComplexType> where TComplexType : class
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0002DFA5 File Offset: 0x0002C1A5
		internal ComplexTypeConfiguration(ComplexTypeConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002DFAE File Offset: 0x0002C1AE
		internal ComplexTypeConfiguration(ODataModelBuilder modelBuilder)
			: this(modelBuilder, new ComplexTypeConfiguration(modelBuilder, typeof(TComplexType)))
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002DFC7 File Offset: 0x0002C1C7
		internal ComplexTypeConfiguration(ODataModelBuilder modelBuilder, ComplexTypeConfiguration configuration)
			: base(configuration)
		{
			this._modelBuilder = modelBuilder;
			this._configuration = configuration;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002DFDE File Offset: 0x0002C1DE
		public ComplexTypeConfiguration<TComplexType> Abstract()
		{
			this._configuration.IsAbstract = new bool?(true);
			return this;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002DFF2 File Offset: 0x0002C1F2
		public ComplexTypeConfiguration BaseType
		{
			get
			{
				return this._configuration.BaseType;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002DFFF File Offset: 0x0002C1FF
		public ComplexTypeConfiguration<TComplexType> DerivesFromNothing()
		{
			this._configuration.DerivesFromNothing();
			return this;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002E010 File Offset: 0x0002C210
		public ComplexTypeConfiguration<TComplexType> DerivesFrom<TBaseType>() where TBaseType : class
		{
			ComplexTypeConfiguration<TBaseType> complexTypeConfiguration = this._modelBuilder.ComplexType<TBaseType>();
			this._configuration.DerivesFrom(complexTypeConfiguration._configuration);
			return this;
		}

		// Token: 0x040003A2 RID: 930
		private ComplexTypeConfiguration _configuration;

		// Token: 0x040003A3 RID: 931
		private ODataModelBuilder _modelBuilder;
	}
}
