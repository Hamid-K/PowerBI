using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A7 RID: 423
	public class DecimalPropertyConvention : IConceptualModelConvention<EdmProperty>, IConvention
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x0003F01E File Offset: 0x0003D21E
		public DecimalPropertyConvention()
			: this(18, 2)
		{
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0003F029 File Offset: 0x0003D229
		public DecimalPropertyConvention(byte precision, byte scale)
		{
			this._precision = precision;
			this._scale = scale;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0003F040 File Offset: 0x0003D240
		public virtual void Apply(EdmProperty item, DbModel model)
		{
			Check.NotNull<EdmProperty>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				if (item.Precision == null)
				{
					item.Precision = new byte?(this._precision);
				}
				if (item.Scale == null)
				{
					item.Scale = new byte?(this._scale);
				}
			}
		}

		// Token: 0x04000A33 RID: 2611
		private readonly byte _precision;

		// Token: 0x04000A34 RID: 2612
		private readonly byte _scale;
	}
}
