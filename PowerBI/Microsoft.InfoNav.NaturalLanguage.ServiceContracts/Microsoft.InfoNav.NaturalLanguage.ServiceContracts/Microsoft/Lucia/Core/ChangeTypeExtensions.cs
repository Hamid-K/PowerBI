using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000042 RID: 66
	internal static class ChangeTypeExtensions
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00003DA2 File Offset: 0x00001FA2
		internal static bool IsSchemaChange(this ChangeType changeType)
		{
			return ChangeTypeExtensions.HasFlag(changeType, ChangeType.Schema);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003DAB File Offset: 0x00001FAB
		internal static bool IsDataChange(this ChangeType changeType)
		{
			return ChangeTypeExtensions.HasFlag(changeType, ChangeType.Data);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003DB4 File Offset: 0x00001FB4
		internal static bool IsLoadedChange(this ChangeType changeType)
		{
			return ChangeTypeExtensions.HasFlag(changeType, ChangeType.Loaded);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003DBD File Offset: 0x00001FBD
		internal static bool IsUnloadedChange(this ChangeType changeType)
		{
			return ChangeTypeExtensions.HasFlag(changeType, ChangeType.Unloaded);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003DC6 File Offset: 0x00001FC6
		internal static bool IsOverwrittenChange(this ChangeType changeType)
		{
			return ChangeTypeExtensions.HasFlag(changeType, ChangeType.Overwritten);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003DD0 File Offset: 0x00001FD0
		private static bool HasFlag(ChangeType type, ChangeType flag)
		{
			return (type & flag) == flag;
		}
	}
}
