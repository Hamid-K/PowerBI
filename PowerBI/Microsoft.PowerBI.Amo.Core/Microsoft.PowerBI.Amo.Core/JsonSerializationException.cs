using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class JsonSerializationException : AmoException
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00024DB0 File Offset: 0x00022FB0
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00024DB8 File Offset: 0x00022FB8
		public bool HasLineInfo { get; internal set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00024DC1 File Offset: 0x00022FC1
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x00024DC9 File Offset: 0x00022FC9
		public int LineNumber { get; internal set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00024DD2 File Offset: 0x00022FD2
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x00024DDA File Offset: 0x00022FDA
		public int LinePosition { get; internal set; }

		// Token: 0x0600074F RID: 1871 RVA: 0x00024DE3 File Offset: 0x00022FE3
		public JsonSerializationException()
		{
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00024DEB File Offset: 0x00022FEB
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00024DF4 File Offset: 0x00022FF4
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024E00 File Offset: 0x00023000
		protected JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.HasLineInfo = (bool)info.GetValue("HasLineInfo", typeof(bool));
			this.LineNumber = (int)info.GetValue("LineNumber", typeof(int));
			this.LinePosition = (int)info.GetValue("LinePosition", typeof(int));
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00024E78 File Offset: 0x00023078
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("HasLineInfo", this.HasLineInfo, typeof(bool));
			info.AddValue("LineNumber", this.LineNumber, typeof(int));
			info.AddValue("LinePosition", this.LinePosition, typeof(int));
			base.GetObjectData(info, context);
		}
	}
}
