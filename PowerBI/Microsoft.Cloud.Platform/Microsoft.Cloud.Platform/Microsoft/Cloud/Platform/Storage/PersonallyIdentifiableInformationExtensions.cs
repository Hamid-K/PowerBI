using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage
{
	// Token: 0x02000017 RID: 23
	public static class PersonallyIdentifiableInformationExtensions
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000375C File Offset: 0x0000195C
		public static void RemovePersonalInformation([NotNull] this DbConnectionStringBuilder connectionStringBuilder)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<DbConnectionStringBuilder>(connectionStringBuilder, "connectionStringBuilder");
			connectionStringBuilder.Remove("uid");
			connectionStringBuilder.Remove("user id");
			connectionStringBuilder.Remove("password");
			connectionStringBuilder.Remove("pwd");
			connectionStringBuilder.Remove("Initial Catalog");
			connectionStringBuilder.Remove("Database");
		}
	}
}
