using System;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x0200000D RID: 13
	internal struct SecurityArtifact : ISafeLogSecurityArtifact
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002FE4 File Offset: 0x000011E4
		private object Argument { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00002FED File Offset: 0x000011ED
		public SecurityArtifact(object argument, Func<object, string> toStringCallback)
		{
			this.Argument = argument;
			this._disarmCallback = toStringCallback;
			this._callbackUnsafe = null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003004 File Offset: 0x00001204
		public SecurityArtifact(object argument, Func<object, string> toStringCallback, Func<object, string> toStringCallbackUnsafe)
		{
			this.Argument = argument;
			this._disarmCallback = toStringCallback;
			this._callbackUnsafe = toStringCallbackUnsafe;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000301B File Offset: 0x0000121B
		public static string UnknownSafeTokenCallback(object _)
		{
			return "#ScrubbedArtifact#";
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003022 File Offset: 0x00001222
		public override string ToString()
		{
			if (this._disarmCallback == null)
			{
				return "#ScrubbedArtifact#";
			}
			if (this.Argument == null)
			{
				return "null";
			}
			return this._disarmCallback(this.Argument);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003051 File Offset: 0x00001251
		public string UnsafeToString()
		{
			if (this._callbackUnsafe == null || this.Argument == null)
			{
				return this.ToString();
			}
			return this._callbackUnsafe(this.Argument);
		}

		// Token: 0x04000038 RID: 56
		private const string _scrubbedArtifact = "#ScrubbedArtifact#";

		// Token: 0x0400003A RID: 58
		private readonly Func<object, string> _disarmCallback;

		// Token: 0x0400003B RID: 59
		private readonly Func<object, string> _callbackUnsafe;
	}
}
