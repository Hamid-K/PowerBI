using System;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F6 RID: 502
	public class PropertyMappingConfiguration
	{
		// Token: 0x06001A4C RID: 6732 RVA: 0x00046E7A File Offset: 0x0004507A
		internal PropertyMappingConfiguration(PrimitivePropertyConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00046E89 File Offset: 0x00045089
		internal PrimitivePropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00046E91 File Offset: 0x00045091
		public PropertyMappingConfiguration HasColumnName(string columnName)
		{
			this.Configuration.ColumnName = columnName;
			return this;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00046EA0 File Offset: 0x000450A0
		public PropertyMappingConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			this.Configuration.SetAnnotation(name, value);
			return this;
		}

		// Token: 0x04000A90 RID: 2704
		private readonly PrimitivePropertyConfiguration _configuration;
	}
}
