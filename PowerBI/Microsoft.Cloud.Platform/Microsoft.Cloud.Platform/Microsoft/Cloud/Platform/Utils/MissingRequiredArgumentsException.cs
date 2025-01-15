using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002FE RID: 766
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MissingRequiredArgumentsException : ApplicationSwitchesException
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00047F5C File Offset: 0x0004615C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00047F64 File Offset: 0x00046164
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00047F6C File Offset: 0x0004616C
		public string SwitchFullName
		{
			get
			{
				return this.m_switchFullName;
			}
			protected set
			{
				this.m_switchFullName = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00047F75 File Offset: 0x00046175
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00047F7D File Offset: 0x0004617D
		public string SwitchShortName
		{
			get
			{
				return this.m_switchShortName;
			}
			protected set
			{
				this.m_switchShortName = value;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00047F86 File Offset: 0x00046186
		public MissingRequiredArgumentsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00047F9F File Offset: 0x0004619F
		public MissingRequiredArgumentsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00047FB6 File Offset: 0x000461B6
		public MissingRequiredArgumentsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00047FD4 File Offset: 0x000461D4
		protected MissingRequiredArgumentsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MissingRequiredArgumentsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.SwitchFullName = (string)info.GetValue("MissingRequiredArgumentsException_SwitchFullName", typeof(string));
			}
			catch (SerializationException)
			{
				this.SwitchFullName = null;
			}
			try
			{
				this.SwitchShortName = (string)info.GetValue("MissingRequiredArgumentsException_SwitchShortName", typeof(string));
			}
			catch (SerializationException)
			{
				this.SwitchShortName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MissingRequiredArgumentsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000480E4 File Offset: 0x000462E4
		public MissingRequiredArgumentsException(string switchFullName, string switchShortName)
		{
			this.SwitchFullName = switchFullName;
			this.SwitchShortName = switchShortName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00048101 File Offset: 0x00046301
		public MissingRequiredArgumentsException(string switchFullName, string switchShortName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.SwitchFullName = switchFullName;
			this.SwitchShortName = switchShortName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00048126 File Offset: 0x00046326
		public MissingRequiredArgumentsException(string switchFullName, string switchShortName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.SwitchFullName = switchFullName;
			this.SwitchShortName = switchShortName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00048154 File Offset: 0x00046354
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0004818B File Offset: 0x0004638B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00048194 File Offset: 0x00046394
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MissingRequiredArgumentsException))
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

		// Token: 0x0600149A RID: 5274 RVA: 0x00048264 File Offset: 0x00046464
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MissingRequiredArgumentsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MissingRequiredArgumentsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.SwitchFullName != null)
			{
				info.AddValue("MissingRequiredArgumentsException_SwitchFullName", this.SwitchFullName, typeof(string));
			}
			if (this.SwitchShortName != null)
			{
				info.AddValue("MissingRequiredArgumentsException_SwitchShortName", this.SwitchShortName, typeof(string));
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00048308 File Offset: 0x00046508
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A mandatory switch was not provided; please specify '/{0}' or '/{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.SwitchFullName != null) ? this.SwitchFullName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.SwitchFullName != null) ? this.SwitchFullName.MarkIfInternal() : string.Empty) : ((this.SwitchFullName != null) ? this.SwitchFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.SwitchShortName != null) ? this.SwitchShortName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.SwitchShortName != null) ? this.SwitchShortName.MarkIfInternal() : string.Empty) : ((this.SwitchShortName != null) ? this.SwitchShortName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000483EE File Offset: 0x000465EE
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

		// Token: 0x0600149D RID: 5277 RVA: 0x0004840C File Offset: 0x0004660C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SwitchFullName={0}", new object[] { (this.SwitchFullName != null) ? this.SwitchFullName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SwitchFullName={0}", new object[] { (this.SwitchFullName != null) ? this.SwitchFullName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "SwitchFullName={0}", new object[] { (this.SwitchFullName != null) ? this.SwitchFullName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SwitchShortName={0}", new object[] { (this.SwitchShortName != null) ? this.SwitchShortName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SwitchShortName={0}", new object[] { (this.SwitchShortName != null) ? this.SwitchShortName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "SwitchShortName={0}", new object[] { (this.SwitchShortName != null) ? this.SwitchShortName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000485AE File Offset: 0x000467AE
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000485B7 File Offset: 0x000467B7
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000485C0 File Offset: 0x000467C0
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000485AE File Offset: 0x000467AE
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000485CC File Offset: 0x000467CC
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

		// Token: 0x040007BE RID: 1982
		private string creationMessage;

		// Token: 0x040007BF RID: 1983
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007C0 RID: 1984
		private string m_switchFullName;

		// Token: 0x040007C1 RID: 1985
		private string m_switchShortName;
	}
}
