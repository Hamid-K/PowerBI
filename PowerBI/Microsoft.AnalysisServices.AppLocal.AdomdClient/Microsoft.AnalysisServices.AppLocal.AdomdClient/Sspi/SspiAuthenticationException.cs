using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	internal sealed class SspiAuthenticationException : Exception, ISerializable
	{
		// Token: 0x06000F84 RID: 3972 RVA: 0x000356C0 File Offset: 0x000338C0
		internal SspiAuthenticationException(string message)
			: base(message)
		{
			this.ErrorType = SspiAuthenticationError.Unknown;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x000356D0 File Offset: 0x000338D0
		internal SspiAuthenticationException(SspiAuthenticationError errorType)
		{
			this.ErrorType = errorType;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000356DF File Offset: 0x000338DF
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000356FF File Offset: 0x000338FF
		internal SspiAuthenticationException(SspiAuthenticationError errorType, string packageName, SecurityPackageCapabilities capabilities)
		{
			this.ErrorType = errorType;
			base.Data.Add("Package", packageName);
			base.Data.Add("Capabilities", capabilities);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00035735 File Offset: 0x00033935
		internal SspiAuthenticationException(SspiAuthenticationError errorType, SecurityContextRequirements requirements)
		{
			this.ErrorType = errorType;
			base.Data.Add("Requirements", requirements);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003575A File Offset: 0x0003395A
		internal SspiAuthenticationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorType = (SspiAuthenticationError)info.GetInt32("ErrorType");
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00035775 File Offset: 0x00033975
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0003577D File Offset: 0x0003397D
		public SspiAuthenticationError ErrorType { get; private set; }

		// Token: 0x06000F8C RID: 3980 RVA: 0x00035786 File Offset: 0x00033986
		public string GetPackageName()
		{
			return (string)this.Data["Package"];
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003579D File Offset: 0x0003399D
		public SecurityPackageCapabilities GetMissingCapabilities()
		{
			return (SecurityPackageCapabilities)this.Data["Capabilities"];
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000357B4 File Offset: 0x000339B4
		public SecurityContextRequirements GetRequirementsNotObtained()
		{
			return (SecurityContextRequirements)this.Data["Requirements"];
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000357CB File Offset: 0x000339CB
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorType", this.ErrorType);
		}

		// Token: 0x04000A33 RID: 2611
		private const string SerializationInfoElementName_ErrorType = "ErrorType";

		// Token: 0x020001EF RID: 495
		private static class DataKeys
		{
			// Token: 0x04000E8D RID: 3725
			public const string Package = "Package";

			// Token: 0x04000E8E RID: 3726
			public const string Capabilities = "Capabilities";

			// Token: 0x04000E8F RID: 3727
			public const string Requirements = "Requirements";
		}
	}
}
