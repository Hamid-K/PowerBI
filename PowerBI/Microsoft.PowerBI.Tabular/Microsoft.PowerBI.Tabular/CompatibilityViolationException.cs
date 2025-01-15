using System;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class CompatibilityViolationException : InvalidOperationException, ISerializable
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00077309 File Offset: 0x00075509
		internal CompatibilityViolationException(CompatibilityMode activeMode, string requestingPath)
			: base(TomSR.Exception_CompatRestriction_UnsupportedForMode(activeMode.ToString(), requestingPath))
		{
			this.ActiveMode = activeMode;
			this.SupportedCompatibilityLevel = -3;
			this.CompatibilityLevelRequest = -2;
			this.RequestingPath = requestingPath;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00077344 File Offset: 0x00075544
		internal CompatibilityViolationException(CompatibilityMode activeMode, int supportedCompatLevel, int compatLevelRequest, string requestingPath)
			: base(TomSR.Exception_CompatRestriction_ViolationForMode(activeMode.ToString(), supportedCompatLevel.ToString(), compatLevelRequest.ToString(), requestingPath))
		{
			this.ActiveMode = activeMode;
			this.SupportedCompatibilityLevel = supportedCompatLevel;
			this.CompatibilityLevelRequest = compatLevelRequest;
			this.RequestingPath = requestingPath;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00077396 File Offset: 0x00075596
		internal CompatibilityViolationException(string requestingPath)
			: base(TomSR.Exception_CompatRestriction_InvalidForAllModes(requestingPath))
		{
			this.ActiveMode = CompatibilityMode.Unknown;
			this.SupportedCompatibilityLevel = -3;
			this.CompatibilityLevelRequest = -3;
			this.RequestingPath = requestingPath;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000773C4 File Offset: 0x000755C4
		internal CompatibilityViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ActiveMode = (CompatibilityMode)info.GetInt32("ActiveMode");
			this.SupportedCompatibilityLevel = info.GetInt32("SupportedCompatLevel");
			this.CompatibilityLevelRequest = info.GetInt32("CompatLevelRequest");
			this.RequestingPath = info.GetString("RequestingPath");
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0007741D File Offset: 0x0007561D
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x00077425 File Offset: 0x00075625
		public CompatibilityMode ActiveMode { get; private set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0007742E File Offset: 0x0007562E
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00077436 File Offset: 0x00075636
		public int CompatibilityLevelRequest { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0007743F File Offset: 0x0007563F
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x00077447 File Offset: 0x00075647
		public int SupportedCompatibilityLevel { get; private set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00077450 File Offset: 0x00075650
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00077458 File Offset: 0x00075658
		public string RequestingPath { get; private set; }

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00077464 File Offset: 0x00075664
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActiveMode", this.ActiveMode);
			info.AddValue("SupportedCompatLevel", this.SupportedCompatibilityLevel);
			info.AddValue("CompatLevelRequest", this.CompatibilityLevelRequest);
			info.AddValue("RequestingPath", this.RequestingPath, typeof(string));
		}

		// Token: 0x040001E3 RID: 483
		private const string SerializationInfoElementName_ActiveMode = "ActiveMode";

		// Token: 0x040001E4 RID: 484
		private const string SerializationInfoElementName_SupportedCompatLevel = "SupportedCompatLevel";

		// Token: 0x040001E5 RID: 485
		private const string SerializationInfoElementName_CompatLevelRequest = "CompatLevelRequest";

		// Token: 0x040001E6 RID: 486
		private const string SerializationInfoElementName_RequestingPath = "RequestingPath";
	}
}
