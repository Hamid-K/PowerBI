using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	internal sealed class SspiAuthenticationException : Exception, ISerializable
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x00037FC4 File Offset: 0x000361C4
		internal SspiAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = SspiAuthenticationError.Unknown;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00037FD4 File Offset: 0x000361D4
		internal SspiAuthenticationException(SspiAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00037FE3 File Offset: 0x000361E3
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00038003 File Offset: 0x00036203
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName, SecurityPackageCapabilities capabilities)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
			base.Data.Add("Capabilities", capabilities);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00038039 File Offset: 0x00036239
		internal SspiAuthenticationException(SspiAuthenticationError errorType, SecurityContextRequirements requirements)
		{
			this.ErrorType = errorType;
			base.Data.Add("Requirements", requirements);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003805E File Offset: 0x0003625E
		internal SspiAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (SspiAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00038079 File Offset: 0x00036279
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x00038081 File Offset: 0x00036281
		public SspiAuthenticationError ErrorType { get; private set; }

		// Token: 0x0600101A RID: 4122 RVA: 0x0003808A File Offset: 0x0003628A
		public string GetPackageName()
		{
			return (string)this.Data["Package"];
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000380A1 File Offset: 0x000362A1
		public SecurityPackageCapabilities GetMissingCapabilities()
		{
			return (SecurityPackageCapabilities)this.Data["Capabilities"];
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000380B8 File Offset: 0x000362B8
		public SecurityContextRequirements GetRequirementsNotObtained()
		{
			return (SecurityContextRequirements)this.Data["Requirements"];
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000380CF File Offset: 0x000362CF
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x040009EC RID: 2540
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001CC RID: 460
		private static class DataKeys
		{
			// Token: 0x04001148 RID: 4424
			public const string Package = "Package";

			// Token: 0x04001149 RID: 4425
			public const string Capabilities = "Capabilities";

			// Token: 0x0400114A RID: 4426
			public const string Requirements = "Requirements";
		}
	}
}
