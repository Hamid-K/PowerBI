using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000557 RID: 1367
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ManifestUnmappedParameterException : MonitoredException
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x00097A98 File Offset: 0x00095C98
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x00097AA0 File Offset: 0x00095CA0
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x00097AA8 File Offset: 0x00095CA8
		public string ParameterName
		{
			get
			{
				return this.m_parameterName;
			}
			protected set
			{
				this.m_parameterName = value;
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00097AB1 File Offset: 0x00095CB1
		public ManifestUnmappedParameterException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00097AC5 File Offset: 0x00095CC5
		public ManifestUnmappedParameterException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00097ADC File Offset: 0x00095CDC
		public ManifestUnmappedParameterException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00097AFC File Offset: 0x00095CFC
		protected ManifestUnmappedParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ManifestUnmappedParameterException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ParameterName = (string)info.GetValue("ManifestUnmappedParameterException_ParameterName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ParameterName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ManifestUnmappedParameterException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00097BD0 File Offset: 0x00095DD0
		public ManifestUnmappedParameterException(string parameterName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ParameterName = parameterName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00097BEE File Offset: 0x00095DEE
		public ManifestUnmappedParameterException(string parameterName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ParameterName = parameterName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x00097C14 File Offset: 0x00095E14
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00097C4B File Offset: 0x00095E4B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00097C54 File Offset: 0x00095E54
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ManifestUnmappedParameterException))
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00097D24 File Offset: 0x00095F24
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ManifestUnmappedParameterException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ManifestUnmappedParameterException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ParameterName != null)
			{
				info.AddValue("ManifestUnmappedParameterException_ParameterName", this.ParameterName, typeof(string));
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00097DA4 File Offset: 0x00095FA4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Parameters passed in do not contain parameter '{0}'.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.ParameterName != null) ? this.ParameterName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ParameterName != null) ? this.ParameterName.MarkIfInternal() : string.Empty) : ((this.ParameterName != null) ? this.ParameterName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x00097E28 File Offset: 0x00096028
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

		// Token: 0x06002A0E RID: 10766 RVA: 0x00097E48 File Offset: 0x00096048
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ParameterName={0}", new object[] { (this.ParameterName != null) ? this.ParameterName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ParameterName={0}", new object[] { (this.ParameterName != null) ? this.ParameterName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ParameterName={0}", new object[] { (this.ParameterName != null) ? this.ParameterName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00097F27 File Offset: 0x00096127
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00097F30 File Offset: 0x00096130
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00097F39 File Offset: 0x00096139
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00097F27 File Offset: 0x00096127
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00097F44 File Offset: 0x00096144
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

		// Token: 0x04000ECB RID: 3787
		private string creationMessage;

		// Token: 0x04000ECC RID: 3788
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000ECD RID: 3789
		private string m_parameterName;
	}
}
