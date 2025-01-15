using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000129 RID: 297
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class PopulateDatabaseFailedException : AdminProvisioningServiceException
	{
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003EAFC File Offset: 0x0003CCFC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0003EB04 File Offset: 0x0003CD04
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x0003EB0C File Offset: 0x0003CD0C
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0003EB15 File Offset: 0x0003CD15
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x0003EB1D File Offset: 0x0003CD1D
		public string Step
		{
			get
			{
				return this.m_step;
			}
			protected set
			{
				this.m_step = value;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003EB26 File Offset: 0x0003CD26
		public PopulateDatabaseFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003EB3F File Offset: 0x0003CD3F
		public PopulateDatabaseFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003EB56 File Offset: 0x0003CD56
		public PopulateDatabaseFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003EB74 File Offset: 0x0003CD74
		protected PopulateDatabaseFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PopulateDatabaseFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("PopulateDatabaseFailedException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.Step = (string)info.GetValue("PopulateDatabaseFailedException_Step", typeof(string));
			}
			catch (SerializationException)
			{
				this.Step = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("PopulateDatabaseFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public PopulateDatabaseFailedException(string databaseName, string step)
		{
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003ECA1 File Offset: 0x0003CEA1
		public PopulateDatabaseFailedException(string databaseName, string step, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003ECC6 File Offset: 0x0003CEC6
		public PopulateDatabaseFailedException(string databaseName, string step, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003ECF4 File Offset: 0x0003CEF4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003ED2B File Offset: 0x0003CF2B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003ED34 File Offset: 0x0003CF34
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PopulateDatabaseFailedException))
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

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003EE04 File Offset: 0x0003D004
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PopulateDatabaseFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("PopulateDatabaseFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("PopulateDatabaseFailedException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.Step != null)
			{
				info.AddValue("PopulateDatabaseFailedException_Step", this.Step, typeof(string));
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "PopulateDatabase for database '{0}' failed at {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : ((this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0003EF82 File Offset: 0x0003D182
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

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003EFA0 File Offset: 0x0003D1A0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003F10C File Offset: 0x0003D30C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003F115 File Offset: 0x0003D315
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003F11E File Offset: 0x0003D31E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003F10C File Offset: 0x0003D30C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003F128 File Offset: 0x0003D328
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

		// Token: 0x040003A0 RID: 928
		private string creationMessage;

		// Token: 0x040003A1 RID: 929
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003A2 RID: 930
		private string m_databaseName;

		// Token: 0x040003A3 RID: 931
		private string m_step;
	}
}
