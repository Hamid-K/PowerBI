using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073F RID: 1855
	[Guid("5BE8A0E7-E10E-4700-BA68-A4C23093ED14")]
	public interface IRemoteEnvironmentClass
	{
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003A22 RID: 14882
		// (set) Token: 0x06003A23 RID: 14883
		Guid DevDefinesGuid { get; set; }

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003A24 RID: 14884
		// (set) Token: 0x06003A25 RID: 14885
		string RemoteEnvironmentVersion { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003A26 RID: 14886
		// (set) Token: 0x06003A27 RID: 14887
		string Name { get; set; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003A28 RID: 14888
		// (set) Token: 0x06003A29 RID: 14889
		Transport Transport { get; set; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003A2A RID: 14890
		// (set) Token: 0x06003A2B RID: 14891
		string StateMachineName { get; set; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003A2C RID: 14892
		// (set) Token: 0x06003A2D RID: 14893
		string StateMachineFullName { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06003A2E RID: 14894
		// (set) Token: 0x06003A2F RID: 14895
		string AggregateConverterName { get; set; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003A30 RID: 14896
		// (set) Token: 0x06003A31 RID: 14897
		string PrimitiveConverterName { get; set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003A32 RID: 14898
		// (set) Token: 0x06003A33 RID: 14899
		string PrimitiveConverterClassId { get; set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003A34 RID: 14900
		// (set) Token: 0x06003A35 RID: 14901
		string TransportName { get; set; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003A36 RID: 14902
		// (set) Token: 0x06003A37 RID: 14903
		string TransportProtocolName { get; set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003A38 RID: 14904
		// (set) Token: 0x06003A39 RID: 14905
		ProgrammingModel ProgrammingModel { get; set; }

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003A3A RID: 14906
		// (set) Token: 0x06003A3B RID: 14907
		string ProgrammingModelName { get; set; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003A3C RID: 14908
		// (set) Token: 0x06003A3D RID: 14909
		HostEnvironment HostEnvironment { get; set; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003A3E RID: 14910
		// (set) Token: 0x06003A3F RID: 14911
		HostPlatformTypes HostType { get; set; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003A40 RID: 14912
		// (set) Token: 0x06003A41 RID: 14913
		string HostEnvironmentName { get; set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003A42 RID: 14914
		// (set) Token: 0x06003A43 RID: 14915
		string HostPlatformName { get; set; }

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003A44 RID: 14916
		// (set) Token: 0x06003A45 RID: 14917
		HostLanguage[] HostLanguages { get; set; }

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003A46 RID: 14918
		// (set) Token: 0x06003A47 RID: 14919
		RECPropertyPage[] RECPropertyPages { get; set; }

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003A48 RID: 14920
		// (set) Token: 0x06003A49 RID: 14921
		string VendorName { get; set; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003A4A RID: 14922
		// (set) Token: 0x06003A4B RID: 14923
		Guid VendorID { get; set; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003A4C RID: 14924
		// (set) Token: 0x06003A4D RID: 14925
		string RemoteEnvironmentClassID { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003A4E RID: 14926
		// (set) Token: 0x06003A4F RID: 14927
		RemoteEnvironmentTypes RemoteEnvironmentType { get; set; }

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003A50 RID: 14928
		// (set) Token: 0x06003A51 RID: 14929
		string AggregateConverterFullName { get; set; }

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003A52 RID: 14930
		// (set) Token: 0x06003A53 RID: 14931
		string PrimitiveConverterFullName { get; set; }

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003A54 RID: 14932
		// (set) Token: 0x06003A55 RID: 14933
		string TransportFullName { get; set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003A56 RID: 14934
		// (set) Token: 0x06003A57 RID: 14935
		bool IsSupportedByManagedRuntime { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003A58 RID: 14936
		// (set) Token: 0x06003A59 RID: 14937
		bool IsForNew { get; set; }
	}
}
