using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200037A RID: 890
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class WindowsEventLogException : MonitoredException
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00064AAC File Offset: 0x00062CAC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00064AB4 File Offset: 0x00062CB4
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00064ABC File Offset: 0x00062CBC
		public string CaughtException
		{
			get
			{
				return this.m_caughtException;
			}
			protected set
			{
				this.m_caughtException = value;
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00064AC5 File Offset: 0x00062CC5
		public WindowsEventLogException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00064AD9 File Offset: 0x00062CD9
		public WindowsEventLogException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00064AF0 File Offset: 0x00062CF0
		public WindowsEventLogException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00064B10 File Offset: 0x00062D10
		protected WindowsEventLogException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("WindowsEventLogException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CaughtException = (string)info.GetValue("WindowsEventLogException_CaughtException", typeof(string));
			}
			catch (SerializationException)
			{
				this.CaughtException = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("WindowsEventLogException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00064BE4 File Offset: 0x00062DE4
		public WindowsEventLogException(string caughtException, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CaughtException = caughtException;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00064C02 File Offset: 0x00062E02
		public WindowsEventLogException(string caughtException, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CaughtException = caughtException;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00064C28 File Offset: 0x00062E28
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00064C5F File Offset: 0x00062E5F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00064C68 File Offset: 0x00062E68
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(WindowsEventLogException))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<EventingTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00064D38 File Offset: 0x00062F38
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("WindowsEventLogException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("WindowsEventLogException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CaughtException != null)
			{
				info.AddValue("WindowsEventLogException_CaughtException", this.CaughtException, typeof(string));
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00064DB8 File Offset: 0x00062FB8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Exception was thrown while trying to write to the windows event log: {0}.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.CaughtException != null) ? this.CaughtException.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CaughtException != null) ? this.CaughtException.MarkIfInternal() : string.Empty) : ((this.CaughtException != null) ? this.CaughtException.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00064E3C File Offset: 0x0006303C
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

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00064E5C File Offset: 0x0006305C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CaughtException={0}", new object[] { (this.CaughtException != null) ? this.CaughtException.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CaughtException={0}", new object[] { (this.CaughtException != null) ? this.CaughtException.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CaughtException={0}", new object[] { (this.CaughtException != null) ? this.CaughtException.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00064F3B File Offset: 0x0006313B
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00064F44 File Offset: 0x00063144
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00064F4D File Offset: 0x0006314D
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00064F3B File Offset: 0x0006313B
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00064F58 File Offset: 0x00063158
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

		// Token: 0x0400092E RID: 2350
		private string creationMessage;

		// Token: 0x0400092F RID: 2351
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000930 RID: 2352
		private string m_caughtException;
	}
}
