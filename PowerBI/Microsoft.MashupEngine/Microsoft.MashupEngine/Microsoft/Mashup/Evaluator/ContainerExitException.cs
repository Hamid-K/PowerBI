using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C4F RID: 7247
	[Serializable]
	public class ContainerExitException : Exception
	{
		// Token: 0x0600B4CE RID: 46286 RVA: 0x0024A898 File Offset: 0x00248A98
		public ContainerExitException(int pid, string containerLogText, Exception innerException = null)
			: this(string.Format(CultureInfo.InvariantCulture, "Container exit handle fired but exit code could not be found. PID: {0}.", pid), containerLogText, innerException)
		{
		}

		// Token: 0x0600B4CF RID: 46287 RVA: 0x0024A8B7 File Offset: 0x00248AB7
		public ContainerExitException(int pid, uint exitCode, string containerLogText, Exception innerException = null)
			: this(string.Format(CultureInfo.InvariantCulture, "Container unexpectedly exited. Exit code: 0x{0:X8}. PID: {1}", exitCode, pid), containerLogText, innerException)
		{
		}

		// Token: 0x0600B4D0 RID: 46288 RVA: 0x0024A8DD File Offset: 0x00248ADD
		private ContainerExitException(string message, string containerLogText, Exception innerException = null)
			: base(message, innerException)
		{
			base.HResult = -467599358;
			this.containerLogText = containerLogText;
		}

		// Token: 0x0600B4D1 RID: 46289 RVA: 0x0024A8F9 File Offset: 0x00248AF9
		protected ContainerExitException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.containerLogText = (string)info.GetValue("containerLogText", typeof(string));
		}

		// Token: 0x17002D35 RID: 11573
		// (get) Token: 0x0600B4D2 RID: 46290 RVA: 0x0024A923 File Offset: 0x00248B23
		public string ContainerLogText
		{
			get
			{
				return this.containerLogText;
			}
		}

		// Token: 0x0600B4D3 RID: 46291 RVA: 0x0024A92B File Offset: 0x00248B2B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("containerLogText", this.containerLogText, typeof(string));
			base.GetObjectData(info, context);
		}

		// Token: 0x0600B4D4 RID: 46292 RVA: 0x0024A950 File Offset: 0x00248B50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			if (this.containerLogText != null)
			{
				stringBuilder.AppendLine("Log Contents:");
				stringBuilder.AppendLine(this.containerLogText);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005BE7 RID: 23527
		private readonly string containerLogText;
	}
}
