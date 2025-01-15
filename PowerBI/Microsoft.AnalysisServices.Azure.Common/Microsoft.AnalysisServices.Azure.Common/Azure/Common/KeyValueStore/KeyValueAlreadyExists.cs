using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.KeyValueStore
{
	// Token: 0x02000137 RID: 311
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class KeyValueAlreadyExists : MonitoredException
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x0004437C File Offset: 0x0004257C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00044384 File Offset: 0x00042584
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x0004438C File Offset: 0x0004258C
		public string Key
		{
			get
			{
				return this.m_key;
			}
			protected set
			{
				this.m_key = value;
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00044395 File Offset: 0x00042595
		public KeyValueAlreadyExists()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000443A9 File Offset: 0x000425A9
		public KeyValueAlreadyExists(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000443C0 File Offset: 0x000425C0
		public KeyValueAlreadyExists(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000443E0 File Offset: 0x000425E0
		protected KeyValueAlreadyExists(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("KeyValueAlreadyExists_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Key = (string)info.GetValue("KeyValueAlreadyExists_Key", typeof(string));
			}
			catch (SerializationException)
			{
				this.Key = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("KeyValueAlreadyExists_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000444B4 File Offset: 0x000426B4
		public KeyValueAlreadyExists(string key, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000444D2 File Offset: 0x000426D2
		public KeyValueAlreadyExists(string key, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000444F8 File Offset: 0x000426F8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00044530 File Offset: 0x00042730
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("KeyValueAlreadyExists_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("KeyValueAlreadyExists_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Key != null)
			{
				info.AddValue("KeyValueAlreadyExists_Key", this.Key, typeof(string));
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000445B0 File Offset: 0x000427B0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Key {0} Already Exists", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : ((this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0004462B File Offset: 0x0004282B
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00044648 File Offset: 0x00042848
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Key={0}", (this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0004470C File Offset: 0x0004290C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00044715 File Offset: 0x00042915
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0004471E File Offset: 0x0004291E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0004470C File Offset: 0x0004290C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00044728 File Offset: 0x00042928
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x040003CB RID: 971
		private string creationMessage;

		// Token: 0x040003CC RID: 972
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003CD RID: 973
		private string m_key;
	}
}
