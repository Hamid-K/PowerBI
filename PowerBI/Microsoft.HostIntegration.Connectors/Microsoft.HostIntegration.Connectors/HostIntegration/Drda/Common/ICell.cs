using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080C RID: 2060
	public interface ICell
	{
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06004104 RID: 16644
		// (set) Token: 0x06004105 RID: 16645
		object Value { get; set; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06004106 RID: 16646
		// (set) Token: 0x06004107 RID: 16647
		string DataTypeName { get; set; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06004108 RID: 16648
		// (set) Token: 0x06004109 RID: 16649
		bool IsDBNull { get; set; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600410A RID: 16650
		// (set) Token: 0x0600410B RID: 16651
		string Name { get; set; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600410C RID: 16652
		// (set) Token: 0x0600410D RID: 16653
		int FieldType { get; set; }
	}
}
