using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001FE RID: 510
	internal sealed class QueryExtensionSchemaUpgrader
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001B3A7 File Offset: 0x000195A7
		private QueryExtensionSchemaUpgrader(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001B3B6 File Offset: 0x000195B6
		internal static void Upgrade(QueryExtensionSchema extensionSchema, IErrorContext errorContext)
		{
			if (extensionSchema == null)
			{
				return;
			}
			new QueryExtensionSchemaUpgrader(errorContext).TryUpgrade(extensionSchema);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001B3D0 File Offset: 0x000195D0
		private void TryUpgrade(QueryExtensionSchema extensionSchema)
		{
			extensionSchema.Version = new int?(extensionSchema.Version.GetValueOrDefault());
			int? num = extensionSchema.Version;
			int num2 = 0;
			if ((num.GetValueOrDefault() == num2) & (num != null))
			{
				extensionSchema.Version = new int?(1);
			}
			num = extensionSchema.Version;
			num2 = 1;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				this._errorContext.RegisterError(QueryExtensionSchemaUpgradeMessages.UnsupportedExtensionSchemaVersion(extensionSchema.Version.Value), new object[0]);
			}
		}

		// Token: 0x04000708 RID: 1800
		private readonly IErrorContext _errorContext;
	}
}
