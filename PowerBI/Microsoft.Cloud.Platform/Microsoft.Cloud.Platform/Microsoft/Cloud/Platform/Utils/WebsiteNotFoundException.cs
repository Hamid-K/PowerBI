using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000307 RID: 775
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class WebsiteNotFoundException : MonitoredException
	{
		// Token: 0x06001540 RID: 5440 RVA: 0x0004B7A8 File Offset: 0x000499A8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0004B7B0 File Offset: 0x000499B0
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x0004B7B8 File Offset: 0x000499B8
		public string WebsiteName
		{
			get
			{
				return this.m_websiteName;
			}
			protected set
			{
				this.m_websiteName = value;
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0004B7C1 File Offset: 0x000499C1
		public WebsiteNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0004B7D5 File Offset: 0x000499D5
		public WebsiteNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004B7EC File Offset: 0x000499EC
		public WebsiteNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004B80C File Offset: 0x00049A0C
		protected WebsiteNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("WebsiteNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.WebsiteName = (string)info.GetValue("WebsiteNotFoundException_WebsiteName", typeof(string));
			}
			catch (SerializationException)
			{
				this.WebsiteName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("WebsiteNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0004B8E0 File Offset: 0x00049AE0
		public WebsiteNotFoundException(string websiteName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.WebsiteName = websiteName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0004B8FE File Offset: 0x00049AFE
		public WebsiteNotFoundException(string websiteName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.WebsiteName = websiteName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0004B924 File Offset: 0x00049B24
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0004B95B File Offset: 0x00049B5B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0004B964 File Offset: 0x00049B64
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(WebsiteNotFoundException))
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

		// Token: 0x0600154C RID: 5452 RVA: 0x0004BA34 File Offset: 0x00049C34
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("WebsiteNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("WebsiteNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.WebsiteName != null)
			{
				info.AddValue("WebsiteNotFoundException_WebsiteName", this.WebsiteName, typeof(string));
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004BAB4 File Offset: 0x00049CB4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to locate the website '{0}' for querying", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.WebsiteName != null) ? this.WebsiteName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.WebsiteName != null) ? this.WebsiteName.MarkIfInternal() : string.Empty) : ((this.WebsiteName != null) ? this.WebsiteName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0004BB38 File Offset: 0x00049D38
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

		// Token: 0x0600154F RID: 5455 RVA: 0x0004BB58 File Offset: 0x00049D58
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "WebsiteName={0}", new object[] { (this.WebsiteName != null) ? this.WebsiteName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "WebsiteName={0}", new object[] { (this.WebsiteName != null) ? this.WebsiteName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "WebsiteName={0}", new object[] { (this.WebsiteName != null) ? this.WebsiteName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004BC37 File Offset: 0x00049E37
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004BC40 File Offset: 0x00049E40
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0004BC49 File Offset: 0x00049E49
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0004BC37 File Offset: 0x00049E37
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0004BC54 File Offset: 0x00049E54
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

		// Token: 0x040007D7 RID: 2007
		private string creationMessage;

		// Token: 0x040007D8 RID: 2008
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007D9 RID: 2009
		private string m_websiteName;
	}
}
