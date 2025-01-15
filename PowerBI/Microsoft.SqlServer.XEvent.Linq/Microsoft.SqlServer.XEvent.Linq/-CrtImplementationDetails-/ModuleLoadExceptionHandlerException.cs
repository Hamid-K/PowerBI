using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00018CC8 File Offset: 0x00018CC8
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string text = "NestedException";
			this.NestedException = (Exception)info.GetValue(text, typeFromHandle);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001930C File Offset: 0x0001930C
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException)
			: base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00018BA4 File Offset: 0x00018BA4
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00018BC0 File Offset: 0x00018BC0
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00018BDC File Offset: 0x00018BDC
		public override string ToString()
		{
			string text;
			if (this.InnerException != null)
			{
				text = this.InnerException.ToString();
			}
			else
			{
				text = string.Empty;
			}
			string text2;
			if (this.NestedException != null)
			{
				text2 = this.NestedException.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			object[] array = new object[4];
			Type type = this.GetType();
			array[0] = type;
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			return string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00018C8C File Offset: 0x00018C8C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
		}

		// Token: 0x0400023B RID: 571
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x0400023C RID: 572
		private Exception <backing_store>NestedException;
	}
}
