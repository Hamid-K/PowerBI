using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	internal sealed class SspiAuthenticationException : Exception, ISerializable
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x00035390 File Offset: 0x00033590
		internal SspiAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = SspiAuthenticationError.Unknown;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000353A0 File Offset: 0x000335A0
		internal SspiAuthenticationException(SspiAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000353AF File Offset: 0x000335AF
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000353CF File Offset: 0x000335CF
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName, SecurityPackageCapabilities capabilities)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
			base.Data.Add("Capabilities", capabilities);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00035405 File Offset: 0x00033605
		internal SspiAuthenticationException(SspiAuthenticationError errorType, SecurityContextRequirements requirements)
		{
			this.ErrorType = errorType;
			base.Data.Add("Requirements", requirements);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003542A File Offset: 0x0003362A
		internal SspiAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (SspiAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00035445 File Offset: 0x00033645
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x0003544D File Offset: 0x0003364D
		public SspiAuthenticationError ErrorType { get; private set; }

		// Token: 0x06000F7F RID: 3967 RVA: 0x00035456 File Offset: 0x00033656
		public string GetPackageName()
		{
			return (string)this.Data["Package"];
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003546D File Offset: 0x0003366D
		public SecurityPackageCapabilities GetMissingCapabilities()
		{
			return (SecurityPackageCapabilities)this.Data["Capabilities"];
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00035484 File Offset: 0x00033684
		public SecurityContextRequirements GetRequirementsNotObtained()
		{
			return (SecurityContextRequirements)this.Data["Requirements"];
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003549B File Offset: 0x0003369B
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x04000A26 RID: 2598
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001EF RID: 495
		private static class DataKeys
		{
			// Token: 0x04000E7C RID: 3708
			public const string Package = "Package";

			// Token: 0x04000E7D RID: 3709
			public const string Capabilities = "Capabilities";

			// Token: 0x04000E7E RID: 3710
			public const string Requirements = "Requirements";
		}
	}
}
