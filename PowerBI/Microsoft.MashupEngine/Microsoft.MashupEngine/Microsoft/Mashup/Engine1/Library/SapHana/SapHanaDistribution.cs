using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000443 RID: 1091
	internal static class SapHanaDistribution
	{
		// Token: 0x060024FF RID: 9471 RVA: 0x0006A0F6 File Offset: 0x000682F6
		public static bool TryGetConnectionStringValue(SapHanaDistributionOption value, out string valueString)
		{
			return SapHanaDistribution.distributionToText.TryGetValue(value, out valueString);
		}

		// Token: 0x04000EFA RID: 3834
		public static readonly IntEnumTypeValue<SapHanaDistributionOption> Type = new IntEnumTypeValue<SapHanaDistributionOption>("SapHanaDistribution.Type");

		// Token: 0x04000EFB RID: 3835
		public static readonly NumberValue Off = SapHanaDistribution.Type.NewEnumValue("SapHanaDistribution.Off", 0, SapHanaDistributionOption.Off, null);

		// Token: 0x04000EFC RID: 3836
		public static readonly NumberValue Connection = SapHanaDistribution.Type.NewEnumValue("SapHanaDistribution.Connection", 1, SapHanaDistributionOption.Connection, null);

		// Token: 0x04000EFD RID: 3837
		public static readonly NumberValue Statement = SapHanaDistribution.Type.NewEnumValue("SapHanaDistribution.Statement", 2, SapHanaDistributionOption.Statement, null);

		// Token: 0x04000EFE RID: 3838
		public static readonly NumberValue All = SapHanaDistribution.Type.NewEnumValue("SapHanaDistribution.All", 3, SapHanaDistributionOption.All, null);

		// Token: 0x04000EFF RID: 3839
		private static readonly Dictionary<SapHanaDistributionOption, string> distributionToText = new Dictionary<SapHanaDistributionOption, string>
		{
			{
				SapHanaDistributionOption.Off,
				"OFF"
			},
			{
				SapHanaDistributionOption.Connection,
				"CONNECTION"
			},
			{
				SapHanaDistributionOption.Statement,
				"STATEMENT"
			},
			{
				SapHanaDistributionOption.All,
				"ALL"
			}
		};
	}
}
