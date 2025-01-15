using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000439 RID: 1081
	internal sealed class DataContractImplementor
	{
		// Token: 0x060034BA RID: 13498 RVA: 0x000A95A4 File Offset: 0x000A77A4
		internal DataContractImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			this._dataContract = this._baseClrType.GetCustomAttributes(false).FirstOrDefault<DataContractAttribute>();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000A95D0 File Offset: 0x000A77D0
		internal void Implement(TypeBuilder typeBuilder)
		{
			if (this._dataContract != null)
			{
				object[] array = new object[] { this._dataContract.IsReference };
				CustomAttributeBuilder customAttributeBuilder = new CustomAttributeBuilder(DataContractImplementor.DataContractAttributeConstructor, new object[0], DataContractImplementor.DataContractProperties, array);
				typeBuilder.SetCustomAttribute(customAttributeBuilder);
			}
		}

		// Token: 0x040010FC RID: 4348
		internal static readonly ConstructorInfo DataContractAttributeConstructor = typeof(DataContractAttribute).GetDeclaredConstructor(new Type[0]);

		// Token: 0x040010FD RID: 4349
		internal static readonly PropertyInfo[] DataContractProperties = new PropertyInfo[] { typeof(DataContractAttribute).GetDeclaredProperty("IsReference") };

		// Token: 0x040010FE RID: 4350
		private readonly Type _baseClrType;

		// Token: 0x040010FF RID: 4351
		private readonly DataContractAttribute _dataContract;
	}
}
