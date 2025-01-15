using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class PBIProjectReadException : PBIProjectException
	{
		// Token: 0x06000304 RID: 772 RVA: 0x00008928 File Offset: 0x00006B28
		public PBIProjectReadException(string path, string message, bool appendPathMessage, PBIProjectException.PBIProjectErrorCode errorCode, Exception innerException = null, string learnMoreLinkUrl = null)
			: base(PBIProjectReadException.MakeMessage(path, message, appendPathMessage), PBIProjectException.GetErrorWithPath(path), errorCode, innerException, learnMoreLinkUrl)
		{
			this.Path = path;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000894B File Offset: 0x00006B4B
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00008953 File Offset: 0x00006B53
		public string Path { get; private set; }

		// Token: 0x06000307 RID: 775 RVA: 0x0000895C File Offset: 0x00006B5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.Path);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008977 File Offset: 0x00006B77
		private static string MakeMessage(string path, string message, bool appendPathMessage)
		{
			if (!appendPathMessage)
			{
				return message;
			}
			return PBIProjectReadException.MakePathMessage(path) + " " + message;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000898F File Offset: 0x00006B8F
		private static string MakePathMessage(string path)
		{
			return ("Cannot read '" + path + "'.").ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x040001BB RID: 443
		private const string pathTag = "path";
	}
}
