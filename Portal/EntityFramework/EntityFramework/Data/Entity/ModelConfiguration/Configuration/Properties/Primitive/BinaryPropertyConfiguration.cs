using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000201 RID: 513
	internal class BinaryPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0004A398 File Offset: 0x00048598
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0004A3A0 File Offset: 0x000485A0
		public bool? IsRowVersion { get; set; }

		// Token: 0x06001B28 RID: 6952 RVA: 0x0004A3A9 File Offset: 0x000485A9
		public BinaryPropertyConfiguration()
		{
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0004A3B1 File Offset: 0x000485B1
		private BinaryPropertyConfiguration(BinaryPropertyConfiguration source)
			: base(source)
		{
			this.IsRowVersion = source.IsRowVersion;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0004A3C6 File Offset: 0x000485C6
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new BinaryPropertyConfiguration(this);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0004A3D0 File Offset: 0x000485D0
		protected override void ConfigureProperty(EdmProperty property)
		{
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				base.ConcurrencyMode = new ConcurrencyMode?(base.ConcurrencyMode ?? global::System.Data.Entity.Core.Metadata.Edm.ConcurrencyMode.Fixed);
				base.DatabaseGeneratedOption = new DatabaseGeneratedOption?(base.DatabaseGeneratedOption ?? global::System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
				base.IsNullable = new bool?(base.IsNullable.GetValueOrDefault());
				base.MaxLength = new int?(base.MaxLength ?? 8);
			}
			base.ConfigureProperty(property);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0004A494 File Offset: 0x00048694
		protected override void ConfigureColumn(EdmProperty column, EntityType table, DbProviderManifest providerManifest)
		{
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				base.ColumnType = base.ColumnType ?? "rowversion";
			}
			base.ConfigureColumn(column, table, providerManifest);
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				column.MaxLength = null;
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0004A510 File Offset: 0x00048710
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration != null)
			{
				this.IsRowVersion = binaryPropertyConfiguration.IsRowVersion;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0004A53C File Offset: 0x0004873C
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration != null && this.IsRowVersion == null)
			{
				this.IsRowVersion = binaryPropertyConfiguration.IsRowVersion;
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0004A578 File Offset: 0x00048778
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration == null)
			{
				return;
			}
			if (binaryPropertyConfiguration.IsRowVersion != null)
			{
				this.IsRowVersion = null;
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0004A5B8 File Offset: 0x000487B8
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = binaryPropertyConfiguration == null || base.IsCompatible<bool, BinaryPropertyConfiguration>((BinaryPropertyConfiguration c) => c.IsRowVersion, binaryPropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
