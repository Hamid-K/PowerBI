using System;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200051C RID: 1308
	[BlockServiceProvider(typeof(IElementInstanceId))]
	public class ElementInstanceIdProvider : Block, IElementInstanceId
	{
		// Token: 0x0600287E RID: 10366 RVA: 0x00092002 File Offset: 0x00090202
		public ElementInstanceIdProvider()
			: this("ElementInstanceIdProvider")
		{
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x00010777 File Offset: 0x0000E977
		public ElementInstanceIdProvider(string name)
			: base(name)
		{
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0009200F File Offset: 0x0009020F
		public ElementId GetElementInstanceId()
		{
			return this.m_instanceId;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00092018 File Offset: 0x00090218
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus == BlockInitializationStatus.Done)
			{
				string name = CurrentProcess.Name;
				this.m_instanceId = new ElementId(name);
				this.m_applicationSwitches.RegisterSwitch("InstanceId", "iid", "Id of the current instance. Reflected by the Element Manager", ParameterType.String, false, string.Empty);
				string stringSwitch = this.m_applicationSwitches.GetStringSwitch("InstanceId");
				if (!string.IsNullOrEmpty(stringSwitch))
				{
					this.m_instanceId = new ElementId(stringSwitch);
				}
				TraceSourceBase<CommonTrace>.Tracer.Trace(TraceVerbosity.Info, "Determined that this is: IElementInstanceId ({0})", new object[] { this.m_instanceId });
				Tracing.InstanceId = this.m_instanceId.Name;
			}
			return blockInitializationStatus;
		}

		// Token: 0x04000E06 RID: 3590
		[BlockServiceDependency]
		private IApplicationSwitches m_applicationSwitches;

		// Token: 0x04000E07 RID: 3591
		private ElementId m_instanceId;

		// Token: 0x04000E08 RID: 3592
		private const string c_instanceIdLongFlagName = "InstanceId";

		// Token: 0x04000E09 RID: 3593
		private const string c_instanceIdShortFlagName = "iid";
	}
}
