using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A3 RID: 163
	public interface IApplicationSwitches
	{
		// Token: 0x06000491 RID: 1169
		void RegisterSwitch(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType, bool required, string defaultValue);

		// Token: 0x06000492 RID: 1170
		void RegisterRequiredSwitch(string switchFullName, string switchShortName, string helpInfo, ParameterType switchType);

		// Token: 0x06000493 RID: 1171
		string GetStringSwitch(string name);

		// Token: 0x170000C8 RID: 200
		string this[string name] { get; }

		// Token: 0x06000495 RID: 1173
		bool GetBoolSwitch(string name);

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000496 RID: 1174
		string UsageString { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000497 RID: 1175
		bool IsHelpRequested { get; }
	}
}
