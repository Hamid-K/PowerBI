using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000104 RID: 260
	internal enum BackupOptionKind
	{
		// Token: 0x04000B3E RID: 2878
		None,
		// Token: 0x04000B3F RID: 2879
		NoRecovery,
		// Token: 0x04000B40 RID: 2880
		TruncateOnly,
		// Token: 0x04000B41 RID: 2881
		NoLog,
		// Token: 0x04000B42 RID: 2882
		NoTruncate,
		// Token: 0x04000B43 RID: 2883
		Unload,
		// Token: 0x04000B44 RID: 2884
		NoUnload,
		// Token: 0x04000B45 RID: 2885
		Rewind,
		// Token: 0x04000B46 RID: 2886
		NoRewind,
		// Token: 0x04000B47 RID: 2887
		Format,
		// Token: 0x04000B48 RID: 2888
		NoFormat,
		// Token: 0x04000B49 RID: 2889
		Init,
		// Token: 0x04000B4A RID: 2890
		NoInit,
		// Token: 0x04000B4B RID: 2891
		Skip,
		// Token: 0x04000B4C RID: 2892
		NoSkip,
		// Token: 0x04000B4D RID: 2893
		Restart,
		// Token: 0x04000B4E RID: 2894
		Stats,
		// Token: 0x04000B4F RID: 2895
		Differential,
		// Token: 0x04000B50 RID: 2896
		Snapshot,
		// Token: 0x04000B51 RID: 2897
		Checksum,
		// Token: 0x04000B52 RID: 2898
		NoChecksum,
		// Token: 0x04000B53 RID: 2899
		ContinueAfterError,
		// Token: 0x04000B54 RID: 2900
		StopOnError,
		// Token: 0x04000B55 RID: 2901
		CopyOnly,
		// Token: 0x04000B56 RID: 2902
		Standby,
		// Token: 0x04000B57 RID: 2903
		ExpireDate,
		// Token: 0x04000B58 RID: 2904
		RetainDays,
		// Token: 0x04000B59 RID: 2905
		Name,
		// Token: 0x04000B5A RID: 2906
		Description,
		// Token: 0x04000B5B RID: 2907
		Password,
		// Token: 0x04000B5C RID: 2908
		MediaName,
		// Token: 0x04000B5D RID: 2909
		MediaDescription,
		// Token: 0x04000B5E RID: 2910
		MediaPassword,
		// Token: 0x04000B5F RID: 2911
		BlockSize,
		// Token: 0x04000B60 RID: 2912
		BufferCount,
		// Token: 0x04000B61 RID: 2913
		MaxTransferSize,
		// Token: 0x04000B62 RID: 2914
		EnhancedIntegrity,
		// Token: 0x04000B63 RID: 2915
		Compression,
		// Token: 0x04000B64 RID: 2916
		NoCompression
	}
}
