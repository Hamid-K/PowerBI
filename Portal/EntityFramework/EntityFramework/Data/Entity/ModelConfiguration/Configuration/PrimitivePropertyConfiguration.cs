using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F5 RID: 501
	public class PrimitivePropertyConfiguration
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x00046D1A File Offset: 0x00044F1A
		internal PrimitivePropertyConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00046D29 File Offset: 0x00044F29
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00046D31 File Offset: 0x00044F31
		public PrimitivePropertyConfiguration IsOptional()
		{
			this.Configuration.IsNullable = new bool?(true);
			return this;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00046D45 File Offset: 0x00044F45
		public PrimitivePropertyConfiguration IsRequired()
		{
			this.Configuration.IsNullable = new bool?(false);
			return this;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00046D59 File Offset: 0x00044F59
		public PrimitivePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			if (databaseGeneratedOption != null && !Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			this.Configuration.DatabaseGeneratedOption = databaseGeneratedOption;
			return this;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00046D93 File Offset: 0x00044F93
		public PrimitivePropertyConfiguration IsConcurrencyToken()
		{
			this.IsConcurrencyToken(new bool?(true));
			return this;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00046DA4 File Offset: 0x00044FA4
		public PrimitivePropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			this.Configuration.ConcurrencyMode = ((concurrencyToken == null) ? null : new ConcurrencyMode?(concurrencyToken.Value ? ConcurrencyMode.Fixed : ConcurrencyMode.None));
			return this;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00046DE3 File Offset: 0x00044FE3
		public PrimitivePropertyConfiguration HasColumnType(string columnType)
		{
			this.Configuration.ColumnType = columnType;
			return this;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00046DF2 File Offset: 0x00044FF2
		public PrimitivePropertyConfiguration HasColumnName(string columnName)
		{
			this.Configuration.ColumnName = columnName;
			return this;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00046E01 File Offset: 0x00045001
		public PrimitivePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.Configuration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00046E1D File Offset: 0x0004501D
		public PrimitivePropertyConfiguration HasParameterName(string parameterName)
		{
			this.Configuration.ParameterName = parameterName;
			return this;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00046E2C File Offset: 0x0004502C
		public PrimitivePropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			if (columnOrder != null && columnOrder.Value < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			this.Configuration.ColumnOrder = columnOrder;
			return this;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00046E59 File Offset: 0x00045059
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00046E61 File Offset: 0x00045061
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00046E6A File Offset: 0x0004506A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00046E72 File Offset: 0x00045072
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A8F RID: 2703
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}
