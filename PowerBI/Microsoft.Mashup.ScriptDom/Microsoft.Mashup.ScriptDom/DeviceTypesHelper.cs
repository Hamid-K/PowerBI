using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000143 RID: 323
	internal class DeviceTypesHelper : OptionsHelper<DeviceType>
	{
		// Token: 0x060014D4 RID: 5332 RVA: 0x000911E3 File Offset: 0x0008F3E3
		private DeviceTypesHelper()
		{
			base.AddOptionMapping(DeviceType.Disk, "DISK");
			base.AddOptionMapping(DeviceType.Tape, "TAPE");
			base.AddOptionMapping(DeviceType.VirtualDevice, "VIRTUAL_DEVICE");
			base.AddOptionMapping(DeviceType.DatabaseSnapshot, "DATABASE_SNAPSHOT");
		}

		// Token: 0x040011DC RID: 4572
		internal static readonly DeviceTypesHelper Instance = new DeviceTypesHelper();
	}
}
