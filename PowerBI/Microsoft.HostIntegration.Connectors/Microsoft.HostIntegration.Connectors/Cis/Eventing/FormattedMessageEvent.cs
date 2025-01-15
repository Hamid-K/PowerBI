using System;
using System.Reflection;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000487 RID: 1159
	public class FormattedMessageEvent : RDEventBase
	{
		// Token: 0x06002847 RID: 10311 RVA: 0x00078F11 File Offset: 0x00077111
		public FormattedMessageEvent()
		{
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000798C8 File Offset: 0x00077AC8
		public FormattedMessageEvent(string formatMessage, object[] args)
		{
			this.args = new object[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] is DateTime)
				{
					this.args[i] = ((DateTime)args[i]).ToString("o");
				}
				else if (args[i] != null && args[i].GetType().IsByRef && !(args[i] is string))
				{
					this.args[i] = args[i].ToString();
				}
				else
				{
					this.args[i] = args[i];
				}
			}
			this.Format = formatMessage;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00079962 File Offset: 0x00077B62
		private string GetArgOrNull(int i)
		{
			if (this.args == null || i < 0)
			{
				return null;
			}
			if (i >= this.args.Length)
			{
				return null;
			}
			if (this.args[i] == null)
			{
				return string.Empty;
			}
			return this.args[i].ToString();
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x0007999C File Offset: 0x00077B9C
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x000799A4 File Offset: 0x00077BA4
		[RDEventProperty]
		public string Format { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000799AD File Offset: 0x00077BAD
		[RDEventProperty]
		public string Argument0
		{
			get
			{
				return this.GetArgOrNull(0);
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000799B6 File Offset: 0x00077BB6
		[RDEventProperty]
		public string Argument1
		{
			get
			{
				return this.GetArgOrNull(1);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x000799BF File Offset: 0x00077BBF
		[RDEventProperty]
		public string Argument2
		{
			get
			{
				return this.GetArgOrNull(2);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000799C8 File Offset: 0x00077BC8
		[RDEventProperty]
		public string Argument3
		{
			get
			{
				return this.GetArgOrNull(3);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000799D1 File Offset: 0x00077BD1
		[RDEventProperty]
		public string Argument4
		{
			get
			{
				return this.GetArgOrNull(4);
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000799DA File Offset: 0x00077BDA
		[RDEventProperty]
		public string Argument5
		{
			get
			{
				return this.GetArgOrNull(5);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000799E3 File Offset: 0x00077BE3
		[RDEventProperty]
		public string Argument6
		{
			get
			{
				return this.GetArgOrNull(6);
			}
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000799EC File Offset: 0x00077BEC
		internal static string DoFormat(string format, object[] arguments)
		{
			if (FormattedMessageEvent.dele == null)
			{
				FormattedMessageEvent.dele = typeof(string).GetMethod("Format", new Type[]
				{
					typeof(string),
					typeof(object[])
				});
			}
			return FormattedMessageEvent.dele.Invoke(null, new object[] { format, arguments }) as string;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x00079A61 File Offset: 0x00077C61
		public string Message
		{
			get
			{
				if (this.args == null || this.args.Length == 0)
				{
					return this.Format;
				}
				return FormattedMessageEvent.DoFormat(this.Format, this.args);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x00079A8D File Offset: 0x00077C8D
		public int ArgsLength
		{
			get
			{
				return this.args.Length;
			}
		}

		// Token: 0x040017AB RID: 6059
		protected static MethodInfo dele;

		// Token: 0x040017AC RID: 6060
		protected object[] args;
	}
}
