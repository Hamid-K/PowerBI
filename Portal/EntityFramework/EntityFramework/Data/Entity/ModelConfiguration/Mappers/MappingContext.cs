using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000159 RID: 345
	internal sealed class MappingContext
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x00038D8C File Offset: 0x00036F8C
		public MappingContext(ModelConfiguration modelConfiguration, ConventionsConfiguration conventionsConfiguration, EdmModel model, DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest, AttributeProvider attributeProvider = null)
		{
			this._modelConfiguration = modelConfiguration;
			this._conventionsConfiguration = conventionsConfiguration;
			this._model = model;
			this._modelBuilderVersion = modelBuilderVersion;
			this._attributeProvider = attributeProvider ?? new AttributeProvider();
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00038DC2 File Offset: 0x00036FC2
		public ModelConfiguration ModelConfiguration
		{
			get
			{
				return this._modelConfiguration;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00038DCA File Offset: 0x00036FCA
		public ConventionsConfiguration ConventionsConfiguration
		{
			get
			{
				return this._conventionsConfiguration;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00038DD2 File Offset: 0x00036FD2
		public EdmModel Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00038DDA File Offset: 0x00036FDA
		public AttributeProvider AttributeProvider
		{
			get
			{
				return this._attributeProvider;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00038DE2 File Offset: 0x00036FE2
		public DbModelBuilderVersion ModelBuilderVersion
		{
			get
			{
				return this._modelBuilderVersion;
			}
		}

		// Token: 0x040009F6 RID: 2550
		private readonly ModelConfiguration _modelConfiguration;

		// Token: 0x040009F7 RID: 2551
		private readonly ConventionsConfiguration _conventionsConfiguration;

		// Token: 0x040009F8 RID: 2552
		private readonly EdmModel _model;

		// Token: 0x040009F9 RID: 2553
		private readonly AttributeProvider _attributeProvider;

		// Token: 0x040009FA RID: 2554
		private readonly DbModelBuilderVersion _modelBuilderVersion;
	}
}
