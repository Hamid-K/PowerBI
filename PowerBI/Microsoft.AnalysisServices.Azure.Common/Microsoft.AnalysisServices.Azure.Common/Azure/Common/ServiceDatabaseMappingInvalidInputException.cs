using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000BE RID: 190
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceDatabaseMappingInvalidInputException : StateManagerBaseException
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x000148E4 File Offset: 0x00012AE4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000148EC File Offset: 0x00012AEC
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000148F4 File Offset: 0x00012AF4
		public string Name
		{
			get
			{
				return this.m_name;
			}
			protected set
			{
				this.m_name = value;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000148FD File Offset: 0x00012AFD
		public ServiceDatabaseMappingInvalidInputException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00014911 File Offset: 0x00012B11
		public ServiceDatabaseMappingInvalidInputException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00014928 File Offset: 0x00012B28
		public ServiceDatabaseMappingInvalidInputException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00014948 File Offset: 0x00012B48
		protected ServiceDatabaseMappingInvalidInputException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceDatabaseMappingInvalidInputException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Name = (string)info.GetValue("ServiceDatabaseMappingInvalidInputException_Name", typeof(string));
			}
			catch (SerializationException)
			{
				this.Name = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceDatabaseMappingInvalidInputException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00014A1C File Offset: 0x00012C1C
		public ServiceDatabaseMappingInvalidInputException(string name, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Name = name;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00014A3A File Offset: 0x00012C3A
		public ServiceDatabaseMappingInvalidInputException(string name, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Name = name;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00014A60 File Offset: 0x00012C60
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00014A97 File Offset: 0x00012C97
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00014AA0 File Offset: 0x00012CA0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ServiceDatabaseMappingInvalidInputException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00014B70 File Offset: 0x00012D70
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceDatabaseMappingInvalidInputException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceDatabaseMappingInvalidInputException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Name != null)
			{
				info.AddValue("ServiceDatabaseMappingInvalidInputException_Name", this.Name, typeof(string));
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00014BF0 File Offset: 0x00012DF0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Input {0} cannot  be null", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Name != null) ? this.Name.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Name != null) ? this.Name.MarkIfInternal() : string.Empty) : ((this.Name != null) ? this.Name.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00014C6B File Offset: 0x00012E6B
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

		// Token: 0x060006FB RID: 1787 RVA: 0x00014C88 File Offset: 0x00012E88
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Name={0}", (this.Name != null) ? this.Name.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Name={0}", (this.Name != null) ? this.Name.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Name={0}", (this.Name != null) ? this.Name.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00014D4C File Offset: 0x00012F4C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00014D55 File Offset: 0x00012F55
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00014D5E File Offset: 0x00012F5E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00014D4C File Offset: 0x00012F4C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00014D68 File Offset: 0x00012F68
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

		// Token: 0x04000248 RID: 584
		private string creationMessage;

		// Token: 0x04000249 RID: 585
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400024A RID: 586
		private string m_name;
	}
}
