using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200031C RID: 796
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class Base64UrlEncodingException : MonitoredException
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x00053180 File Offset: 0x00051380
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00053188 File Offset: 0x00051388
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00053190 File Offset: 0x00051390
		public string Arg
		{
			get
			{
				return this.m_arg;
			}
			protected set
			{
				this.m_arg = value;
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00053199 File Offset: 0x00051399
		public Base64UrlEncodingException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000531AD File Offset: 0x000513AD
		public Base64UrlEncodingException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000531C4 File Offset: 0x000513C4
		public Base64UrlEncodingException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000531E4 File Offset: 0x000513E4
		protected Base64UrlEncodingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("Base64UrlEncodingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Arg = (string)info.GetValue("Base64UrlEncodingException_Arg", typeof(string));
			}
			catch (SerializationException)
			{
				this.Arg = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("Base64UrlEncodingException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000532B8 File Offset: 0x000514B8
		public Base64UrlEncodingException(string arg, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Arg = arg;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000532D6 File Offset: 0x000514D6
		public Base64UrlEncodingException(string arg, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Arg = arg;
			this.ConstructorInternal(false);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000532FC File Offset: 0x000514FC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00053333 File Offset: 0x00051533
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005333C File Offset: 0x0005153C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(Base64UrlEncodingException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0005340C File Offset: 0x0005160C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("Base64UrlEncodingException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("Base64UrlEncodingException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Arg != null)
			{
				info.AddValue("Base64UrlEncodingException_Arg", this.Arg, typeof(string));
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0005348C File Offset: 0x0005168C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to parse args to Base64Url. {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Arg != null) ? this.Arg.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Arg != null) ? this.Arg.MarkIfInternal() : string.Empty) : ((this.Arg != null) ? this.Arg.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00053510 File Offset: 0x00051710
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

		// Token: 0x060016EE RID: 5870 RVA: 0x00053530 File Offset: 0x00051730
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Arg={0}", new object[] { (this.Arg != null) ? this.Arg.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Arg={0}", new object[] { (this.Arg != null) ? this.Arg.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Arg={0}", new object[] { (this.Arg != null) ? this.Arg.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005360F File Offset: 0x0005180F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00053618 File Offset: 0x00051818
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00053621 File Offset: 0x00051821
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005360F File Offset: 0x0005180F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005362C File Offset: 0x0005182C
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

		// Token: 0x04000810 RID: 2064
		private string creationMessage;

		// Token: 0x04000811 RID: 2065
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000812 RID: 2066
		private string m_arg;
	}
}
