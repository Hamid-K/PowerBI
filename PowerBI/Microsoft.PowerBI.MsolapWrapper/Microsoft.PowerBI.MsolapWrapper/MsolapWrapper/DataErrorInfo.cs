using System;

namespace MsolapWrapper
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class DataErrorInfo
	{
		// Token: 0x04000083 RID: 131
		public string Description;

		// Token: 0x04000084 RID: 132
		public string GenericMessage;

		// Token: 0x04000085 RID: 133
		public string Guid;

		// Token: 0x04000086 RID: 134
		public string Source;

		// Token: 0x04000087 RID: 135
		public string HelpFile;

		// Token: 0x04000088 RID: 136
		public uint HelpContext;

		// Token: 0x04000089 RID: 137
		public int Hresult;

		// Token: 0x0400008A RID: 138
		public uint ErrorCode;

		// Token: 0x0400008B RID: 139
		public bool HasUserSafeDescription;

		// Token: 0x0400008C RID: 140
		public string OnPremErrorCode;

		// Token: 0x0400008D RID: 141
		public __MIDL_IASErrorInfo_0001 Type;

		// Token: 0x0400008E RID: 142
		public WrapperErrorSourceOrigin TypeOrigin;
	}
}
